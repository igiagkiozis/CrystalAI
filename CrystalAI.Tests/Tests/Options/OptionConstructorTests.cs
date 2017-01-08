// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// OptionConstructorTests.cs is part of Crystal AI.
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


namespace Crystal.OptionTests {

  [TestFixture]
  internal class OptionConstructorTests {
    IActionCollection _ac;
    IConsiderationCollection _cc;
    IOptionCollection _oc;

    [OneTimeSetUp]
    public void Initialize() {
      _ac = new ActionCollection();
      _cc = new ConsiderationCollection();
      _oc = new OptionCollection(_ac, _cc);
    }

    [Test]
    public void ChebyshevOptionInCollectionTest() {
      var o = OptionConstructor.Chebyshev("optionche", _oc);
      Assert.That(o.Measure is Chebyshev);
    }

    [Test]
    public void ChebyshevOptionTest() {
      var o = OptionConstructor.Chebyshev();
      Assert.That(o.Measure is Chebyshev);
    }

    [Test]
    public void ConstrainedChebyshevInCollectionTest() {
      var o = OptionConstructor.ConstrainedChebyshev("optconstche", _oc);
      Assert.That(o.Measure is ConstrainedChebyshev);
    }

    [Test]
    public void ConstrainedChebyshevTest() {
      var o = OptionConstructor.ConstrainedChebyshev();
      Assert.That(o.Measure is ConstrainedChebyshev);
    }

    [Test]
    public void ConstrainedWeightedMetricsInCollectionTest() {
      var o = OptionConstructor.ConstrainedWeightedMetrics("cwmo", _oc);
      Assert.That(o.Measure is ConstrainedWeightedMetrics);
    }

    [Test]
    public void ConstrainedWeightedMetricsTest() {
      var o = OptionConstructor.ConstrainedWeightedMetrics();
      Assert.That(o.Measure is ConstrainedWeightedMetrics);
    }

    [Test]
    public void WeightedMetricsInCollectionTest() {
      var o = OptionConstructor.WeightedMetrics("optionwc", _oc);
      Assert.That(o.Measure is WeightedMetrics);
      Assert.That(CrMath.AeqB(((WeightedMetrics)o.Measure).PNorm, 2.0f));
    }

    [Test]
    public void WeightedMetricsTest() {
      var o = OptionConstructor.WeightedMetrics();
      Assert.That(o.Measure is WeightedMetrics);
    }
  }

}