// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IntervalTests.cs is part of Crystal AI.
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


namespace Crystal.GeneralTests {

  [TestFixture]
  public class IntervalTests {
    static readonly object[] ContainsTestCases = {
      // Closed interval: [a, b]
      new object[] {Interval.Range(1, 10), 1, true},
      new object[] {Interval.Range(1, 10), 10, true},
      new object[] {Interval.Range(1, 10), 11, false},
      new object[] {Interval.Range(1, 10), 0, false},

      // Empty interval: (a, a], [a, a), (a, a)
      new object[] {Interval.Range(1, 1, IntervalType.Open), 1, false},
      new object[] {Interval.Range(1, 1, IntervalType.Open, IntervalType.Open), 1, false},

      // Degernate interval: [a, a] = {a}
      new object[] {Interval.Range(1, 1), 1, true},

      // Lower bounded interval: (a, +∞), [a, +∞), (a, +∞], [a, +∞]
      new object[] {Interval.Range(-100d, double.PositiveInfinity), double.PositiveInfinity, true}, // []
      new object[] {Interval.Range(-100d, double.PositiveInfinity), 1d, true}, // []
      new object[] {Interval.Range(-100d, double.PositiveInfinity), -100d, true}, // []
      new object[] {Interval.Range(-100d, double.PositiveInfinity), -101d, false}, // []
      new object[] {Interval.Range(-100d, double.PositiveInfinity, IntervalType.Open), 1d, true}, // (]
      new object[] {Interval.Range(-100d, double.PositiveInfinity, IntervalType.Open), -100d, false}, // (]
      new object[] {
        Interval.Range(-100d, double.PositiveInfinity, IntervalType.Closed, IntervalType.Open),
        double.PositiveInfinity,
        false
      }, // [)
      new object[] {Interval.Range(-100d, double.PositiveInfinity, IntervalType.Closed, IntervalType.Open), 1d, true},
      // [)
      new object[] {Interval.Range(-100d, double.PositiveInfinity, IntervalType.Open, IntervalType.Open), 1d, true},
      // ()

      // Upper bounded interval: (-∞, b), [-∞, b), (-∞, b], [-∞, b]
      new object[] {Interval.Range(double.NegativeInfinity, 0), -1d, true},
      new object[] {Interval.Range(double.NegativeInfinity, 0), double.NegativeInfinity, true},
      new object[] {Interval.Range(double.NegativeInfinity, 0, IntervalType.Open), double.NegativeInfinity, false},

      // Unbounded interval: (-∞, +∞), [-∞, +∞] etc
      new object[] {Interval.Range(double.NegativeInfinity, double.PositiveInfinity), 1d, true},
      new object[] {Interval.Range(double.NegativeInfinity, double.PositiveInfinity), double.NegativeInfinity, true},
      new object[] {Interval.Range(double.NegativeInfinity, double.PositiveInfinity), double.PositiveInfinity, true},
      new object[] {
        Interval.Range(
                       double.NegativeInfinity,
                       double.PositiveInfinity,
                       IntervalType.Open,
                       IntervalType.Open),
        1d,
        true
      },
      new object[] {
        Interval.Range(
                       double.NegativeInfinity,
                       double.PositiveInfinity,
                       IntervalType.Open,
                       IntervalType.Open),
        double.NegativeInfinity,
        false
      },
      new object[] {
        Interval.Range(
                       double.NegativeInfinity,
                       double.PositiveInfinity,
                       IntervalType.Open,
                       IntervalType.Open),
        double.PositiveInfinity,
        false
      },

      // Implicit swapping of a and b, i.e., when b < a
      new object[] {Interval.Range(10, 1), 1, true},
      new object[] {Interval.Range(10, 1), 0, false},
      new object[] {Interval.Range(10, 1), 11, false}
    };

    static readonly object[] IntIntervalLengthCases = {
      new object[] {10, 20, 10},
      new object[] {-10, 30, 40},
      new object[] {-10, -5, 5}
    };

    static readonly object[] FloatIntervalLengthCases = {
      new object[] {1f, 2f, 1f},
      new object[] {-1f, 1f, 2f},
      new object[] {-1f, -0.5f, 0.5f}
    };

    static readonly object[] DoubleIntervalLengthCases = {
      new object[] {1.0, 2.0, 1.0},
      new object[] {-1.0, 1.0, 2.0},
      new object[] {-1.0, -0.5, 0.5}
    };

    [Test, TestCaseSource("ContainsTestCases")]
    public void ContainsTests<T>(Interval<T> interval, T point, bool expected) where T : struct, IComparable {
      Assert.That(interval.Contains(point), Is.EqualTo(expected));
    }

    [Test, TestCase(1f, 10f), TestCase(-1f, 20f), TestCase(200f, -100f),
     TestCase(float.PositiveInfinity, float.NegativeInfinity), TestCase(float.NegativeInfinity, float.PositiveInfinity),
     TestCase(-42.423f, float.PositiveInfinity), TestCase(float.NegativeInfinity, 34.4f), TestCase(1.0f, 1.0f),
     TestCase(-1.0f, -1.0f)]
    public void BoundsTests(float lowerBound, float upperBound) {
      var interval = Interval.Range(lowerBound, upperBound);
      float cLowerBound = Math.Min(lowerBound, upperBound);
      float cUpperBound = Math.Max(lowerBound, upperBound);
      Assert.That(interval.LowerBound, Is.EqualTo(cLowerBound));
      Assert.That(interval.UpperBound, Is.EqualTo(cUpperBound));
    }

    [Test]
    public void ConstructorTest() {
      var floatInterval = new Interval<float>(1.0f, 2.0f);
    }

    [Test, TestCaseSource("IntIntervalLengthCases")]
    public void IntIntervalLengthTests(int start, int end, int expected) {
      var interval = new Interval<int>(start, end);
      Assert.That(interval.Length(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("FloatIntervalLengthCases")]
    public void FloatIntervalLengthTests(float start, float end, float expected) {
      var interval = new Interval<float>(start, end);
      Assert.That(interval.Length(), Is.EqualTo(expected));
    }

    [Test, TestCaseSource("DoubleIntervalLengthCases")]
    public void DoubleIntervalLengthTests(double start, double end, double expected) {
      var interval = new Interval<double>(start, end);
      Assert.That(interval.Length(), Is.EqualTo(expected));
    }
  }

}