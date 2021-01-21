using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonnes.QuadTreeF
{
  public class QuadTreeF<T> : ICollection<QuadNodeF<T>>, IEquatable<QuadTreeF<T>>, IEnumerable<QuadNodeF<T>> {
    /// <summary>Default number of max nodes in each tree</summary>
    public const int DEFAULT_QUADTREE_MAXNODES = 4;
    
    private QuadTreeF<T> _quadTopLeft, _quadTopRight, _quadBottomLeft, _quadBottomRight;
    private List<QuadNodeF<T>> _nodes;

    private readonly int _maxNodes;
    /// <summary>Max number of nodes in the leaf</summary>
    public int MaxNodes => this._maxNodes;

    private readonly RectangleF _bounds;
    /// <summary>Bounds of the leaf/tree</summary>
    public RectangleF Bounds => this._bounds;

    /// <summary>This branch is a leaf</summary>
    private bool _isLeaf => this._quadTopLeft == null;

    public bool IsReadOnly => false;
    public bool IsFixedSize => true;

    /// <summary>Number of nodes in the leaf/tree</summary>
    public int Count => this.AllNodes.Count;

    /// <summary>Returns a list of all nodes the leaf/tree</summary>
    public List<QuadNodeF<T>> AllNodes { 
      get {
        List<QuadNodeF<T>> nodes = new List<QuadNodeF<T>>();
        nodes.AddRange(this._nodes);
        if(!this._isLeaf) {
          nodes.AddRange(this._quadTopLeft.AllNodes);
          nodes.AddRange(this._quadTopRight.AllNodes);
          nodes.AddRange(this._quadBottomLeft.AllNodes);
          nodes.AddRange(this._quadBottomRight.AllNodes);
        }
        return nodes;
      }
    }

    /// <summary>Returns a list of all points in the leaf/tree</summary>
    public List<PointF> Points { 
      get {
        List<PointF> points = new List<PointF>();
        foreach(QuadNodeF<T> node in this._nodes) points.Add(node.Point);
        if(!this._isLeaf) {
          points.AddRange(this._quadTopLeft.Points);
          points.AddRange(this._quadTopRight.Points);
          points.AddRange(this._quadBottomLeft.Points);
          points.AddRange(this._quadBottomRight.Points);
        }
        return points;
      }
    }

    /// <summary>Returns a list of all values in the leaf/tree</summary>
    public List<T> Values { 
      get {
        List<T> values = new List<T>();
        foreach(QuadNodeF<T> node in this._nodes) values.Add(node.Value);
        if(!this._isLeaf) {
          values.AddRange(this._quadTopLeft.Values);
          values.AddRange(this._quadTopRight.Values);
          values.AddRange(this._quadBottomLeft.Values);
          values.AddRange(this._quadBottomRight.Values);
        }
        return values;
      } 
    }

    /// <summary>Returns a list of all leafs in the leaf/tree</summary>
    public List<QuadTreeF<T>> Leafs {
      get {
        List<QuadTreeF<T>> leafs = new List<QuadTreeF<T>>();
        if(this._isLeaf) leafs.Add(this);
        else {
          leafs.AddRange(this._quadTopLeft.Leafs);
          leafs.AddRange(this._quadTopRight.Leafs);
          leafs.AddRange(this._quadBottomLeft.Leafs);
          leafs.AddRange(this._quadBottomRight.Leafs);
        }
        return leafs;
      }
    }

    /// <summary>Allocates an empty tree with (w: 0, y: 0, w: 0, h: 0) bounds</summary>
    public static QuadTreeF<T> Empty => new QuadTreeF<T>(RectangleF.Empty);

    /// <summary>Construct a new <see cref="QuadTree" /></summary>
    /// <param name="bounds">Bounds/rectangle of the new tree</param>
    /// <param name="maxNodes">Max number of nodes in the new tree</param>
    public QuadTreeF(RectangleF bounds, int maxNodes = QuadTreeF<T>.DEFAULT_QUADTREE_MAXNODES) {
      this._bounds = bounds;
      this._maxNodes = maxNodes;
      this._nodes = new List<QuadNodeF<T>>();
    }

    /// <summary>Add a node to the tree</summary>
    public void Add(float x, float y, T value) => this.Add(new QuadNodeF<T>(new PointF(x, y), value));
    /// <summary>Add a node to the tree</summary>
    public void Add(PointF point, T value) => this.Add(new QuadNodeF<T>(point, value));
    /// <summary>Add a node to the tree</summary>
    public void Add(QuadNodeF<T> node) {
      PointF point = node.Point;
      if(!this._bounds.Contains(point)) return;
      if(this._isLeaf) {
        int existingNodePointIndex = this._indexOfPointInLeaf(point);
        if(existingNodePointIndex >= 0) {
          this._nodes[existingNodePointIndex] = node;
        } else {
          this._nodes.Add(node);
        }
        if(this._nodes.Count >= this._maxNodes) this._createQuads();
        return;
      }
      this._quadTopLeft.Add(node);
      this._quadTopRight.Add(node);
      this._quadBottomLeft.Add(node);
      this._quadBottomRight.Add(node);
    }
    
    /// <summary>Add multiple nodes to the tree</summary>
    public void AddRange(ICollection<QuadNodeF<T>> nodes) {
      if(nodes == null) throw new ArgumentNullException(nameof(nodes));
      foreach(QuadNodeF<T> node in nodes) this.Add(node);
    }

    /// <summary>Check if there are a node at point</summary>
    public bool Contains(float x, float y) => this.Contains(new PointF(x, y));
    /// <summary>Check if there are a node at point</summary>
    public bool Contains(PointF point) {
      if(!this._bounds.Contains(point)) return false;
      if(this._isLeaf) {
        foreach(QuadNodeF<T> node in this._nodes) {
          if(EqualityComparer<PointF>.Default.Equals(point, node.Point)) return true;
        }
        return false;
      }
      return (
        this._quadTopLeft.Contains(point) || 
        this._quadTopRight.Contains(point) || 
        this._quadBottomLeft.Contains(point) || 
        this._quadBottomRight.Contains(point)
      );
    }
    /// <summary>Check the tree contains a node. Testing values not reference.</summary>
    public bool Contains(QuadNodeF<T> node) {
      if(!this._bounds.Contains(node.Point)) return false;
      if(this._isLeaf) {
        foreach(QuadNodeF<T> item in this._nodes) {
          if(EqualityComparer<QuadNodeF<T>>.Default.Equals(node, item)) return true;
        }
        return false;
      }
      return (
        this._quadTopLeft.Contains(node) || 
        this._quadTopRight.Contains(node) || 
        this._quadBottomLeft.Contains(node) || 
        this._quadBottomRight.Contains(node)
      );
    }

    /// <summary>Try to get a node by searching for it by a point</summary>
    public QuadNodeF<T> Search(float x, float y) => this.Search(new PointF(x, y));
    /// <summary>Try to get a node by searching for it by a tuple point</summary>
    public QuadNodeF<T> Search((float x, float y) point) => this.Search((PointF)point);
    /// <summary>TryGet by searching for point </summary>
    public bool Search(float x, float y, out QuadNodeF<T> node) {
      bool exists = this.Search(new PointF(x, y), out QuadNodeF<T> outNode);
      node = outNode;
      return exists;
    }
    /// <summary>Try to get a node by searching for it by a point</summary>
    public QuadNodeF<T> Search(PointF point) {
      if(!this._bounds.Contains(point)) return default(QuadNodeF<T>);
      if(this._isLeaf) {
        foreach(QuadNodeF<T> node in this._nodes) {
          if(EqualityComparer<PointF>.Default.Equals(point, node.Point)) return node;
        }
        return default(QuadNodeF<T>);
      }
      QuadNodeF<T> tlNode = this._quadTopLeft.Search(point);
      if(tlNode != default(QuadNodeF<T>)) return tlNode; 
      QuadNodeF<T> trNode = this._quadTopRight.Search(point);
      if(tlNode != default(QuadNodeF<T>)) return trNode;
      QuadNodeF<T> blNode = this._quadBottomLeft.Search(point);
      if(blNode != default(QuadNodeF<T>)) return blNode;
      QuadNodeF<T> brNode = this._quadBottomRight.Search(point);
      if(brNode != default(QuadNodeF<T>)) return brNode;
      return default(QuadNodeF<T>);
    }
    /// <summary>TryGet a node by searching for it by a point</summary>
    public bool Search(PointF point, out QuadNodeF<T> node) {
      node = default(QuadNodeF<T>);
      if(!this._bounds.Contains(point)) return false;
      if(this._isLeaf) {
        foreach(QuadNodeF<T> item in this._nodes) {
          if(EqualityComparer<PointF>.Default.Equals(point, item.Point)) {
            node = item;
            return true;
          }
        }
        return false;
      }
      if(this._quadTopLeft.Search(point, out QuadNodeF<T> tlNode)) {
        node = tlNode;
        return true;
      }
      if(this._quadTopRight.Search(point, out QuadNodeF<T> trNode)) {
        node = trNode;
        return true;
      }
      if(this._quadBottomLeft.Search(point, out QuadNodeF<T> blNode)) {
        node = blNode;
        return true;
      }
      if(this._quadBottomRight.Search(point, out QuadNodeF<T> brNode)) {
        node = brNode;
        return true;
      }
      return false;
    }
    /// <summary>Search for all nodes inside of a rectangle.</summary>
    /// <returns>A list of all nodes inside of the rectangle</returns>
    public List<QuadNodeF<T>> Search(RectangleF rect) {
      List<QuadNodeF<T>> nodes = new List<QuadNodeF<T>>();
      if(!this._bounds.Intersects(rect)) return nodes;
      if(this._isLeaf) {
        foreach(QuadNodeF<T> node in this._nodes) {
          if(rect.Contains(node.Point)) nodes.Add(node);
        }
        return nodes;
      }
      nodes.AddRange(this._quadTopLeft.Search(rect));
      nodes.AddRange(this._quadTopRight.Search(rect));
      nodes.AddRange(this._quadBottomLeft.Search(rect));
      nodes.AddRange(this._quadBottomRight.Search(rect));
      return nodes;
    }

    /// <summary>Remove a node from the tree using a point</summary>
    public bool Remove(float x, float y) => this.Remove(new PointF(x, y));
    /// <summary>Remove a node from the tree using a point</summary>
    public bool Remove(PointF point) {
      if(!this._bounds.Contains(point)) return false;
      if(this._isLeaf) {
        foreach(QuadNodeF<T> node in this._nodes) {
          if(EqualityComparer<PointF>.Default.Equals(point, node.Point)) return this._nodes.Remove(node);
        }
        return false;
      }
      return (
        this._quadTopLeft.Remove(point) || 
        this._quadTopRight.Remove(point) || 
        this._quadBottomLeft.Remove(point) || 
        this._quadBottomRight.Remove(point)        
      );
    }
    /// <summary>Remove a node form the tree. Comparing using values not reference.</summary>
    public bool Remove(QuadNodeF<T> node) {
      if(!this._bounds.Contains(node.Point)) return false;
      if(this._isLeaf) return this._nodes.Remove(node);
      return (
        this._quadTopLeft.Remove(node) || 
        this._quadTopRight.Remove(node) || 
        this._quadBottomLeft.Remove(node) || 
        this._quadBottomRight.Remove(node)        
      );
    }

    /// <summary>Divide this branch into four new ones</summary>
    private void _createQuads() {
      float halfWidth = this._bounds.Width / 2;
      float halfHeight = this._bounds.Height / 2;
      this._quadTopLeft = new QuadTreeF<T>(new RectangleF(this._bounds.X, this._bounds.Y, halfWidth, halfHeight), this._maxNodes);
      this._quadTopRight = new QuadTreeF<T>(new RectangleF(this._bounds.X + this._bounds.Width  / 2, this._bounds.Y, halfWidth, halfHeight), this._maxNodes);
      this._quadBottomLeft = new QuadTreeF<T>(new RectangleF(this._bounds.X, this._bounds.Y + halfHeight, halfWidth, halfHeight), this._maxNodes);
      this._quadBottomRight = new QuadTreeF<T>(new RectangleF(this._bounds.X + halfWidth, this._bounds.Y + halfHeight, halfWidth, halfHeight), this._maxNodes);
      this.AddRange(this._nodes);
      this._nodes.Clear();
    }

    /// <summary>Get index of a node using the node's point in this leaf.</summary>
    private int _indexOfPointInLeaf(PointF point) {
      for(int i = 0; i < this._nodes.Count; ++i) {
        QuadNodeF<T> node = this._nodes[i];
        if(node != null && node.Point.Equals(point)) return i;
      }
      return -1;
    }

    /// <summary>Clear all nodes/leafs/branches from the tree. The root (original) tree is reverted to its original state.</summary>
    public void Clear() {
      this._nodes.Clear();
      this._nodes = null;
      this._quadTopLeft = null;
      this._quadTopRight = null;
      this._quadBottomLeft = null;
      this._quadBottomRight = null;
    }

    /// <see href="https://docs.microsoft.com/en-us/dotnet/api/system.array.copyto?view=net-5.0"/>
    public void CopyTo(QuadNodeF<T>[] array, int index) {
      if(array == default(QuadNodeF<T>[])) throw new ArgumentNullException(nameof(array));
      ((ICollection<QuadNodeF<T>>)this).CopyTo(array, index);
    }

    /// <summary>Similar float point values might be overwritten by other values. Col 0.1 and 0.5 on row 0 will both be assigned to the same index. The last value will be the only one stored in the array.</summary>
    /// <remarks>Should probably not be used in critical systems.</remarks>
    public virtual T[][] ToJaggedArray() { // MAYBE default value parameter
      RectangleF bounds = this.Bounds;
      T[][] array = new T[(int)MathF.Ceiling(bounds.Height)][];
      int width = (int)MathF.Ceiling(bounds.Width);
      foreach(QuadNodeF<T> node in this.AllNodes) {
        PointF vector = node.Point;
        int y = (int)vector.Y;
        T[] row = array[y];
        if(row == null) row = new T[width];
        row[(int)vector.X] = node.Value;
        array[y] = row;
      }
      return array;
    }
    /// <summary>Similar float point values might be overwritten by other values. Col 0.1 and 0.5 on row 0 will both be assigned to the same index. The last value will be the only one stored in the array.</summary>
    /// <remarks>Should probably not be used in critical systems.</remarks>
    public virtual T[,] To2dArray() { // MAYBE default value parameter
      RectangleF bounds = this.Bounds;
      T[,] array = new T[(int)MathF.Ceiling(bounds.Height), (int)MathF.Ceiling(bounds.Width)];
      foreach(QuadNodeF<T> node in this.AllNodes) {
        PointF vector = node.Point;
        array[(int)vector.Y, (int)vector.X] = node.Value;
      }
      return array;
    }

    /// <summary>Check value passed equality of two quadtrees of same generic type</summary>
    public bool Equals(QuadTreeF<T> tree) {
      EqualityComparer<QuadTreeF<T>> treeComparer = EqualityComparer<QuadTreeF<T>>.Default;
      return (
        EqualityComparer<int>.Default.Equals(this._maxNodes, tree._maxNodes) &&
        EqualityComparer<RectangleF>.Default.Equals(this._bounds, tree._bounds) &&
        EqualityComparer<List<QuadNodeF<T>>>.Default.Equals(this._nodes, tree._nodes) &&
        treeComparer.Equals(this._quadTopLeft, tree._quadTopLeft) &&
        treeComparer.Equals(this._quadTopRight, tree._quadTopRight) &&
        treeComparer.Equals(this._quadBottomLeft, tree._quadBottomLeft) &&
        treeComparer.Equals(this._quadBottomRight, tree._quadBottomRight)
      );
    }
    public override bool Equals(object obj) => this.Equals((QuadTreeF<T>)obj);
    public override int GetHashCode() => HashCode.Combine(this._maxNodes, this._bounds, this._nodes, this._quadTopLeft, this._quadTopRight, this._quadBottomLeft, this._quadBottomRight);
    
    public IEnumerator<QuadNodeF<T>> GetEnumerator() => this.AllNodes.GetEnumerator(); // MAYBE rewrite to use yield
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
  }
}