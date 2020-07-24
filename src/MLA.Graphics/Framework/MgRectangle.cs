using System;

namespace MLA.Graphics
{
  /// <summary>
  /// Stores a set of four floating-point numbers that represent the location and size of a box.
  /// </summary>
  [Serializable]
  public struct MgRectangle
  {
    /// <summary>
    /// Represents an uninitialized instance of a Box.
    /// </summary>
    public static readonly MgRectangle Empty;

    /// <summary>
    /// Gets or sets the upper-left x-coordinate of this Box.
    /// </summary>
    public int X;

    /// <summary>
    /// Gets or sets the upper-left y-coordinate of this Box.
    /// </summary>
    public int Y;

    /// <summary>
    /// Gets or set the width of this Box.
    /// </summary>
    public int Width;

    /// <summary>
    /// Gets or sets the height of this Box.
    /// </summary>
    public int Height;

    static MgRectangle()
    {
      Empty = new MgRectangle();
    }

    /// <summary>
    /// Creates a new instance of the Box class with the specified location and size.
    /// </summary>
    /// <param name="location">The upper-left corner of the box.</param>
    /// <param name="size">The size of the box.</param>
    public MgRectangle(MgPoint location, MgSize size)
      : this(location.X, location.Y, size.Width, size.Height) {}

    /// <summary>
    /// reates a new instance of the Box class with the specified location and size.
    /// </summary>
    /// <param name="x">The x-coordinate of the upper-left corner of the box.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the box.</param>
    /// <param name="width">The width of the box.</param>
    /// <param name="height">The height of the box.</param>
    public MgRectangle(int x, int y, int width, int height)
    {
      X = x;
      Y = y;
      Width = width;
      Height = height;
    }

    /// <summary>
    /// Gets or sets the coordinates of the upper-left corner of this Box.
    /// </summary>
    public MgPoint Location
    {
      get { return new MgPoint(X, Y); }
      set
      {
        X = value.X;
        Y = value.Y;
      }
    }

    /// <summary>
    /// Gets or sets the size of this Box.
    /// </summary>
    public MgSize Size
    {
      get { return new MgSize(Width, Height); }
      set
      {
        Width = value.Width;
        Height = value.Height;
      }
    }

    /// <summary>
    /// Gets the upper-left x-coordinate of this Box.
    /// </summary>
    public int Left
    {
      get { return X; }
    }

    /// <summary>
    /// Gets the upper-left y-coordinate of this Box.
    /// </summary>
    public int Top
    {
      get { return Y; }
    }

    /// <summary>
    /// Gets the lower-right x-coordinate of this Box.
    /// </summary>
    public int Right
    {
      get { return (X + Width); }
    }

    /// <summary>
    /// Gets the lower-right y-coordinate of this Box.
    /// </summary>
    public int Bottom
    {
      get { return (Y + Height); }
    }

    /// <summary>
    /// Gets the center of this Box.
    /// </summary>
    public MgPoint Center
    {
      get { return new MgPoint((X + Right) / 2, (Y + Bottom) / 2); }
      set
      {
        var center = Center;
        var location = new MgPoint(X + (value.X - center.X), Y + (value.Y - center.Y));
        Location = location;
      }
    }

    /// <summary>
    /// Tests whether the Width or Height property of this Box has a less than or equal to 0.
    /// </summary>
    public bool IsEmpty
    {
      get
      {
        if (Width > 0)
        {
          return (Height <= 0);
        }
        return true;
      }
    }

    /// <summary>
    /// Creates a Box structure with upper-left corner and lower-right corner at the specified locations.
    /// </summary>
    /// <param name="left">The x-coordinate of the upper-left corner of the box.</param>
    /// <param name="top">The y-coordinate of the upper-left corner of the box.</param>
    /// <param name="right">The x-coordinate of the lower-right corner of the box.</param>
    /// <param name="bottom">The y-coordinate of the lower-right corner of the box.</param>
    /// <returns>A new box created from the coordinates.</returns>
    public static MgRectangle FromLTRB(int left, int top, int right, int bottom)
    {
      return new MgRectangle(left, top, right - left, bottom - top);
    }

    /// <summary>
    /// Tests whether obj is a Box with the same location and size of this Box.
    /// </summary>
    /// <param name="obj">The object to test.</param>
    /// <returns>A value indicating if the object matches this Box.</returns>
    public override bool Equals(object obj)
    {
      if (!(obj is MgRectangle))
      {
        return false;
      }

      var rectangle = (MgRectangle)obj;
      return (this == rectangle);
    }

    /// <summary>
    /// Tests whether two Box structures have equal location and size.
    /// </summary>
    /// <param name="left">The box on the left side of the equality equation.</param>
    /// <param name="right">The box on the right side of the equality equation.</param>
    /// <returns>A value indicating if the two boxes are equal.</returns>
    public static bool operator ==(MgRectangle left, MgRectangle right)
    {
      return ((((left.X == right.X) && (left.Y == right.Y)) && (left.Width == right.Width)) && (left.Height == right.Height));
    }

    /// <summary>
    /// Tests whether two Box structures don't have equal location and size.
    /// </summary>
    /// <param name="left">The box on the left side of the equality equation.</param>
    /// <param name="right">The box on the right side of the equality equation.</param>
    /// <returns>A value indicating if the two boxes aren't equal.</returns>
    public static bool operator !=(MgRectangle left, MgRectangle right)
    {
      return !(left == right);
    }

    /// <summary>
    /// Converts the specified BoxF structure to a Box structure by rounding the BoxF values to the
    /// next higher integer values.
    /// </summary>
    /// <param name="value">The box to be converted.</param>
    /// <returns>A converted box.</returns>
    public static MgRectangle Ceiling(MgRectangleF value)
    {
      return new MgRectangle((int)Math.Ceiling(value.X), (int)Math.Ceiling(value.Y), (int)Math.Ceiling(value.Width), (int)Math.Ceiling(value.Height));
    }

    /// <summary>
    /// Converts the specified BoxF to a Box by truncating the BoxF values.
    /// </summary>
    /// <param name="value">The box to be converted.</param>
    /// <returns>A converted box.</returns>
    public static MgRectangle Truncate(MgRectangleF value)
    {
      return new MgRectangle((int)value.X, (int)value.Y, (int)value.Width, (int)value.Height);
    }

    /// <summary>
    /// Converts the specified BoxF to a Box by rounding the BoxF values to the nearest integer values.
    /// </summary>
    /// <param name="value">The box to be converted.</param>
    /// <returns>A converted box.</returns>
    public static MgRectangle Round(MgRectangleF value)
    {
      return new MgRectangle((int)Math.Round(value.X), (int)Math.Round(value.Y), (int)Math.Round(value.Width), (int)Math.Round(value.Height));
    }

    /// <summary>
    /// Determines if the specified point is contained within this box.
    /// </summary>
    /// <param name="x">The x-coordinate of the point to test.</param>
    /// <param name="y">The y-coordinate of the point to test.</param>
    /// <returns>A value indicating if the point is inside this box</returns>
    public bool Contains(int x, int y)
    {
      return (X <= x) && (x < Right) && (Y <= y) && (y < Bottom);
    }

    /// <summary>
    /// Determines if the specified point is contained within this box.
    /// </summary>
    /// <param name="pt">The point to test.</param>
    /// <returns>A value indicating if the point is inside this box</returns>
    public bool Contains(MgPoint pt)
    {
      return Contains(pt.X, pt.Y);
    }

    /// <summary>
    /// Determines if the rectangular region represented by box is entirely contained within this Box.
    /// </summary>
    /// <param name="box">The box to test.</param>
    /// <returns>A value indicating if this box contains the box.</returns>
    public bool Contains(MgRectangle box)
    {
      return (X <= box.X) && (box.Right <= Right) && (Y <= box.Y) && (box.Bottom <= Bottom);
    }

    /// <summary>
    /// Gets the hash code for this Box structure.
    /// </summary>
    /// <returns>The has code of this box.</returns>
    public override int GetHashCode()
    {
      return (((X ^ ((Y << 13) | (Y >> 0x13))) ^ ((Width << 0x1a) | (Width >> 6))) ^ ((Height << 7) | (Height >> 0x19)));
    }

    /// <summary>
    /// Inflates this Box structure by the specified amount. This will adjust the x and y values to grow (or shrink), and
    /// then
    /// adjust the width and height to match.
    /// </summary>
    /// <param name="x">The amount to inflate horizontally.</param>
    /// <param name="y">The amount to inflate vertically.</param>
    public void Inflate(int x, int y)
    {
      X -= x;
      Y -= y;
      Width += 2 * x;
      Height += 2 * y;
    }

    /// <summary>
    /// Inflates this Box structure by the specified amount. This will adjust the x and y values to grow (or shrink), and
    /// then
    /// adjust the width and height to match.
    /// </summary>
    /// <param name="size">The amount to inflate this box.</param>
    public void Inflate(MgSize size)
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
    public static MgRectangle Inflate(MgRectangle box, int x, int y)
    {
      var rectangle = box;
      rectangle.Inflate(x, y);
      return rectangle;
    }

    /// <summary>
    /// Converts this Box to a XNA Rectangle.
    /// </summary>
    /// <returns>The box as a rectangle.</returns>
    public MgRectangle ToRectangle()
    {
      return new MgRectangle(X, Y, Width, Height);
    }

    /// <summary>
    /// Replaces this Box structure with the intersection of itself and the specified Box structure.
    /// </summary>
    /// <param name="box">The box to intersect.</param>
    public void Intersect(MgRectangle box)
    {
      var rectangle = Intersect(box, this);
      X = rectangle.X;
      Y = rectangle.Y;
      Width = rectangle.Width;
      Height = rectangle.Height;
    }

    /// <summary>
    /// Returns a Box structure that represents the intersection of two boxes. If there is no intersection, an empty Box is
    /// returned.
    /// </summary>
    /// <param name="a">The first box to intersect.</param>
    /// <param name="b">The second box to intersect.</param>
    /// <returns>A box that represents the intersection of two boxes.</returns>
    public static MgRectangle Intersect(MgRectangle a, MgRectangle b)
    {
      var x = Math.Max(a.X, b.X);
      var num2 = Math.Min(a.X + a.Width, b.X + b.Width);
      var y = Math.Max(a.Y, b.Y);
      var num4 = Math.Min(a.Y + a.Height, b.Y + b.Height);
      if ((num2 >= x) && (num4 >= y))
      {
        return new MgRectangle(x, y, num2 - x, num4 - y);
      }
      return Empty;
    }

    /// <summary>
    /// Determines if this box intersects with another box.
    /// </summary>
    /// <param name="box">The box to test.</param>
    /// <returns>A value indicating if the two boxes intersect.</returns>
    public bool IntersectsWith(MgRectangle box)
    {
      return (box.X < (Right)) && (X < (box.Right)) && (box.Y < (Bottom)) && (Y < (box.Bottom));
    }

    /// <summary>
    /// Creates the smallest possible box that can contain both of two box that form a union.
    /// </summary>
    /// <param name="a">The first box to union.</param>
    /// <param name="b">The second box to union.</param>
    /// <returns>A box that represents the union of two boxes.</returns>
    public static MgRectangle Union(MgRectangle a, MgRectangle b)
    {
      var x = Math.Min(a.X, b.X);
      var num2 = Math.Max(a.X + a.Width, b.X + b.Width);
      var y = Math.Min(a.Y, b.Y);
      var num4 = Math.Max(a.Y + a.Height, b.Y + b.Height);
      return new MgRectangle(x, y, num2 - x, num4 - y);
    }

    /// <summary>
    /// Translates the upper-left coordinate of this box by the specified point.
    /// </summary>
    /// <param name="pos">A point representing the amount to translate in the x and y direction.</param>
    public void Offset(MgPoint pos)
    {
      Offset(pos.X, pos.Y);
    }

    /// <summary>
    /// Translates the upper-left coordinate of this box by the specified point.
    /// </summary>
    /// <param name="x">The amount to translate in the x direction.</param>
    /// <param name="y">The amount to translate in the y direction.</param>
    public void Offset(int x, int y)
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
    public static MgRectangle Offset(MgRectangle box, int dx, int dy)
    {
      return new MgRectangle(box.X + dx, box.Y + dy, box.Width, box.Height);
    }

    /// <summary>
    /// Translates the upper-left coordinate of the box by the specified point.
    /// </summary>
    /// <param name="box">The box to adjust. This is not affected.</param>
    /// <param name="offset">The amount to translate in the x and y direction.</param>
    /// <returns>A new translated box.</returns>
    public static MgRectangle Offset(MgRectangle box, MgPoint offset)
    {
      return new MgRectangle(box.X + offset.X, box.Y + offset.Y, box.Width, box.Height);
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