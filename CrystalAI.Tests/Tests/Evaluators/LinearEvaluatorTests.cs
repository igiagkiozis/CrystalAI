// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// LinearEvaluatorTests.cs is part of Crystal AI.
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
//using UE = UnityEngine;


namespace Crystal.EvaluatorTests {

  [TestFixture]
  public class LinearEvaluatorTests {
    float _floatPrecision = 1e-6f;
    int _evN = 1000;

    [OneTimeSetUp]
    public void Initialize() {
    }

    [Test]
    public void DefaultConsturctorTest() {
      LinearEvaluator ev = new LinearEvaluator();
      Assert.IsNotNull(ev);
    }

    [Test]
    public void TwoParamConstructorTest() {
      var ptA = new Pointf(0.0f, 0.0f);
      var ptB = new Pointf(1.0f, 1.0f);
      var ev = new LinearEvaluator(ptA, ptB);
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
      var ev = new LinearEvaluator(pt1, pt2);
      Assert.AreEqual(pt1, ev.PtA);
    }

    [Test]
    public void EvaluateTest(
      [Range(-10.0f, 10.0f, 10f)] float xA,
      [Range(0.0f, 2.0f, 0.5f)] float yA,
      [Range(30.0f, 50.0f, 10f)] float xB,
      [Range(2.0f, 0.0f, -0.5f)] float yB) {
      var pt1 = new Pointf(xA, yA);
      var pt2 = new Pointf(xB, yB);
      var ev = new LinearEvaluator(pt1, pt2);

      var yAn = yA.Clamp01();
      var yBn = yB.Clamp01();
      var xdf = xB - xA;
      var ydf = yBn - yAn;
      var alpha = ydf / xdf;
      var beta = yBn - alpha * xB;
      var xqArray = CrMath.LinearSpaced(_evN, xA - 10.0f, xB + 10.0f);
      for(int i = 0; i < _evN - 1; i++) {
        Utility cResult = (alpha * xqArray[i] + beta).Clamp01();
        var aResult = ev.Evaluate(xqArray[i]);
        Assert.That(aResult, Is.EqualTo(cResult.Value).Within(_floatPrecision));
        Assert.That(aResult, Is.EqualTo(cResult.Value).Within(_floatPrecision));
        Assert.That(aResult <= 1.0f);
        Assert.That(aResult >= 0.0f);
      }
      // Check the end points
      var utilA = ev.Evaluate(xA);
      var utilB = ev.Evaluate(xB);
      Assert.That(utilA, Is.EqualTo(yA.Clamp01()).Within(_floatPrecision));
      Assert.That(utilB, Is.EqualTo(yB.Clamp01()).Within(_floatPrecision));
    }
  }

}