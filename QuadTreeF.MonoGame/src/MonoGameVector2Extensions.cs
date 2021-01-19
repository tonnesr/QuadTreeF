using Microsoft.Xna.Framework;

namespace Tonnes.QuadTreeF.MonoGame
{
  public static class MonoGameVector2Extensions {
    public static PointF ToPointF(this Vector2 vector) => new PointF(vector.X, vector.Y);
  }
}