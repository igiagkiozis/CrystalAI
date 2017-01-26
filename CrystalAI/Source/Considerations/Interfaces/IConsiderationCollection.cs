// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IConsiderationCollection.cs is part of Crystal AI.
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
  ///   Interface for a collection that contains <see cref="T:Crystal.IConsideration"/>s
  ///   and <see cref="T:Crystal.ICompositeConsideration"/>s.
  /// </summary>
  public interface IConsiderationCollection {
    /// <summary>
    ///   Adds the specified consideration.
    /// </summary>
    /// <param name="consideration">The consideration.</param>
    /// <returns>
    ///   <c>true</c> if the consideration was successfully added to the collection, <c>false</c> otherwise.
    /// </returns>
    bool Add(IConsideration consideration);

    /// <summary>
    ///   Determines whether [contains] [the specified name identifier].
    /// </summary>
    /// <param name="nameId">The name identifier.</param>
    /// <c>true</c>
    /// if a consideration with the specified identifier exists within the collection;
    /// otherwise,
    /// <c>false</c>
    /// .
    /// </returns>
    bool Contains(string nameId);

    /// <summary>
    ///   Removes all considerations from this collection.
    /// </summary>
    void Clear();

    /// <summary>
    ///   Creates a consideration that has the specified identifier, if no such consideration
    ///   exists within this collection <c>null</c> is returned.
    /// </summary>
    /// <param name="considerationId">The consideration identifier.</param>
    IConsideration Create(string considerationId);
  }

}