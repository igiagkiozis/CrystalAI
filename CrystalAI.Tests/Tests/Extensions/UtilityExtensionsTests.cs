// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// UtilityExtensionsTests.cs is part of Crystal AI.
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
using NUnit.Framework;


namespace Crystal.ExtensionsTests {

  [TestFixture]
  public class UtilityExtensionsTests {
    Pcg _rng = new Pcg();


    [OneTimeSetUp]
    public void Initialize() {
      _rng.NextDouble();
    }

    [Test, Repeat(RandomIterations)]
    public void ChebyshevNormTest() {
      var list = RandomUtilityList(VecLen);
      float aVal = list.Chebyshev();
      float cVal = CalculateChebyshevNorm(list);
      Assert.That(aVal, Is.EqualTo(cVal).Within(Tolerance));
      Assert.That(aVal <= One);
      Assert.That(aVal >= Zero);
    }

    [Test]
    public void ChebyshevNormZeroSizeListTest() {
      var list = new List<Utility>();
      float aVal = list.Chebyshev();
      Assert.That(aVal, Is.EqualTo(Zero).Within(Tolerance));
    }

    [Test, Repeat(RandomIterations)]
    public void WeightedMetricsNormTest([Range(1.0f, 20.0f, 0.5f)] float p) {
      var list = RandomUtilityList(VecLen);
      float aVal = list.WeightedMetrics(p);
      float cVal = CalculateWeightedMetricsNorm(list, p);
      Assert.That(aVal, Is.EqualTo(cVal).Within(Tolerance));
      Assert.That(aVal <= One);
      Assert.That(aVal >= Zero);
    }

    [Test]
    public void WeightedMetricsNormZeroSizeListTest() {
      var list = new List<Utility>();
      float aVal = list.WeightedMetrics();
      Assert.That(aVal, Is.EqualTo(Zero).Within(Tolerance));
    }

    [Test, Repeat(RandomIterations)]
    public void MultiplyCombinedTest() {
      var list = RandomUtilityList(VecLen);
      var aRes = list.MultiplyCombined();
      var cRes = 1.0f;
      foreach(var el in list)
        cRes = cRes * el.Combined;

      Assert.That(aRes, Is.EqualTo(cRes).Within(Tolerance));
    }

    [Test, Repeat(RandomIterations)]
    public void MultiplyValuesTest() {
      var list = RandomUtilityList(VecLen);
      var aRes = list.MultiplyValues();
      var cRes = 1.0f;
      foreach(var el in list)
        cRes = cRes * el.Value;

      Assert.That(aRes, Is.EqualTo(cRes).Within(Tolerance));
    }

    [Test, Repeat(RandomIterations)]
    public void MultiplyWeightsTest() {
      var list = RandomUtilityList(VecLen);
      var aRes = list.MultiplyWeights();
      var cRes = 1.0f;
      foreach(var el in list)
        cRes = cRes * el.Weight;

      Assert.That(aRes, Is.EqualTo(cRes).Within(Tolerance));
    }

    float CalculateChebyshevNorm(IEnumerable<Utility> list) {
      var vlist = new List<float>();
      var wsum = 0.0f;
      foreach(var u in list)
        wsum += u.Weight;

      foreach(var el in list)
        vlist.Add(el.Weight / wsum * el.Value);

      float res = -10.0f;
      foreach(var v in vlist)
        if(v >= res)
          res = v;

      return res;
    }

    float CalculateWeightedMetricsNorm(IEnumerable<Utility> list, float p = 2.0f) {
      var vlist = new List<float>();
      var wsum = 0.0f;
      foreach(var u in list)
        wsum += u.Weight;

      foreach(var u in list) {
        var v = u.Weight / wsum * (float)Math.Pow(u.Value, p);
        vlist.Add(v);
      }

      var sum = 0.0f;
      foreach(var v in vlist)
        sum += v;

      return (float)Math.Pow(sum, 1 / p);
    }

    List<Utility> RandomUtilityList(int vecLen) {
      var list = new List<Utility>();
      for(int i = 0; i < vecLen; i++) {
        var rValue = (float)_rng.NextDouble();
        var rWeight = (float)_rng.NextDouble();
        var util = new Utility(rValue, rWeight);
        list.Add(util);
      }

      return list;
    }

    const float Tolerance = 1e-6f;
    const int VecLen = 20;
    const int RandomIterations = 50;
    const float One = 1.0f;
    const float Zero = 0.0f;
  }

}