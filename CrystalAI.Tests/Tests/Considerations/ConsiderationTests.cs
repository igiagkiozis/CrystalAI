// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ConsiderationTests.cs is part of Crystal AI.
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
using CrystalAI.TestHelpers;
using NUnit.Framework;


namespace Crystal.ConsiderationTests {

  [TestFixture]
  public class ConsiderationTests {
    CustomContext _customContext;

    [OneTimeSetUp]
    public void Initialize() {
      _customContext = new CustomContext();
    }

    [Test]
    public void ConstructorTest() {
      var c = new BasicConsideration();
      Assert.IsNotNull(c);
    }

    [Test]
    public void GenericConstructorTest() {
      var c = new GenericConsideration();
      Assert.IsNotNull(c);
    }

    [Test, TestCase(0.1f, 0.2f), TestCase(0.0f, 0.0f), TestCase(1.0f, 0.0f), TestCase(0.5f, 0.4f), TestCase(0.5f, 0.0f),
     TestCase(0.2f, 1.0f), TestCase(0.7f, 0.4f)]
    public void ConsiderTest(float value, float weight) {
      var c = new BasicConsideration();
      _customContext.BaseUtility = new Utility(value, weight);
      c.Consider(_customContext);
      var retUtil = c.Utility;
      Assert.AreEqual(_customContext.BaseUtility, retUtil);
    }

    [Test, TestCase(0.1f, 0.2f), TestCase(0.0f, 0.0f), TestCase(1.0f, 0.0f), TestCase(0.5f, 0.4f), TestCase(0.5f, 0.0f),
     TestCase(0.2f, 1.0f), TestCase(0.7f, 0.4f)]
    public void GenericConsiderTest(float value, float weight) {
      var c = new GenericConsideration();
      _customContext.BaseUtility = new Utility(value, weight);
      c.Consider(_customContext);
      var retUtil = c.Utility;
      Assert.AreEqual(_customContext.BaseUtility, retUtil);
    }
  }

}