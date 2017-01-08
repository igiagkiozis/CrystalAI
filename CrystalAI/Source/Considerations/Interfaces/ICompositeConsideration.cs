// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ICompositeConsideration.cs is part of Crystal AI.
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
  ///   Interface for considerations that themselves calculate a Utility based on the Utility of
  ///   <see cref="T:Crystal.IConsideration"/>s.
  /// </summary>
  /// <seealso cref="T:Crystal.Option"/>
  public interface ICompositeConsideration : IConsideration {
    /// <summary>
    ///   The measure to be used to evaluate the utility of this consideration.
    /// </summary>
    IMeasure Measure { get; set; }

    /// <summary>
    ///   AddConsideration the specified consideration.
    /// </summary>
    bool AddConsideration(IConsideration consideration);

    /// <summary>
    ///   AddConsideration the consideration associated with the given ID.
    /// </summary>
    bool AddConsideration(string considerationId);
  }

}