// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ActionTests.cs is part of Crystal AI.
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
  public class ActionTests {
    CustomContext _customContext;
    static AiConstructor _aiConstructor = new HelperAiConstructor();

    static object[] _actionConstructorTestCases = {
      new object[] {new MockAction()},
      new object[] {new MockGenericAction()},
      new object[] {new MockAction("name", new ActionCollection())},
      new object[] {new MockGenericAction("name", new ActionCollection())}
    };

    static object[] _cloneTestCases = {
      new object[] {new MockAction()},
      new object[] {new MockGenericAction()},
      new object[] {new MockAction("name", new ActionCollection())},
      new object[] {new MockGenericAction("name", new ActionCollection())}
    };

    static object[] _actionCooldownTestCases = {
      new object[] {new MockAction(), 0.05f},
      new object[] {new MockAction(), 0.075f},
      new object[] {new MockAction(), 0.1f},
      new object[] {new MockGenericAction(), 0.05f},
      new object[] {new MockGenericAction(), 0.075f},
      new object[] {new MockGenericAction(), 0.1f},
      new object[] {new MockAction("name", new ActionCollection()), 0.05f},
      new object[] {new MockAction("name", new ActionCollection()), 0.075f},
      new object[] {new MockAction("name", new ActionCollection()), 0.1f},
      new object[] {new MockGenericAction("name", new ActionCollection()), 0.05f},
      new object[] {new MockGenericAction("name", new ActionCollection()), 0.075f},
      new object[] {new MockGenericAction("name", new ActionCollection()), 0.1f}
    };

    static object[] _noCooldownActionTestCases = {
      new object[] {new MockAction()},
      new object[] {new MockGenericAction()}
    };

    static object[] _updatingActionTestCases = {
      new object[] {new UpdatingAction(UpdateIterations)},
      new object[] {new UpdatingGenericAction(UpdateIterations)}
    };

    static object[] _failingActionTestCases = {
      new object[] {new FailingAction()},
      new object[] {new FailingGenericAction()}
    };

    [OneTimeSetUp]
    public void Initialize() {
      _customContext = new CustomContext();
      for(int i = 0; i < 10; i++)
        _customContext.IntList.Add(i);
    }

    [Test, TestCaseSource("_actionConstructorTestCases")]
    public void ConstructorTests(IAction action) {
      Assert.IsNotNull(action);
    }

    [Test]
    public void ConstructorThrowsNameIdEmptyOrNullTest() {
      Assert.Throws<ActionBase.NameIdEmptyOrNullException>(() => new MockAction("", null));
      Assert.Throws<ActionBase.NameIdEmptyOrNullException>(() => new MockAction(null, null));
      Assert.Throws<ActionBase<CustomContext>.NameIdEmptyOrNullException>(() => new MockGenericAction("", null));
      Assert.Throws<ActionBase<CustomContext>.NameIdEmptyOrNullException>(() => new MockGenericAction(null, null));
    }

    [Test]
    public void ConstructorThrowsNullCollectionExceptionTest() {
      Assert.Throws<ActionBase.ActionCollectionNullException>(() => new MockAction("name", null));
      Assert.Throws<ActionBase<CustomContext>.ActionCollectionNullException>(() => new MockGenericAction("name", null));
    }

    [Test]
    public void ConstructorThrowsAlreadyExistsInCollectionTest() {
      var collection = new ActionCollection();
      var a = new MockAction("name", collection);
      var b = new MockGenericAction("nameG", collection);
      Assert.Throws<ActionBase.ActionAlreadyExistsInCollectionException>(() => new MockAction("name", collection));
      Assert.Throws<ActionBase<CustomContext>.ActionAlreadyExistsInCollectionException>(() => new MockGenericAction(
                                                                                                                    "nameG",
                                                                                                                    collection));
    }

    [Test, TestCaseSource("_cloneTestCases")]
    public void CloneTests(IAction action) {
      var newAction = action.Clone();
      Assert.AreEqual(action.NameId, newAction.NameId);
      Assert.AreEqual(action.ActionStatus, newAction.ActionStatus);
      Assert.AreEqual(action.InCooldown, newAction.InCooldown);
      Assert.AreNotEqual(action, newAction);
    }

    [Test]
    public void CreatingSameActionThrowsTest() {
      var collection = new ActionCollection();
      var action = new MockAction("name", collection);
      Assert.Throws<ActionBase.ActionAlreadyExistsInCollectionException>(() => new MockAction("name", collection));
      Assert.Throws<ActionBase<CustomContext>.ActionAlreadyExistsInCollectionException>(() => new MockGenericAction(
                                                                                                                    "name",
                                                                                                                    collection));
    }

    [Test, TestCaseSource("_actionCooldownTestCases")]
    public void DoesActionRespectCooldownTest(IAction action, float cooldown) {
      int milliSeconds = (int)(1000 * cooldown) + 1;
      action.Cooldown = cooldown;
      action.Execute(_customContext);
      Assert.AreEqual(ActionStatus.Success, action.ActionStatus);

      action.Execute(_customContext);
      Assert.AreEqual(ActionStatus.Failure, action.ActionStatus);
      Assert.AreEqual(true, action.InCooldown);

      Thread.Sleep(milliSeconds);
      Assert.AreEqual(false, action.InCooldown);
      action.Execute(_customContext);
      Assert.AreEqual(ActionStatus.Success, action.ActionStatus);
      Assert.AreEqual(true, action.InCooldown);
    }

    [Test, TestCaseSource("_noCooldownActionTestCases")]
    public void NoCooldownNonGenericActionTest(IAction action) {
      action.Cooldown = 0.0f;
      for(int i = 0; i < 10; i++) {
        action.Execute(_customContext);
        Assert.AreEqual(ActionStatus.Success, action.ActionStatus);
        Assert.AreEqual(false, action.InCooldown);
      }
    }

    [Test, TestCaseSource("_updatingActionTestCases")]
    public void UpdatingActionTests(IAction action) {
      int counter = 0;
      do {
        counter++;
        action.Execute(_customContext);
      } while(action.ActionStatus == ActionStatus.Running);

      Assert.AreEqual(UpdateIterations, counter);
      Assert.AreEqual(ActionStatus.Success, action.ActionStatus);
    }

    [Test, TestCaseSource("_failingActionTestCases")]
    public void FailingActionTests(IAction action) {
      action.Execute(_customContext);
      Assert.AreEqual(ActionStatus.Failure, action.ActionStatus);
    }

    [Test]
    public void LinkUtilityAiActionConstructionTest() {
      var ai = new UtilityAi();
      Assert.IsNotNull(ai);
      var action = new AiTransition(ai);
      Assert.IsNotNull(action);
      Assert.IsNotNull(action.TargetAi);
    }

    [Test]
    public void BehaviourTransitionConstructionTest() {
      var behaviour = new Behaviour();
      behaviour.Selector = new MaxUtilitySelector();

      Assert.IsNotNull(behaviour);
      var action = new BehaviourTransition(behaviour);
      Assert.IsNotNull(action);
    }

    const int UpdateIterations = 100;
  }

}