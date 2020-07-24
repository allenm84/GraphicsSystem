using System;
using System.Collections.Generic;
using System.Linq;

namespace MLA.Graphics
{
  public static class MgMath
  {
    private const int XAxis = 0;
    private const int YAxis = 1;
    private const int CosValue = 0;
    private const int SinValue = 1;

    public const float E = 2.71828175f;
    public const float Log2E = 1.442695f;
    public const float Log10E = 0.4342945f;
    public const float Pi = 3.14159274f;
    public const float TwoPi = 6.28318548f;
    public const float PiOver2 = 1.57079637f;
    public const float PiOver4 = 0.7853982f;

    private const short MaxCircleAngle = 512;
    private const short HalfMaxCircleAngle = (MaxCircleAngle / 2);
    private const short QuarterMaxCircleAngle = (MaxCircleAngle / 4);
    private const short MaskMaxCircleAngle = (MaxCircleAngle - 1);
    private const float HalfMaxCircleAngleTPi = HalfMaxCircleAngle / Pi;

    private static float[] sTrigTable = new float[MaxCircleAngle];
    private static float[,] sValues = new float[2, 2];

    static MgMath()
    {
      sValues[CosValue, XAxis] = (float)Math.Cos(Pi);
      sValues[CosValue, YAxis] = (float)Math.Cos(PiOver2);
      sValues[SinValue, XAxis] = (float)Math.Sin(Pi);
      sValues[SinValue, YAxis] = (float)Math.Sin(PiOver2);

      double v = 0;
      double r = 0;
      double h = HalfMaxCircleAngle;

      for (short i = 0; i < MaxCircleAngle; i++)
      {
        v = i * Math.PI / h;
        r = Math.Sin(v);
        sTrigTable[i] = (float)r;
      }
    }

    /// <summary>
    /// Rounds a single-precision floating-point number.
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public static int Round(this float f)
    {
      return (int)Math.Round(f);
    }

    /// <summary>
    /// Rounds a vector2.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static MgPoint Round(this MgVector2 v)
    {
      return new MgPoint(Round(v.X), Round(v.Y));
    }

    /// <summary>
    /// Rounds an enumeration of vector2.
    /// </summary>
    /// <param name="vecs"></param>
    /// <returns></returns>
    public static IEnumerable<MgPoint> Round(this IEnumerable<MgVector2> vecs)
    {
      return vecs.Select(vec => Round(vec));
    }

    /// <summary>
    /// Floors a single-precision floating-point number.
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public static int Floor(this float f)
    {
      return (int)Math.Floor(f);
    }

    /// <summary>
    /// Floors a vector2.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static MgPoint Floor(this MgVector2 v)
    {
      return new MgPoint(Floor(v.X), Floor(v.Y));
    }

    /// <summary>
    /// Floors an enumeration of vector2.
    /// </summary>
    /// <param name="vecs"></param>
    /// <returns></returns>
    public static IEnumerable<MgPoint> Floor(this IEnumerable<MgVector2> vecs)
    {
      return vecs.Select(vec => Floor(vec));
    }

    /// <summary>
    /// Ceils a single-precision floating-point number.
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public static int Ceiling(this float f)
    {
      return (int)Math.Ceiling(f);
    }

    /// <summary>
    /// Ceils a vector2.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static MgPoint Ceiling(this MgVector2 v)
    {
      return new MgPoint(Ceiling(v.X), Ceiling(v.Y));
    }

    /// <summary>
    /// Ceils an enumeration of vector2.
    /// </summary>
    /// <param name="vecs"></param>
    /// <returns></returns>
    public static IEnumerable<MgPoint> Ceiling(this IEnumerable<MgVector2> vecs)
    {
      return vecs.Select(vec => Ceiling(vec));
    }

    /// <summary>
    /// Translates a vector2 by a specified amount.
    /// </summary>
    /// <param name="v">The vector to translate</param>
    /// <param name="dx">The amount to translate on the x axis.</param>
    /// <param name="dy">The amount to translate on the y axis.</param>
    /// <returns>A translated vector.</returns>
    public static MgVector2 Translate(this MgVector2 v, float dx, float dy)
    {
      return new MgVector2(v.X + dx, v.Y + dy);
    }

    /// <summary>
    /// Computes the percent that the value is with respect to the maximum and minimum.
    /// </summary>
    /// <param name="value">The current value.</param>
    /// <param name="minimum">The minimum value.</param>
    /// <param name="maximum">The maximum value.</param>
    /// <returns>The percentage.</returns>
    public static float GetPercent(float value, float minimum, float maximum)
    {
      return (value - minimum) / (maximum - minimum);
    }

    /// <summary>
    /// Returns the sine of the specified angle.
    /// </summary>
    /// <param name="n">An angle, measured in radians.</param>
    /// <returns>The sine of the angle.</returns>
    public static float Sin(float n)
    {
      var f = n * HalfMaxCircleAngleTPi;
      var i = (int)f;

      if (i < 0)
      {
        return sTrigTable[(-((-i) & MaskMaxCircleAngle)) + MaxCircleAngle];
      }
      return sTrigTable[i & MaskMaxCircleAngle];
    }

    /// <summary>
    /// Returns the cosine of the specified angle.
    /// </summary>
    /// <param name="n">An angle, measured in radians.</param>
    /// <returns>The cosine of the angle.</returns>
    public static float Cos(float n)
    {
      var f = n * HalfMaxCircleAngleTPi;
      var i = (int)f;

      if (i < 0)
      {
        return sTrigTable[((-i) + QuarterMaxCircleAngle) & MaskMaxCircleAngle];
      }
      return sTrigTable[(i + QuarterMaxCircleAngle) & MaskMaxCircleAngle];
    }

    /// <summary>
    /// Wraps an angle to fall within the degrees allowed by a circle (e.g. 361 degrees becomes 1 degree or
    /// "rotate around circle once, then go 1 degree".
    /// </summary>
    /// <param name="angle">The angle (in radians) to wrap</param>
    /// <returns>The wrapped angle.</returns>
    public static float WrapAngle(float angle)
    {
      while (angle < -Pi)
      {
        angle += TwoPi;
      }
      while (angle > Pi)
      {
        angle -= TwoPi;
      }
      return angle;
    }

    /// <summary>
    /// Computes the scale necessary to go from the original size to the desired size. For example, the scale to go
    /// from (400,400) to (800,800) is (2,2).
    /// </summary>
    /// <param name="originalSize">The original size to go from.</param>
    /// <param name="desiredSize">The desired size to get to.</param>
    /// <returns>A vector representing the scale.</returns>
    public static MgVector2 GetScale(MgSizeF originalSize, MgSizeF desiredSize)
    {
      return GetScale(originalSize.Width, originalSize.Height, desiredSize.Width, desiredSize.Height);
    }

    /// <summary>
    /// Computes the scale necessary to go from the original size to the desired size. For example, the scale to go
    /// from (400,400) to (800,800) is (2,2).
    /// </summary>
    /// <param name="orgWidth">The original width to go from.</param>
    /// <param name="orgHeight">The original height to go from.</param>
    /// <param name="desWidth">The desired width to get to.</param>
    /// <param name="desHeight">The desired height to get to.</param>
    /// <returns>A vector representing the scale.</returns>
    public static MgVector2 GetScale(float orgWidth, float orgHeight, float desWidth, float desHeight)
    {
      return new MgVector2(desWidth / orgWidth, desHeight / orgHeight);
    }

    public static float ToRadians(float degrees)
    {
      return degrees * 0.0174532924f;
    }

    public static float ToDegrees(float radians)
    {
      return radians * 57.2957764f;
    }

    public static float Distance(float value1, float value2)
    {
      return Math.Abs(value1 - value2);
    }

    public static float Min(float value1, float value2)
    {
      return Math.Min(value1, value2);
    }

    public static float Max(float value1, float value2)
    {
      return Math.Max(value1, value2);
    }

    public static float Clamp(float value, float min, float max)
    {
      value = ((value > max) ? max : value);
      value = ((value < min) ? min : value);
      return value;
    }

    public static float Lerp(float value1, float value2, float amount)
    {
      return value1 + (value2 - value1) * amount;
    }

    public static float Barycentric(float value1, float value2, float value3, float amount1, float amount2)
    {
      return value1 + amount1 * (value2 - value1) + amount2 * (value3 - value1);
    }

    public static float SmoothStep(float value1, float value2, float amount)
    {
      var num = Clamp(amount, 0f, 1f);
      return Lerp(value1, value2, num * num * (3f - 2f * num));
    }

    public static float CatmullRom(float value1, float value2, float value3, float value4, float amount)
    {
      var num = amount * amount;
      var num2 = amount * num;
      return 0.5f *
        (2f * value2 + (-value1 + value3) * amount + (2f * value1 - 5f * value2 + 4f * value3 - value4) * num +
          (-value1 + 3f * value2 - 3f * value3 + value4) * num2);
    }

    public static float Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
    {
      var num = amount * amount;
      var num2 = amount * num;
      var num3 = 2f * num2 - 3f * num + 1f;
      var num4 = -2f * num2 + 3f * num;
      var num5 = num2 - 2f * num + amount;
      var num6 = num2 - num;
      return value1 * num3 + value2 * num4 + tangent1 * num5 + tangent2 * num6;
    }

    /// <summary>
    /// Multiplies two colors together.
    /// </summary>
    /// <param name="color1">The left side of the multiplication.</param>
    /// <param name="color">The right side of the multiplication.</param>
    /// <returns>The result of the multiplication.</returns>
    public static MgColor Mult(MgColor color1, MgColor color)
    {
      var a = color1.ToVector4();
      var b = color.ToVector4();
      return new MgColor(a * b);
    }

    /// <summary>
    /// Linear interpolates between two colors.
    /// </summary>
    /// <param name="color1">The first color.</param>
    /// <param name="color2">The second color.</param>
    /// <param name="mu">The weight value.</param>
    /// <returns>The interpolated color.</returns>
    public static MgColor Lerp(MgColor color1, MgColor color2, float mu)
    {
      var c1 = color1.ToVector4();
      var c2 = color2.ToVector4();

      MgVector4 c;
      MgVector4.Lerp(ref c1, ref c2, mu, out c);

      return new MgColor(c);
    }

    /// <summary>
    /// Interpolates between two colors using a cubic equation.
    /// </summary>
    /// <param name="color1">The first color.</param>
    /// <param name="color2">The second color.</param>
    /// <param name="mu">The weight value.</param>
    /// <returns>The interpolated color.</returns>
    public static MgColor SmoothStep(MgColor color1, MgColor color2, float mu)
    {
      var c1 = color1.ToVector4();
      var c2 = color2.ToVector4();

      MgVector4 c;
      MgVector4.SmoothStep(ref c1, ref c2, mu, out c);

      return new MgColor(c);
    }

    /// <summary>
    /// Alpha blends two colors together using the following formula:
    /// <para>Ra = Sa + Da * (1 - Sa).</para>
    /// <para>Rc = (Sc * Sa + Dc * Da * (1 - Sa)) / Ra.</para>
    /// <para>* Where R is the result, S is the source and D is the destination.</para>
    /// </summary>
    /// <param name="current">The current color.</param>
    /// <param name="color">The color to alpha blend the current color with.</param>
    /// <returns>The alpha blended color.</returns>
    public static MgColor AlphaBlend(MgColor current, MgColor color)
    {
      /*** Ra and Rc - Result alpha and color, Sa and Sc - source alpha and color, Dc and Da - Destination color and alpha.
       * Ra = Sa + Da * (1 - Sa)
       * Rc = (Sc * Sa + Dc * Da * (1 - Sa)) / Ra
      */

      var retval = current;

      var dest = current.ToVector4();
      var src = color.ToVector4();

      var Sa = src.W;
      var Da = dest.W;

      // if the passed in color has an alpha of 0, then there is no need for alpha blending
      if (Sa > 0)
      {
        var invSa = 1 - Sa;
        var daInvSa = Da * invSa;
        var Ra = Sa + daInvSa;
        var Sc = new MgVector3(src.X, src.Y, src.Z);
        var Dc = new MgVector3(dest.X, dest.Y, dest.Z);

        // Vector3 Rc = (Sc * Sa + Dc * Da * invSa) / Ra;
        MgVector3 left, right, result, Rc;
        MgVector3.Multiply(ref Sc, Sa, out left);
        MgVector3.Multiply(ref Dc, daInvSa, out right);
        MgVector3.Add(ref left, ref right, out result);
        MgVector3.Divide(ref result, Ra, out Rc);
        retval = new MgColor(new MgVector4(Rc, Ra));
      }

      return retval;
    }

    /// <summary>
    /// Determines if a point is inside of an ellipse.
    /// </summary>
    /// <param name="ellipse">The bounds of the ellipse. The major and minor axis will be determined in the function.</param>
    /// <param name="pt">The point to test.</param>
    /// <returns>A value indicating if the ellipse contains a point.</returns>
    public static bool EllipseContains(MgRectangleF ellipse, MgVector2 pt)
    {
      /*
       * X = (x-x0)*cos(t)+(y-y0)*sin(t); % Translate and rotate coords. 
       * Y = -(x-x0)*sin(t)+(y-y0)*cos(t); % to align with ellipse 
       * X^2/a^2 + Y^2/b^2 < 1
       */

      var dx = pt.X - ellipse.X;
      var dy = pt.Y - ellipse.Y;

      var a = Math.Max(ellipse.Width, ellipse.Height);
      var b = Math.Min(ellipse.Width, ellipse.Height);

      var axis = (ellipse.Width < ellipse.Height) ? YAxis : XAxis;
      var cosT = sValues[CosValue, axis];
      var sinT = sValues[SinValue, axis];

      var X = (dx * cosT) + (dy * sinT);
      var Y = -((dx * sinT) + (dy * cosT));

      var X2 = X * X;
      var Y2 = Y * Y;

      var a2 = a * a;
      var b2 = b * b;

      return ((X2 / a2) + (Y2 / b2)) < 1;
    }

    /// <summary>
    /// Determines if a point is inside a circle
    /// </summary>
    /// <param name="center">The center point of the circle (h,k).</param>
    /// <param name="radius">The radius of the circle (r)</param>
    /// <param name="point">The point to test.</param>
    /// <returns>A value indicating the circle contains a point.</returns>
    public static bool CircleContains(MgVector2 center, float radius, MgVector2 point)
    {
      var rad2 = radius * radius;
      return MgVector2.DistanceSquared(point, center) <= rad2;
    }

    /// <summary>
    /// Determines if a box (or rectangle) intersects with a circle.
    /// </summary>
    /// <param name="center">The center point of the circle (h,k).</param>
    /// <param name="radius">The radius of the circle (r)</param>
    /// <param name="box">The box to test.</param>
    /// <returns>A value indicating the circle intersects with a box.</returns>
    public static bool CircleContains(MgVector2 center, float radius, MgRectangleF box)
    {
      MgVector2[] points =
      {
        // the main corners of the rectangle
        new MgVector2(box.Left, box.Top),
        new MgVector2(box.Right, box.Top),
        new MgVector2(box.Right, box.Bottom),
        new MgVector2(box.Left, box.Bottom),

        // the center of the rectangle
        box.Center
      };

      var anyTrue = false;
      for (var i = 0; !anyTrue && i < points.Length; ++i)
      {
        var pt = points[i];
        anyTrue = CircleContains(center, radius, pt);
      }
      return anyTrue;
    }

    /// <summary>
    /// Check if a point lies inside a conical region. Good for checking if a point lies in something's
    /// field-of-view cone.
    /// </summary>
    /// <param name="point">Point to check</param>
    /// <param name="coneOrigin">Cone's origin</param>
    /// <param name="coneDirection">Cone's forward direction</param>
    /// <param name="coneAngle">Cone's theta angle (radians)</param>
    /// <returns>True if point is inside the conical region</returns>
    public static bool PointInsideCone(MgVector3 point, MgVector3 coneOrigin, MgVector3 coneDirection, double coneAngle)
    {
      var tempVect = MgVector3.Normalize(point - coneOrigin);
      return MgVector3.Dot(coneDirection, tempVect) >= Math.Cos(coneAngle);
    }

    /// <summary>
    /// 2D Check if a circle is within another circle
    /// If the circles touch on a single point, they are
    /// considered intersecting.
    /// </summary>
    /// <param name="centerA">Center of first circle.</param>
    /// <param name="radiusA">Radius of first circle.</param>
    /// <param name="centerB">Center of second circle.</param>
    /// <param name="radiusB">Radius of second circle.</param>
    /// <returns>
    /// A boolean flag indicating if the circles intersect.
    /// </returns>
    public static bool CircleInCircle(MgVector2 centerA, float radiusA, MgVector2 centerB, float radiusB)
    {
      var d = MgVector2.DistanceSquared(centerB, centerA);
      var rad2 = (radiusA * radiusA) + (radiusB * radiusB);

      // circles do not intersect if they are too far apart
      return d <= rad2;
    }

    /// <summary>
    /// 2D Check if a point lies withing a polygon.
    /// </summary>
    /// <param name="polygon">The points of the polygon.</param>
    /// <param name="testVertex">The point to check.</param>
    /// <returns>
    /// A boolean flag indicating if the test vertex
    /// is inside the polygon.
    /// </returns>
    public static bool PointInPolygon(IEnumerable<MgVector2> polygon, MgVector2 testVertex)
    {
      var c = false;
      var polygonVertex = polygon.ToArray();
      var nvert = polygonVertex.Length;
      if (nvert > 2)
      {
        int i, j;
        for (i = 0, j = nvert - 1; i < nvert; j = i++)
        {
          if (((polygonVertex[i].Y > testVertex.Y) != (polygonVertex[j].Y > testVertex.Y)) &&
            (testVertex.X < (polygonVertex[j].X - polygonVertex[i].X) *
              (testVertex.Y - polygonVertex[i].Y) /
              (polygonVertex[j].Y - polygonVertex[i].Y) + polygonVertex[i].X))
          {
            c = !c;
          }
        }
      }
      return c;
    }
  }
}