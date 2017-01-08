// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// SequentialSelectorTests.cs is part of Crystal AI.
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
  public class SequentialSelectorTests {
    Pcg _rnd = new Pcg();

    [Test]
    public void ConstructorTest() {
      var s = new SequentialSelector();
      Assert.IsNotNull(s);
    }

    [Test]
    public void SelectTest1() {
      var size = 20;
      var s = new SequentialSelector();
      var list = SelectorTestsHelper.CreateRandomUtilityList(size);

      for(int i = 0; i < 1000; i++) {
        var aVal = s.Select(list);
        var cVal = i % size;
        Assert.AreEqual(cVal, aVal);
      }
    }

    [Test]
    public void SelectTest2() {
      var size = 11;
      var s = new SequentialSelector();
      var list = SelectorTestsHelper.CreateRandomUtilityList(size);

      for(int i = 0; i < 1000; i++) {
        var aVal = s.Select(list);
        var cVal = i % size;
        Assert.AreEqual(cVal, aVal);
      }

      // Now change size
      size = 35;
      list = SelectorTestsHelper.CreateRandomUtilityList(size);

      for(int i = 0; i < 1000; i++) {
        var aVal = s.Select(list);
        var cVal = i % size;
        Assert.AreEqual(cVal, aVal);
      }
    }

    [Test]
    public void ZeroSizeUtilityListSelectTest() {
      var uList = new List<Utility>();
      var s = new SequentialSelector();
      Assert.That(s.Select(uList), Is.EqualTo(-1));
    }
  }

}