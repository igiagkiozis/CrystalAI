// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// PriorityQueue.cs is part of Crystal AI.
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
using System.Collections.Generic;


namespace Crystal {

  // The initial version of this file was based on https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp.git

  public class PriorityQueueNode<TPriority> {
    /// <summary>
    ///   The Priority to insert this node at.  Must be set BEFORE adding a node to the queue
    ///   (ideally just once, in the node's constructor).
    ///   Should not be manually edited once the node has been enqueued - use queue.UpdatePriority()
    ///   instead
    /// </summary>
    public TPriority Priority { get; protected internal set; }

    /// <summary>
    ///   Represents the current position in the queue
    /// </summary>
    public int QueueIndex { get; internal set; }

    /// <summary>
    ///   Represents the order the node was inserted in
    /// </summary>
    public long InsertionIndex { get; internal set; }
  }


  public class PriorityQueue<TItem, TPriority> : IPriorityQueue<TItem, TPriority>
    where TPriority : IComparable<TPriority> {
    QueueNode[] _nodes;
    int _numNodes;
    long _numNodesEverEnqueued;

    /// <summary>
    ///   Returns true if there is an element at the head of the queue, i.e. if the queue is not
    ///   empty.
    /// </summary>
    /// <value>true</value>
    /// <c>false</c>
    public bool HasNext {
      get { return Count > 0; }
    }

    /// <summary>
    ///   Returns the number of nodes in the queue.
    ///   O(1)
    /// </summary>
    public int Count {
      get { return _numNodes; }
    }

    /// <summary>
    ///   Verifies if the heap is in a valid state. Use only for debuging.
    /// </summary>
    /// <returns><c>true</c> if this instance is binary heap valid; otherwise, <c>false</c>.</returns>
    public bool IsBinaryHeapValid() {
      for(int i = 1; i < _nodes.Length; i++)
        if(_nodes[i] != null) {
          int childLeftIndex = 2 * i;
          if(childLeftIndex < _nodes.Length &&
             _nodes[childLeftIndex] != null &&
             HasHigherPriority(_nodes[childLeftIndex], _nodes[i]))
            return false;

          int childRightIndex = childLeftIndex + 1;
          if(childRightIndex < _nodes.Length &&
             _nodes[childRightIndex] != null &&
             HasHigherPriority(_nodes[childRightIndex], _nodes[i]))
            return false;
        }

      return true;
    }

    /// <summary>
    ///   Returns the item at the head of the queue without removing it.
    /// </summary>
    public TItem Peek() {
      return _numNodes <= 0 ? default(TItem) : _nodes[1].Data;
    }

    /// <summary>
    ///   Enqueue a node to the priority queue.  Lower values are placed in front. Ties are broken by first-in-first-out.
    ///   If the queue is full, the result is undefined.
    ///   If the node is already enqueued, the result is undefined.
    ///   O(log n)
    /// </summary>
    public void Enqueue(TItem item, TPriority priority) {
      var node = new QueueNode(item);

      ResizeIfNeedsResizing();

      node.Priority = priority;
      _numNodes++;
      _nodes[_numNodes] = node;
      node.QueueIndex = _numNodes;
      node.InsertionIndex = _numNodesEverEnqueued++;
      CascadeUp(_nodes[_numNodes]);
    }

    /// <summary>
    ///   Removes the head of the queue (node with minimum priority; ties are broken by order of insertion), and returns it.
    ///   If queue is empty, result is undefined
    ///   O(log n)
    /// </summary>
    public TItem Dequeue() {
      if(_numNodes <= 0)
        return default(TItem);

      var retv = _nodes[1].Data;
      Remove(_nodes[1]);
      return retv;
    }

    /// <summary>
    ///   Returns true if the queue has 1 or more of the secified items.
    /// </summary>
    /// <param name="item">Item.</param>
    public bool Contains(TItem item) {
      var comparer = EqualityComparer<TItem>.Default;
      for(int i = 1; i <= _numNodes; i++) {
        var node = _nodes[i];
        if(comparer.Equals(node.Data, item))
          return true;
      }

      return false;
    }

    /// <summary>
    ///   RemoveBehaviour the specified item. Note that the queue may contain multiples of the same item, in
    ///   which case this removes the one that is closest to the head.
    /// </summary>
    /// <param name="item">Item.</param>
    public TItem Remove(TItem item) {
      var rNode = GetExistingNode(item);
      Remove(rNode);
      return rNode != null ? rNode.Data : default(TItem);
    }

    /// <summary>
    ///   RemoveBehaviour the specified item. Note that the queue may contain multiples of the same item, in
    ///   which case this removes the one that is closest to the head.
    /// </summary>
    public TItem Remove(Func<TItem, bool> predicate) {
      QueueNode rNode = null;
      for(int i = 1; i <= _numNodes; i++)
        if(predicate(_nodes[i].Data)) {
          rNode = _nodes[i];
          break;
        }

      Remove(rNode);
      return rNode != null ? rNode.Data : default(TItem);
    }

    /// <summary>
    ///   Updates the priority of the specified item. If the item does not exist in the queue, it simply
    ///   returns.
    /// </summary>
    /// <param name="item">Item.</param>
    /// <param name="priority">Priority.</param>
    public void UpdatePriority(TItem item, TPriority priority) {
      if(Contains(item) == false)
        return;

      var node = GetExistingNode(item);
      node.Priority = priority;
      OnNodeUpdated(node);
    }

    /// <summary>
    ///   Removes every node from the queue.
    /// </summary>
    public void Clear() {
      Array.Clear(_nodes, 1, _numNodes);
      _numNodes = 0;
    }

    public PriorityQueue() {
      _numNodes = 0;
      _nodes = new QueueNode[DefaultSize];
      _numNodesEverEnqueued = 0;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="Crystal.PriorityQueue`2"/> class.
    /// </summary>
    /// <param name="maxNodes">Max nodes.</param>
    public PriorityQueue(int maxNodes) {
      _numNodes = 0;
      _nodes = maxNodes > 0 ? new QueueNode[maxNodes + 1] : new QueueNode[DefaultSize + 1];
      _numNodesEverEnqueued = 0;
    }

    int MaxSize {
      get { return _nodes.Length - 1; }
    }

    QueueNode GetExistingNode(TItem item) {
      var comparer = EqualityComparer<TItem>.Default;
      for(int i = 1; i <= _numNodes; i++) {
        var node = _nodes[i];
        if(comparer.Equals(node.Data, item))
          return node;
      }

      return null;
    }

    QueueNode Remove(QueueNode node) {
      if(node == null)
        return null;

      QueueNode rNode;
      // Check if the node is the last since we can remove this one quickly.
      if(node.QueueIndex == _numNodes) {
        rNode = _nodes[_numNodes];
        _nodes[_numNodes] = null;
        _numNodes--;
        return rNode;
      }

      QueueNode oldLastNode = _nodes[_numNodes];
      Swap(node, oldLastNode);
      rNode = _nodes[_numNodes];
      _nodes[_numNodes] = null;
      _numNodes--;

      // Reposition the previous last node up or down as appropriate.
      OnNodeUpdated(oldLastNode);
      return rNode;
    }

    void Swap(QueueNode node1, QueueNode node2) {
      //Swap the nodes
      _nodes[node1.QueueIndex] = node2;
      _nodes[node2.QueueIndex] = node1;

      //Swap their indicies
      int temp = node1.QueueIndex;
      node1.QueueIndex = node2.QueueIndex;
      node2.QueueIndex = temp;
    }

    void ResizeIfNeedsResizing() {
      if(Count == MaxSize)
        Resize(2 * MaxSize + 1);
    }

    /// <summary>
    ///   Reheapify up
    /// </summary>
    /// <param name="node">Node.</param>
    void CascadeUp(QueueNode node) {
      int parent = node.QueueIndex / 2;
      while(parent >= 1) {
        QueueNode parentNode = _nodes[parent];
        if(HasHigherPriority(parentNode, node))
          break;

        // Node has lower priority value, so move it up the heap
        Swap(node, parentNode);
        parent = node.QueueIndex / 2;
      }
    }

    /// <summary>
    ///   Reheapify down.
    /// </summary>
    /// <param name="node">Node.</param>
    void CascadeDown(QueueNode node) {
      QueueNode newParent;
      int finalQueueIndex = node.QueueIndex;
      while(true) {
        newParent = node;
        int childLeftIndex = 2 * finalQueueIndex;

        //Check if the left-child is higher-priority than the current node
        if(childLeftIndex > _numNodes) {
          //This could be placed outside the loop, but then we'd have to check newParent != node twice
          node.QueueIndex = finalQueueIndex;
          _nodes[finalQueueIndex] = node;
          break;
        }

        QueueNode childLeft = _nodes[childLeftIndex];
        if(HasHigherPriority(childLeft, newParent))
          newParent = childLeft;

        //Check if the right-child is higher-priority than either the current node or the left child
        int childRightIndex = childLeftIndex + 1;
        if(childRightIndex <= _numNodes) {
          QueueNode childRight = _nodes[childRightIndex];
          if(HasHigherPriority(childRight, newParent))
            newParent = childRight;
        }

        //If either of the children has higher (smaller) priority, swap and continue cascading
        if(newParent != node) {
          //Move new parent to its new index.  node will be moved once, at the end
          //Doing it this way is one less assignment operation than calling Swap()
          _nodes[finalQueueIndex] = newParent;

          int temp = newParent.QueueIndex;
          newParent.QueueIndex = finalQueueIndex;
          finalQueueIndex = temp;
        } else {
          //See note above
          node.QueueIndex = finalQueueIndex;
          _nodes[finalQueueIndex] = node;
          break;
        }
      }
    }

    /// <summary>
    ///   Returns true if 'higher' has higher priority than 'lower', false otherwise.
    ///   Note that calling HasHigherPriority(node, node) (ie. both arguments the same node) will return false
    /// </summary>
    bool HasHigherPriority(QueueNode higher, QueueNode lower) {
      var cmp = higher.Priority.CompareTo(lower.Priority);
      return cmp < 0 || cmp == 0 && higher.InsertionIndex < lower.InsertionIndex;
    }

    void Resize(int maxNodes) {
      if(maxNodes <= 0 ||
         maxNodes < _numNodes)
        return;

      var newArray = new QueueNode[maxNodes + 1];
      if(_numNodes > 0)
        Array.Copy(_nodes, 1, newArray, 1, _numNodes);
      _nodes = newArray;
    }

    void OnNodeUpdated(QueueNode node) {
      //Bubble the updated node up or down as appropriate
      int parentIndex = node.QueueIndex / 2;
      QueueNode parentNode = _nodes[parentIndex];

      if(parentIndex > 0 &&
         HasHigherPriority(node, parentNode))
        CascadeUp(node);
      else
        CascadeDown(node);
    }

    const int DefaultSize = 128;

    class QueueNode : PriorityQueueNode<TPriority> {
      public TItem Data { get; private set; }

      public QueueNode(TItem data) {
        Data = data;
      }
    }
  }

}