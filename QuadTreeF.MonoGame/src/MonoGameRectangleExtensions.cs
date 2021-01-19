using Microsoft.Xna.Framework;

namespace Tonnes.QuadTreeF.MonoGame
{
  public static class MonoGameRectangleExtensions {
    public static RectangleF ToRectangleF(this Rectangle rect) => new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
  }
}