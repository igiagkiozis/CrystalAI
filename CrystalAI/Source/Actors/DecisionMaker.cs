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

    float _initThinkDelayMax;

    float _initThinkDelayMin;

    float _initUpdateDelayMax;

    float _initUpdateDelayMin;
    IDeferredCommand _thinkCommand;
    IDeferredCommandHandle _thinkCommandHandle;

    float _thinkDelayMax;

    float _thinkDelayMin;
    IDeferredCommand _updateCommand;
    IDeferredCommandHandle _updateCommandHandle;

    float _updateDelayMax;

    float _updateDelayMin;

    public float InitThinkDelayMin {
      get { return _initThinkDelayMin; }
      set {
        _initThinkDelayMin = value.ClampToPositive();
        _initThinkDelayMax = _initThinkDelayMax.ClampToLowerBound(_initThinkDelayMin);
        _thinkCommand.InitExecutionDelayMin = _initThinkDelayMin;
      }
    }

    public float InitThinkDelayMax {
      get { return _initThinkDelayMax; }
      set {
        _initThinkDelayMax = value.ClampToLowerBound(_initThinkDelayMin);
        _thinkCommand.InitExecutionDelayMax = _initThinkDelayMax;
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

    public float InitUpdateDelayMin {
      get { return _initUpdateDelayMin; }
      set {
        _initUpdateDelayMin = value.ClampToPositive();
        _initUpdateDelayMax = _initUpdateDelayMax.ClampToLowerBound(_initUpdateDelayMin);
        _updateCommand.InitExecutionDelayMin = _initUpdateDelayMin;
      }
    }

    public float InitUpdateDelayMax {
      get { return _initUpdateDelayMax; }
      set {
        _initUpdateDelayMax = value.ClampToLowerBound(_initUpdateDelayMin);
        _updateCommand.InitExecutionDelayMax = _initUpdateDelayMax;
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
        InitExecutionDelayMin = InitThinkDelayMin,
        InitExecutionDelayMax = InitThinkDelayMin,
        ExecutionDelayMin = ThinkDelayMin,
        ExecutionDelayMax = ThinkDelayMax
      };
    }

    void InitializeUpdateCommand() {
      _updateCommand = new DeferredCommand(Update) {
        InitExecutionDelayMin = InitUpdateDelayMin,
        InitExecutionDelayMax = InitUpdateDelayMin,
        ExecutionDelayMin = UpdateDelayMin,
        ExecutionDelayMax = UpdateDelayMax
      };
    }

    internal class SchedulerNullException : Exception {
    }
  }

}