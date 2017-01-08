// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// UpdatingGenericAction.cs is part of Crystal AI.
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

  public class UpdatingGenericAction : ActionBase<CustomContext> {
    int _currentIteration;
    public int MaxIterations { get; set; }

    public override IAction Clone() {
      return new UpdatingGenericAction(this);
    }

    protected override void OnExecute(CustomContext context) {
      _currentIteration = 1;
    }

    protected override void OnUpdate(CustomContext context) {
      _currentIteration++;
      if(_currentIteration >= MaxIterations)
        EndInSuccess(context);
    }

    protected override void OnStop(CustomContext context) {
    }

    public UpdatingGenericAction() {
    }

    UpdatingGenericAction(UpdatingGenericAction other) : base(other) {
    }

    public UpdatingGenericAction(int iterations) {
      MaxIterations = iterations;
    }
  }

}