// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// UtilityAiTests.cs is part of Crystal AI.
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
using CrystalAI.TestHelpers;
using NUnit.Framework;


namespace Crystal.ActorTests {

  public class UaiContext : IContext {
    float _val1;
    float _val2;

    public float Val1 {
      get { return _val1; }
      set { _val1 = value.Clamp01(); }
    }

    public float Val2 {
      get { return _val2; }
      set { _val2 = value.Clamp01(); }
    }
  }

  [TestFixture]
  public class UtilityAiTests {
    IActionCollection _ac;
    IConsiderationCollection _cc;
    IOptionCollection _oc;
    IBehaviourCollection _bc;
    IAiCollection _aic;

    [OneTimeSetUp]
    public void Initialize() {
      _ac = new ActionCollection();
      _cc = new ConsiderationCollection();
      _oc = new OptionCollection(_ac, _cc);
      _bc = new BehaviourCollection(_oc);
      _aic = new AiCollection(_bc);

      var b = new Behaviour("b1", _bc);
    }

    [Test]
    public void ConstructorTest() {
      var ai = new UtilityAi();
      Assert.IsNotNull(ai);
    }

    [Test]
    public void SetNullSelectorDoesNotChangeSelectorTest() {
      var ai = new UtilityAi();
      Assert.IsNotNull(ai.Selector);
      ai.Selector = null;
      Assert.IsNotNull(ai.Selector);
    }

    [Test]
    public void AddNullBehaviourReturnsFalseTest() {
      var ai = new UtilityAi();
      Assert.That(ai.Add((Behaviour)null), Is.False);
    }

    [Test]
    public void AddStringBehaviourReturnsFalseWithNullCollectionTest() {
      var ai = new UtilityAi();
      Assert.That(ai.AddBehaviour("SomeBehaviour"), Is.False);
    }

    [Test]
    public void AddExistingStringBehaviourReturnsFalseTest() {
      _aic.Clear();
      var ai = new UtilityAi("testai", _aic);
      Assert.That(ai.AddBehaviour("b1"), Is.True);
      Assert.That(ai.AddBehaviour("b1"), Is.False);
    }

    [Test]
    public void AddNonExistentBehaviourReturnsFalseTest() {
      _aic.Clear();
      var ai = new UtilityAi("testai", _aic);
      Assert.That(ai.AddBehaviour("some"), Is.False);
    }

    [Test]
    public void RemoveNullBehaviourDoesNotThrowTest() {
      var ai = new UtilityAi();
      Assert.DoesNotThrow(() => ai.Remove((Behaviour)null));
    }

    [Test]
    public void RemoveNonExistentStringBehaviourReturnsFalseTest() {
      _aic.Clear();
      var ai = new UtilityAi("testai", _aic);
      Assert.That(ai.RemoveBehaviour("some"), Is.False);
    }

    [Test]
    public void RemoveStringBehaviourReturnsTrueTest() {
      _aic.Clear();
      var ai = new UtilityAi("testai", _aic);
      Assert.That(ai.AddBehaviour("b1"), Is.True);
      Assert.That(ai.RemoveBehaviour("b1"), Is.True);
    }

    [Test]
    public void DuplicateIsNotAddedTest() {
      var ai = new UtilityAi();
      var b = new Behaviour();
      b.NameId = "some";
      Assert.That(ai.Add(b), Is.True);
      Assert.That(ai.Add(b), Is.False);
    }

    [Test]
    public void RepeatedDeletionIsOkayTest() {
      var ai = new UtilityAi();
      var b = new Behaviour();
      b.NameId = "beh";
      Assert.That(ai.Add(b), Is.True);
      Assert.That(ai.Remove(b), Is.True);
      Assert.That(ai.Remove(b), Is.False);
    }

    [Test]
    public void NoBehavioursThenNullSelectionTest() {
      var ai = new UtilityAi();
      var context = new UaiContext();
      Assert.IsNull(ai.Select(context));
    }

    [Test]
    public void BehaviourSelectionTest() {
      var ai = new UtilityAi();
      var context = new UaiContext();
      var b1 = new Behaviour();
      b1.NameId = "b1";
      var b2 = new Behaviour();
      b2.NameId = "b2";
      var consideration1 = new BehaviourConsideration1();
      var consideration2 = new BehaviourConsideration2();
      b1.AddConsideration(consideration1);
      b2.AddConsideration(consideration2);

      var a1 = new MockAction();
      a1.NameId = "a1";
      var fo1 = new ConstantUtilityOption();
      fo1.SetAction(a1);
      b1.AddOption(fo1);
      var a2 = new MockAction();
      a2.NameId = "a2";
      var fo2 = new ConstantUtilityOption();
      fo2.SetAction(a2);
      b2.AddOption(fo2);

      ai.Add(b1);
      ai.Add(b2);

      context.Val1 = 1.0f;
      context.Val2 = 0.0f;
      var sel1 = ai.Select(context);
      Assert.That(sel1 == a1);
      context.Val1 = 0.0f;
      context.Val2 = 1.0f;
      var sel2 = ai.Select(context);
      Assert.That(sel2 == a2);
    }
  }

  public class BehaviourConsideration1 : ConsiderationBase<UaiContext> {
    public override void Consider(UaiContext context) {
      Utility = new Utility(context.Val1, Weight);
    }

    public override IConsideration Clone() {
      return new BehaviourConsideration1(this);
    }

    public BehaviourConsideration1() {
    }

    BehaviourConsideration1(BehaviourConsideration1 other) : base(other) {
    }
  }

  public class BehaviourConsideration2 : ConsiderationBase<UaiContext> {
    public override void Consider(UaiContext context) {
      Utility = new Utility(context.Val2, Weight);
    }

    public override IConsideration Clone() {
      return new BehaviourConsideration2(this);
    }

    public BehaviourConsideration2() {
    }

    BehaviourConsideration2(BehaviourConsideration2 other) : base(other) {
    }
  }

}