using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsSystem
{
  public static class GsTextMeasurer
  {
    private static IGsTextMeasurer sMeasurer;

    public static void Register(IGsTextMeasurer measurer)
    {
      sMeasurer = measurer;
    }

    public static GsSize MeasureString(GsFont font, string text)
    {
      return sMeasurer.MeasureString(font, text);
    }
  }
}
