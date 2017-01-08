// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ChebyshevMeasureTests.cs is part of Crystal AI.
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
  public class ChebyshevMeasureTests {
    Pcg _rnd = new Pcg();

    [Test]
    public void ConstructorTest() {
      var m = new Chebyshev();
      Assert.IsNotNull(m);
    }

    [Test, Repeat(RandomIterations)]
    public void CalculateTest() {
      var m = new Chebyshev();
      var list = MeasureTestsHelper.RandomUtilityList(_rnd.Next(1, MaxVecLen));
      var aVal = m.Calculate(list);
      float cVal = MeasureTestsHelper.CalculateChebyshevNorm(list);
      Assert.That(aVal, Is.EqualTo(cVal).Within(Tolerance));
      Assert.That(aVal <= One);
      Assert.That(aVal >= Zero);
    }

    [Test, Repeat(RandomIterations)]
    public void ZeroWeightsTest() {
      var m = new Chebyshev();
      var list = MeasureTestsHelper.RandomUtilityValueList(_rnd.Next(1, MaxVecLen), 0.0f);

      float aVal = m.Calculate(list);
      float cVal = MeasureTestsHelper.CalculateChebyshevNorm(list);
      Assert.That(aVal, Is.EqualTo(Zero).Within(Tolerance));
      Assert.That(cVal, Is.EqualTo(Zero).Within(Tolerance));
    }

    [Test, Repeat(RandomIterations)]
    public void ZeroValuesTest() {
      var m = new Chebyshev();
      var list = MeasureTestsHelper.RandomUtilityWeightList(_rnd.Next(1, MaxVecLen), 0.0f);

      float aVal = m.Calculate(list);
      float cVal = MeasureTestsHelper.CalculateChebyshevNorm(list);
      Assert.That(aVal, Is.EqualTo(Zero).Within(Tolerance));
      Assert.That(cVal, Is.EqualTo(Zero).Within(Tolerance));
    }

    [Test]
    public void ZeroSizeListTest() {
      var m = new Chebyshev();
      var list = new List<Utility>();
      var aVal = m.Calculate(list);
      Assert.That(aVal, Is.EqualTo(Zero).Within(Tolerance));
    }

    const int MaxVecLen = 100;
    const int RandomIterations = 50;
    const float Tolerance = 1e-6f;
    const float Zero = 0.0f;
    const float One = 1.0f;
  }

}