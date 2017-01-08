// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ActionSequenceTests.cs is part of Crystal AI.
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
using System.Threading;
using CrystalAI.TestHelpers;
using NUnit.Framework;


#pragma warning disable


namespace Crystal.ActionTests {

  [TestFixture]
  public class ActionSequenceTests {
    CustomContext _customContext;
    AiConstructor _aiConstructor;

    static object[] _constructorTestCases = {
      new object[] {new ActionSequence()},
      new object[] {new ActionSequence("name", new ActionCollection())}
    };

    static object[] _cloneTestCases = {
      new object[] {new ActionSequence(), 5},
      new object[] {new ActionSequence(), 10},
      new object[] {new ActionSequence("name", new ActionCollection()), 5},
      new object[] {new ActionSequence("name", new ActionCollection()), 10}
    };

    [OneTimeSetUp]
    public void Initialize() {
      _aiConstructor = new HelperAiConstructor();
      _customContext = new CustomContext();
      for(int i = 0; i < 10; i++)
        _customContext.IntList.Add(i);
    }

    [Test, TestCaseSource("_constructorTestCases")]
    public void ConstructionTest(IAction action) {
      Assert.IsNotNull(action);
    }

    [Test, TestCaseSource("_cloneTestCases")]
    public void CloneTests(ActionSequence action, int numActions) {
      for(int i = 0; i < numActions; i++) {
        var a = new MockAction();
        a.NameId = string.Format("Name{0}", i);
        action.Actions.Add(a);
      }

      var newAction = action.Clone() as ActionSequence;
      Assert.AreEqual(action.Actions.Count, newAction.Actions.Count);
      Assert.AreEqual(action.NameId, newAction.NameId);
      for(int i = 0; i < numActions; i++) {
        var name = string.Format("Name{0}", i);
        Assert.AreEqual(action.Actions[i].NameId, newAction.Actions[i].NameId);
      }
    }

    [Test, TestCase(0.05f), TestCase(0.09f), TestCase(0.1f)]
    public void CooldownTest(float cooldown) {
      var action = new ActionSequence();
      for(int i = 0; i < 10; i++)
        action.Actions.Add(new MockAction());

      int milliSeconds = (int)(1000 * cooldown) + 1;
      action.Cooldown = cooldown;
      action.Execute(_customContext);
      Assert.AreEqual(ActionStatus.Success, action.ActionStatus);
      Assert.AreEqual(true, action.InCooldown);
      action.Execute(_customContext);
      Assert.AreEqual(ActionStatus.Failure, action.ActionStatus);

      Thread.Sleep(milliSeconds);

      Assert.AreEqual(false, action.InCooldown);
      action.Execute(_customContext);
      Assert.AreEqual(ActionStatus.Success, action.ActionStatus);
      Assert.AreEqual(true, action.InCooldown);
    }

    [Test]
    public void EnsureActionsExecutedTest() {
      var action = new ActionSequence();
      for(int i = 0; i < 10; i++)
        action.Actions.Add(new MockAction());
      for(int i = 0; i < 10; i++)
        Assert.AreEqual(ActionStatus.Idle, action.Actions[i].ActionStatus);

      action.Execute(_customContext);
      Assert.AreEqual(ActionStatus.Success, action.ActionStatus);
      for(int i = 0; i < 10; i++)
        Assert.AreEqual(ActionStatus.Success, action.Actions[i].ActionStatus);

      Assert.AreEqual(ActionStatus.Success, action.ActionStatus);
    }

    [Test]
    public void EnsureUpdatableActionsExecutedTest() {
      var action = new ActionSequence();
      for(int i = 0; i < 10; i++)
        action.Actions.Add(new UpdatingAction(UpdateIterations));
      for(int i = 0; i < 10; i++)
        Assert.AreEqual(ActionStatus.Idle, action.Actions[i].ActionStatus);

      int count = 0;
      do {
        count++;
        action.Execute(_customContext);
      } while(action.ActionStatus == ActionStatus.Running);

      Assert.AreEqual(UpdateIterations, count);
      Assert.AreEqual(ActionStatus.Success, action.ActionStatus);
      for(int i = 0; i < 10; i++)
        Assert.AreEqual(ActionStatus.Success, action.Actions[i].ActionStatus);
    }

    [Test]
    public void FailureConditionTest() {
      var actionSequence = new ActionSequence();
      for(int i = 0; i < 10; i++)
        actionSequence.Actions.Add(new UpdatingAction(UpdateIterations));
      // TODO Here we're adding an action directly. This shouldn't be happening.
      var newAction = _aiConstructor.Actions.Create(TestActionDefs.FailingAction);
      actionSequence.Actions.Add(newAction);
      for(int i = 0; i < 11; i++)
        Assert.AreEqual(ActionStatus.Idle, actionSequence.Actions[i].ActionStatus);

      int count = 0;
      do {
        count++;
        actionSequence.Execute(_customContext);
      } while(actionSequence.ActionStatus == ActionStatus.Running);

      Assert.AreEqual(UpdateIterations, count);
      Assert.AreEqual(ActionStatus.Failure, actionSequence.ActionStatus);
      Assert.AreEqual(ActionStatus.Failure, actionSequence.Actions[10].ActionStatus);
      for(int i = 0; i < 10; i++)
        Assert.AreEqual(ActionStatus.Success, actionSequence.Actions[i].ActionStatus);
    }

    const int UpdateIterations = 100;
  }

}