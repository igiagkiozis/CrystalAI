// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ExAiConstructor.cs is part of Crystal AI.
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

  internal class ExAiConstructor : AiConstructor {
    protected override void DefineActions() {
      A = new DrinkAction(Actions);
      A = new EatAction(Actions);
      A = new ShowerAction(Actions);
      A = new ToiletAction(Actions);
      A = new SleepAction(Actions);
      A = new PhysicalExerciseAction(Actions);

      A = new WorkAction(Actions);
      A = new ReadAction(Actions);

      A = new IdleAction(Actions);
    }

    protected override void DefineConsiderations() {
      C = new BladderConsideration(Considerations);
      C = new ShowerConsideration(Considerations);
      C = new HungerConsideration(Considerations);
      C = new ThirstConsideration(Considerations);
      C = new EnergyConsideration(Considerations);
      C = new TirednessConsideration(Considerations);
      C = new HowUnfitConsideration(Considerations);
      C = new GreedConsideration(Considerations);
      C = new CuriosityConsideration(Considerations);

      Cc = new CompositeConsideration(ConsiderationDefs.LiveLong, Considerations);
      Cc.AddConsideration(ConsiderationDefs.Tiredness);
      Cc.AddConsideration(ConsiderationDefs.Hunger);
      Cc.AddConsideration(ConsiderationDefs.Thirst);
      Cc.Measure = new Chebyshev();

      Cc = new CompositeConsideration(ConsiderationDefs.Prosper, Considerations);
      Cc.AddConsideration(ConsiderationDefs.Curiosity);
      Cc.AddConsideration(ConsiderationDefs.Greed);
      Cc.Measure = new WeightedMetrics(3.0f);
    }

    protected override void DefineOptions() {
      O = new Option(OptionDefs.Drink, Options);
      O.SetAction(ActionDefs.Drink);
      O.AddConsideration(ConsiderationDefs.Thirst);

      O = new Option(OptionDefs.Eat, Options);
      O.SetAction(ActionDefs.Eat);
      O.AddConsideration(ConsiderationDefs.Hunger);

      O = new Option(OptionDefs.Shower, Options);
      O.SetAction(ActionDefs.Shower);
      O.AddConsideration(ConsiderationDefs.Shower);

      O = new Option(OptionDefs.Sleep, Options);
      O.SetAction(ActionDefs.Sleep);
      O.AddConsideration(ConsiderationDefs.Tiredness);

      O = new Option(OptionDefs.Toilet, Options);
      O.SetAction(ActionDefs.Toilet);
      O.AddConsideration(ConsiderationDefs.Bladder);

      O = new Option(OptionDefs.Exercise, Options);
      O.SetAction(ActionDefs.PhysicalExercise);
      O.AddConsideration(ConsiderationDefs.HowUnfit);
      O.AddConsideration(ConsiderationDefs.Energy);
      O.Measure = new MultiplicativePseudoMeasure();

      O = new Option(OptionDefs.Work, Options);
      O.SetAction(ActionDefs.Work);
      O.AddConsideration(ConsiderationDefs.Greed);

      O = new Option(OptionDefs.Read, Options);
      O.SetAction(ActionDefs.Read);
      O.AddConsideration(ConsiderationDefs.Curiosity);

      O = new ConstantUtilityOption(OptionDefs.Idle, Options);
      O.SetAction(ActionDefs.Idle);
      O.DefaultUtility = new Utility(0.01f, 1f);
    }

    protected override void DefineBehaviours() {
      B = new Behaviour(BehaviourDefs.LiveLong, Behaviours);
      B.AddOption(OptionDefs.Drink);
      B.AddOption(OptionDefs.Eat);
      B.AddOption(OptionDefs.Toilet);
      B.AddOption(OptionDefs.Sleep);
      B.AddOption(OptionDefs.Shower);
      B.AddOption(OptionDefs.Exercise);
      B.AddOption(OptionDefs.Idle);
      B.AddConsideration(ConsiderationDefs.LiveLong);

      B = new Behaviour(BehaviourDefs.Prosper, Behaviours);
      B.AddOption(OptionDefs.Work);
      B.AddOption(OptionDefs.Read);
      B.AddConsideration(ConsiderationDefs.Prosper);
    }

    protected override void ConfigureAi() {
      Ai = new UtilityAi(AiDefs.ToonAi, AIs);
      Ai.AddBehaviour(BehaviourDefs.LiveLong);
      Ai.AddBehaviour(BehaviourDefs.Prosper);
    }

    public ExAiConstructor(IAiCollection collection) : base(collection) {
    }
  }

}