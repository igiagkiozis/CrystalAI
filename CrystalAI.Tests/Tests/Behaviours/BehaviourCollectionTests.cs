// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// BehaviourCollectionTests.cs is part of Crystal AI.
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
using NUnit.Framework;


#pragma warning disable 


namespace Crystal.BehaviourTests {

  [TestFixture]
  public class BehaviourCollectionTests {
    IActionCollection _actions;
    IConsiderationCollection _considerations;
    IOptionCollection _options;
    IBehaviourCollection _behaviours;

    [OneTimeSetUp]
    public void Initialize() {
      _actions = new ActionCollection();
      _considerations = new ConsiderationCollection();
      _options = new OptionCollection(_actions, _considerations);
      _behaviours = new BehaviourCollection(_options);
    }

    [Test]
    public void AddBehaviourTest() {
      _behaviours.ClearAll();
      var behaviour = new Behaviour();
      behaviour.NameId = "some";
      Assert.That(_behaviours.Add(behaviour));
      Assert.That(_behaviours.Add(behaviour) == false);
    }

    [Test]
    public void CreateBehaviourTest() {
      _behaviours.ClearAll();
      var behaviour = new Behaviour();
      behaviour.NameId = "some";
      _behaviours.Add(behaviour);
      var newOption = _behaviours.Create("some");
      Assert.AreEqual(behaviour.NameId, newOption.NameId);
      Assert.AreNotEqual(behaviour, newOption);
    }

    [Test]
    public void ContainsBehaviourTest1() {
      _behaviours.ClearAll();
      var behaviour = new Behaviour("name", _behaviours);
      Assert.That(_behaviours.Contains("name"));
    }

    [Test]
    public void ContainsBehaviourTest2() {
      _behaviours.ClearAll();
      var behaviour = new Behaviour();
      behaviour.NameId = "name";
      _behaviours.Add(behaviour);
      Assert.That(_behaviours.Contains("name"));
    }

    [Test]
    public void ClearTest() {
      _behaviours.ClearAll();
      var behaviour = new Behaviour("name", _behaviours);
      Assert.That(_behaviours.Contains("name"));
      _behaviours.Clear();
      Assert.That(_behaviours.Contains("name") == false);
    }
  }

}