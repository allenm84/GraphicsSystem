using System;

namespace MLA.Graphics
{
  /// <summary>
  /// Stores a set of four floating-point numbers that represent the location and size of a box.
  /// </summary>
  [Serializable]
  public struct MgRectangleF
  {
    /// <summary>
    /// Represents an uninitialized instance of a BoxF.
    /// </summary>
    public static readonly MgRectangleF Empty;

    /// <summary>
    /// Gets or sets the upper-left x-coordinate of this BoxF.
    /// </summary>
    public float X;

    /// <summary>
    /// Gets or sets the upper-left y-coordinate of this BoxF.
    /// </summary>
    public float Y;

    /// <summary>
    /// Gets or set the width of this BoxF.
    /// </summary>
    public float Width;

    /// <summary>
    /// Gets or sets the height of this BoxF.
    /// </summary>
    public float Height;

    static MgRectangleF()
    {
      Empty = new MgRectangleF();
    }

    /// <summary>
    /// Creates a new instance of the BoxF class with the specified location and size.
    /// </summary>
    /// <param name="location">The upper-left corner of the box.</param>
    /// <param name="size">The size of the box.</param>
    public MgRectangleF(MgVector2 location, MgSizeF size)
      : this(location.X, location.Y, size.Width, size.Height) {}

    /// <summary>
    /// Creates a new instance of the BoxF class with the specified location and size.
    /// </summary>
    /// <param name="location">The upper-left corner of the box.</param>
    /// <param name="size">The size of the box.</param>
    public MgRectangleF(MgVector2 location, MgVector2 size)
      : this(location.X, location.Y, size.X, size.Y) {}

    /// <summary>
    /// Creates a new instance of the BoxF class with the specified location and size.
    /// </summary>
    /// <param name="location">The upper-left corner of the box.</param>
    /// <param name="width">The width of the box.</param>
    /// <param name="height">The height of the box.</param>
    public MgRectangleF(MgVector2 location, float width, float height)
      : this(location.X, location.Y, width, height) {}

    /// <summary>
    /// Creates a new instance of the BoxF class with the specified location and size.
    /// </summary>
    /// <param name="x">The x-coordinate of the upper-left corner of the box.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the box.</param>
    /// <param name="width">The width of the box.</param>
    /// <param name="height">The height of the box.</param>
    public MgRectangleF(float x, float y, float width, float height)
    {
      X = x;
      Y = y;
      Width = width;
      Height = height;
    }

    /// <summary>
    /// Gets or sets the coordinates of the upper-left corner of this BoxF.
    /// </summary>
    public MgVector2 Location
    {
      get { return new MgVector2(X, Y); }
      set
      {
        X = value.X;
        Y = value.Y;
      }
    }

    /// <summary>
    /// Gets or sets the size of this BoxF.
    /// </summary>
    public MgSizeF Size
    {
      get { return new MgSizeF(Width, Height); }
      set
      {
        Width = value.Width;
        Height = value.Height;
      }
    }

    /// <summary>
    /// Gets the upper-left x-coordinate of this BoxF.
    /// </summary>
    public float Left
    {
      get { return X; }
    }

    /// <summary>
    /// Gets the upper-left y-coordinate of this BoxF.
    /// </summary>
    public float Top
    {
      get { return Y; }
    }

    /// <summary>
    /// Gets the lower-right x-coordinate of this BoxF.
    /// </summary>
    public float Right
    {
      get { return (X + Width); }
    }

    /// <summary>
    /// Gets the lower-right y-coordinate of this BoxF.
    /// </summary>
    public float Bottom
    {
      get { return (Y + Height); }
    }

    /// <summary>
    /// Gets the center of this Box.
    /// </summary>
    public MgVector2 Center
    {
      get { return new MgVector2((X + Right) / 2, (Y + Bottom) / 2); }
      set { Location += value - Center; }
    }

    /// <summary>
    /// Tests whether the Width or Height property of this BoxF has a less than or equal to 0.
    /// </summary>
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

    /// <summary>
    /// Creates a BoxF structure with upper-left corner and lower-right corner at the specified locations.
    /// </summary>
    /// <param name="left">The x-coordinate of the upper-left corner of the box.</param>
    /// <param name="top">The y-coordinate of the upper-left corner of the box.</param>
    /// <param name="right">The x-coordinate of the lower-right corner of the box.</param>
    /// <param name="bottom">The y-coordinate of the lower-right corner of the box.</param>
    /// <returns>A new box created from the coordinates.</returns>
    public static MgRectangleF FromLTRB(float left, float top, float right, float bottom)
    {
      return new MgRectangleF(left, top, right - left, bottom - top);
    }

    /// <summary>
    /// Creates a BoxF structre with the specified center and size.
    /// </summary>
    /// <param name="center">The center of the box.</param>
    /// <param name="size">The size of the box.</param>
    /// <returns>A new box created from the coordinates.</returns>
    public static MgRectangleF FromCenterAndSize(MgVector2 center, MgVector2 size)
    {
      return FromCenterAndSize(center.X, center.Y, size.X, size.Y);
    }

    /// <summary>
    /// Creates a BoxF structre with the specified center and size.
    /// </summary>
    /// <param name="center">The center of the box.</param>
    /// <param name="size">The size of the box.</param>
    /// <returns>A new box created from the coordinates.</returns>
    public static MgRectangleF FromCenterAndSize(MgVector2 center, MgSizeF size)
    {
      return FromCenterAndSize(center.X, center.Y, size.Width, size.Height);
    }

    /// <summary>
    /// Creates a BoxF structre with the specified center and size.
    /// </summary>
    /// <param name="cx">The center x-coordinate of the box.</param>
    /// <param name="cy">The center y-coordinate of the box.</param>
    /// <param name="size">The size of the box.</param>
    /// <returns>A new box created from the coordinates.</returns>
    public static MgRectangleF FromCenterAndSize(float cx, float cy, MgVector2 size)
    {
      return FromCenterAndSize(cx, cy, size.X, size.Y);
    }

    /// <summary>
    /// Creates a BoxF structre with the specified center and size.
    /// </summary>
    /// <param name="cx">The center x-coordinate of the box.</param>
    /// <param name="cy">The center y-coordinate of the box.</param>
    /// <param name="size">The size of the box.</param>
    /// <returns>A new box created from the coordinates.</returns>
    public static MgRectangleF FromCenterAndSize(float cx, float cy, MgSizeF size)
    {
      return FromCenterAndSize(cx, cy, size.Width, size.Height);
    }

    /// <summary>
    /// Creates a BoxF structre with the specified center and size.
    /// </summary>
    /// <param name="center">The center of the box.</param>
    /// <param name="width">The width of the box.</param>
    /// <param name="height">The height of the box.</param>
    /// <returns>A new box created from the coordinates.</returns>
    public static MgRectangleF FromCenterAndSize(MgVector2 center, float width, float height)
    {
      return FromCenterAndSize(center.X, center.Y, width, height);
    }

    /// <summary>
    /// Creates a BoxF structre with the specified center and size.
    /// </summary>
    /// <param name="cx">The center x-coordinate of the box.</param>
    /// <param name="cy">The center y-coordinate of the box.</param>
    /// <param name="width">The width of the box.</param>
    /// <param name="height">The height of the box.</param>
    /// <returns>A new box created from the coordinates.</returns>
    public static MgRectangleF FromCenterAndSize(float cx, float cy, float width, float height)
    {
      var retval = new MgRectangleF(0, 0, width, height);
      retval.Center = new MgVector2(cx, cy);
      return retval;
    }

    /// <summary>
    /// Tests whether obj is a BoxF with the same location and size of this BoxF.
    /// </summary>
    /// <param name="obj">The object to test.</param>
    /// <returns>A value indicating if the object matches this BoxF.</returns>
    public override bool Equals(object obj)
    {
      if (!(obj is MgRectangleF))
      {
        return false;
      }
      var ef = (MgRectangleF)obj;
      return ((((ef.X == X) && (ef.Y == Y)) && (ef.Width == Width)) && (ef.Height == Height));
    }

    /// <summary>
    /// Tests whether two BoxF structures have equal location and size.
    /// </summary>
    /// <param name="left">The box on the left side of the equality equation.</param>
    /// <param name="right">The box on the right side of the equality equation.</param>
    /// <returns>A value indicating if the two boxes are equal.</returns>
    public static bool operator ==(MgRectangleF left, MgRectangleF right)
    {
      return ((((left.X == right.X) && (left.Y == right.Y)) && (left.Width == right.Width)) && (left.Height == right.Height));
    }

    /// <summary>
    /// Tests whether two BoxF structures don't have equal location and size.
    /// </summary>
    /// <param name="left">The box on the left side of the equality equation.</param>
    /// <param name="right">The box on the right side of the equality equation.</param>
    /// <returns>A value indicating if the two boxes aren't equal.</returns>
    public static bool operator !=(MgRectangleF left, MgRectangleF right)
    {
      return !(left == right);
    }

    /// <summary>
    /// Determines if the specified point is contained within this BoxF structure.
    /// </summary>
    /// <param name="x">The x-coordinate of the point.</param>
    /// <param name="y">The y-coordinate of the point.</param>
    /// <returns>A value indicating if this box contains the point.</returns>
    public bool Contains(float x, float y)
    {
      return (X <= x) && (x < Right) && (Y <= y) && (y < Bottom);
    }

    /// <summary>
    /// Determines if the specified point is contained within this BoxF structure.
    /// </summary>
    /// <param name="pt">The point to test.</param>
    /// <returns>A value indicating if this box contains the point.</returns>
    public bool Contains(MgVector2 pt)
    {
      return Contains(pt.X, pt.Y);
    }

    /// <summary>
    /// Determines if the specified point is contained within this BoxF structure.
    /// </summary>
    /// <param name="pt">The point to test.</param>
    /// <returns>A value indicating if this box contains the point.</returns>
    public bool Contains(MgPoint pt)
    {
      return Contains(pt.X, pt.Y);
    }

    /// <summary>
    /// Determines if the rectangular region represented by box is entirely contained within this BoxF structure.
    /// </summary>
    /// <param name="box">The box to test.</param>
    /// <returns>A value indicating if this box contains the box.</returns>
    public bool Contains(MgRectangleF box)
    {
      return (X <= box.X) && (box.Right <= Right) && (Y <= box.Y) && (box.Bottom <= Bottom);
    }

    /// <summary>
    /// Gets the hash code for this BoxF structure.
    /// </summary>
    /// <returns>The has code of this box.</returns>
    public override int GetHashCode()
    {
      return (int)(((((uint)X) ^ ((((uint)Y) << 13) | (((uint)Y) >> 0x13))) ^ ((((uint)Width) << 0x1a) | (((uint)Width) >> 6))) ^ ((((uint)Height) << 7) | (((uint)Height) >> 0x19)));
    }

    /// <summary>
    /// Inflates this BoxF structure by the specified amount. This will adjust the x and y values to grow (or shrink), and
    /// then
    /// adjust the width and height to match.
    /// </summary>
    /// <param name="x">The amount to inflate horizontally.</param>
    /// <param name="y">The amount to inflate vertically.</param>
    public void Inflate(float x, float y)
    {
      X -= x;
      Y -= y;
      Width += 2f * x;
      Height += 2f * y;
    }

    /// <summary>
    /// Inflates this BoxF structure by the specified amount. This will adjust the x and y values to grow (or shrink), and
    /// then
    /// adjust the width and height to match.
    /// </summary>
    /// <param name="size">The amount to inflate this box.</param>
    public void Inflate(MgSizeF size)
    {
      Inflate(size.Width, size.Height);
    }

    /// <summary>
    /// Returns an inflated copy of the box.
    /// </summary>
    /// <param name="box">The box to copy and inflate. This is not modified.</param>
    /// <param name="x">The amount to inflate horizontally.</param>
    /// <param name="y">The amount to inflate vertically.</param>
    /// <returns>A newly created inflated box.</returns>
    public static MgRectangleF Inflate(MgRectangleF box, float x, float y)
    {
      var ef = box;
      ef.Inflate(x, y);
      return ef;
    }

    /// <summary>
    /// Replaces this BoxF structure with the intersection of itself and the specified BoxF structure.
    /// </summary>
    /// <param name="box">The box to intersect.</param>
    public void Intersect(MgRectangleF box)
    {
      var ef = Intersect(box, this);
      X = ef.X;
      Y = ef.Y;
      Width = ef.Width;
      Height = ef.Height;
    }

    /// <summary>
    /// Converts this floating-point box to an integer rectangle.
    /// </summary>
    public MgRectangle ToRectangle()
    {
      return new MgRectangle
      {
        X = (int)Math.Round(X),
        Y = (int)Math.Round(Y),
        Width = (int)Math.Round(Width),
        Height = (int)Math.Round(Height),
      };
    }

    /// <summary>
    /// Returns a BoxF structure that represents the intersection of two boxes. If there is no intersection, an empty BoxF is
    /// returned.
    /// </summary>
    /// <param name="a">The first box to intersect.</param>
    /// <param name="b">The second box to intersect.</param>
    /// <returns>A box that represents the intersection of two boxes.</returns>
    public static MgRectangleF Intersect(MgRectangleF a, MgRectangleF b)
    {
      var x = Math.Max(a.X, b.X);
      var num2 = Math.Min(a.X + a.Width, b.X + b.Width);
      var y = Math.Max(a.Y, b.Y);
      var num4 = Math.Min(a.Y + a.Height, b.Y + b.Height);
      if ((num2 >= x) && (num4 >= y))
      {
        return new MgRectangleF(x, y, num2 - x, num4 - y);
      }
      return Empty;
    }

    /// <summary>
    /// Determines if this box intersects with another box.
    /// </summary>
    /// <param name="box">The box to test.</param>
    /// <returns>A value indicating if the two boxes intersect.</returns>
    public bool IntersectsWith(MgRectangleF box)
    {
      return ((((box.X < (X + Width)) && (X < (box.X + box.Width))) && (box.Y < (Y + Height))) && (Y < (box.Y + box.Height)));
    }

    /// <summary>
    /// Creates the smallest possible box that can contain both of two box that form a union.
    /// </summary>
    /// <param name="a">The first box to union.</param>
    /// <param name="b">The second box to union.</param>
    /// <returns>A box that represents the union of two boxes.</returns>
    public static MgRectangleF Union(MgRectangleF a, MgRectangleF b)
    {
      var x = Math.Min(a.X, b.X);
      var num2 = Math.Max(a.X + a.Width, b.X + b.Width);
      var y = Math.Min(a.Y, b.Y);
      var num4 = Math.Max(a.Y + a.Height, b.Y + b.Height);
      return new MgRectangleF(x, y, num2 - x, num4 - y);
    }

    /// <summary>
    /// Translates the upper-left coordinate of this box by the specified point.
    /// </summary>
    /// <param name="pos">A point representing the amount to translate in the x and y direction.</param>
    public void Offset(MgVector2 pos)
    {
      Offset(pos.X, pos.Y);
    }

    /// <summary>
    /// Translates the upper-left coordinate of this box by the specified point.
    /// </summary>
    /// <param name="x">The amount to translate in the x direction.</param>
    /// <param name="y">The amount to translate in the y direction.</param>
    public void Offset(float x, float y)
    {
      X += x;
      Y += y;
    }

    /// <summary>
    /// Translates the upper-left coordinate of the box by the specified point.
    /// </summary>
    /// <param name="box">The box to adjust. This is not affected.</param>
    /// <param name="dx">The amount to translate in the x direction.</param>
    /// <param name="dy">The amount to translate in the y direction.</param>
    /// <returns>A new translated box.</returns>
    public static MgRectangleF Offset(MgRectangleF box, float dx, float dy)
    {
      return new MgRectangleF(box.X + dx, box.Y + dy, box.Width, box.Height);
    }

    /// <summary>
    /// Translates the upper-left coordinate of the box by the specified point.
    /// </summary>
    /// <param name="box">The box to adjust. This is not affected.</param>
    /// <param name="offset">The amount to translate in the x and y direction.</param>
    /// <returns>A new translated box.</returns>
    public static MgRectangleF Offset(MgRectangleF box, MgVector2 offset)
    {
      return new MgRectangleF(box.Location + offset, box.Size);
    }

    /// <summary>
    /// Converts the specified Box structure to a BoxF structure.
    /// </summary>
    /// <param name="r">The box to convert.</param>
    /// <returns>The converted box.</returns>
    public static implicit operator MgRectangleF(MgRectangle r)
    {
      return new MgRectangleF(r.X, r.Y, r.Width, r.Height);
    }

    /// <summary>
    /// Converts the Location and Size of this BoxF to a human-readable string.
    /// </summary>
    /// <returns>A string that contains the Location and Size.</returns>
    public override string ToString()
    {
      return string.Format("{{X={0},Y={1},Width={2},Height={3}}}", X, Y, Width, Height);
    }
  }
}