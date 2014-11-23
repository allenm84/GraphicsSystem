
namespace GraphicsSystem
{
  /// <summary>
  /// Specifies both horizontal and vertical alignment of contents.
  /// </summary>
  public enum GsAlignment
  {
    /// <summary>
    /// No alignment is defined
    /// </summary>
    None = -1,
    /// <summary>
    /// Content is vertically aligned at the top, and horizontally aligned on the left.
    /// </summary>
    TopLeft = (GsVerticalAlignment.Top << 8) | GsHorizontalAlignment.Left,
    /// <summary>
    /// Content is vertically aligned at the top, and horizontally aligned at the center.
    /// </summary>
    TopCenter = (GsVerticalAlignment.Top << 8) | GsHorizontalAlignment.Center,
    /// <summary>
    /// Content is vertically aligned at the top, and horizontally aligned on the right.
    /// </summary>
    TopRight = (GsVerticalAlignment.Top << 8) | GsHorizontalAlignment.Right,
    /// <summary>
    /// Content is vertically aligned in the middle, and horizontally aligned on the left.
    /// </summary>
    MiddleLeft = (GsVerticalAlignment.Center << 8) | GsHorizontalAlignment.Left,
    /// <summary>
    /// Content is vertically aligned in the middle, and horizontally aligned at the center.
    /// </summary>
    MiddleCenter = (GsVerticalAlignment.Center << 8) | GsHorizontalAlignment.Center,
    /// <summary>
    /// Content is vertically aligned in the middle, and horizontally aligned on the right.
    /// </summary>
    MiddleRight = (GsVerticalAlignment.Center << 8) | GsHorizontalAlignment.Right,
    /// <summary>
    /// Content is vertically aligned at the bottom, and horizontally aligned on the left.
    /// </summary>
    BottomLeft = (GsVerticalAlignment.Bottom << 8) | GsHorizontalAlignment.Left,
    /// <summary>
    /// Content is vertically aligned at the bottom, and horizontally aligned at the center.
    /// </summary>
    BottomCenter = (GsVerticalAlignment.Bottom << 8) | GsHorizontalAlignment.Center,
    /// <summary>
    /// Content is vertically aligned at the bottom, and horizontally aligned on the right.
    /// </summary>
    BottomRight = (GsVerticalAlignment.Bottom << 8) | GsHorizontalAlignment.Right,
  }
}
