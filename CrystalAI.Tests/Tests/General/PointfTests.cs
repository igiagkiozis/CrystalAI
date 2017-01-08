// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// PointfTests.cs is part of Crystal AI.
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


namespace Crystal.GeneralTests {

  [TestFixture]
  internal class PointfTests {
    static readonly object[] PointEqualityCases = {
      new object[] {1f, 1f, 1f, 1f, true},
      new object[] {1f, 2f, 1f, 1f, false}
    };

    [Test]
    public void DefaultConstructorTest() {
      var p = new Pointf();
      Assert.That(p.X, Is.EqualTo(0f));
      Assert.That(p.Y, Is.EqualTo(0f));
    }

    [Test]
    public void TwoArgConstructorTest() {
      var p = new Pointf(1f, 2f);
      Assert.That(p.X, Is.EqualTo(1f));
      Assert.That(p.Y, Is.EqualTo(2f));
    }

    [Test]
    public void ToStringTest() {
      var p = new Pointf();
      Assert.That(string.IsNullOrEmpty(p.ToString()), Is.False);
    }

    [Test, TestCaseSource("PointEqualityCases")]
    public void PointEqualityTests(float p1x, float p1y, float p2x, float p2y, bool expected) {
      var pt1 = new Pointf(p1x, p1y);
      var pt2 = new Pointf(p2x, p2y);
      Assert.That(pt1.Equals(pt2), Is.EqualTo(expected));
    }
  }

}