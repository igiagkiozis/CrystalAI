// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// GreedConsideration.cs is part of Crystal AI.
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

  public class GreedConsideration : ConsiderationBase<CharacterContext> {
    IEvaluator _evaluator;
    public static readonly string Name = "Greed";

    public override void Consider(CharacterContext context) {
      Utility = new Utility(_evaluator.Evaluate(context.Greed), Weight);
    }

    public override IConsideration Clone() {
      return new GreedConsideration(this);
    }

    public GreedConsideration() {
      Initialize();
    }

    GreedConsideration(GreedConsideration other) : base(other) {
      Initialize();
    }

    public GreedConsideration(IConsiderationCollection collection)
      : base(Name, collection) {
      Initialize();
    }

    void Initialize() {
      var xa = Pcg.Default.NextFloat(20f, 30f);
      var ptA = new Pointf(xa, 0f);
      var xb = Pcg.Default.NextFloat(40f, 100f);
      var ptB = new Pointf(xb, 1f);
      _evaluator = new SigmoidEvaluator(ptA, ptB, 0.3f);
    }

  }

}