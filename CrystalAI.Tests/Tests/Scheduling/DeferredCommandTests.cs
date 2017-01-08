// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// DeferredCommandTests.cs is part of Crystal AI.
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
  internal class DeferredCommandTests {
    [Test]
    public void ConstructorTest() {
      CommandAction ca = () => { };
      var dc = new DeferredCommand(ca);
      Assert.IsNotNull(dc);
      Assert.IsNotNull(dc.Process);
      Assert.That(dc.Process, Is.EqualTo(ca));
    }

    [Test]
    public void NullProcessThrowsTest() {
      Assert.Throws<DeferredCommand.ProcessNullException>(() => new DeferredCommand(null));
    }

    [Test]
    public void RepeatingTest() {
      CommandAction ca = () => { };
      var dc = new DeferredCommand(ca);
      Assert.That(dc.IsRepeating, Is.True);
      dc.IsRepeating = false;
      Assert.That(dc.IsRepeating, Is.False);
    }

    [Test]
    public void GetSetFirstExecutionDelayMinTest() {
      CommandAction ca = () => { };
      var dc = new DeferredCommand(ca);
      Assert.That(dc.InitExecutionDelayMin, Is.EqualTo(0f));
      dc.InitExecutionDelayMin = 0.5f;
      Assert.That(dc.InitExecutionDelayMin, Is.EqualTo(0.5f));
      dc.InitExecutionDelayMin = -1f;
      Assert.That(dc.InitExecutionDelayMin, Is.EqualTo(0f));
    }
  }

}