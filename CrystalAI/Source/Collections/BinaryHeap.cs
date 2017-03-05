using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrystalAI {

  public interface IPriorityQueueHandle<T> {    
  }

  public interface IHeapItem<T>  {    
    IPriorityQueueHandle<T> Handle { get; set; }
  }
  
  class BinaryHeap<T> where T : class, IHeapItem<T> {

    IComparer<T> _comparer = Comparer<T>.Default;

    [Serializable]
    class Handle : IPriorityQueueHandle<T> {
      /// <summary>
      /// To save space, the index is 2*cell for heap[cell].first, and 2*cell+1 for heap[cell].last
      /// </summary>
      internal int index = -1;

      public override string ToString() {
        return string.Format("[{0}]", index);
      }
    }
    
    class HeapNode {
      internal T Item;
      internal Handle Handle;

      public HeapNode() {
        Item = null;
        Handle = null;
      }

      public HeapNode(T item) {
        Item = item;
        Handle = new Handle();
        Item.Handle = Handle;
      }

      ~HeapNode() {
        Item.Handle = null;
      }

      public override string ToString() {
        return string.Format("[{0}]", Item);
      }
    }

    int _heapSize;
    HeapNode[] _heap;

    const int DefaultSize = 128;


    /// <summary>
    ///   Returns true if there is an element at the head of the queue, i.e. if the queue is not
    ///   empty.
    /// </summary>
    public bool HasNext { get { return _heapSize > 0; } }

    /// <summary>
    ///   Returns the number of items in the queue.
    /// </summary>
    public int Count { get { return _heapSize; } }

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
      node.Handle.index = _heapSize;
      MinBubbleUp(_heapSize);
    }

    void ResizeHeap() {
      var resizedHeap = new HeapNode[2 * _heap.Length];
      Array.Copy(_heap, 1, resizedHeap, 1, _heapSize);
      _heap = resizedHeap;
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
      if(h == null || h.index > _heapSize)
        return false;
      
      return _heap[h.index].Item == item;      
    }

    /// <summary>
    ///   RemoveBehaviour the specified item. Note that the queue may contain multiples of the same item, in
    ///   which case this removes the one that is closest to the head.
    /// </summary>
    /// <param name="item">Item.</param>
    public T Remove(T item) {
      
      return default(T);
    }

    /// <summary>
    ///   Updates the priority of the specified item. If the item does not exist in the queue, it simply
    ///   returns.
    /// </summary>
    public void UpdatePriority(T item) {
      
    }

    /// <summary>
    ///   Removes every node from the queue.
    /// </summary>
    public void Clear() {
      Array.Clear(_heap, 1, _heapSize);
      _heapSize = 0;
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

    void MaxHeapify(int i) {
      int largest;
      int left = i << 1;
      int right = (i<<1) | 1;
      if(left <= _heapSize && _comparer.Compare(_heap[left].Item, _heap[i].Item) > 0) {
        largest = left;
      } else {
        largest = i;
      }

      if(right <= _heapSize && _comparer.Compare(_heap[right].Item, _heap[i].Item) > 0) {
        largest = right;
      }

      if(largest != i) {
        Swap(i, largest);
        MaxHeapify(largest);
      }
    }

    void MinHeapify(int i) {
      int smallest;
      int left = i << 1;
      int right = (i << 1) | 1;
      if(left <= _heapSize && _comparer.Compare(_heap[left].Item, _heap[i].Item) < 0) {
        smallest = left;
      } else {
        smallest = i;
      }

      if(right <= _heapSize && _comparer.Compare(_heap[right].Item, _heap[i].Item) < 0) {
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
      Handle minHandle = _heap[i].Handle;
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

      _heap[i].Handle.index = i;
      _heap[j].Handle.index = j;
    }


  }


}
