using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MLA.Graphics.Drawing.Internal;
using MLA.Graphics.Extensions;
using MLA.Graphics.Imaging;

namespace MLA.Graphics.Drawing
{
  /// <summary>
  /// Provides methods for drawing on a surface buffer. All drawing is done "immediately", however, the resulting
  /// buffer isn't rendered until told to do so. This should only be used for drawing small items as drawing is all
  /// done in software.
  /// </summary>
  public sealed class MgSurfaceGraphics : MgGraphicsBase, IDisposable
  {
    /// <summary>
    /// The resampler used for up/down sampling images.
    /// </summary>
    private Resampler mResampler;

    /// <summary>
    /// Creates a new surface and an internal buffer from an existing graphics device.
    /// </summary>
    /// <param name="size">The size of the buffer.</param>
    public MgSurfaceGraphics(MgSize size) :
      this(new MgSurfaceBuffer(size)) {}

    /// <summary>
    /// Creates a new surface and an internal buffer from an existing graphics device.
    /// </summary>
    /// <param name="width">The width of the buffer.</param>
    /// <param name="height">The height of the buffer.</param>
    public MgSurfaceGraphics(int width, int height) :
      this(new MgSurfaceBuffer(width, height)) {}

    /// <summary>
    /// Creates a new surface and stores the internal buffer. Any drawing is done directly
    /// to the buffer passed in.
    /// </summary>
    /// <param name="buffer">The buffer in which to do drawing.</param>
    public MgSurfaceGraphics(MgSurfaceBuffer buffer)
    {
      Buffer = buffer;
      mResampler = new Resampler();
      mResampler.Filter = MgResamplingFilter.Triangle;
    }

    /// <summary>
    /// Gets the buffer that all the drawing is done upon.
    /// </summary>
    public MgSurfaceBuffer Buffer { get; private set; }

    /// <summary>
    /// Gets the width of the internal buffer.
    /// </summary>
    public int Width
    {
      get { return Buffer.Width; }
    }

    /// <summary>
    /// Gets the height of the internal buffer.
    /// </summary>
    public int Height
    {
      get { return Buffer.Height; }
    }

    /// <summary>
    /// Gets the size of the internal buffer.
    /// </summary>
    public int Size
    {
      get { return Buffer.Size; }
    }

    /// <summary>
    /// Gets or sets the resampler used when upsampling or downsampling images.
    /// </summary>
    public MgResamplingFilter ResamplingFilter
    {
      get { return mResampler.Filter; }
      set { mResampler.Filter = value; }
    }

    /// <summary>
    /// Gets or sets a value to determine if alpha blending should be used when drawing.
    /// The default is true, meaning alpha blending will be performed.
    /// </summary>
    public bool AlphaBlend
    {
      get { return Buffer.AlphaBlend; }
      set { Buffer.AlphaBlend = value; }
    }

    #region Dispose Pattern

    private bool isDisposed;

    /// <summary>
    /// Immediately releases the unmanaged resources used by this object.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Immediately releases the unmanaged resources used by this object.
    /// </summary>
    private void Dispose(bool disposing)
    {
      if (disposing && !isDisposed)
      {
        if (Buffer != null)
        {
          Buffer.Clear();
          Buffer = null;
        }

        isDisposed = true;
      }
    }

    #endregion

    /// <summary>
    /// Clears the internal buffer of the surface.
    /// </summary>
    public void Clear()
    {
      Buffer.Clear();
    }

    /// <summary>
    /// Clears the internal buffer of the surface to the specified color.
    /// </summary>
    /// <param name="color">The color to clear the buffer to.</param>
    public void Clear(MgColor color)
    {
      Buffer.Clear(color);
    }

    /// <summary>
    /// Sets a pixel on the internal buffer.
    /// </summary>
    /// <param name="color">The color to make the pixel.</param>
    /// <param name="loc">The location of the pixel to set.</param>
    public void SetPixel(MgColor color, MgVector2 loc)
    {
      var pt = loc.Round();
      Buffer.SetPixel(color, pt.X, pt.Y);
    }

    /// <summary>
    /// Sets a pixel on the internal buffer.
    /// </summary>
    /// <param name="color">The color to make the pixel.</param>
    /// <param name="loc">The location of the pixel to set.</param>
    public void SetPixel(MgColor color, MgPoint loc)
    {
      Buffer.SetPixel(color, loc.X, loc.Y);
    }

    /// <summary>
    /// Sets a pixel on the internal buffer.
    /// </summary>
    /// <param name="color">The color to make the pixel.</param>
    /// <param name="x">The x-coordinate of the pixel to set.</param>
    /// <param name="y">The y-coordinate of the pixel to set.</param>
    public void SetPixel(MgColor color, int x, int y)
    {
      Buffer.SetPixel(color, x, y);
    }

    /// <summary>
    /// Draws a line.
    /// </summary>
    /// <param name="x0">The starting x-coordinate.</param>
    /// <param name="y0">The starting y-coordinate.</param>
    /// <param name="x1">The ending x-coordinate.</param>
    /// <param name="y1">The ending y-coordinate.</param>
    /// <param name="color">The color of the line to draw.</param>
    public override void DrawLine(float x0, float y0, float x1, float y1, MgColor color)
    {
      __drawLine(Buffer, x0.Round(), y0.Round(), x1.Round(), y1.Round(), color);
    }

    /// <summary>
    /// Fills a rectangle.
    /// </summary>
    /// <param name="x">The upper-left x-coordinate.</param>
    /// <param name="y">The upper-left y-coordinate.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <param name="color">The color of the rectangle.</param>
    public override void FillRectangle(float x, float y, float width, float height, MgColor color)
    {
      var left = x.Round();
      var top = y.Round();
      var right = (x + width).Round();
      var bottom = (y + height).Round();

      int iy, ix;
      for (iy = top; iy <= bottom; ++iy)
      {
        for (ix = left; ix <= right; ++ix)
        {
          Buffer.SetPixel(color, ix, iy);
        }
      }
    }

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
    public override void FillRoundRectangle(float x, float y, float width, float height, float radius, MgRectangleCorners part, MgColor color)
    {
      if (radius <= 0 || part == MgRectangleCorners.None)
      {
        FillRectangle(x, y, width, height, color);
      }
      else
      {
        var rect = new MgRectangleF(x, y, width, height);
        FillPolygon(CreateRoundRect(rect, radius, part), color);
      }
    }

    /// <summary>
    /// Fills a polygon.
    /// </summary>
    /// <param name="polygon">The polygon to fill.</param>
    /// <param name="color">The color to fill the polygon with.</param>
    public override void FillPolygon(IEnumerable<MgVector2> polygon, MgColor color)
    {
      __fillPolygon(Buffer, polygon, new ColorProxy {Color = color});
    }

    /// <summary>
    /// Draws an image.
    /// </summary>
    /// <param name="image">The image to draw.</param>
    /// <param name="destination">The location and size to draw the image.</param>
    /// <param name="source">The portion of the image to draw.</param>
    public void DrawImage(MgColor[] image, MgRectangleF destination, MgRectangle source)
    {
      __drawImage(image, source, destination);
    }

    /// <summary>
    /// Copies data from a 1D array to a 2D array.
    /// </summary>
    /// <typeparam name="T">The type inside the array.</typeparam>
    /// <param name="src">The 1D source array to pull data from.</param>
    /// <param name="sizeX">The number of columns in the array.</param>
    /// <param name="sizeY">The number of rows in the array.</param>
    /// <returns>A 2D array containing all of the data.</returns>
    private static T[,] Transfer<T>(T[] src, int sizeX, int sizeY)
    {
      var retval = new T[sizeX, sizeY];
      for (var x = 0; x < sizeX; ++x)
      {
        for (var y = 0; y < sizeY; ++y)
        {
          retval[x, y] = src[x + y * sizeX];
        }
      }
      return retval;
    }

    /// <summary>
    /// Copies data from a 2D array to a 1D array.
    /// </summary>
    /// <typeparam name="T">The type inside the array.</typeparam>
    /// <param name="src">The 2D source array to pull data from.</param>
    /// <param name="sizeX">The number of columns in the array.</param>
    /// <param name="sizeY">The number of rows in the array.</param>
    /// <returns>A 1D array containing all of the data.</returns>
    private static T[] Transfer<T>(T[,] src, int sizeX, int sizeY)
    {
      var retval = new T[sizeX * sizeY];
      for (var x = 0; x < sizeX; ++x)
      {
        for (var y = 0; y < sizeY; ++y)
        {
          retval[x + y * sizeX] = src[x, y];
        }
      }
      return retval;
    }

    #region Helper Functions

    private static void __drawLine(MgSurfaceBuffer surface, int x0, int y0, int x1, int y1, MgColor color)
    {
      #region Implementation

      var dy = y1 - y0;
      var dx = x1 - x0;
      int stepx, stepy;

      if (dy < 0)
      {
        dy = -dy;
        stepy = -1;
      }
      else
      {
        stepy = 1;
      }
      if (dx < 0)
      {
        dx = -dx;
        stepx = -1;
      }
      else
      {
        stepx = 1;
      }

      surface.SetPixel(color, x0, y0);
      surface.SetPixel(color, x1, y1);
      if (dx > dy)
      {
        var length = (dx - 1) >> 2;
        var extras = (dx - 1) & 3;
        var incr2 = (dy << 2) - (dx << 1);
        if (incr2 < 0)
        {
          var c = dy << 1;
          var incr1 = c << 1;
          var d = incr1 - dx;
          for (var i = 0; i < length; i++)
          {
            x0 += stepx;
            x1 -= stepx;
            if (d < 0)
            {
              // Pattern:
              surface.SetPixel(color, x0, y0); //
              surface.SetPixel(color, x0 += stepx, y0); //  x o o
              surface.SetPixel(color, x1, y1); //
              surface.SetPixel(color, x1 -= stepx, y1);
              d += incr1;
            }
            else
            {
              if (d < c)
              {
                // Pattern:
                surface.SetPixel(color, x0, y0); //      o
                surface.SetPixel(color, x0 += stepx, y0 += stepy); //  x o
                surface.SetPixel(color, x1, y1); //
                surface.SetPixel(color, x1 -= stepx, y1 -= stepy);
              }
              else
              {
                surface.SetPixel(color, x0, y0 += stepy); // Pattern:
                surface.SetPixel(color, x0 += stepx, y0); //    o o 
                surface.SetPixel(color, x1, y1 -= stepy); //  x
                surface.SetPixel(color, x1 -= stepx, y1); //
              }
              d += incr2;
            }
          }
          if (extras > 0)
          {
            if (d < 0)
            {
              surface.SetPixel(color, x0 += stepx, y0);
              if (extras > 1) { surface.SetPixel(color, x0 += stepx, y0); }
              if (extras > 2) { surface.SetPixel(color, x1 -= stepx, y1); }
            }
            else if (d < c)
            {
              surface.SetPixel(color, x0 += stepx, y0);
              if (extras > 1) { surface.SetPixel(color, x0 += stepx, y0 += stepy); }
              if (extras > 2) { surface.SetPixel(color, x1 -= stepx, y1); }
            }
            else
            {
              surface.SetPixel(color, x0 += stepx, y0 += stepy);
              if (extras > 1) { surface.SetPixel(color, x0 += stepx, y0); }
              if (extras > 2) { surface.SetPixel(color, x1 -= stepx, y1 -= stepy); }
            }
          }
        }
        else
        {
          var c = (dy - dx) << 1;
          var incr1 = c << 1;
          var d = incr1 + dx;
          for (var i = 0; i < length; i++)
          {
            x0 += stepx;
            x1 -= stepx;
            if (d > 0)
            {
              surface.SetPixel(color, x0, y0 += stepy); // Pattern:
              surface.SetPixel(color, x0 += stepx, y0 += stepy); //      o
              surface.SetPixel(color, x1, y1 -= stepy); //    o
              surface.SetPixel(color, x1 -= stepx, y1 -= stepy); //  x
              d += incr1;
            }
            else
            {
              if (d < c)
              {
                surface.SetPixel(color, x0, y0); // Pattern:
                surface.SetPixel(color, x0 += stepx, y0 += stepy); //      o
                surface.SetPixel(color, x1, y1); //  x o
                surface.SetPixel(color, x1 -= stepx, y1 -= stepy); //
              }
              else
              {
                surface.SetPixel(color, x0, y0 += stepy); // Pattern:
                surface.SetPixel(color, x0 += stepx, y0); //    o o
                surface.SetPixel(color, x1, y1 -= stepy); //  x
                surface.SetPixel(color, x1 -= stepx, y1); //
              }
              d += incr2;
            }
          }
          if (extras > 0)
          {
            if (d > 0)
            {
              surface.SetPixel(color, x0 += stepx, y0 += stepy);
              if (extras > 1) { surface.SetPixel(color, x0 += stepx, y0 += stepy); }
              if (extras > 2) { surface.SetPixel(color, x1 -= stepx, y1 -= stepy); }
            }
            else if (d < c)
            {
              surface.SetPixel(color, x0 += stepx, y0);
              if (extras > 1) { surface.SetPixel(color, x0 += stepx, y0 += stepy); }
              if (extras > 2) { surface.SetPixel(color, x1 -= stepx, y1); }
            }
            else
            {
              surface.SetPixel(color, x0 += stepx, y0 += stepy);
              if (extras > 1) { surface.SetPixel(color, x0 += stepx, y0); }
              if (extras > 2)
              {
                if (d > c)
                {
                  surface.SetPixel(color, x1 -= stepx, y1 -= stepy);
                }
                else
                {
                  surface.SetPixel(color, x1 -= stepx, y1);
                }
              }
            }
          }
        }
      }
      else
      {
        var length = (dy - 1) >> 2;
        var extras = (dy - 1) & 3;
        var incr2 = (dx << 2) - (dy << 1);
        if (incr2 < 0)
        {
          var c = dx << 1;
          var incr1 = c << 1;
          var d = incr1 - dy;
          for (var i = 0; i < length; i++)
          {
            y0 += stepy;
            y1 -= stepy;
            if (d < 0)
            {
              surface.SetPixel(color, x0, y0);
              surface.SetPixel(color, x0, y0 += stepy);
              surface.SetPixel(color, x1, y1);
              surface.SetPixel(color, x1, y1 -= stepy);
              d += incr1;
            }
            else
            {
              if (d < c)
              {
                surface.SetPixel(color, x0, y0);
                surface.SetPixel(color, x0 += stepx, y0 += stepy);
                surface.SetPixel(color, x1, y1);
                surface.SetPixel(color, x1 -= stepx, y1 -= stepy);
              }
              else
              {
                surface.SetPixel(color, x0 += stepx, y0);
                surface.SetPixel(color, x0, y0 += stepy);
                surface.SetPixel(color, x1 -= stepx, y1);
                surface.SetPixel(color, x1, y1 -= stepy);
              }
              d += incr2;
            }
          }
          if (extras > 0)
          {
            if (d < 0)
            {
              surface.SetPixel(color, x0, y0 += stepy);
              if (extras > 1) { surface.SetPixel(color, x0, y0 += stepy); }
              if (extras > 2) { surface.SetPixel(color, x1, y1 -= stepy); }
            }
            else if (d < c)
            {
              surface.SetPixel(color, stepx, y0 += stepy);
              if (extras > 1) { surface.SetPixel(color, x0 += stepx, y0 += stepy); }
              if (extras > 2) { surface.SetPixel(color, x1, y1 -= stepy); }
            }
            else
            {
              surface.SetPixel(color, x0 += stepx, y0 += stepy);
              if (extras > 1) { surface.SetPixel(color, x0, y0 += stepy); }
              if (extras > 2) { surface.SetPixel(color, x1 -= stepx, y1 -= stepy); }
            }
          }
        }
        else
        {
          var c = (dx - dy) << 1;
          var incr1 = c << 1;
          var d = incr1 + dy;
          for (var i = 0; i < length; i++)
          {
            y0 += stepy;
            y1 -= stepy;
            if (d > 0)
            {
              surface.SetPixel(color, x0 += stepx, y0);
              surface.SetPixel(color, x0 += stepx, y0 += stepy);
              surface.SetPixel(color, x1 -= stepy, y1);
              surface.SetPixel(color, x1 -= stepx, y1 -= stepy);
              d += incr1;
            }
            else
            {
              if (d < c)
              {
                surface.SetPixel(color, x0, y0);
                surface.SetPixel(color, x0 += stepx, y0 += stepy);
                surface.SetPixel(color, x1, y1);
                surface.SetPixel(color, x1 -= stepx, y1 -= stepy);
              }
              else
              {
                surface.SetPixel(color, x0 += stepx, y0);
                surface.SetPixel(color, x0, y0 += stepy);
                surface.SetPixel(color, x1 -= stepx, y1);
                surface.SetPixel(color, x1, y1 -= stepy);
              }
              d += incr2;
            }
          }
          if (extras > 0)
          {
            if (d > 0)
            {
              surface.SetPixel(color, x0 += stepx, y0 += stepy);
              if (extras > 1) { surface.SetPixel(color, x0 += stepx, y0 += stepy); }
              if (extras > 2) { surface.SetPixel(color, x1 -= stepx, y1 -= stepy); }
            }
            else if (d < c)
            {
              surface.SetPixel(color, x0, y0 += stepy);
              if (extras > 1) { surface.SetPixel(color, x0 += stepx, y0 += stepy); }
              if (extras > 2) { surface.SetPixel(color, x1, y1 -= stepy); }
            }
            else
            {
              surface.SetPixel(color, x0 += stepx, y0 += stepy);
              if (extras > 1) { surface.SetPixel(color, x0, y0 += stepy); }
              if (extras > 2)
              {
                if (d > c)
                {
                  surface.SetPixel(color, x1 -= stepx, y1 -= stepy);
                }
                else
                {
                  surface.SetPixel(color, x1, y1 -= stepy);
                }
              }
            }
          }
        }
      }

      #endregion
    }

    private static void __fillPolygon(MgSurfaceBuffer surface, IEnumerable<MgVector2> data, ColorProxy proxy)
    {
      #region Implementation

      proxy.CalculateFor(data);
      var polygon = data.Round().Distinct().ToArray();

      int nodes;
      var nodeX = new int[polygon.Length];
      int pixelY;
      int i, j, swap;
      var polyCorners = polygon.Length;

      var polyLeft = polygon.Min(pt => pt.X) - 1;
      var polyRight = polygon.Max(pt => pt.X) + 1;

      var polyTop = polygon.Min(pt => pt.Y) - 1;
      var polyBottom = polygon.Max(pt => pt.Y) + 1;

      var polyX = polygon.Select(pt => (double)pt.X).ToArray();
      var polyY = polygon.Select(pt => (double)pt.Y).ToArray();

      //  Loop through the rows of the image.
      for (pixelY = polyTop; pixelY < polyBottom; pixelY++)
      {
        //  Build a list of nodes.
        nodes = 0;
        j = polyCorners - 1;
        for (i = 0; i < polyCorners; i++)
        {
          if (polyY[i] < pixelY && polyY[j] >= pixelY || polyY[j] < pixelY && polyY[i] >= pixelY)
          {
            nodeX[nodes++] = (int)(polyX[i] + (pixelY - polyY[i]) / (polyY[j] - polyY[i]) * (polyX[j] - polyX[i]));
          }
          j = i;
        }

        //  Sort the nodes, via a simple "Bubble" sort.
        i = 0;
        while (i < nodes - 1)
        {
          if (nodeX[i] > nodeX[i + 1])
          {
            swap = nodeX[i];
            nodeX[i] = nodeX[i + 1];
            nodeX[i + 1] = swap;
            if (i == 1)
            {
              i--;
            }
          }
          else
          {
            i++;
          }
        }

        //  Fill the pixels between node pairs.
        for (i = 0; i < nodes; i += 2)
        {
          if (nodeX[i] >= polyRight)
          {
            break;
          }
          if (nodeX[i + 1] > polyLeft)
          {
            if (nodeX[i] < polyLeft) { nodeX[i] = polyLeft; }
            if (nodeX[i + 1] > polyRight) { nodeX[i + 1] = polyRight; }
            for (j = nodeX[i]; j < nodeX[i + 1]; j++)
            {
              var color = proxy.Color.GetValueOrDefault();
              if (proxy.Gradient != null)
              {
                color = proxy.Gradient.GetColorAt(j, pixelY);
              }

              surface.SetPixel(color, j, pixelY);
            }
          }
        }
      }

      #endregion
    }

    private void __drawImage(MgColor[] data, MgRectangle source, MgRectangleF destination)
    {
      #region Implementation

      // get the source information
      var rect = source.ToRectangle();

      // get the desitination information
      var dest = destination.ToRectangle().ToRectangle();

      // convert the 1D array to a 2D array
      var input = Transfer(data, rect.Width, rect.Height);

      // resample the image data
      var output = mResampler.Resample(input, dest.Width, dest.Height);

      // draw the image
      for (int r = 0, screen_y = dest.Y; r < dest.Height; ++r, ++screen_y)
      {
        for (int c = 0, screen_x = dest.X; c < dest.Width; ++c, ++screen_x)
        {
          var pixel = output[c, r];
          Buffer.SetPixel(pixel, screen_x, screen_y);
        }
      }

      #endregion
    }

    #endregion
  }
}