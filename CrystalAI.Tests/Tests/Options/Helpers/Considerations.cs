// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// Considerations.cs is part of Crystal AI.
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
namespace Crystal.OptionTests {

  public class OptionContext : IContext {
    float _xVal1;
    float _xVal2;
    float _xVal3;
    float _xVal4;
    float _xVal5;
    float _xVal6;
    float _xVal7;
    float _xVal8;

    public float XVal1 {
      get { return _xVal1; }
      set { _xVal1 = value.Clamp<float>(0.0f, 10.0f); }
    }

    public float XVal2 {
      get { return _xVal2; }
      set { _xVal2 = value.Clamp<float>(0.0f, 10.0f); }
    }

    public float XVal3 {
      get { return _xVal3; }
      set { _xVal3 = value.Clamp<float>(0.0f, 10.0f); }
    }

    public float XVal4 {
      get { return _xVal4; }
      set { _xVal4 = value.Clamp<float>(0.0f, 10.0f); }
    }

    public float XVal5 {
      get { return _xVal5; }
      set { _xVal5 = value.Clamp<float>(0.0f, 10.0f); }
    }

    public float XVal6 {
      get { return _xVal6; }
      set { _xVal6 = value.Clamp<float>(0.0f, 10.0f); }
    }

    public float XVal7 {
      get { return _xVal7; }
      set { _xVal7 = value.Clamp<float>(0.0f, 10.0f); }
    }

    public float XVal8 {
      get { return _xVal8; }
      set { _xVal8 = value.Clamp<float>(0.0f, 10.0f); }
    }
  }

  public class OptionConsideration1 : ConsiderationBase<OptionContext> {
    IEvaluator _ev;

    public override void Consider(OptionContext context) {
      Utility = new Utility(_ev.Evaluate(context.XVal1), Weight);
    }

    public override IConsideration Clone() {
      return new OptionConsideration1(this);
    }

    public OptionConsideration1() {
      Initialize();
    }

    OptionConsideration1(OptionConsideration1 other) : base(other) {
      Initialize();
    }

    public OptionConsideration1(string nameId, IConsiderationCollection collection) : base(nameId, collection) {
      Initialize();
    }

    void Initialize() {
      var ptA = new Pointf(0.0f, 0.0f);
      var ptB = new Pointf(10.0f, 1.0f);
      _ev = new LinearEvaluator(ptA, ptB);
    }
  }

  public class OptionConsideration2 : ConsiderationBase<OptionContext> {
    IEvaluator _ev;

    public override void Consider(OptionContext context) {
      Utility = new Utility(_ev.Evaluate(context.XVal2), Weight);
    }

    public override IConsideration Clone() {
      return new OptionConsideration2(this);
    }

    public OptionConsideration2() {
      Initialize();
    }

    OptionConsideration2(OptionConsideration2 other) : base(other) {
      Initialize();
    }

    public OptionConsideration2(string nameId, IConsiderationCollection collection) : base(nameId, collection) {
      Initialize();
    }

    void Initialize() {
      var ptA = new Pointf(0.0f, 1.0f);
      var ptB = new Pointf(10.0f, 0.0f);
      _ev = new LinearEvaluator(ptA, ptB);
    }
  }

  public class OptionConsideration3 : ConsiderationBase<OptionContext> {
    IEvaluator _ev;

    public override void Consider(OptionContext context) {
      Utility = new Utility(_ev.Evaluate(context.XVal3), Weight);
    }

    public override IConsideration Clone() {
      return new OptionConsideration3(this);
    }

    public OptionConsideration3() {
      Initialize();
    }

    OptionConsideration3(OptionConsideration3 other) : base(other) {
      Initialize();
    }

    public OptionConsideration3(string nameId, IConsiderationCollection collection) : base(nameId, collection) {
      Initialize();
    }

    void Initialize() {
      var ptA = new Pointf(0.0f, 0.0f);
      var ptB = new Pointf(10.0f, 1.0f);
      _ev = new PowerEvaluator(ptA, ptB, 3.0f);
    }
  }

  public class OptionConsideration4 : ConsiderationBase<OptionContext> {
    IEvaluator _ev;

    public override void Consider(OptionContext context) {
      Utility = new Utility(_ev.Evaluate(context.XVal4), Weight);
    }

    public override IConsideration Clone() {
      return new OptionConsideration4(this);
    }

    public OptionConsideration4() {
      Initialize();
    }

    OptionConsideration4(OptionConsideration4 other) : base(other) {
      Initialize();
    }

    public OptionConsideration4(string nameId, IConsiderationCollection collection) : base(nameId, collection) {
      Initialize();
    }

    void Initialize() {
      var ptA = new Pointf(0.0f, 1.0f);
      var ptB = new Pointf(10.0f, 0.0f);
      _ev = new PowerEvaluator(ptA, ptB, .5f);
    }
  }

  public class OptionConsideration5 : ConsiderationBase<OptionContext> {
    IEvaluator _ev;

    public override void Consider(OptionContext context) {
      Utility = new Utility(_ev.Evaluate(context.XVal5), Weight);
    }

    public override IConsideration Clone() {
      return new OptionConsideration5(this);
    }

    public OptionConsideration5() {
      Initialize();
    }

    OptionConsideration5(OptionConsideration5 other) : base(other) {
      Initialize();
    }

    public OptionConsideration5(string nameId, IConsiderationCollection collection) : base(nameId, collection) {
      Initialize();
    }

    void Initialize() {
      var ptA = new Pointf(0.0f, 0.0f);
      var ptB = new Pointf(10.0f, 1.0f);
      _ev = new SigmoidEvaluator(ptA, ptB, -0.5f);
    }
  }

  public class OptionConsideration6 : ConsiderationBase<OptionContext> {
    IEvaluator _ev;

    public override void Consider(OptionContext context) {
      Utility = new Utility(_ev.Evaluate(context.XVal6), Weight);
    }

    public override IConsideration Clone() {
      return new OptionConsideration6(this);
    }

    public OptionConsideration6() {
      Initialize();
    }

    OptionConsideration6(OptionConsideration6 other) : base(other) {
      Initialize();
    }

    public OptionConsideration6(string nameId, IConsiderationCollection collection) : base(nameId, collection) {
      Initialize();
    }

    void Initialize() {
      var ptA = new Pointf(0.0f, 1.0f);
      var ptB = new Pointf(10.0f, 0.0f);
      _ev = new SigmoidEvaluator(ptA, ptB, -0.5f);
    }
  }

  public class OptionConsideration7 : ConsiderationBase<OptionContext> {
    IEvaluator _ev;

    public override void Consider(OptionContext context) {
      Utility = new Utility(_ev.Evaluate(context.XVal7), Weight);
    }

    public override IConsideration Clone() {
      return new OptionConsideration7(this);
    }

    public OptionConsideration7() {
      Initialize();
    }

    OptionConsideration7(OptionConsideration7 other) : base(other) {
      Initialize();
    }

    public OptionConsideration7(string nameId, IConsiderationCollection collection) : base(nameId, collection) {
      Initialize();
    }

    void Initialize() {
      var ptA = new Pointf(0.0f, 0.0f);
      var ptB = new Pointf(5.0f, 0.2f);
      var ev1 = new LinearEvaluator(ptA, ptB);
      var ptA1 = new Pointf(5.0f, 0.2f);
      var ptB1 = new Pointf(10.0f, 1.0f);
      var ev2 = new SigmoidEvaluator(ptA1, ptB1, -0.5f);
      var cev = new CompositeEvaluator();
      cev.Add(ev1);
      cev.Add(ev2);
      _ev = cev;
    }
  }

  public class OptionConsideration8 : ConsiderationBase<OptionContext> {
    IEvaluator _ev;

    public override void Consider(OptionContext context) {
      Utility = new Utility(_ev.Evaluate(context.XVal8), Weight);
    }

    public override IConsideration Clone() {
      return new OptionConsideration8(this);
    }

    public OptionConsideration8() {
      Initialize();
    }

    OptionConsideration8(OptionConsideration8 other) : base(other) {
      Initialize();
    }

    public OptionConsideration8(string nameId, IConsiderationCollection collection) : base(nameId, collection) {
      Initialize();
    }

    void Initialize() {
      var ptA = new Pointf(0.0f, 1.0f);
      var ptB = new Pointf(5.0f, 0.8f);
      var ev1 = new LinearEvaluator(ptA, ptB);
      var ptA1 = new Pointf(5.0f, 0.8f);
      var ptB1 = new Pointf(10.0f, 0.0f);
      var ev2 = new SigmoidEvaluator(ptA1, ptB1, -0.5f);
      var cev = new CompositeEvaluator();
      cev.Add(ev1);
      cev.Add(ev2);
      _ev = cev;
    }
  }

}