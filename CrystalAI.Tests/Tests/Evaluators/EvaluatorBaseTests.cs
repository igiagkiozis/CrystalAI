// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// EvaluatorBaseTests.cs is part of Crystal AI.
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


namespace Crystal.EvaluatorTests {

  [TestFixture]
  internal class EvaluatorBaseTests {
    static readonly object[] CompareToTestCases = {
      new object[] {0f, 1f, 0f, 1f, 0},
      new object[] {0f, 1f, 1f, 2f, -1},
      new object[] {1f, 2f, 0f, 1f, 1},
      new object[] {0.5f, 1.5f, 0.75f, 1.75f, 0}
    };

    [Test]
    public void DefaultConstructorTest() {
      var ev = new TestEvaluator();
      Assert.That(ev.PtA.X, Is.EqualTo(0.0f));
      Assert.That(ev.PtA.Y, Is.EqualTo(0.0f));
      Assert.That(ev.PtB.X, Is.EqualTo(1.0f));
      Assert.That(ev.PtB.Y, Is.EqualTo(1.0f));
      var interval = new Interval<float>(0f, 1f);
      Assert.That(ev.XInterval, Is.EqualTo(interval));
      Assert.That(ev.YInterval, Is.EqualTo(interval));
      Assert.That(ev.MinX, Is.EqualTo(0.0f));
      Assert.That(ev.MaxX, Is.EqualTo(1.0f));
      Assert.That(ev.MinY, Is.EqualTo(0.0f));
      Assert.That(ev.MaxY, Is.EqualTo(1.0f));
    }

    [Test]
    public void TwoPointConstructorTest() {
      var ptA = new Pointf(-1.0f, -1.0f);
      var ptB = new Pointf(1.0f, 1.0f);
      var ev = new TestEvaluator(ptA, ptB);
      Assert.That(ev.PtA.X, Is.EqualTo(-1.0f));
      Assert.That(ev.PtA.Y, Is.EqualTo(0.0f));
      Assert.That(ev.PtB.X, Is.EqualTo(1.0f));
      Assert.That(ev.PtB.Y, Is.EqualTo(1.0f));
      var xInterval = new Interval<float>(-1f, 1f);
      var yInterval = new Interval<float>(0f, 1f);
      Assert.That(ev.XInterval, Is.EqualTo(xInterval));
      Assert.That(ev.YInterval, Is.EqualTo(yInterval));
      Assert.That(ev.MinX, Is.EqualTo(-1.0f));
      Assert.That(ev.MaxX, Is.EqualTo(1.0f));
      Assert.That(ev.MinY, Is.EqualTo(0.0f));
      Assert.That(ev.MaxY, Is.EqualTo(1.0f));
    }

    [Test]
    public void DxZeroThrowsTest() {
      var ptA = new Pointf(10f, 0f);
      var ptB = new Pointf(10f, 1f);
      Assert.Throws<EvaluatorBase.EvaluatorDxZeroException>(() => new TestEvaluator(ptA, ptB));
    }

    [Test]
    public void XaGreaterThanXbThrowsTest() {
      var ptA = new Pointf(11f, 0f);
      var ptB = new Pointf(10f, 1f);
      Assert.Throws<EvaluatorBase.EvaluatorXaGreaterThanXbException>(() => new TestEvaluator(ptA, ptB));
    }

    [Test, TestCaseSource("CompareToTestCases")]
    public void CompareToTests(float ev1X1, float ev1X2, float ev2X1, float ev2X2, int expected) {
      var rnd = new Pcg();
      var ev1Y1 = rnd.NextFloat();
      var ev1Y2 = rnd.NextFloat();
      var ev2Y1 = rnd.NextFloat();
      var ev2Y2 = rnd.NextFloat();
      var ev1PtA = new Pointf(ev1X1, ev1Y1);
      var ev1PtB = new Pointf(ev1X2, ev1Y2);
      var ev2PtA = new Pointf(ev2X1, ev2Y1);
      var ev2PtB = new Pointf(ev2X2, ev2Y2);
      var ev1 = new TestEvaluator(ev1PtA, ev1PtB);
      var ev2 = new TestEvaluator(ev2PtA, ev2PtB);
      Assert.That(ev1.CompareTo(ev2), Is.EqualTo(expected));
      Assert.That(ev2.CompareTo(ev1), Is.EqualTo(-expected));
    }
  }

}