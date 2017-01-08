// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// HungerConsideration.cs is part of Crystal AI.
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


namespace Crystal.ActorTests {

  public class HungerConsideration : ConsiderationBase<CharacterContext> {
    IEvaluator _ev;

    public override void Consider(CharacterContext context) {
      Utility = new Utility(_ev.Evaluate(context.Hunger), Weight);
    }

    public override IConsideration Clone() {
      return new HungerConsideration(this);
    }

    public HungerConsideration() {
      var ptA = new Pointf(0.0f, 0.0f);
      var ptB = new Pointf(1.0f, 1.0f);
      _ev = new SigmoidEvaluator(ptA, ptB, -0.5f);
    }

    HungerConsideration(HungerConsideration other) : base(other) {
      var ptA = new Pointf(0.0f, 0.0f);
      var ptB = new Pointf(1.0f, 1.0f);
      _ev = new SigmoidEvaluator(ptA, ptB, -0.5f);
    }
  }

}