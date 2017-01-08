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

  public sealed class RandomSelector : ISelector {
    Pcg _random;

    public int Select(ICollection<Utility> elements) {
      var count = elements.Count;
      return count == 0 ? -1 : _random.Next(0, count);
    }

    public ISelector Clone() {
      return new RandomSelector();
    }

    public RandomSelector() {
      _random = new Pcg();
    }

    public RandomSelector(Pcg random) {
      _random = random;
    }
  }

}