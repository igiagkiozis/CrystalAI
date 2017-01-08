// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IDeferredCommand.cs is part of Crystal AI.
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
  ///   This delegate is used for
  /// </summary>
  public delegate void CommandAction();

  /// <summary>
  ///   Interface for commands whose execution is temporally decoupled from their creation.
  /// </summary>
  public interface IDeferredCommand {
    /// <summary>
    ///   If true this command will be executed repeatedly.
    /// </summary>
    bool IsRepeating { get; set; }

    /// <summary>
    ///   Controls the minimum initial time delay before this command is executed.
    /// </summary>
    /// <value>The minimum initial execution delay in seconds.</value>
    float InitExecutionDelayMin { get; set; }

    /// <summary>
    ///   Controls the maximum initial time delay before this command is executed.
    /// </summary>
    /// <value>The maximum initial execution delay in seconds.</value>
    float InitExecutionDelayMax { get; set; }

    /// <summary>
    ///   The initial execution delay. This is a uniform random value in the interval
    ///   [InitExecutionDelayMin, InitExecutionDelayMax].
    /// </summary>
    /// <value>The initial execution delay in seconds.</value>
    float InitExecutionDelay { get; }

    /// <summary>
    ///   The minimum time to wait before executing this command again.
    /// </summary>
    /// <value>Minimum execution delay in seconds.</value>
    float ExecutionDelayMin { get; set; }

    /// <summary>
    ///   The maximum time to wait before executing this command again.
    /// </summary>
    /// <value>Maximum execution delay in seconds.</value>
    float ExecutionDelayMax { get; set; }

    /// <summary>
    ///   The time to wait to execute again this command. This is a uniform random value
    ///   in the interval [ExecutionDelayMin, ExecutionDelayMax].
    /// </summary>
    /// <value>The next execution delay in seconds.</value>
    float ExecutionDelay { get; }


    /// <summary>
    ///   This action is what runs upon executing this command.
    /// </summary>
    CommandAction Process { get; }

    /// <summary>
    ///   Execute the command.
    /// </summary>
    void Execute();
  }

}