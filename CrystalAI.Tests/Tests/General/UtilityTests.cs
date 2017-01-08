// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// UtilityTests.cs is part of Crystal AI.
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


namespace Crystal.GeneralTests {

  [TestFixture]
  public class UtilityTests {
    static readonly object[] EqualsObjectTestCases = {
      new object[] {1f, 1f, 1f, 1f, true},
      new object[] {10f, 1f, 1f, 1f, true},
      new object[] {-1f, 1f, 0f, 1f, true},
      new object[] {1f, 1f, 0.5f, 1f, false},
      new object[] {-1f, -1f, -1f, 0f, true}
    };

    static readonly object[] CompareToTestCases = {
      new object[] {1f, 1f, 1f, 1f, 0},
      new object[] {0.5f, 1f, 1f, 1f, -1},
      new object[] {1f, 1f, 0.5f, 1f, 1},
      new object[] {0.5f, 1f, 1f, 0.5f, 0}
    };

    [Test]
    public void SingleParamConstructorTest([Range(-20.0f, 20.0f, 4.5023249f)] float value) {
      var cValue = value.Clamp01();
      var cUtil = new Utility(cValue);
      var aUtil = new Utility(value);
      Assert.AreEqual(cUtil, aUtil);
    }

    [Test]
    public void TwoParamConstructorTest(
      [Range(-1.4f, 1.4f, 0.300234234f)] float value,
      [Range(-1.4f, 1.4f, 0.300582962f)] float weight) {
      var cValue = value.Clamp01();
      var cWeight = weight.Clamp01();
      var cUtil = new Utility(cValue, cWeight);
      var aUtil = new Utility(value, weight);
      Assert.AreEqual(cUtil, aUtil);
    }

    [Test, TestCaseSource("EqualsObjectTestCases")]
    public void EqualsObjectTest(float v1, float w1, float v2, float w2, bool expected) {
      var u1 = new Utility(v1, w1);
      var u2 = (object)new Utility(v2, w2);
      Assert.That(u1.Equals(u2), Is.EqualTo(expected));
    }

    [Test]
    public void EqualsObjectNullFalseTest() {
      var u1 = new Utility();
      var u2 = (object)null;
      Assert.That(u1.Equals(u2), Is.False);
    }

    [Test]
    public void HashCodeTest() {
      var u1 = new Utility(0f, 1f);
      var u2 = new Utility(1f, 1f);
      Assert.That(u1.GetHashCode() != u2.GetHashCode());
    }

    [Test, TestCaseSource("CompareToTestCases")]
    public void CompareToTests(float v1, float w1, float v2, float w2, int expected) {
      var u1 = new Utility(v1, w1);
      var u2 = new Utility(v2, w2);
      Assert.That(u1.CompareTo(u2), Is.EqualTo(expected));
    }

    [Test]
    public void ToStringTest() {
      var u = new Utility();
      u.ToString();
    }

    [Test]
    public void SetValueTest([Range(-20.0f, 20.0f, 4.100239432934f)] float value) {
      var cValue = value.Clamp01();
      var cUtil = new Utility(cValue);
      Utility aUtil = value;
      // Since the parameterless constructor is "forbidden" in c# (at least as far as Microsofts VS is 
      // concerned, it is best if avoided to reduce compatibility issues.
      Assert.AreEqual(cUtil, aUtil);
    }

    [Test]
    public void SetWeightTest([Range(-20.0f, 20.0f, 4.100239432934f)] float weight) {
      var cWeight = weight.Clamp01();
      var cUtil = new Utility(0.5f, cWeight);
      var aUtil = new Utility(0.5f);
      aUtil.Weight = weight;
      Assert.AreEqual(cUtil, aUtil);
    }

    [Test]
    public void CombinedValueTest1(
      [Range(-1.4f, 1.4f, 0.280234234f)] float value,
      [Range(-1.4f, 1.4f, 0.280582962f)] float weight) {
      var cValue = value.Clamp01();
      var cWeight = weight.Clamp01();
      var cCombined = cValue * cWeight;
      var util = new Utility(value, weight);
      Assert.AreEqual(cCombined, util.Combined);
    }

    [Test]
    public void CombinedValueTest2(
      [Range(-1.4f, 1.4f, 0.280234234f)] float value,
      [Range(-1.4f, 1.4f, 0.280582962f)] float weight) {
      var cValue = value.Clamp01();
      var cWeight = weight.Clamp01();
      var cCombined = cValue * cWeight;
      var util = new Utility();
      util.Value = value;
      util.Weight = weight;
      Assert.AreEqual(cCombined, util.Combined);
    }

    [Test]
    public void IsZeroTest(
      [Range(-2.0f, 2.0f, 0.5f)] float value,
      [Range(-2.0f, 2.0f, 0.5f)] float weight) {
      var cValue = value.Clamp01();
      var cWeight = weight.Clamp01();
      var cResult = CrMath.AeqZero(cValue * cWeight);
      var util = new Utility(value, weight);
      var aResult = util.IsZero;
      Assert.AreEqual(cResult, aResult);
    }

    [Test]
    public void IsOneTest(
      [Range(-2.0f, 2.0f, 0.5f)] float value,
      [Range(-2.0f, 2.0f, 0.5f)] float weight) {
      var cValue = value.Clamp01();
      var cWeight = weight.Clamp01();
      var cResult = CrMath.AeqB(cValue * cWeight, 1.0f);
      var util = new Utility(value, weight);
      var aResult = util.IsOne;
      Assert.AreEqual(cResult, aResult);
    }

    [Test]
    public void ImplicitOperatorTest([Range(-10.0f, 10.0f, 3.38632823f)] float value) {
      var cValue = value.Clamp01();
      var cUtil = new Utility(cValue);
      Utility aUtil = value;
      Assert.AreEqual(cUtil, aUtil);
    }

    [Test]
    public void EqualityOperatorTest(
      [Range(0.0f, 1.0f, 0.5f)] float valueA,
      [Range(0.0f, 1.0f, 0.5f)] float weightA,
      [Range(0.0f, 1.0f, 0.5f)] float valueB,
      [Range(0.0f, 1.0f, 0.5f)] float weightB) {
      var cValueA = valueA.Clamp01();
      var cWeightA = weightA.Clamp01();
      var cValueB = valueB.Clamp01();
      var cWeightB = weightB.Clamp01();
      var cResult = CrMath.AeqB(cValueA, cValueB) && CrMath.AeqB(cWeightA, cWeightB);
      var utilA = new Utility(valueA, weightA);
      var utilB = new Utility(valueB, weightB);
      var aResult = utilA == utilB;
      Assert.AreEqual(cResult, aResult);
    }

    [Test]
    public void NotEqualOperatorTest(
      [Range(0.0f, 1.0f, 0.5f)] float valueA,
      [Range(0.0f, 1.0f, 0.5f)] float weightA,
      [Range(0.0f, 1.0f, 0.5f)] float valueB,
      [Range(0.0f, 1.0f, 0.5f)] float weightB) {
      var cValueA = valueA.Clamp01();
      var cWeightA = weightA.Clamp01();
      var cValueB = valueB.Clamp01();
      var cWeightB = weightB.Clamp01();
      var cResult = !(CrMath.AeqB(cValueA, cValueB) && CrMath.AeqB(cWeightA, cWeightB));
      var utilA = new Utility(valueA, weightA);
      var utilB = new Utility(valueB, weightB);
      var aResult = utilA != utilB;
      Assert.AreEqual(cResult, aResult);
    }

    [Test]
    public void GreaterThanOperatorTest(
      [Range(0.0f, 1.0f, 0.5f)] float valueA,
      [Range(0.0f, 1.0f, 0.5f)] float weightA,
      [Range(0.0f, 1.0f, 0.5f)] float valueB,
      [Range(0.0f, 1.0f, 0.5f)] float weightB) {
      var cValueA = valueA.Clamp01();
      var cWeightA = weightA.Clamp01();
      var cValueB = valueB.Clamp01();
      var cWeightB = weightB.Clamp01();
      var cResult = cValueA * cWeightA > cValueB * cWeightB;
      var utilA = new Utility(valueA, weightA);
      var utilB = new Utility(valueB, weightB);
      var aResult = utilA > utilB;
      Assert.AreEqual(cResult, aResult);
    }

    [Test]
    public void GreaterThanOrEqualOperatorTest(
      [Range(0.0f, 1.0f, 0.5f)] float valueA,
      [Range(0.0f, 1.0f, 0.5f)] float weightA,
      [Range(0.0f, 1.0f, 0.5f)] float valueB,
      [Range(0.0f, 1.0f, 0.5f)] float weightB) {
      var cValueA = valueA.Clamp01();
      var cWeightA = weightA.Clamp01();
      var cValueB = valueB.Clamp01();
      var cWeightB = weightB.Clamp01();
      var cResult = cValueA * cWeightA >= cValueB * cWeightB;
      var utilA = new Utility(valueA, weightA);
      var utilB = new Utility(valueB, weightB);
      var aResult = utilA >= utilB;
      Assert.AreEqual(cResult, aResult);
    }

    [Test]
    public void LessThanOperatorTest(
      [Range(0.0f, 1.0f, 0.5f)] float valueA,
      [Range(0.0f, 1.0f, 0.5f)] float weightA,
      [Range(0.0f, 1.0f, 0.5f)] float valueB,
      [Range(0.0f, 1.0f, 0.5f)] float weightB) {
      var cValueA = valueA.Clamp01();
      var cWeightA = weightA.Clamp01();
      var cValueB = valueB.Clamp01();
      var cWeightB = weightB.Clamp01();
      var cResult = cValueA * cWeightA < cValueB * cWeightB;
      var utilA = new Utility(valueA, weightA);
      var utilB = new Utility(valueB, weightB);
      var aResult = utilA < utilB;
      Assert.AreEqual(cResult, aResult);
    }

    [Test]
    public void LessThanOrEqualOperatorTest(
      [Range(0.0f, 1.0f, 0.5f)] float valueA,
      [Range(0.0f, 1.0f, 0.5f)] float weightA,
      [Range(0.0f, 1.0f, 0.5f)] float valueB,
      [Range(0.0f, 1.0f, 0.5f)] float weightB) {
      var cValueA = valueA.Clamp01();
      var cWeightA = weightA.Clamp01();
      var cValueB = valueB.Clamp01();
      var cWeightB = weightB.Clamp01();
      var cResult = cValueA * cWeightA <= cValueB * cWeightB;
      var utilA = new Utility(valueA, weightA);
      var utilB = new Utility(valueB, weightB);
      var aResult = utilA <= utilB;
      Assert.AreEqual(cResult, aResult);
    }
  }

}