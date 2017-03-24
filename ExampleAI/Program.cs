// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// Program.cs is part of Crystal AI.
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
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Crystal;


namespace ExampleAI {

  internal class Program {
    static void Main() {
      int N = 1;
      var characters = new List<Character>();
      var decisionMakers = new List<DecisionMaker>();

      Console.WriteLine("Hello from ExampleAI");
      var aiCollection = AiCollectionConstructor.Create();
      ExAiConstructor aiConstructor = new ExAiConstructor(aiCollection);
      Scheduler scheduler = new Scheduler();

      // Create characters and their corresponding decision making logic
      for(int i = 0; i < N; i++) {
        var character = new Character(string.Format("Toon {0}", i.ToString()));
        var decisionMaker = new DecisionMaker(aiConstructor.Create(AiDefs.ToonAi), character, scheduler) {
          InitThinkDelay = Interval.Create(0.1f, 0.5f),
          ThinkDelay = Interval.Create(0.25f, 0.3f),
          InitUpdateDelay = Interval.Create(0.1f, 0.15f),
          UpdateDelay = Interval.Create(0.1f, 0.12f)
        };

        characters.Add(character);
        decisionMakers.Add(decisionMaker);
        decisionMaker.Start();
      }

      // Simulation loop
      Console.WriteLine("Entering Simulation Loop");
      while(true) {
        StringBuilder sb = new StringBuilder();
        foreach(var character in characters) {
          character.Update();
          sb.AppendLine(character.ToString());
        }

        Console.SetCursorPosition(0, 0);
        Console.Write(sb.ToString());

        scheduler.Tick();
        Thread.Sleep(250);
      }
    }
  }

}