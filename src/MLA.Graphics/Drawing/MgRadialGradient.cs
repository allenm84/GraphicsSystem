using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MLA.Graphics.Extensions;

namespace MLA.Graphics.Drawing
{
  /// <summary>
  /// Contains the data needed to draw a radial gradient.
  /// </summary>
  public class MgRadialGradient : MgGradient
  {
    /// <summary>
    /// Creates a new radial gradient.
    /// </summary>
    /// <param name="radius">The radius of the gradient.</param>
    /// <param name="center">The center of the gradient.</param>
    /// <param name="color1">The inner color to use for the gradient.</param>
    /// <param name="color2">The outer color to use for the gradient.</param>
    public MgRadialGradient(float radius, MgVector2 center, MgColor color1, MgColor color2)
      : base(color1, color2)
    {
      Radius = radius;
      Center = center;
    }

    /// <summary>
    /// Gets the radius of the radial gradient.
    /// </summary>
    public float Radius { get; internal set; }

    /// <summary>
    /// Gets the center of the radial gradient.
    /// </summary>
    public MgVector2 Center { get; internal set; }

    internal override MgColor GetColorAt(int x, int y)
    {
      // X = (cx), Y = (cy), Z = (radius), W = 0
      var pixelPos = new MgVector2(x, y);
      var dist = (pixelPos - Center).Length();

      var mu = dist / Radius;
      return MgMath.Lerp(Color1, Color2, mu);
    }
  }
}