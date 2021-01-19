using Microsoft.Xna.Framework;

namespace Tonnes.QuadTreeF.MonoGame
{
  public static class PointFExtensions {
    public static Point ToPoint(this PointF point) => new Point((int)point.X, (int)point.Y);
    public static Vector2 ToVector2(this PointF point) => new Vector2(point.X, point.Y);
  }
}