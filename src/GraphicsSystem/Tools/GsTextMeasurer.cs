using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsSystem
{
  public static class GsTextMeasurer
  {
    private static Func<GsFont, string, GsSize> sMeasure;

    public static void Register(Func<GsFont, string, GsSize> measure)
    {
      sMeasure = measure;
    }

    public static GsSize MeasureString(GsFont font, string text)
    {
      return sMeasure(font, text);
    }
  }
}
