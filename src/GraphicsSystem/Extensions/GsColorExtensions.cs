using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsSystem
{
  public static partial class GsExtensions
  {
    public static double GetBrightness(this GsColor c)
    {
      return Math.Sqrt(
         c.R * c.R * .241 +
         c.G * c.G * .691 +
         c.B * c.B * .068);
    }

    public static GsColor LightOrDark(this GsColor color)
    {
      double bright = color.GetBrightness();
      if (bright < 130)
        return color.Lighten();
      else
        return color.Darken();
    }

    public static GsColor Lighten(this GsColor color, float amt = 0.5f)
    {
      return GsMath.Lerp(color, GsColor.White, amt);
    }

    public static GsColor Darken(this GsColor color, float amt = 0.5f)
    {
      return GsMath.Lerp(color, GsColor.Black, amt);
    }
  }
}
