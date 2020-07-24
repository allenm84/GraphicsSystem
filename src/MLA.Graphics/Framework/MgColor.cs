using System;
using System.Globalization;

namespace MLA.Graphics
{
  [Serializable]
  public struct MgColor : IEquatable<MgColor>
  {
    private uint packedValue;

    private MgColor(uint packedValue)
    {
      this.packedValue = packedValue;
    }

    public MgColor(MgColor rgb, byte alpha)
    {
      packedValue = 0;

      R = rgb.R;
      G = rgb.G;
      B = rgb.B;
      A = (byte)MgMath.Clamp(alpha, Byte.MinValue, Byte.MaxValue);
    }

    public MgColor(MgColor rgb, float alpha)
    {
      packedValue = 0;

      R = rgb.R;
      G = rgb.G;
      B = rgb.B;
      A = (byte)MgMath.Clamp(alpha * 255, Byte.MinValue, Byte.MaxValue);
    }

    public MgColor(int r, int g, int b)
    {
      if (((r | g | b) & -256) != 0)
      {
        r = ClampToByte64(r);
        g = ClampToByte64(g);
        b = ClampToByte64(b);
      }
      g <<= 8;
      b <<= 16;
      this.packedValue = (uint)(r | g | b | -16777216);
    }

    public MgColor(int r, int g, int b, int a)
    {
      if (((r | g | b | a) & -256) != 0)
      {
        r = ClampToByte32(r);
        g = ClampToByte32(g);
        b = ClampToByte32(b);
        a = ClampToByte32(a);
      }
      g <<= 8;
      b <<= 16;
      a <<= 24;
      this.packedValue = (uint)(r | g | b | a);
    }

    public MgColor(float r, float g, float b)
    {
      this.packedValue = PackHelper(r, g, b, 1f);
    }

    public MgColor(float r, float g, float b, float a)
    {
      this.packedValue = PackHelper(r, g, b, a);
    }

    public MgColor(MgVector3 vector)
    {
      this.packedValue = PackHelper(vector.X, vector.Y, vector.Z, 1f);
    }

    public MgColor(MgVector4 vector)
    {
      this.packedValue = PackHelper(vector.X, vector.Y, vector.Z, vector.W);
    }

    public byte R
    {
      get { return (byte)this.packedValue; }
      set { this.packedValue = ((this.packedValue & 4294967040u) | value); }
    }

    public byte G
    {
      get { return (byte)(this.packedValue >> 8); }
      set { this.packedValue = ((this.packedValue & 4294902015u) | (uint)value << 8); }
    }

    public byte B
    {
      get { return (byte)(this.packedValue >> 16); }
      set { this.packedValue = ((this.packedValue & 4278255615u) | (uint)value << 16); }
    }

    public byte A
    {
      get { return (byte)(this.packedValue >> 24); }
      set { this.packedValue = ((this.packedValue & 16777215u) | (uint)value << 24); }
    }

    public uint PackedValue
    {
      get { return this.packedValue; }
      set { this.packedValue = value; }
    }

    public static MgColor Transparent
    {
      get { return new MgColor(0u); }
    }

    public static MgColor AliceBlue
    {
      get { return new MgColor(4294965488u); }
    }

    public static MgColor AntiqueWhite
    {
      get { return new MgColor(4292340730u); }
    }

    public static MgColor Aqua
    {
      get { return new MgColor(4294967040u); }
    }

    public static MgColor Aquamarine
    {
      get { return new MgColor(4292149119u); }
    }

    public static MgColor Azure
    {
      get { return new MgColor(4294967280u); }
    }

    public static MgColor Beige
    {
      get { return new MgColor(4292670965u); }
    }

    public static MgColor Bisque
    {
      get { return new MgColor(4291093759u); }
    }

    public static MgColor Black
    {
      get { return new MgColor(4278190080u); }
    }

    public static MgColor BlanchedAlmond
    {
      get { return new MgColor(4291685375u); }
    }

    public static MgColor Blue
    {
      get { return new MgColor(4294901760u); }
    }

    public static MgColor BlueViolet
    {
      get { return new MgColor(4293012362u); }
    }

    public static MgColor Brown
    {
      get { return new MgColor(4280953509u); }
    }

    public static MgColor BurlyWood
    {
      get { return new MgColor(4287084766u); }
    }

    public static MgColor CadetBlue
    {
      get { return new MgColor(4288716383u); }
    }

    public static MgColor Chartreuse
    {
      get { return new MgColor(4278255487u); }
    }

    public static MgColor Chocolate
    {
      get { return new MgColor(4280183250u); }
    }

    public static MgColor Coral
    {
      get { return new MgColor(4283465727u); }
    }

    public static MgColor CornflowerBlue
    {
      get { return new MgColor(4293760356u); }
    }

    public static MgColor Cornsilk
    {
      get { return new MgColor(4292671743u); }
    }

    public static MgColor Crimson
    {
      get { return new MgColor(4282127580u); }
    }

    public static MgColor Cyan
    {
      get { return new MgColor(4294967040u); }
    }

    public static MgColor DarkBlue
    {
      get { return new MgColor(4287299584u); }
    }

    public static MgColor DarkCyan
    {
      get { return new MgColor(4287335168u); }
    }

    public static MgColor DarkGoldenrod
    {
      get { return new MgColor(4278945464u); }
    }

    public static MgColor DarkGray
    {
      get { return new MgColor(4289309097u); }
    }

    public static MgColor DarkGreen
    {
      get { return new MgColor(4278215680u); }
    }

    public static MgColor DarkKhaki
    {
      get { return new MgColor(4285249469u); }
    }

    public static MgColor DarkMagenta
    {
      get { return new MgColor(4287299723u); }
    }

    public static MgColor DarkOliveGreen
    {
      get { return new MgColor(4281297749u); }
    }

    public static MgColor DarkOrange
    {
      get { return new MgColor(4278226175u); }
    }

    public static MgColor DarkOrchid
    {
      get { return new MgColor(4291572377u); }
    }

    public static MgColor DarkRed
    {
      get { return new MgColor(4278190219u); }
    }

    public static MgColor DarkSalmon
    {
      get { return new MgColor(4286224105u); }
    }

    public static MgColor DarkSeaGreen
    {
      get { return new MgColor(4287347855u); }
    }

    public static MgColor DarkSlateBlue
    {
      get { return new MgColor(4287315272u); }
    }

    public static MgColor DarkSlateGray
    {
      get { return new MgColor(4283387695u); }
    }

    public static MgColor DarkTurquoise
    {
      get { return new MgColor(4291939840u); }
    }

    public static MgColor DarkViolet
    {
      get { return new MgColor(4292018324u); }
    }

    public static MgColor DeepPink
    {
      get { return new MgColor(4287829247u); }
    }

    public static MgColor DeepSkyBlue
    {
      get { return new MgColor(4294950656u); }
    }

    public static MgColor DimGray
    {
      get { return new MgColor(4285098345u); }
    }

    public static MgColor DodgerBlue
    {
      get { return new MgColor(4294938654u); }
    }

    public static MgColor Firebrick
    {
      get { return new MgColor(4280427186u); }
    }

    public static MgColor FloralWhite
    {
      get { return new MgColor(4293982975u); }
    }

    public static MgColor ForestGreen
    {
      get { return new MgColor(4280453922u); }
    }

    public static MgColor Fuchsia
    {
      get { return new MgColor(4294902015u); }
    }

    public static MgColor Gainsboro
    {
      get { return new MgColor(4292664540u); }
    }

    public static MgColor GhostWhite
    {
      get { return new MgColor(4294965496u); }
    }

    public static MgColor Gold
    {
      get { return new MgColor(4278245375u); }
    }

    public static MgColor Goldenrod
    {
      get { return new MgColor(4280329690u); }
    }

    public static MgColor Gray
    {
      get { return new MgColor(4286611584u); }
    }

    public static MgColor Green
    {
      get { return new MgColor(4278222848u); }
    }

    public static MgColor GreenYellow
    {
      get { return new MgColor(4281335725u); }
    }

    public static MgColor Honeydew
    {
      get { return new MgColor(4293984240u); }
    }

    public static MgColor HotPink
    {
      get { return new MgColor(4290013695u); }
    }

    public static MgColor IndianRed
    {
      get { return new MgColor(4284243149u); }
    }

    public static MgColor Indigo
    {
      get { return new MgColor(4286709835u); }
    }

    public static MgColor Ivory
    {
      get { return new MgColor(4293984255u); }
    }

    public static MgColor Khaki
    {
      get { return new MgColor(4287424240u); }
    }

    public static MgColor Lavender
    {
      get { return new MgColor(4294633190u); }
    }

    public static MgColor LavenderBlush
    {
      get { return new MgColor(4294308095u); }
    }

    public static MgColor LawnGreen
    {
      get { return new MgColor(4278254716u); }
    }

    public static MgColor LemonChiffon
    {
      get { return new MgColor(4291689215u); }
    }

    public static MgColor LightBlue
    {
      get { return new MgColor(4293318829u); }
    }

    public static MgColor LightCoral
    {
      get { return new MgColor(4286611696u); }
    }

    public static MgColor LightCyan
    {
      get { return new MgColor(4294967264u); }
    }

    public static MgColor LightGoldenrodYellow
    {
      get { return new MgColor(4292016890u); }
    }

    public static MgColor LightGreen
    {
      get { return new MgColor(4287688336u); }
    }

    public static MgColor LightGray
    {
      get { return new MgColor(4292072403u); }
    }

    public static MgColor LightPink
    {
      get { return new MgColor(4290885375u); }
    }

    public static MgColor LightSalmon
    {
      get { return new MgColor(4286226687u); }
    }

    public static MgColor LightSeaGreen
    {
      get { return new MgColor(4289376800u); }
    }

    public static MgColor LightSkyBlue
    {
      get { return new MgColor(4294626951u); }
    }

    public static MgColor LightSlateGray
    {
      get { return new MgColor(4288252023u); }
    }

    public static MgColor LightSteelBlue
    {
      get { return new MgColor(4292789424u); }
    }

    public static MgColor LightYellow
    {
      get { return new MgColor(4292935679u); }
    }

    public static MgColor Lime
    {
      get { return new MgColor(4278255360u); }
    }

    public static MgColor LimeGreen
    {
      get { return new MgColor(4281519410u); }
    }

    public static MgColor Linen
    {
      get { return new MgColor(4293325050u); }
    }

    public static MgColor Magenta
    {
      get { return new MgColor(4294902015u); }
    }

    public static MgColor Maroon
    {
      get { return new MgColor(4278190208u); }
    }

    public static MgColor MediumAquamarine
    {
      get { return new MgColor(4289383782u); }
    }

    public static MgColor MediumBlue
    {
      get { return new MgColor(4291624960u); }
    }

    public static MgColor MediumOrchid
    {
      get { return new MgColor(4292040122u); }
    }

    public static MgColor MediumPurple
    {
      get { return new MgColor(4292571283u); }
    }

    public static MgColor MediumSeaGreen
    {
      get { return new MgColor(4285641532u); }
    }

    public static MgColor MediumSlateBlue
    {
      get { return new MgColor(4293814395u); }
    }

    public static MgColor MediumSpringGreen
    {
      get { return new MgColor(4288346624u); }
    }

    public static MgColor MediumTurquoise
    {
      get { return new MgColor(4291613000u); }
    }

    public static MgColor MediumVioletRed
    {
      get { return new MgColor(4286911943u); }
    }

    public static MgColor MidnightBlue
    {
      get { return new MgColor(4285536537u); }
    }

    public static MgColor MintCream
    {
      get { return new MgColor(4294639605u); }
    }

    public static MgColor MistyRose
    {
      get { return new MgColor(4292994303u); }
    }

    public static MgColor Moccasin
    {
      get { return new MgColor(4290110719u); }
    }

    public static MgColor NavajoWhite
    {
      get { return new MgColor(4289584895u); }
    }

    public static MgColor Navy
    {
      get { return new MgColor(4286578688u); }
    }

    public static MgColor OldLace
    {
      get { return new MgColor(4293326333u); }
    }

    public static MgColor Olive
    {
      get { return new MgColor(4278222976u); }
    }

    public static MgColor OliveDrab
    {
      get { return new MgColor(4280520299u); }
    }

    public static MgColor Orange
    {
      get { return new MgColor(4278232575u); }
    }

    public static MgColor OrangeRed
    {
      get { return new MgColor(4278207999u); }
    }

    public static MgColor Orchid
    {
      get { return new MgColor(4292243674u); }
    }

    public static MgColor PaleGoldenrod
    {
      get { return new MgColor(4289390830u); }
    }

    public static MgColor PaleGreen
    {
      get { return new MgColor(4288215960u); }
    }

    public static MgColor PaleTurquoise
    {
      get { return new MgColor(4293848751u); }
    }

    public static MgColor PaleVioletRed
    {
      get { return new MgColor(4287852763u); }
    }

    public static MgColor PapayaWhip
    {
      get { return new MgColor(4292210687u); }
    }

    public static MgColor PeachPuff
    {
      get { return new MgColor(4290370303u); }
    }

    public static MgColor Peru
    {
      get { return new MgColor(4282353101u); }
    }

    public static MgColor Pink
    {
      get { return new MgColor(4291543295u); }
    }

    public static MgColor Plum
    {
      get { return new MgColor(4292714717u); }
    }

    public static MgColor PowderBlue
    {
      get { return new MgColor(4293320880u); }
    }

    public static MgColor Purple
    {
      get { return new MgColor(4286578816u); }
    }

    public static MgColor Red
    {
      get { return new MgColor(4278190335u); }
    }

    public static MgColor RosyBrown
    {
      get { return new MgColor(4287598524u); }
    }

    public static MgColor RoyalBlue
    {
      get { return new MgColor(4292962625u); }
    }

    public static MgColor SaddleBrown
    {
      get { return new MgColor(4279453067u); }
    }

    public static MgColor Salmon
    {
      get { return new MgColor(4285694202u); }
    }

    public static MgColor SandyBrown
    {
      get { return new MgColor(4284523764u); }
    }

    public static MgColor SeaGreen
    {
      get { return new MgColor(4283927342u); }
    }

    public static MgColor SeaShell
    {
      get { return new MgColor(4293850623u); }
    }

    public static MgColor Sienna
    {
      get { return new MgColor(4281160352u); }
    }

    public static MgColor Silver
    {
      get { return new MgColor(4290822336u); }
    }

    public static MgColor SkyBlue
    {
      get { return new MgColor(4293643911u); }
    }

    public static MgColor SlateBlue
    {
      get { return new MgColor(4291648106u); }
    }

    public static MgColor SlateGray
    {
      get { return new MgColor(4287660144u); }
    }

    public static MgColor Snow
    {
      get { return new MgColor(4294638335u); }
    }

    public static MgColor SpringGreen
    {
      get { return new MgColor(4286578432u); }
    }

    public static MgColor SteelBlue
    {
      get { return new MgColor(4290019910u); }
    }

    public static MgColor Tan
    {
      get { return new MgColor(4287411410u); }
    }

    public static MgColor Teal
    {
      get { return new MgColor(4286611456u); }
    }

    public static MgColor Thistle
    {
      get { return new MgColor(4292394968u); }
    }

    public static MgColor Tomato
    {
      get { return new MgColor(4282868735u); }
    }

    public static MgColor Turquoise
    {
      get { return new MgColor(4291878976u); }
    }

    public static MgColor Violet
    {
      get { return new MgColor(4293821166u); }
    }

    public static MgColor Wheat
    {
      get { return new MgColor(4289978101u); }
    }

    public static MgColor White
    {
      get { return new MgColor(4294967295u); }
    }

    public static MgColor WhiteSmoke
    {
      get { return new MgColor(4294309365u); }
    }

    public static MgColor Yellow
    {
      get { return new MgColor(4278255615u); }
    }

    public static MgColor YellowGreen
    {
      get { return new MgColor(4281519514u); }
    }

    #region IEquatable<MgColor> Members

    public bool Equals(MgColor other)
    {
      return this.packedValue.Equals(other.packedValue);
    }

    #endregion

    public static MgColor FromNonPremultiplied(MgVector4 vector)
    {
      MgColor result;
      result.packedValue = PackHelper(vector.X * vector.W, vector.Y * vector.W, vector.Z * vector.W, vector.W);
      return result;
    }

    public static MgColor FromNonPremultiplied(int r, int g, int b, int a)
    {
      r = ClampToByte64(r * (long)a / 255L);
      g = ClampToByte64(g * (long)a / 255L);
      b = ClampToByte64(b * (long)a / 255L);
      a = ClampToByte32(a);
      g <<= 8;
      b <<= 16;
      a <<= 24;
      MgColor result;
      result.packedValue = (uint)(r | g | b | a);
      return result;
    }

    private static uint PackHelper(float vectorX, float vectorY, float vectorZ, float vectorW)
    {
      var num = PackUNorm(255f, vectorX);
      var num2 = PackUNorm(255f, vectorY) << 8;
      var num3 = PackUNorm(255f, vectorZ) << 16;
      var num4 = PackUNorm(255f, vectorW) << 24;
      return num | num2 | num3 | num4;
    }

    private static int ClampToByte32(int value)
    {
      if (value < 0)
      {
        return 0;
      }
      if (value > 255)
      {
        return 255;
      }
      return value;
    }

    private static int ClampToByte64(long value)
    {
      if (value < 0L)
      {
        return 0;
      }
      if (value > 255L)
      {
        return 255;
      }
      return (int)value;
    }

    public MgVector3 ToVector3()
    {
      MgVector3 result;
      result.X = UnpackUNorm(255u, this.packedValue);
      result.Y = UnpackUNorm(255u, this.packedValue >> 8);
      result.Z = UnpackUNorm(255u, this.packedValue >> 16);
      return result;
    }

    public MgVector4 ToVector4()
    {
      MgVector4 result;
      result.X = UnpackUNorm(255u, this.packedValue);
      result.Y = UnpackUNorm(255u, this.packedValue >> 8);
      result.Z = UnpackUNorm(255u, this.packedValue >> 16);
      result.W = UnpackUNorm(255u, this.packedValue >> 24);
      return result;
    }

    public static MgColor Lerp(MgColor value1, MgColor value2, float amount)
    {
      var num = value1.packedValue;
      var num2 = value2.packedValue;
      int num3 = (byte)num;
      int num4 = (byte)(num >> 8);
      int num5 = (byte)(num >> 16);
      int num6 = (byte)(num >> 24);
      int num7 = (byte)num2;
      int num8 = (byte)(num2 >> 8);
      int num9 = (byte)(num2 >> 16);
      int num10 = (byte)(num2 >> 24);
      var num11 = (int)PackUNorm(65536f, amount);
      var num12 = num3 + ((num7 - num3) * num11 >> 16);
      var num13 = num4 + ((num8 - num4) * num11 >> 16);
      var num14 = num5 + ((num9 - num5) * num11 >> 16);
      var num15 = num6 + ((num10 - num6) * num11 >> 16);
      MgColor result;
      result.packedValue = (uint)(num12 | num13 << 8 | num14 << 16 | num15 << 24);
      return result;
    }

    public static MgColor Multiply(MgColor value, float scale)
    {
      var num = value.packedValue;
      uint num2 = (byte)num;
      uint num3 = (byte)(num >> 8);
      uint num4 = (byte)(num >> 16);
      uint num5 = (byte)(num >> 24);
      scale *= 65536f;
      uint num6;
      if (scale < 0f)
      {
        num6 = 0u;
      }
      else
      {
        if (scale > 16777215f)
        {
          num6 = 16777215u;
        }
        else
        {
          num6 = (uint)scale;
        }
      }
      num2 = num2 * num6 >> 16;
      num3 = num3 * num6 >> 16;
      num4 = num4 * num6 >> 16;
      num5 = num5 * num6 >> 16;
      if (num2 > 255u)
      {
        num2 = 255u;
      }
      if (num3 > 255u)
      {
        num3 = 255u;
      }
      if (num4 > 255u)
      {
        num4 = 255u;
      }
      if (num5 > 255u)
      {
        num5 = 255u;
      }
      MgColor result;
      result.packedValue = (num2 | num3 << 8 | num4 << 16 | num5 << 24);
      return result;
    }

    public static MgColor operator *(MgColor value, float scale)
    {
      var num = value.packedValue;
      uint num2 = (byte)num;
      uint num3 = (byte)(num >> 8);
      uint num4 = (byte)(num >> 16);
      uint num5 = (byte)(num >> 24);
      scale *= 65536f;
      uint num6;
      if (scale < 0f)
      {
        num6 = 0u;
      }
      else
      {
        if (scale > 16777215f)
        {
          num6 = 16777215u;
        }
        else
        {
          num6 = (uint)scale;
        }
      }
      num2 = num2 * num6 >> 16;
      num3 = num3 * num6 >> 16;
      num4 = num4 * num6 >> 16;
      num5 = num5 * num6 >> 16;
      if (num2 > 255u)
      {
        num2 = 255u;
      }
      if (num3 > 255u)
      {
        num3 = 255u;
      }
      if (num4 > 255u)
      {
        num4 = 255u;
      }
      if (num5 > 255u)
      {
        num5 = 255u;
      }
      MgColor result;
      result.packedValue = (num2 | num3 << 8 | num4 << 16 | num5 << 24);
      return result;
    }

    public override string ToString()
    {
      return String.Format(CultureInfo.CurrentCulture, "{{R:{0} G:{1} B:{2} A:{3}}}",
        this.R, this.G, this.B, this.A);
    }

    public override int GetHashCode()
    {
      return this.packedValue.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      return obj is MgColor && this.Equals((MgColor)obj);
    }

    public static bool operator ==(MgColor a, MgColor b)
    {
      return a.Equals(b);
    }

    public static bool operator !=(MgColor a, MgColor b)
    {
      return !a.Equals(b);
    }

    private static uint PackUnsigned(float bitmask, float value)
    {
      return (uint)ClampAndRound(value, 0f, bitmask);
    }

    private static uint PackSigned(uint bitmask, float value)
    {
      float num = bitmask >> 1;
      var min = -num - 1f;
      return (uint)((int)ClampAndRound(value, min, num) & (int)bitmask);
    }

    private static uint PackUNorm(float bitmask, float value)
    {
      value *= bitmask;
      return (uint)ClampAndRound(value, 0f, bitmask);
    }

    private static float UnpackUNorm(uint bitmask, uint value)
    {
      value &= bitmask;
      return value / bitmask;
    }

    private static uint PackSNorm(uint bitmask, float value)
    {
      float num = bitmask >> 1;
      value *= num;
      return (uint)((int)ClampAndRound(value, -num, num) & (int)bitmask);
    }

    private static float UnpackSNorm(uint bitmask, uint value)
    {
      var num = bitmask + 1u >> 1;
      if ((value & num) != 0u)
      {
        if ((value & bitmask) == num)
        {
          return -1f;
        }
        value |= ~bitmask;
      }
      else
      {
        value &= bitmask;
      }
      float num2 = bitmask >> 1;
      return value / num2;
    }

    private static double ClampAndRound(float value, float min, float max)
    {
      if (Single.IsNaN(value))
      {
        return 0.0;
      }
      if (Single.IsInfinity(value))
      {
        return Single.IsNegativeInfinity(value) ? min : max;
      }
      if (value < min)
      {
        return min;
      }
      if (value > max)
      {
        return max;
      }
      return Math.Round(value);
    }
  }
}