// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ISelector.cs is part of Crystal AI.
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


namespace Crystal {

  /// <summary>
  /// Interface for Selectors. Selectors are functions that accept a vector
  /// of <see cref="T:Crystal.Utility"/>s and return an index that corresponds
  /// to the selected <see cref="T:Crystal.Utility"/>.
  /// </summary>
  /// <seealso cref="T:Crystal.IAiPrototype`1" />
  public interface ISelector : IAiPrototype<ISelector> {
    /// <summary>
    /// Selects a <see cref="T:Crystal.Utility"/> from the given set and returns its
    /// index.
    /// </summary>
    /// <param name="elements">The elements.</param>
    /// <returns>The index of the selected utility. This returns -1 no selection
    /// was made or if the count of elements was 0.</returns>
    int Select(ICollection<Utility> elements);
  }

}