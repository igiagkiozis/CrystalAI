// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// Behaviour.cs is part of Crystal AI.
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

  public sealed class Behaviour : CompositeConsideration, IBehaviour {
    IBehaviourCollection _collection;
    List<IOption> _options;
    List<Utility> _optionUtilities;

    ISelector _selector;

    public ISelector Selector {
      get { return _selector; }
      set { _selector = value ?? _selector; }
    }

    public bool AddOption(IOption option) {
      if(option == null)
        return false;
      if(_options.Contains(option))
        return false;

      InternalAddOption(option);
      return true;
    }

    public bool AddOption(string optionId) {
      if(string.IsNullOrEmpty(optionId))
        return false;
      if(_collection == null)
        return false;
      if(_collection.Options.Contains(optionId) == false)
        return false;

      InternalAddOption(optionId);
      return true;
    }

    /// <summary>
    ///   Selects the action for execution, given the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>The action to execute.</returns>
    public IAction Select(IContext context) {
      for(int i = 0; i < _options.Count; i++) {
        _options[i].Consider(context);
        _optionUtilities[i] = _options[i].Utility;
      }

      return SelectAction();
    }

    public override IConsideration Clone() {
      return new Behaviour(this);
    }

    public Behaviour() {
      Initialize();
    }

    Behaviour(Behaviour other) : base(other) {
      CreateLists();
      _collection = other._collection;
      _selector = other.Selector.Clone();
      Measure = other.Measure.Clone();

      for(int i = 0; i < other._options.Count; i++) {
        var o = other._options[i].Clone() as IOption;
        _options.Add(o);
        _optionUtilities.Add(o.Utility);
      }
    }

    public Behaviour(string nameId, IBehaviourCollection collection) : base(collection?.Options?.Considerations) {
      if(collection == null)
        throw new BehaviourCollectionNullException();

      NameId = nameId;
      _collection = collection;
      Initialize();
      if(_collection.Add(this) == false)
        throw new BehaviourAlreadyExistsInCollectionException(nameId);
    }

    void Initialize() {
      Measure = new Chebyshev();
      _selector = new MaxUtilitySelector();
      CreateLists();
    }

    void InternalAddOption(IOption option) {
      _options.Add(option);
      _optionUtilities.Add(new Utility(0.0f, 0.0f));
    }

    void InternalAddOption(string optionId) {
      _options.Add(_collection.Options.Create(optionId));
      _optionUtilities.Add(new Utility(0.0f, 0.0f));
    }

    void CreateLists() {
      _options = new List<IOption>();
      _optionUtilities = new List<Utility>();
    }

    IAction SelectAction() {
      var idx = Selector.Select(_optionUtilities);
      IOption option = idx >= 0 ? _options[idx] : null;
      return option?.Action;
    }

    internal class BehaviourCollectionNullException : Exception {
    }

    internal class BehaviourAlreadyExistsInCollectionException : Exception {
      string _message;

      public override string Message {
        get { return _message; }
      }

      public BehaviourAlreadyExistsInCollectionException(string nameId) {
        _message = string.Format("{0} already exists in behaviour collection", nameId);
      }
    }
  }

}