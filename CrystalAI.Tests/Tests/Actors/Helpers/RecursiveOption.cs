// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// RecursiveOption.cs is part of Crystal AI.
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

  public class RecursiveOption : Option {
    public override void Consider(IContext context) {
      Utility = new Utility(1.0f, 1.0f);
    }

    public override IConsideration Clone() {
      return new RecursiveOption(this);
    }

//    IUtilityAi _ai;

//    public RecursiveOption(IUtilityAi ai) {
//      _ai = ai;
//      SetAction(new AITransition(ai));
//    }

    RecursiveOption(RecursiveOption other) : base(other) {
    }

    public RecursiveOption(string nameId, IOptionCollection collection) : base(nameId, collection) {
    }
  }

}