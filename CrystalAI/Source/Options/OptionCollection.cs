// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// OptionCollection.cs is part of Crystal AI.
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


namespace Crystal {

  public class OptionCollection : IOptionCollection {
    Dictionary<string, IOption> _optionsMap;
    public IActionCollection Actions { get; private set; }
    public IConsiderationCollection Considerations { get; private set; }

    public bool Add(IOption option) {
      if(option == null)
        return false;
      if(string.IsNullOrEmpty(option.NameId))
        return false;
      if(_optionsMap.ContainsKey(option.NameId))
        return false;

      _optionsMap.Add(option.NameId, option);
      return true;
    }

    public bool Contains(string nameId) {
      return _optionsMap.ContainsKey(nameId);
    }

    public void Clear() {
      _optionsMap.Clear();
    }

    public void ClearAll() {
      _optionsMap.Clear();
      Actions.Clear();
      Considerations.Clear();
    }

    public IOption Create(string nameId) {
      return _optionsMap.ContainsKey(nameId) ? _optionsMap[nameId].Clone() as IOption : null;
    }

    public OptionCollection(IActionCollection actionCollection, IConsiderationCollection considerationCollection) {
      if(actionCollection == null)
        throw new ActionCollectionNullException();
      if(considerationCollection == null)
        throw new ConsiderationCollectionNullException();

      _optionsMap = new Dictionary<string, IOption>();
      Actions = actionCollection;
      Considerations = considerationCollection;
    }

    internal class ActionCollectionNullException : Exception {
    }

    internal class ConsiderationCollectionNullException : Exception {
    }
  }

}