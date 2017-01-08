// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// GeneralCompositeConsiderationTests.cs is part of Crystal AI.
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


#pragma warning disable


namespace Crystal.ConsiderationTests {

  [TestFixture]
  public class GeneralCompositeConsiderationTests {
    IConsiderationCollection _considerations;

    [OneTimeSetUp]
    public void Initialize() {
      _considerations = new ConsiderationCollection();
    }

    [Test]
    public void ConstructorTest() {
      _considerations.Clear();
      var c = ConsiderationConstructor.Chebyshev("name", _considerations);
      Assert.That(_considerations.Contains("name"));
    }

    [Test]
    public void ThrowsNameIdAlreadyExistsTest() {
      _considerations.Clear();
      var c1 = ConsiderationConstructor.Chebyshev("name", _considerations);
      Assert.Throws<CompositeConsideration.NameIdAlreadyExistsInCollectionException>(
                                                                                     () =>
                                                                                       ConsiderationConstructor
                                                                                         .Chebyshev("name",
                                                                                                    _considerations));
    }

    [Test]
    public void EnsureNullIsNotAcceptedByMeasureTest() {
      _considerations.Clear();
      var c = ConsiderationConstructor.Chebyshev("name", _considerations);
      Assert.IsNotNull(c.Measure);
      c.Measure = null;
      Assert.IsNotNull(c.Measure);
    }

    [Test]
    public void EnsureSettingDefaultUtilityAlsoSetsUtilityTest() {
      _considerations.Clear();
      var c = ConsiderationConstructor.Chebyshev("name", _considerations);
      c.DefaultUtility = new Utility(0.4f, 0.5f);
      Assert.AreEqual(c.DefaultUtility.Value, c.Utility.Value);
      Assert.AreEqual(c.DefaultUtility.Weight, c.Utility.Weight);
    }

    [Test]
    public void EnsureDuplicateInstancedConsiderationIsNotAddedTest() {
      _considerations.Clear();
      var c = ConsiderationConstructor.Chebyshev("name", _considerations);
      var b1 = new BasicConsideration("b1", _considerations);
      var b1C = b1.Clone();
      Assert.That(c.AddConsideration(b1));
      Assert.That(c.AddConsideration(b1C) == false);
    }

    [Test]
    public void EnsureSameInstanceConsiderationIsNotAddedTest() {
      _considerations.Clear();
      var c = ConsiderationConstructor.Chebyshev("name", _considerations);
      var b1 = new BasicConsideration("b1", _considerations);
      Assert.That(c.AddConsideration(b1));
      Assert.That(c.AddConsideration(b1) == false);
    }

    [Test]
    public void EmptyOrNullConsiderationIdNotAcceptableTest() {
      _considerations.Clear();
      var c = ConsiderationConstructor.Chebyshev("name", _considerations);
      Assert.That(c.AddConsideration("") == false);
      Assert.That(c.AddConsideration((string)null) == false);
    }

    [Test]
    public void IfCollectionNullDontTryToAddNameIdConsiderationTest() {
      _considerations.Clear();
      var c = new CompositeConsideration();
      Assert.That(c.AddConsideration("some") == false);
    }

    [Test]
    public void SameNameIdIsNotAddedTest() {
      _considerations.Clear();
      var c = ConsiderationConstructor.Chebyshev("name", _considerations);
      var b1 = new BasicConsideration("b1", _considerations);
      Assert.That(c.AddConsideration("b1"));
      Assert.That(c.AddConsideration("b1") == false);
    }

    [Test]
    public void CloneHasSameNameIdTest() {
      _considerations.Clear();
      var c = ConsiderationConstructor.Chebyshev("name", _considerations);
      var cc = c.Clone();
      Assert.That(c.NameId == cc.NameId);
    }

    [Test]
    public void CloneHasCollectionTest() {
      _considerations.Clear();
      var c = ConsiderationConstructor.Chebyshev("name", _considerations);
      var cc = c.Clone() as ICompositeConsideration;
      var b1 = new BasicConsideration("b1", _considerations);
      Assert.That(c.AddConsideration("b1"));
      Assert.That(cc.AddConsideration("b1"));
    }

    [Test]
    public void EnsureCloneHasConsiderationsTest() {
      _considerations.Clear();
      var c = ConsiderationConstructor.Chebyshev("name", _considerations);
      var b1 = new BasicConsideration("b1", _considerations);
      var b2 = new BasicConsideration("b2", _considerations);
      c.AddConsideration("b1");
      c.AddConsideration("b2");
      var cc = c.Clone() as ICompositeConsideration;

      Assert.That(cc.AddConsideration("b1") == false);
      Assert.That(cc.AddConsideration("b2") == false);
    }

    [Test]
    public void CloneProducesSameUtilityTest([Range(0.0f, 1.0f, 0.1f)] float util) {
      _considerations.Clear();
      var c = ConsiderationConstructor.WeightedMetrics("name", _considerations, 3.0f);
      CustomContext context = new CustomContext();
      context.BaseUtility = new Utility(util, 0.8f);
      var b1 = new BasicConsideration("b1", _considerations);
      var b2 = new BasicConsideration("b2", _considerations);
      c.AddConsideration("b1");
      c.AddConsideration("b2");
      var cc = c.Clone() as ICompositeConsideration;

      c.Consider(context);
      cc.Consider(context);
      Assert.AreEqual(c.Utility.Value, cc.Utility.Value);
      Assert.AreEqual(c.Utility.Weight, cc.Utility.Weight);
    }

    const float Tolerance = 1e-6f;
  }

}