using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsSystem
{
  public interface IGsGraphics
  {
    void DrawLine(GsColor color, float x0, float y0, float x1, float y1);

    void DrawRectangle(GsColor color, float x, float y, float width, float height);
    void FillRectangle(GsColor color, float x, float y, float width, float height);

    void DrawEllipse(GsColor color, float x, float y, float width, float height);
    void FillEllipse(GsColor color, float x, float y, float width, float height);

    void DrawPolygon(GsColor color, GsVector[] pts);
    void FillPolygon(GsColor color, GsVector[] pts);

    void DrawString(GsFont font, string text, float x, float y, GsColor color);
    void DrawImage(GsImage image, float x, float y, float width, float height, GsColor tint);
  }
}
