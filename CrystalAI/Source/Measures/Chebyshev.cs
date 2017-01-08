// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// Chebyshev.cs is part of Crystal AI.
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

  public sealed class Chebyshev : IMeasure {
    public float Calculate(ICollection<Utility> elements) {
      var wsum = 0.0f;
      int count = elements.Count;

      if(count == 0)
        return 0.0f;

      foreach(var el in elements)
        wsum += el.Weight;

      if(CrMath.AeqZero(wsum))
        return 0.0f;

      var vlist = new List<float>(count);
      foreach(var el in elements)
        vlist.Add(el.Value * (el.Weight / wsum));

      var ret = vlist.Max<float>();
      return ret;
    }

    public IMeasure Clone() {
      return new Chebyshev();
    }
  }

}