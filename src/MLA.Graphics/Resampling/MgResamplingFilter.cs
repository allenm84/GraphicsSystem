namespace MLA.Graphics.Imaging
{
  /// <summary>
  /// Contains possible algorithm to use when upsampling or downsampling an image.
  /// </summary>
  public enum MgResamplingFilter
  {
    /// <summary>
    /// Equivalent to Nearest Neighbor on upsampling, averages pixels on downsampling.
    /// </summary>
    Box = 0,

    /// <summary>
    /// Equivalent to Low; the function can be called Tent function for its shape. This filter produces
    /// sharp transition lines and is relatively fast. It produces reasonably good results with a little
    /// smoothing for both reduction and magnification. This is the default filter when resampling.
    /// </summary>
    Triangle,

    /// <summary>
    /// Use of the cubic spline from Hermite interpolation. The Hermite method creates noticeable smoothing
    /// but maintains a good degree of sharpness.
    /// </summary>
    Hermite,

    /// <summary>
    /// Attempt to compromise between reducing block artifacts and blurring image. This filter is relatively
    /// fast and produces smooth image, bordering on soft.
    /// </summary>
    Bell,

    /// <summary>
    /// Known samples are just "magnets" for this curve. This method does not produce sharp transitions and may cause
    /// excessive blurring.
    /// It has the advantage of dampening noise and JPEG artifacts. B-spline is one of the slower filters.
    /// </summary>
    CubicBSpline,

    /// <summary>
    /// Windowed Sinc function (sin(x)/x) - promising quality, but ringing artifacts can appear. This is a slow method but
    /// it usually produces the sharpest images. Under certain conditions, it may introduce some ringing patterns and
    /// emphasize JPEG artifacts. Generally speaking, this gives the best results.
    /// </summary>
    Lanczos3,

    /// <summary>
    /// Resizing with this algorithm produces no sharp transitions and tends to be a good compromise between the "ringing"
    /// effect of
    /// Lanczos and "blurring" of other methods. The Mitchell algorithm produces good results when enlarging pictures. It is
    /// also one
    /// of the slower filters.
    /// </summary>
    Mitchell,

    /// <summary>
    /// An attemp to replace curve of high order polynomial by cosine function (which is even).
    /// </summary>
    Cosine,

    /// <summary>
    /// Use of the catmull rom spline to get smooth transitions from each pixel.
    /// </summary>
    CatmullRom,

    /// <summary>
    /// Performance optimized filter - results are like with B-Splines, but using quadratic function only.
    /// </summary>
    Quadratic,

    /// <summary>
    /// Quadratic Bezier spline modification.  This method does not produce sharp transitions and may cause
    /// excessive blurring. It has the advantage of dampening noise and JPEG artifacts. B-spline is one of
    /// the slower filters.
    /// </summary>
    QuadraticBSpline,

    /// <summary>
    /// Determines the grey level from the weighted average of the 16 closest pixels to the input coordinates,
    /// and assigns that value to the output coordinates. This method is closer to the perfect sin(x)/x resampler
    /// than nearest neighbour or bilinear interpolation.
    /// </summary>
    CubicConvolution,

    /// <summary>
    /// Has a larger window than Lanczos3 and includes largest neighborhood.  This is a slow method but
    /// it usually produces the sharpest images. Under certain conditions, it may introduce some ringing patterns and
    /// emphasize JPEG artifacts. Generally speaking, this gives the best results.
    /// </summary>
    Lanczos8
  }
}