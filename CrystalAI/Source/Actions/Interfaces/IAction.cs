// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IAction.cs is part of Crystal AI.
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

  public interface IAction : IAiPrototype<IAction> {
    /// <summary>
    ///   Named unique identifier for this action.
    /// </summary>
    string NameId { get; }

    /// <summary>
    ///   The Time that this action has been running for since it has been started. If the task is currently
    ///   on pause, this will return the difference pauseTime - startTime.
    /// </summary>
    float ElapsedTime { get; }

    /// <summary>
    ///   The required cooldown Time needed before this action executes again.
    /// </summary>
    /// <value>The cooldown.</value>
    float Cooldown { get; set; }

    /// <summary>
    ///   This returns true if the cooldown Time for this action has not elapsed.
    /// </summary>
    /// <value><c>true</c> if in cooldown; otherwise, <c>false</c>.</value>
    bool InCooldown { get; }

    /// <summary>
    ///   Gets the action status.
    /// </summary>
    ActionStatus ActionStatus { get; }

    /// <summary>
    ///   Executes the AI Action.
    /// </summary>
    /// <param name="context">AI Context.</param>
    void Execute(IContext context);
  }

}