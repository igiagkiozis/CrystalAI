// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// WeightedMetrics.cs is part of Crystal AI.
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
using System;
using System.Collections.Generic;
using System.Linq;


namespace Crystal {

  /// <summary>
  /// Calculates the l-p weighted metrics <see cref="https://en.wikipedia.org/wiki/Lp_space"/>. 
  /// </summary>
  /// <seealso cref="T:Crystal.IMeasure" />
  public sealed class WeightedMetrics : IMeasure {
    float _oneOverP;
    float _p;

    /// <summary>
    /// The minimum value for <see cref="T:Crystal.ConstrainedWeightedMetrics.PNorm"/>.
    /// </summary>
    public readonly float PNormMin = 1.0f;

    /// <summary>
    /// The minimum value for <see cref="T:Crystal.ConstrainedWeightedMetrics.PNorm"/>.
    /// </summary>
    public readonly float PNormMax = 10000.0f;

    /// <summary>
    /// Gets or sets the p norm used in this instance.
    /// </summary>
    public float PNorm {
      get { return _p; }
      set {
        _p = value.Clamp(PNormMin, PNormMax);
        _oneOverP = 1.0f / _p;
      }
    }

    /// <summary>
    /// Calculate the l-p weighted metrics measure for the given set of elements.
    /// </summary>
    /// <param name="elements"></param>
    /// <returns></returns>
    public float Calculate(ICollection<Utility> elements) {
      var count = elements.Count;
      if(count == 0)
        return 0.0f;

      var wsum = 0.0f;
      foreach(var el in elements)
        wsum += el.Weight;

      if(CrMath.AeqZero(wsum))
        return 0.0f;

      var vlist = new List<float>(count);
      foreach(var el in elements) {
        var v = el.Weight / wsum * (float)Math.Pow(el.Value, _p);
        vlist.Add(v);
      }

      var sum = vlist.Sum();
      var res = (float)Math.Pow(sum, _oneOverP);

      return res;
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
      return new WeightedMetrics(PNorm);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Crystal.WeightedMetrics"/> class.
    /// </summary>
    /// <param name="pNorm">The p norm.</param>
    public WeightedMetrics(float pNorm = 2f) {
      PNorm = pNorm;
    }
  }

}