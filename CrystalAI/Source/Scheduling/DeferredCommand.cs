// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// DeferredCommand.cs is part of Crystal AI.
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
  ///   This delegate is used for the process in DeferredCommand
  /// </summary>
  public delegate void CommandAction();

  /// <summary>
  ///   DeferredCommand is a command whose execution is delegated to a future time and/or executes
  ///   repeatedly.
  /// </summary>
  public class DeferredCommand {
    float _executionDelayMax;
    float _executionDelayMin;
    float _firstExecutionDelayMax;
    float _firstExecutionDelayMin;
    // This is by far the most common use.
    bool _isRepeating = true;
    CommandAction _process;
    long _timesExecuted;
    float _lastExecutionTime;
    float _lastUpdateDeltaTime;


    /// <summary>
    ///   Gets or sets a value indicating whether this instance is repeating.
    /// </summary>
    /// <value><c>true</c> if this instance is repeating; otherwise, <c>false</c>.</value>
    public bool IsRepeating {
      get { return _isRepeating; }
      set { _isRepeating = value; }
    }

    /// <summary>
    ///   Controls the minimum Time delay before this command is executed for the first time.
    /// </summary>
    /// <value>The first execution delay minimum.</value>
    public float InitExecutionDelayMin {
      get { return _firstExecutionDelayMin; }
      set {
        _firstExecutionDelayMin = value.ClampToPositive();
        _firstExecutionDelayMax = _firstExecutionDelayMax.ClampToLowerBound(_firstExecutionDelayMin);
      }
    }

    /// <summary>
    ///   Controls the maximum Time delay before this command is executed for the first Time.
    /// </summary>
    /// <value>The first execution delay max.</value>
    public float InitExecutionDelayMax {
      get { return _firstExecutionDelayMax; }
      set { _firstExecutionDelayMax = value.ClampToLowerBound(_firstExecutionDelayMin); }
    }

    /// <summary>
    ///   This determines how much Time to wait (in seconds), before executing the scheduled item
    ///   for the first Time. This is ignored after the first execution
    /// </summary>
    /// <value>The first execution delay.</value>
    public float InitExecutionDelay {
      get { return PcgExtended.Default.NextFloat(_firstExecutionDelayMin, _firstExecutionDelayMax); }
    }

    /// <summary>
    ///   The minimum Time to wait (in seconds) before executing this command again.
    /// </summary>
    /// <value>The next execution delay minimum.</value>
    public float ExecutionDelayMin {
      get { return _executionDelayMin; }
      set { _executionDelayMin = value.ClampToPositive(); }
    }

    /// <summary>
    ///   The maximum Time to wait (in seconds) before executing this command again.
    /// </summary>
    /// <value>The next execution delay max.</value>
    public float ExecutionDelayMax {
      get { return _executionDelayMax; }
      set { _executionDelayMax = value.ClampToLowerBound(_executionDelayMin); }
    }

    /// <summary>
    ///   The Time to wait in seconds to execute again the scheduled item.
    /// </summary>
    /// <value>The next execution delay.</value>
    public float ExecutionDelay {
      get { return PcgExtended.Default.NextFloat(_executionDelayMin, _executionDelayMax); }
    }

    /// <summary>
    ///   The number of times the Execute() command was called.
    /// </summary>
    public long TimesExecuted {
      get { return _timesExecuted; }
    }

    /// <summary>
    ///   The time of the last execution in seconds since the start of the application.
    /// </summary>
    public float LastExecution {
      get { return _lastExecutionTime; }
    }

    /// <summary>
    ///   The time since the last update in seconds.
    /// </summary>
    public float TimeSinceLastUpdate {
      get { return CrTime.Time - _lastExecutionTime; }
    }

    /// <summary>
    ///   This returns the time difference between the last update time and the second to
    ///   last update time.
    /// </summary>
    public float LastUpdateDeltaTime {
      get { return _lastUpdateDeltaTime; }
    }

    public void Execute() {
      _process();
      var lastExecOld = _lastExecutionTime;
      _lastExecutionTime = CrTime.Time;
      _lastUpdateDeltaTime = _lastExecutionTime - lastExecOld;
      unchecked {
        _timesExecuted++;
      }
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="DeferredCommand"/> class.
    /// </summary>
    /// <param name="process">Process.</param>
    public DeferredCommand(CommandAction process) {
      if(process == null)
        throw new ProcessNullException();

      _process = process;
    }

    internal class ProcessNullException : Exception {
    }
  }

}