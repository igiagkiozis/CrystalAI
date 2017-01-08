// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// AiTransition.cs is part of Crystal AI.
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

  public sealed class AiTransition : ActionBase, ITransition {
    IAiCollection _aiCollection;
    string _aiNameId;

    IUtilityAi _targetAi;

    public override IAction Clone() {
      return new AiTransition(this);
    }

    public IAction Select(IContext context) {
      return TargetAi.Select(context);
    }

    internal AiTransition() {
    }

    public AiTransition(IUtilityAi ai) {
      if(ai == null)
        throw new TargetAiNullException();

      _targetAi = ai;
    }

    AiTransition(AiTransition other) : base(other) {
      _aiNameId = other._aiNameId;
      _aiCollection = other._aiCollection;
    }

    public AiTransition(string nameId, string aiNameId, IAiCollection collection) : base(nameId, collection?.Actions) {
      if(string.IsNullOrEmpty(nameId))
        throw new NameIdEmptyOrNullException();
      if(string.IsNullOrEmpty(aiNameId))
        throw new NameIdEmptyOrNullException();

      NameId = nameId;
      _aiNameId = aiNameId;
      _aiCollection = collection;
    }

    internal IUtilityAi TargetAi {
      get {
        if(_targetAi == null) {
          if(_aiCollection.Contains(_aiNameId) == false)
            throw new TargetAiDoesNotExistException(_aiNameId);

          _targetAi = _aiCollection.Create(_aiNameId);
        }

        return _targetAi;
      }
    }

    internal class TargetAiNullException : Exception {
    }

    internal class TargetAiDoesNotExistException : Exception {
      string _message;

      public override string Message {
        get { return _message; }
      }

      public TargetAiDoesNotExistException(string nameId) {
        _message = string.Format("Error: {0} does not exist in the AI collection!", nameId);
      }
    }
  }

}