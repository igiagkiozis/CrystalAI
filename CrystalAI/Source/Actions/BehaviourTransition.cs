// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// BehaviourTransition.cs is part of Crystal AI.
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

  public sealed class BehaviourTransition : ActionBase, ITransition {
    Behaviour _behaviour;
    IBehaviourCollection _behaviourCollection;
    string _behaviourId;

    public Behaviour Behaviour {
      get {
        if(_behaviour != null)
          return _behaviour;

        if(string.IsNullOrEmpty(_behaviourId) ||
           _behaviourCollection.Contains(_behaviourId) == false)
          throw new BehaviourDoesNotExistException(_behaviourId);

        _behaviour = _behaviourCollection.Create(_behaviourId) as Behaviour;
        return _behaviour;
      }
      set { _behaviour = value ?? _behaviour; }
    }

    public IAction Select(IContext context) {
      return Behaviour.Select(context);
    }

    public override IAction Clone() {
      return new BehaviourTransition(this);
    }

    internal BehaviourTransition() {
    }

    BehaviourTransition(BehaviourTransition other) : base(other) {
      _behaviourId = other._behaviourId;
      _behaviourCollection = other._behaviourCollection;
    }

    public BehaviourTransition(Behaviour behaviour) {
      if(behaviour == null)
        throw new BehaviourNullException();

      _behaviour = behaviour;
    }

    public BehaviourTransition(string nameId, string behaviourId, IBehaviourCollection collection)
      : base(nameId, collection?.Options?.Actions) {
      if(string.IsNullOrEmpty(behaviourId))
        throw new NameIdEmptyOrNullException();

      _behaviourId = behaviourId;
      _behaviourCollection = collection;
    }

    internal class BehaviourNullException : Exception {
    }

    internal class BehaviourDoesNotExistException : Exception {
      string _message;

      public override string Message {
        get { return _message; }
      }

      public BehaviourDoesNotExistException(string nameId) {
        _message = string.Format("Error: {0} does not exist in the Behaviour collection!", nameId);
      }
    }
  }

}