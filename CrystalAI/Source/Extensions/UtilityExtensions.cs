// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// UtilityExtensions.cs is part of Crystal AI.
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
  ///   Utility extensions.
  /// </summary>
  public static class UtilityExtensions {
    /// <summary>
    ///   Chebyshev norm for the given utility list.
    /// </summary>
    /// <param name="this">Utility list.</param>
    public static float Chebyshev(this ICollection<Utility> @this) {
      if(@this.Count == 0)
        return 0.0f;

      var wsum = @this.SumWeights();
      if(CrMath.AeqZero(wsum))
        return 0.0f;

      var vlist = new List<float>(@this.Count);
      foreach(var util in @this) {
        var v = util.Value * (util.Weight / wsum);
        vlist.Add(v);
      }

      var ret = vlist.Max<float>();
      return ret;
    }

    /// <summary>
    ///   Returns the p-weighted metrics norm for the given Utility list.
    /// </summary>
    /// <returns>The metrics.</returns>
    /// <param name="this">Utility list.</param>
    /// <param name="p">The norm</param>
    public static float WeightedMetrics(this ICollection<Utility> @this, float p = 2.0f) {
      if(p < 1.0f)
        throw new PowerLessThanOneInWeightedMetricsException();

      if(@this.Count == 0)
        return 0.0f;

      var wsum = @this.SumWeights();
      var vlist = new List<float>(@this.Count);
      foreach(var util in @this) {
        var v = util.Weight / wsum * (float)Math.Pow(util.Value, p);
        vlist.Add(v);
      }

      var sum = vlist.Sum();
      var res = (float)Math.Pow(sum, 1 / p);
      return res;
    }

    public static float MultiplyCombined(this ICollection<Utility> @this) {
      var count = @this.Count;
      if(count == 0)
        return 0.0f;

      var res = 1.0f;
      foreach(var el in @this)
        res *= el.Combined;

      return res;
    }

    public static float MultiplyValues(this ICollection<Utility> @this) {
      var count = @this.Count;
      if(count == 0)
        return 0.0f;

      var res = 1.0f;
      foreach(var el in @this)
        res *= el.Value;

      return res;
    }

    public static float MultiplyWeights(this ICollection<Utility> @this) {
      var count = @this.Count;
      if(count == 0)
        return 0.0f;

      var res = 1.0f;
      foreach(var el in @this)
        res *= el.Weight;

      return res;
    }

    public static float SumValues(this ICollection<Utility> @this) {
      var count = @this.Count;
      if(count == 0)
        return 0.0f;

      var res = 0.0f;
      foreach(var el in @this)
        res += el.Value;

      return res;
    }

    public static float SumWeights(this ICollection<Utility> @this) {
      var count = @this.Count;
      if(count == 0)
        return 0.0f;

      var res = 0.0f;
      foreach(var el in @this)
        res += el.Weight;

      return res;
    }

    class PowerLessThanOneInWeightedMetricsException : Exception {
    }
  }

}