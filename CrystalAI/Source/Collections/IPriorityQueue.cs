// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IPriorityQueueDEPRECATED.cs is part of Crystal AI.
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


namespace Crystal {

  /// <summary>
  /// Interface to a priority queue handle.
  /// </summary>
  /// <typeparam name="TQueuedItem">The type of the queued item.</typeparam>
  public interface IPriorityQueueHandle<TQueuedItem> {
  }

  /// <summary>
  /// Every item that needs to be included in a <see cref="T:Crystal.PriorityQueue`1"/> must implement
  /// this interface. NB Just add a the public property in this interface as is.
  /// </summary>
  /// <typeparam name="TQueuedItem">The type of the queued item.</typeparam>
  public interface IHeapItem<TQueuedItem> {
    IPriorityQueueHandle<TQueuedItem> Handle { get; set; }
  }
  
  /// <summary>
  ///   Priority queue interface.
  /// </summary>
  public interface IPriorityQueue<TQueuedItem> {
    /// <summary>
    ///   Returns true if there is an element at the head of the queue, i.e. if the queue is not
    ///   empty.
    /// </summary>
    bool HasNext { get; }

    /// <summary>
    ///   Returns the number of items on the queue.
    /// </summary>
    int Count { get; }

    /// <summary>
    ///   Returns the item at the head of the queue without removing it.
    /// </summary>
    TQueuedItem Peek();

    /// <summary>
    ///   Enqueues an item to the list. 
    /// </summary>
    void Enqueue(TQueuedItem item);

    /// <summary>
    ///   Removes and returns the item at the head of the queue. In the event of a priority tie the item
    ///   inserted first in the queue is returned.
    /// </summary>
    TQueuedItem Dequeue();

    /// <summary>
    ///   Returns true if the queue has 1 or more of the specified items.
    /// </summary>
    bool Contains(TQueuedItem item);

    /// <summary>
    ///   Remove the specified item. Note that the queue may contain multiples of the same item, in
    ///   which case this removes the one that is closest to the head.
    /// </summary>
    void Remove(TQueuedItem item);
    
    /// <summary>
    ///   Updates the priority of the given item. If the item does not exist in the queue no operation is 
    /// performed.
    /// </summary>
    void UpdatePriority(TQueuedItem item);

    /// <summary>
    ///   Removes all queued items.
    /// </summary>
    void Clear();
  }
  
}