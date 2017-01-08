// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// MaxUtilitySelectorTests.cs is part of Crystal AI.
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
  public class MaxUtilitySelectorTests {
    [Test]
    public void ConstructorTest() {
      var s = new MaxUtilitySelector();
      Assert.IsNotNull(s);
    }

    [Test]
    public void SelectTest() {
      var size = 20;
      var s = new MaxUtilitySelector();
      for(int i = 0; i < 100; i++) {
        var list = SelectorTestsHelper.CreateRandomUtilityList(size);
        var aVal = s.Select(list);
        var cVal = MaxUtilityIndex(list);
        Assert.AreEqual(cVal, aVal);
      }
    }

    [Test]
    public void ZeroSizeUtilityListSelectTest() {
      var uList = new List<Utility>();
      var s = new MaxUtilitySelector();
      Assert.That(s.Select(uList), Is.EqualTo(-1));
    }

    public int MaxUtilityIndex(List<Utility> elements) {
      var count = elements.Count;
      var maxUtil = -1.0f;
      int index = -1;
      for(int i = 0; i < count; i++) {
        var el = elements[i];
        if(el.Combined > maxUtil) {
          maxUtil = el.Combined;
          index = i;
        }
      }

      return index;
    }
  }

}