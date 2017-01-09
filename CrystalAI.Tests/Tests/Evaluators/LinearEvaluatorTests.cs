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
    static readonly object[] LinearEvaluatorCases = {
      new object[] {-10f, 0f, 10f, 1f},
      new object[] {-10f, 1f, 10f, 0f},
      new object[] {1f, 0.2f, 100f, 0.8f},
      new object[] {0f, -2f, 1f, 2f}
    };
    const float Tolerance = 1e-6f;
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

    [Test, TestCaseSource("LinearEvaluatorCases")]
    public void PtAandPtBTest(float xA, float yA, float xB, float yB) {
      var pt1 = new Pointf(xA, yA.Clamp01());
      var pt2 = new Pointf(xB, yB.Clamp01());
      var ev = new LinearEvaluator(pt1, pt2);
      Assert.AreEqual(pt1, ev.PtA);
      Assert.AreEqual(pt2, ev.PtB);
    }

    [Test, TestCaseSource("LinearEvaluatorCases")]
    public void EvaluateTest(float xA, float yA, float xB, float yB) {
      var pt1 = new Pointf(xA, yA);
      var pt2 = new Pointf(xB, yB);
      IEvaluator ev = new LinearEvaluator(pt1, pt2);

      var yAn = yA.Clamp01();
      var yBn = yB.Clamp01();
      var xdf = xB - xA;
      var ydf = yBn - yAn;
      var alpha = ydf / xdf;
      var beta = yBn - alpha * xB;
      var xqArray = CrMath.LinearSpaced(_evN, xA - 10.0f, xB + 10.0f);
      for(int i = 0; i < _evN - 1; i++) {
        var cVal = LinearClamped(xqArray[i], alpha, beta);
        var aVal = ev.Evaluate(xqArray[i]);
        Assert.That(aVal, Is.EqualTo(cVal).Within(Tolerance));
        Assert.That(aVal, Is.EqualTo(cVal).Within(Tolerance));
        Assert.That(aVal <= 1.0f);
        Assert.That(aVal >= 0.0f);
      }
      // Check the end points
      var utilA = ev.Evaluate(xA);
      var utilB = ev.Evaluate(xB);
      Assert.That(utilA, Is.EqualTo(yA.Clamp01()).Within(Tolerance));
      Assert.That(utilB, Is.EqualTo(yB.Clamp01()).Within(Tolerance));
    }

    [Test, TestCaseSource("LinearEvaluatorCases")]
    public void InverseEvaluateTest(float xA, float yA, float xB, float yB) {
      var pt1 = new Pointf(xA, yA);
      var pt2 = new Pointf(xB, yB);
      IEvaluator ev = new LinearEvaluator(pt1, pt2);
      ev.IsInverted = true;

      var yAn = yA.Clamp01();
      var yBn = yB.Clamp01();
      var xdf = xB - xA;
      var ydf = yBn - yAn;
      var alpha = ydf / xdf;
      var beta = yBn - alpha * xB;
      var xqArray = CrMath.LinearSpaced(_evN, xA - 10.0f, xB + 10.0f);
      for(int i = 0; i < _evN - 1; i++) {
        var cVal = InverseLinearClamped(xqArray[i], alpha, beta);
        var aVal = ev.Evaluate(xqArray[i]);
        Assert.That(aVal, Is.EqualTo(cVal).Within(Tolerance));
        Assert.That(aVal, Is.EqualTo(cVal).Within(Tolerance));
        Assert.That(aVal <= 1.0f);
        Assert.That(aVal >= 0.0f);
      }
      // Check the end points
      var utilA = ev.Evaluate(xA);
      var utilB = ev.Evaluate(xB);
      Assert.That(utilA, Is.EqualTo(1f - yA.Clamp01()).Within(Tolerance));
      Assert.That(utilB, Is.EqualTo(1f - yB.Clamp01()).Within(Tolerance));
    }

    float LinearClamped(float x, float alpha, float beta) {
      return (alpha * x + beta).Clamp01();
    }

    float InverseLinearClamped(float x, float alpha, float beta) {
      return 1f - LinearClamped(x, alpha, beta);
    }
  }

}