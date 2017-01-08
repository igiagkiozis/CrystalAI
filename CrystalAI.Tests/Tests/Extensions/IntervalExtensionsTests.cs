// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IntervalExtensionsTests.cs is part of Crystal AI.
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
  public class IntervalExtensionsTests {


    [Test, TestCase(0f, 1f, 1f, 2f, Closed, Closed, Closed, Closed, true),
     TestCase(0f, 1f, 1f, 2f, Open, Closed, Closed, Closed, true),
     TestCase(0f, 1f, 1f, 2f, Open, Open, Open, Open, false), TestCase(0f, 1f, 1f, 2f, Open, Open, Closed, Open, false),
     TestCase(1f, 2f, 0f, 1f, Closed, Closed, Closed, Closed, true),
     TestCase(1f, 2f, 0f, 1f, Open, Closed, Closed, Closed, false),
     TestCase(1f, 2f, 0f, 1f, Open, Open, Open, Open, false), TestCase(1f, 2f, 0f, 1f, Open, Open, Closed, Open, false),
     TestCase(0f, 1f, 1.1f, 2f, Closed, Closed, Closed, Closed, false),
     TestCase(0f, 1f, 1.1f, 2f, Open, Closed, Closed, Closed, false),
     TestCase(0f, 0.9f, 1f, 2f, Open, Open, Open, Open, false),
     TestCase(0f, 0.9f, 1f, 2f, Open, Open, Closed, Open, false),
     TestCase(1.1f, 2f, 0f, 1f, Closed, Closed, Closed, Closed, false),
     TestCase(1.1f, 2f, 0f, 1f, Open, Closed, Closed, Closed, false),
     TestCase(1f, 2f, 0f, 0.9f, Open, Open, Open, Open, false),
     TestCase(1f, 2f, 0f, 0.9f, Open, Open, Closed, Open, false),
     TestCase(0f, 1f, 0.5f, 1.5f, Open, Open, Open, Open, true),
     TestCase(0.5f, 1.5f, 0f, 1f, Open, Open, Open, Open, true)]
    public void OverlapTest(float a, float b, float c, float d,
                            IntervalType at, IntervalType bt, IntervalType ct, IntervalType dt, bool expected) {
      var interval1 = Interval.Range(a, b, at, bt);
      var interval2 = Interval.Range(c, d, ct, dt);
      Assert.That(interval1.Overlaps(interval2), Is.EqualTo(expected));
    }

    [Test, TestCase(0f, 1f, 1f, 2f, Closed, Closed, Closed, Closed, true),
     TestCase(0f, 1f, 1f, 2f, Open, Closed, Closed, Closed, true),
     TestCase(0f, 1f, 1f, 2f, Open, Open, Open, Open, false), TestCase(0f, 1f, 1f, 2f, Open, Open, Closed, Open, false),
     TestCase(1f, 2f, 0f, 1f, Closed, Closed, Closed, Closed, true),
     TestCase(1f, 2f, 0f, 1f, Open, Closed, Closed, Closed, false),
     TestCase(1f, 2f, 0f, 1f, Open, Open, Open, Open, false), TestCase(1f, 2f, 0f, 1f, Open, Open, Closed, Open, false),
     TestCase(0f, 1f, 1.1f, 2f, Closed, Closed, Closed, Closed, false),
     TestCase(0f, 1f, 1.1f, 2f, Open, Closed, Closed, Closed, false),
     TestCase(0f, 0.9f, 1f, 2f, Open, Open, Open, Open, false),
     TestCase(0f, 0.9f, 1f, 2f, Open, Open, Closed, Open, false),
     TestCase(1.1f, 2f, 0f, 1f, Closed, Closed, Closed, Closed, false),
     TestCase(1.1f, 2f, 0f, 1f, Open, Closed, Closed, Closed, false),
     TestCase(1f, 2f, 0f, 0.9f, Open, Open, Open, Open, false),
     TestCase(1f, 2f, 0f, 0.9f, Open, Open, Closed, Open, false),
     TestCase(0f, 1f, 0.5f, 1.5f, Open, Open, Open, Open, false),
     TestCase(0.5f, 1.5f, 0f, 1f, Open, Open, Open, Open, false)]
    public void AdjacentTest(float a, float b, float c, float d,
                             IntervalType at, IntervalType bt, IntervalType ct, IntervalType dt, bool expected) {
      var interval1 = Interval.Range(a, b, at, bt);
      var interval2 = Interval.Range(c, d, ct, dt);
      Assert.That(interval1.Adjacent(interval2), Is.EqualTo(expected));
    }

    [Test, TestCase(0f, 1f, 2f, 3f, Closed, Closed, Closed, Closed, false),
     TestCase(2f, 3f, 0f, 1f, Closed, Closed, Closed, Closed, false),
     TestCase(0f, 1f, 1f, 2f, Closed, Closed, Closed, Closed, true),
     TestCase(0f, 1f, 1f, 2f, Closed, Open, Closed, Closed, false),
     TestCase(0f, 1f, 1f, 2f, Closed, Closed, Open, Closed, false),
     TestCase(0f, 1f, 1f, 2f, Closed, Open, Open, Closed, false),
     TestCase(1f, 2f, 0f, 1f, Closed, Closed, Closed, Closed, false),
     TestCase(1f, 2f, 0f, 1f, Open, Closed, Closed, Closed, false),
     TestCase(1f, 2f, 0f, 1f, Closed, Closed, Closed, Open, false),
     TestCase(1f, 2f, 0f, 1f, Open, Closed, Closed, Open, false)]
    public void LeftAdjacentTest(float a, float b, float c, float d,
                                 IntervalType at, IntervalType bt, IntervalType ct, IntervalType dt, bool expected) {
      var interval1 = Interval.Range(a, b, at, bt);
      var interval2 = Interval.Range(c, d, ct, dt);
      Assert.That(interval1.LeftAdjacent(interval2), Is.EqualTo(expected));
    }

    [Test, TestCase(0f, 1f, 2f, 3f, Closed, Closed, Closed, Closed, false),
     TestCase(2f, 3f, 0f, 1f, Closed, Closed, Closed, Closed, false),
     TestCase(0f, 1f, 1f, 2f, Closed, Closed, Closed, Closed, false),
     TestCase(0f, 1f, 1f, 2f, Closed, Open, Closed, Closed, false),
     TestCase(0f, 1f, 1f, 2f, Closed, Closed, Open, Closed, false),
     TestCase(0f, 1f, 1f, 2f, Closed, Open, Open, Closed, false),
     TestCase(1f, 2f, 0f, 1f, Closed, Closed, Closed, Closed, true),
     TestCase(1f, 2f, 0f, 1f, Open, Closed, Closed, Closed, false),
     TestCase(1f, 2f, 0f, 1f, Closed, Closed, Closed, Open, false),
     TestCase(1f, 2f, 0f, 1f, Open, Closed, Closed, Open, false)]
    public void RightAdjacentTest(float a, float b, float c, float d,
                                  IntervalType at, IntervalType bt, IntervalType ct, IntervalType dt, bool expected) {
      var interval1 = Interval.Range(a, b, at, bt);
      var interval2 = Interval.Range(c, d, ct, dt);
      Assert.That(interval1.RightAdjacent(interval2), Is.EqualTo(expected));
    }

    const IntervalType Open = IntervalType.Open;
    const IntervalType Closed = IntervalType.Closed;
  }

}