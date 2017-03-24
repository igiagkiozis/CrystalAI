namespace Crystal {

  /// <summary>
  ///   Extensions for <see cref="T:Crystal.Pcg"/> and <see cref="T:Crystal.PcgExtended"/> generartors.
  /// </summary>
  public static class PcgExtensions {
    /// <summary>
    ///   Returns a random single precision floating point number within the given interval.
    /// </summary>
    /// <param name="this">The this.</param>
    /// <param name="interval">The interval.</param>
    public static float NextFloat(this Pcg @this, Interval<float> interval) {
      return @this.NextFloat(interval.LowerBound, interval.UpperBound);
    }

    /// <summary>
    ///   Returns a random single precision floating point number within the given interval.
    /// </summary>
    /// <param name="this">The this.</param>
    /// <param name="interval">The interval.</param>
    public static float NextFloat(this PcgExtended @this, Interval<float> interval) {
      return @this.NextFloat(interval.LowerBound, interval.UpperBound);
    }
  }

}