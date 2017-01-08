// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// PriorityQueueTestBase.cs is part of Crystal AI.
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

  public abstract class PriorityQueueTestBase<TQueue> where TQueue : IPriorityQueue<Node, float> {
    [SetUp]
    public void SetUp() {
      Queue = CreateQueue();
    }

    [Test]
    public void HasNextTest() {
      var node1 = new Node(1);
      var node2 = new Node(2);

      Assert.That(Queue.HasNext, Is.False);

      Enqueue(node1);
      Enqueue(node2);

      Assert.That(Queue.HasNext, Is.True);
      Dequeue();
      Assert.That(Queue.HasNext, Is.True);
      Dequeue();
      Assert.That(Queue.HasNext, Is.False);
    }

    [Test]
    public void RemoveUsingPredicateTest() {
      Queue.Clear();
      var node1 = new Node(1);
      var node2 = new Node(2);
      var node3 = new Node(3);
      var node4 = new Node(4);

      Enqueue(node1);
      Enqueue(node2);
      Enqueue(node3);
      Enqueue(node4);

      // Removes the first item whose priority is > 2
      Queue.Remove(n => n.Priority > 2);
      Assert.That(Queue.Count, Is.EqualTo(3));

      Assert.That(Queue.Peek(), Is.EqualTo(node1));
      Queue.Remove(n => n.Priority >= 1);
      Assert.That(Queue.Peek(), Is.EqualTo(node2));
      Queue.Remove(n => n.Priority >= 4);
      Assert.That(Queue.Peek(), Is.EqualTo(node2));
    }

    [Test]
    public void SanityTest() {
      Node node1 = new Node(1);
      Node node2 = new Node(2);

      Assert.AreEqual(node1, node1);
      Assert.AreEqual(node2, node2);
      Assert.AreNotEqual(node1, node2);
    }

    [Test]
    public void TestCount() {
      Assert.AreEqual(0, Queue.Count);

      Enqueue(new Node(1));
      Assert.AreEqual(1, Queue.Count);

      Dequeue();
      Assert.AreEqual(0, Queue.Count);
    }

    [Test]
    public void TestFirst() {
      Node node1 = new Node(1);
      Node node2 = new Node(2);

      Enqueue(node1);
      Enqueue(node2);

      Assert.AreEqual(node1, Queue.Peek());
      Assert.AreEqual(node1, Dequeue());
      Assert.AreEqual(node2, Queue.Peek());
    }

    [Test]
    public void TestEnqueueWorksWithTwoNodesWithSamePriority() {
      Node node11 = new Node(1);
      Node node12 = new Node(1);

      Enqueue(node11);
      Enqueue(node12);

      Node firstNode = Dequeue();
      Node secondNode = Dequeue();

      //Assert we got the correct nodes, but since the queue might not be stable, the order doesn't matter
      Assert.IsTrue(firstNode == node11 && secondNode == node12 || firstNode == node12 && secondNode == node11);
    }

    [Test]
    public void TestSimpleQueue() {
      Node node1 = new Node(1);
      Node node2 = new Node(2);
      Node node3 = new Node(3);
      Node node4 = new Node(4);
      Node node5 = new Node(5);

      Enqueue(node2);
      Enqueue(node5);
      Enqueue(node1);
      Enqueue(node3);
      Enqueue(node4);

      Assert.AreEqual(node1, Dequeue());
      Assert.AreEqual(node2, Dequeue());
      Assert.AreEqual(node3, Dequeue());
      Assert.AreEqual(node4, Dequeue());
      Assert.AreEqual(node5, Dequeue());
    }

    [Test]
    public void TestForwardOrder() {
      Node node1 = new Node(1);
      Node node2 = new Node(2);
      Node node3 = new Node(3);
      Node node4 = new Node(4);
      Node node5 = new Node(5);

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
    public void TestBackwardOrder() {
      Node node1 = new Node(1);
      Node node2 = new Node(2);
      Node node3 = new Node(3);
      Node node4 = new Node(4);
      Node node5 = new Node(5);

      Enqueue(node5);
      Enqueue(node4);
      Enqueue(node3);
      Enqueue(node2);
      Enqueue(node1);

      Assert.AreEqual(node1, Dequeue());
      Assert.AreEqual(node2, Dequeue());
      Assert.AreEqual(node3, Dequeue());
      Assert.AreEqual(node4, Dequeue());
      Assert.AreEqual(node5, Dequeue());
    }

    [Test]
    public void TestAddingSameNodesLater() {
      Node node1 = new Node(1);
      Node node2 = new Node(2);
      Node node3 = new Node(3);
      Node node4 = new Node(4);
      Node node5 = new Node(5);

      Enqueue(node2);
      Enqueue(node5);
      Enqueue(node1);
      Enqueue(node3);
      Enqueue(node4);

      Assert.AreEqual(node1, Dequeue());
      Assert.AreEqual(node2, Dequeue());
      Assert.AreEqual(node3, Dequeue());
      Assert.AreEqual(node4, Dequeue());
      Assert.AreEqual(node5, Dequeue());

      Enqueue(node5);
      Enqueue(node3);
      Enqueue(node1);
      Enqueue(node2);
      Enqueue(node4);

      Assert.AreEqual(node1, Dequeue());
      Assert.AreEqual(node2, Dequeue());
      Assert.AreEqual(node3, Dequeue());
      Assert.AreEqual(node4, Dequeue());
      Assert.AreEqual(node5, Dequeue());
    }

    [Test]
    public void TestAddingDifferentNodesLater() {
      Node node1 = new Node(1);
      Node node2 = new Node(2);
      Node node3 = new Node(3);
      Node node4 = new Node(4);
      Node node5 = new Node(5);

      Enqueue(node2);
      Enqueue(node5);
      Enqueue(node1);
      Enqueue(node3);
      Enqueue(node4);

      Assert.AreEqual(node1, Dequeue());
      Assert.AreEqual(node2, Dequeue());
      Assert.AreEqual(node3, Dequeue());
      Assert.AreEqual(node4, Dequeue());
      Assert.AreEqual(node5, Dequeue());

      Node node6 = new Node(6);
      Node node7 = new Node(7);
      Node node8 = new Node(8);
      Node node9 = new Node(9);
      Node node10 = new Node(10);

      Enqueue(node6);
      Enqueue(node7);
      Enqueue(node8);
      Enqueue(node10);
      Enqueue(node9);

      Assert.AreEqual(node6, Dequeue());
      Assert.AreEqual(node7, Dequeue());
      Assert.AreEqual(node8, Dequeue());
      Assert.AreEqual(node9, Dequeue());
      Assert.AreEqual(node10, Dequeue());
    }

    [Test]
    public void TestClear() {
      Node node1 = new Node(1);
      Node node2 = new Node(2);
      Node node3 = new Node(3);
      Node node4 = new Node(4);
      Node node5 = new Node(5);

      Enqueue(node2);
      Enqueue(node5);
      Queue.Clear();
      Enqueue(node1);
      Enqueue(node3);
      Enqueue(node4);

      Assert.AreEqual(node1, Dequeue());
      Assert.AreEqual(node3, Dequeue());
      Assert.AreEqual(node4, Dequeue());
      Assert.AreEqual(0, Queue.Count);
    }

    protected void Enqueue(Node node) {
      Queue.Enqueue(node, node.Priority);
      Assert.IsTrue(IsValidQueue());
    }

    protected Node Dequeue() {
      Node returnMe = Queue.Dequeue();
      Assert.IsTrue(IsValidQueue());
      return returnMe;
    }

    protected abstract TQueue CreateQueue();
    protected abstract bool IsValidQueue();
    protected TQueue Queue { get; set; }
  }

}