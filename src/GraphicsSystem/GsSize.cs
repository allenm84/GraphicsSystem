using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsSystem
{
  public struct GsSize
  {
    public float Width, Height;

    public bool IsEmpty
    {
      get
      {
        if (Width > 0f)
        {
          return (Height <= 0f);
        }
        return true;
      }
    }

    public GsSize(float width, float height)
    {
      Width = width;
      Height = height;
    }

    public GsVector ToVector()
    {
      return new GsVector(Width, Height);
    }
  }
}
