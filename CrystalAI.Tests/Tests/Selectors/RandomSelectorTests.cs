// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// RandomSelectorTests.cs is part of Crystal AI.
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
using NUnit.Framework;


namespace Crystal.SelectorTests {

  [TestFixture]
  public class RandomSelectorTests {
    [Test]
    public void ConstructorTest() {
      var s = new RandomSelector();
      Assert.IsNotNull(s);
    }

    [Test]
    public void CloneTest() {
      var s = new RandomSelector();
      Assert.IsNotNull(s);
      var sc = s.Clone();
      Assert.IsNotNull(sc);
    }

    [Test]
    public void SelectTest([Range(-1, 21, 1)] int selIdx) {
      var rn = new MockRandom();
      rn.IntValue = selIdx;
      var uList = SelectorTestsHelper.CreateRandomUtilityList(20);
      var s = new RandomSelector(rn);

      var aIdx = s.Select(uList);
      var cIdx = selIdx.Clamp(0, 19);
      Assert.That(aIdx, Is.EqualTo(cIdx));
    }

    [Test]
    public void ZeroSizeUtilityListSelectTest() {
      var uList = new List<Utility>();
      var s = new RandomSelector();
      Assert.That(s.Select(uList), Is.EqualTo(-1));
    }
  }

}