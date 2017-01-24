// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IntervalExtensions.cs is part of Crystal AI.
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
namespace Crystal {

  public static class IntervalExtensions {
    static readonly IntervalType Open = IntervalType.Open;
    static readonly IntervalType Closed = IntervalType.Closed;

    /// <summary>
    ///   Returns true if the interval defined by @this overlaps with the interval defined
    ///   by other.
    /// </summary>
    public static bool Overlaps(this Interval<float> @this, Interval<float> other) {
      return @this.Contains(other.LowerBound) || @this.Contains(other.UpperBound);
    }

    /// <summary>
    ///   Returns true if the interval defined by @this is adjacent to the interval defined by
    ///   other, e.g. if @this = [a,b] and other = [b, c] or @this = [c, d] and other = [b, c].
    ///   Note this works for open or half-open intervals as well.
    /// </summary>
    /// <param name="this">Value.</param>
    /// <param name="other">Other.</param>
    public static bool Adjacent(this Interval<float> @this, Interval<float> other) {
      if(CrMath.AeqB(@this.UpperBound, other.LowerBound) &&
         @this.UpperBoundType == Closed &&
         other.LowerBoundType == Closed)
        return true;
      if(CrMath.AeqB(@this.LowerBound, other.UpperBound) &&
         @this.LowerBoundType == Closed &&
         other.UpperBoundType == Closed)
        return true;

      return false;
    }

    /// <summary>
    ///   Returns true if the interval defined by @this is to the left of the interval defined
    ///   by other and is adjacent, i.e. if @this = [a, b] and other = [b, c].
    ///   Note this works for open or half-open intervals as well.
    /// </summary>
    /// <returns><c>true</c>, if adjacent to the left, <c>false</c> otherwise.</returns>
    /// <param name="this">Value.</param>
    /// <param name="other">Other.</param>
    public static bool LeftAdjacent(this Interval<float> @this, Interval<float> other) {
      if(CrMath.AeqB(@this.UpperBound, other.LowerBound) &&
         @this.UpperBoundType == Closed &&
         other.LowerBoundType == Closed)
        return true;

      return false;
    }

    /// <summary>
    ///   Returns true if the interval defined by @this is to the right of the interval defined
    ///   by other and is adjacent, i.e. if other = [b, c] and @this = [c, d].
    /// </summary>
    /// <returns><c>true</c>, if adjacent to the right, <c>false</c> otherwise.</returns>
    /// <param name="this">Value.</param>
    /// <param name="other">Other.</param>
    public static bool RightAdjacent(this Interval<float> @this, Interval<float> other) {
      if(CrMath.AeqB(@this.LowerBound, other.UpperBound) &&
         @this.LowerBoundType == Closed &&
         other.UpperBoundType == Closed)
        return true;

      return false;
    }

    /// <summary>
    ///   If the every point (except the right bound) of @this is less than the interval other
    ///   this returns true. For example both of the following will return true: @this = [a, b],
    ///   other = [b, c], and @this = [a, b-K], other = [b, c] where K positive number.
    /// </summary>
    /// <returns><c>true</c>, if less than other interval, <c>false</c> otherwise.</returns>
    /// <param name="this">Value.</param>
    /// <param name="other">Other.</param>
    public static bool LessThan(this Interval<float> @this, Interval<float> other) {
      return @this.UpperBound <= other.LowerBound;
    }

    /// <summary>
    ///   If the every point (except the left bound) of @this is greater than the interval other
    ///   this returns true. For example both of the following will return true: @this = [c, d],
    ///   other = [k, c], and @this = [c+K, d], other = [k, c] where K positive number.
    /// </summary>
    /// <returns><c>true</c>, if less than other interval, <c>false</c> otherwise.</returns>
    /// <param name="this">Value.</param>
    /// <param name="other">Other.</param>
    public static bool GreaterThan(this Interval<float> @this, Interval<float> other) {
      return @this.LowerBound >= other.UpperBound;
    }

    /// <summary>
    ///   The standard CompareTo for intervals.
    /// </summary>
    /// <returns>Returns -1 if @this precedes other, 1 if other precedes @this and 0 otherwise.</returns>
    /// <param name="this">Value.</param>
    /// <param name="other">Other.</param>
    public static int CompareTo(this Interval<float> @this, Interval<float> other) {
      if(@this.LessThan(other))
        return -1;

      return @this.GreaterThan(other) ? 1 : 0;
    }

    /// <summary>
    ///   Returns the length (aka measure or size) of the interval. In the int case
    ///   this is also the cardinality-1 of the set corresponding to the elements of the
    ///   closed interval.
    /// </summary>
    public static int Length(this Interval<int> @this) {
      return @this.UpperBound - @this.LowerBound;
    }

    /// <summary>
    ///   Returns the length (aka measure or size) of the interval.
    /// </summary>
    public static float Length(this Interval<float> @this) {
      return @this.UpperBound - @this.LowerBound;
    }

    /// <summary>
    ///   Returns the length (aka measure or size) of the interval.
    /// </summary>
    public static double Length(this Interval<double> @this) {
      return @this.UpperBound - @this.LowerBound;
    }
  }

}