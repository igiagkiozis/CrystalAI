// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// AIComponents.cs is part of Crystal AI.
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


namespace Crystal.CollectionsTests {

  public interface IActions {
  }

  public interface IConsiderations {
  }

  public interface ICompositeConsiderations {
  }

  public interface IOptions {
  }

  public interface IBehaviours {
  }

  public interface IUtilityAIs {
  }


  public class AiComponentCollection {
    Dictionary<string, IAction> _actionsMap;
    Dictionary<string, IConsideration> _considerationsMap;
    Dictionary<string, IOption> _optionsMap;
    Dictionary<string, IBehaviour> _behavioursMap;
    Dictionary<string, IUtilityAi> _aiMap;

    public AiComponentCollection() {
      _actionsMap = new Dictionary<string, IAction>();
      _considerationsMap = new Dictionary<string, IConsideration>();
      _optionsMap = new Dictionary<string, IOption>();
      _behavioursMap = new Dictionary<string, IBehaviour>();
      _aiMap = new Dictionary<string, IUtilityAi>();
    }

    bool Add(IAction action) {
      if(action == null)
        return false;
      if(_actionsMap.ContainsKey(action.NameId))
        return false;

      _actionsMap.Add(action.NameId, action);
      return true;
    }

    bool Add(IConsideration consideration) {
      if(consideration == null)
        return false;
      if(_considerationsMap.ContainsKey(consideration.NameId))
        return false;

      _considerationsMap.Add(consideration.NameId, consideration);
      return true;
    }

    bool Add(IOption option) {
      if(option == null)
        return false;
      if(_optionsMap.ContainsKey(option.NameId))
        return false;

      _optionsMap.Add(option.NameId, option);
      return true;
    }

    bool Add(IBehaviour behaviour) {
      if(behaviour == null)
        return false;
      if(_behavioursMap.ContainsKey(behaviour.NameId))
        return false;

      _behavioursMap.Add(behaviour.NameId, behaviour);
      return true;
    }

    bool Add(IUtilityAi ai) {
      if(ai == null)
        return false;
      if(_aiMap.ContainsKey(ai.NameId))
        return false;

      _aiMap.Add(ai.NameId, ai);
      return true;
    }

    IAction GetAction(string name) {
      return null;
    }

    IConsideration GetConsideration(string name) {
      return null;
    }

    IOption GetOption(string name) {
      return null;
    }

    IBehaviour GetBehaviour(string name) {
      return null;
    }

    IUtilityAi GetAi(string name) {
      return null;
    }

    bool ContainsAction(string name) {
      return _actionsMap.ContainsKey(name);
    }

    bool ContainsConsideration(string name) {
      return _considerationsMap.ContainsKey(name);
    }

    bool ContainsOption(string name) {
      return _optionsMap.ContainsKey(name);
    }

    bool ContainsBehaviour(string name) {
      return _behavioursMap.ContainsKey(name);
    }

    bool ContainsAi(string name) {
      return _aiMap.ContainsKey(name);
    }
  }

}