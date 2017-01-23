// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IActionCollection.cs is part of Crystal AI.
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
  ///   A collection of <see cref="T:Crystal.IAction"/>s.
  /// </summary>
  public interface IActionCollection {
    /// <summary>
    ///   Adds the specified action.
    /// </summary>
    /// <param name="action">The action.</param>
    /// <returns>
    ///   <c>true</c> if the action was successfully added to the collection, <c>false</c> otherwise.
    /// </returns>
    bool Add(IAction action);

    /// <summary>
    ///   Determines whether this collection contains an action with the specified identifier (actionId).
    /// </summary>
    /// <param name="actionId">The action identifier.</param>
    /// <returns>
    ///   <c>true</c> if an action with the specified identifier exists within the collection;
    ///   otherwise, <c>false</c>.
    /// </returns>
    bool Contains(string actionId);

    /// <summary>
    ///   Removes all actions in this collection.
    /// </summary>
    void Clear();

    /// <summary>
    ///   Creates the specified action identifier.
    /// </summary>
    /// <param name="actionId">The action identifier.</param>
    /// <returns>
    ///   <see cref="T:Crystal.IAction"/> if an action with the specified identifier exists,
    ///   <c>null</c> otherwise.
    /// </returns>
    IAction Create(string actionId);
  }

}