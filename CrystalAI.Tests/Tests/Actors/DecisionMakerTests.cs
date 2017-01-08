// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// DecisionMakerTests.cs is part of Crystal AI.
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
using NUnit.Framework;


namespace Crystal.ActorTests {

  [TestFixture]
  public class DecisionMakerTests {
    Toon _toon;

    IUtilityAi _ai;
    IOption _eatOption;
    IOption _drinkOption;
    IOption _toiletOption;

    IActionCollection _ac;
    IConsiderationCollection _cc;
    IOptionCollection _oc;
    IBehaviourCollection _bc;
    IAiCollection _aic;

    IScheduler _scheduler;

    [OneTimeSetUp]
    public void Inititalize() {
      _toon = new Toon();

      _ac = new ActionCollection();
      _cc = new ConsiderationCollection();
      _oc = new OptionCollection(_ac, _cc);
      _bc = new BehaviourCollection(_oc);
      _aic = new AiCollection(_bc);

      // The main AI
      _ai = new UtilityAi("ai", _aic);
      var coreBehaviour = new Behaviour("coreBehaviour", _bc);
      coreBehaviour.Selector = new MaxUtilitySelector();
      _ai.AddBehaviour(coreBehaviour.NameId);

      // Eat Option
      _eatOption = new Option();
      _eatOption.Measure = new WeightedMetrics(1.4f);
      var eatAction = new EatAction();
      var hungerConsideration = new HungerConsideration();
      _eatOption.SetAction(eatAction);
      _eatOption.AddConsideration(hungerConsideration);
      _eatOption.AddConsideration(new InverseBladderConsideration());
      coreBehaviour.AddConsideration(_eatOption);

      // Drink Option
      _drinkOption = new Option();
      _drinkOption.Measure = new WeightedMetrics(3.0f);
      var drinkAction = new DrinkAction();
      var thirstConsideration = new ThirstConsideration();
      _drinkOption.SetAction(drinkAction);
      _drinkOption.AddConsideration(thirstConsideration);
      _drinkOption.AddConsideration(new InverseBladderConsideration());

      // Toilet Option
      _toiletOption = new Option();
      _toiletOption.Measure = new WeightedMetrics();
      var toiletAction = new ToiletAction();
      var bladderConsideration = new BladderConsideration();
      _toiletOption.SetAction(toiletAction);
      _toiletOption.AddConsideration(bladderConsideration);

      coreBehaviour.AddConsideration(_eatOption);
      coreBehaviour.AddConsideration(_drinkOption);
      coreBehaviour.AddConsideration(_toiletOption);

      Console.WriteLine(coreBehaviour);

      _scheduler = new Scheduler();
    }

    [Test]
    public void ConstructorTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      Assert.IsNotNull(dm);
    }

    [Test]
    public void ConstructorExceptionsTest() {
      Assert.Throws<DecisionMakerBase.UtilityAiNullException>(() => new DecisionMaker(null, _toon, _scheduler));
      Assert.Throws<DecisionMakerBase.ContextProviderNullException>(() => new DecisionMaker(_ai, null, _scheduler));
      Assert.Throws<DecisionMaker.SchedulerNullException>(() => new DecisionMaker(_ai, _toon, null));
    }

    [Test]
    public void GetSetFirstThinkDelayMinTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      Assert.That(dm.InitThinkDelayMin, Is.EqualTo(0f));
      dm.InitThinkDelayMin = 0.5f;
      Assert.That(dm.InitThinkDelayMin, Is.EqualTo(0.5));
    }

    [Test]
    public void FirstThinkDelayMinCannotBeNegativeTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      dm.InitThinkDelayMin = -100f;
      Assert.That(dm.InitThinkDelayMin, Is.EqualTo(0f));
    }

    [Test]
    public void GetSetFirstThinkDelayMaxTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      Assert.That(dm.InitThinkDelayMax, Is.EqualTo(0f));
      dm.InitThinkDelayMax = 0.5f;
      Assert.That(dm.InitThinkDelayMax, Is.EqualTo(0.5f));
    }

    [Test]
    public void FirstThinkDelayMaxCannotBeNegativeTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      dm.InitThinkDelayMax = -100f;
      Assert.That(dm.InitThinkDelayMax, Is.EqualTo(0f));
    }

    [Test]
    public void FirstThinkDelayMinIsLessOrEqualToMinTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      dm.InitThinkDelayMax = 0.5f;
      dm.InitThinkDelayMin = 0.6f;
      Assert.That(dm.InitThinkDelayMax, Is.EqualTo(0.6f));

      dm.InitThinkDelayMax = 0.5f;
      Assert.That(dm.InitThinkDelayMax, Is.EqualTo(0.6f));

      dm.InitThinkDelayMin = 0f;
      dm.InitThinkDelayMax = 0.1f;
      Assert.That(dm.InitThinkDelayMax, Is.EqualTo(0.1f));
    }


    [Test]
    public void GetSetThinkDelayMinTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      Assert.That(dm.ThinkDelayMin, Is.EqualTo(0f));
      dm.ThinkDelayMin = 0.5f;
      Assert.That(dm.ThinkDelayMin, Is.EqualTo(0.5));
    }

    [Test]
    public void ThinkDelayMinCannotBeNegativeTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      dm.ThinkDelayMin = -100f;
      Assert.That(dm.ThinkDelayMin, Is.EqualTo(0f));
    }

    [Test]
    public void GetSetThinkDelayMaxTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      Assert.That(dm.ThinkDelayMax, Is.EqualTo(0f));
      dm.ThinkDelayMax = 0.5f;
      Assert.That(dm.ThinkDelayMax, Is.EqualTo(0.5f));
    }

    [Test]
    public void ThinkDelayMaxCannotBeNegativeTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      dm.ThinkDelayMax = -100f;
      Assert.That(dm.ThinkDelayMax, Is.EqualTo(0f));
    }

    [Test]
    public void ThinkDelayMinIsLessOrEqualToMinTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      dm.ThinkDelayMax = 0.5f;
      dm.ThinkDelayMin = 0.6f;
      Assert.That(dm.ThinkDelayMax, Is.EqualTo(0.6f));

      dm.ThinkDelayMax = 0.5f;
      Assert.That(dm.ThinkDelayMax, Is.EqualTo(0.6f));

      dm.ThinkDelayMin = 0f;
      dm.ThinkDelayMax = 0.1f;
      Assert.That(dm.ThinkDelayMax, Is.EqualTo(0.1f));
    }


    [Test]
    public void GetSetFirstUpdateDelayMinTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      Assert.That(dm.InitUpdateDelayMin, Is.EqualTo(0f));
      dm.InitUpdateDelayMin = 0.5f;
      Assert.That(dm.InitUpdateDelayMin, Is.EqualTo(0.5));
    }

    [Test]
    public void FirstUpdateMinCannotBeNegativeTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      dm.InitUpdateDelayMin = -100f;
      Assert.That(dm.InitUpdateDelayMin, Is.EqualTo(0f));
    }

    [Test]
    public void GetSetFirstUpdateDelayMaxTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      Assert.That(dm.InitUpdateDelayMax, Is.EqualTo(0f));
      dm.InitUpdateDelayMax = 0.5f;
      Assert.That(dm.InitUpdateDelayMax, Is.EqualTo(0.5f));
    }

    [Test]
    public void FirstUpdateMaxCannotBeNegativeTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      dm.InitUpdateDelayMax = -100f;
      Assert.That(dm.InitUpdateDelayMax, Is.EqualTo(0f));
    }

    [Test]
    public void FirstUpdateMinIsLessOrEqualToMinTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      dm.InitUpdateDelayMax = 0.5f;
      dm.InitUpdateDelayMin = 0.6f;
      Assert.That(dm.InitUpdateDelayMax, Is.EqualTo(0.6f));

      dm.InitUpdateDelayMax = 0.5f;
      Assert.That(dm.InitUpdateDelayMax, Is.EqualTo(0.6f));

      dm.InitUpdateDelayMin = 0f;
      dm.InitUpdateDelayMax = 0.1f;
      Assert.That(dm.InitUpdateDelayMax, Is.EqualTo(0.1f));
    }


    [Test]
    public void GetSetUpdateDelayMinTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      Assert.That(dm.UpdateDelayMin, Is.EqualTo(0f));
      dm.UpdateDelayMin = 0.5f;
      Assert.That(dm.UpdateDelayMin, Is.EqualTo(0.5));
    }

    [Test]
    public void UpdateDelayMinCannotBeNegativeTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      dm.UpdateDelayMin = -100f;
      Assert.That(dm.UpdateDelayMin, Is.EqualTo(0f));
    }

    [Test]
    public void GetSetUpdateDelayMaxTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      Assert.That(dm.UpdateDelayMax, Is.EqualTo(0f));
      dm.UpdateDelayMax = 0.5f;
      Assert.That(dm.UpdateDelayMax, Is.EqualTo(0.5f));
    }

    [Test]
    public void UpdateDelayMaxCannotBeNegativeTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      dm.UpdateDelayMax = -100f;
      Assert.That(dm.UpdateDelayMax, Is.EqualTo(0f));
    }

    [Test]
    public void UpdateDelayMinIsLessOrEqualToMinTest() {
      var dm = new DecisionMaker(_ai, _toon, _scheduler);
      dm.UpdateDelayMax = 0.5f;
      dm.UpdateDelayMin = 0.6f;
      Assert.That(dm.UpdateDelayMax, Is.EqualTo(0.6f));

      dm.UpdateDelayMax = 0.5f;
      Assert.That(dm.UpdateDelayMax, Is.EqualTo(0.6f));

      dm.UpdateDelayMin = 0f;
      dm.UpdateDelayMax = 0.1f;
      Assert.That(dm.UpdateDelayMax, Is.EqualTo(0.1f));
    }


    [Test]
    public void StartStopTest() {
      var s = new Scheduler();
      var dm = new DecisionMaker(_ai, _toon, s);
      var thinkStream = s.ThinkStream as CommandStream;
      var updateStream = s.UpdateStream as CommandStream;
      Assert.IsNotNull(thinkStream);
      Assert.IsNotNull(updateStream);

      dm.Start();
      Assert.AreEqual(1, thinkStream.CommandsCount);
      Assert.AreEqual(1, updateStream.CommandsCount);
      Assert.AreEqual(dm.State, DecisionMakerState.Running);

      dm.Stop();
      Assert.AreEqual(0, thinkStream.CommandsCount);
      Assert.AreEqual(0, thinkStream.CommandsCount);
      Assert.AreEqual(dm.State, DecisionMakerState.Stopped);
    }

    [Test]
    public void PauseResumeTest() {
      var s = new Scheduler();
      var dm = new DecisionMaker(_ai, _toon, s);
      var thinkStream = s.ThinkStream as CommandStream;
      var updateStream = s.UpdateStream as CommandStream;
      Assert.IsNotNull(thinkStream);
      Assert.IsNotNull(updateStream);

      dm.Start();
      Assert.AreEqual(1, thinkStream.CommandsCount);
      Assert.AreEqual(1, thinkStream.CommandsCount);
      Assert.AreEqual(dm.State, DecisionMakerState.Running);

      dm.Pause();
      Assert.AreEqual(1, thinkStream.CommandsCount);
      Assert.AreEqual(1, updateStream.CommandsCount);
      Assert.AreEqual(dm.State, DecisionMakerState.Paused);

      dm.Resume();
      Assert.AreEqual(1, thinkStream.CommandsCount);
      Assert.AreEqual(1, thinkStream.CommandsCount);
      Assert.AreEqual(dm.State, DecisionMakerState.Running);

      dm.Stop();
      Assert.AreEqual(0, thinkStream.CommandsCount);
      Assert.AreEqual(0, thinkStream.CommandsCount);
      Assert.AreEqual(dm.State, DecisionMakerState.Stopped);

      dm.Resume();
      Assert.AreEqual(0, thinkStream.CommandsCount);
      Assert.AreEqual(0, thinkStream.CommandsCount);
      Assert.AreEqual(dm.State, DecisionMakerState.Stopped);
    }

    [Test]
    public void ThinkRecursionDetectionTest() {
      _aic.ClearAll();
      var s = new Scheduler();
      // The main AI
      var ta = new AiTransition("TransitionToCircularAi", "circularAi", _aic);
      var circularAi = new UtilityAi("circularAi", _aic);
      var coreBehaviour = new Behaviour("cb", _bc);
      coreBehaviour.Selector = new MaxUtilitySelector();
      var circularOption = new ConstantUtilityOption("recursive", _oc);
      circularOption.DefaultUtility = new Utility(1f, 1f);
      circularOption.SetAction("TransitionToCircularAi");

      Assert.That(coreBehaviour.AddOption("recursive"), Is.True);
      Assert.That(circularAi.AddBehaviour(coreBehaviour.NameId), Is.True);

      var dm = new DecisionMaker(circularAi, _toon, s);
      Assert.Throws<DecisionMakerBase.PotentialCircularDependencyException>(dm.Think);
      try {
        dm.Think();
      } catch(Exception ex) {
        Console.WriteLine(ex.Message);
      }
    }
  }

}