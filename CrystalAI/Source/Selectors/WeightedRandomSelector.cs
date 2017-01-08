// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// WeightedRandomSelector.cs is part of Crystal AI.
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

  public sealed class WeightedRandomSelector : ISelector {
    float _proportion = 0.2f;
    Pcg _random;

    public float Proportion {
      get { return _proportion; }
      set { _proportion = value.Clamp01(); }
    }

    public int Select(ICollection<Utility> elements) {
      var count = elements.Count;
      if(count == 0)
        return -1;

      var maxElemIdx = (_proportion * count).CeilToInt().Clamp(0, count - 1);

      var sorted = elements.Select((x, i) => new KeyValuePair<Utility, int>(x, i))
                           .OrderByDescending(x => x.Key.Combined)
                           .ToList();

      List<Utility> sortedUtils = sorted.Select(x => x.Key).ToList();
      List<int> sortedUtilIndices = sorted.Select(x => x.Value).ToList();

      if(maxElemIdx == 0)
        return sortedUtilIndices[0];

      var cumSum = new float[maxElemIdx];
      cumSum[0] = sortedUtils[0].Weight;
      for(int i = 1; i < maxElemIdx; i++)
        cumSum[i] = cumSum[i - 1] + sortedUtils[i].Weight;
      for(int i = 0; i < maxElemIdx; i++)
        cumSum[i] /= cumSum[maxElemIdx - 1];

      float rval = (float)_random.NextDouble();
      int index = Array.BinarySearch(cumSum, rval);

      // From MSDN: If the index is negative, it represents the bitwise
      // complement of the next larger element in the array.
      if(index < 0)
        index = ~index;
      return sortedUtilIndices[index];
    }

    public ISelector Clone() {
      return new WeightedRandomSelector(Proportion);
    }

    public WeightedRandomSelector() {
      _random = new Pcg();
    }

    public WeightedRandomSelector(float proportion) {
      Proportion = proportion;
    }

    public WeightedRandomSelector(Pcg random) {
      _random = random;
    }
  }

}