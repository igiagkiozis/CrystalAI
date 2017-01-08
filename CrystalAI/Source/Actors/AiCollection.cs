// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// AiCollection.cs is part of Crystal AI.
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

  public class AiCollection : IAiCollection {
    Dictionary<string, IUtilityAi> _aiMap;
    public IBehaviourCollection Behaviours { get; private set; }

    public IOptionCollection Options {
      get { return Behaviours.Options; }
    }

    public IConsiderationCollection Considerations {
      get { return Behaviours.Options.Considerations; }
    }

    public IActionCollection Actions {
      get { return Behaviours.Options.Actions; }
    }

    public bool Add(IUtilityAi ai) {
      if(ai == null)
        return false;
      if(string.IsNullOrEmpty(ai.NameId))
        return false;
      if(_aiMap.ContainsKey(ai.NameId))
        return false;

      _aiMap.Add(ai.NameId, ai);
      return true;
    }

    public bool Contains(string nameId) {
      return _aiMap.ContainsKey(nameId);
    }

    public IUtilityAi GetAi(string nameId) {
      return _aiMap.ContainsKey(nameId) ? _aiMap[nameId] : null;
    }

    public IUtilityAi Create(string nameId) {
      return _aiMap.ContainsKey(nameId) ? _aiMap[nameId].Clone() : null;
    }

    public void Clear() {
      _aiMap.Clear();
    }

    public void ClearAll() {
      _aiMap.Clear();
      Behaviours.Clear();
      Options.Clear();
      Considerations.Clear();
      Actions.Clear();
    }

    public AiCollection(IBehaviourCollection behaviourCollection) {
      if(behaviourCollection == null)
        throw new BehaviourCollectionNullException();

      _aiMap = new Dictionary<string, IUtilityAi>();
      Behaviours = behaviourCollection;
    }

    internal class BehaviourCollectionNullException : Exception {
    }
  }

}