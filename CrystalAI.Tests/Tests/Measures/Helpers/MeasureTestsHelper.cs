// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// MeasureTestsHelper.cs is part of Crystal AI.
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
using System.Collections.Generic;


namespace Crystal.MeasureTests {

  public static class MeasureTestsHelper {
    public static readonly Pcg Rng = new Pcg();

    public static List<Utility> RandomUtilityList(int count) {
      var list = new List<Utility>();
      for(int i = 0; i < count; i++) {
        var rValue = (float)Rng.NextDouble();
        var rWeight = (float)Rng.NextDouble();
        var util = new Utility(rValue, rWeight);
        list.Add(util);
      }

      return list;
    }

    public static List<Utility> RandomUtilityValueList(int veclen, float weight) {
      var list = new List<Utility>();
      for(int i = 0; i < veclen; i++) {
        var rValue = (float)Rng.NextDouble();
        var util = new Utility(rValue, weight);
        list.Add(util);
      }

      return list;
    }

    public static List<Utility> RandomUtilityWeightList(int veclen, float value) {
      var list = new List<Utility>();
      for(int i = 0; i < veclen; i++) {
        var rWeight = (float)Rng.NextDouble();
        var util = new Utility(value, rWeight);
        list.Add(util);
      }

      return list;
    }

    public static float CalculateChebyshevNorm(IEnumerable<Utility> list) {
      var vlist = new List<float>();
      var wsum = 0.0f;
      foreach(var u in list)
        wsum += u.Weight;

      if(CrMath.AeqZero(wsum))
        return 0.0f;

      foreach(var el in list)
        vlist.Add(el.Weight / wsum * el.Value);

      float res = -10.0f;
      foreach(var v in vlist)
        if(v >= res)
          res = v;

      return res;
    }

    public static float CalculateConstrainedChebyshevNorm(IEnumerable<Utility> list, float lowerBound = 0.0f) {
      var vlist = new List<float>();
      var wsum = 0.0f;
      foreach(var u in list)
        wsum += u.Weight;

      if(CrMath.AeqZero(wsum))
        return 0.0f;

      foreach(var el in list) {
        if(el.Combined < lowerBound)
          return 0.0f;

        vlist.Add(el.Weight / wsum * el.Value);
      }

      float res = -10.0f;
      foreach(var v in vlist)
        if(v >= res)
          res = v;

      return res;
    }

    public static float CalculateWeightedMetricsNorm(ICollection<Utility> list, float p = 2.0f) {
      var vlist = new List<float>();
      var wsum = 0.0f;
      foreach(var u in list)
        wsum += u.Weight;

      if(CrMath.AeqZero(wsum))
        return 0.0f;

      foreach(var u in list) {
        var v = u.Weight / wsum * (float)Math.Pow(u.Value, p);
        vlist.Add(v);
      }

      var sum = 0.0f;
      foreach(var v in vlist)
        sum += v;

      return (float)Math.Pow(sum, 1 / p);
    }

    public static float CalculateConstrainedWeightedMetricsNorm(ICollection<Utility> list, float p = 2.0f,
                                                                float lowerBound = 0.0f) {
      var vlist = new List<float>();
      var wsum = 0.0f;
      foreach(var u in list)
        wsum += u.Weight;

      if(CrMath.AeqZero(wsum))
        return 0.0f;

      foreach(var u in list) {
        if(u.Combined < lowerBound)
          return 0.0f;

        var v = u.Weight / wsum * (float)Math.Pow(u.Value, p);
        vlist.Add(v);
      }

      var sum = 0.0f;
      foreach(var v in vlist)
        sum += v;

      return (float)Math.Pow(sum, 1 / p);
    }
  }

}