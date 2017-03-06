using System;
using System.Collections.Generic;


namespace Crystal {

  /// <summary>
  ///   A standard priority queue implemented using a min-heap with some extra functionality
  ///   such as "contains" test and efficient item removal.
  /// </summary>
  /// <typeparam name="TQueuedItem">The type of the queued item.</typeparam>
  /// <seealso cref="Crystal.IPriorityQueue{TQueuedItem}"/>
  public class PriorityQueue<TQueuedItem> : IPriorityQueue<TQueuedItem>
    where TQueuedItem : class, IHeapItem<TQueuedItem> {
    const int DefaultSize = 128;

    int _heapSize;
    ulong _runningCount;
    HeapNode[] _heap;
    IComparer<TQueuedItem> _comparer;

    /// <summary>
    ///   Returns true if there is an element at the head of the queue, i.e. if the queue is not
    ///   empty.
    /// </summary>
    public bool HasNext {
      get { return _heapSize > 0; }
    }

    /// <summary>
    ///   Returns the number of items in the queue.
    /// </summary>
    public int Count {
      get { return _heapSize; }
    }

    /// <summary>
    ///   Returns the item at the head of the queue without removing it.
    /// </summary>
    public TQueuedItem Peek() {
      return _heap[1]?.Item;
    }

    /// <summary>
    ///   Enqueues an item to the list. Items with lower priority values are placed ahead of the
    ///   queue.
    /// </summary>
    public void Enqueue(TQueuedItem item) {
      var node = new HeapNode(item);

      if(_heapSize + 1 >= _heap.Length)
        ResizeHeap();

      _heapSize++;
      _heap[_heapSize] = node;
      node.Handle.Index = _heapSize;
      node.Handle.Order = _runningCount++;
      MinBubbleUp(_heapSize);
    }

    /// <summary>
    ///   Removes and returns the item at the head of the queue. In the event of a priority tie the item
    ///   inserted first in the queue is returned.
    /// </summary>
    public TQueuedItem Dequeue() {
      if(_heapSize < 1)
        return default(TQueuedItem);

      TQueuedItem ret = _heap[1].Item;
      _heap[1] = _heap[_heapSize];
      _heap[_heapSize] = null;
      _heapSize--;
      MinHeapify(1);
      return ret;
    }

    /// <summary>
    ///   Returns true if the queue has 1 or more of the specified items.
    /// </summary>
    public bool Contains(TQueuedItem item) {
      Handle h = item.Handle as Handle;
      if(h == null || h.Index > _heapSize)
        return false;

      return _heap[h.Index].Item == item;
    }

    /// <summary>
    ///   RemoveBehaviour the specified item. Note that the queue may contain multiples of the same item, in
    ///   which case this removes the one that is closest to the head.
    /// </summary>
    /// <param name="item">Item.</param>
    public void Remove(TQueuedItem item) {
      if(item?.Handle == null)
        return;

      Handle itemHandle = item.Handle as Handle;
      if(itemHandle == null)
        return;

      int itemIndex = itemHandle.Index;

      if(itemIndex == _heapSize) {
        _heap[_heapSize] = null;
        _heapSize--;
        return;
      }

      Swap(itemIndex, _heapSize);
      _heap[_heapSize] = null;
      _heapSize--;
      int parent = itemIndex >> 1;

      if(parent > 0 && _comparer.Compare(_heap[itemIndex].Item, _heap[parent].Item) < 0)
        MinBubbleUp(itemIndex);
      else
        MinHeapify(itemIndex);
    }

    /// <summary>
    ///   Updates the priority of the specified item. If the item does not exist in the queue, it simply
    ///   returns.
    /// </summary>
    public void UpdatePriority(TQueuedItem item) {
      Handle h = item.Handle as Handle;
      if(h == null)
        return;

      int idx = h.Index;
      h.Order = _runningCount++;
      int parent = idx >> 1;

      if(parent > 0 && _comparer.Compare(_heap[idx].Item, _heap[parent].Item) < 0)
        MinBubbleUp(idx);
      else
        MinHeapify(idx);
    }

    /// <summary>
    ///   Removes every node from the queue.
    /// </summary>
    public void Clear() {
      Array.Clear(_heap, 1, _heapSize);
      _heapSize = 0;
    }

    /// <summary>
    ///   Determines whether the min-heap used in the priority queue is valid.
    /// </summary>
    public bool IsHeapValid() {
      for(int i = 1; i < _heap.Length; i++)
        if(_heap[i] != null) {
          int left = i << 1;
          if(left < _heap.Length &&
             _heap[left] != null &&
             _comparer.Compare(_heap[left].Item, _heap[i].Item) < 0)
            return false;

          int right = left | 1;
          if(right < _heap.Length &&
             _heap[right] != null &&
             _comparer.Compare(_heap[right].Item, _heap[i].Item) < 0)
            return false;
        }

      return true;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="PriorityQueue{TQueuedItem}"/> class.
    /// </summary>
    /// <param name="comparer">The comparer.</param>
    public PriorityQueue(IComparer<TQueuedItem> comparer = null) {
      _heap = new HeapNode[DefaultSize];
      _comparer = comparer ?? Comparer<TQueuedItem>.Default;
      _heapSize = 0;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="PriorityQueue{TQueuedItem}"/> class.
    /// </summary>
    /// <param name="initialHeapSize">Initial size of the heap.</param>
    /// <param name="comparer">The comparer.</param>
    public PriorityQueue(int initialHeapSize, IComparer<TQueuedItem> comparer = null) {
      _heap = initialHeapSize < 1 ? new HeapNode[DefaultSize] : new HeapNode[initialHeapSize];
      _comparer = comparer ?? Comparer<TQueuedItem>.Default;
      _heapSize = 0;
    }

    void ResizeHeap() {
      var resizedHeap = new HeapNode[2 * _heap.Length];
      Array.Copy(_heap, 1, resizedHeap, 1, _heapSize);
      _heap = resizedHeap;
    }

    void MinHeapify(int i) {
      while(true) {
        int smallest = i;
        int left = i << 1;
        int right = (i << 1) | 1;

        if(left <= _heapSize) {
          var cmp = _comparer.Compare(_heap[left].Item, _heap[i].Item);
          if(cmp < 0 || cmp == 0 && _heap[left].Handle.Order < _heap[i].Handle.Order)
            smallest = left;
        }

        if(right <= _heapSize) {
          var cmp = _comparer.Compare(_heap[right].Item, _heap[smallest].Item);
          if(cmp < 0 || cmp == 0 && _heap[right].Handle.Order < _heap[smallest].Handle.Order)
            smallest = right;
        }

        if(smallest == i)
          return;

        Swap(i, smallest);
        i = smallest;
      }
    }

    void MinBubbleUp(int i) {
      if(i < 1)
        return;

      int parent = i >> 1;
      while(parent > 0)
        if(_comparer.Compare(_heap[i].Item, _heap[parent].Item) < 0) {
          Swap(parent, i);
          i = parent;
          parent = parent >> 1;
        } else
          break;
    }

    void Swap(int i, int j) {
      var tmp = _heap[i];
      _heap[i] = _heap[j];
      _heap[j] = tmp;

      _heap[i].Handle.Index = i;
      _heap[j].Handle.Index = j;
    }


    class Handle : IPriorityQueueHandle<TQueuedItem> {
      internal int Index = -1;
      internal ulong Order;
    }

    class HeapNode {
      internal TQueuedItem Item;
      internal Handle Handle;

      public HeapNode(TQueuedItem item) {
        Item = item;
        Handle = new Handle();
        Item.Handle = Handle;
      }

      ~HeapNode() {
        Item.Handle = null;
      }
    }
  }

}