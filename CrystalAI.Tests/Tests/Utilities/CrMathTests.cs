// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// CrMathTests.cs is part of Crystal AI.
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
  internal class CrMathTests {
    static readonly float Eps10 = 1e-7f;
    static readonly float EpsD2 = CrMath.Eps / 2.0f;
    static readonly double Deps10 = 10.0 * CrMath.Deps;
    static readonly double DepsD2 = CrMath.Deps / 2.0;
    
    static readonly object[] AeqBFloatTestCases = {
      new object[] {1.0f, 1.0f, true},
      new object[] {1.0f, 2.0f, false},
      new object[] {1.0f, 1.0f + EpsD2, true},
      new object[] {1.0f, 1.0f + Eps10, false},
      new object[] {-1.0f, -1.0f, true},
      new object[] {-1.0f, -2.0f, false},
      new object[] {-1.0f, -1.0f + EpsD2, true},
      new object[] {-1.0f, -1.0f + Eps10, false}
    };


    static readonly object[] AeqBDoubleTestCases = {
      new object[] {1.0, 1.0, true},
      new object[] {1.0, 2.0, false},
      new object[] {1.0, 1.0 + DepsD2, true},
      new object[] {1.0, 1.0 + Deps10, false},
      new object[] {-1.0, -1.0, true},
      new object[] {-1.0, -2.0, false},
      new object[] {-1.0, -1.0 + DepsD2, true},
      new object[] {-1.0, -1.0 + Deps10, false}
    };

    static readonly object[] AneqBFloatTestCases = {
      new object[] {1.0f, 1.0f, false},
      new object[] {1.0f, 2.0f, true},
      new object[] {1.0f, 1.0f + EpsD2, false},
      new object[] {1.0f, 1.0f + Eps10, true},
      new object[] {-1.0f, -1.0f, false},
      new object[] {-1.0f, -2.0f, true},
      new object[] {-1.0f, -1.0f + EpsD2, false},
      new object[] {-1.0f, -1.0f + Eps10, true}
    };


    static readonly object[] AneqBDoubleTestCases = {
      new object[] {1.0, 1.0, false},
      new object[] {1.0, 2.0, true},
      new object[] {1.0, 1.0 + DepsD2, false},
      new object[] {1.0, 1.0 + Deps10, true},
      new object[] {-1.0, -1.0, false},
      new object[] {-1.0, -2.0, true},
      new object[] {-1.0, -1.0 + DepsD2, false},
      new object[] {-1.0, -1.0 + Deps10, true}
    };

    static readonly object[] AeqZeroFloatTestCases = {
      new object[] {0.0f, true},
      new object[] {EpsD2, true},
      new object[] {Eps10, false}
    };

    static readonly object[] AeqZeroDoubleTestCases = {
      new object[] {0.0, true},
      new object[] {DepsD2, true},
      new object[] {Deps10, false}
    };


    static readonly object[] AneqZeroFloatTestCases = {
      new object[] {0.0f, false},
      new object[] {EpsD2, false},
      new object[] {Eps10, true}
    };

    static readonly object[] AneqZeroDoubleTestCases = {
      new object[] {0.0, false},
      new object[] {DepsD2, false},
      new object[] {Deps10, true}
    };

    static readonly object[] LinearSpacedTestCases = {
      new object[] {100, 1f, 2f},
      new object[] {2, 1f, 2f},
      new object[] {20, -1f, 22f},
      new object[] {33, -1f, -2f},
      new object[] {2, 2f, 1f},
      new object[] {3, 2f, 1f},
      new object[] {12, -2f, -1f}
    };

    [Test]
    public void DefaultEpsCalculationTest() {
      var eps = CrMath.Eps;
      // Just a sanity check
      Assert.That(eps, Is.LessThan(1e-6f));
    }

    [Test]
    public void DefaultDepsCalculationTest() {
      var deps = CrMath.Deps;
      // Just a sanity check
      Assert.That(deps, Is.LessThan(1e-6));
    }

    [Test]
    public void UpdateEpsTest() {
      var eps = CrMath.Eps;
      CrMath.UpdateEps(1000.0f);
      var eps1000 = CrMath.Eps;
      Assert.That(eps1000, Is.GreaterThan(eps));
    }

    [Test]
    public void UpdateDepsTest() {
      var deps = CrMath.Deps;
      CrMath.UpdateDeps(1000.0);
      var deps1000 = CrMath.Deps;
      Assert.That(deps1000, Is.GreaterThan(deps));
    }
    
    [Test, TestCaseSource("AeqBFloatTestCases")]
    public void AeqBFloatTests(float a, float b, bool expected) {
      Assert.That(CrMath.AeqB(a, b), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("AeqBDoubleTestCases")]
    public void AeqBDoubleTests(double a, double b, bool expected) {
      Assert.That(CrMath.AeqB(a, b), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("AneqBFloatTestCases")]
    public void AneqBFloatTests(float a, float b, bool expected) {
      Assert.That(CrMath.AneqB(a, b), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("AneqBDoubleTestCases")]
    public void AneqBDoubleTests(double a, double b, bool expected) {
      Assert.That(CrMath.AneqB(a, b), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("AeqZeroFloatTestCases")]
    public void AeqZeroFloatTests(float a, bool expected) {
      Assert.That(CrMath.AeqZero(a), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("AeqZeroDoubleTestCases")]
    public void AeqZeroDoubleTests(double a, bool expected) {
      Assert.That(CrMath.AeqZero(a), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("AneqZeroFloatTestCases")]
    public void AneqZeroFloatTests(float a, bool expected) {
      Assert.That(CrMath.AneqZero(a), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("AneqZeroDoubleTestCases")]
    public void AneqZeroDoubleTests(double a, bool expected) {
      Assert.That(CrMath.AneqZero(a), Is.EqualTo(expected));
    }

    [Test]
    public void LinearSpacedCountZeroOrNegativeThrows() {
      Assert.Throws<ArgumentOutOfRangeException>(() => CrMath.LinearSpaced(0, 1f, 2f));
      Assert.Throws<ArgumentOutOfRangeException>(() => CrMath.LinearSpaced(-1, 1f, 2f));
    }

    [Test]
    public void LinearSpacedCountOneReturnsEnd() {
      var ls = CrMath.LinearSpaced(1, 1f, 2f);
      Assert.That(ls[0], Is.EqualTo(2.0f));
      Assert.That(ls.Length == 1);
    }

    [Test, TestCaseSource("LinearSpacedTestCases")]
    public void LinearSpacedTest(int count, float start, float end) {
      var ls = CrMath.LinearSpaced(count, start, end);
      Assert.That(ls.Length == count);
      Assert.That(ls[0], Is.EqualTo(start));
      Assert.That(ls[ls.Length - 1], Is.EqualTo(end));
    }

    [Test]
    public void IsEvenIntTest() {
      var val = 11;
      Assert.That(val.IsEven(), Is.False);
      val = 10;
      Assert.That(val.IsEven(), Is.True);
    }

    [Test]
    public void IsEvenLongTest() {
      var val = 11L;
      Assert.That(val.IsEven(), Is.False);
      val = 1111111112L;
      Assert.That(val.IsEven(), Is.True);
    }

    [Test]
    public void IsOddIntTest() {
      var val = 11;
      Assert.That(val.IsOdd(), Is.True);
      val = 10002;
      Assert.That(val.IsOdd(), Is.False);
    }

    [Test]
    public void IsOddLongTest() {
      var val = 11L;
      Assert.That(val.IsOdd(), Is.True);
      val = 10000000000000002L;
      Assert.That(val.IsOdd(), Is.False);
    }
  }

}