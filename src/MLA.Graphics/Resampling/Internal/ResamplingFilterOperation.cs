using System;

namespace MLA.Graphics.Imaging
{
  /// <summary>
  /// Represents a filter used to resample an image. More specifically, resample a single pixel to determine
  /// its value based on the pixels around it.
  /// </summary>
  internal abstract class ResamplingFilterOperation
  {
    /// <summary>
    /// Gets or sets the default filter radius.
    /// </summary>
    public float defaultFilterRadius;

    /// <summary>
    /// Creates a resampling filter from the passed in enum value.
    /// </summary>
    /// <param name="filter">The type of filter to create.</param>
    /// <returns>The filter to use for resampling a pixel.</returns>
    public static ResamplingFilterOperation Create(MgResamplingFilter filter)
    {
      ResamplingFilterOperation resamplingFilter = null;
      switch (filter)
      {
        case MgResamplingFilter.Box:
          resamplingFilter = new BoxFilter();
          break;
        case MgResamplingFilter.Triangle:
          resamplingFilter = new TriangleFilter();
          break;
        case MgResamplingFilter.Hermite:
          resamplingFilter = new HermiteFilter();
          break;
        case MgResamplingFilter.Bell:
          resamplingFilter = new BellFilter();
          break;
        case MgResamplingFilter.CubicBSpline:
          resamplingFilter = new CubicBSplineFilter();
          break;
        case MgResamplingFilter.Lanczos3:
          resamplingFilter = new Lanczos3Filter();
          break;
        case MgResamplingFilter.Mitchell:
          resamplingFilter = new MitchellFilter();
          break;
        case MgResamplingFilter.Cosine:
          resamplingFilter = new CosineFilter();
          break;
        case MgResamplingFilter.CatmullRom:
          resamplingFilter = new CatmullRomFilter();
          break;
        case MgResamplingFilter.Quadratic:
          resamplingFilter = new QuadraticFilter();
          break;
        case MgResamplingFilter.QuadraticBSpline:
          resamplingFilter = new QuadraticBSplineFilter();
          break;
        case MgResamplingFilter.CubicConvolution:
          resamplingFilter = new CubicConvolutionFilter();
          break;
        case MgResamplingFilter.Lanczos8:
          resamplingFilter = new Lanczos8Filter();
          break;
      }

      return resamplingFilter;
    }

    /// <summary>
    /// Gets a weight value for a specified pixel.
    /// </summary>
    /// <param name="x">The pixel to get a weight for.</param>
    /// <returns>The weight value for the specified pixel.</returns>
    public abstract float GetValue(float x);
  }

  internal class HermiteFilter : ResamplingFilterOperation
  {
    public HermiteFilter()
    {
      defaultFilterRadius = 1;
    }

    public override float GetValue(float x)
    {
      if (x < 0) { x = -x; }
      if (x < 1) { return ((2 * x - 3) * x * x + 1); }
      return 0;
    }
  }

  internal class BoxFilter : ResamplingFilterOperation
  {
    public BoxFilter()
    {
      defaultFilterRadius = 0.5f;
    }

    public override float GetValue(float x)
    {
      if (x < 0) { x = -x; }
      if (x <= 0.5f) { return 1; }
      return 0;
    }
  }

  internal class TriangleFilter : ResamplingFilterOperation
  {
    public TriangleFilter()
    {
      defaultFilterRadius = 1;
    }

    public override float GetValue(float x)
    {
      if (x < 0) { x = -x; }
      if (x < 1) { return (1 - x); }
      return 0;
    }
  }

  internal class BellFilter : ResamplingFilterOperation
  {
    public BellFilter()
    {
      defaultFilterRadius = 1.5f;
    }

    public override float GetValue(float x)
    {
      if (x < 0) { x = -x; }
      if (x < 0.5f) { return (0.75f - x * x); }
      if (x < 1.5f) { return (0.5f * (float)Math.Pow(x - 1.5f, 2)); }
      return 0;
    }
  }

  internal class CubicBSplineFilter : ResamplingFilterOperation
  {
    private float temp;

    public CubicBSplineFilter()
    {
      defaultFilterRadius = 2;
    }

    public override float GetValue(float x)
    {
      if (x < 0) { x = -x; }
      if (x < 1)
      {
        temp = x * x;
        return (0.5f * temp * x - temp + 2f / 3f);
      }
      if (x < 2)
      {
        x = 2f - x;
        return (float)(Math.Pow(x, 3) / 6f);
      }
      return 0;
    }
  }

  internal class Lanczos3Filter : ResamplingFilterOperation
  {
    public Lanczos3Filter()
    {
      defaultFilterRadius = 3;
    }

    private float SinC(float x)
    {
      if (x != 0)
      {
        x *= MgMath.Pi;
        return (float)(Math.Sin(x) / x);
      }
      return 1;
    }

    public override float GetValue(float x)
    {
      if (x < 0) { x = -x; }
      if (x < 3) { return (SinC(x) * SinC(x / 3f)); }
      return 0;
    }
  }

  internal class MitchellFilter : ResamplingFilterOperation
  {
    private const float C = 1f / 3f;
    private float temp;

    public MitchellFilter()
    {
      defaultFilterRadius = 2;
    }

    public override float GetValue(float x)
    {
      if (x < 0) { x = -x; }
      temp = x * x;
      if (x < 1)
      {
        x = (((12 - 9 * C - 6 * C) * (x * temp)) + ((-18 + 12 * C + 6 * C) * temp) + (6 - 2 * C));
        return (x / 6);
      }
      if (x < 2)
      {
        x = (((-C - 6 * C) * (x * temp)) + ((6 * C + 30 * C) * temp) + ((-12 * C - 48 * C) * x) + (8 * C + 24 * C));
        return (x / 6);
      }
      return 0;
    }
  }

  internal class CosineFilter : ResamplingFilterOperation
  {
    public CosineFilter()
    {
      defaultFilterRadius = 1;
    }

    public override float GetValue(float x)
    {
      if ((x >= -1) && (x <= 1))
      {
        return (float)((Math.Cos(x * Math.PI) + 1) / 2f);
      }
      return 0;
    }
  }

  internal class CatmullRomFilter : ResamplingFilterOperation
  {
    private const float C = 1 / 2;
    private float temp;

    public CatmullRomFilter()
    {
      defaultFilterRadius = 2;
    }

    public override float GetValue(float x)
    {
      if (x < 0) { x = -x; }
      temp = x * x;
      if (x <= 1) { return (1.5f * temp * x - 2.5f * temp + 1); }
      if (x <= 2) { return (-0.5f * temp * x + 2.5f * temp - 4 * x + 2); }
      return 0;
    }
  }

  internal class QuadraticFilter : ResamplingFilterOperation
  {
    public QuadraticFilter()
    {
      defaultFilterRadius = 1.5f;
    }

    public override float GetValue(float x)
    {
      if (x < 0) { x = -x; }
      if (x <= 0.5f) { return (-2 * x * x + 1); }
      if (x <= 1.5f) { return (x * x - 2.5f * x + 1.5f); }
      return 0;
    }
  }

  internal class QuadraticBSplineFilter : ResamplingFilterOperation
  {
    public QuadraticBSplineFilter()
    {
      defaultFilterRadius = 1.5f;
    }

    public override float GetValue(float x)
    {
      if (x < 0) { x = -x; }
      if (x <= 0.5f) { return (-x * x + 0.75f); }
      if (x <= 1.5f) { return (0.5f * x * x - 1.5f * x + 1.125f); }
      return 0;
    }
  }

  internal class CubicConvolutionFilter : ResamplingFilterOperation
  {
    private const float _4o3 = 4f / 3f;
    private const float _7o12 = -(7f / 12f);
    private const float _1o12 = 1f / 12f;
    private const float _7o3 = 7f / 3f;
    private const float _2o3 = 2f / 3f;
    private const float _59o12 = 59f / 12f;
    private float temp;

    public CubicConvolutionFilter()
    {
      defaultFilterRadius = 3;
    }

    public override float GetValue(float x)
    {
      if (x < 0) { x = -x; }
      temp = x * x;
      if (x <= 1) { return (_4o3 * temp * x - _7o3 * temp + 1); }
      if (x <= 2) { return (_7o12 * temp * x + 3 * temp - _59o12 * x + 2.5f); }
      if (x <= 3) { return (_1o12 * temp * x - _2o3 * temp + 1.75f * x - 1.5f); }
      return 0;
    }
  }

  internal class Lanczos8Filter : ResamplingFilterOperation
  {
    public Lanczos8Filter()
    {
      defaultFilterRadius = 8;
    }

    private float SinC(float x)
    {
      if (x != 0)
      {
        x *= MgMath.Pi;
        return (float)(Math.Sin(x) / x);
      }
      return 1;
    }

    public override float GetValue(float x)
    {
      if (x < 0) { x = -x; }
      if (x < 8) { return (SinC(x) * SinC(x / 8f)); }
      return 0;
    }
  }
}