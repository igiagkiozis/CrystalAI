// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ValueTypeExtensionsTests.cs is part of Crystal AI.
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


namespace Crystal.ExtensionsTests {

  [TestFixture]
  internal class ValueTypeExtensionsTests {
    static readonly object[] FloatInIntervalCases = {
      new object[] {1.0f, new Interval<float>(1f, 2f), true},
      new object[] {0.9f, new Interval<float>(1f, 2f), false},
      new object[] {2.0f, new Interval<float>(1f, 2f), true},
      new object[] {2.1f, new Interval<float>(1f, 2f), false}
    };

    static readonly object[] FloatAboveIntervalCases = {
      new object[] {1.0f, new Interval<float>(1f, 2f), false},
      new object[] {0.9f, new Interval<float>(1f, 2f), false},
      new object[] {2.0f, new Interval<float>(1f, 2f), false},
      new object[] {2.1f, new Interval<float>(1f, 2f), true}
    };

    static readonly object[] FloatBelowIntervalCases = {
      new object[] {1.0f, new Interval<float>(1f, 2f), false},
      new object[] {0.9f, new Interval<float>(1f, 2f), true},
      new object[] {2.0f, new Interval<float>(1f, 2f), false},
      new object[] {2.1f, new Interval<float>(1f, 2f), false}
    };

    static readonly object[] FloatCompareToIntervalCases = {
      new object[] {1.0f, new Interval<float>(1f, 2f), 0},
      new object[] {0.9f, new Interval<float>(1f, 2f), -1},
      new object[] {2.0f, new Interval<float>(1f, 2f), 0},
      new object[] {2.1f, new Interval<float>(1f, 2f), 1}
    };

    static readonly object[] GenericClampCases = {
      new object[] {10f, 11f, 20f, 11f},
      new object[] {10f, 5f, 9f, 9f},
      new object[] {10f, 9f, 11f, 10f}
    };

    static readonly object[] ClampFloatCases = {
      new object[] {10f, 11f, 20f, 11f},
      new object[] {10f, 5f, 9f, 9f},
      new object[] {10f, 9f, 11f, 10f}
    };

    static readonly object[] ClampToDoubleCases = {
      new object[] {10.0, 11.0, 20.0, 11.0},
      new object[] {10.0, 5.0, 9.0, 9.0},
      new object[] {10.0, 9.0, 11.0, 10.0}
    };

    static readonly object[] Clamp01FloatCases = {
      new object[] {2.0f, 1.0f},
      new object[] {-1.0f, 0.0f},
      new object[] {0.5f, 0.5f}
    };

    static readonly object[] Clamp01DoubleCases = {
      new object[] {2.0, 1.0},
      new object[] {-1.0, 0.0},
      new object[] {0.5, 0.5}
    };

    static readonly object[] ClampToLowerBoundIntCases = {
      new object[] {5, 1, 5},
      new object[] {0, 3, 3},
      new object[] {-10, -11, -10},
      new object[] {-10, -3, -3}
    };

    static readonly object[] ClampToLowerBoundFloatCases = {
      new object[] {5f, 1f, 5f},
      new object[] {0f, 3f, 3f},
      new object[] {-10f, -11f, -10f},
      new object[] {-10f, -3f, -3f}
    };

    static readonly object[] ClampToLowerBoundDoubleCases = {
      new object[] {5.0, 1.0, 5.0},
      new object[] {0.0, 3.0, 3.0},
      new object[] {-10.0, -11.0, -10.0},
      new object[] {-10.0, -3.0, -3.0}
    };

    static readonly object[] ClampToUpperBoundIntCases = {
      new object[] {10, 11, 10},
      new object[] {10, 9, 9},
      new object[] {-10, -11, -11},
      new object[] {-10, -9, -10}
    };

    static readonly object[] ClampToUpperBoundFloatCases = {
      new object[] {10f, 11f, 10f},
      new object[] {10f, 9f, 9f},
      new object[] {-10f, -11f, -11f},
      new object[] {-10f, -9f, -10f}
    };

    static readonly object[] ClampToUpperBoundDoubleCases = {
      new object[] {10.0, 11.0, 10.0},
      new object[] {10.0, 9.0, 9.0},
      new object[] {-10.0, -11.0, -11.0},
      new object[] {-10.0, -9.0, -10.0}
    };

    static readonly object[] ClampToPositiveIntCases = {
      new object[] {1, 1},
      new object[] {-1, 0},
      new object[] {0, 0}
    };

    static readonly object[] ClampToPositiveFloatCases = {
      new object[] {1f, 1f},
      new object[] {-1f, 0f},
      new object[] {0f, 0f}
    };

    static readonly object[] ClampToPositiveDoubleCases = {
      new object[] {1.0, 1.0},
      new object[] {-1.0, 0.0},
      new object[] {0.0, 0.0}
    };

    static readonly object[] ClampToNegativeIntCases = {
      new object[] {1, 0},
      new object[] {-1, -1},
      new object[] {0, 0}
    };

    static readonly object[] ClampToNegativeFloatCases = {
      new object[] {1f, 0f},
      new object[] {-1f, -1f},
      new object[] {0f, 0f}
    };

    static readonly object[] ClampToNegativeDoubleCases = {
      new object[] {1.0, 0.0},
      new object[] {-1.0, -1.0},
      new object[] {0.0, 0.0}
    };

    static readonly object[] CeilFloatCases = {
      new object[] {1.1f, 2.0f},
      new object[] {1.6f, 2.0f},
      new object[] {-2.9f, -2.0f},
      new object[] {-2.1f, -2.0f}
    };

    static readonly object[] CeilDoubleCases = {
      new object[] {1.1, 2.0},
      new object[] {1.6, 2.0},
      new object[] {-2.9, -2.0},
      new object[] {-2.1, -2.0}
    };

    static readonly object[] CeilFloatToIntCases = {
      new object[] {1.1f, 2},
      new object[] {1.9f, 2},
      new object[] {-2.9f, -2},
      new object[] {-2.1f, -2}
    };

    static readonly object[] CeilDoubleToIntCases = {
      new object[] {1.1, 2},
      new object[] {1.9, 2},
      new object[] {-2.9, -2},
      new object[] {-2.1, -2}
    };

    static readonly object[] FloorFloatCases = {
      new object[] {1.1f, 1f},
      new object[] {1.9f, 1f},
      new object[] {-1.1f, -2f},
      new object[] {-1.9f, -2f}
    };

    static readonly object[] FloorDoubleCases = {
      new object[] {1.1, 1.0},
      new object[] {1.9, 1.0},
      new object[] {-1.1, -2.0},
      new object[] {-1.9, -2.0}
    };

    static readonly object[] FloorToIntFloatCases = {
      new object[] {1.1f, 1},
      new object[] {1.9f, 1},
      new object[] {-1.1f, -2},
      new object[] {-1.9f, -2}
    };

    static readonly object[] FloorToIntDoubleCases = {
      new object[] {1.1, 1},
      new object[] {1.9, 1},
      new object[] {-1.1, -2},
      new object[] {-1.9, -2}
    };

    static readonly object[] Normalize01FloatCases = {
      new object[] {50.0f, 0.0f, 100f, 0.5f},
      new object[] {75.0f, 0.0f, 100f, 0.75f},
      new object[] {25.0f, 0.0f, 100f, 0.25f},
      new object[] {15.0f, 0.0f, 100f, 0.15f},
      new object[] {150.0f, 0.0f, 100f, 1.0f},
      new object[] {-50.0f, 0.0f, 100f, 0.0f}
    };

    static readonly object[] Normalize01DoubleCases = {
      new object[] {50.0, 0.0, 100.0, 0.5},
      new object[] {75.0, 0.0, 100.0, 0.75},
      new object[] {25.0, 0.0, 100.0, 0.25},
      new object[] {15.0, 0.0, 100.0, 0.15},
      new object[] {150.0, 0.0, 100.0, 1.0},
      new object[] {-50.0, 0.0, 100.0, 0.0}
    };

    [Test, TestCaseSource("FloatInIntervalCases")]
    public void FloatInIntervalTests(float value, Interval<float> interval, bool expected) {
      Assert.That(value.InInterval(interval), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("FloatAboveIntervalCases")]
    public void FloatAboveIntervalTests(float value, Interval<float> interval, bool expected) {
      Assert.That(value.AboveInterval(interval), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("FloatBelowIntervalCases")]
    public void FloatBelowIntervalTests(float value, Interval<float> interval, bool expected) {
      Assert.That(value.BelowInterval(interval), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("FloatCompareToIntervalCases")]
    public void FloatCampareToIntervalTests(float value, Interval<float> interval, int expected) {
      Assert.That(value.CompareToInterval(interval), Is.EqualTo(expected));
    }

    [Test]
    public void GenericClampThrowsIfMaxLessThanMinTest() {
      var value = 10f;
      Assert.Throws<MinGreaterThanMaxException>(() => value.Clamp<float>(10f, 1f));
    }

    [Test, TestCaseSource("GenericClampCases")]
    public void GenericClampTest(float value, float lb, float ub, float expected) {
      var aVal = value.Clamp<float>(lb, ub);
      Assert.That(aVal, Is.EqualTo(expected));
    }

    [Test, TestCaseSource("ClampFloatCases")]
    public void ClampFloatTest(float value, float lb, float ub, float expected) {
      var aVal = value.Clamp(lb, ub);
      Assert.That(aVal, Is.EqualTo(expected));
    }

    [Test, TestCaseSource("ClampToDoubleCases")]
    public void ClampDoubleTest(double value, double lb, double ub, double expected) {
      var aVal = value.Clamp(lb, ub);
      Assert.That(aVal, Is.EqualTo(expected));
    }

    [Test, TestCaseSource("Clamp01FloatCases")]
    public void Clamp01FloatTest(float value, float expected) {
      Assert.That(value.Clamp01(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("Clamp01DoubleCases")]
    public void Clamp01DoubleTest(double value, double expected) {
      Assert.That(value.Clamp01(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("ClampToLowerBoundIntCases")]
    public void ClampToLowerBoundIntTest(int value, int lb, int expected) {
      Assert.That(value.ClampToLowerBound(lb), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("ClampToLowerBoundFloatCases")]
    public void ClampToLowerBoundFloatTest(float value, float lb, float expected) {
      Assert.That(value.ClampToLowerBound(lb), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("ClampToLowerBoundDoubleCases")]
    public void ClampToLowerBoundDoubleTest(double value, double lb, double expected) {
      Assert.That(value.ClampToLowerBound(lb), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("ClampToUpperBoundIntCases")]
    public void ClampToUpperBoundIntTest(int value, int ub, int expected) {
      Assert.That(value.ClampToUpperBound(ub), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("ClampToUpperBoundFloatCases")]
    public void ClampToUpperBoundFloatTest(float value, float ub, float expected) {
      Assert.That(value.ClampToUpperBound(ub), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("ClampToUpperBoundDoubleCases")]
    public void ClampToUpperBoundDoubleTest(double value, double ub, double expected) {
      Assert.That(value.ClampToUpperBound(ub), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("ClampToPositiveIntCases")]
    public void ClampToPositiveIntTest(int value, int expected) {
      Assert.That(value.ClampToPositive(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("ClampToPositiveFloatCases")]
    public void ClampToPositiveFloatTest(float value, float expected) {
      Assert.That(value.ClampToPositive(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("ClampToPositiveDoubleCases")]
    public void ClampToPositiveDoubleTest(double value, double expected) {
      Assert.That(value.ClampToPositive(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("ClampToNegativeIntCases")]
    public void ClampToNegativeIntTest(int value, int expected) {
      Assert.That(value.ClampToNegative(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("ClampToNegativeFloatCases")]
    public void ClampToNegativeFloatTest(float value, float expected) {
      Assert.That(value.ClampToNegative(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("ClampToNegativeDoubleCases")]
    public void ClampToNegativeDoubleTest(double value, double expected) {
      Assert.That(value.ClampToNegative(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("CeilFloatCases")]
    public void CeilFloatTest(float value, float expected) {
      Assert.That(value.Ceil(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("CeilDoubleCases")]
    public void CeilDoubleTest(double value, double expected) {
      Assert.That(value.Ceil(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("CeilFloatToIntCases")]
    public void CeilFloatToIntTest(float value, int expected) {
      Assert.That(value.CeilToInt(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("CeilDoubleToIntCases")]
    public void CeilDoubleToIntTest(double value, int expected) {
      Assert.That(value.CeilToInt(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("FloorFloatCases")]
    public void FloorFloatTests(float value, float expected) {
      Assert.That(value.Floor(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("FloorDoubleCases")]
    public void FloorDoubleTests(double value, double expected) {
      Assert.That(value.Floor(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("FloorToIntFloatCases")]
    public void FloorToIntFloatTests(float value, int expected) {
      Assert.That(value.FloorToInt(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("FloorToIntDoubleCases")]
    public void FloorToIntDoubleTests(double value, int expected) {
      Assert.That(value.FloorToInt(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("Normalize01FloatCases")]
    public void Normalize01FloatTests(float value, float lb, float ub, float expected) {
      Assert.That(value.Normalize01(lb, ub), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("Normalize01DoubleCases")]
    public void Normalize01DoubleTests(double value, double lb, double ub, double expected) {
      Assert.That(value.Normalize01(lb, ub), Is.EqualTo(expected));
    }

    [Test]
    public void Normalize01FloatThrowsIfLbEqUbTest() {
      var value = 100f;
      Assert.Throws<MinEqualMaxException>(() => value.Normalize01(10f, 10f));
    }

    [Test]
    public void Normalize01DoubleThrowsIfLbEqUbTest() {
      var value = 100.0;
      Assert.Throws<MinEqualMaxException>(() => value.Normalize01(10.0, 10.0));
    }
  }

}