// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IAICollection.cs is part of Crystal AI.
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
  ///   Interface for AiCollections. AiCollections contain the building blocks for
  ///   spawning concrete AI instances. An AiCollection also contains all sub-collections
  ///   used in its composition.
  /// </summary>
  public interface IAiCollection {
    /// <summary>
    ///   All behaviours available to this AiCollection.
    /// </summary>
    IBehaviourCollection Behaviours { get; }

    /// <summary>
    ///   All options available to this AiCollection.
    /// </summary>
    IOptionCollection Options { get; }

    /// <summary>
    ///   All considerations available to this AiCollection.
    /// </summary>
    IConsiderationCollection Considerations { get; }

    /// <summary>
    ///   All actions available to this AiCollection.
    /// </summary>
    IActionCollection Actions { get; }

    /// <summary>
    ///   Adds the specified AI.
    /// </summary>
    /// <param name="ai">The ai.</param>
    /// <returns>Returns true if the AI was successfully added false otherwise.</returns>
    bool Add(IUtilityAi ai);

    /// <summary>
    ///   Determines whether [contains] [the specified name identifier].
    /// </summary>
    /// <param name="nameId">The name identifier.</param>
    /// <returns>
    ///   <c>true</c> if [contains] [the specified name identifier]; otherwise, <c>false</c>.
    /// </returns>
    bool Contains(string nameId);

    /// <summary>
    ///   Gets the ai.
    /// </summary>
    /// <param name="nameId">The name identifier.</param>
    /// <returns></returns>
    IUtilityAi GetAi(string nameId);

    /// <summary>
    ///   Removes all AIs in this collection.
    /// </summary>
    void Clear();

    /// <summary>
    ///   Clears everything, i.e. all actions, considerations, options, behaviours and AIs.
    /// </summary>
    void ClearAll();

    /// <summary>
    ///   Creates a new utility AI instance if the requested AI named nameId exists within
    ///   the AiCollection, otherwise this returns null.
    /// </summary>
    /// <param name="nameId"></param>
    /// <returns></returns>
    IUtilityAi Create(string nameId);
  }

}