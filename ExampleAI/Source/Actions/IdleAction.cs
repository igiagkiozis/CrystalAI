// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IdleAction.cs is part of Crystal AI.
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

  public class IdleAction : ActionBase<CharacterContext> {
    public static readonly string Name = "Idle";

    public override IAction Clone() {
      return new IdleAction(this);
    }

    protected override void OnExecute(CharacterContext context) {
      context.Character.Report(Name);
      context.Fitness -= 0.2f;
      EndInSuccess(context);
    }

    protected override void OnUpdate(CharacterContext context) {
      
    }

    public IdleAction() {
    }

    IdleAction(IdleAction other) : base(other) {
    }

    public IdleAction(IActionCollection collection) : base(Name, collection) {
    }
  }

}