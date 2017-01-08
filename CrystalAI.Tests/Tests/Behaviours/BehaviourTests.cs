// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// BehaviourTests.cs is part of Crystal AI.
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
using System.Collections.Generic;
using Crystal.OptionTests;
using CrystalAI.TestHelpers;
using NUnit.Framework;


namespace Crystal.BehaviourTests {

  [TestFixture]
  public class BehaviourTests {
    IContext _customContext;
    OptionContext _optionContext;
    HelperAiConstructor _aiConstructor;
    Pcg _rng;

    IConsideration _c1;
    IConsideration _c2;
    IConsideration _c3;
    IConsideration _c4;
    IConsideration _c5;
    IConsideration _c6;
    IConsideration _c7;
    IConsideration _c8;

    [OneTimeSetUp]
    public void Initialize() {
      _aiConstructor = new HelperAiConstructor();
      _customContext = new CustomContext();
      _optionContext = new OptionContext();
      _rng = new Pcg();
      CreateConsiderations();
    }

    [Test]
    public void ConstructorTest() {
      var r = new Behaviour();
      r.Selector = new MaxUtilitySelector();
      Assert.IsNotNull(r);
    }

    [Test, TestCase(0.2f, 0.3f), TestCase(0.43f, 0.0f), TestCase(0.112f, 0.23f), TestCase(0.3f, 0.3f)]
    public void SelectTest1(float uval1, float uval2) {
      var r = new Behaviour();
      r.Selector = new MaxUtilitySelector();

      r.AddOption(new ConstantUtilityOption());

      var option1 = new ConstantUtilityOption();
      option1.SetAction(new MockAction());
      option1.DefaultUtility = uval1;
      var option2 = new ConstantUtilityOption();
      option2.SetAction(new MockAction());
      option2.DefaultUtility = uval2;
      Assert.That(r.AddOption(option1), Is.True);
      Assert.That(r.AddOption(option2), Is.True);

      IAction cAction;
      cAction = uval2 > uval1 ? option2.Action : option1.Action;

      // When all options have equal utility the first one 
      // is selected. Does this make sense? Perhaps...
      IAction aAction = r.Select(_customContext);

      Assert.That(cAction == aAction);
    }

    [Test, Repeat(10000)]
    public void SelectTreeOfConsiderationsTest() {
      var r = new Behaviour();
      r.Selector = new MaxUtilitySelector();
      RandomizeOptionContext();

      var optionRootA = CreateOptionA();
      var optionRootB = CreateOptionB();
      r.AddOption(optionRootA);
      r.AddOption(optionRootB);

      IAction aAction = r.Select(_optionContext);

      var aVal = ComputeConsiderationsValue(_c1, _c2, _c3, _c4);
      var bVal = ComputeConsiderationsValue(_c5, _c6, _c7, _c8);
      IOption cOption = GetCorrectOption(optionRootA, aVal, optionRootB, bVal);

      Assert.That(cOption.Action == aAction);
    }

    [Test]
    public void DoesNotAddDuplicateOptionTest() {
      var b = new Behaviour();
      var o = new Option();
      Assert.That(b.AddConsideration(o), Is.True);
      Assert.That(b.AddConsideration(o), Is.False);
    }

    [Test]
    public void AddConsiderationTest() {
      var b = new Behaviour();
      var consideration = new BehaviourConsideration();
      b.AddConsideration(consideration);
    }

    [Test]
    public void AddDuplicateConsiderationTest() {
      var b = new Behaviour();
      var consideration = new BehaviourConsideration();
      b.AddConsideration(consideration);
      b.AddConsideration(consideration);
    }

    [Test]
    public void ConsiderTest([Values(-1f, 0f, 0.5f, 1f, 2f)] float value) {
      var b = new Behaviour();
      var context = new BehaviourContext();
      var consideration = new BehaviourConsideration();
      b.AddConsideration(consideration);
      context.Bval = value;
      var cVal = value.Clamp01();
      b.Consider(context);
      Assert.That(b.Utility.Value, Is.EqualTo(cVal).Within(Tolerance));
      Assert.That(b.Utility.Weight, Is.EqualTo(One).Within(Tolerance));
    }

    void RandomizeOptionContext() {
      _optionContext.XVal1 = (float)(_rng.NextDouble() * 10.0);
      _optionContext.XVal3 = (float)(_rng.NextDouble() * 10.0);
      _optionContext.XVal5 = (float)(_rng.NextDouble() * 10.0);
      _optionContext.XVal7 = (float)(_rng.NextDouble() * 10.0);

      _optionContext.XVal2 = (float)(_rng.NextDouble() * 10.0);
      _optionContext.XVal4 = (float)(_rng.NextDouble() * 10.0);
      _optionContext.XVal6 = (float)(_rng.NextDouble() * 10.0);
      _optionContext.XVal8 = (float)(_rng.NextDouble() * 10.0);
    }

    void CreateConsiderations() {
      _aiConstructor.Collection.ClearAll();
      _c1 = new OptionConsideration1("c1", _aiConstructor.Considerations);
      _c2 = new OptionConsideration3("c2", _aiConstructor.Considerations);
      _c3 = new OptionConsideration5("c3", _aiConstructor.Considerations);
      _c4 = new OptionConsideration7("c4", _aiConstructor.Considerations);

      _c5 = new OptionConsideration2("c5", _aiConstructor.Considerations);
      _c6 = new OptionConsideration4("c6", _aiConstructor.Considerations);
      _c7 = new OptionConsideration6("c7", _aiConstructor.Considerations);
      _c8 = new OptionConsideration8("c8", _aiConstructor.Considerations);
    }

    Option CreateOptionA() {
      var optionRootA = new Option();
      optionRootA.Measure = new Chebyshev();
      optionRootA.SetAction(new MockAction());
      optionRootA.NameId = "Root Option A";

      var optionL1A = new CompositeConsideration {
        Measure = new Chebyshev()
      };
      optionL1A.NameId = "l1a";
      var optionL2A = new CompositeConsideration {
        Measure = new Chebyshev()
      };
      optionL2A.NameId = "l2a";
      optionRootA.AddConsideration(optionL1A);
      optionRootA.AddConsideration(optionL2A);
      optionL1A.AddConsideration(_c1);
      optionL1A.AddConsideration(_c2);
      optionL2A.AddConsideration(_c3);
      optionL2A.AddConsideration(_c4);
      return optionRootA;
    }

    Option CreateOptionB() {
      var optionRootB = new Option();
      optionRootB.Measure = new Chebyshev();
      optionRootB.NameId = "Root Option B";
      optionRootB.SetAction(new MockAction());
      var optionL1B = new CompositeConsideration {
        Measure = new Chebyshev()
      };
      optionL1B.NameId = "l1b";
      var optionL2B = new CompositeConsideration {
        Measure = new Chebyshev()
      };
      optionL2B.NameId = "l2b";
      optionRootB.AddConsideration(optionL1B);
      optionRootB.AddConsideration(optionL2B);
      optionL1B.AddConsideration(_c5);
      optionL1B.AddConsideration(_c6);
      optionL2B.AddConsideration(_c7);
      optionL2B.AddConsideration(_c8);
      return optionRootB;
    }

    float ComputeConsiderationsValue(IConsideration c1, IConsideration c2, IConsideration c3, IConsideration c4) {
      var oA1List = new List<Utility>();
      var oA2List = new List<Utility>();
      c1.Consider(_optionContext);
      c2.Consider(_optionContext);
      c3.Consider(_optionContext);
      c4.Consider(_optionContext);
      oA1List.Add(c1.Utility);
      oA1List.Add(c2.Utility);
      oA2List.Add(c3.Utility);
      oA2List.Add(c4.Utility);

      var a1Util = new Utility(oA1List.Chebyshev(), 1.0f);
      var a2Util = new Utility(oA2List.Chebyshev(), 1.0f);
      var aList = new List<Utility>();
      aList.Add(a1Util);
      aList.Add(a2Util);

      return aList.Chebyshev();
    }

    IOption GetCorrectOption(IOption optionA, float aVal, IOption optionB, float bVal) {
      IOption cOption;
      cOption = aVal > bVal ? optionA : optionB;
      if(CrMath.AeqB(aVal, bVal))
        cOption = optionA;
      return cOption;
    }

    const float One = 1.0f;
    const float Zero = 0.0f;
    const float Tolerance = 1e-6f;
  }

}