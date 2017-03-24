// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// DecisionMaker.cs is part of Crystal AI.
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
  public sealed class DecisionMaker : DecisionMakerBase {
    IScheduler _aiScheduler;
    
    DeferredCommand _thinkCommand;
    IDeferredCommandHandle _thinkCommandHandle;
    
    DeferredCommand _updateCommand;
    IDeferredCommandHandle _updateCommandHandle;

    /// <summary>
    /// Called after <see cref="M:Crystal.DecisionMakerBase.Start" />.
    /// </summary>
    protected override void OnStart() {
      _thinkCommandHandle = _aiScheduler.ThinkStream.Add(_thinkCommand);
      _updateCommandHandle = _aiScheduler.UpdateStream.Add(_updateCommand);
    }

    /// <summary>
    /// Called after <see cref="M:Crystal.DecisionMakerBase.Stop" />.
    /// </summary>
    protected override void OnStop() {
      _thinkCommandHandle.IsActive = false;
      _updateCommandHandle.IsActive = false;
    }

    /// <summary>
    /// Called after <see cref="M:Crystal.DecisionMakerBase.Pause" />.
    /// </summary>
    protected override void OnPause() {
      _thinkCommandHandle.Pause();
      _updateCommandHandle.Pause();
    }

    /// <summary>
    /// Called after <see cref="M:Crystal.DecisionMakerBase.Resume" />.
    /// </summary>
    protected override void OnResume() {
      _thinkCommandHandle.Resume();
      _updateCommandHandle.Resume();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DecisionMaker"/> class.
    /// </summary>
    /// <param name="uai">The uai.</param>
    /// <param name="contextProvider">The context provider.</param>
    /// <param name="aiScheduler">The ai scheduler.</param>
    /// <exception cref="Crystal.DecisionMaker.SchedulerNullException"></exception>
    public DecisionMaker(IUtilityAi uai, IContextProvider contextProvider, IScheduler aiScheduler)
      : base(uai, contextProvider) {
      if(aiScheduler == null)
        throw new SchedulerNullException();

      InitializeThinkCommand();
      InitializeUpdateCommand();
      _aiScheduler = aiScheduler;
    }

    void InitializeThinkCommand() {
      _thinkCommand = new DeferredCommand(Think) {
        InitExecutionDelayInterval = InitThinkDelay,
        ExecutionDelayInterval = ThinkDelay
      };
    }

    void InitializeUpdateCommand() {
      _updateCommand = new DeferredCommand(Update) {
        InitExecutionDelayInterval = InitUpdateDelay,
        ExecutionDelayInterval = UpdateDelay
      };
    }

    internal class SchedulerNullException : Exception {
    }
  }

}