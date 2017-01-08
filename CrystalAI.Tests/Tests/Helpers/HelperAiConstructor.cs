// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// HelperAiConstructor.cs is part of Crystal AI.
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


namespace CrystalAI.TestHelpers {

  public class HelperAiConstructor : AiConstructor {
    protected override void DefineActions() {
//      A = new MockAction(EatAction, Actions) { Message = "Eating..." };
//      A = new MockAction(DrinkAction, Actions) { Message = "Drinking..." };
//      A = new MockAction(ToiletAction, Actions) { Message = "Toilet..." };
//      A = new MockAction(ShowerAction, Actions) { Message = "Shower..." };

      A = new MockAction(TestActionDefs.MockAction, Actions);
      A = new MockGenericAction(TestActionDefs.GenericMockAction, Actions);
      A = new FailingAction(TestActionDefs.FailingAction, Actions);
      A = new FailingGenericAction(TestActionDefs.GenericFailingAction, Actions);
//      A = new MockGenericAction(TestActionDefs.);
    }

    protected override void DefineConsiderations() {
//      C = new MockConsideration(HungerConsideration, Considerations);
//      C = new MockConsideration(ThirstConsideration, Considerations);
//      C = new MockConsideration(BladderConsideration, Considerations);
//      C = new MockConsideration(CleanlinessConsideration, Considerations);
    }

    protected override void DefineOptions() {
//      O = new Option(EatOption, Options);
//      O.SetAction(EatAction);
//      IsOkay(O.AddConsideration(HungerConsideration));
//
//      O = new Option(DrinkOption, Options);
//      O.SetAction(DrinkAction);
//      IsOkay(O.AddConsideration(ThirstConsideration));
//
//      O = new Option(ToiletOption, Options);
//      O.SetAction(ToiletAction);
//      IsOkay(O.AddConsideration(BladderConsideration));
//
//      O = new Option(ShowerOption, Options);
//      O.SetAction(ShowerAction);
//      IsOkay(O.AddConsideration(CleanlinessConsideration));
    }

    protected override void DefineBehaviours() {
//      B = new Behaviour(SurvivalBehaviour, Behaviours);
    }

    protected override void ConfigureAi() {
//      Ai = new UtilityAi(CharacterAi, AIs);
    }

    public HelperAiConstructor() : base(AiCollectionConstructor.Create()) {
    }

    public HelperAiConstructor(IAiCollection collection) : base(collection) {
    }
  }

}