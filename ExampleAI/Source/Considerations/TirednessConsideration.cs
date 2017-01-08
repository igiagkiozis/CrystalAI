// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// EnergyConsideration.cs is part of Crystal AI.
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

  public class TirednessConsideration : ConsiderationBase<CharacterContext> {
    IEvaluator _evaluator;
    public static readonly string Name = "Tiredness";

    public override void Consider(CharacterContext context) {
      Utility = new Utility(_evaluator.Evaluate(context.Energy), Weight);
    }

    public override IConsideration Clone() {
      return new TirednessConsideration(this);
    }

    public TirednessConsideration() {
      Initialize();
    }

    TirednessConsideration(TirednessConsideration other) : base(other) {
      Initialize();
    }

    public TirednessConsideration(IConsiderationCollection collection)
      : base(Name, collection) {
      Initialize();
    }

    void Initialize() {
      var ptA1 = new Pointf(0f, 1f);
      var ptB1 = new Pointf(30f, 0f);
      _evaluator = new PowerEvaluator(ptA1, ptB1, 2f);      
    }

  }

}