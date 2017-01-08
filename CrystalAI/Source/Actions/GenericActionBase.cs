// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// GenericActionBase.cs is part of Crystal AI.
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
using System.Diagnostics;


namespace Crystal {

  public class ActionBase<TContext> : IAction where TContext : class, IContext {
    readonly IActionCollection _collection;
    readonly Stopwatch _cooldownTimer = new Stopwatch();
    float _cooldown;
    float _startedTime;
    ActionStatus _actionStatus = ActionStatus.Idle;

    /// <summary>
    ///   A string alias for ID.
    /// </summary>
    public string NameId { get; }

    /// <summary>
    ///   The Time that this action has been running for since it has been started. If the task is currently
    ///   on pause, this will return the difference pauseTime - startTime.
    /// </summary>
    public float ElapsedTime {
      get {
        if(ActionStatus == ActionStatus.Running)
          return CrTime.Time - _startedTime;

        return 0f;
      }
    }

    /// <summary>
    ///   The required cooldown Time needed before this action executes again.
    /// </summary>
    /// <value>The cooldown.</value>
    public float Cooldown {
      get { return _cooldown; }
      set { _cooldown = value.ClampToLowerBound(0.0f); }
    }

    /// <summary>
    ///   This returns true if the cooldown Time for this action has not elapsed.
    /// </summary>
    /// <value><c>true</c> if in cooldown; otherwise, <c>false</c>.</value>
    public bool InCooldown {
      get {
        if(ActionStatus == ActionStatus.Running ||
           ActionStatus == ActionStatus.Idle)
          return false;

        return (float)_cooldownTimer.Elapsed.TotalSeconds < _cooldown;
      }
    }

    /// <summary>
    ///   Gets the action status.
    /// </summary>
    /// <value>The action status.</value>
    public ActionStatus ActionStatus {
      get { return _actionStatus; }
      protected set { _actionStatus = value; }
    }

    /// <summary>Executes the action.</summary>
    /// <param name="context">The context.</param>
    public void Execute(TContext context) {
      if(CanExecute() == false)
        return;

      if(TryUpdate(context) == false) {
        _startedTime = CrTime.Time;
        ActionStatus = ActionStatus.Running;
        OnExecute(context);
      }
    }

    public virtual IAction Clone() {
      return new ActionBase<TContext>(this);
    }

    protected void EndInSuccess(IContext context) {
      EndInSuccess((TContext)context);
    }

    protected void EndInFailure(IContext context) {
      EndInFailure((TContext)context);
    }

    /// <summary>
    ///   End the action and sets its state to ActionState.Success.
    /// </summary>
    /// <param name="context">Context.</param>
    protected void EndInSuccess(TContext context) {
      if(ActionStatus != ActionStatus.Running)
        return;

      ActionStatus = ActionStatus.Success;
      FinalizeAction(context);
    }

    /// <summary>
    ///   End the action and sets its state to ActionState.Failure.
    /// </summary>
    /// <param name="context">Context.</param>
    protected void EndInFailure(TContext context) {
      if(ActionStatus != ActionStatus.Running)
        return;

      ActionStatus = ActionStatus.Failure;
      FinalizeAction(context);
    }

    /// <summary>
    ///   Executed once when the action starts.
    /// </summary>
    /// <param name="context">Context.</param>
    protected virtual void OnExecute(TContext context) {
      EndInSuccess(context);
    }

    /// <summary>
    ///   Executed on every action update, until <see cref="ActionBase.EndInSuccess"/> or
    ///   <see cref="ActionBase.EndInFailure"/> is called.
    /// </summary>
    /// <param name="context">Context.</param>
    protected virtual void OnUpdate(TContext context) {
    }

    /// <summary>
    ///   This can be used for cleanup. It executes after <see cref="ActionBase.EndInSuccess"/> or
    ///   <see cref="ActionBase.EndInFailure"/> is called.
    /// </summary>
    /// <param name="context">Context.</param>
    protected virtual void OnStop(TContext context) {
    }

    public ActionBase() {
    }

    protected ActionBase(ActionBase<TContext> other) {
      NameId = other.NameId;
      _collection = other._collection;
      Cooldown = other.Cooldown;
      _cooldownTimer = new Stopwatch();
    }

    public ActionBase(string nameId, IActionCollection collection) {
      if(string.IsNullOrEmpty(nameId))
        throw new NameIdEmptyOrNullException();
      if(collection == null)
        throw new ActionCollectionNullException();

      NameId = nameId;
      _collection = collection;
      AddSelfToCollection();
    }

    void IAction.Execute(IContext context) {
      Execute((TContext)context);
    }

    bool CanExecute() {
      if(InCooldown) {
        ActionStatus = ActionStatus.Failure;
        return false;
      }

      return true;
    }

    bool TryUpdate(TContext context) {
      if(ActionStatus == ActionStatus.Running) {
        OnUpdate(context);
        return true;
      }

      return false;
    }

    void FinalizeAction(TContext context) {
      OnStop(context);
      ResetAndStartCooldownTimer();
    }

    void ResetAndStartCooldownTimer() {
      _cooldownTimer.Reset();
      _cooldownTimer.Start();
    }

    void AddSelfToCollection() {
      if(_collection.Add(this) == false)
        throw new ActionAlreadyExistsInCollectionException(NameId);
    }

    internal class NameIdEmptyOrNullException : Exception {
    }

    internal class ActionCollectionNullException : Exception {
    }

    internal class ActionAlreadyExistsInCollectionException : Exception {
      public override string Message { get; }

      public ActionAlreadyExistsInCollectionException(string nameId) {
        Message = string.Format("Error: {0} already exists in the actions collection.", nameId);
      }
    }
  }

}