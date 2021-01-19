using System;

namespace Tonnes.QuadTreeF 
{
  /// <summary>A <see langword="struct" /> representing a 2D point of float values</summary>
  public struct PointF : IEquatable<PointF>, IEquatable<(float x, float y)> {
    private static readonly PointF _zero = new PointF();
    private static readonly PointF _one = new PointF(1f);

    /// <summary>A (x: 0, y: 0) point</summary>
    public PointF Zero => PointF._zero;
    /// <summary>A (x: 1, y: 1) point</summary>
    public PointF One => PointF._one;

    /// <summary>X value of <see cref="PointF"/></summary>
    public float X { get; set; }
    /// <summary>Y value of <see cref="PointF"/></summary>
    public float Y { get; set; }

    ///<summary>Constructs a <see cref="PointF"/> using floats</summary>
    public PointF(float x, float y) => (this.X, this.Y) = (x, y);
    /// <summary>Constructs a <see cref="PointF"/> using a single float value. Results in a <see cref="PointF"/> with the same value in <see cref="PointF.X"/> and <see cref="PointF.Y"/>. </summary>
    public PointF(float xy) : this(xy, xy) {}

    /// <summary>Convert a <see cref="ValueTuple"/> to <see cref="PointF" />.</summary>
    public static explicit operator PointF((float x, float y) point) => new PointF(point.x, point.y);

    /// <summary>Negates <see cref="PointF" /> (-PointF)</summary>
    public static PointF operator -(PointF value) => PointF.Negate(value);

    /// <summary>Add two <see cref="PointF" /></summary>
    public static PointF operator +(PointF value, PointF value2) => PointF.Add(value, value2);

    /// <summary>Subtract <see cref="PointF" /> from <see cref="PointF" /></summary>
    public static PointF operator -(PointF value, PointF value2) => PointF.Subtract(value, value2);

    /// <summary>Multiply <see cref="PointF" /> with <see cref="PointF" /></summary>
    public static PointF operator *(PointF value, PointF value2) => PointF.Multiply(value, value2);

    /// <summary>Divide <see cref="PointF" /> by <see cref="PointF" /></summary>
    public static PointF operator /(PointF value, PointF value2) => PointF.Divide(value, value2);

    public static bool operator ==(PointF value, PointF value2) => value.X == value2.X && value.Y == value2.Y;
    public static bool operator !=(PointF value, PointF value2) => value.X != value2.X || value.Y != value2.Y;

    /// <summary>Negates <see cref="PointF" /> (-PointF)</summary>
    public void Negate() => this = PointF.Negate(this);
    /// <summary>Negates <see cref="PointF" /> (-PointF)</summary>
    public static PointF Negate(PointF point) {
      point.X = -point.X;
      point.Y = -point.Y;
      return point;
    }

    /// <summary>Subtract <see cref="PointF" /> with <see cref="PointF" /></summary>
    public void Subtract(PointF point) => this = PointF.Subtract(this, point);
    /// <summary>Subtract <see cref="PointF" /> with <see cref="PointF" /></summary>
    public static PointF Subtract(PointF value, PointF value2) {
      value.X -= value2.X;
      value.Y -= value2.Y;
      return value;
    }
    
    /// <summary>Add <see cref="PointF" /> and <see cref="PointF" /></summary>
    public void Add(PointF point) => this = PointF.Add(this, point);
    /// <summary>Add <see cref="PointF" /> and <see cref="PointF" /></summary>
    public static PointF Add(PointF value, PointF value2) {
      value.X += value2.X;
      value.Y += value2.Y;
      return value;
    }

    /// <summary>Multiply <see cref="PointF" /> with <see cref="PointF" /></summary>
    public void Multiply(PointF point) => this = PointF.Multiply(this, point);
    /// <summary>Multiply <see cref="PointF" /> with <see cref="PointF" /></summary>
    public static PointF Multiply(PointF value, PointF value2) {
      value.X *= value2.X;
      value.Y *= value2.Y;
      return value;
    }

    /// <summary>Divide <see cref="PointF" /> by <see cref="PointF" /></summary>
    public void Divide(PointF point) => this = PointF.Divide(this, point);
    /// <summary>Divide <see cref="PointF" /> by <see cref="PointF" /></summary>
    public static PointF Divide(PointF value, PointF value2) {
      value.X /= value2.X;
      value.Y /= value2.Y;
      return value;
    }

    /// <summary>Ceiling X and Y values of <see cref="PointF" /></summary>
    public void Ceiling() => this = PointF.Ceiling(this);
    /// <summary>Ceiling X and Y values of <see cref="PointF" /></summary>
    public static PointF Ceiling(PointF point) {
      point.X = MathF.Ceiling(point.X);
      point.Y = MathF.Ceiling(point.Y);
      return point;
    }

    /// <summary>Floor X and Y values of <see cref="PointF" /></summary>
    public void Floor() => this = PointF.Floor(this);
    /// <summary>Floor X and Y values of <see cref="PointF" /></summary>
    public static PointF Floor(PointF point) {
      point.X = MathF.Floor(point.X);
      point.Y = MathF.Floor(point.Y);
      return point;
    }

    /// <summary>Round X and Y values of <see cref="PointF" /></summary>
    public void Round() => this = PointF.Round(this);
    /// <summary>Round X and Y values of <see cref="PointF" /></summary>
    /// <param name="point"><see cref="PointF" /> to round</param>
    public static PointF Round(PointF point) {
      point.X = MathF.Round(point.X);
      point.Y = MathF.Round(point.Y);
      return point;
    }

    /// <summary>Convert <see cref="PointF" /> to a <see cref="ValueTuple"/>.</summary>
    public (float x, float y) ToValueTuple() => (this.X, this.Y);

    public bool Equals(PointF point) => this.X == point.X && this.Y == point.Y;
    public bool Equals((float x, float y) point) => this.X == point.x && this.Y == point.y;
    public override bool Equals(object obj) => this.Equals((PointF)obj);
    public override int GetHashCode() => HashCode.Combine(this.X, this.Y);

    public override string ToString() => String.Format("x: {0}, y: {1}", this.X, this.Y);
  }
}