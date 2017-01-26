// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// OptionConstructor.cs is part of Crystal AI.
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

  /// <summary>
  ///   <see cref="T:Crystal.IOption"/> constructor convenience class.
  /// </summary>
  public static class OptionConstructor {
    /// <summary>
    ///   Creates an <see cref="T:Crystal.IOption"/> that uses the <see cref="T:Crystal.Chebyshev"/>
    ///   measure.
    /// </summary>
    /// <param name="optionId">The option identifier.</param>
    /// <param name="collection">The collection.</param>
    /// <returns></returns>
    public static IOption Chebyshev(string optionId, IOptionCollection collection) {
      var option = new Option(optionId, collection);
      option.Measure = new Chebyshev();
      return option;
    }

    /// <summary>
    ///   Creates an <see cref="T:Crystal.IOption"/> that uses the <see cref="T:Crystal.WeightedMetrics"/>
    ///   measure.
    /// </summary>
    /// <param name="optionId">The option identifier.</param>
    /// <param name="collection">The collection.</param>
    /// <param name="pNorm">The p norm.</param>
    /// <returns></returns>
    public static IOption WeightedMetrics(string optionId, IOptionCollection collection,
                                          float pNorm = 2.0f) {
      var option = new Option(optionId, collection);
      option.Measure = new WeightedMetrics(pNorm);
      return option;
    }

    /// <summary>
    ///   Creates an <see cref="T:Crystal.IOption"/> that uses the <see cref="T:Crystal.ConstrainedChebyshev"/>
    ///   measure.
    /// </summary>
    /// <param name="optionId">The option identifier.</param>
    /// <param name="collection">The collection.</param>
    /// <param name="lowerBound">The lower bound.</param>
    /// <returns></returns>
    public static IOption ConstrainedChebyshev(string optionId, IOptionCollection collection,
                                               float lowerBound = 0.0f) {
      var option = new Option(optionId, collection);
      option.Measure = new ConstrainedChebyshev(lowerBound);
      return option;
    }

    /// <summary>
    ///   Creates an <see cref="T:Crystal.IOption"/> that uses the <see cref="T:Crystal.ConstrainedWeightedMetrics"/>
    ///   measure.
    /// </summary>
    /// <param name="optionId">The option identifier.</param>
    /// <param name="collection">The collection.</param>
    /// <param name="pNorm">The p norm.</param>
    /// <param name="lowerBound">The lower bound.</param>
    /// <returns></returns>
    public static IOption ConstrainedWeightedMetrics(string optionId, IOptionCollection collection,
                                                     float pNorm = 2.0f, float lowerBound = 0.0f) {
      var option = new Option(optionId, collection);
      option.Measure = new ConstrainedWeightedMetrics(pNorm, lowerBound);
      return option;
    }

    /// <summary>
    ///   Creates an <see cref="T:Crystal.IOption"/> that uses the <see cref="T:Crystal.Chebyshev"/>
    ///   measure.
    /// </summary>
    internal static IOption Chebyshev() {
      var option = new Option();
      option.Measure = new Chebyshev();
      return option;
    }

    /// <summary>
    ///   Creates an <see cref="T:Crystal.IOption"/> that uses the <see cref="T:Crystal.WeightedMetrics"/>
    ///   measure.
    /// </summary>
    /// <param name="pNorm">The p norm.</param>
    /// <returns></returns>
    internal static IOption WeightedMetrics(float pNorm = 2.0f) {
      var option = new Option();
      option.Measure = new WeightedMetrics(pNorm);
      return option;
    }

    /// <summary>
    ///   Creates an <see cref="T:Crystal.IOption"/> that uses the <see cref="T:Crystal.ConstrainedChebyshev"/>
    ///   measure.
    /// </summary>
    /// <param name="lowerBound">The lower bound.</param>
    /// <returns></returns>
    internal static IOption ConstrainedChebyshev(float lowerBound = 0.0f) {
      var option = new Option();
      option.Measure = new ConstrainedChebyshev(lowerBound);
      return option;
    }

    /// <summary>
    ///   Creates an <see cref="T:Crystal.IOption"/> that uses the <see cref="T:Crystal.ConstrainedWeightedMetrics"/>
    ///   measure.
    /// </summary>
    /// <param name="pNorm">The p norm.</param>
    /// <param name="lowerBound">The lower bound.</param>
    /// <returns></returns>
    internal static IOption ConstrainedWeightedMetrics(float pNorm = 2.0f, float lowerBound = 0.0f) {
      var option = new Option();
      option.Measure = new ConstrainedWeightedMetrics(pNorm, lowerBound);
      return option;
    }
  }

}