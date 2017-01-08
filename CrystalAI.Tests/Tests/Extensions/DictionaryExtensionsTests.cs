// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// DictionaryExtensionsTests.cs is part of Crystal AI.
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


namespace Crystal.ExtensionsTests {

  [TestFixture]
  internal class DictionaryExtensionsTests {
    Dictionary<int, string> _dict;

    [OneTimeSetUp]
    public void Initialize() {
      _dict = new Dictionary<int, string>();
      for(int i = 0; i < 10; i++)
        _dict.Add(i, i.ToString());
    }

    [Test]
    public void GetValueTest() {
      Assert.That(_dict.Value(0) == 0.ToString());
    }

    [Test]
    public void RemoveAllTest() {
      var d = new Dictionary<int, string>(_dict);
      d.RemoveAll(i => i >= 1);
      Assert.That(d.Count == 1);
    }
  }

}