// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// DecisionMakerBase.cs is part of Crystal AI.
//  
// Crystal AI is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//  
// Crystal AI is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Crystal AI.  If not, see <http://www.gnu.org/licenses/>.
using System;


namespace Crystal {

  /// <summary>
  ///   The decision maker, every agent will have one or more of these to execute different AIs.
  /// </summary>
  /// <seealso cref="T:Crystal.IDecisionMaker"/>
  public abstract class DecisionMakerBase : IDecisionMaker {
    const int MaxRecursions = 100;
    IUtilityAi _ai;
    IContextProvider _contextProvider;
    IAction _currentAction;
    IContext _currentContext;
    int _recursionCounter;
    ITransition _transitionAction;

    Interval<float> _initThinkDelay = Interval.Create(0f);
    // Default decision making frequency 10Hz, i.e. 10 times per second.
    Interval<float> _thinkDelay = Interval.Create(0.1f);
    Interval<float> _initUpdateDelay = Interval.Create(0f);
    // Default update frequency ~60Hz (or 60 Frames per second).
    Interval<float> _updateDelay = Interval.Create(0.0167f);

    /// <summary>
    ///   The state of the decision maker.
    /// </summary>
    public DecisionMakerState State { get; protected set; }

    /// <summary>
    ///   The initial think delay interval in seconds. If this is a point interval (e.g. [a,a])
    ///   the delay is deterministic, always i.e. equal to the given point.
    /// </summary>
    public Interval<float> InitThinkDelay {
      get { return _initThinkDelay; }
      set { _initThinkDelay = value.ClampToPositive(); }
    }

    /// <summary>
    ///   The think delay interval in seconds. If this is a point interval (e.g. [a,a]) the delay
    ///   is deterministic, i.e. always equal to the given point.
    /// </summary>
    public Interval<float> ThinkDelay {
      get { return _thinkDelay; }
      set { _thinkDelay = value.ClampToPositive(); }
    }

    /// <summary>
    ///   The initial update delay interval in seconds. If this is a point interval (e.g. [a,a])
    ///   the delay is deterministic, i.e. always equal to the given point.
    /// </summary>
    public Interval<float> InitUpdateDelay {
      get { return _initUpdateDelay; }
      set { _initUpdateDelay = value.ClampToPositive(); }
    }

    /// <summary>
    ///   The update delay interval in seconds. If this is a point interval (e.g. [a,a]) the
    ///   delay is deterministic, i.e. always equal to the given point.
    /// </summary>
    public Interval<float> UpdateDelay {
      get { return _updateDelay; }
      set { _updateDelay = value.ClampToPositive(); }
    }

    /// <summary>
    ///   Starts the associated AI and sets the decision maker state to
    ///   <see cref="F:Crystal.DecisionMakerState.Running"/> informing the
    ///   <see cref="T:Crystal.IScheduler"/> that this AI should be executed.
    ///   Don't forget to invoke in your Update loop <see cref="M:Crystal.IScheduler.Tick()"/>
    ///   otherwise the AI will never actually run!
    /// </summary>
    public void Start() {
      if(State != DecisionMakerState.Stopped)
        return;

      State = DecisionMakerState.Running;
      OnStart();
    }

    /// <summary>
    ///   Stops the associated AI and sets the decision maker state to
    ///   <see cref="F:Crystal.DecisionMakerState.Stopped"/>.
    /// </summary>
    public void Stop() {
      if(State == DecisionMakerState.Stopped)
        return;

      State = DecisionMakerState.Stopped;
      OnStop();
    }

    /// <summary>
    ///   Pauses the associated AI and the decision maker state to
    ///   <see cref="F:Crystal.DecisionMakerState.Paused"/>.
    /// </summary>
    public void Pause() {
      if(State != DecisionMakerState.Running)
        return;

      State = DecisionMakerState.Paused;
      OnPause();
    }

    /// <summary>
    ///   Resumes execution of the associated AI and sets the decision maker state to
    ///   <see cref="F:Crystal.DecisionMakerState.Running"/>.
    /// </summary>
    public void Resume() {
      if(State != DecisionMakerState.Paused)
        return;

      State = DecisionMakerState.Running;
      OnResume();
    }

    /// <summary>
    ///   Makes a decision on what should be the next action to be executed.
    /// </summary>
    public void Think() {
      if(ActionStillRunning())
        return;

      if(CouldNotUpdateContext())
        return;

      if(AiDidSelectAction()) {
        while(IsTransition())
          ConnectorSelectAction();

        ExecuteCurrentAction();
      }
    }

    /// <summary>
    ///   Updates the action selected by Think() - that is if it needs updating, otherwise this will
    ///   simply return.
    /// </summary>
    public void Update() {
      if(CouldNotUpdateContext())
        return;

      ExecuteCurrentAction();
    }

    /// <summary>
    ///   Called after <see cref="M:Crystal.DecisionMakerBase.Start"/>.
    /// </summary>
    protected abstract void OnStart();

    /// <summary>
    ///   Called after <see cref="M:Crystal.DecisionMakerBase.Stop"/>.
    /// </summary>
    protected abstract void OnStop();

    /// <summary>
    ///   Called after <see cref="M:Crystal.DecisionMakerBase.Pause"/>.
    /// </summary>
    protected abstract void OnPause();

    /// <summary>
    ///   Called after <see cref="M:Crystal.DecisionMakerBase.Resume"/>.
    /// </summary>
    protected abstract void OnResume();

    /// <summary>
    ///   Initializes a new instance of the <see cref="DecisionMakerBase"/> class.
    /// </summary>
    /// <param name="ai">The ai.</param>
    /// <param name="contextProvider">The context provider.</param>
    /// <exception cref="Crystal.DecisionMakerBase.UtilityAiNullException"></exception>
    /// <exception cref="Crystal.DecisionMakerBase.ContextProviderNullException"></exception>
    protected DecisionMakerBase(IUtilityAi ai, IContextProvider contextProvider) {
      if(ai == null)
        throw new UtilityAiNullException();
      if(contextProvider == null)
        throw new ContextProviderNullException();

      _ai = ai;
      _contextProvider = contextProvider;
      State = DecisionMakerState.Stopped;
    }

    bool ActionStillRunning() {
      return _currentAction?.ActionStatus == ActionStatus.Running;
    }

    bool CouldNotUpdateContext() {
      _recursionCounter = 0;
      _currentContext = _contextProvider.Context();
      return _currentContext == null;
    }

    bool AiDidSelectAction() {
      _currentAction = _ai.Select(_currentContext);
      return _currentAction != null;
    }

    bool IsTransition() {
      CheckForRecursions();
      _transitionAction = _currentAction as ITransition;
      return _transitionAction != null;
    }

    void CheckForRecursions() {
      _recursionCounter++;
      if(_recursionCounter >= MaxRecursions)
        throw new PotentialCircularDependencyException(_recursionCounter);
    }

    void ConnectorSelectAction() {
      _currentAction = _transitionAction.Select(_currentContext);
    }

    void ExecuteCurrentAction() {
      if(_currentAction == null)
        return;

      _currentAction.Execute(_currentContext);
      if(_currentAction.ActionStatus != ActionStatus.Running)
        _currentAction = null;
    }

    internal class UtilityAiNullException : Exception {
    }

    internal class ContextProviderNullException : Exception {
    }

    internal class PotentialCircularDependencyException : Exception {
      int _loopCount;

      public override string Message {
        get {
          return string.Format("The Think() loop completed {0} iterations without " +
                               "reaching to an executable Action! It appears that there is a circular " +
                               "dependency at play here.",
                               _loopCount);
        }
      }

      public PotentialCircularDependencyException(int loopCount) : base(loopCount.ToString()) {
        _loopCount = loopCount;
      }
    }
  }

}