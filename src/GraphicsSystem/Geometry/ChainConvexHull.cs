﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsSystem
{
  /// <summary>
  /// Static class for performing an alternative to the Graham scan that uses a linear lexographic 
  /// sort of the point set by the x- and y-coordinates.
  /// </summary>
  public static class ChainConvexHull
  {
    /// <summary>
    /// Andrew's monotone chain 2D convex hull algorithm.
    /// </summary>
    /// <param name="P">an array of 2D points presorted by increasing x- and y-coordinates</param>
    /// <param name="n">the number of points in P[]</param>
    /// <param name="H">an array of the convex hull vertices (max is n)</param>
    /// <returns>the number of points in H[]</returns>
    public static int ComputeHull(GsVector[] P, int n, ref GsVector[] H)
    {
      // the output array H[] will be used as the stack
      int bot = 0, top = (-1);  // indices for bottom and top of the stack
      int i;                // array scan index

      // Get the indices of points with min x-coord and min|max y-coord
      int minmin = 0, minmax;
      float xmin = P[0].X;
      for (i = 1; i < n; i++)
        if (P[i].X != xmin) break;
      minmax = i - 1;
      if (minmax == n - 1)
      {       // degenerate case: all x-coords == xmin
        H[++top] = P[minmin];
        if (P[minmax].Y != P[minmin].Y) // a nontrivial segment
          H[++top] = P[minmax];
        H[++top] = P[minmin];           // add polygon endpoint
        return top + 1;
      }

      // Get the indices of points with max x-coord and min|max y-coord
      int maxmin, maxmax = n - 1;
      float xmax = P[n - 1].X;
      for (i = n - 2; i >= 0; i--)
        if (P[i].X != xmax) break;
      maxmin = i + 1;

      // Compute the lower hull on the stack H
      H[++top] = P[minmin];      // push minmin point onto stack
      i = minmax;
      while (++i <= maxmin)
      {
        // the lower line joins P[minmin] with P[maxmin]
        if (isLeft(P[minmin], P[maxmin], P[i]) >= 0 && i < maxmin)
          continue;          // ignore P[i] above or on the lower line

        while (top > 0)        // there are at least 2 points on the stack
        {
          // test if P[i] is left of the line at the stack top
          if (isLeft(H[top - 1], H[top], P[i]) > 0)
            break;         // P[i] is a new hull vertex
          else
            top--;         // pop top point off stack
        }
        H[++top] = P[i];       // push P[i] onto stack
      }

      // Next, compute the upper hull on the stack H above the bottom hull
      if (maxmax != maxmin)      // if distinct xmax points
        H[++top] = P[maxmax];  // push maxmax point onto stack
      bot = top;                 // the bottom point of the upper hull stack
      i = maxmin;
      while (--i >= minmax)
      {
        // the upper line joins P[maxmax] with P[minmax]
        if (isLeft(P[maxmax], P[minmax], P[i]) >= 0 && i > minmax)
          continue;          // ignore P[i] below or on the upper line

        while (top > bot)    // at least 2 points on the upper stack
        {
          // test if P[i] is left of the line at the stack top
          if (isLeft(H[top - 1], H[top], P[i]) > 0)
            break;         // P[i] is a new hull vertex
          else
            top--;         // pop top point off stack
        }
        H[++top] = P[i];       // push P[i] onto stack
      }
      if (minmax != minmin)
        H[++top] = P[minmin];  // push joining endpoint onto stack

      return top + 1;
    }

    /// <summary>
    /// tests if a point is Left|On|Right of an infinite line.
    /// </summary>
    /// <param name="P0">Point 1.</param>
    /// <param name="P1">Point 2.</param>
    /// <param name="P2">Point 3.</param>
    /// <returns>
    /// &gt;0 for P2 left of the line through P0 and P1
    /// =0 for P2 on the line
    /// &lt;0 for P2 right of the line
    /// </returns>
    private static float isLeft(GsVector P0, GsVector P1, GsVector P2)
    {
      return (P1.X - P0.X) * (P2.Y - P0.Y) - (P2.X - P0.X) * (P1.Y - P0.Y);
    }
  }
}