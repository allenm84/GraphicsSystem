using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsSystem
{
  public static partial class GsExtensions
  {
    private static GsVector CalculatePosition(GsSize size, float x, float y, float width, float height, GsAlignment alignment)
    {
      GsVector pos = new GsVector(x, y);

      float top = y;
      float middle = (y + (height / 2)) - (size.Height / 2);
      float bottom = (y + height) - size.Height;

      float left = x;
      float center = (x + (width / 2)) - (size.Width / 2);
      float right = (x + width) - size.Width;

      switch (alignment)
      {
        case GsAlignment.BottomCenter:
          pos.X = center;
          pos.Y = bottom;
          break;
        case GsAlignment.BottomLeft:
          pos.X = left;
          pos.Y = bottom;
          break;
        case GsAlignment.BottomRight:
          pos.X = right;
          pos.Y = bottom;
          break;

        case GsAlignment.MiddleCenter:
          pos.X = center;
          pos.Y = middle;
          break;
        case GsAlignment.MiddleLeft:
          pos.X = left;
          pos.Y = middle;
          break;
        case GsAlignment.MiddleRight:
          pos.X = right;
          pos.Y = middle;
          break;

        case GsAlignment.TopCenter:
          pos.X = center;
          pos.Y = top;
          break;
        case GsAlignment.TopLeft:
          pos.X = left;
          pos.Y = top;
          break;
        case GsAlignment.TopRight:
          pos.X = right;
          pos.Y = top;
          break;
      }

      return pos;
    }

    public static void DrawString(this IGsGraphics graphics, GsFont font, string text, float x, float y, float width, float height, GsColor color, GsAlignment alignment)
    {
      var size = GsTextMeasurer.MeasureString(font, text);
      var pos = CalculatePosition(size, x, y, width, height, alignment);
      graphics.DrawString(font, text, pos.X, pos.Y, color);
    }

    public static void DrawRectangle(this IGsGraphics graphics, GsColor color, GsRectangle rect)
    {
      graphics.DrawRectangle(color, rect.X, rect.Y, rect.Width, rect.Height);
    }

    public static void FillRectangle(this IGsGraphics graphics, GsColor color, GsRectangle rect)
    {
      graphics.FillRectangle(color, rect.X, rect.Y, rect.Width, rect.Height);
    }
  }
}
