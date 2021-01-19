using Microsoft.Xna.Framework;

namespace Tonnes.QuadTreeF.MonoGame
{
  public static class RectangleFExtensions {
    public static Rectangle ToRectangle(this RectangleF rect) => new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
  }
}