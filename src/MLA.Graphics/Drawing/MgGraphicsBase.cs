using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MLA.Graphics.Drawing.Internal;
using MLA.Graphics.Extensions;

namespace MLA.Graphics.Drawing
{
  /// <summary>
  /// Provides a base for graphics classes.
  /// </summary>
  public abstract class MgGraphicsBase
  {
    /// <summary>The default number of slices to use when constructing an ellipse.</summary>
    internal const int DefaultSlices = 32;

    /// <summary>The amount to increment the angle by when construction an arc.</summary>
    internal const int AngleIncrement = 20;

    /// <summary>The amount to step when incrementing a delta.</summary>
    internal const float ThetaStep = MgMath.TwoPi / DefaultSlices;

    /// <summary>
    /// Initializes all the settings to their default values.
    /// </summary>
    protected MgGraphicsBase()
    {
      Slices = DefaultSlices;
    }

    #region Helper Functions

    #region Create Arc

    internal static IEnumerable<MgVector2> CreateArc(float cx, float cy, float width, float height, int startAngle, int sweepAngle)
    {
      var retval = new List<MgVector2>();
      var endAngle = startAngle + sweepAngle;
      var sign = Math.Sign(endAngle - startAngle);
      var negsign = -sign;
      var d = sign * AngleIncrement;

      for (var angle = startAngle; angle.CompareTo(endAngle).Equals(negsign); angle += d)
      {
        retval.Add(new MgVector2
        {
          X = cx + width * MgMath.Cos(angle),
          Y = cy + height * MgMath.Sin(angle),
        });
      }

      return retval;
    }

    #endregion

    #region Create Bezier

    internal static IEnumerable<MgVector2> CreateBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
    {
      MgVector2[] array =
      {
        new MgVector2(x1, y1),
        new MgVector2(x2, y2),
        new MgVector2(x3, y3),
        new MgVector2(x4, y4)
      };
      return CreateBezier(array);
    }

    internal static IEnumerable<MgVector2> CreateBezier(IEnumerable<MgVector2> points)
    {
      var array = points.ToArray();
      var len = array.Length;
      var count = len * 3;

      var input = new double[len * 2];
      var output = new double[count * 2];

      for (var i = 0; i < input.Length; i += 2)
      {
        var j = i / 2;
        input[i] = array[j].X;
        input[i + 1] = array[j].Y;
      }

      BeizerMaker.Bezier2D(ref input, count, ref output);

      var retval = new List<MgVector2>();
      for (var i = 0; i < output.Length; i += 2)
      {
        retval.Add(new MgVector2
        {
          X = (float)output[i],
          Y = (float)output[i + 1],
        });
      }

      return retval;
    }

    #endregion

    #region Create Round Rectangle

    internal static IEnumerable<MgVector2> CreateRoundRect(MgRectangleF rect, float radius, MgRectangleCorners corners)
    {
      var l = rect.Left;
      var t = rect.Top;
      var r = rect.Right;
      var b = rect.Bottom;

      var lr = rect.Left + radius;
      var tr = rect.Top + radius;
      var rr = rect.Right - radius;
      var br = rect.Bottom - radius;

      var polygon = new List<MgVector2>();

      // upper-left
      if ((corners & MgRectangleCorners.TopLeft) != 0)
      {
        var upleft_corner = new MgVector2[3]
        {
          new MgVector2(l, tr),
          new MgVector2(l, t),
          new MgVector2(lr, t)
        };
        polygon.AddRange(CreateBezier(upleft_corner));
      }
      else
      {
        polygon.Add(new MgVector2(l, t));
      }

      // upper-right
      if ((corners & MgRectangleCorners.TopRight) != 0)
      {
        var upright_corner = new MgVector2[3]
        {
          new MgVector2(rr, t),
          new MgVector2(r, t),
          new MgVector2(r, tr)
        };
        polygon.AddRange(CreateBezier(upright_corner));
      }
      else
      {
        polygon.Add(new MgVector2(r, t));
      }

      // bottom-right
      if ((corners & MgRectangleCorners.BottomRight) != 0)
      {
        var bottomright_corner = new MgVector2[3]
        {
          new MgVector2(r, br),
          new MgVector2(r, b),
          new MgVector2(rr, b)
        };
        polygon.AddRange(CreateBezier(bottomright_corner));
      }
      else
      {
        polygon.Add(new MgVector2(r, b));
      }

      // bottom-left
      if ((corners & MgRectangleCorners.BottomLeft) != 0)
      {
        var bottomleft_corner = new MgVector2[3]
        {
          new MgVector2(lr, b),
          new MgVector2(l, b),
          new MgVector2(l, br)
        };
        polygon.AddRange(CreateBezier(bottomleft_corner));
      }
      else
      {
        polygon.Add(new MgVector2(l, b));
      }

      // close it up
      polygon.Add(polygon[0]);

      // return it!
      return polygon;
    }

    #endregion

    #region Create Ellipse

    internal static MgVector2[] CreateEllipse(float h, float k, float width, float height, int slices)
    {
      // create a list of points of the circle
      var max = MgMath.TwoPi;
      var step = max / slices;
      var vectors = new List<MgVector2>();
      for (var t = 0.0f; t < max; t += step)
      {
        var x = h + width * MgMath.Cos(t);
        var y = k + height * MgMath.Sin(t);
        vectors.Add(new MgVector2(x, y));
      }
      vectors.Add(vectors[0]);

      // return the points
      return vectors.ToArray();
    }

    #endregion

    #region Create Donut

    internal static void CreateDonut(float cx, float cy, float outerRadius, float innerRadius, out int[] indices, out MgVector2[] vertices)
    {
      var opts = new List<MgVector2>();
      var ipts = new List<MgVector2>();
      for (float theta = 0; theta < MgMath.TwoPi; theta += ThetaStep)
      {
        var cos = (float)Math.Cos(theta);
        var sin = (float)Math.Sin(theta);

        var xOut = cx + (outerRadius * cos);
        var yOut = cy + (outerRadius * sin);

        var xIn = cx + (innerRadius * cos);
        var yIn = cy + (innerRadius * sin);

        opts.Add(new MgVector2(xOut, yOut));
        ipts.Add(new MgVector2(xIn, yIn));
      }

      opts.Add(opts[0]);
      ipts.Add(ipts[0]);

      var pointData = new List<MgVector2>();
      var indexData = new List<int>();

      var outerOffset = 0;
      pointData.AddRange(opts);

      var innerOffset = pointData.Count;
      pointData.AddRange(ipts);

      for (var i = 0; i < opts.Count - 1; ++i)
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

    #endregion

    #endregion

    #region Draw Line

    /// <summary>
    /// Draws a line.
    /// </summary>
    /// <param name="x0">The starting x-coordinate.</param>
    /// <param name="y0">The starting y-coordinate.</param>
    /// <param name="x1">The ending x-coordinate.</param>
    /// <param name="y1">The ending y-coordinate.</param>
    /// <param name="color">The color of the line to draw.</param>
    public abstract void DrawLine(float x0, float y0, float x1, float y1, MgColor color);

    /// <summary>
    /// Draws a line.
    /// </summary>
    /// <param name="p0">The starting point of the line.</param>
    /// <param name="p1">The ending point of the line.</param>
    /// <param name="color">The color of the line to draw.</param>
    public void DrawLine(MgVector2 p0, MgVector2 p1, MgColor color)
    {
      DrawLine(p0.X, p0.Y, p1.X, p1.Y, color);
    }

    #endregion

    #region Draw Arc

    /// <summary>
    /// Draws an arc representing a portion of an ellipse.
    /// </summary>
    /// <param name="cx">The center x-coordinate of the arc (or ellipse).</param>
    /// <param name="cy">The center y-coordinate of the arc (or ellipse).</param>
    /// <param name="width">The width of the arc (or ellipse).</param>
    /// <param name="height">The height of the arc (or ellipse).</param>
    /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc.</param>
    /// <param name="sweepAngle">Angle in degrees measured clockwise from the startAngle parameter to ending point of the arc.</param>
    /// <param name="color">The color of the arc</param>
    public virtual void DrawArc(float cx, float cy, float width, float height, int startAngle, int sweepAngle, MgColor color)
    {
      var arc = CreateArc(cx, cy, width, height, startAngle, sweepAngle);
      DrawPolygon(arc, color);
    }

    /// <summary>
    /// Draws an arc representing a portion of an ellipse.
    /// </summary>
    /// <param name="center">The center of the arc (or ellipse).</param>
    /// <param name="size">The size of the arc (or ellipse).</param>
    /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc.</param>
    /// <param name="sweepAngle">Angle in degrees measured clockwise from the startAngle parameter to ending point of the arc.</param>
    /// <param name="color">The color of the arc</param>
    public void DrawArc(MgVector2 center, MgVector2 size, int startAngle, int sweepAngle, MgColor color)
    {
      DrawArc(center.X, center.Y, size.X, size.Y, startAngle, sweepAngle, color);
    }

    /// <summary>
    /// Draws an arc representing a portion of an ellipse.
    /// </summary>
    /// <param name="center">The center of the arc (or ellipse).</param>
    /// <param name="size">The size of the arc (or ellipse).</param>
    /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc.</param>
    /// <param name="sweepAngle">Angle in degrees measured clockwise from the startAngle parameter to ending point of the arc.</param>
    /// <param name="color">The color of the arc</param>
    public void DrawArc(MgVector2 center, MgSizeF size, int startAngle, int sweepAngle, MgColor color)
    {
      DrawArc(center.X, center.Y, size.Width, size.Height, startAngle, sweepAngle, color);
    }

    /// <summary>
    /// Draws an arc representing a portion of an ellipse.
    /// </summary>
    /// <param name="rect">The bounds of the arc (or ellipse). The x,y must correspond to the center of the arc.</param>
    /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc.</param>
    /// <param name="sweepAngle">Angle in degrees measured clockwise from the startAngle parameter to ending point of the arc.</param>
    /// <param name="color">The color of the arc</param>
    public virtual void DrawArc(MgRectangleF rect, int startAngle, int sweepAngle, MgColor color)
    {
      DrawArc(rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle, color);
    }

    #endregion

    #region Draw Beizer

    /// <summary>
    /// Draws a beizer curve.
    /// </summary>
    /// <param name="x0">The x-coordinate of the starting point of the curve.</param>
    /// <param name="y0">The y-coordinate of the starting point of the curve.</param>
    /// <param name="x1">The x-coordinate of the first control point of the curve.</param>
    /// <param name="y1">The y-coordinate of the first control point of the curve.</param>
    /// <param name="x2">The x-coordinate of the second control point of the curve.</param>
    /// <param name="y2">The y-coordinate of the second control point of the curve</param>
    /// <param name="x3">The x-coordinate of the ending point of the curve.</param>
    /// <param name="y3">The y-coordinate of the ending point of the curve.</param>
    /// <param name="color">The color of the curve.</param>
    public virtual void DrawBeizer(float x0, float y0, float x1, float y1, float x2, float y2, float x3, float y3, MgColor color)
    {
      var beizer = CreateBezier(x0, y0, x1, y1, x2, y2, x3, y3);
      DrawPolygon(beizer, color);
    }

    /// <summary>
    /// Draws a beizer curve.
    /// </summary>
    /// <param name="controlPoints">The points to use for interpolating the curve.</param>
    /// <param name="color">The color of the curve.</param>
    public virtual void DrawBeizer(IEnumerable<MgVector2> controlPoints, MgColor color)
    {
      var beizer = CreateBezier(controlPoints);
      DrawPolygon(beizer.ToArray(), color);
    }

    #endregion

    #region Draw Rectangle

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    /// <param name="x">The upper-left x-coordinate.</param>
    /// <param name="y">The upper-left y-coordinate.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <param name="color">The color of the rectangle.</param>
    public virtual void DrawRectangle(float x, float y, float width, float height, MgColor color)
    {
      var rect = new MgRectangleF(x, y, width, height);
      DrawPolygon(CreateRoundRect(rect, 0, MgRectangleCorners.None).ToArray(), color);
    }

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    /// <param name="location">The upper-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void DrawRectangle(MgVector2 location, MgSizeF size, MgColor color)
    {
      DrawRectangle(location.X, location.Y, size.Width, size.Height, color);
    }

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    /// <param name="location">The upper-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void DrawRectangle(MgVector2 location, MgVector2 size, MgColor color)
    {
      DrawRectangle(location.X, location.Y, size.X, size.Y, color);
    }

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    /// <param name="rect">The bounds of the rectangle.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void DrawRectangle(MgRectangleF rect, MgColor color)
    {
      DrawRectangle(rect.X, rect.Y, rect.Width, rect.Height, color);
    }

    #endregion

    #region Draw Round Rectangle

    /// <summary>
    /// Draws a rounded rectangle.
    /// </summary>
    /// <param name="x">The upper-left x-coordinate.</param>
    /// <param name="y">The upper-left y-coordinate.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <param name="radius">The radius of each corner of the rectangle.</param>
    /// <param name="part">The parts of the rectangle to round.</param>
    /// <param name="color">The color of the rectangle.</param>
    public virtual void DrawRoundRectangle(float x, float y, float width, float height, float radius, MgRectangleCorners part, MgColor color)
    {
      var rect = new MgRectangleF(x, y, width, height);
      DrawPolygon(CreateRoundRect(rect, radius, part), color);
    }

    /// <summary>
    /// Draws a rounded rectangle.
    /// </summary>
    /// <param name="x">The upper-left x-coordinate.</param>
    /// <param name="y">The upper-left y-coordinate.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <param name="radius">The radius of each corner of the rectangle.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void DrawRoundRectangle(float x, float y, float width, float height, float radius, MgColor color)
    {
      DrawRoundRectangle(x, y, width, height, radius, MgRectangleCorners.All, color);
    }

    /// <summary>
    /// Draws a rounded rectangle.
    /// </summary>
    /// <param name="location">The upper-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="radius">The radius of each corner of the rectangle.</param>
    /// <param name="part">The parts of the rectangle to round.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void DrawRoundRectangle(MgVector2 location, MgSizeF size, float radius, MgRectangleCorners part, MgColor color)
    {
      DrawRoundRectangle(location.X, location.Y, size.Width, size.Height, radius, part, color);
    }

    /// <summary>
    /// Draws a rounded rectangle.
    /// </summary>
    /// <param name="location">The upper-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="radius">The radius of each corner of the rectangle.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void DrawRoundRectangle(MgVector2 location, MgSizeF size, float radius, MgColor color)
    {
      DrawRoundRectangle(location.X, location.Y, size.Width, size.Height, radius, MgRectangleCorners.All, color);
    }

    /// <summary>
    /// Draws a rounded rectangle.
    /// </summary>
    /// <param name="location">The upper-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="radius">The radius of each corner of the rectangle.</param>
    /// <param name="part">The parts of the rectangle to round.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void DrawRoundRectangle(MgVector2 location, MgVector2 size, float radius, MgRectangleCorners part, MgColor color)
    {
      DrawRoundRectangle(location.X, location.Y, size.X, size.Y, radius, part, color);
    }

    /// <summary>
    /// Draws a rounded rectangle.
    /// </summary>
    /// <param name="location">The upper-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="radius">The radius of each corner of the rectangle.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void DrawRoundRectangle(MgVector2 location, MgVector2 size, float radius, MgColor color)
    {
      DrawRoundRectangle(location.X, location.Y, size.X, size.Y, radius, MgRectangleCorners.All, color);
    }

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    /// <param name="rect">The bounds of the rectangle.</param>
    /// <param name="radius">The radius of each corner of the rectangle.</param>
    /// <param name="part">The parts of the rectangle to round.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void DrawRoundRectangle(MgRectangleF rect, float radius, MgRectangleCorners part, MgColor color)
    {
      DrawRoundRectangle(rect.X, rect.Y, rect.Width, rect.Height, radius, part, color);
    }

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    /// <param name="rect">The bounds of the rectangle.</param>
    /// <param name="radius">The radius of each corner of the rectangle.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void DrawRoundRectangle(MgRectangleF rect, float radius, MgColor color)
    {
      DrawRoundRectangle(rect.X, rect.Y, rect.Width, rect.Height, radius, MgRectangleCorners.All, color);
    }

    #endregion

    #region Draw Circle

    /// <summary>
    /// Draws a circle.
    /// </summary>
    /// <param name="h">The center x-coordinate of the circle.</param>
    /// <param name="k">The center y-coordinate of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="color">The color of the circle.</param>
    public void DrawCircle(float h, float k, float radius, MgColor color)
    {
      DrawEllipse(h, k, radius, radius, color);
    }

    /// <summary>
    /// Draws a circle.
    /// </summary>
    /// <param name="center">The center of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="color">The color of the circle.</param>
    public void DrawCircle(MgVector2 center, float radius, MgColor color)
    {
      DrawCircle(center.X, center.Y, radius, color);
    }

    #endregion

    #region Draw Ellipse

    /// <summary>
    /// Draws an ellipse.
    /// </summary>
    /// <param name="h">The center x-coordinate of the ellipse.</param>
    /// <param name="k">The center y-coordinate of the ellipse.</param>
    /// <param name="width">The width of the ellipse.</param>
    /// <param name="height">The height of the ellipse.</param>
    /// <param name="color">The color of the ellipse.</param>
    public virtual void DrawEllipse(float h, float k, float width, float height, MgColor color)
    {
      var ellipse = CreateEllipse(h, k, width, height, Slices);
      DrawPolygon(ellipse, color);
    }

    /// <summary>
    /// Draws an ellipse.
    /// </summary>
    /// <param name="center">The center of the ellipse</param>
    /// <param name="size">The size of the ellipse.</param>
    /// <param name="color">The color of the ellipse.</param>
    public void DrawEllipse(MgVector2 center, MgSizeF size, MgColor color)
    {
      DrawEllipse(center.X, center.Y, size.Width, size.Height, color);
    }

    /// <summary>
    /// Draws an ellipse.
    /// </summary>
    /// <param name="center">The center of the ellipse</param>
    /// <param name="size">The size of the ellipse.</param>
    /// <param name="color">The color of the ellipse.</param>
    public void DrawEllipse(MgVector2 center, MgVector2 size, MgColor color)
    {
      DrawEllipse(center.X, center.Y, size.X, size.Y, color);
    }

    /// <summary>
    /// Draws an ellipse.
    /// </summary>
    /// <param name="rect">The bounds of the ellipse.</param>
    /// <param name="color">The color of the ellipse.</param>
    public void DrawEllipse(MgRectangleF rect, MgColor color)
    {
      DrawEllipse(rect.X, rect.Y, rect.Width, rect.Height, color);
    }

    #endregion

    #region Draw Polygon

    /// <summary>
    /// Draws a polygon.
    /// </summary>
    /// <param name="polygon">The collection of points to draw.</param>
    /// <param name="color">The color of the polygon.</param>
    public virtual void DrawPolygon(IEnumerable<MgVector2> polygon, MgColor color)
    {
      var data = polygon.ToArray();
      for (var i = 1; i < data.Length; ++i)
      {
        var v0 = data[i - 1];
        var v1 = data[i];
        DrawLine(v0, v1, color);
      }
    }

    #endregion

    #region Fill Rectangle

    /// <summary>
    /// Fills a rectangle.
    /// </summary>
    /// <param name="x">The upper-left x-coordinate.</param>
    /// <param name="y">The upper-left y-coordinate.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <param name="color">The color of the rectangle.</param>
    public virtual void FillRectangle(float x, float y, float width, float height, MgColor color)
    {
      var vectors = new MgVector2[4];

      vectors[0] = new MgVector2(x, y);
      vectors[1] = new MgVector2(x, y + height);
      vectors[2] = new MgVector2(x + width, y + height);
      vectors[3] = new MgVector2(x + width, y);

      FillPolygon(vectors, color);
    }

    /// <summary>
    /// Fills a rectangle.
    /// </summary>
    /// <param name="location">The upper-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void FillRectangle(MgVector2 location, MgSizeF size, MgColor color)
    {
      FillRectangle(location.X, location.Y, size.Width, size.Height, color);
    }

    /// <summary>
    /// Fills a rectangle.
    /// </summary>
    /// <param name="location">The upper-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void FillRectangle(MgVector2 location, MgVector2 size, MgColor color)
    {
      FillRectangle(location.X, location.Y, size.X, size.Y, color);
    }

    /// <summary>
    /// Fills a rectangle.
    /// </summary>
    /// <param name="rect">The bounds of the rectangle.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void FillRectangle(MgRectangleF rect, MgColor color)
    {
      FillRectangle(rect.X, rect.Y, rect.Width, rect.Height, color);
    }

    #endregion

    #region Fill Round Rectangle

    /// <summary>
    /// Fills a rectangle.
    /// </summary>
    /// <param name="x">The upper-left x-coordinate.</param>
    /// <param name="y">The upper-left y-coordinate.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <param name="radius">The radius of each corner of the rectangle.</param>
    /// <param name="part">The parts of the rectangle to round.</param>
    /// <param name="color">The color of the rectangle.</param>
    public virtual void FillRoundRectangle(float x, float y, float width, float height, float radius, MgRectangleCorners part, MgColor color)
    {
      var rect = new MgRectangleF(x, y, width, height);
      FillPolygon(CreateRoundRect(rect, radius, part), color);
    }

    /// <summary>
    /// Fills a rectangle.
    /// </summary>
    /// <param name="x">The upper-left x-coordinate.</param>
    /// <param name="y">The upper-left y-coordinate.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <param name="radius">The radius of each corner of the rectangle.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void FillRoundRectangle(float x, float y, float width, float height, float radius, MgColor color)
    {
      FillRoundRectangle(x, y, width, height, radius, MgRectangleCorners.All, color);
    }

    /// <summary>
    /// Fills a rectangle.
    /// </summary>
    /// <param name="location">The upper-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="radius">The radius of each corner of the rectangle.</param>
    /// <param name="part">The parts of the rectangle to round.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void FillRoundRectangle(MgVector2 location, MgSizeF size, float radius, MgRectangleCorners part, MgColor color)
    {
      FillRoundRectangle(location.X, location.Y, size.Width, size.Height, radius, part, color);
    }

    /// <summary>
    /// Fills a rectangle.
    /// </summary>
    /// <param name="location">The upper-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="radius">The radius of each corner of the rectangle.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void FillRoundRectangle(MgVector2 location, MgSizeF size, float radius, MgColor color)
    {
      FillRoundRectangle(location.X, location.Y, size.Width, size.Height, radius, MgRectangleCorners.All, color);
    }

    /// <summary>
    /// Fills a rectangle.
    /// </summary>
    /// <param name="location">The upper-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="radius">The radius of each corner of the rectangle.</param>
    /// <param name="part">The parts of the rectangle to round.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void FillRoundRectangle(MgVector2 location, MgVector2 size, float radius, MgRectangleCorners part, MgColor color)
    {
      FillRoundRectangle(location.X, location.Y, size.X, size.Y, radius, part, color);
    }

    /// <summary>
    /// Fills a rectangle.
    /// </summary>
    /// <param name="location">The upper-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="radius">The radius of each corner of the rectangle.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void FillRoundRectangle(MgVector2 location, MgVector2 size, float radius, MgColor color)
    {
      FillRoundRectangle(location.X, location.Y, size.X, size.Y, radius, MgRectangleCorners.All, color);
    }

    /// <summary>
    /// Fills a rectangle.
    /// </summary>
    /// <param name="rect">The bounds of the rectangle.</param>
    /// <param name="radius">The radius of each corner of the rectangle.</param>
    /// <param name="part">The parts of the rectangle to round.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void FillRoundRectangle(MgRectangleF rect, float radius, MgRectangleCorners part, MgColor color)
    {
      FillRoundRectangle(rect.X, rect.Y, rect.Width, rect.Height, radius, part, color);
    }

    /// <summary>
    /// Fills a rectangle.
    /// </summary>
    /// <param name="rect">The bounds of the rectangle.</param>
    /// <param name="radius">The radius of each corner of the rectangle.</param>
    /// <param name="color">The color of the rectangle.</param>
    public void FillRoundRectangle(MgRectangleF rect, float radius, MgColor color)
    {
      FillRoundRectangle(rect.X, rect.Y, rect.Width, rect.Height, radius, MgRectangleCorners.All, color);
    }

    #endregion

    #region Fill Circle

    /// <summary>
    /// Fills a circle.
    /// </summary>
    /// <param name="h">The center x-coordinate of the circle.</param>
    /// <param name="k">The center y-coordinate of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="color">The color of the circle.</param>
    public void FillCircle(float h, float k, float radius, MgColor color)
    {
      FillEllipse(h, k, radius, radius, color);
    }

    /// <summary>
    /// Fills a circle
    /// </summary>
    /// <param name="center">The center of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="color">The color of the circle.</param>
    public void FillCircle(MgVector2 center, float radius, MgColor color)
    {
      FillCircle(center.X, center.Y, radius, color);
    }

    #endregion

    #region Fill Ellipse

    /// <summary>
    /// Fills an ellipse.
    /// </summary>
    /// <param name="h">The center x-coordinate of the ellipse.</param>
    /// <param name="k">The center y-coordinate of the ellipse.</param>
    /// <param name="width">The width of the ellipse.</param>
    /// <param name="height">The height of the ellipse.</param>
    /// <param name="color">The color of the ellipse.</param>
    public virtual void FillEllipse(float h, float k, float width, float height, MgColor color)
    {
      var points = CreateEllipse(h, k, width, height, Slices);
      FillPolygon(points, color);
    }

    /// <summary>
    /// Fills an ellipse.
    /// </summary>
    /// <param name="center">The center of the ellipse</param>
    /// <param name="size">The size of the ellipse.</param>
    /// <param name="color">The color of the ellipse.</param>
    public void FillEllipse(MgVector2 center, MgSizeF size, MgColor color)
    {
      FillEllipse(center.X, center.Y, size.Width, size.Height, color);
    }

    /// <summary>
    /// Fills an ellipse.
    /// </summary>
    /// <param name="center">The center of the ellipse</param>
    /// <param name="size">The size of the ellipse.</param>
    /// <param name="color">The color of the ellipse.</param>
    public void FillEllipse(MgVector2 center, MgVector2 size, MgColor color)
    {
      FillEllipse(center.X, center.Y, size.X, size.Y, color);
    }

    /// <summary>
    /// Fills an ellipse.
    /// </summary>
    /// <param name="rect">The bounds of the ellipse.</param>
    /// <param name="color">The color of the ellipse.</param>
    public void FillEllipse(MgRectangleF rect, MgColor color)
    {
      FillEllipse(rect.X, rect.Y, rect.Width, rect.Height, color);
    }

    #endregion

    #region Fill Polygon

    /// <summary>
    /// Fills a polygon.
    /// </summary>
    /// <param name="polygon">The polygon to fill.</param>
    /// <param name="color">The color to fill the polygon with.</param>
    public abstract void FillPolygon(IEnumerable<MgVector2> polygon, MgColor color);

    #endregion

    /// <summary>
    /// Gets or sets the number of slices to use when constructing an ellipse. The default is 32.
    /// </summary>
    public int Slices { get; set; }

    /// <summary>
    /// Prepares for rendereing.
    /// </summary>
    public virtual void Begin() {}

    /// <summary>
    /// Ends rendering.
    /// </summary>
    public virtual void End() {}
  }
}