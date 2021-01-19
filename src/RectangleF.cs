using System;

namespace Tonnes.QuadTreeF
{
  /// <summary>A <see langword="struct" /> representing a rectangle using float values</summary>
  public struct RectangleF : IEquatable<RectangleF> {
    /// <summary>X position of <see cref="RectangleF" /></summary>
    public float X { get; set; }
    /// <summary>Y position of <see cref="RectangleF" /></summary>
    public float Y { get; set; }
    /// <summary>Width of <see cref="RectangleF" /></summary>
    public float Width { get; set; }
    /// <summary>Height of <see cref="RectangleF" /></summary>
    public float Height { get; set; }

    /// <summary>Topmost Y of <see cref="RectangleF" /></summary>
    public float Top { get => this.Y; set => this.Y = value; }
    /// <summary>Bottommost Y of <see cref="RectangleF" /></summary>
    public float Bottom { get => this.Y + this.Height; set => this.Y = value - this.Height; }
    /// <summary>Leftmost X of <see cref="RectangleF" /></summary>
    public float Left { get => this.X; set => this.X = value; }
    /// <summary>Rightmost X of <see cref="RectangleF" /></summary>
    public float Right { get => this.X + this.Width; set => this.X = value - this.Width; }
    
    /// <summary>Position <see cref="PointF" /> of <see cref="RectangleF" /></summary>
    public PointF Position => new PointF(this.X, this.Y);
    /// <summary>Size <see cref="PointF" /> of <see cref="RectangleF" /></summary>
    public PointF Size => new PointF(this.Width, this.Height);
    /// <summary>Center <see cref="PointF" /> of <see cref="RectangleF" /></summary>
    public PointF Center  => new PointF(this.Right / 2, this.Bottom / 2);

    /// <summary>Returns an empty <see cref="RectangleF" /> (x: 0, y: 0, w: 0, h: 0)</summary>
    public static RectangleF Empty => new RectangleF(0, 0, 0, 0);

    /// <summary>Construct a new <see cref="RectangleF" /></summary>
    public RectangleF(float x, float y, float width, float height) => (this.X, this.Y, this.Width, this.Height) = (x, y, width, height);
    /// <summary>Construct a new <see cref="RectangleF" /></summary>
    public RectangleF(PointF position, PointF size) => (this.X, this.Y, this.Width, this.Height) = (position.X, position.Y, size.X, size.Y);

    /// <summary>Check if the <see cref="RectangleF" /> contains another <see cref="RectangleF" /></summary>
    public bool Contains(RectangleF rect) => (this.X <= rect.X && this.Right > rect.Right) && (this.Y <= rect.Y && this.Bottom > rect.Bottom);
    /// <summary>Check if the <see cref="RectangleF" /> contains a <see cref="PointF" /></summary>
    public bool Contains(PointF point) => (this.X <= point.X && this.Right > point.X) && (this.Y <= point.Y && this.Bottom > point.Y);

    /// <summary>Check if the <see cref="RectangleF" /> intersects with another <see cref="RectangleF" /></summary>
    public bool Intersects(RectangleF rect) => this.X < rect.Right && this.Right > rect.X && this.Y < rect.Bottom && this.Bottom > rect.Y;

    public bool Equals(RectangleF rect) => this.X == rect.X && this.Y == rect.Y && this.Width == rect.Width && this.Height == rect.Height;
    public override bool Equals(object obj) => this.Equals((RectangleF)obj);
    public override int GetHashCode() => HashCode.Combine(this.X, this.Y, this.Width, this.Height);

    public override string ToString() => String.Format("x: {0}, y: {1}, width: {2}, height: {3}", this.X, this.Y, this.Width, this.Height);
  }
}