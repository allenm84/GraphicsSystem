using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsSystem
{
  public static class GsMath
  {
    public const float E = (float)Math.E;
    public const float Log10E = 0.4342945f;
    public const float Log2E = 1.442695f;
    public const float Pi = (float)Math.PI;
    public const float PiOver2 = (float)(Math.PI / 2.0);
    public const float PiOver4 = (float)(Math.PI / 4.0);
    public const float TwoPi = (float)(Math.PI * 2.0);

    public static float ToDegrees(float radians)
    {
      return (float)(radians * 57.295779513082320876798154814105);
    }

    public static float ToRadians(float degrees)
    {
      return (float)(degrees * 0.017453292519943295769236907684886);
    }

    public static float WrapAngle(float angle)
    {
      while (angle < -Pi)
      {
        angle += TwoPi;
      }
      while (angle > Pi)
      {
        angle -= TwoPi;
      }
      return angle;
    }

    public static bool InRange(int value, int min, int max)
    {
      return min <= value && value <= max;
    }

    public static float Cos(float value)
    {
      return (float)Math.Cos(value);
    }

    public static float Sin(float value)
    {
      return (float)Math.Sin(value);
    }

    public static float Lerp(float start, float end, float amount)
    {
      amount = Clamp(amount, 0, 1);
      return (start * (1 - amount) + end * amount);
    }

    public static float SmoothStep(float start, float end, float amount)
    {
      amount = Clamp(amount, 0, 1);
      return Lerp(start, end, amount * amount * (3f - 2f * amount));
    }

    public static float Coserp(float start, float end, float amount)
    {
      float amt = (float)(1 - Math.Cos(amount * Math.PI)) / 2f;
      return (start * (1 - amt) + end * amt);
    }

    public static GsColor Interpolate(GsColor c1, GsColor c2, float amount, Func<float, float, float, float> op)
    {
      // start colours as lerp-able floats
      float sr = c1.R, sg = c1.G, sb = c1.B;

      // end colours as lerp-able floats
      float er = c2.R, eg = c2.G, eb = c2.B;

      // lerp the colours to get the difference
      byte r = (byte)op(sr, er, amount),
        g = (byte)op(sg, eg, amount),
        b = (byte)op(sb, eb, amount);

      // return the new colour
      return new GsColor(r, g, b);
    }

    public static GsColor Lerp(GsColor c1, GsColor c2, float amount)
    {
      return Interpolate(c1, c2, amount, Lerp);
    }

    public static GsColor SmoothStep(GsColor c1, GsColor c2, float amount)
    {
      return Interpolate(c1, c2, amount, SmoothStep);
    }

    public static GsColor Coserp(GsColor c1, GsColor c2, float amount)
    {
      return Interpolate(c1, c2, amount, Coserp);
    }

    public static int Clamp(int value, int min, int max)
    {
      return Math.Max(min, Math.Min(value, max));
    }

    public static float Clamp(float value, float min, float max)
    {
      return Math.Max(min, Math.Min(value, max));
    }
  }
}
