// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// GeneralOptionTests.cs is part of Crystal AI.
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
using CrystalAI.TestHelpers;
using NUnit.Framework;


#pragma warning disable


namespace Crystal.OptionTests {

  [TestFixture]
  public class GeneralOptionTests {
    HelperAiConstructor _aiConstructor;

    [OneTimeSetUp]
    public void Initialize() {
      _aiConstructor = new HelperAiConstructor();
      SetupActionsAndConsiderations();
    }

    [Test]
    public void NameConstructorThrowsIfOptionWithNameAlreadyExistsTest() {
      _aiConstructor.Collection.ClearAll();
      var o = new Option("name", _aiConstructor.Options);
      Assert.Throws<Option.OptionAlreadyExistsInCollectionException>(() => new Option("name", _aiConstructor.Options));
    }

    [Test]
    public void SetActionIfNullRetrunFalseTest() {
      var o = new Option();
      Assert.That(o.SetAction((IAction)null), Is.False);
    }

    [Test]
    public void SetActionStringIfNullOrEmptyFalseTest() {
      var o = new Option();
      Assert.That(o.SetAction((string)null), Is.False);
    }

    [Test]
    public void SetActionStringCollectionNullFalseTest() {
      var o = new Option();
      Assert.That(o.SetAction("some"), Is.False);
    }

    [Test]
    public void SetActionStringNonExistentFalseTest() {
      _aiConstructor.Collection.ClearAll();
      var o = new Option("name", _aiConstructor.Options);
      Assert.That(o.SetAction("nonexistent"), Is.False);
    }

    [Test]
    public void CanAddExistingActionTest() {
      _aiConstructor.Collection.ClearAll();
      SetupActionsAndConsiderations();
      var o = new Option("o1", _aiConstructor.Options);
      Assert.That(o.SetAction("a1"));
    }

    [Test]
    public void RefusesToAddNonExistentActionTest() {
      _aiConstructor.Collection.ClearAll();
      var o = new Option("o1", _aiConstructor.Options);
      Assert.That(o.SetAction("NonExistentAction"), Is.False);
    }

    [Test]
    public void DoesNotAddEmptyActionTest() {
      _aiConstructor.Collection.ClearAll();
      var o = new Option("o1", _aiConstructor.Options);
      Assert.That(o.SetAction(""), Is.False);
    }

    [Test]
    public void DoesNotAddNullStringActionTest() {
      _aiConstructor.Collection.ClearAll();
      var o = new Option("o1", _aiConstructor.Options);
      Assert.That(o.SetAction((string)null), Is.False);
    }

    [Test]
    public void ClonedOptionHasPreviousActionTest() {
      _aiConstructor.Collection.ClearAll();
      SetupActionsAndConsiderations();
      var o = new Option("o1", _aiConstructor.Options);
      o.SetAction("a1");
      var oc = o.Clone() as IOption;
      Assert.IsNotNull(oc);
      Assert.That(oc.Action.NameId == o.Action.NameId);
    }

    [Test]
    public void ClonedOptionHasAllPreviousConsiderationsTest() {
      _aiConstructor.Collection.ClearAll();
      SetupActionsAndConsiderations();
      var o = new Option("o1", _aiConstructor.Options);
      o.AddConsideration("c1");
      o.AddConsideration("c2");
      o.AddConsideration("c3");
      o.AddConsideration("c4");
      var oc = o.Clone() as IOption;
      Assert.IsNotNull(oc);
      Assert.That(oc.AddConsideration("c1"), Is.False);
      Assert.That(oc.AddConsideration("c2"), Is.False);
      Assert.That(oc.AddConsideration("c3"), Is.False);
      Assert.That(oc.AddConsideration("c4"), Is.False);
      Assert.That(oc.AddConsideration("c5"), Is.True);
    }

    [Test]
    public void EnsureClonedOptionHasResultsInSameUtilityTest([Range(0.0f, 10.0f, 1f)] float util) {
      _aiConstructor.Collection.ClearAll();
      SetupActionsAndConsiderations();
      var context = new OptionContext();
      var o = new Option("o1", _aiConstructor.Options);
      Assert.IsNotNull(o);
      Assert.That(o.SetAction("a1"), Is.True);
      o.AddConsideration("c1");
      var oc = o.Clone() as IOption;
      Assert.IsNotNull(oc);

      context.XVal1 = util;
      o.Consider(context);
      oc.Consider(context);
      Assert.AreEqual(o.Utility.Value, oc.Utility.Value);
      Assert.AreEqual(o.Utility.Weight, oc.Utility.Weight);
      Console.WriteLine("{0}", o.Utility.Value);
    }

    void SetupActionsAndConsiderations() {
      _aiConstructor.Collection.ClearAll();

      var tmpa = new MockAction("a1", _aiConstructor.Actions);
      tmpa = new MockAction("a2", _aiConstructor.Actions);
      tmpa = new MockAction("a3", _aiConstructor.Actions);
      tmpa = new MockAction("a4", _aiConstructor.Actions);

      var tmpc = new OptionConsideration1("c1", _aiConstructor.Considerations);
      tmpc = new OptionConsideration1("c2", _aiConstructor.Considerations);
      tmpc = new OptionConsideration1("c3", _aiConstructor.Considerations);
      tmpc = new OptionConsideration1("c4", _aiConstructor.Considerations);
      tmpc = new OptionConsideration1("c5", _aiConstructor.Considerations);
    }
  }

}