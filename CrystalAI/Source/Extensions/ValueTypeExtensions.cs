// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ValueTypeExtensions.cs is part of Crystal AI.
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

  public static class ValueTypeExtensions {
    /// <summary>
    ///   Returns true if val is within the given interval. Note that unqualified intervals are assumed
    ///   to be closed(!) intervals, i.e. [a,b].
    ///   WARNING: Never use this extension like this: 1.0.InInterval(interval), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static bool InInterval(this float @this, Interval<float> interval) {
      return interval.Contains(@this);
    }

    /// <summary>
    ///   If the value is larger than the upper bound of the interval this will return
    ///   true and false otherwise.
    ///   WARNING: Never use this extension like this: 1.0.AboveInterval(interval), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static bool AboveInterval(this float @this, Interval<float> interval) {
      return @this > interval.UpperBound;
    }

    /// <summary>
    ///   If the value is smaller than the lower bound of the interval this will return
    ///   true and false otherwise.
    ///   WARNING: Never use this extension like this: 1.0.BelowInterval(interval), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static bool BelowInterval(this float @this, Interval<float> interval) {
      return @this < interval.LowerBound;
    }

    /// <summary>
    ///   If the value is below the interval, this will return -1, if it is above
    ///   the interval this will return 1 and 0 otherwise. Note that the interval has a
    ///   closed interval semantics, i.e. interval = [a,b].
    ///   WARNING: Never use this extension like this: 1.0.CompareTo(interval), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static int CompareToInterval(this float @this, Interval<float> interval) {
      if(@this.BelowInterval(interval))
        return -1;

      return @this.AboveInterval(interval) ? 1 : 0;
    }

    /// <summary>
    ///   Clamp the given value, min and max.
    ///   WARNING: Never use this extension like this: 1.0.Clamp{T}(min, max), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static T Clamp<T>(this T @this, T min, T max) where T : IComparable<T> {
      if(min.CompareTo(max) > 0)
        throw new MinGreaterThanMaxException();

      if(@this.CompareTo(min) < 0)
        return min;

      return @this.CompareTo(max) > 0 ? max : @this;
    }

    /// <summary>
    ///   Clamp the given value in the interval [min, max].
    ///   WARNING: Never use this extension like this: 1.0f.Clamp(min, max), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static float Clamp(this float @this, float min, float max) {
      return @this.Clamp<float>(min, max);
    }

    /// <summary>
    ///   Clamp the given value in the interval [min, max].
    ///   WARNING: Never use this extension like this: 1.0.Clamp(min, max), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static double Clamp(this double @this, double min, double max) {
      return @this.Clamp<double>(min, max);
    }

    /// <summary>
    ///   Clamp the given value in the interval [0, 1].
    ///   WARNING: Never use this extension like this: 1.0f.Clamp01(), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static float Clamp01(this float @this) {
      return @this.Clamp<float>(0.0f, 1.0f);
    }

    /// <summary>
    ///   Clamp01 the given value.
    ///   WARNING: Never use this extension like this: 1.0.Clamp01(), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static double Clamp01(this double @this) {
      return @this.Clamp<double>(0.0, 1.0);
    }

    /// <summary>
    ///   If val is higher than the lower bound it is returned unchanged, otherwise the
    ///   lower bound is returned.
    ///   WARNING: Never use this extension like this: 1.ClampToLowerBound(lowerBound), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static int ClampToLowerBound(this int @this, int lowerBound) {
      return @this > lowerBound ? @this : lowerBound;
    }

    /// <summary>
    ///   If val is higher than the lower bound it is returned unchanged, otherwise the
    ///   lower bound is returned.
    ///   WARNING: Never use this extension like this: 1.0f.ClampToLowerBound(lowerBound), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static float ClampToLowerBound(this float @this, float lowerBound) {
      return @this > lowerBound ? @this : lowerBound;
    }

    /// <summary>
    ///   If val is higher than the lower bound it is returned unchanged, otherwise the
    ///   lower bound is returned.
    ///   WARNING: Never use this extension like this: 1.0.ClampToLowerBound(lowerBound), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static double ClampToLowerBound(this double @this, double lowerBound) {
      return @this > lowerBound ? @this : lowerBound;
    }

    /// <summary>
    ///   If val is lower than the upper bound it is returned unchanged, otherwise the
    ///   upper bound is returned.
    ///   WARNING: Never use this extension like this: 1.ClampToUpperBound(upperBound), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static int ClampToUpperBound(this int @this, int upperBound) {
      return @this < upperBound ? @this : upperBound;
    }

    /// <summary>
    ///   If val is lower than the upper bound it is returned unchanged, otherwise the
    ///   upper bound is returned.
    ///   WARNING: Never use this extension like this: 1.0f.ClampToUpperBound(upperBound), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static float ClampToUpperBound(this float @this, float upperBound) {
      return @this < upperBound ? @this : upperBound;
    }

    /// <summary>
    ///   If val is lower than the upper bound it is returned unchanged, otherwise the
    ///   upper bound is returned.
    ///   WARNING: Never use this extension like this: 1.0.ClampToUpperBound(upperBound), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static double ClampToUpperBound(this double val, double upperBound) {
      return val < upperBound ? val : upperBound;
    }

    /// <summary>
    ///   If the value is non-negative it is returned as is. If it is negative its clamped to 0.
    ///   WARNING: Never use this extension like this: 1.ClampToPositive(), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static int ClampToPositive(this int @this) {
      return @this >= 0 ? @this : 0;
    }

    /// <summary>
    ///   If the value is non-negative it is returned as is. If it is negative its clamped to 0.0f.
    ///   WARNING: Never use this extension like this: 1.0f.ClampToPositive(), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static float ClampToPositive(this float @this) {
      return @this >= 0.0f ? @this : 0.0f;
    }

    /// <summary>
    ///   If the value is non-negative it is returned as is. If it is negative its clamped to 0.0.
    ///   WARNING: Never use this extension like this: 1.0.ClampToPositive(), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static double ClampToPositive(this double @this) {
      return @this >= 0.0 ? @this : 0.0;
    }

    /// <summary>
    ///   If the value is non-positive it is returned as is, otherwise a zero is returned. A more
    ///   apt signature would be ClampToNonPositive, but this is a bit too verbose and potentially confusing
    ///   for some.
    ///   WARNING: Never use this extension like this: 1.ClampToNegative(), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static int ClampToNegative(this int @this) {
      return @this <= 0 ? @this : 0;
    }

    /// <summary>
    ///   If the value is non-positive it is returned as is, otherwise a zero is returned. A more
    ///   apt signature would be ClampToNonPositive, but this is a bit too verbose and potentially confusing
    ///   for some.
    ///   WARNING: Never use this extension like this: 1.0f.ClampToNegative(), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static float ClampToNegative(this float @this) {
      return @this <= 0.0f ? @this : 0.0f;
    }

    /// <summary>
    ///   If the value is non-positive it is returned as is, otherwise a zero is returned. A more
    ///   apt signature would be ClampToNonPositive, but this is a bit too verbose and potentially confusing
    ///   for some.
    ///   WARNING: Never use this extension like this: 1.0.ClampToNegative(), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    ///   For example this, -1.0.ClampToNegative() is interpreted by the extension method
    ///   like this 1.0.ClampToNegative()! You could use parentheses but to be on the safe side avoid this usage.
    /// </summary>
    public static double ClampToNegative(this double @this) {
      return @this <= 0.0 ? @this : 0.0;
    }

    /// <summary>
    ///   Returns the smallest integer greater to or equal to val.
    ///   WARNING: Never use this extension like this: 1.0f.Ceil(), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static float Ceil(this float @this) {
      return (float)Math.Ceiling(@this);
    }

    /// <summary>
    ///   Returns the smallest integer greater to or equal to val.
    ///   WARNING: Never use this extension like this: 1.0.Ceil(), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static double Ceil(this double @this) {
      return Math.Ceiling(@this);
    }

    /// <summary>
    ///   Returns the smallest integer greater to or equal to val.
    ///   WARNING: Never use this extension like this: 1.0f.CeilToInt(), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static int CeilToInt(this float @this) {
      return (int)Math.Ceiling(@this);
    }

    /// <summary>
    ///   Returns the smallest integer greater to or equal to val.
    ///   WARNING: Never use this extension like this: 1.0.CeilToInt(), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static int CeilToInt(this double @this) {
      return (int)Math.Ceiling(@this);
    }

    /// <summary>
    ///   Returns the largest integer smaller to or equal to val.
    ///   WARNING: Never use this extension like this: 1.0f.Floor(), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static float Floor(this float @this) {
      return (float)Math.Floor(@this);
    }

    /// <summary>
    ///   Returns the largest integer smaller to or equal to val.
    ///   WARNING: Never use this extension like this: 1.0.Floor(), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static double Floor(this double @this) {
      return Math.Floor(@this);
    }

    /// <summary>
    ///   Returns the largest integer smaller to or equal to val.
    ///   WARNING: Never use this extension like this: 1.0f.FloorToInt(), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static int FloorToInt(this float @this) {
      return (int)Math.Floor(@this);
    }

    /// <summary>
    ///   Returns the largest integer smaller to or equal to val.
    ///   WARNING: Never use this extension like this: 1.0.FloorToInt(), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static int FloorToInt(this double @this) {
      return (int)Math.Floor(@this);
    }

    /// <summary>
    ///   Rescales the given value to the interval [0,1]. If the value is smaller than min, it will be
    ///   clamped to min and if the value is larger than max it will be clamped to max.
    ///   WARNING: Never use this extension like this: 1.0f.Normalize01(min, max), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static float Normalize01(this float @this, float min, float max) {
      @this = @this.Clamp<float>(min, max);
      @this = @this - min;
      var df = max - min;
      if(CrMath.AeqZero(df))
        throw new MinEqualMaxException();

      return @this / df;
    }

    /// <summary>
    ///   Rescales the given value to the interval [0,1]. If the value is smaller than min, it will be
    ///   clamped to min and if the value is larger than max it will be clamped to max.
    ///   WARNING: Never use this extension like this: 1.0.Normalize(min, max), since if the value is negative
    ///   the sign (if there is one) will not be passed to the extension method leading to unexpected results.
    /// </summary>
    public static double Normalize01(this double @this, double min, double max) {
      @this = @this.Clamp<double>(min, max);
      @this = @this - min;
      var df = max - min;
      if(CrMath.AeqZero(df))
        throw new MinEqualMaxException();

      return @this / df;
    }
  }

  /// <summary>
  ///   Minimum greater than max exception.
  /// </summary>
  public class MinGreaterThanMaxException : Exception {
  }

  /// <summary>
  ///   Minimum equal max exception.
  /// </summary>
  public class MinEqualMaxException : Exception {
  }

}