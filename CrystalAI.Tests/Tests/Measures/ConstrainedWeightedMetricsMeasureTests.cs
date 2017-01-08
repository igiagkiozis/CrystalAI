// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ConstrainedWeightedMetricsMeasureTests.cs is part of Crystal AI.
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
using NUnit.Framework;


namespace Crystal.MeasureTests {

  [TestFixture]
  public class ConstrainedWeightedMetricsMeasureTests {
    Pcg _rnd = new Pcg();

    [Test]
    public void ConstructorTest1() {
      var m = new ConstrainedWeightedMetrics();
      Assert.IsNotNull(m);
      Assert.That(m.LowerBound, Is.EqualTo(Zero).Within(Tolerance));
      Assert.That(m.PNorm, Is.EqualTo(2.0f).Within(Tolerance));
    }

    [Test]
    public void ConstructorTest2([Values(-1.0f, 0.0f, 1.0f, 10f, 100000f)] float pNorm) {
      var m = new ConstrainedWeightedMetrics(pNorm);
      Assert.That(m.LowerBound, Is.EqualTo(Zero).Within(Tolerance));
      Assert.That(m.PNorm, Is.EqualTo(pNorm.Clamp(m.PNormMin, m.PNormMax)).Within(Tolerance));
    }

    [Test]
    public void ConstructorTest3(
      [Values(-1.0f, 2.65f, 100000f)] float pNorm,
      [Values(-1.0f, 0.1f, 0.9f, 2.0f)] float lowerBound) {
      var m = new ConstrainedWeightedMetrics(pNorm, lowerBound);
      Assert.That(m.LowerBound, Is.EqualTo(lowerBound.Clamp01()).Within(Tolerance));
      Assert.That(m.PNorm, Is.EqualTo(pNorm.Clamp(m.PNormMin, m.PNormMax)).Within(Tolerance));
    }

    [Test, Repeat(RandomIterations)]
    public void CalculateTest(
      [Values(-100f, -1f, 0f, 1f, 5f, 10f, 100f, 1000000f)] float p,
      [Values(-1f, 0f, 0.5f, 0.75f, 1f, 2f)] float lowerBound) {
      var m = new ConstrainedWeightedMetrics(p, lowerBound);
      var cLb = lowerBound.Clamp01();
      var cP = p.Clamp(m.PNormMin, m.PNormMax);

      Assert.That(m.PNorm, Is.EqualTo(p.Clamp(m.PNormMin, m.PNormMax)).Within(Tolerance));
      var list = MeasureTestsHelper.RandomUtilityList(_rnd.Next(1, MaxVecLen));

      float aVal = m.Calculate(list);
      float cVal = MeasureTestsHelper.CalculateConstrainedWeightedMetricsNorm(list, cP, cLb);

      Assert.That(aVal, Is.EqualTo(cVal).Within(Tolerance));
      Assert.That(aVal <= One);
      Assert.That(aVal >= Zero);
    }

    [Test, Repeat(RandomIterations)]
    public void ZeroWeightsTest() {
      var m = new ConstrainedWeightedMetrics();
      var list = MeasureTestsHelper.RandomUtilityValueList(_rnd.Next(1, MaxVecLen), 0.0f);

      float aVal = m.Calculate(list);
      float cVal = MeasureTestsHelper.CalculateConstrainedWeightedMetricsNorm(list);
      Assert.That(aVal, Is.EqualTo(Zero).Within(Tolerance));
      Assert.That(cVal, Is.EqualTo(Zero).Within(Tolerance));
    }

    [Test, Repeat(RandomIterations)]
    public void ZeroValuesTest() {
      var m = new ConstrainedWeightedMetrics();
      var list = MeasureTestsHelper.RandomUtilityWeightList(_rnd.Next(1, MaxVecLen), 0.0f);

      float aVal = m.Calculate(list);
      float cVal = MeasureTestsHelper.CalculateConstrainedWeightedMetricsNorm(list);
      Assert.That(aVal, Is.EqualTo(Zero).Within(Tolerance));
      Assert.That(cVal, Is.EqualTo(Zero).Within(Tolerance));
    }

    [Test]
    public void ZeroSizeListTest() {
      var m = new ConstrainedWeightedMetrics();
      var list = new List<Utility>();
      float aVal = m.Calculate(list);
      Assert.That(aVal, Is.EqualTo(Zero).Within(Tolerance));
    }

    const int MaxVecLen = 100;
    const int RandomIterations = 50;
    const float Tolerance = 1e-6f;
    const float Zero = 0.0f;
    const float One = 1.0f;
  }

}