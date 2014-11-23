using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsSystem
{
  public static partial class GsExtensions
  {
    public static GsVector Rotate(this GsVector pointToRotate, GsVector centerPoint, double angleInDegrees)
    {
      double angleInRadians = angleInDegrees * (Math.PI / 180);
      double cosTheta = Math.Cos(angleInRadians);
      double sinTheta = Math.Sin(angleInRadians);
      return new GsVector
      {
        X =
            (float)
            (cosTheta * (pointToRotate.X - centerPoint.X) -
            sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
        Y =
            (float)
            (sinTheta * (pointToRotate.X - centerPoint.X) +
            cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
      };
    }
  }
}
