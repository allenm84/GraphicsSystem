using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLA.Graphics.Drawing.Internal
{
  internal struct ColorProxy
  {
    public MgColor? Color { get; set; }
    public MgGradient Gradient { get; set; }

    public void CalculateFor(IEnumerable<MgVector2> polygon)
    {
      if (Gradient != null)
      {
        Gradient.CalculateFor(polygon);
      }
    }
  }
}