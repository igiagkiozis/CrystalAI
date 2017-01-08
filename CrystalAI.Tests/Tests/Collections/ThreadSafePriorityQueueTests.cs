// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ThreadSafePriorityQueueTests.cs is part of Crystal AI.
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


namespace Crystal.CollectionsTests {

  [TestFixture]
  public class ThreadSafePriorityQueueTests : PriorityQueueTestBase<ThreadSafePriorityQueue<Node, float>> {
    [Test]
    public void SizeConstructorTest() {
      var q = new ThreadSafePriorityQueue<Node, float>(128);
      Assert.IsNotNull(q);
    }

    [Test]
    public void TestOrderedQueue() {
      Node node1 = new Node(1);
      Node node2 = new Node(1);
      Node node3 = new Node(1);
      Node node4 = new Node(1);
      Node node5 = new Node(1);

      Enqueue(node1);
      Enqueue(node2);
      Enqueue(node3);
      Enqueue(node4);
      Enqueue(node5);

      Assert.AreEqual(node1, Dequeue());
      Assert.AreEqual(node2, Dequeue());
      Assert.AreEqual(node3, Dequeue());
      Assert.AreEqual(node4, Dequeue());
      Assert.AreEqual(node5, Dequeue());
    }

    [Test]
    public void TestMoreComplicatedOrderedQueue() {
      Node node11 = new Node(1);
      Node node12 = new Node(1);
      Node node13 = new Node(1);
      Node node14 = new Node(1);
      Node node15 = new Node(1);
      Node node21 = new Node(2);
      Node node22 = new Node(2);
      Node node23 = new Node(2);
      Node node24 = new Node(2);
      Node node25 = new Node(2);
      Node node31 = new Node(3);
      Node node32 = new Node(3);
      Node node33 = new Node(3);
      Node node34 = new Node(3);
      Node node35 = new Node(3);
      Node node41 = new Node(4);
      Node node42 = new Node(4);
      Node node43 = new Node(4);
      Node node44 = new Node(4);
      Node node45 = new Node(4);
      Node node51 = new Node(5);
      Node node52 = new Node(5);
      Node node53 = new Node(5);
      Node node54 = new Node(5);
      Node node55 = new Node(5);

      Enqueue(node31);
      Enqueue(node51);
      Enqueue(node52);
      Enqueue(node11);
      Enqueue(node21);
      Enqueue(node22);
      Enqueue(node53);
      Enqueue(node41);
      Enqueue(node12);
      Enqueue(node32);
      Enqueue(node13);
      Enqueue(node42);
      Enqueue(node43);
      Enqueue(node44);
      Enqueue(node45);
      Enqueue(node54);
      Enqueue(node14);
      Enqueue(node23);
      Enqueue(node24);
      Enqueue(node33);
      Enqueue(node34);
      Enqueue(node55);
      Enqueue(node35);
      Enqueue(node25);
      Enqueue(node15);

      Assert.AreEqual(node11, Dequeue());
      Assert.AreEqual(node12, Dequeue());
      Assert.AreEqual(node13, Dequeue());
      Assert.AreEqual(node14, Dequeue());
      Assert.AreEqual(node15, Dequeue());
      Assert.AreEqual(node21, Dequeue());
      Assert.AreEqual(node22, Dequeue());
      Assert.AreEqual(node23, Dequeue());
      Assert.AreEqual(node24, Dequeue());
      Assert.AreEqual(node25, Dequeue());
      Assert.AreEqual(node31, Dequeue());
      Assert.AreEqual(node32, Dequeue());
      Assert.AreEqual(node33, Dequeue());
      Assert.AreEqual(node34, Dequeue());
      Assert.AreEqual(node35, Dequeue());
      Assert.AreEqual(node41, Dequeue());
      Assert.AreEqual(node42, Dequeue());
      Assert.AreEqual(node43, Dequeue());
      Assert.AreEqual(node44, Dequeue());
      Assert.AreEqual(node45, Dequeue());
      Assert.AreEqual(node51, Dequeue());
      Assert.AreEqual(node52, Dequeue());
      Assert.AreEqual(node53, Dequeue());
      Assert.AreEqual(node54, Dequeue());
      Assert.AreEqual(node55, Dequeue());
    }

    [Test]
    public void TestQueueAutomaticallyResizes() {
      for(int i = 0; i < 1000; i++) {
        Enqueue(new Node(i));
        Assert.AreEqual(i + 1, Queue.Count);
      }

      for(int i = 0; i < 1000; i++) {
        Node node = Dequeue();
        Assert.AreEqual(i, node.Priority);
      }
    }

    [Test]
    public void TestDequeueIsNullOnEmptyQueue() {
      Assert.IsNull(Queue.Dequeue());
    }

    [Test]
    public void TestDequeueIsNullOnEmptyQueue2() {
      Node node1 = new Node(1);
      Node node2 = new Node(2);

      Enqueue(node1);
      Enqueue(node2);

      Dequeue();
      Dequeue();
      Assert.IsNull(Queue.Dequeue());
    }

    [Test]
    public void TestPeekNullOnEmptyQueue() {
      Assert.IsNull(Queue.Peek());
    }

    [Test]
    public void TestPeekNullOnEmptyQueue2() {
      Node node1 = new Node(1);
      Node node2 = new Node(2);

      Enqueue(node1);
      Enqueue(node2);

      Dequeue();
      Dequeue();
      Assert.IsNull(Queue.Peek());
    }

    [Test]
    public void TestEnqueueRemovesOneCopyOfItem() {
      Node node = new Node(1);

      Enqueue(node);
      Enqueue(node);

      Assert.AreEqual(2, Queue.Count);
      Assert.IsTrue(Queue.Contains(node));

      Queue.Remove(node);

      Assert.AreEqual(1, Queue.Count);
      Assert.IsTrue(Queue.Contains(node));

      Queue.Remove(node);

      Assert.AreEqual(0, Queue.Count);
      Assert.IsFalse(Queue.Contains(node));
    }

    [Test]
    public void TestEnqueueRemovesFirstCopyOfItem() {
      Node node11 = new Node(1);
      Node node12 = new Node(1);

      Enqueue(node11);
      Enqueue(node12);
      Enqueue(node11);

      Assert.AreEqual(node11, Queue.Peek());

      Queue.Remove(node11);

      Assert.AreEqual(node12, Dequeue());
      Assert.AreEqual(node11, Dequeue());
      Assert.AreEqual(0, Queue.Count);
    }

    [Test]
    public void TestMultipleCopiesOfSameItem() {
      Node node1 = new Node(1);
      Node node21 = new Node(2);
      Node node22 = new Node(2);
      Node node3 = new Node(3);

      Enqueue(node1);
      Enqueue(node21);
      Enqueue(node22);
      Enqueue(node21);
      Enqueue(node22);
      Enqueue(node3);
      Enqueue(node3);
      Enqueue(node1);

      Assert.AreEqual(node1, Dequeue());
      Assert.AreEqual(node1, Dequeue());
      Assert.AreEqual(node21, Dequeue());
      Assert.AreEqual(node22, Dequeue());
      Assert.AreEqual(node21, Dequeue());
      Assert.AreEqual(node22, Dequeue());
      Assert.AreEqual(node3, Dequeue());
      Assert.AreEqual(node3, Dequeue());
    }

    [Test]
    public void TestEnqueuingNull() {
      Queue.Enqueue(null, 1);
      Assert.AreEqual(1, Queue.Count);
      Assert.AreEqual(null, Queue.Peek());
      Assert.IsTrue(Queue.Contains(null));
      Assert.IsFalse(Queue.Contains(new Node(1)));

      Assert.AreEqual(null, Dequeue());

      Assert.AreEqual(0, Queue.Count);
      Assert.IsFalse(Queue.Contains(null));
    }

    [Test]
    public void TestRemoveIsGracefulOnNodeNotInQueue() {
      Node node = new Node(1);

      Assert.DoesNotThrow(() => Queue.Remove(node));
    }

    [Test]
    public void TestUpdatePriorityIsGracefulOnNodeNotInQueue() {
      Node node = new Node(1);

      Assert.DoesNotThrow(() => Queue.UpdatePriority(node, 2));
    }

    protected override ThreadSafePriorityQueue<Node, float> CreateQueue() {
      return new ThreadSafePriorityQueue<Node, float>();
    }

    protected override bool IsValidQueue() {
      return Queue.IsBinaryHeapValid();
    }
  }

}