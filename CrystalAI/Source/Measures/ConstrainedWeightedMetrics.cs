// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ConstrainedWeightedMetrics.cs is part of Crystal AI.
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
using System.Linq;


namespace Crystal {

  /// <summary>
  /// Calculates a constrained version of the l-p weighted metrics <see cref="https://en.wikipedia.org/wiki/Lp_space"/>. 
  /// </summary>
  /// <seealso cref="Crystal.IMeasure" />
  public sealed class ConstrainedWeightedMetrics : IMeasure {
    float _lowerBound;
    WeightedMetrics _measure;

    /// <summary>
    /// The minimum value for <see cref="T:Crystal.ConstrainedWeightedMetrics.PNorm"/>.
    /// </summary>
    public float PNormMin {
      get { return _measure.PNormMin; }
    }

    /// <summary>
    /// The maximum value for <see cref="T:Crystal.ConstrainedWeightedMetrics.PNorm"/>.
    /// </summary>
    public float PNormMax {
      get { return _measure.PNormMax; }
    }

    /// <summary>
    /// Gets or sets the p norm used in this instance.
    /// </summary>
    public float PNorm {
      get { return _measure.PNorm; }
      set { _measure.PNorm = value; }
    }

    /// <summary>
    ///   If the combined value of any utility is below this, the value of this measure will be 0.
    /// </summary>
    public float LowerBound {
      get { return _lowerBound; }
      set { _lowerBound = value.Clamp01(); }
    }

    /// <summary>
    /// Calculate the l-p weighted metrics measure for the given set of elements.
    /// </summary>
    /// <param name="elements"></param>
    /// <returns></returns>
    public float Calculate(ICollection<Utility> elements) {
      if(elements.Any(el => el.Combined < LowerBound))
        return 0.0f;

      return _measure.Calculate(elements);
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
      return new ConstrainedWeightedMetrics(PNorm, LowerBound);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConstrainedWeightedMetrics"/> class.
    /// </summary>
    public ConstrainedWeightedMetrics() {
      _measure = new WeightedMetrics();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConstrainedWeightedMetrics"/> class.
    /// </summary>
    /// <param name="pNorm">The p norm.</param>
    /// <param name="lowerBound">The lower bound.</param>
    public ConstrainedWeightedMetrics(float pNorm, float lowerBound = 0f) {
      _measure = new WeightedMetrics(pNorm);
      LowerBound = lowerBound;
    }
  }

}