// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ConsiderationCollectionTests.cs is part of Crystal AI.
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


namespace Crystal.ConsiderationTests {

  [TestFixture]
  public class ConsiderationCollectionTests {
    [Test]
    public void ConstructorTest() {
      var collection = new ConsiderationCollection();
      Assert.IsNotNull(collection);
    }

    [Test]
    public void AddConsiderationTest() {
      var collection = new ConsiderationCollection();
      var consideration = new BasicConsideration();
      consideration.NameId = "some";
      Assert.That(collection.Add(consideration));
      Assert.That(collection.Add(consideration) == false);
    }

    [Test]
    public void CreateConsiderationTest() {
      var collection = new ConsiderationCollection();
      var consideration = new BasicConsideration();
      consideration.NameId = "some";
      collection.Add(consideration);
      var newConsideration = collection.Create("some");
      Assert.AreEqual(consideration.NameId, newConsideration.NameId);
      Assert.AreNotEqual(consideration, newConsideration);
    }

    [Test]
    public void ContainsConsiderationTest1() {
      var collection = new ConsiderationCollection();
      var consideration = new BasicConsideration("name", collection);
      Assert.That(collection.Contains("name"));
    }

    [Test]
    public void ContainsConsiderationTest2() {
      var collection = new ConsiderationCollection();
      var consideration = new BasicConsideration();
      consideration.NameId = "name";
      collection.Add(consideration);
      Assert.That(collection.Contains("name"));
    }

    [Test]
    public void ClearTest() {
      var collection = new ConsiderationCollection();
      var consideration = new BasicConsideration("name", collection);
      Assert.That(collection.Contains("name"));
      collection.Clear();
      Assert.That(collection.Contains("name") == false);
    }
  }

}