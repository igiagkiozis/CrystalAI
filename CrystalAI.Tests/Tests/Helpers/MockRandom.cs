// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// MockRandom.cs is part of Crystal AI.
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
using System.Collections.Generic;


#pragma warning disable


namespace Crystal {

  /// <summary>
  ///   A mock random number generator for testing. This returns either Value or the numbers in
  ///   the Values list in sequence.
  /// </summary>
  public class MockRandom : Pcg {
    double _value;
    List<double> _values;

    float _valuef;
    List<float> _valuesf;

    int _intValue;
    List<int> _intValues;

    int _dCounter;

    int _fCounter;

    int _iCounter;

    public double Value {
      get { return _value; }
      set { _value = value; }
    }

    public List<double> Values {
      get { return _values; }
      set { _values = value; }
    }

    public float Valuef {
      get { return _valuef; }
      set { _valuef = value; }
    }

    public List<float> Valuesf {
      get { return _valuesf; }
      set { _valuesf = value; }
    }

    public int IntValue {
      get { return _intValue; }
      set { _intValue = value; }
    }

    public List<int> IntValues {
      get { return _intValues; }
      set { _intValues = value; }
    }


    public override int Next() {
      return DoSampleInteger();
    }

    public override int Next(int maxExclusive) {
      return DoSampleInteger(maxExclusive);
    }

    public override int Next(int minInclusive, int maxExclusive) {
      return DoSampleInteger(minInclusive, maxExclusive);
    }

    public override double NextDouble() {
      return DoSample();
    }

    protected double DoSample() {
      if(_values.Count == 0)
        return _value;

      int idx = _dCounter % _values.Count;
      _dCounter++;
      return _values[idx];
    }

    protected float DoSampleFloat() {
      if(_valuesf.Count == 0)
        return _valuef;

      int idx = _fCounter % _valuesf.Count;
      _fCounter++;
      return _valuesf[idx];
    }

    protected int DoSampleInteger() {
      if(_intValues.Count == 0)
        return _intValue;

      int idx = _iCounter % _intValues.Count;
      _iCounter++;
      return _intValues[idx] < 0 ? 0 : _intValues[idx];
    }

    public MockRandom()
      : this(0) {
    }

    public MockRandom(int seed) {
      _values = new List<double>();
      _intValues = new List<int>();
    }

    int DoSampleInteger(int maxExclusive) {
      int ret = DoSampleInteger();
      if(ret < 0)
        return 0;

      return ret < maxExclusive ? ret : maxExclusive - 1;
    }

    int DoSampleInteger(int minInclusive, int maxExclusive) {
      int ret;
      if(_intValues.Count == 0)
        ret = _intValue;
      else {
        int idx = _iCounter % _intValues.Count;
        _iCounter++;
        ret = _intValues[idx];
      }

      if(ret < minInclusive)
        return minInclusive;

      return ret >= maxExclusive ? maxExclusive - 1 : _intValue;
    }
  }

}