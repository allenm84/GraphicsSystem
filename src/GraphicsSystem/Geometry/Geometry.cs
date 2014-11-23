using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsSystem
{
  public static class Geometry
  {
    /// <summary>The default number of slices to use when constructing an ellipse.</summary>
    internal const int DefaultSlices = 32;

    /// <summary>The amount to increment the angle by when construction an arc.</summary>
    internal const int AngleIncrement = 20;

    /// <summary>The amount to step when incrementing a delta.</summary>
    internal const float ThetaStep = GsMath.TwoPi / DefaultSlices;

    public static IEnumerable<GsVector> CreateArc(float cx, float cy, float width, float height, int startAngle, int sweepAngle)
    {
      int endAngle = startAngle + sweepAngle;
      int sign = Math.Sign(endAngle - startAngle);
      int negsign = -sign;
      int d = sign * AngleIncrement;

      for (int angle = startAngle; angle.CompareTo(endAngle).Equals(negsign); angle += d)
      {
        yield return new GsVector
        {
          X = cx + width * GsMath.Cos(angle),
          Y = cy + height * GsMath.Sin(angle),
        };
      }
    }

    public static IEnumerable<GsVector> CreateBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
    {
      GsVector[] array = new GsVector[]
      {
        new GsVector(x1, y1),
        new GsVector(x2, y2),
        new GsVector(x3, y3),
        new GsVector(x4, y4),
      };
      return CreateBezier(array);
    }

    public static IEnumerable<GsVector> CreateBezier(IEnumerable<GsVector> points)
    {
      GsVector[] array = points.ToArray();
      int len = array.Length;
      int count = len * 3;

      double[] input = new double[len * 2];
      double[] output = new double[count * 2];

      for (int i = 0; i < input.Length; i += 2)
      {
        int j = i / 2;
        input[i] = array[j].X;
        input[i + 1] = array[j].Y;
      }

      BeizerMaker.Bezier2D(ref input, count, ref output);
      for (int i = 0; i < output.Length; i += 2)
      {
        yield return new GsVector
        {
          X = (float)output[i],
          Y = (float)output[i + 1],
        };
      }
    }

    public static IEnumerable<GsVector> CreateRoundRect(GsRectangle rect, float radius, RectangleCorners corners)
    {
      float l = rect.Left;
      float t = rect.Top;
      float r = rect.Right;
      float b = rect.Bottom;

      float lr = rect.Left + radius;
      float tr = rect.Top + radius;
      float rr = rect.Right - radius;
      float br = rect.Bottom - radius;

      List<GsVector> polygon = new List<GsVector>();

      // upper-left
      if ((corners & RectangleCorners.TopLeft) != 0)
      {
        GsVector[] upleft_corner = new GsVector[3]
        {
          new GsVector(l, tr),
          new GsVector(l, t),
          new GsVector(lr, t),
        };
        polygon.AddRange(CreateBezier(upleft_corner));
      }
      else
      {
        polygon.Add(new GsVector(l, t));
      }

      // upper-right
      if ((corners & RectangleCorners.TopRight) != 0)
      {
        GsVector[] upright_corner = new GsVector[3]
        {
          new GsVector(rr, t),
          new GsVector(r, t),
          new GsVector(r, tr),
        };
        polygon.AddRange(CreateBezier(upright_corner));
      }
      else
      {
        polygon.Add(new GsVector(r, t));
      }

      // bottom-right
      if ((corners & RectangleCorners.BottomRight) != 0)
      {
        GsVector[] bottomright_corner = new GsVector[3]
        {
          new GsVector(r, br),
          new GsVector(r, b),
          new GsVector(rr, b),
        };
        polygon.AddRange(CreateBezier(bottomright_corner));
      }
      else
      {
        polygon.Add(new GsVector(r, b));
      }

      // bottom-left
      if ((corners & RectangleCorners.BottomLeft) != 0)
      {
        GsVector[] bottomleft_corner = new GsVector[3]
        {
          new GsVector(lr, b),
          new GsVector(l, b),
          new GsVector(l, br),
        };
        polygon.AddRange(CreateBezier(bottomleft_corner));
      }
      else
      {
        polygon.Add(new GsVector(l, b));
      }

      // close it up
      polygon.Add(polygon[0]);

      // return it!
      return polygon;
    }

    public static GsVector[] CreateEllipse(float h, float k, float width, float height, int slices = DefaultSlices)
    {
      // create a list of points of the circle
      float max = GsMath.TwoPi;
      float step = max / (float)slices;
      List<GsVector> vectors = new List<GsVector>();
      for (float t = 0.0f; t < max; t += step)
      {
        float x = (float)(h + width * GsMath.Cos(t));
        float y = (float)(k + height * GsMath.Sin(t));
        vectors.Add(new GsVector(x, y));
      }
      vectors.Add(vectors[0]);

      // return the points
      return vectors.ToArray();
    }

    public static void CreateDonut(float cx, float cy, float outerRadius, float innerRadius, out int[] indices, out GsVector[] vertices)
    {
      List<GsVector> opts = new List<GsVector>();
      List<GsVector> ipts = new List<GsVector>();
      for (float theta = 0; theta < GsMath.TwoPi; theta += ThetaStep)
      {
        float cos = (float)Math.Cos((double)theta);
        float sin = (float)Math.Sin((double)theta);

        float xOut = cx + (outerRadius * cos);
        float yOut = cy + (outerRadius * sin);

        float xIn = cx + (innerRadius * cos);
        float yIn = cy + (innerRadius * sin);

        opts.Add(new GsVector(xOut, yOut));
        ipts.Add(new GsVector(xIn, yIn));
      }

      opts.Add(opts[0]);
      ipts.Add(ipts[0]);

      List<GsVector> pointData = new List<GsVector>();
      List<int> indexData = new List<int>();

      int outerOffset = 0;
      pointData.AddRange(opts);

      int innerOffset = pointData.Count;
      pointData.AddRange(ipts);

      for (int i = 0; i < opts.Count - 1; ++i)
      {
        indexData.Add(innerOffset + i);
        indexData.Add(outerOffset + i);
        indexData.Add(outerOffset + i + 1);

        indexData.Add(innerOffset + i);
        indexData.Add(outerOffset + i + 1);
        indexData.Add(innerOffset + i + 1);
      }

      vertices = pointData.ToArray();
      indices = indexData.ToArray();
    }
  }
}
