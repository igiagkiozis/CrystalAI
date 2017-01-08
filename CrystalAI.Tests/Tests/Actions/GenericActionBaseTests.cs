// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// GenericActionBaseTests.cs is part of Crystal AI.
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
using Crystal.GeneralTests;
using CrystalAI.TestHelpers;
using NUnit.Framework;


namespace Crystal.ActionTests {

  [TestFixture]
  internal class GenericActionBaseTests {
    MockContext _context;
    HelperAiConstructor _aiConstructor;

    [OneTimeSetUp]
    public void Initialize() {
      _context = new MockContext();
      _aiConstructor = new HelperAiConstructor();
    }

    [Test]
    public void DefaultConstructorTest() {
      var a = new ActionBase<MockContext>();
      Assert.IsNotNull(a);
      Assert.That(a.ActionStatus == ActionStatus.Idle);
    }

    [Test]
    public void NameConstructorTest() {
      _aiConstructor.Collection.ClearAll();
      var a = new ActionBase<MockContext>("name", _aiConstructor.Actions);
      Assert.IsNotNull(a);
      Assert.That(_aiConstructor.Actions.Contains("name"));
    }

    [Test]
    public void NameOrCollectionNullThrowsTest() {
      _aiConstructor.Collection.ClearAll();

      Assert.Throws<ActionBase<MockContext>.NameIdEmptyOrNullException>
        (() => new ActionBase<MockContext>(null, _aiConstructor.Actions));

      Assert.Throws<ActionBase<MockContext>.ActionCollectionNullException>
        (() => new ActionBase<MockContext>("name", null));

      Assert.Throws<ActionBase<MockContext>.NameIdEmptyOrNullException>
        (() => new ActionBase<MockContext>(null, null));
    }

    [Test]
    public void CloneTest() {
      _aiConstructor.Collection.ClearAll();
      var a = new ActionBase<MockContext>("name", _aiConstructor.Actions);
      a.Execute(_context);
      var ac = a.Clone();
      Assert.That(ac.NameId, Is.EqualTo(a.NameId));
      Assert.That(a.ActionStatus == ActionStatus.Success);
      Assert.That(ac.ActionStatus == ActionStatus.Idle);
    }

    [Test]
    public void ExecuteTest() {
      var a = new ActionBase<MockContext>();
      Assert.That(a.ActionStatus == ActionStatus.Idle);
      Assert.DoesNotThrow(() => a.Execute(_context));
      Assert.That(a.ActionStatus == ActionStatus.Success);
    }

    [Test]
    public void ExecuteAsIActionTest() {
      var a = new ActionBase<MockContext>() as IAction;
      Assert.That(a.ActionStatus == ActionStatus.Idle);
      Assert.DoesNotThrow(() => a.Execute(_context));
      Assert.That(a.ActionStatus == ActionStatus.Success);
    }

    [Test]
    public void CooldownTest() {
      var a = new ActionBase<MockContext>();
      a.Cooldown = 20f;
      Assert.That(a.InCooldown, Is.False);
      a.Execute(_context);
      Assert.That(a.ActionStatus == ActionStatus.Success);
      Assert.That(a.InCooldown, Is.True);
      a.Execute(_context);
      Assert.That(a.ActionStatus == ActionStatus.Failure);
    }
  }

}