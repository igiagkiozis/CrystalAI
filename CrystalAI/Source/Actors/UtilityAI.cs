// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// UtilityAI.cs is part of Crystal AI.
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
using System.Linq;


namespace Crystal {

  public sealed class UtilityAi : IUtilityAi {
    Dictionary<string, Behaviour> _behaviourMap;
    List<Behaviour> _behaviours;
    List<Utility> _behaviourUtilities;
    IAiCollection _collection;

    ISelector _selector;

    public string NameId { get; set; }

    public ISelector Selector {
      get { return _selector; }
      set { _selector = value ?? _selector; }
    }

    public bool Add(Behaviour behaviour) {
      if(behaviour == null)
        return false;
      if(_behaviours.Contains(behaviour))
        return false;

      return InternalAddBehaviour(behaviour);
    }

    public bool Remove(Behaviour behaviour) {
      if(string.IsNullOrEmpty(behaviour?.NameId))
        return false;

      return InternalRemove(behaviour.NameId);
    }

    public bool InternalRemove(string behaviourId) {
      if(_behaviourMap.ContainsKey(behaviourId) == false)
        return false;

      var idx = _behaviours.IndexOf(_behaviourMap[behaviourId]);
      _behaviourUtilities.RemoveAt(idx);
      _behaviours.RemoveAt(idx);
      _behaviourMap.Remove(behaviourId);
      return true;
    }

    public bool AddBehaviour(string behaviourId) {
      if(_collection == null)
        return false;
      if(_collection.Behaviours.Contains(behaviourId) == false)
        return false;
      if(_behaviours.Any(b => string.Equals(b.NameId, behaviourId)))
        return false;

      return InternalAddBehaviour(behaviourId);
    }

    public bool RemoveBehaviour(string behaviourId) {
      return InternalRemove(behaviourId);
    }

    public IAction Select(IContext context) {
      if(_behaviours.Count == 0)
        return null;
      if(_behaviours.Count == 1)
        return _behaviours[0].Select(context);

      UpdateBehaviourUtilitites(context);
      return SelectAction(context);
    }

    public IUtilityAi Clone() {
      return new UtilityAi(this);
    }

    public UtilityAi() {
      Initialize();
    }

    UtilityAi(UtilityAi other) {
      NameId = other.NameId;
      _collection = other._collection;
      Initialize();
      _selector = other._selector.Clone();

      for(int i = 0; i < other._behaviours.Count; i++) {
        var b = other._behaviours[i].Clone() as Behaviour;
        _behaviours.Add(b);
        _behaviourUtilities.Add(b.Utility);
      }
    }

    public UtilityAi(string nameId, IAiCollection collection) {
      if(string.IsNullOrEmpty(nameId))
        throw new NameIdIsNullOrEmptyException();
      if(collection == null)
        throw new AiCollectionNullException();

      NameId = nameId;
      _collection = collection;
      Initialize();
      if(_collection.Add(this) == false)
        throw new AiAlreadyExistsInCollectionException(nameId);
    }

    bool InternalAddBehaviour(Behaviour behaviour) {
      if(string.IsNullOrEmpty(behaviour.NameId) ||
         _behaviourMap.ContainsKey(behaviour.NameId))
        return false;

      _behaviourMap.Add(behaviour.NameId, behaviour);
      _behaviours.Add(behaviour);
      _behaviourUtilities.Add(new Utility(0.0f, 0.0f));
      return true;
    }

    bool InternalAddBehaviour(string behaviourId) {
      if(_behaviourMap.ContainsKey(behaviourId))
        return false;

      var behaviour = _collection.Behaviours.Create(behaviourId) as Behaviour;
      _behaviourMap.Add(behaviourId, behaviour);
      _behaviours.Add(behaviour);
      _behaviourUtilities.Add(new Utility(0.0f, 0.0f));
      return true;
    }

    void Initialize() {
      _selector = new MaxUtilitySelector();
      _behaviours = new List<Behaviour>();
      _behaviourMap = new Dictionary<string, Behaviour>();
      _behaviourUtilities = new List<Utility>();
    }

    void UpdateBehaviourUtilitites(IContext context) {
      for(int i = 0; i < _behaviours.Count; i++) {
        _behaviours[i].Consider(context);
        _behaviourUtilities[i] = _behaviours[i].Utility;
      }
    }

    IAction SelectAction(IContext context) {
      var idx = Selector.Select(_behaviourUtilities);
      IBehaviour selectedBehaviour = idx >= 0 ? _behaviours[idx] : null;
      return selectedBehaviour?.Select(context);
    }

    internal class AiCollectionNullException : Exception {
    }

    internal class NameIdIsNullOrEmptyException : Exception {
    }

    internal class AiAlreadyExistsInCollectionException : Exception {
      string _message;

      public override string Message {
        get { return _message; }
      }

      public AiAlreadyExistsInCollectionException() {
      }

      public AiAlreadyExistsInCollectionException(string nameId) {
        _message = string.Format("{0} already exists in behaviour collection", nameId);
      }
    }
  }

}