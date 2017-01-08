// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IConsideration.cs is part of Crystal AI.
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

  /// <summary>Interface for contextual scorers.</summary>
  /// <seealso cref="T:Crystal.ICompositeConsideration"/>
  public interface IConsideration : IAiPrototype<IConsideration> {
    /// <summary>
    ///   A unique named identifier for this consideration.
    /// </summary>
    string NameId { get; }

    /// <summary>
    ///   Gets or sets the default utility.
    /// </summary>
    Utility DefaultUtility { get; set; }

    /// <summary>
    ///   Returns the utility for this consideration.
    /// </summary>
    Utility Utility { get; }

    /// <summary>
    ///   The weight of this consideration.
    /// </summary>
    float Weight { get; set; }

    /// <summary>Calculates the utility given the specified context.</summary>
    /// <param name="context">The context.</param>
    /// <returns>The utility.</returns>
    void Consider(IContext context);
  }

}