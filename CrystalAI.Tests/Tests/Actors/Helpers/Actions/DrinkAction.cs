// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// DrinkAction.cs is part of Crystal AI.
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
using System;


namespace Crystal.ActorTests {

  public class DrinkAction : ActionBase<CharacterContext> {
    protected override void OnExecute(CharacterContext context) {
      var thirstBefore = context.Thirst;
      context.Thirst -= 0.8f;
      if(context.Thirst < 0.0f)
        context.Thirst = 0.0f;
      context.Bladder += 0.25f;
      if(context.Bladder > 1.0f)
        context.Bladder = 1.0f;
      Console.WriteLine("Drinking... Thirst before {0}, after {1}", thirstBefore, context.Thirst);
      EndInSuccess(context);
    }

    protected override void OnUpdate(CharacterContext context) {
      Console.WriteLine("+++ DrinkAction update...");
      if(ElapsedTime > 5f) {
        Console.WriteLine("+=+=+=+=+ END Drink +=+=+=+=+");
        EndInSuccess(context);
      }
    }
  }

}