// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// PrimaryNeedsBehaviourConsideration.cs is part of Crystal AI.
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
using Crystal;


namespace ExampleAI {

  public class PrimaryNeedsBehaviourConsideration : ConsiderationBase<CharacterContext> {
    IEvaluator _evaluator;
    public static readonly string Name = "PrimaryNeedsBehaviourConsideration";

    public override void Consider(CharacterContext context) {
      //Utility = _evaluator.Evaluate();
    }

    public override IConsideration Clone() {
      return new PrimaryNeedsBehaviourConsideration(this);
    }

    public PrimaryNeedsBehaviourConsideration() {
    }

    PrimaryNeedsBehaviourConsideration(PrimaryNeedsBehaviourConsideration other) : base(other) {
    }

    public PrimaryNeedsBehaviourConsideration(IConsiderationCollection collection)
      : base(Name, collection) {
    }
  }

}