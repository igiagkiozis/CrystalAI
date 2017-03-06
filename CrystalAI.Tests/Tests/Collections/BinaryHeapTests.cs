using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Crystal;
using NUnit.Framework;

namespace CrystalAI.CollectionTests {

  [TestFixture]
  class BinaryHeapTests {
    
    TestHeapItemComparer _testHeapComparer;
    BinaryHeap<TestHeapItem> _pq;
    const int N = 10000;

    [OneTimeSetUp]
    public void Initialize() {
      _testHeapComparer = new TestHeapItemComparer();
      _pq = new BinaryHeap<TestHeapItem>(_testHeapComparer);
    }

    public class TestHeapItemComparer : IComparer<TestHeapItem> {
      /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
      /// <returns>Value Condition Less than zero<paramref name="x" /> is less than <paramref name="y" />.Zero<paramref name="x" /> equals <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.</returns>
      /// <param name="x">The first object to compare.</param>
      /// <param name="y">The second object to compare.</param>
      public int Compare(TestHeapItem x, TestHeapItem y) {
        return x.Priority.CompareTo(y.Priority);
      }
    }

    public class TestHeapItem : IHeapItem<TestHeapItem> {      
      public float Value;
      public float Priority;

      IPriorityQueueHandle<TestHeapItem> IHeapItem<TestHeapItem>.Handle { get; set; }

      /// <summary>Returns a string that represents the current object.</summary>
      /// <returns>A string that represents the current object.</returns>
      public override string ToString() {
        return string.Format("Value : {0}, Priority : {1}", Value, Priority);
      }

      public TestHeapItem() {
        Value = 0f;
        Priority = 0f;
      }

      public TestHeapItem(float value, float priority) {
        Value = value;
        Priority = priority;
      }
    }
    

    [Test]
    public void DefaultConstructorTest() {
      var h = new BinaryHeap<TestHeapItem>();
      Assert.IsNotNull(h);
    }

    [Test]
    public void ComparerOnlyConstructorTest() {
      var h = new BinaryHeap<TestHeapItem>(_testHeapComparer);
      Assert.IsNotNull(h);
    }

    [Test]
    public void TwoParamConstructorTest() {
      var h = new BinaryHeap<TestHeapItem>(100, _testHeapComparer);
      Assert.IsNotNull(h);
    }

    [Test]
    public void EnqueueDequeueTest() {
      _pq.Clear();
      var item1 = new TestHeapItem(0f, 0f);
      var item2 = new TestHeapItem(0f, 2f);
      var item3 = new TestHeapItem(0f, 5f);
      var item4 = new TestHeapItem(0f, 4f);
      var item5 = new TestHeapItem(0f, 8f);

      _pq.Enqueue(item3);
      _pq.Enqueue(item2);
      _pq.Enqueue(item1);
      _pq.Enqueue(item4);
      _pq.Enqueue(item5);

      Assert.AreEqual(5, _pq.Count);
      Assert.True(_pq.IsBinaryHeapValid());

      Assert.AreEqual(item1, _pq.Dequeue());
      Assert.AreEqual(item2, _pq.Peek());
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.AreEqual(4, _pq.Count);
      Assert.True(_pq.HasNext);

      Assert.AreEqual(item2, _pq.Dequeue());
      Assert.AreEqual(item4, _pq.Peek());
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.AreEqual(3, _pq.Count);
      Assert.True(_pq.HasNext);

      Assert.AreEqual(item4, _pq.Dequeue());
      Assert.AreEqual(item3, _pq.Peek());
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.AreEqual(2, _pq.Count);
      Assert.True(_pq.HasNext);

      Assert.AreEqual(item3, _pq.Dequeue());
      Assert.AreEqual(item5, _pq.Peek());
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.AreEqual(1, _pq.Count);
      Assert.True(_pq.HasNext);

      Assert.AreEqual(item5, _pq.Dequeue());
      Assert.AreEqual(null, _pq.Peek());
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.AreEqual(0, _pq.Count);
      Assert.False(_pq.HasNext);
    }

    [Test]
    public void EnqueueDequeueOrderTest() {
      _pq.Clear();
      var item1 = new TestHeapItem(0f, 0f);
      var item2 = new TestHeapItem(0f, 0f);
      var item3 = new TestHeapItem(0f, 1f);
      var item4 = new TestHeapItem(0f, 1f);
      var item5 = new TestHeapItem(0f, 1f);

      _pq.Enqueue(item1);
      _pq.Enqueue(item2);
      _pq.Enqueue(item3);
      _pq.Enqueue(item4);
      _pq.Enqueue(item5);

      Assert.AreEqual(5, _pq.Count);
      Assert.True(_pq.IsBinaryHeapValid());

      Assert.AreEqual(item1, _pq.Dequeue());
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.AreEqual(4, _pq.Count);
      Assert.True(_pq.HasNext);

      Assert.AreEqual(item2, _pq.Dequeue());
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.AreEqual(3, _pq.Count);
      Assert.True(_pq.HasNext);

      Assert.AreEqual(item3, _pq.Dequeue());
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.AreEqual(2, _pq.Count);
      Assert.True(_pq.HasNext);

      Assert.AreEqual(item4, _pq.Dequeue());
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.AreEqual(1, _pq.Count);
      Assert.True(_pq.HasNext);

      Assert.AreEqual(item5, _pq.Dequeue());
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.AreEqual(0, _pq.Count);
      Assert.False(_pq.HasNext);
    }

    [Test]
    public void EnqueueDequeueLargeScaleTest() {
      _pq.Clear();
      var itemList = new List<TestHeapItem>();
      for(int i = 0; i < N; i++) {
        itemList.Add(new TestHeapItem(0f, i));
      }

      for(int i = N - 1; i >= 0; i--) {
        _pq.Enqueue(itemList[i]);        
      }
      Assert.True(_pq.IsBinaryHeapValid());

      int count = 0;
      while(_pq.HasNext) {
        Assert.AreEqual(itemList[count], _pq.Dequeue());
        if(count == N / 2) // Good as any point
          Assert.True(_pq.IsBinaryHeapValid());
        count++;
      }
      Assert.True(_pq.IsBinaryHeapValid());
    }

    [Test]
    public void ContainsTest() {
      _pq.Clear();
      var item1 = new TestHeapItem(0f, 0f);
      var item2 = new TestHeapItem(0f, 1f);
      var item3 = new TestHeapItem(0f, 2f);
      var item4 = new TestHeapItem(0f, 3f);
      var item5 = new TestHeapItem(0f, 4f);
      var item6 = new TestHeapItem(11f, 100f);

      _pq.Enqueue(item3);
      _pq.Enqueue(item2);
      _pq.Enqueue(item1);
      _pq.Enqueue(item4);
      _pq.Enqueue(item5);


      Assert.True(_pq.Contains(item1));
      Assert.True(_pq.Contains(item2));
      Assert.True(_pq.Contains(item3));
      Assert.True(_pq.Contains(item4));
      Assert.True(_pq.Contains(item5));
      Assert.False(_pq.Contains(item6));

      Assert.AreEqual(5, _pq.Count);
      Assert.True(_pq.IsBinaryHeapValid());

      Assert.AreEqual(item1, _pq.Dequeue());
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.False(_pq.Contains(item1));

      Assert.AreEqual(item2, _pq.Dequeue());
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.False(_pq.Contains(item2));
      
      Assert.AreEqual(item3, _pq.Dequeue());
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.False(_pq.Contains(item3));

      Assert.AreEqual(item4, _pq.Dequeue());
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.False(_pq.Contains(item4));

      Assert.AreEqual(item5, _pq.Dequeue());
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.False(_pq.Contains(item5));
    }

    [Test]
    public void RemoveTest() {
      _pq.Clear();
      var item1 = new TestHeapItem(0f, 0f);
      var item2 = new TestHeapItem(0f, 1f);
      var item3 = new TestHeapItem(0f, 2f);
      var item4 = new TestHeapItem(0f, 3f);
      var item5 = new TestHeapItem(0f, 4f);

      _pq.Enqueue(item3);
      _pq.Enqueue(item2);
      _pq.Enqueue(item1);
      _pq.Enqueue(item4);
      _pq.Enqueue(item5);
      
      Assert.AreEqual(5, _pq.Count);
      Assert.True(_pq.IsBinaryHeapValid());

      _pq.Remove(item1);
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.False(_pq.Contains(item1));

      _pq.Remove(item2);
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.False(_pq.Contains(item2));

      _pq.Remove(item3);
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.False(_pq.Contains(item3));

      _pq.Remove(item4);
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.False(_pq.Contains(item4));

      _pq.Remove(item5);
      Assert.True(_pq.IsBinaryHeapValid());
      Assert.False(_pq.Contains(item5));
    }

    [Test]
    public void UpdatePriorityTest() {
      _pq.Clear();
      var item1 = new TestHeapItem(0f, 0f);
      var item2 = new TestHeapItem(0f, 1f);
      var item3 = new TestHeapItem(0f, 2f);
      var item4 = new TestHeapItem(0f, 3f);
      var item5 = new TestHeapItem(0f, 4f);

      _pq.Enqueue(item3);
      _pq.Enqueue(item2);
      _pq.Enqueue(item1);
      _pq.Enqueue(item4);
      _pq.Enqueue(item5);

      Assert.AreEqual(item1, _pq.Peek());

      item5.Priority = -1f;
      _pq.UpdatePriority(item5);
      Assert.AreEqual(item5, _pq.Peek());

      item4.Priority = -2f;
      _pq.UpdatePriority(item4);
      Assert.AreEqual(item4, _pq.Peek());

      item3.Priority = -3f;
      _pq.UpdatePriority(item3);
      Assert.AreEqual(item3, _pq.Peek());

      item2.Priority = -4f;
      _pq.UpdatePriority(item2);
      Assert.AreEqual(item2, _pq.Peek());

      item1.Priority = -5f;
      _pq.UpdatePriority(item1);
      Assert.AreEqual(item1, _pq.Peek());
    }

    [Test]
    public void UpdatePriorityLargeScaleTest() {
      _pq.Clear();
      var itemList = new List<TestHeapItem>();
      var rnd = new Random();
      for(int i = 0; i < N; i++) {
        itemList.Add(new TestHeapItem(rnd.Next(), i));
        _pq.Enqueue(itemList[i]);
      }

      float priority = -1f;
      for(int i = N - 1; i >= 0; i--) {
        var item = itemList[i];
        item.Priority = priority;
        _pq.UpdatePriority(item);
        Assert.AreEqual(item, _pq.Peek());
        priority--;
      }
    }

    [Test]
    public void EnqueueRemoveBenchmark() {
      //prevent the JIT Compiler from optimizing Fkt calls away
      long seed = Environment.TickCount;

      //use the second Core/Processor for the test
      Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2);

      //prevent "Normal" Processes from interrupting Threads
      Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

      //prevent "Normal" Threads from interrupting this thread
      Thread.CurrentThread.Priority = ThreadPriority.Highest;


      Stopwatch sw = new Stopwatch();

      _pq.Clear();
      var itemList = new List<TestHeapItem>();
      var rnd = new Random();

      for(int i = 0; i < N; i++) {
        itemList.Add(new TestHeapItem(rnd.Next(), 100f * (float)rnd.NextDouble()));
      }

      sw.Reset();
      sw.Start();
      for(int i = 0; i < N; i++) {
        _pq.Enqueue(itemList[i]);
      }
      sw.Stop();
      Console.WriteLine("New Enqueue : {0}", sw.Elapsed.TotalMilliseconds);

      sw.Reset();
      sw.Start();
      for(int i = 0; i < N; i++) {
        _pq.Remove(itemList[i]);
      }
      sw.Stop();
      Console.WriteLine("New Remove : {0}", sw.Elapsed.TotalMilliseconds);

      
      var pqo = new PriorityQueueDEPRECATED<TestHeapItem, float>();
      sw.Reset();
      sw.Start();
      for(int i = 0; i < N; i++) {
        pqo.Enqueue(itemList[i], itemList[i].Priority);
      }
      sw.Stop();
      Assert.AreEqual(N, pqo.Count);
      Console.WriteLine("Old Enqueue : {0}", sw.Elapsed.TotalMilliseconds);

      sw.Reset();
      sw.Start();
      for(int i = 0; i < N; i++) {
        pqo.Remove(itemList[i]);
      }
      sw.Stop();
      Console.WriteLine("Old Remove : {0}", sw.Elapsed.TotalMilliseconds);
    }

    [Test]
    public void TempBenchmarkTest() {
      //prevent the JIT Compiler from optimizing Fkt calls away
      long seed = Environment.TickCount;

      //use the second Core/Processor for the test
      Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2);

      //prevent "Normal" Processes from interrupting Threads
      Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

      //prevent "Normal" Threads from interrupting this thread
      Thread.CurrentThread.Priority = ThreadPriority.Highest;


      Stopwatch sw = new Stopwatch();

      _pq.Clear();
      var itemList = new List<TestHeapItem>();
      var rnd = new Random();

      for(int i = 0; i < N; i++) {
        itemList.Add(new TestHeapItem(rnd.Next(), 100f * (float)rnd.NextDouble()));
      }

      sw.Reset();
      sw.Start();
      for(int i = 0; i < N; i++) {
        _pq.Enqueue(itemList[i]);
      }
      sw.Stop();
      Console.WriteLine("New Enqueue : {0}", sw.Elapsed.TotalMilliseconds);

//      float priority = -1f;
//      sw.Reset();
//      sw.Start();
//      for(int i = N - 1; i >= 0; i--) {
//        var item = itemList[i];
//        item.Priority = priority;
//        _pq.UpdatePriority(item);
//        Assert.AreEqual(item, _pq.Peek());
//        priority--;
//      }
//      sw.Stop();
//      Console.WriteLine("New UpdatePriority : {0}", sw.Elapsed.TotalMilliseconds);

      sw.Reset();
      sw.Start();
      for(int i = 0; i < N; i++) {
        _pq.Remove(itemList[i]);
      }
      sw.Stop();
      Console.WriteLine("New Remove : {0}", sw.Elapsed.TotalMilliseconds);


//      for(int i = 0; i < N; i++) {
//        itemList[i].Priority = i;
//      }
      var pqo = new PriorityQueueDEPRECATED<TestHeapItem, float>();

      sw.Reset();
      sw.Start();
      for(int i = 0; i < N; i++) {
        pqo.Enqueue(itemList[i], itemList[i].Priority);
      }
      sw.Stop();
      Assert.AreEqual(N, pqo.Count);
      Console.WriteLine("Old Enqueue : {0}", sw.Elapsed.TotalMilliseconds);

//      priority = -1f;
//      sw.Reset();
//      sw.Start();
//      for(int i = N - 1; i >= 0; i--) {
//        var item = itemList[i];
//        item.Priority = priority;
//        pqo.UpdatePriority(item, priority);
//        Assert.AreEqual(item, pqo.Peek());
//        priority--;
//      }
//      sw.Stop();
//      Console.WriteLine("Old UpdatePriority : {0}", sw.Elapsed.TotalMilliseconds);

      Console.WriteLine(pqo.Count);

      sw.Reset();
      sw.Start();
      for(int i = 0; i < N; i++) {
        pqo.Remove(itemList[i]);
      }
      sw.Stop();
      Console.WriteLine("Old Remove : {0}", sw.Elapsed.TotalMilliseconds);

    }


//    [Test]
//    public void PriorityQueueTests() {
//      var h = new BinaryHeap<TestHeapItem>(_testHeapComparer);
//      h.Enqueue(new TestHeapItem(0f));
//      h.Enqueue(new TestHeapItem(1f));
//
//      Assert.True(h.IsBinaryHeapValid());
//
//      Assert.AreEqual(2, h.Count);
//      Assert.True(h.IsBinaryHeapValid());
//
//      Console.WriteLine(h.Dequeue().ToString());
//      Console.WriteLine(h.Dequeue().ToString());
//
//      Assert.True(h.IsBinaryHeapValid());
//
//      Assert.AreEqual(0, h.Count);
//
//      for(int i = 0; i < 1024; i++) {
//        h.Enqueue(new TestHeapItem((float)i));
//        Assert.True(h.IsBinaryHeapValid());
//      }
//
//      Assert.AreEqual(1024, h.Count);
//      Assert.True(h.IsBinaryHeapValid());
//
//      h.Clear();
//      Assert.True(h.IsBinaryHeapValid());
//
//      Assert.AreEqual(0, h.Count);
//
//      var i1 = new TestHeapItem(0f);
//      var i2 = new TestHeapItem(0f);
//      h.Enqueue(i1);
//      h.Enqueue(i2);
//
//      h.Remove(i2);
//      Assert.AreEqual(1, h.Count);
//      Assert.True(h.IsBinaryHeapValid());
//
//      h.Remove(i1);
//      Assert.AreEqual(0, h.Count);
//      Assert.True(h.IsBinaryHeapValid());
//
//      h.Enqueue(i1);
//      h.Enqueue(i2);
//
//      i1.Value = 1f;
//      h.UpdatePriority(i1);
//      Assert.True(h.IsBinaryHeapValid());
//      Assert.That(h.Dequeue() == i2);
//      Assert.That(h.Dequeue() == i1);
//    }

  }

}
