// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// PCGTests.cs is part of Crystal AI.
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


namespace Crystal.UtilitiesTests {

  [TestFixture]
  public class PcgTests {
    Pcg _rng;

    static object[] _nextFloatMeanTestCases = {1f, 2f, 4.3f, 10f, 1000f, 10000000f};

    static object[] _nextDoubleMeanTestCases = {1f, 2f, 4.3f, 10f, 1000f, 10000000f};

    [OneTimeSetUp]
    public void Initialize() {
      _rng = Pcg.Default;
    }

    [Test]
    public void DefaultConstructorTest() {
      var rng = new Pcg();
      Assert.IsNotNull(rng);
    }

    [Test]
    public void StaticConstructorTest() {
      var rng = Pcg.Default;
      Assert.IsNotNull(rng);
      Assert.AreEqual(rng, _rng);
    }

    [Test]
    public void SingleParameterIntConstructorTest() {
      var rng = new Pcg(11);
      Assert.IsNotNull(rng);
    }

    [Test]
    public void SingleParameterULongConstructorTest() {
      var rng = new Pcg(11ul);
      Assert.IsNotNull(rng);
    }

    [Test]
    public void TwoParameterIntConstructorTest() {
      var rng = new Pcg(42, 54);
      Assert.IsNotNull(rng);
    }

    [Test]
    public void TwoParameterULongConstructorTest() {
      var rng = new Pcg(42ul, 54ul);
      Assert.IsNotNull(rng);
    }

    [Test]
    public void CorrectnessTest() {
      int seed = 42;
      int sequence = 54;
      var list = RandomHelpers.ReadPcgOutput(42);
      Assert.AreEqual(10000, list.Count);
      var pcg = new Pcg(seed, sequence);
      for(int i = 0; i < 10000; i++) {
        var aVal = pcg.NextUInt();
        var cVal = list[i];
        Assert.That(aVal, Is.EqualTo(cVal));
      }
    }

    [Test]
    public void ReproducibilityTest() {
      var r1 = new PcgExtended(11, 1);
      var r1V = r1.NextInts(N);
      var r2 = new PcgExtended(11, 1);
      var r2V = r2.NextInts(N);
      for(int i = 0; i < N; i++)
        Assert.That(r1V[i], Is.EqualTo(r2V[i]));
    }

    [Test]
    public void NextTest() {
      for(int i = 0; i < N; i++) {
        var aVal = _rng.Next();
        Assert.That(aVal >= 0);
      }
    }

    [Test]
    public void NextIntsTest() {
      var vals = _rng.NextInts(N);
      foreach(var v in vals)
        Assert.That(v >= 0);
    }

    [Test]
    public void NextIntsUpperBoundTest() {
      var vals = _rng.NextInts(N, 10);
      foreach(var v in vals) {
        Assert.That(v >= 0);
        Assert.That(v < 10);
      }
    }

    [Test]
    public void NextThrowsIfUpperBoundNegativeTest() {
      Assert.Throws<ArgumentException>(() => _rng.Next(-1));
    }

    [Test]
    public void NextMaxLessThanMinThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.Next(10, 1));
    }

    [Test]
    public void NextIntsIntervalTest() {
      var list = _rng.NextInts(N, -4, 6);
      for(int i = 0; i < N; i++) {
        Assert.That(list[i] >= -4);
        Assert.That(list[i] < 6);
      }
    }

    [Test]
    public void NextIntUpperBoundTest() {
      var maxV = 10;
      var pcg = new Pcg(42);
      var lbCount = 0;
      var ubCount = 0;
      for(int i = 0; i < N; i++) {
        var aVal = pcg.Next(maxV);
        Assert.That(aVal >= 0);
        Assert.That(aVal < maxV);
        if(aVal == 0)
          lbCount++;
        if(aVal == maxV - 1)
          ubCount++;
      }

      Assert.That(lbCount > 0);
      Assert.That(ubCount > 0);
    }

    [Test]
    public void NextIntIntervalTest() {
      var minV = -20;
      var maxV = 10;
      var pcg = new Pcg(42);
      var lbCount = 0;
      var ubCount = 0;
      for(int i = 0; i < N; i++) {
        var aVal = pcg.Next(minV, maxV);
        Assert.That(aVal >= minV);
        Assert.That(aVal < maxV);
        if(aVal == minV)
          lbCount++;
        if(aVal == maxV - 1)
          ubCount++;
      }

      Assert.That(lbCount > 0);
      Assert.That(ubCount > 0);
    }

    [Test]
    public void NextIntsZeroCountThrowsTest() {
      var rng = Pcg.Default;
      Assert.Throws<ArgumentException>(() => { rng.NextInts(0); });
    }

    [Test]
    public void NextIntsUpperBoundZeroCountThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextInts(0, 10));
    }

    [Test]
    public void NextIntsIntervalZeroCountThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextInts(0, 10, 20));
    }

    [Test]
    public void NextUIntsTest() {
      Assert.That(_rng.NextUInts(N).Length == N);
    }

    [Test]
    public void NextUIntIntervalMaxLessThanMinThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextUInt(20, 10));
    }

    [Test]
    public void NextUIntsUpperBoundZeroCountThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextUInts(0, 20));
    }

    [Test]
    public void NextUIntsIntervalZeroCountThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextUInts(0, 10, 20));
    }

    [Test]
    public void NextUIntUpperBoundTest() {
      for(int i = 0; i < N; i++) {
        var aVal = _rng.NextUInt(10);
        Assert.That(aVal < 10);
      }
    }

    [Test]
    public void NextUIntTest() {
      for(int i = 0; i < N; i++) {
        var aVal = _rng.NextUInt(10, 15);
        Assert.That(aVal >= 10);
        Assert.That(aVal < 15);
      }
    }

    [Test]
    public void NextUIntsUpperBoundTest() {
      var list = _rng.NextUInts(N, 4);
      foreach(var v in list)
        Assert.That(v < 4);
    }

    [Test]
    public void NextUIntsIntervalTest() {
      var list = _rng.NextUInts(N, 44, 55);
      foreach(var v in list) {
        Assert.That(v >= 44);
        Assert.That(v < 55);
      }
    }

    [Test]
    public void NextUIntsZeroCountThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextUInts(0));
    }

    [Test]
    public void NextFloatTest() {
      for(int i = 0; i < N; i++) {
        var v = _rng.NextFloat();
        Assert.That(v >= 0f);
        Assert.That(v <= 1.0f);
      }
    }

    [Test]
    public void NextFloatMaxLessThanMinThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextFloat(10f, 1f));
    }

    [Test]
    public void NextFloatsZeroCountThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextFloats(0));
    }

    [Test]
    public void NextFloatsUpperBoundTest() {
      var list = _rng.NextFloats(N, 3.3f);
      foreach(var v in list) {
        Assert.That(v >= 0f);
        Assert.That(v <= 3.3f);
      }
    }

    [Test]
    public void NextFloatBoundsTest() {
      var pcg = new Pcg(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextFloat();
        Assert.That(aVal <= 1.0f);
        Assert.That(aVal >= 0.0f);
      }
    }

    [Test]
    public void NextFloatUpperBoundTest() {
      var maxV = 2.5f;
      var pcg = new Pcg(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextFloat(maxV);
        Assert.That(aVal <= maxV);
        Assert.That(aVal >= 0.0f);
      }
    }

    [Test]
    public void NextFloatIntervalTest() {
      var minV = -10f;
      var maxV = 2.5f;
      var pcg = new Pcg(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextFloat(minV, maxV);
        Assert.That(aVal <= maxV);
        Assert.That(aVal >= minV);
      }
    }

    [Test]
    public void NextFloatsTest() {
      Assert.That(_rng.NextFloats(N).Length == N);
    }

    [Test]
    public void NextFloatUpperBoundLessThanZeroThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextFloat(-3.4f));
    }

    [Test]
    public void NextFloatsUpperBoundZeroCountThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextFloats(0, 1f));
    }

    [Test]
    public void NextFloatsIntervalZeroCountThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextFloats(0, 1f, 2f));
    }

    [Test]
    public void NextFloatsIntervalTest() {
      var list = _rng.NextFloats(N, -4.4f, 8.8f);
      foreach(var v in list) {
        Assert.That(v >= -4.4f);
        Assert.That(v <= 8.8f);
      }
    }

    [Test, TestCaseSource("_nextFloatMeanTestCases")]
    public void NextFloatMeanTest(float val) {
      var minV = -val;
      var maxV = val;
      var pcg = new Pcg(42);
      var rsum = 0.0f;
      var lTol = 0.1f * (maxV - minV);
      for(int i = 0; i < N; i++)
        rsum += pcg.NextFloat(minV, maxV);

      var mean = rsum / N;
      Assert.That(mean, Is.EqualTo(0.0f).Within(lTol));
    }

    [Test]
    public void NextDoubleTest() {
      for(int i = 0; i < N; i++) {
        var v = _rng.NextDouble();
        Assert.That(v >= 0.0);
        Assert.That(v <= 1.0);
      }
    }

    [Test]
    public void NextDoublesTest() {
      Assert.That(_rng.NextDoubles(N).Length == N);
    }

    [Test]
    public void NextDoubleUpperBoundNegativeThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextDouble(-1.0));
    }

    [Test]
    public void NextDoubleMaxLessThanMinThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextDouble(10.0, 1.0));
    }

    [Test]
    public void NextDoublesUpperBoundZeroCountThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextDoubles(0, 1.0));
    }

    [Test]
    public void NextDoublesIntervalZeroCountThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextDoubles(0, 1.0, 2.0));
    }

    [Test]
    public void NextDoublesZeroCountThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextDoubles(0));
    }

    [Test]
    public void NextDoublesUpperBoundTest() {
      var list = _rng.NextDoubles(N, 4.4);
      foreach(var v in list) {
        Assert.That(v >= 0.0);
        Assert.That(v <= 4.4);
      }
    }

    [Test]
    public void NextDoublesIntervalTest() {
      var list = _rng.NextDoubles(N, -1.1, 1.1);
      foreach(var v in list) {
        Assert.That(v >= -1.1);
        Assert.That(v <= 1.1);
      }
    }

    [Test]
    public void NextDoubleBoundsTest() {
      var pcg = new Pcg(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextDouble();
        Assert.That(aVal <= 1.0);
        Assert.That(aVal >= 0.0);
      }
    }

    [Test]
    public void NextDoubleUpperBoundTest() {
      var maxV = 2.5;
      var pcg = new Pcg(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextDouble(maxV);
        Assert.That(aVal <= maxV);
        Assert.That(aVal >= 0.0f);
      }
    }

    [Test]
    public void NextDoubleIntervalTest() {
      var minV = -10;
      var maxV = 2.5;
      var pcg = new Pcg(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextDouble(minV, maxV);
        Assert.That(aVal <= maxV);
        Assert.That(aVal >= minV);
      }
    }

    [Test, TestCaseSource("_nextDoubleMeanTestCases")]
    public void NextDoubleMeanTest(float val) {
      var minV = -val;
      var maxV = val;
      var pcg = new Pcg(10);
      var rsum = 0.0;
      var lTol = 0.1 * (maxV - minV);
      for(int i = 0; i < N; i++)
        rsum += pcg.NextDouble(minV, maxV);

      var mean = rsum / N;
      Assert.That(mean, Is.EqualTo(0.0f).Within(lTol));
    }

    [Test]
    public void NextByteTest() {
      for(int i = 0; i < N; i++) {
        var val = Convert.ToInt32(_rng.NextByte());
        Assert.That(val >= 0);
        Assert.That(val <= 255);
      }
    }

    [Test]
    public void NextBytesTest() {
      Assert.That(_rng.NextBytes(N).Length == N);
    }

    [Test]
    public void NextBytesZeroCountThrowsTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextBytes(0));
    }

    [Test]
    public void NextBoolTest() {
      for(int i = 0; i < N; i++) {
        var val = Convert.ToInt32(_rng.NextBool());
        Assert.That(val == 0 || val == 1);
      }
    }

    [Test]
    public void NextBoolsTest() {
      Assert.That(_rng.NextBools(N).Length == N);
    }

    [Test]
    public void NextBoolsThrowsWhenZeroCountTest() {
      Assert.Throws<ArgumentException>(() => _rng.NextBools(0));
    }

    [Test]
    public void SetStreamTest() {
      _rng.SetStream(0);
      Console.WriteLine(_rng.CurrentStream());
      Assert.That(_rng.CurrentStream() == 0);
      _rng.SetStream(1);
      Assert.That(_rng.CurrentStream() == 1);
    }

    [Test]
    public void PeriodPow2Test() {
      Assert.AreEqual(64, _rng.PeriodPow2());
    }

    const int N = 10000;
  }

}