// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ActionStatus.cs is part of Crystal AI.
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
namespace Crystal {

  /// <summary>
  ///   AI Action execution status.
  /// </summary>
  public enum ActionStatus {
    /// <summary>
    ///   AI Action failed to execute.
    /// </summary>
    Failure = 0,

    /// <summary>
    ///   The AI Action executed successfully.
    /// </summary>
    Success = 1,

    /// <summary>
    ///   The AI Action is still running.
    /// </summary>
    Running = 2,

    /// <summary>
    ///   The AI is... idle. This is the initial state of all actions. Actions never enter
    ///   this state again after the first execution.
    /// </summary>
    Idle = 3
  }

}