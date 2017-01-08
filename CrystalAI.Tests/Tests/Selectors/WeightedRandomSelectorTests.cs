// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// WeightedRandomSelectorTests.cs is part of Crystal AI.
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
using NUnit.Framework;


namespace Crystal.SelectorTests {

  [TestFixture]
  public class WeightedRandomSelectorTests {
    Pcg _rnd = new Pcg();

    [Test]
    public void ConstructorTest() {
      var s = new WeightedRandomSelector();
      Assert.IsNotNull(s);
    }

    [Test]
    public void ProportionSetterClampedTo01Test([Values(0.0f, 0.1f, 0.5f, 0.9f, 1.0f, 2.0f)] float proportion) {
      var s = new WeightedRandomSelector();
      var cP = proportion.Clamp01();
      s.Proportion = proportion;
      Assert.That(s.Proportion, Is.EqualTo(cP).Within(Tolerance));
    }

    [Test, Repeat(20)]
    public void SelectTest([Values(-0.1f, 0.0f, 0.1f, 0.5f, 0.9f, 1.0f, 2.0f)] float proportion) {
      var cP = proportion.Clamp01();
      var rn = new MockRandom();
      var rnList = new List<double>();
      for(int i = 0; i < 100; i++)
        rnList.Add(_rnd.NextDouble());

      rn.Values = rnList;
      var s = new WeightedRandomSelector(rn);
      s.Proportion = proportion;

      var uList = SelectorTestsHelper.CreateRandomUtilityList(20);
      var sorted = uList.OrderByDescending(x => x.Combined).ToList();
      for(int curI = 0; curI < 100; curI++) {
        Utility cUtil = CorrectUtility(curI, cP, uList, sorted, rnList);

        var aIdx = s.Select(uList);
        var aUtil = new Utility(0.0f, 0.0f);
        if(aIdx >= 0)
          aUtil = uList[aIdx];

        Assert.That(aUtil.Value, Is.EqualTo(cUtil.Value).Within(Tolerance));
        Assert.That(aUtil.Weight, Is.EqualTo(cUtil.Weight).Within(Tolerance));
      }
    }

    [Test]
    public void ZeroSizeUtilityListSelectTest() {
      var uList = new List<Utility>();
      var s = new WeightedRandomSelector();
      Assert.That(s.Select(uList), Is.EqualTo(-1));
    }

    Utility CorrectUtility(int curIdx, float cP, List<Utility> uList, List<Utility> sorted, List<double> rnList) {
      Utility cUtil;
      var numItems = (cP * uList.Count).CeilToInt().Clamp(0, uList.Count - 1);
      if(numItems == 0)
        cUtil = sorted[0];
      else {
        var array = new float[numItems];
        float wsum = 0.0f;
        for(int i = 0; i < numItems; i++)
          wsum += sorted[i].Weight;

        array[0] = sorted[0].Weight / wsum;
        for(int i = 1; i < numItems; i++)
          array[i] = array[i - 1] + sorted[i].Weight / wsum;

        int idx = Array.BinarySearch(array, (float)rnList[curIdx]);
        if(idx < 0)
          idx = ~idx;

        cUtil = sorted[idx];
      }

      return cUtil;
    }

    const float Tolerance = 1e-6f;
  }

}