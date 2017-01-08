// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ConsiderationConstructor.cs is part of Crystal AI.
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

  public static class ConsiderationConstructor {
    public static ICompositeConsideration Create(IMeasure measure) {
      var consideration = new CompositeConsideration {
        Measure = measure
      };
      return consideration;
    }

    public static ICompositeConsideration Create(string nameId, IConsiderationCollection collection,
                                                 IMeasure measure) {
      var consideration = new CompositeConsideration(nameId, collection) {
        Measure = measure
      };
      return consideration;
    }

    public static ICompositeConsideration Chebyshev() {
      var consideration = new CompositeConsideration {
        Measure = new Chebyshev()
      };
      return consideration;
    }

    public static ICompositeConsideration Chebyshev(string nameId, IConsiderationCollection collection) {
      var consideration = new CompositeConsideration(nameId, collection) {
        Measure = new Chebyshev()
      };
      return consideration;
    }

    public static ICompositeConsideration WeightedMetrics(float pNorm = 2.0f) {
      var consideration = new CompositeConsideration {
        Measure = new WeightedMetrics(pNorm)
      };
      return consideration;
    }

    public static ICompositeConsideration WeightedMetrics(string nameId, IConsiderationCollection collection,
                                                          float pNorm = 2.0f) {
      var consideration = new CompositeConsideration(nameId, collection) {
        Measure = new WeightedMetrics(pNorm)
      };
      return consideration;
    }

    public static ICompositeConsideration ConstrainedChebyshev(float lowerBound = 0.0f) {
      var consideration = new CompositeConsideration {
        Measure = new ConstrainedChebyshev(lowerBound)
      };
      return consideration;
    }

    public static ICompositeConsideration ConstrainedChebyshev(string nameId, IConsiderationCollection collection,
                                                               float lowerBound = 0.0f) {
      var consideration = new CompositeConsideration(nameId, collection) {
        Measure = new ConstrainedChebyshev(lowerBound)
      };
      return consideration;
    }

    public static ICompositeConsideration ConstrainedWeightedMetrics(float pNorm = 2.0f, float lowerBound = 0.0f) {
      var consideration = new CompositeConsideration {
        Measure = new ConstrainedWeightedMetrics(pNorm, lowerBound)
      };
      return consideration;
    }

    public static ICompositeConsideration ConstrainedWeightedMetrics(string nameId, IConsiderationCollection collection,
                                                                     float pNorm = 2.0f, float lowerBound = 0.0f) {
      var consideration = new CompositeConsideration(nameId, collection) {
        Measure = new ConstrainedWeightedMetrics(pNorm, lowerBound)
      };
      return consideration;
    }

    public static ICompositeConsideration Multiplicative() {
      var consideration = new CompositeConsideration {
        Measure = new MultiplicativePseudoMeasure()
      };
      return consideration;
    }

    public static ICompositeConsideration Multiplicative(string nameId, IConsiderationCollection collection) {
      var consideration = new CompositeConsideration(nameId, collection) {
        Measure = new MultiplicativePseudoMeasure()
      };
      return consideration;
    }
  }

}