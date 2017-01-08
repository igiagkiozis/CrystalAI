// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// CommandStreamTests.cs is part of Crystal AI.
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
#define DISPLAY_LOG
using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;


namespace Crystal.SchedulingTests {

  [TestFixture]
  public class CommandStreamTests {
    CommandAction _proc1;
    CommandAction _proc2;
    CommandAction _proc3;
    DeferredCommand _cmd1;
    DeferredCommand _cmd2;
    DeferredCommand _cmd3;
    Processes _p;

    [OneTimeSetUp]
    public void Initialize() {
      _p = new Processes();
      _proc1 = _p.Process1;
      _proc2 = _p.Process2;
      _proc3 = _p.Process3;
      _cmd1 = new DeferredCommand(_proc1) {
        InitExecutionDelayMin = 0.0f,
        InitExecutionDelayMax = 0.0f,
        ExecutionDelayMin = 0.0f,
        ExecutionDelayMax = 0.0f
      };
      _cmd2 = new DeferredCommand(_proc2) {
        InitExecutionDelayMin = 0.0f,
        InitExecutionDelayMax = 0.0f,
        ExecutionDelayMin = 0.0f,
        ExecutionDelayMax = 0.0f
      };
      _cmd3 = new DeferredCommand(_proc3) {
        InitExecutionDelayMin = 0.0f,
        InitExecutionDelayMax = 0.0f,
        ExecutionDelayMin = 0.0f,
        ExecutionDelayMax = 0.0f
      };
    }


    [Test]
    public void ConstructorTest() {
      var s = new CommandStream(128);
      Assert.IsNotNull(s);
    }

    [Test]
    public void AddingCommandsTest() {
      var s = new CommandStream(128) {
        MaxProcessingTime = 10
      };
      s.Add(_cmd1);
      s.Add(_cmd2);
      s.Add(_cmd3);

      Assert.AreEqual(3, s.CommandsCount);
    }

    [Test]
    public void NonRepeatingCommandsTest() {
      var numOfCommands = 100;
      var s = new CommandStream(128) {
        MaxProcessingTime = 3
      };

      for(int i = 0; i < numOfCommands; i++) {
        var cm1 = new DeferredCommand(_p.PopulateRandomList100) {
          IsRepeating = false,
          InitExecutionDelayMin = 0.0f,
          InitExecutionDelayMax = 0.0f,
          ExecutionDelayMin = 0.0f,
          ExecutionDelayMax = 0.0f
        };
        s.Add(cm1);
      }

      int totalExecutions = 0;
      for(int i = 0; i < 5; i++) {
        s.Process();
#if DISPLAY_LOG
        StreamLog(s);
#endif
        totalExecutions += s.ProcessedCount;
        Thread.Sleep(1);
      }

      Assert.AreEqual(numOfCommands, totalExecutions);
    }

    [Test]
    public void ActivateAndDeactivateCommandTest() {
      var s = new CommandStream(128) {
        MaxProcessingTime = 3
      };

      var cm1 = new DeferredCommand(_p.PopulateRandomList100) {
        IsRepeating = false,
        InitExecutionDelayMin = 0.0f,
        InitExecutionDelayMax = 0.0f,
        ExecutionDelayMin = 0.0f,
        ExecutionDelayMax = 0.0f
      };
      var cmh = s.Add(cm1);

      Assert.AreEqual(1, s.CommandsCount);
      Assert.AreEqual(0, s.ProcessedCount);
      s.Process();
      Assert.AreEqual(1, s.ProcessedCount);

      cmh.IsActive = false;
      Assert.AreEqual(0, s.CommandsCount);

      cmh.IsActive = true;
      Assert.AreEqual(1, s.CommandsCount);
      s.Process();
      Assert.AreEqual(1, s.ProcessedCount);
    }

    // TODO Need to reconsider the behaviour of this.
    [Test]
    public void ProcessingTimeTest(
      [Range(2.0, 10.0, 1.0)] double processingTime,
      [Values(10, 50, 100)] int numOfCommands) {
      var s = new CommandStream(128) {
        MaxProcessingTime = processingTime
      };

      var cmdList = new List<DeferredCommand>();
      for(int i = 0; i < numOfCommands; i++) {
        var cm1 = new DeferredCommand(_p.PopulateRandomList100) {
          InitExecutionDelayMin = 0.0f,
          InitExecutionDelayMax = 0.0f,
          ExecutionDelayMin = 0.00001f,
          ExecutionDelayMax = 0.00001f
//          ExecutionDelayMin = 0.0f,
//          ExecutionDelayMax = 0.0f
        };
        s.Add(cm1);
        cmdList.Add(cm1);
      }

      // This is to give Time for the structures within to take their appropriate size
      // which may take some Time in the first round.
      s.Process();
      for(int i = 0; i < 5; i++) {
        s.Process();
#if DISPLAY_LOG
        StreamLog(s);
#endif
//        if(i > 1)
//          Assert.That(s.TotalMilliseconds, Is.LessThanOrEqualTo(processingTime+0.01f));
//        Thread.Sleep(2);
      }

#if DISPLAY_LOG
      for(int i = 0; i < numOfCommands; i++)
        Console.WriteLine("Cmd {0} Executed {1} times.", i, cmdList[i].TimesExecuted);
#endif
    }

    void StreamLog(CommandStream s) {
      Console.WriteLine("-------------------------------------");
      Console.WriteLine("Total Time {0}", s.TotalMilliseconds);
      Console.WriteLine("Processed items {0}", s.ProcessedCount);
      Console.WriteLine("Number of items in queue {0}", s.CommandsCount);
      Console.WriteLine("-------------------------------------");
    }
  }

}