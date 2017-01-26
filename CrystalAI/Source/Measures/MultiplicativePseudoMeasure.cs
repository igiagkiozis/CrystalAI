// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// MultiplicativePseudoMeasure.cs is part of Crystal AI.
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
using System.Collections.Generic;


namespace Crystal {

  /// <summary>
  /// Calculates a multiplicative pseudo-measure, namely, this class calculates the product
  /// of <see cref="P:Crystal.Utility.Combined"/> in <see cref="T:System.Collections.ICollection"/>.
  /// 
  /// Note that this pseudo-measure, in this or similar forms, is used utility AIs by game developers. 
  /// From a mathematical point of view the use of this pseudo-measure simply doesn’t make sense. That, 
  /// in and of itself, doesn’t 
  /// mean that this pseudo-measure shouldn’t be used or that it is somehow inherently a bad practice. 
  /// However, due to the fact that this pseudo-measure multiplies the elements of the vector of 
  /// utilities, it exhibits some peculiar behaviour for vectors of different length. For example, 
  /// ignoring the weight in utilities for the time being, let’s say that you have a vector of 
  /// utilities that is (0.9, 0.9) and another that belongs to a different option that is 
  /// (0.92, 0.92, 0.92). Using the MultiplicativePseudoMeasure on these two vectors will result in 
  /// a final value for the first equal to 0.81 and 0.77 for the second. This means that the first 
  /// option will be selected ignoring the fact that each of its considerations individually have 
  /// lower utility compared with the second option. This is counter intuitive, and because of this, 
  /// <strong>I would avoid using this pseudo-measure</strong>. Nevertheless, it appears to be in common use so 
  /// an implementation is available for completeness. If you have insights on the reasons behind 
  /// the use of this particular pseudo-measure in the game development industry I would love to hear them!
  /// </summary>
  /// <seealso cref="Crystal.IMeasure" />
  public sealed class MultiplicativePseudoMeasure : IMeasure {
    /// <summary>
    /// Calculate the measure for the given set of elements.
    /// </summary>
    /// <param name="elements"></param>
    /// <returns></returns>
    public float Calculate(ICollection<Utility> elements) {
      return elements.MultiplyCombined();
    }

    /// <summary>
    /// Creates a new instance of the implementing class. Note that the semantics here
    /// are somewhat vague, however, by convention the "Prototype Pattern" uses a "Clone"
    /// function. Note that this may have very different semantics when compared with either
    /// shallow or deep cloning. When implementing this remember to include only the defining
    /// characteristics of the class and not its state!
    /// </summary>
    /// <returns></returns>
    public IMeasure Clone() {
      return new MultiplicativePseudoMeasure();
    }
  }

}