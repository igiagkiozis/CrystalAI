using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystal;
using NUnit.Framework;

namespace CrystalAI.CollectionTests {

  [TestFixture]
  class BinaryHeapTests {

    FloatHeapItemComparer _floatHeapComparer;

    [OneTimeSetUp]
    public void Initialize() {
      _floatHeapComparer = new FloatHeapItemComparer();
    }

    public class FloatHeapItemComparer : IComparer<FloatHeapItem> {
      /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
      /// <returns>Value Condition Less than zero<paramref name="x" /> is less than <paramref name="y" />.Zero<paramref name="x" /> equals <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.</returns>
      /// <param name="x">The first object to compare.</param>
      /// <param name="y">The second object to compare.</param>
      public int Compare(FloatHeapItem x, FloatHeapItem y) {
        return x.Value.CompareTo(y.Value);
      }
    }

    public class FloatHeapItem : IHeapItem<FloatHeapItem> {
      public float Value;
      
      IPriorityQueueHandle<FloatHeapItem> IHeapItem<FloatHeapItem>.Handle { get; set; }

      /// <summary>Returns a string that represents the current object.</summary>
      /// <returns>A string that represents the current object.</returns>
      public override string ToString() {
        return string.Format("Value : {0}", Value);
      }

      public FloatHeapItem(float value) {
        Value = value;
      }
    }
    

    [Test]
    public void DefaultConstructorTest() {
      var h = new BinaryHeap<FloatHeapItem>(_floatHeapComparer);
      Assert.IsNotNull(h);
    }

    [Test]
    public void PriorityQueueTests() {
      var h = new BinaryHeap<FloatHeapItem>(_floatHeapComparer);
      h.Enqueue(new FloatHeapItem(0f));
      h.Enqueue(new FloatHeapItem(1f));

      Assert.True(h.IsBinaryHeapValid());

      Assert.AreEqual(2, h.Count);
      Assert.True(h.IsBinaryHeapValid());

      Console.WriteLine(h.Dequeue().ToString());
      Console.WriteLine(h.Dequeue().ToString());

      Assert.True(h.IsBinaryHeapValid());

      Assert.AreEqual(0, h.Count);

      for(int i = 0; i < 1024; i++) {
        h.Enqueue(new FloatHeapItem((float)i));
        Assert.True(h.IsBinaryHeapValid());
      }

      Assert.AreEqual(1024, h.Count);
      Assert.True(h.IsBinaryHeapValid());

      h.Clear();
      Assert.True(h.IsBinaryHeapValid());

      Assert.AreEqual(0, h.Count);

      var i1 = new FloatHeapItem(0f);
      var i2 = new FloatHeapItem(0f);
      h.Enqueue(i1);
      h.Enqueue(i2);

      h.Remove(i2);
      Assert.AreEqual(1, h.Count);
      Assert.True(h.IsBinaryHeapValid());

      h.Remove(i1);
      Assert.AreEqual(0, h.Count);
      Assert.True(h.IsBinaryHeapValid());

      h.Enqueue(i1);
      h.Enqueue(i2);

      i1.Value = 1f;
      h.UpdatePriority(i1);
      Assert.True(h.IsBinaryHeapValid());
      Assert.That(h.Dequeue() == i2);
      Assert.That(h.Dequeue() == i1);
    }

  }

}
