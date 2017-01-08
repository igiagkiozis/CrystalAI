// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// Processes.cs is part of Crystal AI.
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
using System.Threading;


#pragma warning disable


namespace Crystal.SchedulingTests {

  public class Processes {
    Pcg _rng = new Pcg();
    double _someNumber;

    public void Process1() {
      // Simulate workload.
      var a = _rng.NextDouble();
      if(a > 0.5)
        _someNumber = _rng.NextDouble();
      else
        _someNumber = _rng.NextDouble();
    }

    public void Process2() {
      // Simulate workload.
      var a = _rng.NextDouble();
      if(a > 0.5)
        _someNumber = _rng.NextDouble();
      else
        _someNumber = _rng.NextDouble();
    }

    public void Process3() {
      // Simulate workload.
      var a = _rng.NextDouble();
      if(a > 0.5)
        _someNumber = _rng.NextDouble();
      else
        _someNumber = _rng.NextDouble();
    }

    public void PopulateRandomList100() {
      var list = new List<double>();
      for(int i = 0; i < 100; i++)
        list.Add(_rng.NextDouble());
    }

    public void SleepOneMs() {
      Thread.Sleep(1);
    }
  }

}