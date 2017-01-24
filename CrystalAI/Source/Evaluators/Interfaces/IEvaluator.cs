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
    ///   The first point of the evaluator. The x-coordinate of this point will always
    ///   be strictly smaller than that of PtB.
    /// </summary>
    Pointf PtA { get; }

    /// <summary>
    ///   The second point of the evaluator. The x-coordinate of this point will always be
    ///   strictly larger than that of PtA.
    /// </summary>
    Pointf PtB { get; }

    /// <summary>
    ///   The lower bound of the x-coordinate interval.
    /// </summary>
    float MinX { get; }

    /// <summary>
    ///   The upper bound of the x-coordinate interval.
    /// </summary>
    float MaxX { get; }

    /// <summary>
    ///   The lower bound of the y-coordinate interval.
    /// </summary>
    float MinY { get; }

    /// <summary>
    ///   The upper bound of the y-coordinate interval.
    /// </summary>
    float MaxY { get; }

    /// <summary>
    ///   The x-coordinate interval represents the domain of definition of this evaluator.
    /// </summary>
    Interval<float> XInterval { get; }

    /// <summary>
    ///   The y-coordinate interval represents the range of this evaluator. Note that this must
    ///   be a sub-interval (or the entire interval) [0,1].
    /// </summary>
    Interval<float> YInterval { get; }

    /// <summary>
    ///   When true, the output of the Evaluate method is transformed to 1.0f - (normal output).
    /// </summary>
    bool IsInverted { get; set; }

    /// <summary>
    ///   Returns the value for the specified x.
    /// </summary>
    float Evaluate(float x);
  }

}