[![NuGet](https://img.shields.io/nuget/v/Tonnes.QuadTreeF.MonoGame?color=blue)](https://www.nuget.org/packages/Tonnes.QuadTreeF.MonoGame/)

# QuadTreeF.MonoGame

MonoGame compatibility layer for QuadTreeF

## Extensions

```c#

  // From Tonnes.QuadTreeF.RectangleF To Microsoft.Xna.Framework.Rectangle
  Rectangle ToRectangle(this RectangleF rect);
  
  // From Tonnes.QuadTreeF.PointF To Microsoft.Xna.Framework.Point
  Point ToPoint(this PointF point);
  
  // From Tonnes.QuadTreeF.PointF To Microsoft.Xna.Framework.Vector2
  Vector2 ToVector2(this PointF point);

  // From Microsoft.Xna.Framework.Point To Tonnes.QuadTreeF.PointF
  PointF ToPointF(this Point point);
  
  // From Microsoft.Xna.Framework.Vector2 To Tonnes.QuadTreeF.PointF
  PointF ToPointF(this Vector2 vector);

  // From Microsoft.Xna.Framework.Rectangle To Tonnes.QuadTreeF.RectangleF
  RectangleF ToRectangleF(this Rectangle rect);

```