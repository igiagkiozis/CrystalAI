// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// Utility.cs is part of Crystal AI.
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

  [Serializable]
  public struct Utility : IEquatable<Utility>, IComparable<Utility> {
    float _value;

    float _weight;

    /// <summary>
    ///   The value of the option associated with this Utility.
    ///   This parameter is normalized to the interval [0,1].
    /// </summary>
    public float Value {
      get { return _value; }
      set { _value = value.Clamp01(); }
    }

    /// <summary>
    ///   The weighting of the option associated with this Utility.
    ///   This parameter is normalized to the interval [0,1].
    /// </summary>
    /// <value>The weight.</value>
    public float Weight {
      get { return _weight; }
      set { _weight = value.Clamp01(); }
    }

    /// <summary>
    ///   Returns the Value*Weight of this Utiliity.
    /// </summary>
    public float Combined {
      get { return Value * Weight; }
    }

    /// <summary>
    ///   Gets a value indicating whether the combined utility is zero.
    /// </summary>
    /// <value><c>true</c> if the combined utility is zero; otherwise, <c>false</c>.</value>
    public bool IsZero {
      get { return CrMath.AeqZero(Combined); }
    }

    /// <summary>
    ///   Gets a value indicating whether the combined utility is one.
    /// </summary>
    /// <value><c>true</c> if the combined utility is one; otherwise, <c>false</c>.</value>
    public bool IsOne {
      get { return CrMath.AeqB(Combined, 1.0f); }
    }

    /// <summary>
    ///   Determines whether the specified <see cref="Utility"/> is equal to the current
    ///   <see cref="Utility"/>.
    /// </summary>
    /// <param name="other">The <see cref="Utility"/> to compare with the current <see cref="Utility"/>.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="Utility"/> is equal to the current
    ///   <see cref="Utility"/>; otherwise, <c>false</c>.
    /// </returns>
    public bool Equals(Utility other) {
      return CrMath.AeqB(Value, other.Value) && CrMath.AeqB(Weight, other.Weight);
    }

    public override bool Equals(object obj) {
      if(obj == null)
        return false;

      var util = (Utility)obj;
      return Equals(util);
    }

    public override int GetHashCode() {
      return Combined.GetHashCode();
    }

    /// <summary>
    ///   Compares to.
    /// </summary>
    /// <returns>The to.</returns>
    /// <param name="other">Other.</param>
    public int CompareTo(Utility other) {
      return Combined.CompareTo(other.Combined);
    }

    /// <param name="a">The alpha component.</param>
    public static implicit operator Utility(float a) {
      return new Utility(a);
    }

    /// <param name="a">The alpha component.</param>
    /// <param name="b">The blue component.</param>
    public static bool operator ==(Utility a, Utility b) {
      return CrMath.AeqB(a.Value, b.Value) && CrMath.AeqB(a.Weight, b.Weight);
    }

    /// <param name="a">The alpha component.</param>
    /// <param name="b">The blue component.</param>
    public static bool operator !=(Utility a, Utility b) {
      return CrMath.AneqB(a.Value, b.Value) || CrMath.AneqB(a.Weight, b.Weight);
    }

    /// <param name="a">The alpha component.</param>
    /// <param name="b">The blue component.</param>
    public static bool operator >(Utility a, Utility b) {
      return a.Combined > b.Combined;
    }

    /// <param name="a">The alpha component.</param>
    /// <param name="b">The blue component.</param>
    public static bool operator <(Utility a, Utility b) {
      return a.Combined < b.Combined;
    }

    /// <param name="a">The alpha component.</param>
    /// <param name="b">The blue component.</param>
    public static bool operator >=(Utility a, Utility b) {
      return a.Combined >= b.Combined;
    }

    /// <param name="a">The alpha component.</param>
    /// <param name="b">The blue component.</param>
    public static bool operator <=(Utility a, Utility b) {
      return a.Combined <= b.Combined;
    }

    /// <summary>
    ///   Returns a <see cref="string"/> that represents the current <see cref="Utility"/>.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the current <see cref="Utility"/>.</returns>
    public override string ToString() {
      return string.Format("[Utility: Value={0}, Weight={1}, Combined={2}]", Value, Weight, Combined);
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="Utility"/> struct.
    /// </summary>
    /// <param name="value">Value.</param>
    public Utility(float value) {
      _value = value.Clamp01();
      _weight = 1.0f;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="Utility"/> struct.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <param name="weight">Weight.</param>
    public Utility(float value, float weight) {
      _value = value.Clamp01();
      _weight = weight.Clamp01();
    }
  }

}