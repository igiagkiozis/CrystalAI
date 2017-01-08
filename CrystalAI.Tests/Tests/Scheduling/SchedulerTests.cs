// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// SchedulerTests.cs is part of Crystal AI.
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


namespace Crystal.SchedulingTests {

  [TestFixture]
  internal class SchedulerTests {
    [Test]
    public void ConstructorTest() {
      var s = new Scheduler();
      Assert.IsNotNull(s);
      Assert.IsNotNull(s.ThinkStream);
      Assert.IsNotNull(s.UpdateStream);
    }

    [Test]
    public void TickTest() {
      var s = new Scheduler();
      s.Tick();
    }
  }

}