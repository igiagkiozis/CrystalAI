// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// MultiplicativePseudoMeasureTests.cs is part of Crystal AI.
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
using NUnit.Framework;


namespace Crystal.MeasureTests {

  [TestFixture]
  public class MultiplicativePseudoMeasureTests {
    Pcg _rnd = new Pcg();

    [Test]
    public void ConstructorTest() {
      var m = new MultiplicativePseudoMeasure();
      Assert.IsNotNull(m);
    }

    [Test]
    public void CloneTest() {
      var m = new MultiplicativePseudoMeasure();
      var mc = m.Clone();
      Assert.IsNotNull(mc);
      var list = MeasureTestsHelper.RandomUtilityList(20);
      var mVal = m.Calculate(list);
      var mcVal = mc.Calculate(list);
      Assert.That(mcVal, Is.EqualTo(mVal));
    }

    [Test, Repeat(20)]
    public void CalculateTest() {
      var m = new MultiplicativePseudoMeasure();
      var list = MeasureTestsHelper.RandomUtilityList(20);
      var cVal = list.MultiplyCombined();
      var aVal = m.Calculate(list);
      Assert.That(aVal, Is.EqualTo(cVal));
    }

    const int MaxVecLen = 100;
    const int RandomIterations = 50;
    const float Tolerance = 1e-6f;
    const float Zero = 0.0f;
    const float One = 1.0f;
  }

}