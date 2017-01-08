// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// CircularBufferTests.cs is part of Crystal AI.
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
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;


namespace Crystal.CollectionsTests {

  [TestFixture]
  public class CircularBufferTests {
    Pcg _rng;

    [OneTimeSetUp]
    public void Initialize() {
      _rng = new Pcg();
    }

    [Test]
    public void DefaultConstructorTest() {
      var cb = new CircularBuffer<int>();
      Assert.That(cb.Count == 0);
      Assert.That(cb.Capacity == 4);
    }

    [Test]
    public void InvalidSizeConstructorInitializesWithDefaultSizeTest() {
      var cb = new CircularBuffer<int>(-1);
      Assert.That(cb.Capacity == 4);
    }

    [Test]
    public void SetCapacityTest() {
      var cb = new CircularBuffer<int>(4);
      Assert.That(cb.Capacity == 4);
      cb.Capacity = 2;
      Assert.That(cb.Capacity == 2);
    }

    [Test]
    public void SetCapacityNegativeIsIgnoredTest() {
      var cb = new CircularBuffer<int>(4);
      Assert.That(cb.Capacity == 4);
      cb.Capacity = -1;
      Assert.That(cb.Capacity == 4);
    }

    [Test]
    public void SetCapacitySameDoesNothingTest() {
      var cb = new CircularBuffer<int>(2);
      cb.Enqueue(1);
      cb.Enqueue(2);
      Assert.That(cb.Capacity == 2);
      cb.Capacity = 2;
      Assert.That(cb[0] == 1);
      Assert.That(cb[1] == 2);
    }

    [Test]
    public void SetLargerCapacityIsNonDestructiveTest() {
      var cb = new CircularBuffer<int>(2);
      cb.Enqueue(1);
      cb.Enqueue(2);
      Assert.That(cb.Capacity == 2);
      Assert.That(cb.Count == 2);
      cb.Capacity = 3;
      Assert.That(cb.Count == 2);
      Assert.That(cb[0] == 1);
      Assert.That(cb[1] == 2);
    }

    [Test]
    public void DequeueWhenZeroCountReturnsDefaultTest() {
      var cb = new CircularBuffer<int>();
      Assert.That(cb.Dequeue() == default(int));
    }

    [Test]
    public void ClearTest() {
      var cb = new CircularBuffer<int>(2);
      cb.Enqueue(1);
      cb.Enqueue(2);
      Assert.That(cb.Count == 2);
      Assert.That(cb.Capacity == 2);
      cb.Clear();
      Assert.That(cb.Capacity == 2);
      Assert.That(cb.Count == 0);
    }

    [Test]
    public void IndexGetterThrowsIfOutOfBoundsTest() {
      var cb = new CircularBuffer<int>(2);
      cb.Enqueue(1);
      cb.Enqueue(2);
      Assert.Throws<ArgumentOutOfRangeException>(() => {
        var a = cb[2];
      });
    }

    [Test]
    public void IndexSetterThrowsIfOutOfBoundsTest() {
      var cb = new CircularBuffer<int>(2);
      cb.Enqueue(1);
      cb.Enqueue(2);
      Assert.Throws<ArgumentOutOfRangeException>(() => { cb[2] = 5; });
    }

    [Test]
    public void IndexOfNonExistentReturnsMinusOneTest() {
      var cb = new CircularBuffer<int>(2);
      cb.Enqueue(1);
      cb.Enqueue(2);
      Assert.That(cb.IndexOf(1) == 0);
      Assert.That(cb.IndexOf(2) == 1);
      Assert.That(cb.IndexOf(5) == -1);
    }

    [Test]
    public void IterationTest() {
      var cb = new CircularBuffer<int>(2);
      cb.Enqueue(1);
      cb.Enqueue(2);
      IEnumerator enumerator = cb.GetEnumerator();
      while(enumerator.MoveNext()) {
        var tmp = enumerator.Current;
      }
    }

    [Test]
    public void SetSmallerCapacityDestroysOnlyTailsElementsTest() {
      var cb = new CircularBuffer<int>(2);
      cb.Enqueue(1);
      cb.Enqueue(2);
      Assert.That(cb.Capacity == 2);
      Assert.That(cb.Count == 2);
      cb.Capacity = 1;
      Assert.That(cb.Count == 1);
      Assert.That(cb[0] == 1);
    }

    [Test]
    public void OverwriteTest() {
      var buffer = new CircularBuffer<long>(3);
      Assert.AreEqual(default(long), buffer.Enqueue(1));
      Assert.AreEqual(default(long), buffer.Enqueue(2));
      Assert.AreEqual(default(long), buffer.Enqueue(3));
      Assert.AreEqual(1, buffer.Enqueue(4));
      Assert.AreEqual(3, buffer.Count);
      Assert.AreEqual(2, buffer.Dequeue());
      Assert.AreEqual(3, buffer.Dequeue());
      Assert.AreEqual(4, buffer.Dequeue());
      Assert.AreEqual(0, buffer.Count);
    }

    [Test]
    public void UnderwriteTest() {
      var buffer = new CircularBuffer<long>(5);
      Assert.AreEqual(default(long), buffer.Enqueue(1));
      Assert.AreEqual(default(long), buffer.Enqueue(2));
      Assert.AreEqual(default(long), buffer.Enqueue(3));
      Assert.AreEqual(3, buffer.Count);
      Assert.AreEqual(1, buffer.Dequeue());
      Assert.AreEqual(2, buffer.Dequeue());
      Assert.AreEqual(3, buffer.Dequeue());
      Assert.AreEqual(0, buffer.Count);
    }

    [Test]
    public void IncreaseCapacityWhenFullTest() {
      var buffer = new CircularBuffer<long>(3);
      Assert.AreEqual(default(long), buffer.Enqueue(1));
      Assert.AreEqual(default(long), buffer.Enqueue(2));
      Assert.AreEqual(default(long), buffer.Enqueue(3));
      Assert.AreEqual(3, buffer.Count);
      buffer.Capacity = 4;
      Assert.AreEqual(3, buffer.Count);
      Assert.AreEqual(1, buffer.Dequeue());
      Assert.AreEqual(2, buffer.Dequeue());
      Assert.AreEqual(3, buffer.Dequeue());
      Assert.AreEqual(0, buffer.Count);
    }

    [Test]
    public void DecreaseCapacityWhenFullTest() {
      var buffer = new CircularBuffer<long>(3);
      Assert.AreEqual(default(long), buffer.Enqueue(1));
      Assert.AreEqual(default(long), buffer.Enqueue(2));
      Assert.AreEqual(default(long), buffer.Enqueue(3));
      Assert.AreEqual(3, buffer.Count);
      buffer.Capacity = 2;
      Assert.AreEqual(2, buffer.Count);
      Assert.AreEqual(1, buffer.Dequeue());
      Assert.AreEqual(2, buffer.Dequeue());
      Assert.AreEqual(0, buffer.Count);
    }

    [Test]
    public void EnumerationWhenFullTest() {
      var buffer = new CircularBuffer<long>(3);
      Assert.AreEqual(default(long), buffer.Enqueue(1));
      Assert.AreEqual(default(long), buffer.Enqueue(2));
      Assert.AreEqual(default(long), buffer.Enqueue(3));
      var i = 0;
      foreach(var value in buffer)
        Assert.AreEqual(++i, value);

      Assert.AreEqual(i, 3);
    }

    [Test]
    public void EnumerationWhenPartiallyFullTest() {
      var buffer = new CircularBuffer<long>(3);
      Assert.AreEqual(default(long), buffer.Enqueue(1));
      Assert.AreEqual(default(long), buffer.Enqueue(2));
      var i = 0;
      foreach(var value in buffer)
        Assert.AreEqual(++i, value);

      Assert.AreEqual(i, 2);
    }

    [Test]
    public void EnumerationWhenEmptyTest() {
      var buffer = new CircularBuffer<long>(3);
      foreach(var value in buffer)
        Assert.Fail("Unexpected Value: " + value);
    }

    [Test]
    public void RemoveAtTest() {
      var buffer = new CircularBuffer<long>(5);
      Assert.AreEqual(default(long), buffer.Enqueue(1));
      Assert.AreEqual(default(long), buffer.Enqueue(2));
      Assert.AreEqual(default(long), buffer.Enqueue(3));
      Assert.AreEqual(default(long), buffer.Enqueue(4));
      Assert.AreEqual(default(long), buffer.Enqueue(5));
      buffer.RemoveAt(buffer.IndexOf(2));
      buffer.RemoveAt(buffer.IndexOf(4));
      Assert.AreEqual(3, buffer.Count);
      Assert.AreEqual(1, buffer.Dequeue());
      Assert.AreEqual(3, buffer.Dequeue());
      Assert.AreEqual(5, buffer.Dequeue());
      Assert.AreEqual(0, buffer.Count);
      Assert.AreEqual(default(long), buffer.Enqueue(1));
      Assert.AreEqual(default(long), buffer.Enqueue(2));
      Assert.AreEqual(default(long), buffer.Enqueue(3));
      Assert.AreEqual(default(long), buffer.Enqueue(4));
      Assert.AreEqual(default(long), buffer.Enqueue(5));
      buffer.RemoveAt(buffer.IndexOf(1));
      buffer.RemoveAt(buffer.IndexOf(3));
      buffer.RemoveAt(buffer.IndexOf(5));
      Assert.AreEqual(2, buffer.Count);
      Assert.AreEqual(2, buffer.Dequeue());
      Assert.AreEqual(4, buffer.Dequeue());
      Assert.AreEqual(0, buffer.Count);
    }

    [Test]
    public void RemoveAtInvalidThrowsTest() {
      var cb = new CircularBuffer<int>();
      cb.Enqueue(1);
      cb.Enqueue(2);
      cb.Enqueue(3);
      cb.Enqueue(4);
      Assert.Throws<ArgumentOutOfRangeException>(() => cb.RemoveAt(4));
    }

    [Test]
    public void BehaviourTest() {
      var buffer = new CircularBuffer<float>(4);
      buffer.Enqueue(0.1f);
      buffer.Enqueue(0.2f);
      buffer.Enqueue(0.3f);
      buffer.Enqueue(0.4f);
      Assert.AreEqual(0.1f, buffer[0]);
      Assert.AreEqual(0.1f, buffer.Tail);
      buffer.Enqueue(0.5f);
      Assert.AreEqual(0.2f, buffer[0]);
      Assert.AreEqual(0.2f, buffer.Tail);
      buffer.Enqueue(0.6f);
      Assert.AreEqual(0.3f, buffer[0]);
      Assert.AreEqual(0.3f, buffer.Tail);
    }

    [Test, Repeat(20)]
    public void HeadTest([Range(1, 15, 1)] int bufferSize) {
      var buffer = new CircularBuffer<int>(bufferSize);
      for(int i = 0; i < 100 * bufferSize; i++) {
        var val = _rng.Next();
        buffer.Enqueue(val);
        Assert.AreEqual(val, buffer.Head);
      }
    }

    [Test, Repeat(20)]
    public void TailTest([Range(1, 15, 1)] int bufferSize) {
      var list = new List<int>();
      var buffer = new CircularBuffer<int>(bufferSize);
      for(int i = 0; i < 100 * bufferSize; i++) {
        var val = _rng.Next();
        int cVal = 0;
        list.Add(val);
        if(i < bufferSize)
          cVal = list[0];
        else
          cVal = list[list.Count - bufferSize];
        buffer.Enqueue(val);
        Assert.AreEqual(cVal, buffer.Tail);
      }
    }
  }

}