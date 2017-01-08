// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// SigmoidEvaluatorTests.cs is part of Crystal AI.
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
using NUnit.Framework;


namespace Crystal.EvaluatorTests {

  [TestFixture]
  public class SigmoidEvaluatorTests {
    float _floatPrecision = 1e-6f;
    int _evN = 1000;

    float _minK = -0.99999f;
    float _maxK = 0.99999f;    

    [Test]
    public void DefaultConstructorTest() {
      var ev = new SigmoidEvaluator();
      Assert.IsNotNull(ev);
    }

    [Test]
    public void TwoParamConstructorTest() {
      var ptA = new Pointf(0.0f, 0.0f);
      var ptB = new Pointf(1.0f, 1.0f);
      var ev = new SigmoidEvaluator(ptA, ptB);
      Assert.IsNotNull(ev);
    }

    [Test]
    public void ThreeParamConstructorTest() {
      var ptA = new Pointf(0.0f, 0.0f);
      var ptB = new Pointf(1.0f, 1.0f);
      var ev = new SigmoidEvaluator(ptA, ptB, -0.5f);
      Assert.IsNotNull(ev);
    }

    [Test]
    public void ThreeParamVector2ConstructorTest() {
      var ptA = new Pointf(0.0f, 0.0f);
      var ptB = new Pointf(1.0f, 1.0f);
      var ev = new SigmoidEvaluator(ptA, ptB, -0.5f);
      Assert.IsNotNull(ev);
    }

    [Test]
    public void PtAandPtBTest(
      [Range(-10.0f, 10.0f, 10f)] float xA,
      [Range(0.0f, 2.0f, 0.5f)] float yA,
      [Range(30.0f, 50.0f, 10f)] float xB,
      [Range(2.0f, 0.0f, -0.5f)] float yB) {
      var pt1 = new Pointf(xA, yA.Clamp01());
      var pt2 = new Pointf(xB, yB.Clamp01());
      var ev = new SigmoidEvaluator(pt1, pt2);
      Assert.AreEqual(pt1, ev.PtA);
    }

    [Test, TestCase(0.0f, 0.0f, 1.0f, 1.0f, -0.9999f), TestCase(-100.0f, 0.0f, 100.0f, 1.0f, -0.2f),
     TestCase(-100.0f, 1.0f, -20.0f, 0.0f, 0.3f), TestCase(42.0f, 0.5f, 54f, 0.6f, 0.234f),
     TestCase(42.0f, 0.9f, 100.0f, 0.0f, 0.0f), TestCase(0.0f, 2.0f, 4.0f, 0.2f, 0.33f),
     TestCase(0.0f, 10.0f, 0.00001f, 0.0f, 0.001f), TestCase(0.0f, 1.0f, 0.000001f, 0.99999f, 0.0001f),
     TestCase(-0.0001f, 0.0001f, 0.0001f, 0.0f, 0.00001f), TestCase(0.0f, 10.0f, 0.00001f, 0.0f, 0.54f),
     TestCase(0.0f, 1.0f, 0.000001f, 0.99999f, 0.99999f), TestCase(-0.0001f, 0.0001f, 0.0001f, 0.0f, 0.99991f),
     TestCase(0.0f, 0.2f, 0.5f, 0.75f, 11.999999999f), TestCase(0.0f, 0.75f, 0.5f, 0.2f, -1.0f),
     TestCase(0.0f, 0.75f, 0.5f, 0.75f, 1.0f)]
    public void EvaluateTest(float xA, float yA, float xB, float yB, float k) {
      var ptA = new Pointf(xA, yA);
      var ptB = new Pointf(xB, yB);
      var ev = new SigmoidEvaluator(ptA, ptB, k);
      var kN = k.Clamp<float>(_minK, _maxK);
      var yAn = yA.Clamp01();
      var yBn = yB.Clamp01();

      var xqArray = CrMath.LinearSpaced(_evN, xA - 0.001f * xA, xB + 0.001f * xB);
      for(int i = 0; i < _evN - 1; i++) {
        var dy = yBn - yAn;
        var dx = xB - xA;
        var dyOver2 = dy / 2.0f;
        var twoOverAbsDx = 2.0f / Math.Abs(dx);
        var meanX = (xA + xB) / 2.0f;
        var oneMinusK = 1.0f - kN;
        var meanY = (yAn + yBn) / 2.0f;
        var xqMinusMeanX = xqArray[i].Clamp<float>(xA, xB) - meanX;
        var num = twoOverAbsDx * xqMinusMeanX * oneMinusK;
        var den = kN * (1 - 2 * Math.Abs(twoOverAbsDx * xqMinusMeanX)) + 1;
        var cVal = dyOver2 * (num / den) + meanY;

        Utility cResult = cVal;
        var aResult = ev.Evaluate(xqArray[i]);
        Assert.That(aResult, Is.EqualTo(cResult.Value).Within(_floatPrecision));
        Assert.That(aResult <= 1.0f);
        Assert.That(aResult >= 0.0f);
      }
      // Check the end points
      var utilA = ev.Evaluate(xA);
      var utilB = ev.Evaluate(xB);
      Assert.That(utilA, Is.EqualTo(yAn).Within(_floatPrecision));
      Assert.That(utilB, Is.EqualTo(yBn).Within(_floatPrecision));
    }
  }

}