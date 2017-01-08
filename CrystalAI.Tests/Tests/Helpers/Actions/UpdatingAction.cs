// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// UpdatingAction.cs is part of Crystal AI.
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
using Crystal;


namespace CrystalAI.TestHelpers {

  public class UpdatingAction : ActionBase {
    int _currentIteration;
    public int MaxIterations { get; set; }

    public int UpdatesCount {
      get { return _currentIteration; }
    }

    public override IAction Clone() {
      return new UpdatingAction(this);
    }

    protected override void OnExecute(IContext context) {
      _currentIteration = 1;
    }

    protected override void OnUpdate(IContext context) {
      _currentIteration++;
      if(_currentIteration >= MaxIterations)
        EndInSuccess(context);
    }

    protected override void OnStop(IContext context) {
      // Do nothing
    }

    public UpdatingAction() {
    }

    UpdatingAction(UpdatingAction other) : base(other) {
      MaxIterations = other.MaxIterations;
    }

    public UpdatingAction(int iterations) {
      MaxIterations = iterations;
    }
  }

}