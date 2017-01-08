// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// MockConsideration.cs is part of Crystal AI.
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
namespace Crystal.GeneralTests {

  public class MockConsideration : ConsiderationBase<MockContext> {
    Pcg _rnd = new Pcg();
    IEvaluator _ev;
    public Pointf PtA { get; set; }
    public Pointf PtB { get; set; }

    public override void Consider(MockContext context) {
      var v = _ev.Evaluate((float)_rnd.NextDouble());
      Utility = new Utility(v, Weight);
    }

    public override IConsideration Clone() {
      return new MockConsideration(this);
    }

    public MockConsideration() {
      Initialize();
    }

    MockConsideration(MockConsideration other)
      : base(other) {
    }

    public MockConsideration(string nameId, IConsiderationCollection collection)
      : base(nameId, collection) {
      Initialize();
    }

    void Initialize() {
      PtA = new Pointf(0.0f, 0.0f);
      PtB = new Pointf(1.0f, 1.0f);
      _ev = new LinearEvaluator(PtA, PtB);
    }
  }

}