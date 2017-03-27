// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// OptionCollectionTests.cs is part of Crystal AI.
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

namespace Crystal.OptionTests {

  [TestFixture]
  public class OptionParameterTests {
    HelperAiConstructor _aiConstructor;

    [OneTimeSetUp]
    public void Initialize() {
      _aiConstructor = new HelperAiConstructor();
      SetupActionsAndConsiderations();
    }

    [Test]
    public void AcceptsParametersTest() {
      _aiConstructor.AIs.ClearAll();
      OptionParameters parameters = new OptionParameters();
      parameters.Power = true;

      var o = new Option("o1", _aiConstructor.Options, parameters);
      Assert.AreEqual(true, ((OptionParameters)o.Parameters).Power);
    }

    [Test]
    public void ActionSharesParameterValues() {
      _aiConstructor.AIs.ClearAll();
      SetupActionsAndConsiderations();
      OptionParameters parameters = new OptionParameters();
      parameters.Power = true;

      var o = new Option("o1", _aiConstructor.Options, parameters);
      Assert.That(o.SetAction("a1"), Is.True);
      Assert.AreEqual(parameters.Power, ((OptionParameters)o.Action.Parameters).Power);
    }

    [Test]
    public void ConsiderationsCanUseParameterValues([Range(0.0f, 10.0f, 5f)] float util) {
      _aiConstructor.AIs.ClearAll();
      SetupActionsAndConsiderations();

      OptionParameters linear = new OptionParameters();
      linear.Power = false;
      var linearOption = new Option("o2", _aiConstructor.Options, linear);
      Assert.That(linearOption.SetAction("a2"), Is.True);
      Assert.That(linearOption.AddConsideration("c1"), Is.True);

      OptionParameters power = new OptionParameters();
      power.Power = true;
      var powerOption = new Option("o1", _aiConstructor.Options, power);
      Assert.That(powerOption.SetAction("a1"), Is.True);
      Assert.That(powerOption.AddConsideration("c1"), Is.True);


      var context = new OptionContext();
      context.XVal9 = util;
      linearOption.Consider(context);
      powerOption.Consider(context);

      if(util == 0.0f || util == 10.0f) {
        Assert.AreEqual(linearOption.Utility.Value, powerOption.Utility.Value);
      }
      else {
        Assert.Greater(linearOption.Utility.Value, powerOption.Utility.Value);
      }
    }

    void SetupActionsAndConsiderations() {
      _aiConstructor.AIs.ClearAll();

      var tmpa = new MockAction("a1", _aiConstructor.Actions);
      tmpa = new MockAction("a2", _aiConstructor.Actions);

      var tmpc = new OptionParametersConsideration("c1", _aiConstructor.Considerations);
    }
  }
}