using System;
using System.Globalization;

namespace MLA.Graphics
{
  [Serializable]
  public struct MgPoint : IEquatable<MgPoint>
  {
    private static MgPoint _zero = default(MgPoint);
    public int X;
    public int Y;

    public MgPoint(int x, int y)
    {
      this.X = x;
      this.Y = y;
    }

    public static MgPoint Zero
    {
      get { return _zero; }
    }

    #region IEquatable<Point> Members

    public bool Equals(MgPoint other)
    {
      return this.X == other.X && this.Y == other.Y;
    }

    #endregion

    public override bool Equals(object obj)
    {
      var result = false;
      if (obj is MgPoint)
      {
        result = this.Equals((MgPoint)obj);
      }
      return result;
    }

    public override int GetHashCode()
    {
      return this.X.GetHashCode() + this.Y.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format(CultureInfo.CurrentCulture, "{{X:{0} Y:{1}}}", this.X, this.Y);
    }

    public static bool operator ==(MgPoint a, MgPoint b)
    {
      return a.Equals(b);
    }

    public static bool operator !=(MgPoint a, MgPoint b)
    {
      return a.X != b.X || a.Y != b.Y;
    }
  }
}