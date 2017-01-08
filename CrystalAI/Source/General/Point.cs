// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// Point.cs is part of Crystal AI.
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
  ///   Represents a point in 2D space.
  /// </summary>
  public struct Pointf : IEquatable<Pointf> {
    public float X { get; private set; }
    public float Y { get; private set; }

    public bool Equals(Pointf other) {
      return X.Equals(other.X) && Y.Equals(other.Y);
    }

    public override string ToString() {
      return string.Format("({0}, {1})", X, Y);
    }

    public Pointf(float x, float y) : this() {
      X = x;
      Y = y;
    }
  }

}