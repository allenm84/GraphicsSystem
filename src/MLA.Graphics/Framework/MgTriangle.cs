using System;

namespace MLA.Graphics
{
  /// <summary>
  /// A basic triangle structure that holds the three vertices that make up a given triangle.
  /// </summary>
  [Serializable]
  public struct MgTriangle
  {
    private static Comparison<MgVector2> Vector2Comparison = (v1, v2) =>
    {
      var comparison = v1.X.CompareTo(v2.X);
      if (comparison == 0)
      {
        comparison = v1.Y.CompareTo(v2.Y);
      }
      return comparison;
    };

    /// <summary>
    /// Gets or sets the first point on the triangle.
    /// </summary>
    public MgVector2 A;

    /// <summary>
    /// Gets or sets the second point on the triangle.
    /// </summary>
    public MgVector2 B;

    /// <summary>
    /// Gets or sets the third point on the triangle.
    /// </summary>
    public MgVector2 C;

    /// <summary>
    /// Creates a new triangle from three points.
    /// </summary>
    /// <param name="a">The first point of the triangle.</param>
    /// <param name="b">The second point of the triangle.</param>
    /// <param name="c">The third point of the triangle.</param>
    public MgTriangle(MgVector2 a, MgVector2 b, MgVector2 c)
    {
      A = a;
      B = b;
      C = c;
    }

    /// <summary>
    /// Determines if a point is inside a triangle.
    /// </summary>
    /// <param name="point">The point to test.</param>
    /// <returns>A value indicating if a point is inside a triangle.</returns>
    public bool ContainsPoint(MgVector2 point)
    {
      //return true if the point to test is one of the vertices
      if (point.Equals(A) || point.Equals(B) || point.Equals(C))
      {
        return true;
      }

      var oddNodes = false;

      if (checkPointToSegment(C, A, point))
      {
        oddNodes = !oddNodes;
      }
      if (checkPointToSegment(A, B, point))
      {
        oddNodes = !oddNodes;
      }
      if (checkPointToSegment(B, C, point))
      {
        oddNodes = !oddNodes;
      }

      return oddNodes;
    }

    /// <summary>
    /// Determines if a point is inside a triangle.
    /// </summary>
    /// <param name="Ax">The x coordinate of the first point of the triangle.</param>
    /// <param name="Ay">The y coordinate of the first point of the triangle.</param>
    /// <param name="Bx">The x coordinate of the second point of the triangle.</param>
    /// <param name="By">The y coordinate of the second point of the triangle.</param>
    /// <param name="Cx">The x coordinate of the third point of the triangle.</param>
    /// <param name="Cy">The y coordinate of the third point of the triangle.</param>
    /// <param name="Px">The x coordinate of the point to test.</param>
    /// <param name="Py">The y coordinate of the point to test.</param>
    /// <returns>A value indicating if a point is inside a triangle.</returns>
    public static bool ContainsPoint(float Ax, float Ay, float Bx, float By, float Cx, float Cy, float Px, float Py)
    {
      return ContainsPoint(new MgVector2(Ax, Ay), new MgVector2(Bx, By), new MgVector2(Cx, Cy), new MgVector2(Px, Py));
    }

    /// <summary>
    /// Determines if a point is inside a triangle.
    /// </summary>
    /// <param name="A">The first point of the triangle.</param>
    /// <param name="B">The second point of the triangle.</param>
    /// <param name="C">The third point of the triangle.</param>
    /// <param name="Pt">The point to test.</param>
    /// <returns>A value indicating if a point is inside a triangle.</returns>
    public static bool ContainsPoint(MgVector2 A, MgVector2 B, MgVector2 C, MgVector2 Pt)
    {
      return new MgTriangle(A, B, C).ContainsPoint(Pt);
    }

    private static bool checkPointToSegment(MgVector2 sA, MgVector2 sB, MgVector2 point)
    {
      if ((sA.Y < point.Y && sB.Y >= point.Y) || (sB.Y < point.Y && sA.Y >= point.Y))
      {
        var x = sA.X + (point.Y - sA.Y) / (sB.Y - sA.Y) * (sB.X - sA.X);
        if (x < point.X)
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Determines if obj has the same coordinates as this triangle. It doesn't
    /// matter what order the coordinates are in.
    /// </summary>
    /// <param name="obj">The object to test.</param>
    /// <returns>True if the coordinates are same; otherwise false.</returns>
    public override bool Equals(object obj)
    {
      if (obj.GetType() != typeof(MgTriangle))
      {
        return false;
      }
      return Equals((MgTriangle)obj);
    }

    /// <summary>
    /// Determines if obj has the same coordinates as this triangle. It doesn't
    /// matter what order the coordinates are in.
    /// </summary>
    /// <param name="obj">The triangle to test.</param>
    /// <returns>True if the coordinates are same; otherwise false.</returns>
    public bool Equals(MgTriangle obj)
    {
      var a = new MgVector2[3] {A, B, C};
      var b = new MgVector2[3] {obj.A, obj.B, obj.C};

      Array.Sort(a, Vector2Comparison);
      Array.Sort(b, Vector2Comparison);

      return
        a[0].Equals(b[0]) &&
          a[1].Equals(b[1]) &&
          a[2].Equals(b[2]);
    }

    /// <summary>
    /// Gets a value representing the hash code of this triangle.
    /// </summary>
    /// <returns>A unique code representing this triangle.</returns>
    public override int GetHashCode()
    {
      unchecked
      {
        var result = A.GetHashCode();
        result = (result * 397) ^ B.GetHashCode();
        result = (result * 397) ^ C.GetHashCode();
        return result;
      }
    }
  }
}