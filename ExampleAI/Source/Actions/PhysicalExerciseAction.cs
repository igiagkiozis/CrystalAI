// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// PhysicalExerciseAction.cs is part of Crystal AI.
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

  public class PhysicalExerciseAction : ActionBase<CharacterContext> {
    public static readonly string Name = "Exercise";
    float dFit;
    public override IAction Clone() {
      return new PhysicalExerciseAction(this);
    }

    protected override void OnExecute(CharacterContext context) {
      context.Character.Report(Name);
      dFit = 0f;
      Exercise(context);
    }

    protected override void OnUpdate(CharacterContext context) {
      if(dFit >= 60f || context.Fitness > 98f)
        EndInSuccess(context);

      Exercise(context);
    }

    public PhysicalExerciseAction() {
    }

    PhysicalExerciseAction(PhysicalExerciseAction other) : base(other) {
    }

    public PhysicalExerciseAction(IActionCollection collection) : base(Name, collection) {
    }

    void Exercise(CharacterContext context) {
      dFit += 10f;
      context.Fitness += 10f;
      context.Bladder += 1.5f;
      context.Hunger += 1.5f;
      context.Thirst += 2.5f;
      context.Energy -= 7.5f;
      // Those expensive gyms.. ;) 
      context.Wealth -= 20f;
      context.Cleanliness -= 2.5f;
    }

  }

}