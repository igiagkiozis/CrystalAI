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

  public static class OptionConstructor {
    public static IOption Chebyshev() {
      var option = new Option();
      option.Measure = new Chebyshev();
      return option;
    }

    public static IOption Chebyshev(string nameId, IOptionCollection collection) {
      var option = new Option(nameId, collection);
      option.Measure = new Chebyshev();
      return option;
    }

    public static IOption WeightedMetrics(float pNorm = 2.0f) {
      var option = new Option();
      option.Measure = new WeightedMetrics(pNorm);
      return option;
    }

    public static IOption WeightedMetrics(string nameId, IOptionCollection collection,
                                          float pNorm = 2.0f) {
      var option = new Option(nameId, collection);
      option.Measure = new WeightedMetrics(pNorm);
      return option;
    }

    public static IOption ConstrainedChebyshev(float lowerBound = 0.0f) {
      var option = new Option();
      option.Measure = new ConstrainedChebyshev(lowerBound);
      return option;
    }

    public static IOption ConstrainedChebyshev(string nameId, IOptionCollection collection,
                                               float lowerBound = 0.0f) {
      var option = new Option(nameId, collection);
      option.Measure = new ConstrainedChebyshev(lowerBound);
      return option;
    }

    public static IOption ConstrainedWeightedMetrics(float pNorm = 2.0f, float lowerBound = 0.0f) {
      var option = new Option();
      option.Measure = new ConstrainedWeightedMetrics(pNorm, lowerBound);
      return option;
    }

    public static IOption ConstrainedWeightedMetrics(string nameId, IOptionCollection collection,
                                                     float pNorm = 2.0f, float lowerBound = 0.0f) {
      var option = new Option(nameId, collection);
      option.Measure = new ConstrainedWeightedMetrics(pNorm, lowerBound);
      return option;
    }
  }

}