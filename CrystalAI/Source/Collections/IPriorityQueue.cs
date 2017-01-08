// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// IPriorityQueue.cs is part of Crystal AI.
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

  // The initial version of this file was based on https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp.git

  /// <summary>
  ///   Priority queue interface.
  /// </summary>
  public interface IPriorityQueue<TItem, in TPriority> where TPriority : IComparable<TPriority> {
    /// <summary>
    ///   Returns true if there is an element at the head of the queue, i.e. if the queue is not
    ///   empty.
    /// </summary>
    bool HasNext { get; }

    /// <summary>
    ///   Returns the number of items in the queue.
    /// </summary>
    int Count { get; }

    /// <summary>
    ///   Returns the item at the head of the queue without removing it.
    /// </summary>
    TItem Peek();

    /// <summary>
    ///   Enqueues an item to the list. Items with lower priority values are placed ahead of the
    ///   queue.
    /// </summary>
    void Enqueue(TItem item, TPriority priority);

    /// <summary>
    ///   Removes and returns the item at the head of the queue. In the event of a priority tie the item
    ///   inserted first in the queue is returned.
    /// </summary>
    TItem Dequeue();

    /// <summary>
    ///   Returns true if the queue has 1 or more of the secified items.
    /// </summary>
    bool Contains(TItem item);

    /// <summary>
    ///   RemoveBehaviour the specified item. Note that the queue may contain multiples of the same item, in
    ///   which case this removes the one that is closest to the head.
    /// </summary>
    /// <param name="item">Item.</param>
    TItem Remove(TItem item);

    /// <summary>
    ///   Removes the first item that matches the specified predicate. Note that the queue may contain
    ///   multiples of the same item, in which case this removes the one that is closest to the head.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <returns>The item that was removed or null if no item was not found.</returns>
    TItem Remove(Func<TItem, bool> predicate);

    /// <summary>
    ///   Updates the priority of the specified item. If the item does not exist in the queue, it simply
    ///   returns.
    /// </summary>
    void UpdatePriority(TItem item, TPriority priority);

    /// <summary>
    ///   Removes every node from the queue.
    /// </summary>
    void Clear();
  }

}