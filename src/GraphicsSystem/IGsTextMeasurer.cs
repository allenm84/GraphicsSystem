using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsSystem
{
  public interface IGsTextMeasurer
  {
    GsSize MeasureString(GsFont font, string text);
  }
}
