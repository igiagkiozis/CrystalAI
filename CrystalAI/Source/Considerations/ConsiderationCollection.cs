// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ConsiderationCollection.cs is part of Crystal AI.
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

  public class ConsiderationCollection : IConsiderationCollection {
    Dictionary<string, IConsideration> _considerationsMap;

    public bool Add(IConsideration consideration) {
      if(consideration == null)
        return false;
      if(_considerationsMap.ContainsKey(consideration.NameId))
        return false;
      if(string.IsNullOrEmpty(consideration.NameId))
        return false;

      _considerationsMap.Add(consideration.NameId, consideration);
      return true;
    }

    public bool Contains(string nameId) {
      return _considerationsMap.ContainsKey(nameId);
    }

    public void Clear() {
      _considerationsMap.Clear();
    }

    public IConsideration Create(string nameId) {
      return _considerationsMap.ContainsKey(nameId) ? _considerationsMap[nameId].Clone() : null;
    }

    public ConsiderationCollection() {
      _considerationsMap = new Dictionary<string, IConsideration>();
    }
  }

}