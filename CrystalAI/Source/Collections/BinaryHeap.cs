using System;
using System.Collections.Generic;


namespace CrystalAI {

  public interface IPriorityQueueHandle<T> {
  }

  public interface IHeapItem<T> {
    IPriorityQueueHandle<T> Handle { get; set; }
  }

  internal class BinaryHeap<T> where T : class, IHeapItem<T> {
    const int DefaultSize = 128;

    ulong _runningCount;
    IComparer<T> _comparer = Comparer<T>.Default;
    int _heapSize;
    HeapNode[] _heap;


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
    public T Peek() {
      return _heap[1].Item;
    }

    /// <summary>
    ///   Enqueues an item to the list. Items with lower priority values are placed ahead of the
    ///   queue.
    /// </summary>
    public void Enqueue(T item) {
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
    public T Dequeue() {
      if(_heapSize < 1)
        return default(T);

      T ret = _heap[1].Item;
      _heap[1] = _heap[_heapSize];
      _heap[_heapSize] = null;
      _heapSize--;
      MinHeapify(1);
      return ret;
    }

    /// <summary>
    ///   Returns true if the queue has 1 or more of the specified items.
    /// </summary>
    public bool Contains(T item) {
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
    public void Remove(T item) {
      if(item == null)
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
    public void UpdatePriority(T item) {
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

    public bool IsBinaryHeapValid() {
      for(int i = 1; i < _heap.Length; i++)
        if(_heap[i] != null) {
          int childLeftIndex = i << 1;
          if(childLeftIndex < _heap.Length &&
             _heap[childLeftIndex] != null &&
             _comparer.Compare(_heap[childLeftIndex].Item, _heap[i].Item) < 0)
            return false;

          int childRightIndex = childLeftIndex | 1;
          if(childRightIndex < _heap.Length &&
             _heap[childRightIndex] != null &&
             _comparer.Compare(_heap[childRightIndex].Item, _heap[i].Item) < 0)
            return false;
        }

      return true;
    }

    public BinaryHeap() {
      _heap = new HeapNode[DefaultSize];
      _heapSize = 0;
    }

    public BinaryHeap(IComparer<T> comparer) {
      _heap = new HeapNode[DefaultSize];
      _heapSize = 0;
      _comparer = comparer;
    }

    void ResizeHeap() {
      var resizedHeap = new HeapNode[2 * _heap.Length];
      Array.Copy(_heap, 1, resizedHeap, 1, _heapSize);
      _heap = resizedHeap;
    }

    void MaxHeapify(int i) {
      int largest;
      int left = i << 1;
      int right = (i << 1) | 1;

      if(left <= _heapSize && _comparer.Compare(_heap[left].Item, _heap[i].Item) > 0)
        largest = left;
      else
        largest = i;

      if(right <= _heapSize && _comparer.Compare(_heap[right].Item, _heap[i].Item) > 0)
        largest = right;

      if(largest != i) {
        Swap(i, largest);
        MaxHeapify(largest);
      }
    }

    void MinHeapify(int i) {
      int smallest;
      int left = i << 1;
      int right = (i << 1) | 1;

      smallest = i;
      if(left <= _heapSize) {
        var cmp = _comparer.Compare(_heap[left].Item, _heap[i].Item);
        if(cmp < 0 || cmp == 0 && _heap[left].Handle.Order < _heap[i].Handle.Order)
          smallest = left;
      }

      if(right <= _heapSize) {
        var cmp = _comparer.Compare(_heap[right].Item, _heap[i].Item);
        if(cmp < 0 || cmp == 0 && _heap[right].Handle.Order < _heap[i].Handle.Order)
          smallest = right;
      }

      if(smallest != i) {
        Swap(i, smallest);
        MinHeapify(smallest);
      }
    }

    void MinBubbleUp(int i) {
      if(i < 1)
        return;

      T c = _heap[i].Item;
      T p = c;
      int parent = i >> 1;

      while(parent > 0) {
        p = _heap[parent].Item;
        if(_comparer.Compare(c, p) < 0) {
          Swap(parent, i);
          i = parent;
          parent = parent >> 1;
        } else
          break;
      }
    }

    void Swap(int i, int j) {
      var tmp = _heap[i];
      _heap[i] = _heap[j];
      _heap[j] = tmp;

      _heap[i].Handle.Index = i;
      _heap[j].Handle.Index = j;
    }

    [Serializable]
    class Handle : IPriorityQueueHandle<T> {
      public override string ToString() {
        return string.Format("[{0}]", Index);
      }

      internal int Index = -1;
      internal ulong Order;
    }

    class HeapNode {
      public override string ToString() {
        return string.Format("[{0}]", Item);
      }

      public HeapNode() {
        Item = null;
        Handle = null;
      }

      public HeapNode(T item) {
        Item = item;
        Handle = new Handle();
        Item.Handle = Handle;
      }

      internal T Item;
      internal Handle Handle;

      ~HeapNode() {
        Item.Handle = null;
      }
    }
  }

}