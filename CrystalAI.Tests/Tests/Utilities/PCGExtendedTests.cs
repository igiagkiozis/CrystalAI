// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// PCGExtendedTests.cs is part of Crystal AI.
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

  public class PcgExtendedTests {
    PcgExtended _rng;

    static object[] _nextFloatMeanTestCases = {1f, 2f, 4.3f, 10f, 1000f, 10000000f};

    static object[] _nextDoubleMeanTestCases = {1f, 2f, 4.3f, 10f, 1000f, 10000000f};

    // The comments to the right explain what is being tested. Note that comments about 
    // speed are simply indicative of expected speed, not the result of extensive testing!
    public static object[] PcgExtendedTestCases = {
      new object[] {42, 1, 16}, // 2-dimensionally equidistributed generator, period 2^(2*32 + 64)
      new object[] {42, 1, 32}, // 2-dimensionally equidistributed generator, period 2^(2*32 + 64)
      new object[] {42, 2, 16}, // 4-dimensionally equidistributed generator, period 2^(4*32 + 64)
      new object[] {42, 2, 32}, // 4-dimensionally equidistributed generator, period 2^(4*32 + 64)
      new object[] {42, 3, 16}, // 8-dimensionally equidistributed generator, period 2^(8*32 + 64)
      new object[] {42, 3, 32}, // 8-dimensionally equidistributed generator, period 2^(8*32 + 64)
      new object[] {42, 4, 16}, // 16-dimensionally equidistributed generator, period 2^(16*32 + 64)
      new object[] {42, 4, 32}, // 16-dimensionally equidistributed generator, period 2^(16*32 + 64)
      new object[] {42, 5, 16}, // 32-dimensionally equidistributed generator, period 2^(32*32 + 64)
      new object[] {42, 5, 32}, // 32-dimensionally equidistributed generator, period 2^(32*32 + 64)
      new object[] {42, 6, 16}, // 64-dimensionally equidistributed generator, period 2^(64*32 + 64)
      new object[] {42, 6, 32}, // 64-dimensionally equidistributed generator, period 2^(64*32 + 64)
      new object[] {42, 7, 16}, // 128-dimensionally equidistributed generator, period 2^(128*32 + 64)
      new object[] {42, 7, 32}, // 128-dimensionally equidistributed generator, period 2^(128*32 + 64)
      new object[] {42, 8, 16}, // 256-dimensionally equidistributed generator, period 2^(256*32 + 64)
      new object[] {42, 8, 32}, // 256-dimensionally equidistributed generator, period 2^(256*32 + 64)
      new object[] {42, 9, 16}, // 512-dimensionally equidistributed generator, period 2^(512*32 + 64)
      new object[] {42, 9, 32}, // 512-dimensionally equidistributed generator, period 2^(512*32 + 64)
      new object[] {42, 10, 16},
      // 1024-dimensionally equidistributed generator, period 2^(1024*32 + 64)     Up to here generators perform more  
      new object[] {42, 10, 32},
      // 1024-dimensionally equidistributed generator, period 2^(1024*32 + 64) <-- or less equally fast (within a 10% tolerance)
      new object[] {42, 11, 16},
      // 2048-dimensionally equidistributed generator, period 2^(2048*32 + 64) <-- After this point generators start
      new object[] {42, 11, 32},
      // 2048-dimensionally equidistributed generator, period 2^(2048*32 + 64)     exhibiting noticeable slow downs.
      new object[] {42, 12, 16}, // 4096-dimensionally equidistributed generator, period 2^(4096*32 + 64)     
      new object[] {42, 12, 32}, // 4096-dimensionally equidistributed generator, period 2^(4096*32 + 64)
      new object[] {42, 13, 16}, // 8192-dimensionally equidistributed generator, period 2^(8192*32 + 64)
      new object[] {42, 13, 32}, // 8192-dimensionally equidistributed generator, period 2^(8192*32 + 64)
      new object[] {42, 14, 16},
      // 16384-dimensionally equidistributed generator, period 2^(16384*32 + 64) <-- Approximately 60% as fast as generators
      new object[] {42, 14, 32}
      // 16384-dimensionally equidistributed generator, period 2^(16384*32 + 64)     1024 and below. Still, not bad!
    };

    [OneTimeSetUp]
    public void Initialize() {
      _rng = PcgExtended.Default;
    }

    [Test]
    public void DefaultConstructorTest() {
      var rng = new PcgExtended();
      Assert.IsNotNull(rng);
    }

    [Test]
    public void SingleArgumentConstructorTest() {
      var seed = PcgSeed.TimeBasedSeed();
      var rng = new PcgExtended(seed);
      Assert.IsNotNull(rng);
    }

    [Test]
    public void DefineTablePowConstructorTest() {
      var rng = new PcgExtended(42, 8, 32);
      Assert.That(rng.PeriodPow2() == 256 * 32 + 64);
    }

    [Test]
    public void DefineTablePowAndSequenceConstructorTest() {
      var rng = new PcgExtended(42, 11, 8, 32);
      Assert.That(rng.PeriodPow2() == 256 * 32 + 64);
    }

    [Test, TestCaseSource("PcgExtendedTestCases")]
    public void CorrectnessTests(int seed, int tablePow2, int advancePow2) {
      var list = RandomHelpers.ReadPcgExtendedOutput(seed, tablePow2, advancePow2);
      Assert.AreEqual(10000, list.Count);
      var pcg = new PcgExtended((ulong)seed, 721347520444481703, tablePow2, advancePow2);
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
      var pcg = new PcgExtended(42);
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
      var pcg = new PcgExtended(42);
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
      var pcg = new PcgExtended(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextFloat();
        Assert.That(aVal <= 1.0f);
        Assert.That(aVal >= 0.0f);
      }
    }

    [Test]
    public void NextFloatUpperBoundTest() {
      var maxV = 2.5f;
      var pcg = new PcgExtended(42);
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
      var pcg = new PcgExtended(42);
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
      var pcg = new PcgExtended(42);
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
      var pcg = new PcgExtended(42);
      for(int i = 0; i < N; i++) {
        var aVal = pcg.NextDouble();
        Assert.That(aVal <= 1.0);
        Assert.That(aVal >= 0.0);
      }
    }

    [Test]
    public void NextDoubleUpperBoundTest() {
      var maxV = 2.5;
      var pcg = new PcgExtended(42);
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
      var pcg = new PcgExtended(42);
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
      var pcg = new PcgExtended(10);
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
      Assert.AreEqual(1024 * 32 + 64, _rng.PeriodPow2());
    }

    [Test]
    public void EquidistributionTest() {
      Assert.AreEqual(1024, _rng.Equidistribution());
    }

    [Test]
    public void EquidistributionPow2Test() {
      Assert.AreEqual(10, _rng.EquidistributionPow2());
    }

    const int N = 10000;
  }

}