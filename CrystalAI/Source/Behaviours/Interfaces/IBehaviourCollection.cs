// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IBehaviourCollection.cs is part of Crystal AI.
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
  ///   Interface for BehaviourCollections. A behaviour collection contains behaviours and all sub-collections
  ///   required for these behaviours. Namely, options, considerations and actions.
  /// </summary>
  public interface IBehaviourCollection {
    /// <summary>
    ///   The <see cref="T:Crystal.IOptionCollection"/> used to construct this collection.
    /// </summary>
    IOptionCollection Options { get; }

    /// <summary>
    ///   Adds the given behaviour to this collection.
    /// </summary>
    /// <param name="behaviour">The behaviour.</param>
    /// <returns>
    ///   <c>true</c> if the behaviour was added to the collection; otherwise, <c>false</c>.
    /// </returns>
    bool Add(IBehaviour behaviour);

    /// <summary>
    ///   Determines whether a behaviour with the specified name identifier exists within this collection.
    /// </summary>
    /// <param name="behaviourId">The name identifier.</param>
    /// <returns>
    ///   <c>true</c> if a behaviour with the specified identifier exists within the collection;
    ///   otherwise, <c>false</c>.
    /// </returns>
    bool Contains(string behaviourId);

    /// <summary>
    ///   Removes all behaviours from this collection.
    /// </summary>
    void Clear();

    /// <summary>
    ///   Removes all behaviours from this collection. Additionally all options are removed from
    ///   the <see cref="T:Crystal.IOptionCollection"/> used in constructing this collection as well
    ///   as all considerations and actions are removed from their respective collections.
    /// </summary>
    void ClearAll();

    /// <summary>
    ///   Creates a behaviour with the specified identifier if such a behaviour exists;
    ///   otherwise returns <c>null</c>.
    /// </summary>
    /// <param name="behaviourId">The behaviour identifier.</param>
    IBehaviour Create(string behaviourId);
  }

}