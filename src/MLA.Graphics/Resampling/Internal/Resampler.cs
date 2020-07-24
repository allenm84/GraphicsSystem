using System;
using MLA.Graphics.Extensions;

namespace MLA.Graphics.Imaging
{
  /// <summary>
  /// A class for image resampling with custom filters.
  /// </summary>
  internal class Resampler
  {
    /// <summary>
    /// Creates a new resampling service
    /// </summary>
    public Resampler()
    {
      Filter = MgResamplingFilter.Box;
    }

    /// <summary>
    /// Gets or sets the resampling filter.
    /// </summary>
    public MgResamplingFilter Filter { get; set; }

    /// <summary>
    /// Resamples input array to a new array using current resampling filter.
    /// </summary>
    /// <param name="input">Input array.</param>
    /// <param name="nWidth">Width of the output array.</param>
    /// <param name="nHeight">Height of the output array.</param>
    /// <returns>Output array.</returns>
    public MgColor[,] Resample(MgColor[,] input, int nWidth, int nHeight)
    {
      if (input == null || input.Length == 0 || nWidth <= 1 || nHeight <= 1)
      {
        if (nWidth == 1 && nHeight != 1)
        {
          // every color in the column needs to be a blend of all of the colors
          var retval = new MgColor[nWidth, nHeight];
          for (var y = 0; y < input.GetLength(1); ++y)
          {
            var blended = input[0, y];
            for (var x = 1; x < input.GetLength(0); ++x)
            {
              blended = MgMath.Lerp(blended, input[x, y], .5f);
            }
            retval[0, y] = blended;
          }
          return retval;
        }
        if (nHeight == 1 && nWidth != 1)
        {
          // every color in the row needs to be a blend of all of the colors
          var retval = new MgColor[nWidth, nHeight];
          for (var x = 0; x < input.GetLength(0); ++x)
          {
            var blended = input[x, 0];
            for (var y = 1; y < input.GetLength(1); ++y)
            {
              blended = MgMath.Lerp(blended, input[x, y], .5f);
            }
            retval[x, 0] = blended;
          }
          return retval;
        }
        if (nWidth == 1 && nHeight == 1)
        {
          var retval = new MgColor[nWidth, nHeight];
          var blended = input[0, 0];
          for (var x = 1; x < input.GetLength(0); ++x)
          {
            for (var y = 0; y < input.GetLength(1); ++y)
            {
              if (x == 1 && y == 0) { continue; }
              blended = MgMath.Lerp(blended, input[x, y], .5f);
            }
          }
          retval[0, 0] = blended;
          return retval;
        }
        return null;
      }

      var filter = ResamplingFilterOperation.Create(this.Filter);

      var width = input.GetLength(0);
      var height = input.GetLength(1);

      // create bitmaps
      var work = new MgColor[nWidth, height];
      var output = new MgColor[nWidth, nHeight];

      var xScale = ((float)nWidth / width);
      var yScale = ((float)nHeight / height);

      var contrib = new ContributorEntry[nWidth];
      float wdth = 0, center = 0, weight = 0;
      int left = 0, right = 0, i = 0, j = 0, k = 0;
      var intensity = new float[4];

      // horizontal downsampling
      if (xScale < 1.0f)
      {
        // scales from bigger to smaller width
        wdth = (filter.defaultFilterRadius / xScale);
        for (i = 0; i < nWidth; i++)
        {
          contrib[i].n = 0;
          contrib[i].p = new Contributor[(int)Math.Floor(2 * wdth + 1)];
          contrib[i].wsum = 0;
          center = ((i + 0.5f) / xScale);
          left = (int)(center - wdth);
          right = (int)(center + wdth);

          for (j = left; j <= right; j++)
          {
            weight = filter.GetValue((center - j - 0.5f) * xScale);
            if ((weight == 0) || (j < 0) || (j >= width))
            {
              continue;
            }

            contrib[i].p[contrib[i].n].pixel = j;
            contrib[i].p[contrib[i].n].weight = weight;
            contrib[i].wsum += weight;
            contrib[i].n++;
          }
        }
      }
      else
      {
        // horizontal upsampling
        // scales from smaller to bigger width
        for (i = 0; i < nWidth; i++)
        {
          contrib[i].n = 0;
          contrib[i].p = new Contributor[(int)Math.Floor(2 * filter.defaultFilterRadius + 1)];
          contrib[i].wsum = 0;
          center = ((i + 0.5f) / xScale);
          left = (int)Math.Floor(center - filter.defaultFilterRadius);
          right = (int)Math.Ceiling(center + filter.defaultFilterRadius);

          for (j = left; j <= right; j++)
          {
            weight = filter.GetValue(center - j - 0.5f);
            if ((weight == 0) || (j < 0) || (j >= width))
            {
              continue;
            }

            contrib[i].p[contrib[i].n].pixel = j;
            contrib[i].p[contrib[i].n].weight = weight;
            contrib[i].wsum += weight;
            contrib[i].n++;
          }
        }
      }

      // filter horizontally from input to work
      for (k = 0; k < height; k++)
      {
        for (i = 0; i < nWidth; i++)
        {
          intensity[0] = 0f;
          intensity[1] = 0f;
          intensity[2] = 0f;
          intensity[3] = 0f;

          for (j = 0; j < contrib[i].n; j++)
          {
            weight = contrib[i].p[j].weight;
            if (weight == 0)
            {
              continue;
            }

            var color = input[contrib[i].p[j].pixel, k];
            intensity[0] += color.A * weight;
            intensity[1] += color.R * weight;
            intensity[2] += color.G * weight;
            intensity[3] += color.B * weight;
          }

          work[i, k].A = (byte)Clamp(intensity[0] / contrib[i].wsum, 0, 255);
          work[i, k].R = (byte)Clamp(intensity[1] / contrib[i].wsum, 0, 255);
          work[i, k].G = (byte)Clamp(intensity[2] / contrib[i].wsum, 0, 255);
          work[i, k].B = (byte)Clamp(intensity[3] / contrib[i].wsum, 0, 255);
        }
      }

      // pre-calculate filter contributions for a column
      contrib = new ContributorEntry[nHeight];

      // vertical downsampling
      if (yScale < 1.0f)
      {
        // scales from bigger to smaller height
        wdth = (filter.defaultFilterRadius / yScale);

        for (i = 0; i < nHeight; i++)
        {
          contrib[i].n = 0;
          contrib[i].p = new Contributor[(int)Math.Floor(2 * wdth + 1)];
          contrib[i].wsum = 0;
          center = ((i + 0.5f) / yScale);
          left = (int)(center - wdth);
          right = (int)(center + wdth);

          for (j = left; j <= right; j++)
          {
            weight = filter.GetValue((center - j - 0.5f) * yScale);
            if ((weight == 0) || (j < 0) || (j >= height))
            {
              continue;
            }

            contrib[i].p[contrib[i].n].pixel = j;
            contrib[i].p[contrib[i].n].weight = weight;
            contrib[i].wsum += weight;
            contrib[i].n++;
          }
        }
      }
      else
      {
        // vertical upsampling
        // scales from smaller to bigger height
        for (i = 0; i < nHeight; i++)
        {
          contrib[i].n = 0;
          contrib[i].p = new Contributor[(int)Math.Floor(2 * filter.defaultFilterRadius + 1)];
          contrib[i].wsum = 0;
          center = ((i + 0.5f) / yScale);
          left = (int)(center - filter.defaultFilterRadius);
          right = (int)(center + filter.defaultFilterRadius);

          for (j = left; j <= right; j++)
          {
            weight = filter.GetValue(center - j - 0.5f);
            if ((weight == 0) || (j < 0) || (j >= height))
            {
              continue;
            }

            contrib[i].p[contrib[i].n].pixel = j;
            contrib[i].p[contrib[i].n].weight = weight;
            contrib[i].wsum += weight;
            contrib[i].n++;
          }
        }
      }

      // filter vertically from work to output
      for (k = 0; k < nWidth; k++)
      {
        for (i = 0; i < nHeight; i++)
        {
          intensity[0] = 0f;
          intensity[1] = 0f;
          intensity[2] = 0f;
          intensity[3] = 0f;

          for (j = 0; j < contrib[i].n; j++)
          {
            weight = contrib[i].p[j].weight;
            if (weight == 0)
            {
              continue;
            }

            var color = work[k, contrib[i].p[j].pixel];
            intensity[0] += color.A * weight;
            intensity[1] += color.R * weight;
            intensity[2] += color.G * weight;
            intensity[3] += color.B * weight;
          }

          output[k, i].A = (byte)Clamp(intensity[0] / contrib[i].wsum, 0, 255);
          output[k, i].R = (byte)Clamp(intensity[1] / contrib[i].wsum, 0, 255);
          output[k, i].G = (byte)Clamp(intensity[2] / contrib[i].wsum, 0, 255);
          output[k, i].B = (byte)Clamp(intensity[3] / contrib[i].wsum, 0, 255);
        }
      }

      work = null;
      return output;
    }

    private double Clamp(double val, double min, double max)
    {
      return Math.Min(Math.Max(val, min), max);
    }
  }
}