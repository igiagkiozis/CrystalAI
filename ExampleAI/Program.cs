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
using System.Diagnostics;
using System.Text;
using System.Threading;
using Crystal;


namespace ExampleAI {



  public class TestHeapItemComparer : IComparer<TestHeapItem> {
    /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
    /// <returns>Value Condition Less than zero<paramref name="x" /> is less than <paramref name="y" />.Zero<paramref name="x" /> equals <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.</returns>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    public int Compare(TestHeapItem x, TestHeapItem y) {
      return x.Priority.CompareTo(y.Priority);
    }
  }

  public class TestHeapItem : IHeapItem<TestHeapItem> {
    public float Value;
    public float Priority;

    IPriorityQueueHandle<TestHeapItem> IHeapItem<TestHeapItem>.Handle { get; set; }

    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString() {
      return string.Format("Value : {0}, Priority : {1}", Value, Priority);
    }

    public TestHeapItem() {
      Value = 0f;
      Priority = 0f;
    }

    public TestHeapItem(float value, float priority) {
      Value = value;
      Priority = priority;
    }
  }



  internal class Program {

//    TestHeapItemComparer _testHeapComparer;
//    BinaryHeap<TestHeapItem> _pq;
    

    static void Main() {
      Console.ReadKey();
      //prevent the JIT Compiler from optimizing Fkt calls away
      long seed = Environment.TickCount;

      //use the second Core/Processor for the test
      Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2);

      //prevent "Normal" Processes from interrupting Threads
      Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

      //prevent "Normal" Threads from interrupting this thread
      Thread.CurrentThread.Priority = ThreadPriority.Highest;


      const int N = 500000;
      TestHeapItemComparer _testHeapComparer = new TestHeapItemComparer();
      BinaryHeap<TestHeapItem> _pq = new BinaryHeap<TestHeapItem>(_testHeapComparer);



      Stopwatch sw = new Stopwatch();

      _pq.Clear();
      var itemList = new List<TestHeapItem>();
      var rnd = new Random();

      for(int i = 0; i < N; i++) {
        itemList.Add(new TestHeapItem(rnd.Next(), (float)rnd.NextDouble()));
      }

      sw.Reset();
      sw.Start();
      for(int i = 0; i < N; i++) {
        _pq.Enqueue(itemList[i]);
      }
      sw.Stop();
      Console.WriteLine("New Enqueue : {0}", sw.Elapsed.TotalMilliseconds);

      float priority = -1f;
      sw.Reset();
      sw.Start();
      for(int i = N - 1; i >= 0; i--) {
        var item = itemList[i];
        item.Priority = priority;
        _pq.UpdatePriority(item);        
        priority--;
      }
      sw.Stop();
      Console.WriteLine("New UpdatePriority : {0}", sw.Elapsed.TotalMilliseconds);

      sw.Reset();
      sw.Start();
      for(int i = 0; i < N; i++) {
        _pq.Remove(itemList[i]);
      }
      sw.Stop();
      Console.WriteLine("New Remove : {0}", sw.Elapsed.TotalMilliseconds);


      for(int i = 0; i < N; i++) {
        itemList[i].Priority = i;
      }
      var pqo = new PriorityQueueDEPRECATED<TestHeapItem, float>();

      sw.Reset();
      sw.Start();
      for(int i = 0; i < N; i++) {
        pqo.Enqueue(itemList[i], itemList[i].Priority);
      }
      sw.Stop();
      Console.WriteLine("Old Enqueue : {0}", sw.Elapsed.TotalMilliseconds);

      //      priority = -1f;
      //      sw.Reset();
      //      sw.Start();
      //      for(int i = N - 1; i >= 0; i--) {
      //        var item = itemList[i];
      //        item.Priority = priority;
      //        pqo.UpdatePriority(item, priority);
      //        Assert.AreEqual(item, pqo.Peek());
      //        priority--;
      //      }
      //      sw.Stop();
      //      Console.WriteLine("Old UpdatePriority : {0}", sw.Elapsed.TotalMilliseconds);

      Console.WriteLine(pqo.Count);

      sw.Reset();
      sw.Start();
      for(int i = 0; i < N; i++) {
        pqo.Remove(itemList[i]);
      }
      sw.Stop();
      Console.WriteLine("Old Remove : {0}", sw.Elapsed.TotalMilliseconds);

      Console.ReadKey();
      return;


















//      int N = 1;
//      var characters = new List<Character>();
//      var decisionMakers = new List<DecisionMaker>();
//
//      Console.WriteLine("Hello from ExampleAI");
//      var aiCollection = AiCollectionConstructor.Create();
//      ExAiConstructor aiConstructor = new ExAiConstructor(aiCollection);
//      Scheduler scheduler = new Scheduler();
//
//      // Create characters and their corresponding decision making logic
//      for(int i = 0; i < N; i++) {
//        var character = new Character(string.Format("Toon {0}", i));
//        var decisionMaker = new DecisionMaker(aiConstructor.Create(AiDefs.ToonAi), character, scheduler) {
//          InitThinkDelayMin = 0.1f,
//          InitThinkDelayMax = 0.5f,
//          ThinkDelayMin = 0.25f,
//          ThinkDelayMax = 0.3f,
//          InitUpdateDelayMin = 0.1f,
//          InitUpdateDelayMax = 0.15f,
//          UpdateDelayMin = 0.1f,
//          UpdateDelayMax = 0.12f
//        };
//        
//        characters.Add(character);
//        decisionMakers.Add(decisionMaker);
//        decisionMaker.Start();
//      }
//
//      // Simulation loop
//      Console.WriteLine("Entering Simulation Loop");
//      while(true) {
//        StringBuilder sb = new StringBuilder();
//        foreach(var character in characters) {
//          character.Update();
//          sb.AppendLine(character.ToString());
//        }
//
//        Console.SetCursorPosition(0, 0);
//        Console.Write(sb.ToString());
//
//        scheduler.Tick();
//        Thread.Sleep(250);
//      }

    }
  }

}