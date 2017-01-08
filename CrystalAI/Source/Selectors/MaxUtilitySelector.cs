// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// MaxUtilitySelector.cs is part of Crystal AI.
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
using System.Linq;


namespace Crystal {

  public sealed class MaxUtilitySelector : ISelector {
    public int Select(ICollection<Utility> elements) {
      var count = elements.Count;
      if(count == 0)
        return -1;
      if(count == 1)
        return 0;

      var elemList = elements.ToList();
      var maxUtil = 0.0f;
      var selIdx = -1;
      for(var i = 0; i < count; i++) {
        var el = elemList[i];
        if(el.Combined > maxUtil) {
          maxUtil = el.Combined;
          selIdx = i;
        }
      }

      return selIdx;
    }

    public ISelector Clone() {
      return new MaxUtilitySelector();
    }
  }

}