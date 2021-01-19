using System;
using System.Collections.Generic;

namespace Tonnes.QuadTreeF
{
  /// <summary><see cref="QuadTree" /> node</summary>
  /// <typeparam>Type of node value</typeparam>
  public class QuadNodeF<T> : IEquatable<QuadNodeF<T>> {
    /// <summary><see cref="PointF" /> of node</summary>
    public PointF Point { get; }
    /// <summary>Value of node</summary>
    public T Value { get; set; }

    public QuadNodeF(float x, float y, T value) : this(new PointF(x, y), value) {}
    public QuadNodeF(PointF point, T value) => (this.Point, this.Value) = (point, value);

    public bool Equals(QuadNodeF<T> node) => EqualityComparer<PointF>.Default.Equals(this.Point, node.Point) && EqualityComparer<T>.Default.Equals(this.Value, node.Value);
    public override bool Equals(object obj) => this.Equals((QuadNodeF<T>)obj);
    public override int GetHashCode() => HashCode.Combine(this.Point, this.Value);

    public override string ToString() => String.Format("[{0}]: {1}", this.Point, this.Value);
  }
}