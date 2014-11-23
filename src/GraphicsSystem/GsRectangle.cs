using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsSystem
{
  public class GsRectangle
  {
    public static GsRectangle Empty { get { return new GsRectangle(0, 0, 0, 0); } }

    public float X, Y, Width, Height;

    public float Left { get { return X; } }
    public float Top { get { return Y; } }
    public float Right { get { return X + Width; } }
    public float Bottom { get { return Y + Height; } }

    public GsVector Location { get { return new GsVector(X, Y); } }
    public GsSize Size { get { return new GsSize(Width, Height); } }

    public GsVector Center
    {
      get { return new GsVector((X + Right) / 2, (Y + Bottom) / 2); }
      set
      {
        var l = Location + (value - Center);
        X = l.X;
        Y = l.Y;
      }
    }

    public bool IsEmpty
    {
      get
      {
        if (this.Width > 0f)
        {
          return (this.Height <= 0f);
        }
        return true;
      }
    }

    public GsRectangle(GsVector location, GsSize size)
      : this(location.X, location.Y, size.Width, size.Height)
    {

    }

    public GsRectangle(float x, float y, float width, float height)
    {
      X = x;
      Y = y;
      Width = width;
      Height = height;
    }

    public void Offset(GsVector pos)
    {
      Offset(pos.X, pos.Y);
    }

    public void Offset(float x, float y)
    {
      X += x;
      Y += y;
    }

    public bool IntersectsWith(GsRectangle rect)
    {
      return (rect.X < (Right)) && (X < (rect.Right)) && (rect.Y < (Bottom)) && (Y < (rect.Bottom));
    }

    public bool Contains(float x, float y)
    {
      return (X <= x) && (x < Right) && (Y <= y) && (y < Bottom);
    }

    public bool Contains(GsVector pt)
    {
      return Contains(pt.X, pt.Y);
    }

    public bool Contains(GsRectangle rect)
    {
      return (X <= rect.X) && (rect.Right <= Right) && (Y <= rect.Y) && (rect.Bottom <= Bottom);
    }

    public static GsRectangle Offset(GsRectangle box, float dx, float dy)
    {
      return new GsRectangle(box.X + dx, box.Y + dy, box.Width, box.Height);
    }

    public static GsRectangle Offset(GsRectangle box, GsVector offset)
    {
      return new GsRectangle(box.Location + offset, box.Size);
    }

    public static GsRectangle FromLTRB(float left, float top, float right, float bottom)
    {
      return new GsRectangle(left, top, right - left, bottom - top);
    }
  }
}
