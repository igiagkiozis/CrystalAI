// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// AITransitionTests.cs is part of Crystal AI.
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


namespace Crystal.ActionTests {

  [TestFixture]
  public class AiTransitionTests {
    IActionCollection _actions;
    IConsiderationCollection _considerations;
    IOptionCollection _options;
    IBehaviourCollection _behaviours;
    IAiCollection _aIs;

    IUtilityAi _utilityAi;

    [OneTimeSetUp]
    public void Initialize() {
      _actions = new ActionCollection();
      _considerations = new ConsiderationCollection();
      _options = new OptionCollection(_actions, _considerations);
      _behaviours = new BehaviourCollection(_options);
      _aIs = new AiCollection(_behaviours);

      _utilityAi = new UtilityAi("ai0", _aIs);
    }

    [Test]
    public void ConstructorTest() {
      var transition = new AiTransition("transitionToAI0", "ai0", _aIs);
      Assert.IsNotNull(transition);
      Assert.AreEqual(transition.TargetAi.NameId, _utilityAi.NameId);
    }

    [Test]
    public void TargetAiNullExceptionTest() {
      Assert.Throws<AiTransition.TargetAiNullException>(() => new AiTransition(null));
    }

    [Test]
    public void ConstructorThrowsNameIdEmptyOrNullTest() {
      _aIs.ClearAll();
      Assert.Throws<ActionBase.NameIdEmptyOrNullException>(() => new AiTransition("", "a", _aIs));
      _aIs.ClearAll();
      Assert.Throws<ActionBase.NameIdEmptyOrNullException>(() => new AiTransition(null, "a", _aIs));
      _aIs.ClearAll();
      Assert.Throws<ActionBase.NameIdEmptyOrNullException>(() => new AiTransition("some", "", _aIs));
      _aIs.ClearAll();
      Assert.Throws<ActionBase.NameIdEmptyOrNullException>(() => new AiTransition("some", null, _aIs));
    }

    [Test]
    public void ConstructorThrowsActionCollectionNullTest() {
      _aIs.ClearAll();
      Assert.Throws<ActionBase.ActionCollectionNullException>(() => new AiTransition("la", "lida", null));
    }

    [Test]
    public void TargetAiDoesNotExistExceptionTest() {
      _aIs.ClearAll();
      var transition = new AiTransition("transition", "ai5", _aIs);
      Assert.Throws<AiTransition.TargetAiDoesNotExistException>(() => {
        var t = transition.TargetAi;
      });
      var ai5 = new UtilityAi("ai5", _aIs);
      Assert.DoesNotThrow(() => {
        var t = transition.TargetAi;
      });
    }
  }

}