// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IEvaluator.cs is part of Crystal AI.
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
  ///   Utility evaluator interface.
  /// </summary>
  public interface IEvaluator {
    /// <summary>
    ///   The first point in terms of x!
    /// </summary>
    Pointf PtA { get; }

    /// <summary>
    ///   The second point in terms of x!
    /// </summary>
    Pointf PtB { get; }

    /// <summary>
    ///   Gets the minimum x.
    /// </summary>
    /// <value>The minimum x.</value>
    float MinX { get; }

    /// <summary>
    ///   Gets the max x.
    /// </summary>
    /// <value>The max x.</value>
    float MaxX { get; }

    /// <summary>
    ///   Gets the minimum y.
    /// </summary>
    /// <value>The minimum y.</value>
    float MinY { get; }

    /// <summary>
    ///   Gets the max y.
    /// </summary>
    /// <value>The max y.</value>
    float MaxY { get; }

    /// <summary>
    ///   Gets the X interval.
    /// </summary>
    /// <value>The X interval.</value>
    Interval<float> XInterval { get; }

    /// <summary>
    ///   Gets the Y interval.
    /// </summary>
    /// <value>The Y interval.</value>
    Interval<float> YInterval { get; }

    /// <summary>
    ///   Returns the utility value for the specified x.
    /// </summary>
    /// <param name="x">The x value.</param>
    float Evaluate(float x);
  }

}