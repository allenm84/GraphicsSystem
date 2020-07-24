using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLA.Graphics.Extensions
{
  /// <summary>
  /// </summary>
  public static class MgColorExtensions
  {
    #region HLSColor Struct

    private struct HLSColor
    {
      private const int ShadowAdj = -333;
      private const int HilightAdj = 500;
      private const int WatermarkAdj = -50;
      private const int Range = 240;
      private const int HLSMax = 240;
      private const int RGBMax = 0xff;
      private const int Undefined = 160;
      private int hue;
      private int luminosity;
      private int saturation;

      public HLSColor(MgColor color)
      {
        int r = color.R;
        int g = color.G;
        int b = color.B;
        var num4 = Math.Max(Math.Max(r, g), b);
        var num5 = Math.Min(Math.Min(r, g), b);
        var num6 = num4 + num5;
        this.luminosity = ((num6 * 240) + 0xff) / 510;
        var num7 = num4 - num5;
        if (num7 == 0)
        {
          this.saturation = 0;
          this.hue = 160;
        }
        else
        {
          if (this.luminosity <= 120)
          {
            this.saturation = ((num7 * 240) + (num6 / 2)) / num6;
          }
          else
          {
            this.saturation = ((num7 * 240) + ((510 - num6) / 2)) / (510 - num6);
          }
          var num8 = (((num4 - r) * 40) + (num7 / 2)) / num7;
          var num9 = (((num4 - g) * 40) + (num7 / 2)) / num7;
          var num10 = (((num4 - b) * 40) + (num7 / 2)) / num7;
          if (r == num4)
          {
            this.hue = num10 - num9;
          }
          else if (g == num4)
          {
            this.hue = (80 + num8) - num10;
          }
          else
          {
            this.hue = (160 + num9) - num8;
          }
          if (this.hue < 0)
          {
            this.hue += 240;
          }
          if (this.hue > 240)
          {
            this.hue -= 240;
          }
        }
      }

      public int Luminosity
      {
        get { return this.luminosity; }
      }

      public MgColor Darker(float percDarker)
      {
        var num4 = 0;
        var num5 = this.NewLuma(-333, true);
        return this.ColorFromHLS(this.hue, num5 - ((int)((num5 - num4) * percDarker)), this.saturation);
      }

      public override bool Equals(object o)
      {
        if (!(o is HLSColor))
        {
          return false;
        }
        var color = (HLSColor)o;
        return ((((this.hue == color.hue) &&
          (this.saturation == color.saturation)) &&
          (this.luminosity == color.luminosity)));
      }

      public static bool operator ==(HLSColor a, HLSColor b)
      {
        return a.Equals(b);
      }

      public static bool operator !=(HLSColor a, HLSColor b)
      {
        return !a.Equals(b);
      }

      public override int GetHashCode()
      {
        return (((this.hue << 6) | (this.saturation << 2)) | this.luminosity);
      }

      public MgColor Lighter(float percLighter)
      {
        var luminosity = this.luminosity;
        var num5 = this.NewLuma(500, true);
        return this.ColorFromHLS(this.hue, luminosity + ((int)((num5 - luminosity) * percLighter)), this.saturation);
      }

      private int NewLuma(int n, bool scale)
      {
        return this.NewLuma(this.luminosity, n, scale);
      }

      private int NewLuma(int luminosity, int n, bool scale)
      {
        if (n == 0)
        {
          return luminosity;
        }
        if (scale)
        {
          if (n > 0)
          {
            return (int)(((luminosity * (0x3e8 - n)) + (0xf1L * n)) / 0x3e8L);
          }
          return ((luminosity * (n + 0x3e8)) / 0x3e8);
        }
        var num = luminosity;
        num += (int)((n * 240L) / 0x3e8L);
        if (num < 0)
        {
          num = 0;
        }
        if (num > 240)
        {
          num = 240;
        }
        return num;
      }

      private MgColor ColorFromHLS(int hue, int luminosity, int saturation)
      {
        byte num;
        byte num2;
        byte num3;
        if (saturation == 0)
        {
          num = num2 = num3 = (byte)((luminosity * 0xff) / 240);
          if (hue == 160) { }
        }
        else
        {
          int num5;
          if (luminosity <= 120)
          {
            num5 = ((luminosity * (240 + saturation)) + 120) / 240;
          }
          else
          {
            num5 = (luminosity + saturation) - (((luminosity * saturation) + 120) / 240);
          }
          var num4 = (2 * luminosity) - num5;
          num = (byte)(((this.HueToRGB(num4, num5, hue + 80) * 0xff) + 120) / 240);
          num2 = (byte)(((this.HueToRGB(num4, num5, hue) * 0xff) + 120) / 240);
          num3 = (byte)(((this.HueToRGB(num4, num5, hue - 80) * 0xff) + 120) / 240);
        }
        return new MgColor(num, num2, num3);
      }

      private int HueToRGB(int n1, int n2, int hue)
      {
        if (hue < 0)
        {
          hue += 240;
        }
        if (hue > 240)
        {
          hue -= 240;
        }
        if (hue < 40)
        {
          return (n1 + ((((n2 - n1) * hue) + 20) / 40));
        }
        if (hue < 120)
        {
          return n2;
        }
        if (hue < 160)
        {
          return (n1 + ((((n2 - n1) * (160 - hue)) + 20) / 40));
        }
        return n1;
      }
    }

    #endregion

    /// <summary>
    /// Computes the opposite color of the color passed in. The alpha doesn't change.
    /// </summary>
    /// <param name="color">The color to get the opposite of.</param>
    /// <returns>The opposite color of the color passed in.</returns>
    public static MgColor Opposite(this MgColor color)
    {
      return new MgColor(255 - color.R, 255 - color.G, 255 - color.B);
    }

    /// <summary>
    /// Returns a color that is p% lighter.
    /// </summary>
    /// <param name="color">The color to lighten.</param>
    /// <param name="p">[Optional] the percentage to lighten the color. The default is 50% (0.5f).</param>
    /// <returns>A color that is p% lighter.</returns>
    public static MgColor Light(this MgColor color, float p = 0.5f)
    {
      return new HLSColor(color).Lighter(p);
    }

    /// <summary>
    /// Returns a color that is p% darker.
    /// </summary>
    /// <param name="color">The color to darken.</param>
    /// <param name="p">[Optional] the percentage to darken the color. The default is 50% (0.5f).</param>
    /// <returns>A color that is p% darker.</returns>
    public static MgColor Dark(this MgColor color, float p = 0.5f)
    {
      return new HLSColor(color).Darker(p);
    }
  }
}