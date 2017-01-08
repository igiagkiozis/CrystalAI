// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ArrayExtensions.cs is part of Crystal AI.
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
  ///   Array extensions.
  /// </summary>
  public static class ArrayExtensions {
    /// <summary>Gets the index of a value in an array.</summary>
    /// <typeparam name="T">The type of items in the array</typeparam>
    /// <param name="array">The array.</param>
    /// <param name="value">The value to look for.</param>
    /// <returns>The index of the value, or -1 if not found</returns>
    public static int IndexOf<T>(this T[] array, T value) where T : IEquatable<T> {
      for(var i = 0; i < array.Length; i++)
        if(array[i].Equals(value))
          return i;

      return -1;
    }
  }

}