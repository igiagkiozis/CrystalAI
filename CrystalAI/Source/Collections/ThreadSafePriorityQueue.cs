// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ThreadSafePriorityQueue.cs is part of Crystal AI.
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
using System.Threading;


namespace Crystal {

  /// <summary>
  ///   Thread safe priority queue.
  /// </summary>
  /// 
  /// : IPriorityQueue<TQueuedItem> where TQueuedItem : class, IHeapItem<TQueuedItem>
  public class ThreadSafePriorityQueue<TQueuedItem> : IPriorityQueue<TQueuedItem> where TQueuedItem : class, IHeapItem<TQueuedItem> {
    readonly PriorityQueue<TQueuedItem> _queue;

    readonly ReaderWriterLockSlim _rwlock = new ReaderWriterLockSlim();

    /// <summary>
    ///   Returns true if there is an element at the head of the queue, i.e. if the queue is not
    ///   empty.
    /// </summary>
    public bool HasNext {
      get {
        using(_rwlock.Read())
          return _queue.HasNext;
      }
    }

    /// <summary>
    ///   Returns the number of items in the queue.
    /// </summary>
    /// <value>The count.</value>
    public int Count {
      get {
        using(_rwlock.Write())
          return _queue.Count;
      }
    }

    /// <summary>
    ///   Determines whether the binary heap of the internal binary heap queue is valid.
    /// </summary>
    /// <returns><c>true</c> if this instance is binary heap valid; otherwise, <c>false</c>.</returns>
    public bool IsHeapValid() {
      using(_rwlock.Read())
        return _queue.IsHeapValid();
    }

    /// <summary>
    ///   Returns the item at the head of the queue without removing it.
    /// </summary>
    public TQueuedItem Peek() {
      using(_rwlock.Write())
        return _queue.Count <= 0 ? default(TQueuedItem) : _queue.Peek();
    }

    /// <summary>
    ///   Enqueues an item to the list. Items with lower priority values are placed ahead of the
    ///   queue.
    /// </summary>
    /// <param name="item">Item.</param>
    /// <param name="priority">Priority.</param>
    public void Enqueue(TQueuedItem item) {
      using(_rwlock.Write())
        _queue.Enqueue(item);
    }

    /// <summary>
    ///   Removes and returns the item at the head of the queue. In the event of a priority tie the item
    ///   inserted first in the queue is returned.
    /// </summary>
    public TQueuedItem Dequeue() {
      using(_rwlock.Write())
        return _queue.Count <= 0 ? default(TQueuedItem) : _queue.Dequeue();
    }

    /// <summary>
    ///   Returns true if the queue has 1 or more of the specified items.
    /// </summary>
    /// <param name="item">Item.</param>
    public bool Contains(TQueuedItem item) {
      using(_rwlock.Read())
        return _queue.Contains(item);
    }


    public void Remove(TQueuedItem item) {
      using(_rwlock.Write())
        _queue.Remove(item);
    }
    
    /// <summary>
    /// Updates the priority of the given item. If the item does not exist in the queue no operation is
    /// performed.
    /// </summary>
    /// <param name="item"></param>
    public void UpdatePriority(TQueuedItem item) {
      using(_rwlock.Write())
        _queue.UpdatePriority(item);
    }
    
    /// <summary>
    /// Removes all queued items.
    /// </summary>
    public void Clear() {
      using(_rwlock.Write())
        _queue.Clear();
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ThreadSafePriorityQueue{TQueuedItem}"/> class.
    /// </summary>
    public ThreadSafePriorityQueue() {
      _queue = new PriorityQueue<TQueuedItem>(DefaultSize);
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="ThreadSafePriorityQueue{TQueuedItem}"/> class.
    /// </summary>
    /// <param name="size">The size.</param>
    public ThreadSafePriorityQueue(int size) {
      _queue = new PriorityQueue<TQueuedItem>(size);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ThreadSafePriorityQueue{TQueuedItem}"/> class.
    /// </summary>
    /// <param name="size">The size.</param>
    /// <param name="comparer">The comparer.</param>
    public ThreadSafePriorityQueue(int size, IComparer<TQueuedItem> comparer) {
      _queue = new PriorityQueue<TQueuedItem>(size, comparer);
    }

    const int DefaultSize = 128;
  }

}