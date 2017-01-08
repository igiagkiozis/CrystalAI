// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// AIConstructor.cs is part of Crystal AI.
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


namespace Crystal {

  public abstract class AiConstructor {
    public IAiCollection Collection { get; protected set; }

    public IActionCollection Actions {
      get { return Collection.Actions; }
    }

    public IConsiderationCollection Considerations {
      get { return Collection.Considerations; }
    }

    public IOptionCollection Options {
      get { return Collection.Options; }
    }

    public IBehaviourCollection Behaviours {
      get { return Collection.Behaviours; }
    }

    public IAiCollection AIs {
      get { return Collection; }
    }


    public IUtilityAi Create(string name) {
      return AIs.Create(name);
    }

    protected void IsOkay(bool expression) {
      if(expression == false)
        throw new AiConfigurationxception();
    }

    protected abstract void DefineActions();
    protected abstract void DefineConsiderations();
    protected abstract void DefineOptions();
    protected abstract void DefineBehaviours();
    protected abstract void ConfigureAi();

    protected AiConstructor(IAiCollection collection) {
      if(collection == null)
        throw new AiCollectionNullException();

      Collection = collection;
      Initialize();
    }

    void Initialize() {
      DefineActions();
      DefineConsiderations();
      DefineOptions();
      DefineBehaviours();
      ConfigureAi();
    }

    protected IAction A;
    protected IConsideration C;
    protected ICompositeConsideration Cc;
    protected IOption O;
    protected IBehaviour B;
    protected IUtilityAi Ai;

    internal class AiCollectionNullException : Exception {
    }

    internal class AiConfigurationxception : Exception {
    }
  }

}