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

  /// <summary>
  ///   This selector uses roulette wheel selection of the top <see cref="P:Crystal.WeightedRandomSelector.Proportion"/>
  ///   percent utilities in the given <see cref="T:System.Collections.ICollection"/> of utilities.
  /// </summary>
  /// <seealso cref="T:Crystal.ISelector"/>
  public sealed class WeightedRandomSelector : ISelector {
    float _proportion = 0.2f;
    Pcg _random;

    /// <summary>
    ///   Gets or sets the proportion.
    /// </summary>
    /// <value>
    ///   The proportion.
    /// </value>
    public float Proportion {
      get { return _proportion; }
      set { _proportion = value.Clamp01(); }
    }

    /// <summary>
    ///   Selects a <see cref="T:Crystal.Utility"/> from the given set and returns its
    ///   index.
    /// </summary>
    /// <param name="elements">The elements.</param>
    /// <returns>
    ///   The index of the selected utility. This returns -1 no selection
    ///   was made or if the count of elements was 0.
    /// </returns>
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

    /// <summary>
    ///   Creates a new instance of the implementing class. Note that the semantics here
    ///   are somewhat vague, however, by convention the "Prototype Pattern" uses a "Clone"
    ///   function. Note that this may have very different semantics when compared with either
    ///   shallow or deep cloning. When implementing this remember to include only the defining
    ///   characteristics of the class and not its state!
    /// </summary>
    /// <returns></returns>
    public ISelector Clone() {
      return new WeightedRandomSelector(Proportion);
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="T:Crystal.WeightedRandomSelector"/> class.
    /// </summary>
    public WeightedRandomSelector() {
      _random = new Pcg();
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="T:Crystal.WeightedRandomSelector"/> class.
    /// </summary>
    /// <param name="proportion">The proportion.</param>
    public WeightedRandomSelector(float proportion) {
      Proportion = proportion;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="T:Crystal.WeightedRandomSelector"/> class.
    /// </summary>
    /// <param name="random">The random.</param>
    public WeightedRandomSelector(Pcg random) {
      _random = random;
    }
  }

}