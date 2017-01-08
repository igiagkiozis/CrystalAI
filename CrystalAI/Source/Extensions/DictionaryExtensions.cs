// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// DictionaryExtensions.cs is part of Crystal AI.
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
using System.Linq;


namespace Crystal {

  /// <summary>
  ///   Dictionary extensions.
  /// </summary>
  public static class DictionaryExtensions {
    /// <summary>
    ///   Gets a value from a @this or null if the key was not found.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="T">The type of the value</typeparam>
    /// <param name="this">The @this.</param>
    /// <param name="key">The key.</param>
    /// <returns>The value if found; otherwise <c>null</c></returns>
    public static T Value<TKey, T>(this Dictionary<TKey, T> @this, TKey key) {
      T obj;
      return @this.TryGetValue(key, out obj) ? obj : default(T);
    }

    /// <summary>
    ///   Works like List.RemoveAll.
    /// </summary>
    /// <typeparam name="TKey">Key type</typeparam>
    /// <typeparam name="TValue">Value type</typeparam>
    /// <param name="this">Dictionary to remove entries from</param>
    /// <param name="match">Delegate to match keys</param>
    /// <returns>Number of entries removed</returns>
    public static int RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> @this, Predicate<TKey> match) {
      if(@this == null ||
         match == null)
        return 0;

      var keysToRemove = @this.Keys.Where(k => match(k)).ToList();
      if(keysToRemove.Count > 0)
        foreach(var key in keysToRemove)
          @this.Remove(key);

      return keysToRemove.Count;
    }
  }

}