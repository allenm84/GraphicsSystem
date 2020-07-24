using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MLA.Graphics.Drawing;

namespace MLA.Graphics.WinForms
{
  public partial class Form1 : Form
  {
    private class GraphicCall
    {
      public Delegate Method;
      public object[] Args;
    }

    private List<GraphicCall> calls;
    private MgSurfaceBuffer buffer;
    private MgSurfaceGraphics graphics;
    private Random random;

    public Form1()
    {
      InitializeComponent();
      random = new Random();
      calls = new List<GraphicCall>();
      buffer = new MgSurfaceBuffer(screen.Width, screen.Height);
      buffer.AlphaBlend = false;
      graphics = new MgSurfaceGraphics(buffer);
    }

    private float sg() { return (float)random.NextDouble(); }

    private void Redraw()
    {
      buffer.Clear(MgColor.CornflowerBlue);
      foreach (var call in calls)
      {
        call.Method.DynamicInvoke(call.Args);
      }

      byte[] pixels = new byte[buffer.Size * 4];
      int x, y, p = 0;
      MgColor c;
      for (y = 0; y < buffer.Height; ++y)
      {
        for (x = 0; x < buffer.Width; ++x)
        {
          c = buffer.GetPixel(x, y);
          pixels[p++] = c.B;
          pixels[p++] = c.G;
          pixels[p++] = c.R;
          pixels[p++] = c.A;
        }
      }

      Bitmap bitmap = new Bitmap(buffer.Width, buffer.Height, PixelFormat.Format32bppArgb);
      var rect = screen.Bounds;
      var data = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
      Marshal.Copy(pixels, 0, data.Scan0, pixels.Length);
      bitmap.UnlockBits(data);

      var current = screen.Image;
      screen.Image = bitmap;
      if (current != null)
      {
        current.Dispose();
        current = null;
      }
    }

    private void ClearScreen()
    {
      calls.Clear();
      Redraw();
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      ClearScreen();
    }

    private void screen_SizeChanged(object sender, EventArgs e)
    {
      buffer.Resize(screen.Width, screen.Height);
      Redraw();
    }

    private void btnEllipse_Click(object sender, EventArgs e)
    {
      Action<float, float, float, float, MgColor> method;
      float sw = screen.Width;
      float sh = screen.Height;

      float x, y, width, height;
      MgColor color;

      var bounds = new MgRectangleF(0, 0, sw, sh);
      var center = bounds.Center;

      width = MgMath.Lerp(sw * 0.2f, sw * 0.4f, sg());
      height = MgMath.Lerp(sh * 0.2f, sh * 0.4f, sg());
      x = (center.X - (width / 2)) + sg() * width;
      y = (center.Y - (height / 2)) + sg() * height;
      color = new MgColor(sg(), sg(), sg());

      bool filled = random.NextDouble() > 0.5;
      if (filled)
      {
        method = graphics.FillEllipse;
      }
      else
      {
        method = graphics.DrawEllipse;
      }

      calls.Add(new GraphicCall
      {
        Args = new object[] { x, y, width, height, color },
        Method = method,
      });

      Redraw();
    }

    private void btnRectangle_Click(object sender, EventArgs e)
    {

    }

    private void btnPolygon_Click(object sender, EventArgs e)
    {

    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      ClearScreen();
    }
  }
}
