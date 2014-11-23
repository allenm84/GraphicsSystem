using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsSystem
{
  public struct GsVector : IComparable<GsVector>
  {
    public static GsVector Zero { get { return new GsVector(0, 0); } }
    public static GsVector One { get { return new GsVector(1, 1); } }

    public float X, Y;

    public GsVector(float loc)
      : this(loc, loc)
    {

    }

    public GsVector(float x, float y)
    {
      X = x;
      Y = y;
    }

    public int CompareTo(GsVector other)
    {
      int result = X.CompareTo(other.X);
      if (result == 0)
      {
        result = Y.CompareTo(other.Y);
      }
      return result;
    }

    public float Length()
    {
      return (float)Math.Sqrt((double)(X * X + Y * Y));
    }

    public float LengthSquared()
    {
      return X * X + Y * Y;
    }

    public static GsVector Transform(GsVector position, GsMatrix matrix)
    {
      return new GsVector((position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41,
        (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42);
    }

    public static float Dot(GsVector value1, GsVector value2)
    {
      return value1.X * value2.X + value1.Y * value2.Y;
    }

    public static float Distance(GsVector value1, GsVector value2)
    {
      return (float)Math.Sqrt((value1.X - value2.X) * (value1.X - value2.X) + (value1.Y - value2.Y) * (value1.Y - value2.Y));
    }

    public static float DistanceSquared(GsVector value1, GsVector value2)
    {
      return (value1.X - value2.X) * (value1.X - value2.X) + (value1.Y - value2.Y) * (value1.Y - value2.Y);
    }

    public override bool Equals(object obj)
    {
      GsVector? other = obj as GsVector?;
      if (other == null) return false;
      return other.Value == this;
    }

    public override int GetHashCode()
    {
      return Y.GetHashCode() ^ X.GetHashCode();
    }

    public static GsVector operator -(GsVector value)
    {
      value.X = -value.X;
      value.Y = -value.Y;
      return value;
    }

    public static bool operator ==(GsVector value1, GsVector value2)
    {
      return value1.X == value2.X && value1.Y == value2.Y;
    }

    public static bool operator !=(GsVector value1, GsVector value2)
    {
      return value1.X != value2.X || value1.Y != value2.Y;
    }

    public static GsVector operator +(GsVector value1, GsVector value2)
    {
      value1.X += value2.X;
      value1.Y += value2.Y;
      return value1;
    }

    public static GsVector operator -(GsVector value1, GsVector value2)
    {
      value1.X -= value2.X;
      value1.Y -= value2.Y;
      return value1;
    }

    public static GsVector operator *(GsVector value1, GsVector value2)
    {
      value1.X *= value2.X;
      value1.Y *= value2.Y;
      return value1;
    }

    public static GsVector operator *(GsVector value, float scaleFactor)
    {
      value.X *= scaleFactor;
      value.Y *= scaleFactor;
      return value;
    }

    public static GsVector operator *(float scaleFactor, GsVector value)
    {
      value.X *= scaleFactor;
      value.Y *= scaleFactor;
      return value;
    }

    public static GsVector operator /(GsVector value1, GsVector value2)
    {
      value1.X /= value2.X;
      value1.Y /= value2.Y;
      return value1;
    }

    public static GsVector operator /(GsVector value1, float divider)
    {
      float factor = 1 / divider;
      value1.X *= factor;
      value1.Y *= factor;
      return value1;
    }
  }
}
