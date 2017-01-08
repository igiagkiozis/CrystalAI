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

    float _firstThinkDelayMax;

    float _firstThinkDelayMin;

    float _firstUpdateDelayMax;

    float _firstUpdateDelayMin;
    IDeferredCommand _thinkCommand;
    IDeferredCommandHandle _thinkCommandHandle;

    float _thinkDelayMax;

    float _thinkDelayMin;
    IDeferredCommand _updateCommand;
    IDeferredCommandHandle _updateCommandHandle;

    float _updateDelayMax;

    float _updateDelayMin;

    public float FirstThinkDelayMin {
      get { return _firstThinkDelayMin; }
      set {
        _firstThinkDelayMin = value.ClampToPositive();
        _firstThinkDelayMax = _firstThinkDelayMax.ClampToLowerBound(_firstThinkDelayMin);
        _thinkCommand.InitExecutionDelayMin = _firstThinkDelayMin;
      }
    }

    public float FirstThinkDelayMax {
      get { return _firstThinkDelayMax; }
      set {
        _firstThinkDelayMax = value.ClampToLowerBound(_firstThinkDelayMin);
        _thinkCommand.InitExecutionDelayMax = _firstThinkDelayMax;
      }
    }

    public float ThinkDelayMin {
      get { return _thinkDelayMin; }
      set {
        _thinkDelayMin = value.ClampToPositive();
        _thinkDelayMax = _thinkDelayMax.ClampToLowerBound(_thinkDelayMin);
        _thinkCommand.ExecutionDelayMin = _thinkDelayMin;
      }
    }

    public float ThinkDelayMax {
      get { return _thinkDelayMax; }
      set {
        _thinkDelayMax = value.ClampToLowerBound(_thinkDelayMin);
        _thinkCommand.ExecutionDelayMax = _thinkDelayMax;
      }
    }

    public float FirstUpdateDelayMin {
      get { return _firstUpdateDelayMin; }
      set {
        _firstUpdateDelayMin = value.ClampToPositive();
        _firstUpdateDelayMax = _firstUpdateDelayMax.ClampToLowerBound(_firstUpdateDelayMin);
        _updateCommand.InitExecutionDelayMin = _firstUpdateDelayMin;
      }
    }

    public float FirstUpdateDelayMax {
      get { return _firstUpdateDelayMax; }
      set {
        _firstUpdateDelayMax = value.ClampToLowerBound(_firstUpdateDelayMin);
        _updateCommand.InitExecutionDelayMax = _firstUpdateDelayMax;
      }
    }

    public float UpdateDelayMin {
      get { return _updateDelayMin; }
      set {
        _updateDelayMin = value.ClampToPositive();
        _updateDelayMax = _updateDelayMax.ClampToLowerBound(_updateDelayMin);
        _updateCommand.ExecutionDelayMin = _updateDelayMin;
      }
    }

    public float UpdateDelayMax {
      get { return _updateDelayMax; }
      set {
        _updateDelayMax = value.ClampToLowerBound(_updateDelayMin);
        _updateCommand.ExecutionDelayMax = _updateDelayMax;
      }
    }

    /// <summary>Starts the AI.</summary>
    protected override void OnStart() {
      _thinkCommandHandle = _aiScheduler.ThinkStream.Add(_thinkCommand);
      _updateCommandHandle = _aiScheduler.UpdateStream.Add(_updateCommand);
    }

    protected override void OnStop() {
      _thinkCommandHandle.IsActive = false;
      _updateCommandHandle.IsActive = false;
    }

    protected override void OnPause() {
      _thinkCommandHandle.Pause();
      _updateCommandHandle.Pause();
    }

    protected override void OnResume() {
      _thinkCommandHandle.Resume();
      _updateCommandHandle.Resume();
    }

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
        InitExecutionDelayMin = FirstThinkDelayMin,
        InitExecutionDelayMax = FirstThinkDelayMin,
        ExecutionDelayMin = ThinkDelayMin,
        ExecutionDelayMax = ThinkDelayMax
      };
    }

    void InitializeUpdateCommand() {
      _updateCommand = new DeferredCommand(Update) {
        InitExecutionDelayMin = FirstUpdateDelayMin,
        InitExecutionDelayMax = FirstUpdateDelayMin,
        ExecutionDelayMin = UpdateDelayMin,
        ExecutionDelayMax = UpdateDelayMax
      };
    }

    internal class SchedulerNullException : Exception {
    }
  }

}