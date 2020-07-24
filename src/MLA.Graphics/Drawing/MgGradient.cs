using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLA.Graphics.Drawing
{
  /// <summary>
  /// An abstract base class containing the two colors to blend between.
  /// </summary>
  public abstract class MgGradient
  {
    /// <summary>
    /// Constructs a new gradient from two colors. This should never be called directly.
    /// </summary>
    /// <param name="color1">The starting color.</param>
    /// <param name="color2">The ending color.</param>
    public MgGradient(MgColor color1, MgColor color2)
    {
      Color1 = color1;
      Color2 = color2;
    }

    /// <summary>
    /// Gets or sets the starting color.
    /// </summary>
    public MgColor Color1 { get; set; }

    /// <summary>
    /// Gets or sets the ending color.
    /// </summary>
    public MgColor Color2 { get; set; }

    internal virtual void CalculateFor(IEnumerable<MgVector2> polygon)
    {
      // do nothing!
    }

    /// <summary>
    /// Calculates the color for a gradient for the specified position
    /// </summary>
    /// <param name="x">The x position.</param>
    /// <param name="y">The y position.</param>
    internal abstract MgColor GetColorAt(int x, int y);
  }
}