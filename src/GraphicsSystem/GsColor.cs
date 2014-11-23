using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsSystem
{
  public struct GsColor
  {
    public static readonly GsColor Maroon = new GsColor(128, 0, 0);
    public static readonly GsColor DarkRed = new GsColor(139, 0, 0);
    public static readonly GsColor Brown = new GsColor(165, 42, 42);
    public static readonly GsColor Firebrick = new GsColor(178, 34, 34);
    public static readonly GsColor Crimson = new GsColor(220, 20, 60);
    public static readonly GsColor Red = new GsColor(255, 0, 0);
    public static readonly GsColor Tomato = new GsColor(255, 99, 71);
    public static readonly GsColor Coral = new GsColor(255, 127, 80);
    public static readonly GsColor IndianRed = new GsColor(205, 92, 92);
    public static readonly GsColor LightCoral = new GsColor(240, 128, 128);
    public static readonly GsColor DarkSalmon = new GsColor(233, 150, 122);
    public static readonly GsColor Salmon = new GsColor(250, 128, 114);
    public static readonly GsColor LightSalmon = new GsColor(255, 160, 122);
    public static readonly GsColor OrangeRed = new GsColor(255, 69, 0);
    public static readonly GsColor DarkOrange = new GsColor(255, 140, 0);
    public static readonly GsColor Orange = new GsColor(255, 165, 0);
    public static readonly GsColor Gold = new GsColor(255, 215, 0);
    public static readonly GsColor DarkGoldenRod = new GsColor(184, 134, 11);
    public static readonly GsColor GoldenRod = new GsColor(218, 165, 32);
    public static readonly GsColor PaleGoldenRod = new GsColor(238, 232, 170);
    public static readonly GsColor DarkKhaki = new GsColor(189, 183, 107);
    public static readonly GsColor Khaki = new GsColor(240, 230, 140);
    public static readonly GsColor Olive = new GsColor(128, 128, 0);
    public static readonly GsColor Yellow = new GsColor(255, 255, 0);
    public static readonly GsColor YellowGreen = new GsColor(154, 205, 50);
    public static readonly GsColor DarkOliveGreen = new GsColor(85, 107, 47);
    public static readonly GsColor OliveDrab = new GsColor(107, 142, 35);
    public static readonly GsColor LawnGreen = new GsColor(124, 252, 0);
    public static readonly GsColor ChartReuse = new GsColor(127, 255, 0);
    public static readonly GsColor GreenYellow = new GsColor(173, 255, 47);
    public static readonly GsColor DarkGreen = new GsColor(0, 100, 0);
    public static readonly GsColor Green = new GsColor(0, 128, 0);
    public static readonly GsColor ForestGreen = new GsColor(34, 139, 34);
    public static readonly GsColor Lime = new GsColor(0, 255, 0);
    public static readonly GsColor LimeGreen = new GsColor(50, 205, 50);
    public static readonly GsColor LightGreen = new GsColor(144, 238, 144);
    public static readonly GsColor PaleGreen = new GsColor(152, 251, 152);
    public static readonly GsColor DarkSeaGreen = new GsColor(143, 188, 143);
    public static readonly GsColor MediumSpringGreen = new GsColor(0, 250, 154);
    public static readonly GsColor SpringGreen = new GsColor(0, 255, 127);
    public static readonly GsColor SeaGreen = new GsColor(46, 139, 87);
    public static readonly GsColor MediumAquaMarine = new GsColor(102, 205, 170);
    public static readonly GsColor MediumSeaGreen = new GsColor(60, 179, 113);
    public static readonly GsColor LightSeaGreen = new GsColor(32, 178, 170);
    public static readonly GsColor DarkSlateGray = new GsColor(47, 79, 79);
    public static readonly GsColor Teal = new GsColor(0, 128, 128);
    public static readonly GsColor DarkCyan = new GsColor(0, 139, 139);
    public static readonly GsColor Aqua = new GsColor(0, 255, 255);
    public static readonly GsColor Cyan = new GsColor(0, 255, 255);
    public static readonly GsColor LightCyan = new GsColor(224, 255, 255);
    public static readonly GsColor DarkTurquoise = new GsColor(0, 206, 209);
    public static readonly GsColor Turquoise = new GsColor(64, 224, 208);
    public static readonly GsColor MediumTurquoise = new GsColor(72, 209, 204);
    public static readonly GsColor PaleTurquoise = new GsColor(175, 238, 238);
    public static readonly GsColor AquaMarine = new GsColor(127, 255, 212);
    public static readonly GsColor PowderBlue = new GsColor(176, 224, 230);
    public static readonly GsColor CadetBlue = new GsColor(95, 158, 160);
    public static readonly GsColor SteelBlue = new GsColor(70, 130, 180);
    public static readonly GsColor CornFlowerBlue = new GsColor(100, 149, 237);
    public static readonly GsColor DeepSkyBlue = new GsColor(0, 191, 255);
    public static readonly GsColor DodgerBlue = new GsColor(30, 144, 255);
    public static readonly GsColor LightBlue = new GsColor(173, 216, 230);
    public static readonly GsColor SkyBlue = new GsColor(135, 206, 235);
    public static readonly GsColor LightSkyBlue = new GsColor(135, 206, 250);
    public static readonly GsColor MidnightBlue = new GsColor(25, 25, 112);
    public static readonly GsColor Navy = new GsColor(0, 0, 128);
    public static readonly GsColor DarkBlue = new GsColor(0, 0, 139);
    public static readonly GsColor MediumBlue = new GsColor(0, 0, 205);
    public static readonly GsColor Blue = new GsColor(0, 0, 255);
    public static readonly GsColor RoyalBlue = new GsColor(65, 105, 225);
    public static readonly GsColor BlueViolet = new GsColor(138, 43, 226);
    public static readonly GsColor Indigo = new GsColor(75, 0, 130);
    public static readonly GsColor DarkSlateBlue = new GsColor(72, 61, 139);
    public static readonly GsColor SlateBlue = new GsColor(106, 90, 205);
    public static readonly GsColor MediumSlateBlue = new GsColor(123, 104, 238);
    public static readonly GsColor MediumPurple = new GsColor(147, 112, 219);
    public static readonly GsColor DarkMagenta = new GsColor(139, 0, 139);
    public static readonly GsColor DarkViolet = new GsColor(148, 0, 211);
    public static readonly GsColor DarkOrchid = new GsColor(153, 50, 204);
    public static readonly GsColor MediumOrchid = new GsColor(186, 85, 211);
    public static readonly GsColor Purple = new GsColor(128, 0, 128);
    public static readonly GsColor Thistle = new GsColor(216, 191, 216);
    public static readonly GsColor Plum = new GsColor(221, 160, 221);
    public static readonly GsColor Violet = new GsColor(238, 130, 238);
    public static readonly GsColor Magenta = new GsColor(255, 0, 255);
    public static readonly GsColor Fuchsia = new GsColor(255, 0, 255);
    public static readonly GsColor Orchid = new GsColor(218, 112, 214);
    public static readonly GsColor MediumVioletRed = new GsColor(199, 21, 133);
    public static readonly GsColor PaleVioletRed = new GsColor(219, 112, 147);
    public static readonly GsColor DeepPink = new GsColor(255, 20, 147);
    public static readonly GsColor HotPink = new GsColor(255, 105, 180);
    public static readonly GsColor LightPink = new GsColor(255, 182, 193);
    public static readonly GsColor Pink = new GsColor(255, 192, 203);
    public static readonly GsColor AntiqueWhite = new GsColor(250, 235, 215);
    public static readonly GsColor Beige = new GsColor(245, 245, 220);
    public static readonly GsColor Bisque = new GsColor(255, 228, 196);
    public static readonly GsColor BlanchedAlmond = new GsColor(255, 235, 205);
    public static readonly GsColor Wheat = new GsColor(245, 222, 179);
    public static readonly GsColor CornSilk = new GsColor(255, 248, 220);
    public static readonly GsColor LemonChiffon = new GsColor(255, 250, 205);
    public static readonly GsColor LightGoldenRodYellow = new GsColor(250, 250, 210);
    public static readonly GsColor LightYellow = new GsColor(255, 255, 224);
    public static readonly GsColor SaddleBrown = new GsColor(139, 69, 19);
    public static readonly GsColor Sienna = new GsColor(160, 82, 45);
    public static readonly GsColor Chocolate = new GsColor(210, 105, 30);
    public static readonly GsColor Peru = new GsColor(205, 133, 63);
    public static readonly GsColor SandyBrown = new GsColor(244, 164, 96);
    public static readonly GsColor BurlyWood = new GsColor(222, 184, 135);
    public static readonly GsColor Tan = new GsColor(210, 180, 140);
    public static readonly GsColor RosyBrown = new GsColor(188, 143, 143);
    public static readonly GsColor Moccasin = new GsColor(255, 228, 181);
    public static readonly GsColor NavajoWhite = new GsColor(255, 222, 173);
    public static readonly GsColor PeachPuff = new GsColor(255, 218, 185);
    public static readonly GsColor MistyRose = new GsColor(255, 228, 225);
    public static readonly GsColor LavenderBlush = new GsColor(255, 240, 245);
    public static readonly GsColor Linen = new GsColor(250, 240, 230);
    public static readonly GsColor OldLace = new GsColor(253, 245, 230);
    public static readonly GsColor PapayaWhip = new GsColor(255, 239, 213);
    public static readonly GsColor SeaShell = new GsColor(255, 245, 238);
    public static readonly GsColor MintCream = new GsColor(245, 255, 250);
    public static readonly GsColor SlateGray = new GsColor(112, 128, 144);
    public static readonly GsColor LightSlateGray = new GsColor(119, 136, 153);
    public static readonly GsColor LightSteelBlue = new GsColor(176, 196, 222);
    public static readonly GsColor Lavender = new GsColor(230, 230, 250);
    public static readonly GsColor FloralWhite = new GsColor(255, 250, 240);
    public static readonly GsColor AliceBlue = new GsColor(240, 248, 255);
    public static readonly GsColor GhostWhite = new GsColor(248, 248, 255);
    public static readonly GsColor Honeydew = new GsColor(240, 255, 240);
    public static readonly GsColor Ivory = new GsColor(255, 255, 240);
    public static readonly GsColor Azure = new GsColor(240, 255, 255);
    public static readonly GsColor Snow = new GsColor(255, 250, 250);
    public static readonly GsColor Black = new GsColor(0, 0, 0);
    public static readonly GsColor DimGray = new GsColor(105, 105, 105);
    public static readonly GsColor DimGrey = new GsColor(105, 105, 105);
    public static readonly GsColor Gray = new GsColor(128, 128, 128);
    public static readonly GsColor Grey = new GsColor(128, 128, 128);
    public static readonly GsColor DarkGray = new GsColor(169, 169, 169);
    public static readonly GsColor DarkGrey = new GsColor(169, 169, 169);
    public static readonly GsColor Silver = new GsColor(192, 192, 192);
    public static readonly GsColor LightGray = new GsColor(211, 211, 211);
    public static readonly GsColor LightGrey = new GsColor(211, 211, 211);
    public static readonly GsColor Gainsboro = new GsColor(220, 220, 220);
    public static readonly GsColor WhiteSmoke = new GsColor(245, 245, 245);
    public static readonly GsColor White = new GsColor(255, 255, 255);

    public byte A, R, G, B;

    public GsColor(GsColor rgb, byte a)
      : this(a, rgb.R, rgb.G, rgb.B)
    {

    }

    public GsColor(byte r, byte g, byte b)
      : this(255, r, g, b)
    {

    }

    public GsColor(byte a, byte r, byte g, byte b)
    {
      A = a;
      R = r;
      G = g;
      B = b;
    }
  }
}
