// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// Interval.cs is part of Crystal AI.
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


namespace Crystal {

  /// <summary>
  ///   An interval could be open and closed or combination of both at either
  ///   end.
  /// </summary>
  public enum IntervalType {
    Open,
    Closed
  }

  /// <summary>
  ///   Represents vectorless interval of the form [a, b] or (a, b) or any
  ///   combination of exclusive and inclusive end points.
  /// </summary>
  /// <typeparam name="T">Any comparent type</typeparam>
  /// <remarks>
  ///   This is a vectorless interval, therefore if end component is larger
  ///   than start component, the interval will swap the two ends around
  ///   such that a is always %lt; b.
  /// </remarks>
  public struct Interval<T> where T : struct, IComparable {
    public T LowerBound { get; }
    public T UpperBound { get; }

    public IntervalType LowerBoundType { get; }
    public IntervalType UpperBoundType { get; }

    /// <summary>
    ///   Check if given point lies within the interval.
    /// </summary>
    /// <param name="point">Point to check</param>
    /// <returns>True if point lies within the interval, otherwise false</returns>
    public bool Contains(T point) {
      if(LowerBound.GetType() != typeof(T)
         ||
         UpperBound.GetType() != typeof(T))
        throw new ArgumentException("Type mismatch", "point");

      var lower = LowerBoundType == IntervalType.Open
                    ? LowerBound.CompareTo(point) < 0
                    : LowerBound.CompareTo(point) <= 0;
      var upper = UpperBoundType == IntervalType.Open
                    ? UpperBound.CompareTo(point) > 0
                    : UpperBound.CompareTo(point) >= 0;

      return lower && upper;
    }

    /// <summary>
    ///   Convert to mathematical notation using open and closed parenthesis:
    ///   (, ), [, and ].
    /// </summary>
    /// <returns></returns>
    public override string ToString() {
      return string.Format(
                           "{0}{1}, {2}{3}",
                           LowerBoundType == IntervalType.Open ? "(" : "[",
                           LowerBound,
                           UpperBound,
                           UpperBoundType == IntervalType.Open ? ")" : "]"
                          );
    }

    public Interval(T lowerbound, T upperbound,
                    IntervalType lowerboundIntervalType = IntervalType.Closed,
                    IntervalType upperboundIntervalType = IntervalType.Closed)
      : this() {
      var a = lowerbound;
      var b = upperbound;
      var comparison = a.CompareTo(b);

      if(comparison > 0) {
        a = upperbound;
        b = lowerbound;
      }

      LowerBound = a;
      UpperBound = b;
      LowerBoundType = lowerboundIntervalType;
      UpperBoundType = upperboundIntervalType;
    }
  }

  /// <summary>
  ///   Static class to generate regular Intervals using common types.
  /// </summary>
  public static class Interval {
    public static Interval<int> Range(int lowerbound, int upperbound,
                                      IntervalType lowerboundIntervalType = IntervalType.Closed,
                                      IntervalType upperboundIntervalType = IntervalType.Closed) {
      return new Interval<int>(lowerbound, upperbound, lowerboundIntervalType, upperboundIntervalType);
    }

    public static Interval<float> Range(float lowerbound, float upperbound,
                                        IntervalType lowerboundIntervalType = IntervalType.Closed,
                                        IntervalType upperboundIntervalType = IntervalType.Closed) {
      return new Interval<float>(lowerbound, upperbound, lowerboundIntervalType, upperboundIntervalType);
    }

    public static Interval<double> Range(double lowerbound, double upperbound,
                                         IntervalType lowerboundIntervalType = IntervalType.Closed,
                                         IntervalType upperboundIntervalType = IntervalType.Closed) {
      return new Interval<double>(lowerbound, upperbound, lowerboundIntervalType, upperboundIntervalType);
    }
  }

}