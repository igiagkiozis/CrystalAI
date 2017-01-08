// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ConsiderationConstructorTests.cs is part of Crystal AI.
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


namespace Crystal.ConsiderationTests {

  [TestFixture]
  internal class ConsiderationConstructorTests {
    static readonly object[] MeasureInputConstructorTestCases = {
      new Chebyshev(), new ConstrainedChebyshev(),
      new WeightedMetrics(), new ConstrainedWeightedMetrics(),
      new MultiplicativePseudoMeasure()
    };

    [Test, TestCaseSource("MeasureInputConstructorTestCases")]
    public void MeasureInputConstructor(IMeasure measure) {
      var c = ConsiderationConstructor.Create(measure);
      Assert.IsNotNull(c);
      Assert.IsNotNull(c.Measure);
      Assert.That(c.Measure, Is.EqualTo(measure));
    }

    [Test, TestCaseSource("MeasureInputConstructorTestCases")]
    public void MeasureAndNameConstructor(IMeasure measure) {
      var cc = new ConsiderationCollection();
      var c = ConsiderationConstructor.Create("name", cc, measure);
      Assert.IsNotNull(c);
      Assert.IsNotNull(c.Measure);
      Assert.That(c.Measure, Is.EqualTo(measure));
      Assert.That(cc.Contains("name"));
    }

    [Test]
    public void ChebyshevConstructor() {
      var c = ConsiderationConstructor.Chebyshev();
      Assert.IsNotNull(c);
      Assert.IsNotNull(c.Measure);
      Assert.That(c.Measure is Chebyshev);
    }

    [Test]
    public void ChebyshevWithNameConstructor() {
      var cc = new ConsiderationCollection();
      var c = ConsiderationConstructor.Chebyshev("name", cc);
      Assert.IsNotNull(c);
      Assert.IsNotNull(c.Measure);
      Assert.That(c.Measure is Chebyshev);
      Assert.That(cc.Contains("name"));
    }

    [Test]
    public void WeightedMetricsConstructor() {
      var c = ConsiderationConstructor.WeightedMetrics();
      Assert.IsNotNull(c);
      Assert.IsNotNull(c.Measure);
      Assert.That(c.Measure is WeightedMetrics);
    }

    [Test]
    public void WeightedMetricsNameConstructor() {
      var cc = new ConsiderationCollection();
      var c = ConsiderationConstructor.WeightedMetrics("name", cc);
      Assert.IsNotNull(c);
      Assert.IsNotNull(c.Measure);
      Assert.That(c.Measure is WeightedMetrics);
      Assert.That(cc.Contains("name"));
    }

    [Test]
    public void ConstrainedChebyshevConstructor() {
      var c = ConsiderationConstructor.ConstrainedChebyshev();
      Assert.IsNotNull(c);
      Assert.IsNotNull(c.Measure);
      Assert.That(c.Measure is ConstrainedChebyshev);
    }

    [Test]
    public void ConstrainedChebyshevNameConstructor() {
      var cc = new ConsiderationCollection();
      var c = ConsiderationConstructor.ConstrainedChebyshev("name", cc);
      Assert.IsNotNull(c);
      Assert.IsNotNull(c.Measure);
      Assert.That(c.Measure is ConstrainedChebyshev);
      Assert.That(cc.Contains("name"));
    }

    [Test]
    public void ConstrainedChebyshevNameAndLowerBoundConstructor() {
      var cc = new ConsiderationCollection();
      var c = ConsiderationConstructor.ConstrainedChebyshev("name", cc, 0.5f);
      Assert.IsNotNull(c);
      Assert.IsNotNull(c.Measure);
      Assert.That(c.Measure is ConstrainedChebyshev);
      var m = c.Measure as ConstrainedChebyshev;
      Assert.That(m.LowerBound, Is.EqualTo(0.5f));
      Assert.That(cc.Contains("name"));
    }

    [Test]
    public void ConstrainedWeightedMetricsConstructor() {
      var c = ConsiderationConstructor.ConstrainedWeightedMetrics();
      Assert.IsNotNull(c);
      Assert.IsNotNull(c.Measure);
      Assert.That(c.Measure is ConstrainedWeightedMetrics);
    }

    [Test]
    public void ConstrainedWeightedMetricsNameConstructor() {
      var cc = new ConsiderationCollection();
      var c = ConsiderationConstructor.ConstrainedWeightedMetrics("name", cc, 3.0f, 0.5f);
      Assert.IsNotNull(c);
      Assert.IsNotNull(c.Measure);
      Assert.That(c.Measure is ConstrainedWeightedMetrics);
      var m = c.Measure as ConstrainedWeightedMetrics;
      Assert.That(m.LowerBound, Is.EqualTo(0.5f));
      Assert.That(m.PNorm, Is.EqualTo(3.0f));
      Assert.That(cc.Contains("name"));
    }

    [Test]
    public void MultiplicativeConstructor() {
      var c = ConsiderationConstructor.Multiplicative();
      Assert.IsNotNull(c);
      Assert.IsNotNull(c.Measure);
      Assert.That(c.Measure is MultiplicativePseudoMeasure);
    }

    [Test]
    public void MultiplicativeNameConstructor() {
      var cc = new ConsiderationCollection();
      var c = ConsiderationConstructor.Multiplicative("name", cc);
      Assert.IsNotNull(c);
      Assert.IsNotNull(c.Measure);
      Assert.That(c.Measure is MultiplicativePseudoMeasure);
    }
  }

}