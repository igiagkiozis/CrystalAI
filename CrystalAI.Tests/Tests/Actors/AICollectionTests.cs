// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// AICollectionTests.cs is part of Crystal AI.
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


namespace Crystal.ActorTests {

  [TestFixture]
  public class AiCollectionTests {
    IActionCollection _actions;
    IConsiderationCollection _considerations;
    IOptionCollection _options;
    IBehaviourCollection _behaviours;
    AiCollection _aIs;

    [OneTimeSetUp]
    public void Initialize() {
      _actions = new ActionCollection();
      _considerations = new ConsiderationCollection();
      _options = new OptionCollection(_actions, _considerations);
      _behaviours = new BehaviourCollection(_options);
      _aIs = new AiCollection(_behaviours);
    }

    [Test]
    public void AddAiTest() {
      _aIs.ClearAll();
      var ai = new UtilityAi();
      ai.NameId = "some";
      Assert.That(_aIs.Add(ai));
      Assert.That(_aIs.Add(ai) == false);
    }

    [Test]
    public void CreateAiTest() {
      _aIs.ClearAll();
      var ai = new UtilityAi();
      ai.NameId = "some";
      _aIs.Add(ai);
      var newAi = _aIs.Create("some");
      Assert.AreEqual(ai.NameId, newAi.NameId);
      Assert.AreNotEqual(ai, newAi);
    }

    [Test]
    public void ContainsAiTest1() {
      _aIs.ClearAll();
      var ai = new UtilityAi("name", _aIs);
      Assert.That(_aIs.Contains("name"));
    }

    [Test]
    public void ContainsAiTest2() {
      _aIs.ClearAll();
      var ai = new UtilityAi();
      ai.NameId = "name";
      _aIs.Add(ai);
      Assert.That(_aIs.Contains("name"));
    }

    [Test]
    public void ClearTest() {
      _aIs.ClearAll();
      var ai = new UtilityAi("name", _aIs);
      Assert.That(_aIs.Contains("name"));
      _aIs.Clear();
      Assert.That(_aIs.Contains("name") == false);
    }
  }

}