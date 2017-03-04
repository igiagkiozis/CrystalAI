// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// RandomSelector.cs is part of Crystal AI.
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
  ///   This selector returns a random <see cref="T:Crystal.Utility"/> from the
  ///   supplied <see cref="T:System.Collections.ICollection"/>.
  /// </summary>
  /// <seealso cref="T:Crystal.ISelector"/>
  public sealed class RandomSelector : ISelector {
    Pcg _random;

    /// <summary>
    ///   Selects a <see cref="T:Crystal.Utility"/> from the given set and returns its
    ///   index.
    /// </summary>
    /// <param name="elements">The elements.</param>
    /// <returns>
    ///   The index of the selected utility. This returns -1 no selection
    ///   was made or if the count of elements was 0.
    /// </returns>
    public int Select(ICollection<Utility> elements) {
      var count = elements.Count;
      return count == 0 ? -1 : _random.Next(0, count);
    }

    /// <summary>
    ///   Creates a new instance of the implementing class. Note that the semantics here
    ///   are somewhat vague, however, by convention the "Prototype Pattern" uses a "Clone"
    ///   function. Note that this may have very different semantics when compared with either
    ///   shallow or deep cloning. When implementing this remember to include only the defining
    ///   characteristics of the class and not its state!
    /// </summary>
    /// <returns></returns>
    public ISelector Clone() {
      return new RandomSelector();
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="T:Crystal.RandomSelector"/> class.
    /// </summary>
    public RandomSelector() {
      _random = new Pcg();
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="T:Crystal.RandomSelector"/> class.
    /// </summary>
    /// <param name="random">The random.</param>
    public RandomSelector(Pcg random) {
      _random = random;
    }
  }

}