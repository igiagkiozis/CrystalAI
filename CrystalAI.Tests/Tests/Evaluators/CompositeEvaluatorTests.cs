// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// CompositeEvaluatorTests.cs is part of Crystal AI.
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
  public class CompositeEvaluatorTests {
    float _floatPrecision = 1e-6f;
    int _evN = 1000;

    [OneTimeSetUp]
    public void Initialize() {
    }

    [Test]
    public void DefaultConstructorTest() {
      var ev = new CompositeEvaluator();
      Assert.IsNotNull(ev);
    }

    [Test]
    public void AddEvaluatorTest() {
      var cev = new CompositeEvaluator();
      var ptA1 = new Pointf(0.0f, 0.0f);
      var ptB1 = new Pointf(0.2f, 0.3f);
      var ev1 = new LinearEvaluator(ptA1, ptB1);

      var ptA2 = new Pointf(0.22f, 0.32f);
      var ptB2 = new Pointf(0.3f, 0.4f);
      var ev2 = new PowerEvaluator(ptA2, ptB2, 3.0f);

      var ptA3 = new Pointf(0.35f, 0.34f);
      var ptB3 = new Pointf(0.55f, 0.55f);
      var ev3 = new SigmoidEvaluator(ptA3, ptB3, -0.4f);

      var ptA4 = new Pointf(0.55f, 0.55f);
      var ptB4 = new Pointf(1.0f, 1.0f);
      var ev4 = new SigmoidEvaluator(ptA4, ptB4, 0.6f);

      var minXmaxX = new Pointf(0.0f, 1.0f);

      cev.Add(ev1);
      Assert.AreEqual(ptA1, cev.PtA);
      Assert.AreEqual(ptB1, cev.PtB);

      cev.Add(ev2);
      cev.Add(ev3);
      cev.Add(ev4);
      Console.WriteLine(cev.Evaluators.Count);
      Assert.AreEqual(4, cev.Evaluators.Count);
      Assert.AreEqual(cev.MinX, minXmaxX.X);
      Assert.AreEqual(cev.MaxX, minXmaxX.Y);
    }

    [Test,
     TestCase(0.0f, 0.0f, 0.2f, 0.3f, 0.22f, 0.32f, 0.3f, 0.4f, 0.35f, 0.34f, 0.55f, 0.55f, 0.55f, 0.55f, 1.0f, 1.0f),
     TestCase(-100.0f, 1.0f, -20.0f, 0.4f, -15.0f, 0.37f, -2.0f, 0.35f, -2.0f, 0.35f, 0.0f, 0.45f, 0.0f, 0.55f, 1000.0f,
       1.0f),
     TestCase(0.0f, 5.0f, 0.2f, 0.2f, 0.224f, 0.532f, 0.43f, 0.4f, 50.35f, 0.34f, 550.55f, 0.55f, 5520.55f, 0.55f,
       6001.0f, 1.0f),
     TestCase(0.0f, 0.0f, 0.2f, 0.3f, 0.2f, 0.3f, 0.3f, 0.4f, 0.4f, 0.4f, 0.55f, 0.55f, 0.55f, 0.55f, 1.0f, 1.0f),
     TestCase(0.0f, 0.0f, 0.2f, 0.3f, 0.22f, 0.32f, 0.3f, 0.4f, 0.35f, 0.34f, 0.55f, 0.55f, 0.65f, 0.75f, 1.0f, 1.0f),
     TestCase(-10.0f, 5.0f, 10.2f, 0.3f, 20.22f, 0.32f, 21.3f, 0.4f, 27.35f, 0.34f, 28.55f, 0.55f, 10000.55f, 0.95f,
       100000.0f, 1.0f)]
    public void EvaluateTest1(
      float xA1, float yA1,
      float xB1, float yB1,
      float xA2, float yA2,
      float xB2, float yB2,
      float xA3, float yA3,
      float xB3, float yB3,
      float xA4, float yA4,
      float xB4, float yB4) {
      var ptA1 = new Pointf(xA1, yA1);
      var ptB1 = new Pointf(xB1, yB1);
      var ev1 = new LinearEvaluator(ptA1, ptB1);

      var ptA2 = new Pointf(xA2, yA2);
      var ptB2 = new Pointf(xB2, yB2);
      var ev2 = new PowerEvaluator(ptA2, ptB2, 3.0f);

      var ptA3 = new Pointf(xA3, yA3);
      var ptB3 = new Pointf(xB3, yB3);
      var ev3 = new SigmoidEvaluator(ptA3, ptB3, -0.4f);

      var ptA4 = new Pointf(xA4, yA4);
      var ptB4 = new Pointf(xB4, yB4);
      var ev4 = new SigmoidEvaluator(ptA4, ptB4, 0.6f);

//      var minX = UE.Mathf.Min(xA1, xA2, xA3, xA4);
//      var maxX = UE.Mathf.Max(xB1, xB2, xB3, xB4);
      var minX = Math.Min(Math.Min(xA1, xA2), Math.Min(xA3, xA4));
      var maxX = Math.Max(Math.Max(xB1, xB2), Math.Max(xB3, xB4));

      var compositeXInterval = new Interval<float>(minX, maxX);

      var cev = new CompositeEvaluator();
      // AddConsideration them out of order to ensure the ordering works.
      cev.Add(ev2);
      cev.Add(ev4);
      cev.Add(ev3);
      cev.Add(ev1);
      Assert.That(cev.Evaluators.Count == 4);

      var xqArray = CrMath.LinearSpaced(_evN, minX * 0.999f, 1.001f * maxX);
      for(int i = 0; i < _evN - 1; i++) {
        var cVal = CombinedEvaluatorEvaluate(xqArray[i], ev1, ev2, ev3, ev4);
        Utility cResult = cVal;

        var aResult = cev.Evaluate(xqArray[i]);
        Assert.That(aResult, Is.EqualTo(cResult.Value).Within(_floatPrecision));
        Assert.That(aResult <= 1.0f);
        Assert.That(aResult >= 0.0f);
      }
      // Check the end points
      var utilA = cev.Evaluate(compositeXInterval.LowerBound);
      var utilB = cev.Evaluate(compositeXInterval.UpperBound);
      Assert.That(utilA, Is.EqualTo(ev1.PtA.Y).Within(_floatPrecision));
      Assert.That(utilB, Is.EqualTo(ev4.PtB.Y).Within(_floatPrecision));
    }

    float CombinedEvaluatorEvaluate(float x, IEvaluator ev1, IEvaluator ev2, IEvaluator ev3, IEvaluator ev4) {
      Pointf xInterval = CalculateInterval(ev1, ev4);
      // In interval
      if(x >= xInterval.X &&
         x <= xInterval.Y) {
        // Check if x is within any of the evaluator's XIntervals
        if(x >= ev1.XInterval.LowerBound &&
           x <= ev1.XInterval.UpperBound)
          return ev1.Evaluate(x);
        if(x >= ev2.XInterval.LowerBound &&
           x <= ev2.XInterval.UpperBound)
          return ev2.Evaluate(x);
        if(x >= ev3.XInterval.LowerBound &&
           x <= ev3.XInterval.UpperBound)
          return ev3.Evaluate(x);
        if(x >= ev4.XInterval.LowerBound &&
           x <= ev4.XInterval.UpperBound)
          return ev4.Evaluate(x);

        // x is in a hole..
        if(x > ev1.XInterval.UpperBound &&
           x < ev2.XInterval.LowerBound)
          return GetValueInHoleBetween(x, ev1, ev2);
        if(x > ev2.XInterval.UpperBound &&
           x < ev3.XInterval.LowerBound)
          return GetValueInHoleBetween(x, ev2, ev3);
        if(x > ev3.XInterval.UpperBound &&
           x < ev4.XInterval.LowerBound)
          return GetValueInHoleBetween(x, ev3, ev4);
      }

      // Above interval
      if(x > xInterval.Y)
        return ev4.Evaluate(x);

      // Below interval
      if(x < xInterval.X)
        return ev1.Evaluate(x);

      return -1.0f;
    }

    Pointf CalculateInterval(IEvaluator ev1, IEvaluator ev4) {
      return new Pointf(ev1.MinX, ev4.MaxX);
    }

    float GetValueInHoleBetween(float x, IEvaluator ev1, IEvaluator ev2) {
      var xl = ev1.XInterval.UpperBound;
      var xr = ev2.XInterval.LowerBound;
      var yl = ev1.Evaluate(xl);
      var yr = ev2.Evaluate(xr);
      return HoleInterpolator(x, xl, yl, xr, yr);
    }

    float HoleInterpolator(float x, float xl, float yl, float xr, float yr) {
      var alpha = (x - xl) / (xr - xl);
      return yl + alpha * (yr - yl);
    }
  }

}