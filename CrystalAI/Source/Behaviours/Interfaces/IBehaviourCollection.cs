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
  /// Interface for BehaviourCollections. A behaviour collection contains behaviours and all sub-collections
  /// required for these behaviours. Namely, options, considerations and actions. 
  /// </summary>
  public interface IBehaviourCollection {
    /// <summary>
    /// The <see cref="T:Crystal.IOptionCollection"/> used to construct this collection. 
    /// </summary>
    IOptionCollection Options { get; }

    /// <summary>
    /// Adds the given behaviour to this collection.
    /// </summary>
    /// <param name="behaviour">The behaviour.</param>
    /// <returns>Returns true if the behaviour was successfully added, false otherwise.</returns>
    bool Add(IBehaviour behaviour);

    /// <summary>
    /// Determines whether [contains] [the specified name identifier].
    /// </summary>
    /// <param name="behaviourId">The name identifier.</param>
    /// <returns>
    ///   <c>true</c> if [contains] [the specified name identifier]; otherwise, <c>false</c>.
    /// </returns>
    bool Contains(string behaviourId);

    /// <summary>
    /// Clears this instance.
    /// </summary>
    void Clear();

    /// <summary>
    /// Clears all.
    /// </summary>
    void ClearAll();

    /// <summary>
    /// Creates the specified name identifier.
    /// </summary>
    /// <param name="behaviourId">The name identifier.</param>
    /// <returns></returns>
    IBehaviour Create(string behaviourId);
  }

}