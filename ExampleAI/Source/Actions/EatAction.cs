// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// EatAction.cs is part of Crystal AI.
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

  public class EatAction : ActionBase<CharacterContext> {
    public static readonly string Name = "Eat";

    public override IAction Clone() {
      return new EatAction(this);
    }

    protected override void OnExecute(CharacterContext context) {
      context.Character.Report(Name);
      context.Hunger -= 80f;
      context.Bladder += 20f;
      context.Wealth -= 50f;
      EndInSuccess(context);
    }

    protected override void OnUpdate(CharacterContext context) {
    }

    public EatAction() {
    }

    EatAction(EatAction other) : base(other) {
    }

    public EatAction(IActionCollection collection) : base(Name, collection) {
    }
  }

}