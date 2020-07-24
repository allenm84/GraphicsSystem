using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MLA.Graphics.Extensions;

namespace MLA.Graphics.Drawing
{
  /// <summary>
  /// Contains the data needed to draw a linear gradient.
  /// </summary>
  public class MgLinearGradient : MgGradient
  {
    /// <summary>
    /// Creates a new linear gradient.
    /// </summary>
    /// <param name="angle">The angle of the linear gradient.</param>
    /// <param name="color1">The starting color.</param>
    /// <param name="color2">The ending color.</param>
    public MgLinearGradient(float angle, MgColor color1, MgColor color2)
      : base(color1, color2)
    {
      Angle = angle;
    }

    /// <summary>
    /// Gets the angle of the gradient.
    /// </summary>
    public float Angle { get; internal set; }

    /// <summary>
    /// Gets the k scalar value of the gradient in the equation f(x,y) = dot(k * (u',v'), (x,y)) + b.
    /// </summary>
    public float K { get; internal set; }

    /// <summary>
    /// Gets the b constant value of the gradient in the equation f(x,y) = dot(k * (u',v'), (x,y)) + b.
    /// </summary>
    public float B { get; internal set; }

    /// <summary>
    /// Gets the direction of the gradient in the equation f(x,y) = dot(k * (u',v'), (x,y)) + b.
    /// </summary>
    public MgVector2 Direction { get; internal set; }

    internal override MgColor GetColorAt(int x, int y)
    {
      // X = (k), Y = (b), Z = (dir_x), W = (dir_y).
      var dir = MgVector2.Zero;
      dir.X = Direction.X * K;
      dir.Y = Direction.Y * K;

      var pos = MgVector2.Zero;
      pos.X = x + 1;
      pos.Y = y + 1;

      var mu = MgVector2.Dot(dir, pos) + B;
      return MgMath.Lerp(Color1, Color2, mu);
    }

    internal override void CalculateFor(IEnumerable<MgVector2> polygon)
    {
      // create a vector for the x_axis
      var x_axis = new MgVector2(1, 0);

      // create the direction unit vector
      var vec = MgVector2.Transform(x_axis, MgMatrix.CreateRotationZ(MgMath.ToRadians(Angle)));

      // find the min/max dot product
      var minDot = polygon.Min(pt => MgVector2.Dot(vec, new MgVector2(pt.X, pt.Y)));
      var maxDot = polygon.Max(pt => MgVector2.Dot(vec, new MgVector2(pt.X, pt.Y)));

      // find out k and b
      // solve the equation
      // construct an array of coefficients that looks like this
      // | [ux1 + vy1] , [1] |
      // | [ux2 + vy2] , [1] |
      // also, construct another array (rhs) that looks like this
      // | [0] |
      // | [1] |
      // the effect is that we get equations that look like
      // k(ux1 + vy1) + b = 0
      // k(ux2 + vy2) + b = 1
      var kb = SolveEquation(2, new[,] {{minDot, 1}, {maxDot, 1}}, new float[] {0, 1});

      // set the variables
      K = kb[0];
      B = kb[1];
      Direction = vec;
    }

    private static float[] SolveEquation(int n, float[,] a, float[] b)
    {
      // create variables to aid in solving
      float x, sum;

      //convert to upper triangular form
      for (var k = 0; k < n - 1; k++)
      {
        try
        {
          for (var i = k + 1; i < n; i++)
          {
            x = a[i, k] / a[k, k];
            for (var j = k + 1; j < n; j++)
            {
              a[i, j] = a[i, j] - a[k, j] * x;
            }

            b[i] = b[i] - b[k] * x;
          }
        }
        catch (DivideByZeroException e)
        {
          Console.WriteLine(e.Message);
        }
      }

      // back substitution
      b[n - 1] = b[n - 1] / a[n - 1, n - 1];
      for (var i = n - 2; i >= 0; i--)
      {
        sum = b[i];
        for (var j = i + 1; j < n; j++)
        {
          sum = sum - a[i, j] * b[j];
        }
        b[i] = sum / a[i, i];
      }

      // return b
      return b;
    }
  }
}