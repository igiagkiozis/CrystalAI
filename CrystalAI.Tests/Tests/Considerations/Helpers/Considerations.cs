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
using CrystalAI.TestHelpers;


namespace Crystal.ConsiderationTests {

  public class BasicConsideration : ConsiderationBase {
    IEvaluator _ev;

    public override void Consider(IContext context) {
      var c = (CustomContext)context;
      Utility = new Utility(_ev.Evaluate(c.BaseUtility.Value), c.BaseUtility.Weight);
    }

    public override IConsideration Clone() {
      return new BasicConsideration(this);
    }

    public BasicConsideration() {
      Initialize();
    }

    BasicConsideration(BasicConsideration other) : base(other) {
      Initialize();
    }

    public BasicConsideration(string nameId, IConsiderationCollection collection)
      : base(nameId, collection) {
      Initialize();
    }

    void Initialize() {
      var ptA = new Pointf(0.0f, 0.0f);
      var ptB = new Pointf(1.0f, 1.0f);
      _ev = new LinearEvaluator(ptA, ptB);
    }
  }

  public class GenericConsideration : ConsiderationBase<CustomContext> {
    public override void Consider(CustomContext context) {
      Utility = context.BaseUtility;
    }

    public override IConsideration Clone() {
      return new GenericConsideration(this);
    }

    public GenericConsideration() {
    }

    GenericConsideration(GenericConsideration other) : base(other) {
    }

    GenericConsideration(string nameId, IConsiderationCollection collection)
      : base(nameId, collection) {
    }
  }

}