// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ICircularBuffer.cs is part of Crystal AI.
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
namespace Crystal {

  /// <summary>
  ///   Circular buffer interface.
  /// </summary>
  public interface ICircularBuffer<T> {
    /// <summary>
    ///   Number of elements in the buffer.
    /// </summary>
    /// <value>The count.</value>
    int Count { get; }

    /// <summary>
    ///   The maximum capacity of the buffer.
    /// </summary>
    /// <value>The capacity.</value>
    int Capacity { get; set; }

    /// <summary>
    ///   Returns the value at the head of the buffer.
    /// </summary>
    /// <value>The head.</value>
    T Head { get; }

    /// <summary>
    ///   Returns the value at the tail of the buffer.
    /// </summary>
    /// <value>The tail.</value>
    T Tail { get; }

    /// <summary>
    ///   Enqueue the specified item.
    /// </summary>
    T Enqueue(T item);

    /// <summary>
    ///   Dequeue this instance.
    /// </summary>
    T Dequeue();

    /// <summary>
    ///   Clears the buffer.
    /// </summary>
    void Clear();

    /// <summary>
    ///   Indexs the of.
    /// </summary>
    /// <returns>The of.</returns>
    /// <param name="item">Item.</param>
    int IndexOf(T item);

    /// <summary>
    ///   Removes the element at the given index.
    /// </summary>
    /// <param name="index">Index.</param>
    void RemoveAt(int index);

    /// <summary>
    ///   Gets or sets the <see cref="Crystal.ICircularBuffer`1"/> at the specified index.
    /// </summary>
    /// <param name="index">Index.</param>
    T this[int index] { get; set; }
  }

}