// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// MasterAIConstructor.cs is part of Crystal AI.
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
namespace Crystal.GeneralTests {

  public class MasterAiConstructor : AiConstructor {
    // Actions
    public readonly string EatAction = "Eat";
    public readonly string DrinkAction = "Drink";
    public readonly string ToiletAction = "Toilet";
    public readonly string ShowerAction = "Shower";

    // Considerations
    public readonly string HungerConsideration = "Hunger";
    public readonly string ThirstConsideration = "Thirst";
    public readonly string BladderConsideration = "Bladder";
    public readonly string CleanlinessConsideration = "Cleanliness";

    // Options
    public readonly string EatOption = "Eat";
    public readonly string DrinkOption = "Drink";
    public readonly string ToiletOption = "Toilet";
    public readonly string ShowerOption = "Shower";

    // Behaviours
    public readonly string SurvivalBehaviour = "Survive";

    // AIs
    public readonly string CharacterAi = "CharacterAI";


    protected override void DefineActions() {
      A = new MockAction(EatAction, Actions) {Message = "Eating..."};
      A = new MockAction(DrinkAction, Actions) {Message = "Drinking..."};
      A = new MockAction(ToiletAction, Actions) {Message = "Toilet..."};
      A = new MockAction(ShowerAction, Actions) {Message = "Shower..."};
    }

    protected override void DefineConsiderations() {
      C = new MockConsideration(HungerConsideration, Considerations);
      C = new MockConsideration(ThirstConsideration, Considerations);
      C = new MockConsideration(BladderConsideration, Considerations);
      C = new MockConsideration(CleanlinessConsideration, Considerations);
    }

    protected override void DefineOptions() {
      O = new Option(EatOption, Options);
      O.SetAction(EatAction);
      IsOkay(O.AddConsideration(HungerConsideration));

      O = new Option(DrinkOption, Options);
      O.SetAction(DrinkAction);
      IsOkay(O.AddConsideration(ThirstConsideration));

      O = new Option(ToiletOption, Options);
      O.SetAction(ToiletAction);
      IsOkay(O.AddConsideration(BladderConsideration));

      O = new Option(ShowerOption, Options);
      O.SetAction(ShowerAction);
      IsOkay(O.AddConsideration(CleanlinessConsideration));
    }

    protected override void DefineBehaviours() {
      B = new Behaviour(SurvivalBehaviour, Behaviours);
    }

    protected override void ConfigureAi() {
      Ai = new UtilityAi(CharacterAi, AIs);
    }

    public MasterAiConstructor(IAiCollection collection) : base(collection) {
    }
  }

}