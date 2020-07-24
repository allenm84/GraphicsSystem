using System;
using System.Collections.Generic;

namespace MLA.Graphics
{
  /// <summary>
  /// Represents a plane figure that is bounded by a closed path, composed of a finite sequence of straight line segments
  /// (or edges).
  /// </summary>
  [Serializable]
  public sealed class MgPolygon
  {
    private MgVector2[] mEdges;
    private float mMaxX;
    private float mMaxY;
    private float mMinX;
    private float mMinY;
    private MgVector2[] mPoints;

    /// <summary>
    /// Creates a new polygon from a collection of points (connecting them together) and applies
    /// the transformation on the result.
    /// </summary>
    /// <param name="points">The points to use to construct the polygon.</param>
    /// <param name="transform">The transform to apply to the polygon.</param>
    public MgPolygon(MgVector2[] points, MgMatrix transform)
    {
      mPoints = GetPoints(points, transform);
      mEdges = GetEdges(mPoints);
    }

    /// <summary>
    /// Gets the collection of points where the edges meet. Changing this collection will not modify the
    /// actual points of the polygon (e.g. this is read-only).
    /// </summary>
    public MgVector2[] Points
    {
      get
      {
        var copy = new MgVector2[mPoints.Length];
        Array.Copy(mPoints, copy, copy.Length);
        return copy;
      }
    }

    /// <summary>
    /// Gets the collection of edges which make up the polygon. Changing this collection will not modify the
    /// actual edges of the polygon (e.g. this is read-only).
    /// </summary>
    public MgVector2[] Edges
    {
      get
      {
        var copy = new MgVector2[mEdges.Length];
        Array.Copy(mEdges, copy, copy.Length);
        return copy;
      }
    }

    private MgVector2[] GetPoints(MgVector2[] hull, MgMatrix transform)
    {
      mMinX = float.MaxValue;
      mMaxX = float.MinValue;
      mMinY = float.MaxValue;
      mMaxY = float.MinValue;

      var points = new List<MgVector2>(hull.Length);
      foreach (var pt in hull)
      {
        points.Add(MgVector2.Transform(pt, transform));

        var x = points[points.Count - 1].X;
        var y = points[points.Count - 1].Y;

        mMinX = Math.Min(mMinX, x);
        mMinY = Math.Min(mMinY, y);

        mMaxX = Math.Max(mMaxX, x);
        mMaxY = Math.Max(mMaxY, y);
      }
      return points.ToArray();
    }

    private MgVector2[] GetEdges(MgVector2[] points)
    {
      MgVector2 p1;
      MgVector2 p2;

      var edges = new List<MgVector2>(points.Length * 2);
      for (var i = 0; i < points.Length; ++i)
      {
        p1 = points[i];
        if (i + 1 >= points.Length)
        {
          p2 = points[0];
        }
        else
        {
          p2 = points[i + 1];
        }
        edges.Add(p2 - p1);
      }

      return edges.ToArray();
    }

    /// <summary>
    /// Determines if this polygon intersects with another polygon.
    /// </summary>
    /// <param name="polygon">The polygon to test against.</param>
    /// <param name="accurate">[Optional] Determines if the algorithm should be as accurate as possible (but much slower). The default is false.</param>
    /// <returns>A value indicating if the two polygons intersect.</returns>
    public bool IntersectsWith(MgPolygon polygon, bool accurate = false)
    {
      var boxA = MgRectangleF.FromLTRB(this.mMinX, this.mMinY, this.mMaxX, this.mMaxY);
      var boxB = MgRectangleF.FromLTRB(polygon.mMinX, polygon.mMinY, polygon.mMaxX, polygon.mMaxY);
      var intersects = boxA.IntersectsWith(boxB);

      if (!intersects)
      {
        return false;
      }

      if (accurate)
      {
        MgVector2[] edgesA = mEdges;
        MgVector2[] edgesB = polygon.mEdges;

        int edgeCountA = edgesA.Length;
        int edgeCountB = edgesB.Length;
        MgVector2 edge = MgVector2.Zero;

        // Loop through all the edges of both polygons
        for (int edgeIndex = 0; intersects && (edgeIndex < edgeCountA + edgeCountB); ++edgeIndex)
        {
          if (edgeIndex < edgeCountA)
          {
            edge = edgesA[edgeIndex];
          }
          else
          {
            edge = edgesB[edgeIndex - edgeCountA];
          }

          // Find the axis perpendicular to the current edge
          MgVector2 axis = new MgVector2(-edge.Y, edge.X);
          axis.Normalize();

          // Find the projection of the polygon on the current axis
          float minA = 0; float minB = 0; float maxA = 0; float maxB = 0;
          ProjectPolygon(axis, this, ref minA, ref maxA);
          ProjectPolygon(axis, polygon, ref minB, ref maxB);

          // Check if the polygon projections are currentlty intersecting
          if (IntervalDistance(minA, maxA, minB, maxB) > 0)
            intersects = false;
        }
      }

      return intersects;
    }

    /// <summary>
    /// Calculate the distance between [minA, maxA] and [minB, maxB]. The distance will
    /// be negative if the intervals overlap
    /// </summary>
    private static float IntervalDistance(float minA, float maxA, float minB, float maxB)
    {
      if (minA < minB)
      {
        return minB - maxA;
      }
      return minA - maxB;
    }

    /// <summary>
    /// Calculate the projection of a polygon on an axis and returns it as a [min, max] interval
    /// </summary>
    private static void ProjectPolygon(MgVector2 axis, MgPolygon hull, ref float min, ref float max)
    {
      // get the points
      var points = hull.mPoints;

      // To project a point on an axis use the dot product
      var d = MgVector2.Dot(axis, points[0]);
      min = d;
      max = d;
      for (var i = 0; i < points.Length; i++)
      {
        d = MgVector2.Dot(points[i], axis);
        min = Math.Min(min, d);
        max = Math.Max(max, d);
      }
    }
  }
}