// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IUtilityAI.cs is part of Crystal AI.
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
  /// Interface for all utility based AIs. 
  /// </summary>
  /// <seealso cref="Crystal.IAiPrototype{Crystal.IUtilityAi}" />
  public interface IUtilityAi : IAiPrototype<IUtilityAi> {
    /// <summary>
    ///   A unique name identifier for this AI.
    /// </summary>
    string NameId { get; }

    /// <summary>
    /// Returns the selector associated with this AI. Also see <see cref="T:Crystal.ISelector"/>.
    /// </summary>
    ISelector Selector { get; set; }

    /// <summary>
    /// Adds the behaviour.
    /// </summary>
    /// <param name="behaviourId">The behaviour identifier.</param>
    /// <returns>Returns true if the requested behaviour was added to the AI, false otherwise.</returns>
    bool AddBehaviour(string behaviourId);

    /// <summary>
    /// Removes the behaviour.
    /// </summary>
    /// <param name="behaviourId">The behaviour identifier.</param>
    /// <returns>Returns true if the behaviour was removed from the AI, false otherwise.</returns>
    bool RemoveBehaviour(string behaviourId);

    /// <summary>
    /// Enters a selection process that selects one of the contained behaviours. This 
    /// in turn selects one option within the selected behaviour and returns the action
    /// associated with that option.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>The action associated with the selected option.</returns>
    IAction Select(IContext context);
  }

}