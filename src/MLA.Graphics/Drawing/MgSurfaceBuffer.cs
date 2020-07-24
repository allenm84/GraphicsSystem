using System;
using System.Linq;
using MLA.Graphics.Extensions;

namespace MLA.Graphics.Drawing
{
  /// <summary>
  /// Represents a collection of pixels that can be easily rendered to (by a Surface object), or
  /// transformed to a Texture2D.
  /// </summary>
  public sealed class MgSurfaceBuffer
  {
    private MgColor[] pixels;
    private int[] yWidth;

    /// <summary>
    /// Creates a new buffer; or collection of pixels.
    /// </summary>
    /// <param name="device">The device to associate with the buffer.</param>
    /// <param name="size">The width and height of the buffer.</param>
    public MgSurfaceBuffer(MgSize size)
      : this(size.Width, size.Height) {}

    /// <summary>
    /// Creates a new buffer; or collection of pixels.
    /// </summary>
    /// <param name="device">The device to associate with the buffer.</param>
    /// <param name="width">The width of the buffer.</param>
    /// <param name="height">The height of the buffer.</param>
    public MgSurfaceBuffer(int width, int height)
    {
      AlphaBlend = true;
      Resize(width, height);
    }

    /// <summary>
    /// Resizes the pixel buffer. If the desired width/height hasn't changed, then this method does nothing.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <param name="clear">[Optional] Clears the buffer to be empty. The default is true.</param>
    public void Resize(int width, int height, bool clear = true)
    {
      if (width != Width || height != Height)
      {
        Width = width;
        Height = height;
        Size = Width * Height;

        yWidth = new int[Height];
        for (var y = 0; y < Height; ++y)
        {
          yWidth[y] = y * Width;
        }

        if (clear)
        {
          pixels = new MgColor[Size];
        }
        else
        {
          Array.Resize(ref pixels, Size);
        }
      }
    }

    /// <summary>
    /// Gets the width of the buffer.
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// Gets the height of the buffer.
    /// </summary>
    public int Height { get; private set; }

    /// <summary>
    /// Gets the size of the buffer; the number of pixels.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// ets or sets a value to determine if alpha blending should be used when drawing.
    /// The default is true, meaning alpha blending will be performed.
    /// </summary>
    public bool AlphaBlend { get; set; }

    /// <summary>
    /// Sets a pixel at (x,y) to color. If x or y or invalid, this will do nothing.
    /// </summary>
    /// <param name="color">The color to set the pixel.</param>
    /// <param name="x">The zero-based x index of the pixel.</param>
    /// <param name="y">The zero-based y index of the pixel.</param>
    public void SetPixel(MgColor color, int x, int y)
    {
      if ((-1 < x && x < Width) && (-1 < y && y < Height))
      {
        SetPixel(color, x + yWidth[y]);
      }
    }

    /// <summary>
    /// Sets a pixel at index to color. If the index invalid, this will do nothing.
    /// </summary>
    /// <param name="color">The color to set the pixel.</param>
    /// <param name="index">The zero-based index of the pixel.</param>
    public void SetPixel(MgColor color, int index)
    {
      if (-1 < index && index < pixels.Length)
      {
        if (!AlphaBlend)
        {
          pixels[index] = color;
        }
        else
        {
          var current = pixels[index];
          pixels[index] = MgMath.AlphaBlend(current, color);
        }
      }
    }

    /// <summary>
    /// Gets the color of a pixel at index (x,y).
    /// </summary>
    /// <param name="x">The zero-based x index of the pixel.</param>
    /// <param name="y">The zero-based y index of the pixel.</param>
    /// <returns>The color of the pixel, or an empty color if the index is invalid.</returns>
    public MgColor GetPixel(int x, int y)
    {
      var retval = new MgColor();
      if ((-1 < x && x < Width) && (-1 < y && y < Height))
      {
        retval = GetPixel(x + yWidth[y]);
      }
      return retval;
    }

    /// <summary>
    /// Gets the color of a pixel at index.
    /// </summary>
    /// <param name="index">The zero-based index of the pixel.</param>
    /// <returns>The color of the pixel, or an empty color if the index is invalid.</returns>
    public MgColor GetPixel(int index)
    {
      var retval = new MgColor();
      if (-1 < index && index < pixels.Length)
      {
        retval = pixels[index];
      }
      return retval;
    }

    /// <summary>
    /// Clears every pixel of the buffer to be an empty color.
    /// </summary>
    public void Clear()
    {
      Array.Clear(pixels, 0, pixels.Length);
    }

    /// <summary>
    /// Clears every pixel of the buffer to be a specific color.
    /// </summary>
    /// <param name="color">The color to make every pixel in the buffer.</param>
    public void Clear(MgColor color)
    {
      for (int i = 0; i < pixels.Length; ++i)
      {
        pixels[i] = color;
      }
    }
  }
}