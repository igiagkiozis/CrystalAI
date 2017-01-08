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

  public sealed class WeightedMetrics : IMeasure {
    float _p;
    public readonly float PNormMin = 1.0f;
    public readonly float PNormMax = 10000.0f;

    public float PNorm {
      get { return _p; }
      set { _p = value.Clamp(PNormMin, PNormMax); }
    }

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
      var res = (float)Math.Pow(sum, 1 / _p);

      return res;
    }

    public IMeasure Clone() {
      return new WeightedMetrics(PNorm);
    }

    public WeightedMetrics() {
      PNorm = 2.0f;
    }

    public WeightedMetrics(float pNorm) {
      PNorm = pNorm;
    }
  }

}