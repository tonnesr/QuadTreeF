using Microsoft.Xna.Framework;

namespace Tonnes.QuadTreeF.MonoGame
{
  public static class MonoGamePointExtensions {
    public static PointF ToPointF(this Point point) => new PointF(point.X, point.Y);
  }
}