// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// OptionCollectionTests.cs is part of Crystal AI.
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


#pragma warning disable


namespace Crystal.OptionTests {

  [TestFixture]
  public class OptionCollectionTests {
    IActionCollection _actions;
    IConsiderationCollection _considerations;
    IOptionCollection _options;

    [OneTimeSetUp]
    public void Initialize() {
      _actions = new ActionCollection();
      _considerations = new ConsiderationCollection();
      _options = new OptionCollection(_actions, _considerations);
    }

    [Test]
    public void AddOptionTest() {
      _options.Clear();
      var option = new Option();
      option.NameId = "some";
      Assert.That(_options.Add(option));
      Assert.That(_options.Add(option) == false);
    }

    [Test]
    public void CreateOptionTest() {
      var option = new Option();
      option.NameId = "some";
      _options.Add(option);
      var newOption = _options.Create("some");
      Assert.AreEqual(option.NameId, newOption.NameId);
      Assert.AreNotEqual(option, newOption);
    }

    [Test]
    public void ContainsOptionTest1() {
      var option = new Option("name", _options);
      Assert.That(_options.Contains("name"));
    }

    [Test]
    public void ContainsOptionTest2() {
      var option = new Option();
      option.NameId = "name";
      _options.Add(option);
      Assert.That(_options.Contains("name"));
    }

    [Test]
    public void ClearTest() {
      var option = new Option("name", _options);
      Assert.That(_options.Contains("name"));
      _options.Clear();
      Assert.That(_options.Contains("name") == false);
    }
  }

}