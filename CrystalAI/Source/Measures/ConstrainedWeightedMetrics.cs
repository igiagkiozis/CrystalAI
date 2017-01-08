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

  public sealed class ConstrainedWeightedMetrics : IMeasure {
    float _lowerBound;
    WeightedMetrics _measure;

    public float PNormMin {
      get { return _measure.PNormMin; }
    }

    public float PNormMax {
      get { return _measure.PNormMax; }
    }

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

    public float Calculate(ICollection<Utility> elements) {
      if(elements.Any(el => el.Combined < LowerBound))
        return 0.0f;

      return _measure.Calculate(elements);
    }

    public IMeasure Clone() {
      return new ConstrainedWeightedMetrics(PNorm, LowerBound);
    }

    public ConstrainedWeightedMetrics() {
      _measure = new WeightedMetrics();
    }

    public ConstrainedWeightedMetrics(float pNorm) {
      _measure = new WeightedMetrics(pNorm);
    }

    public ConstrainedWeightedMetrics(float pNorm, float lowerBound) {
      _measure = new WeightedMetrics(pNorm);
      LowerBound = lowerBound;
    }
  }

}