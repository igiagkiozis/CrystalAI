// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// MovingAverageTests.cs is part of Crystal AI.
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
using System;
using NUnit.Framework;


namespace Crystal.CollectionsTests {

  [TestFixture]
  public class MovingAverageTests {
    Pcg _rng;

    [OneTimeSetUp]
    public void Initialize() {
      _rng = new Pcg();
    }

    [Test]
    public void ConstructorTest() {
      var ma = new MovingAverage();
      Assert.IsNotNull(ma);
      Assert.AreEqual(4, ma.MovingAverageDepth);
    }

    [Test]
    public void MeanTest([Values(0.0f, 5.0f, -5.0f, 32.0f)] float targetMean, [Values(6, 10, 20, 40)] int bufferSize) {
      var ma = new MovingAverage(bufferSize);
      var count = 100 * bufferSize;
      // The expected error is more or less this, a bit on the generous side
      // with a lot of hand waving, but its good enough.
      var expError = 2f / (float)Math.Sqrt(ma.MovingAverageDepth);
      for(int i = 0; i < count; i++) {
        var val = (float)(_rng.NextDouble() - 0.5) + targetMean;
        ma.Enqueue(val);
        if(i > bufferSize)
          Assert.That(ma.Mean, Is.EqualTo(targetMean).Within(expError));
      }

      Console.WriteLine(expError);
    }
  }

}