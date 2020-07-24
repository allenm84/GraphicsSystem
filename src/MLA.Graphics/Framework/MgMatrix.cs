using System;
using System.Globalization;

namespace MLA.Graphics
{
  [Serializable]
  public struct MgMatrix : IEquatable<MgMatrix>
  {
    private static MgMatrix _identity = new MgMatrix(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);
    public float M11;
    public float M12;
    public float M13;
    public float M14;
    public float M21;
    public float M22;
    public float M23;
    public float M24;
    public float M31;
    public float M32;
    public float M33;
    public float M34;
    public float M41;
    public float M42;
    public float M43;
    public float M44;

    public MgMatrix(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31,
      float m32, float m33, float m34, float m41, float m42, float m43, float m44)
    {
      this.M11 = m11;
      this.M12 = m12;
      this.M13 = m13;
      this.M14 = m14;
      this.M21 = m21;
      this.M22 = m22;
      this.M23 = m23;
      this.M24 = m24;
      this.M31 = m31;
      this.M32 = m32;
      this.M33 = m33;
      this.M34 = m34;
      this.M41 = m41;
      this.M42 = m42;
      this.M43 = m43;
      this.M44 = m44;
    }

    public static MgMatrix Identity
    {
      get { return _identity; }
    }

    public MgVector3 Up
    {
      get
      {
        MgVector3 result;
        result.X = this.M21;
        result.Y = this.M22;
        result.Z = this.M23;
        return result;
      }
      set
      {
        this.M21 = value.X;
        this.M22 = value.Y;
        this.M23 = value.Z;
      }
    }

    public MgVector3 Down
    {
      get
      {
        MgVector3 result;
        result.X = -this.M21;
        result.Y = -this.M22;
        result.Z = -this.M23;
        return result;
      }
      set
      {
        this.M21 = -value.X;
        this.M22 = -value.Y;
        this.M23 = -value.Z;
      }
    }

    public MgVector3 Right
    {
      get
      {
        MgVector3 result;
        result.X = this.M11;
        result.Y = this.M12;
        result.Z = this.M13;
        return result;
      }
      set
      {
        this.M11 = value.X;
        this.M12 = value.Y;
        this.M13 = value.Z;
      }
    }

    public MgVector3 Left
    {
      get
      {
        MgVector3 result;
        result.X = -this.M11;
        result.Y = -this.M12;
        result.Z = -this.M13;
        return result;
      }
      set
      {
        this.M11 = -value.X;
        this.M12 = -value.Y;
        this.M13 = -value.Z;
      }
    }

    public MgVector3 Forward
    {
      get
      {
        MgVector3 result;
        result.X = -this.M31;
        result.Y = -this.M32;
        result.Z = -this.M33;
        return result;
      }
      set
      {
        this.M31 = -value.X;
        this.M32 = -value.Y;
        this.M33 = -value.Z;
      }
    }

    public MgVector3 Backward
    {
      get
      {
        MgVector3 result;
        result.X = this.M31;
        result.Y = this.M32;
        result.Z = this.M33;
        return result;
      }
      set
      {
        this.M31 = value.X;
        this.M32 = value.Y;
        this.M33 = value.Z;
      }
    }

    public MgVector3 Translation
    {
      get
      {
        MgVector3 result;
        result.X = this.M41;
        result.Y = this.M42;
        result.Z = this.M43;
        return result;
      }
      set
      {
        this.M41 = value.X;
        this.M42 = value.Y;
        this.M43 = value.Z;
      }
    }

    #region IEquatable<Matrix> Members

    public bool Equals(MgMatrix other)
    {
      return this.M11 == other.M11 && this.M22 == other.M22 && this.M33 == other.M33 && this.M44 == other.M44 &&
        this.M12 == other.M12 && this.M13 == other.M13 && this.M14 == other.M14 && this.M21 == other.M21 &&
        this.M23 == other.M23 && this.M24 == other.M24 && this.M31 == other.M31 && this.M32 == other.M32 &&
        this.M34 == other.M34 && this.M41 == other.M41 && this.M42 == other.M42 && this.M43 == other.M43;
    }

    #endregion

    public static MgMatrix CreateBillboard(MgVector3 objectPosition, MgVector3 cameraPosition, MgVector3 cameraUpVector,
      MgVector3? cameraForwardVector)
    {
      MgVector3 vector;
      vector.X = objectPosition.X - cameraPosition.X;
      vector.Y = objectPosition.Y - cameraPosition.Y;
      vector.Z = objectPosition.Z - cameraPosition.Z;
      var num = vector.LengthSquared();
      if (num < 0.0001f)
      {
        vector = (cameraForwardVector.HasValue ? (-cameraForwardVector.Value) : MgVector3.Forward);
      }
      else
      {
        MgVector3.Multiply(ref vector, 1f / (float)Math.Sqrt(num), out vector);
      }
      MgVector3 vector2;
      MgVector3.Cross(ref cameraUpVector, ref vector, out vector2);
      vector2.Normalize();
      MgVector3 vector3;
      MgVector3.Cross(ref vector, ref vector2, out vector3);
      MgMatrix result;
      result.M11 = vector2.X;
      result.M12 = vector2.Y;
      result.M13 = vector2.Z;
      result.M14 = 0f;
      result.M21 = vector3.X;
      result.M22 = vector3.Y;
      result.M23 = vector3.Z;
      result.M24 = 0f;
      result.M31 = vector.X;
      result.M32 = vector.Y;
      result.M33 = vector.Z;
      result.M34 = 0f;
      result.M41 = objectPosition.X;
      result.M42 = objectPosition.Y;
      result.M43 = objectPosition.Z;
      result.M44 = 1f;
      return result;
    }

    public static void CreateBillboard(ref MgVector3 objectPosition, ref MgVector3 cameraPosition,
      ref MgVector3 cameraUpVector, MgVector3? cameraForwardVector, out MgMatrix result)
    {
      MgVector3 vector;
      vector.X = objectPosition.X - cameraPosition.X;
      vector.Y = objectPosition.Y - cameraPosition.Y;
      vector.Z = objectPosition.Z - cameraPosition.Z;
      var num = vector.LengthSquared();
      if (num < 0.0001f)
      {
        vector = (cameraForwardVector.HasValue ? (-cameraForwardVector.Value) : MgVector3.Forward);
      }
      else
      {
        MgVector3.Multiply(ref vector, 1f / (float)Math.Sqrt(num), out vector);
      }
      MgVector3 vector2;
      MgVector3.Cross(ref cameraUpVector, ref vector, out vector2);
      vector2.Normalize();
      MgVector3 vector3;
      MgVector3.Cross(ref vector, ref vector2, out vector3);
      result.M11 = vector2.X;
      result.M12 = vector2.Y;
      result.M13 = vector2.Z;
      result.M14 = 0f;
      result.M21 = vector3.X;
      result.M22 = vector3.Y;
      result.M23 = vector3.Z;
      result.M24 = 0f;
      result.M31 = vector.X;
      result.M32 = vector.Y;
      result.M33 = vector.Z;
      result.M34 = 0f;
      result.M41 = objectPosition.X;
      result.M42 = objectPosition.Y;
      result.M43 = objectPosition.Z;
      result.M44 = 1f;
    }

    public static MgMatrix CreateConstrainedBillboard(MgVector3 objectPosition, MgVector3 cameraPosition, MgVector3 rotateAxis,
      MgVector3? cameraForwardVector, MgVector3? objectForwardVector)
    {
      MgVector3 vector;
      vector.X = objectPosition.X - cameraPosition.X;
      vector.Y = objectPosition.Y - cameraPosition.Y;
      vector.Z = objectPosition.Z - cameraPosition.Z;
      var num = vector.LengthSquared();
      if (num < 0.0001f)
      {
        vector = (cameraForwardVector.HasValue ? (-cameraForwardVector.Value) : MgVector3.Forward);
      }
      else
      {
        MgVector3.Multiply(ref vector, 1f / (float)Math.Sqrt(num), out vector);
      }
      var vector2 = rotateAxis;
      float value;
      MgVector3.Dot(ref rotateAxis, ref vector, out value);
      MgVector3 vector3;
      MgVector3 vector4;
      if (Math.Abs(value) > 0.998254657f)
      {
        if (objectForwardVector.HasValue)
        {
          vector3 = objectForwardVector.Value;
          MgVector3.Dot(ref rotateAxis, ref vector3, out value);
          if (Math.Abs(value) > 0.998254657f)
          {
            value = rotateAxis.X * MgVector3.Forward.X + rotateAxis.Y * MgVector3.Forward.Y + rotateAxis.Z * MgVector3.Forward.Z;
            vector3 = ((Math.Abs(value) > 0.998254657f) ? MgVector3.Right : MgVector3.Forward);
          }
        }
        else
        {
          value = rotateAxis.X * MgVector3.Forward.X + rotateAxis.Y * MgVector3.Forward.Y + rotateAxis.Z * MgVector3.Forward.Z;
          vector3 = ((Math.Abs(value) > 0.998254657f) ? MgVector3.Right : MgVector3.Forward);
        }
        MgVector3.Cross(ref rotateAxis, ref vector3, out vector4);
        vector4.Normalize();
        MgVector3.Cross(ref vector4, ref rotateAxis, out vector3);
        vector3.Normalize();
      }
      else
      {
        MgVector3.Cross(ref rotateAxis, ref vector, out vector4);
        vector4.Normalize();
        MgVector3.Cross(ref vector4, ref vector2, out vector3);
        vector3.Normalize();
      }
      MgMatrix result;
      result.M11 = vector4.X;
      result.M12 = vector4.Y;
      result.M13 = vector4.Z;
      result.M14 = 0f;
      result.M21 = vector2.X;
      result.M22 = vector2.Y;
      result.M23 = vector2.Z;
      result.M24 = 0f;
      result.M31 = vector3.X;
      result.M32 = vector3.Y;
      result.M33 = vector3.Z;
      result.M34 = 0f;
      result.M41 = objectPosition.X;
      result.M42 = objectPosition.Y;
      result.M43 = objectPosition.Z;
      result.M44 = 1f;
      return result;
    }

    public static void CreateConstrainedBillboard(ref MgVector3 objectPosition, ref MgVector3 cameraPosition,
      ref MgVector3 rotateAxis, MgVector3? cameraForwardVector, MgVector3? objectForwardVector, out MgMatrix result)
    {
      MgVector3 vector;
      vector.X = objectPosition.X - cameraPosition.X;
      vector.Y = objectPosition.Y - cameraPosition.Y;
      vector.Z = objectPosition.Z - cameraPosition.Z;
      var num = vector.LengthSquared();
      if (num < 0.0001f)
      {
        vector = (cameraForwardVector.HasValue ? (-cameraForwardVector.Value) : MgVector3.Forward);
      }
      else
      {
        MgVector3.Multiply(ref vector, 1f / (float)Math.Sqrt(num), out vector);
      }
      var vector2 = rotateAxis;
      float value;
      MgVector3.Dot(ref rotateAxis, ref vector, out value);
      MgVector3 vector3;
      MgVector3 vector4;
      if (Math.Abs(value) > 0.998254657f)
      {
        if (objectForwardVector.HasValue)
        {
          vector3 = objectForwardVector.Value;
          MgVector3.Dot(ref rotateAxis, ref vector3, out value);
          if (Math.Abs(value) > 0.998254657f)
          {
            value = rotateAxis.X * MgVector3.Forward.X + rotateAxis.Y * MgVector3.Forward.Y + rotateAxis.Z * MgVector3.Forward.Z;
            vector3 = ((Math.Abs(value) > 0.998254657f) ? MgVector3.Right : MgVector3.Forward);
          }
        }
        else
        {
          value = rotateAxis.X * MgVector3.Forward.X + rotateAxis.Y * MgVector3.Forward.Y + rotateAxis.Z * MgVector3.Forward.Z;
          vector3 = ((Math.Abs(value) > 0.998254657f) ? MgVector3.Right : MgVector3.Forward);
        }
        MgVector3.Cross(ref rotateAxis, ref vector3, out vector4);
        vector4.Normalize();
        MgVector3.Cross(ref vector4, ref rotateAxis, out vector3);
        vector3.Normalize();
      }
      else
      {
        MgVector3.Cross(ref rotateAxis, ref vector, out vector4);
        vector4.Normalize();
        MgVector3.Cross(ref vector4, ref vector2, out vector3);
        vector3.Normalize();
      }
      result.M11 = vector4.X;
      result.M12 = vector4.Y;
      result.M13 = vector4.Z;
      result.M14 = 0f;
      result.M21 = vector2.X;
      result.M22 = vector2.Y;
      result.M23 = vector2.Z;
      result.M24 = 0f;
      result.M31 = vector3.X;
      result.M32 = vector3.Y;
      result.M33 = vector3.Z;
      result.M34 = 0f;
      result.M41 = objectPosition.X;
      result.M42 = objectPosition.Y;
      result.M43 = objectPosition.Z;
      result.M44 = 1f;
    }

    public static MgMatrix CreateTranslation(MgVector3 position)
    {
      MgMatrix result;
      result.M11 = 1f;
      result.M12 = 0f;
      result.M13 = 0f;
      result.M14 = 0f;
      result.M21 = 0f;
      result.M22 = 1f;
      result.M23 = 0f;
      result.M24 = 0f;
      result.M31 = 0f;
      result.M32 = 0f;
      result.M33 = 1f;
      result.M34 = 0f;
      result.M41 = position.X;
      result.M42 = position.Y;
      result.M43 = position.Z;
      result.M44 = 1f;
      return result;
    }

    public static void CreateTranslation(ref MgVector3 position, out MgMatrix result)
    {
      result.M11 = 1f;
      result.M12 = 0f;
      result.M13 = 0f;
      result.M14 = 0f;
      result.M21 = 0f;
      result.M22 = 1f;
      result.M23 = 0f;
      result.M24 = 0f;
      result.M31 = 0f;
      result.M32 = 0f;
      result.M33 = 1f;
      result.M34 = 0f;
      result.M41 = position.X;
      result.M42 = position.Y;
      result.M43 = position.Z;
      result.M44 = 1f;
    }

    public static MgMatrix CreateTranslation(float xPosition, float yPosition, float zPosition)
    {
      MgMatrix result;
      result.M11 = 1f;
      result.M12 = 0f;
      result.M13 = 0f;
      result.M14 = 0f;
      result.M21 = 0f;
      result.M22 = 1f;
      result.M23 = 0f;
      result.M24 = 0f;
      result.M31 = 0f;
      result.M32 = 0f;
      result.M33 = 1f;
      result.M34 = 0f;
      result.M41 = xPosition;
      result.M42 = yPosition;
      result.M43 = zPosition;
      result.M44 = 1f;
      return result;
    }

    public static void CreateTranslation(float xPosition, float yPosition, float zPosition, out MgMatrix result)
    {
      result.M11 = 1f;
      result.M12 = 0f;
      result.M13 = 0f;
      result.M14 = 0f;
      result.M21 = 0f;
      result.M22 = 1f;
      result.M23 = 0f;
      result.M24 = 0f;
      result.M31 = 0f;
      result.M32 = 0f;
      result.M33 = 1f;
      result.M34 = 0f;
      result.M41 = xPosition;
      result.M42 = yPosition;
      result.M43 = zPosition;
      result.M44 = 1f;
    }

    public static MgMatrix CreateScale(float xScale, float yScale, float zScale)
    {
      MgMatrix result;
      result.M11 = xScale;
      result.M12 = 0f;
      result.M13 = 0f;
      result.M14 = 0f;
      result.M21 = 0f;
      result.M22 = yScale;
      result.M23 = 0f;
      result.M24 = 0f;
      result.M31 = 0f;
      result.M32 = 0f;
      result.M33 = zScale;
      result.M34 = 0f;
      result.M41 = 0f;
      result.M42 = 0f;
      result.M43 = 0f;
      result.M44 = 1f;
      return result;
    }

    public static void CreateScale(float xScale, float yScale, float zScale, out MgMatrix result)
    {
      result.M11 = xScale;
      result.M12 = 0f;
      result.M13 = 0f;
      result.M14 = 0f;
      result.M21 = 0f;
      result.M22 = yScale;
      result.M23 = 0f;
      result.M24 = 0f;
      result.M31 = 0f;
      result.M32 = 0f;
      result.M33 = zScale;
      result.M34 = 0f;
      result.M41 = 0f;
      result.M42 = 0f;
      result.M43 = 0f;
      result.M44 = 1f;
    }

    public static MgMatrix CreateScale(MgVector3 scales)
    {
      var x = scales.X;
      var y = scales.Y;
      var z = scales.Z;
      MgMatrix result;
      result.M11 = x;
      result.M12 = 0f;
      result.M13 = 0f;
      result.M14 = 0f;
      result.M21 = 0f;
      result.M22 = y;
      result.M23 = 0f;
      result.M24 = 0f;
      result.M31 = 0f;
      result.M32 = 0f;
      result.M33 = z;
      result.M34 = 0f;
      result.M41 = 0f;
      result.M42 = 0f;
      result.M43 = 0f;
      result.M44 = 1f;
      return result;
    }

    public static void CreateScale(ref MgVector3 scales, out MgMatrix result)
    {
      var x = scales.X;
      var y = scales.Y;
      var z = scales.Z;
      result.M11 = x;
      result.M12 = 0f;
      result.M13 = 0f;
      result.M14 = 0f;
      result.M21 = 0f;
      result.M22 = y;
      result.M23 = 0f;
      result.M24 = 0f;
      result.M31 = 0f;
      result.M32 = 0f;
      result.M33 = z;
      result.M34 = 0f;
      result.M41 = 0f;
      result.M42 = 0f;
      result.M43 = 0f;
      result.M44 = 1f;
    }

    public static MgMatrix CreateScale(float scale)
    {
      MgMatrix result;
      result.M11 = scale;
      result.M12 = 0f;
      result.M13 = 0f;
      result.M14 = 0f;
      result.M21 = 0f;
      result.M22 = scale;
      result.M23 = 0f;
      result.M24 = 0f;
      result.M31 = 0f;
      result.M32 = 0f;
      result.M33 = scale;
      result.M34 = 0f;
      result.M41 = 0f;
      result.M42 = 0f;
      result.M43 = 0f;
      result.M44 = 1f;
      return result;
    }

    public static void CreateScale(float scale, out MgMatrix result)
    {
      result.M11 = scale;
      result.M12 = 0f;
      result.M13 = 0f;
      result.M14 = 0f;
      result.M21 = 0f;
      result.M22 = scale;
      result.M23 = 0f;
      result.M24 = 0f;
      result.M31 = 0f;
      result.M32 = 0f;
      result.M33 = scale;
      result.M34 = 0f;
      result.M41 = 0f;
      result.M42 = 0f;
      result.M43 = 0f;
      result.M44 = 1f;
    }

    public static MgMatrix CreateRotationX(float radians)
    {
      var num = (float)Math.Cos(radians);
      var num2 = (float)Math.Sin(radians);
      MgMatrix result;
      result.M11 = 1f;
      result.M12 = 0f;
      result.M13 = 0f;
      result.M14 = 0f;
      result.M21 = 0f;
      result.M22 = num;
      result.M23 = num2;
      result.M24 = 0f;
      result.M31 = 0f;
      result.M32 = -num2;
      result.M33 = num;
      result.M34 = 0f;
      result.M41 = 0f;
      result.M42 = 0f;
      result.M43 = 0f;
      result.M44 = 1f;
      return result;
    }

    public static void CreateRotationX(float radians, out MgMatrix result)
    {
      var num = (float)Math.Cos(radians);
      var num2 = (float)Math.Sin(radians);
      result.M11 = 1f;
      result.M12 = 0f;
      result.M13 = 0f;
      result.M14 = 0f;
      result.M21 = 0f;
      result.M22 = num;
      result.M23 = num2;
      result.M24 = 0f;
      result.M31 = 0f;
      result.M32 = -num2;
      result.M33 = num;
      result.M34 = 0f;
      result.M41 = 0f;
      result.M42 = 0f;
      result.M43 = 0f;
      result.M44 = 1f;
    }

    public static MgMatrix CreateRotationY(float radians)
    {
      var num = (float)Math.Cos(radians);
      var num2 = (float)Math.Sin(radians);
      MgMatrix result;
      result.M11 = num;
      result.M12 = 0f;
      result.M13 = -num2;
      result.M14 = 0f;
      result.M21 = 0f;
      result.M22 = 1f;
      result.M23 = 0f;
      result.M24 = 0f;
      result.M31 = num2;
      result.M32 = 0f;
      result.M33 = num;
      result.M34 = 0f;
      result.M41 = 0f;
      result.M42 = 0f;
      result.M43 = 0f;
      result.M44 = 1f;
      return result;
    }

    public static void CreateRotationY(float radians, out MgMatrix result)
    {
      var num = (float)Math.Cos(radians);
      var num2 = (float)Math.Sin(radians);
      result.M11 = num;
      result.M12 = 0f;
      result.M13 = -num2;
      result.M14 = 0f;
      result.M21 = 0f;
      result.M22 = 1f;
      result.M23 = 0f;
      result.M24 = 0f;
      result.M31 = num2;
      result.M32 = 0f;
      result.M33 = num;
      result.M34 = 0f;
      result.M41 = 0f;
      result.M42 = 0f;
      result.M43 = 0f;
      result.M44 = 1f;
    }

    public static MgMatrix CreateRotationZ(float radians)
    {
      var num = (float)Math.Cos(radians);
      var num2 = (float)Math.Sin(radians);
      MgMatrix result;
      result.M11 = num;
      result.M12 = num2;
      result.M13 = 0f;
      result.M14 = 0f;
      result.M21 = -num2;
      result.M22 = num;
      result.M23 = 0f;
      result.M24 = 0f;
      result.M31 = 0f;
      result.M32 = 0f;
      result.M33 = 1f;
      result.M34 = 0f;
      result.M41 = 0f;
      result.M42 = 0f;
      result.M43 = 0f;
      result.M44 = 1f;
      return result;
    }

    public static void CreateRotationZ(float radians, out MgMatrix result)
    {
      var num = (float)Math.Cos(radians);
      var num2 = (float)Math.Sin(radians);
      result.M11 = num;
      result.M12 = num2;
      result.M13 = 0f;
      result.M14 = 0f;
      result.M21 = -num2;
      result.M22 = num;
      result.M23 = 0f;
      result.M24 = 0f;
      result.M31 = 0f;
      result.M32 = 0f;
      result.M33 = 1f;
      result.M34 = 0f;
      result.M41 = 0f;
      result.M42 = 0f;
      result.M43 = 0f;
      result.M44 = 1f;
    }

    public static MgMatrix CreateFromAxisAngle(MgVector3 axis, float angle)
    {
      var x = axis.X;
      var y = axis.Y;
      var z = axis.Z;
      var num = (float)Math.Sin(angle);
      var num2 = (float)Math.Cos(angle);
      var num3 = x * x;
      var num4 = y * y;
      var num5 = z * z;
      var num6 = x * y;
      var num7 = x * z;
      var num8 = y * z;
      MgMatrix result;
      result.M11 = num3 + num2 * (1f - num3);
      result.M12 = num6 - num2 * num6 + num * z;
      result.M13 = num7 - num2 * num7 - num * y;
      result.M14 = 0f;
      result.M21 = num6 - num2 * num6 - num * z;
      result.M22 = num4 + num2 * (1f - num4);
      result.M23 = num8 - num2 * num8 + num * x;
      result.M24 = 0f;
      result.M31 = num7 - num2 * num7 + num * y;
      result.M32 = num8 - num2 * num8 - num * x;
      result.M33 = num5 + num2 * (1f - num5);
      result.M34 = 0f;
      result.M41 = 0f;
      result.M42 = 0f;
      result.M43 = 0f;
      result.M44 = 1f;
      return result;
    }

    public static void CreateFromAxisAngle(ref MgVector3 axis, float angle, out MgMatrix result)
    {
      var x = axis.X;
      var y = axis.Y;
      var z = axis.Z;
      var num = (float)Math.Sin(angle);
      var num2 = (float)Math.Cos(angle);
      var num3 = x * x;
      var num4 = y * y;
      var num5 = z * z;
      var num6 = x * y;
      var num7 = x * z;
      var num8 = y * z;
      result.M11 = num3 + num2 * (1f - num3);
      result.M12 = num6 - num2 * num6 + num * z;
      result.M13 = num7 - num2 * num7 - num * y;
      result.M14 = 0f;
      result.M21 = num6 - num2 * num6 - num * z;
      result.M22 = num4 + num2 * (1f - num4);
      result.M23 = num8 - num2 * num8 + num * x;
      result.M24 = 0f;
      result.M31 = num7 - num2 * num7 + num * y;
      result.M32 = num8 - num2 * num8 - num * x;
      result.M33 = num5 + num2 * (1f - num5);
      result.M34 = 0f;
      result.M41 = 0f;
      result.M42 = 0f;
      result.M43 = 0f;
      result.M44 = 1f;
    }

    public static MgMatrix CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance,
      float farPlaneDistance)
    {
      if (fieldOfView <= 0f || fieldOfView >= 3.14159274f)
      {
        throw new ArgumentOutOfRangeException("fieldOfView");
      }
      if (nearPlaneDistance <= 0f)
      {
        throw new ArgumentOutOfRangeException("nearPlaneDistance");
      }
      if (farPlaneDistance <= 0f)
      {
        throw new ArgumentOutOfRangeException("farPlaneDistance");
      }
      if (nearPlaneDistance >= farPlaneDistance)
      {
        throw new ArgumentOutOfRangeException("nearPlaneDistance");
      }
      var num = 1f / (float)Math.Tan(fieldOfView * 0.5f);
      var m = num / aspectRatio;
      MgMatrix result;
      result.M11 = m;
      result.M12 = (result.M13 = (result.M14 = 0f));
      result.M22 = num;
      result.M21 = (result.M23 = (result.M24 = 0f));
      result.M31 = (result.M32 = 0f);
      result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
      result.M34 = -1f;
      result.M41 = (result.M42 = (result.M44 = 0f));
      result.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
      return result;
    }

    public static void CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance,
      float farPlaneDistance, out MgMatrix result)
    {
      if (fieldOfView <= 0f || fieldOfView >= 3.14159274f)
      {
        throw new ArgumentOutOfRangeException("fieldOfView");
      }
      if (nearPlaneDistance <= 0f)
      {
        throw new ArgumentOutOfRangeException("nearPlaneDistance");
      }
      if (farPlaneDistance <= 0f)
      {
        throw new ArgumentOutOfRangeException("farPlaneDistance");
      }
      if (nearPlaneDistance >= farPlaneDistance)
      {
        throw new ArgumentOutOfRangeException("nearPlaneDistance");
      }
      var num = 1f / (float)Math.Tan(fieldOfView * 0.5f);
      var m = num / aspectRatio;
      result.M11 = m;
      result.M12 = (result.M13 = (result.M14 = 0f));
      result.M22 = num;
      result.M21 = (result.M23 = (result.M24 = 0f));
      result.M31 = (result.M32 = 0f);
      result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
      result.M34 = -1f;
      result.M41 = (result.M42 = (result.M44 = 0f));
      result.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
    }

    public static MgMatrix CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance)
    {
      if (nearPlaneDistance <= 0f)
      {
        throw new ArgumentOutOfRangeException("nearPlaneDistance");
      }
      if (farPlaneDistance <= 0f)
      {
        throw new ArgumentOutOfRangeException("farPlaneDistance");
      }
      if (nearPlaneDistance >= farPlaneDistance)
      {
        throw new ArgumentOutOfRangeException("nearPlaneDistance");
      }
      MgMatrix result;
      result.M11 = 2f * nearPlaneDistance / width;
      result.M12 = (result.M13 = (result.M14 = 0f));
      result.M22 = 2f * nearPlaneDistance / height;
      result.M21 = (result.M23 = (result.M24 = 0f));
      result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
      result.M31 = (result.M32 = 0f);
      result.M34 = -1f;
      result.M41 = (result.M42 = (result.M44 = 0f));
      result.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
      return result;
    }

    public static void CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance,
      out MgMatrix result)
    {
      if (nearPlaneDistance <= 0f)
      {
        throw new ArgumentOutOfRangeException("nearPlaneDistance");
      }
      if (farPlaneDistance <= 0f)
      {
        throw new ArgumentOutOfRangeException("farPlaneDistance");
      }
      if (nearPlaneDistance >= farPlaneDistance)
      {
        throw new ArgumentOutOfRangeException("nearPlaneDistance");
      }
      result.M11 = 2f * nearPlaneDistance / width;
      result.M12 = (result.M13 = (result.M14 = 0f));
      result.M22 = 2f * nearPlaneDistance / height;
      result.M21 = (result.M23 = (result.M24 = 0f));
      result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
      result.M31 = (result.M32 = 0f);
      result.M34 = -1f;
      result.M41 = (result.M42 = (result.M44 = 0f));
      result.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
    }

    public static MgMatrix CreatePerspectiveOffCenter(float left, float right, float bottom, float top,
      float nearPlaneDistance, float farPlaneDistance)
    {
      if (nearPlaneDistance <= 0f)
      {
        throw new ArgumentOutOfRangeException("nearPlaneDistance");
      }
      if (farPlaneDistance <= 0f)
      {
        throw new ArgumentOutOfRangeException("farPlaneDistance");
      }
      if (nearPlaneDistance >= farPlaneDistance)
      {
        throw new ArgumentOutOfRangeException("nearPlaneDistance");
      }
      MgMatrix result;
      result.M11 = 2f * nearPlaneDistance / (right - left);
      result.M12 = (result.M13 = (result.M14 = 0f));
      result.M22 = 2f * nearPlaneDistance / (top - bottom);
      result.M21 = (result.M23 = (result.M24 = 0f));
      result.M31 = (left + right) / (right - left);
      result.M32 = (top + bottom) / (top - bottom);
      result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
      result.M34 = -1f;
      result.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
      result.M41 = (result.M42 = (result.M44 = 0f));
      return result;
    }

    public static void CreatePerspectiveOffCenter(float left, float right, float bottom, float top,
      float nearPlaneDistance, float farPlaneDistance, out MgMatrix result)
    {
      if (nearPlaneDistance <= 0f)
      {
        throw new ArgumentOutOfRangeException("nearPlaneDistance");
      }
      if (farPlaneDistance <= 0f)
      {
        throw new ArgumentOutOfRangeException("farPlaneDistance");
      }

      if (nearPlaneDistance >= farPlaneDistance)
      {
        throw new ArgumentOutOfRangeException("nearPlaneDistance");
      }

      result.M11 = 2f * nearPlaneDistance / (right - left);
      result.M12 = (result.M13 = (result.M14 = 0f));
      result.M22 = 2f * nearPlaneDistance / (top - bottom);
      result.M21 = (result.M23 = (result.M24 = 0f));
      result.M31 = (left + right) / (right - left);
      result.M32 = (top + bottom) / (top - bottom);
      result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
      result.M34 = -1f;
      result.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
      result.M41 = (result.M42 = (result.M44 = 0f));
    }

    public static MgMatrix CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane)
    {
      MgMatrix result;
      result.M11 = 2f / width;
      result.M12 = (result.M13 = (result.M14 = 0f));
      result.M22 = 2f / height;
      result.M21 = (result.M23 = (result.M24 = 0f));
      result.M33 = 1f / (zNearPlane - zFarPlane);
      result.M31 = (result.M32 = (result.M34 = 0f));
      result.M41 = (result.M42 = 0f);
      result.M43 = zNearPlane / (zNearPlane - zFarPlane);
      result.M44 = 1f;
      return result;
    }

    public static void CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane,
      out MgMatrix result)
    {
      result.M11 = 2f / width;
      result.M12 = (result.M13 = (result.M14 = 0f));
      result.M22 = 2f / height;
      result.M21 = (result.M23 = (result.M24 = 0f));
      result.M33 = 1f / (zNearPlane - zFarPlane);
      result.M31 = (result.M32 = (result.M34 = 0f));
      result.M41 = (result.M42 = 0f);
      result.M43 = zNearPlane / (zNearPlane - zFarPlane);
      result.M44 = 1f;
    }

    public static MgMatrix CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane,
      float zFarPlane)
    {
      MgMatrix result;
      result.M11 = 2f / (right - left);
      result.M12 = (result.M13 = (result.M14 = 0f));
      result.M22 = 2f / (top - bottom);
      result.M21 = (result.M23 = (result.M24 = 0f));
      result.M33 = 1f / (zNearPlane - zFarPlane);
      result.M31 = (result.M32 = (result.M34 = 0f));
      result.M41 = (left + right) / (left - right);
      result.M42 = (top + bottom) / (bottom - top);
      result.M43 = zNearPlane / (zNearPlane - zFarPlane);
      result.M44 = 1f;
      return result;
    }

    public static void CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane,
      float zFarPlane, out MgMatrix result)
    {
      result.M11 = 2f / (right - left);
      result.M12 = (result.M13 = (result.M14 = 0f));
      result.M22 = 2f / (top - bottom);
      result.M21 = (result.M23 = (result.M24 = 0f));
      result.M33 = 1f / (zNearPlane - zFarPlane);
      result.M31 = (result.M32 = (result.M34 = 0f));
      result.M41 = (left + right) / (left - right);
      result.M42 = (top + bottom) / (bottom - top);
      result.M43 = zNearPlane / (zNearPlane - zFarPlane);
      result.M44 = 1f;
    }

    public static MgMatrix CreateLookAt(MgVector3 cameraPosition, MgVector3 cameraTarget, MgVector3 cameraUpVector)
    {
      var vector = MgVector3.Normalize(cameraPosition - cameraTarget);
      var vector2 = MgVector3.Normalize(MgVector3.Cross(cameraUpVector, vector));
      var vector3 = MgVector3.Cross(vector, vector2);
      MgMatrix result;
      result.M11 = vector2.X;
      result.M12 = vector3.X;
      result.M13 = vector.X;
      result.M14 = 0f;
      result.M21 = vector2.Y;
      result.M22 = vector3.Y;
      result.M23 = vector.Y;
      result.M24 = 0f;
      result.M31 = vector2.Z;
      result.M32 = vector3.Z;
      result.M33 = vector.Z;
      result.M34 = 0f;
      result.M41 = -MgVector3.Dot(vector2, cameraPosition);
      result.M42 = -MgVector3.Dot(vector3, cameraPosition);
      result.M43 = -MgVector3.Dot(vector, cameraPosition);
      result.M44 = 1f;
      return result;
    }

    public static void CreateLookAt(ref MgVector3 cameraPosition, ref MgVector3 cameraTarget, ref MgVector3 cameraUpVector,
      out MgMatrix result)
    {
      var vector = MgVector3.Normalize(cameraPosition - cameraTarget);
      var vector2 = MgVector3.Normalize(MgVector3.Cross(cameraUpVector, vector));
      var vector3 = MgVector3.Cross(vector, vector2);
      result.M11 = vector2.X;
      result.M12 = vector3.X;
      result.M13 = vector.X;
      result.M14 = 0f;
      result.M21 = vector2.Y;
      result.M22 = vector3.Y;
      result.M23 = vector.Y;
      result.M24 = 0f;
      result.M31 = vector2.Z;
      result.M32 = vector3.Z;
      result.M33 = vector.Z;
      result.M34 = 0f;
      result.M41 = -MgVector3.Dot(vector2, cameraPosition);
      result.M42 = -MgVector3.Dot(vector3, cameraPosition);
      result.M43 = -MgVector3.Dot(vector, cameraPosition);
      result.M44 = 1f;
    }

    public static MgMatrix CreateWorld(MgVector3 position, MgVector3 forward, MgVector3 up)
    {
      var vector = MgVector3.Normalize(-forward);
      var vector2 = MgVector3.Normalize(MgVector3.Cross(up, vector));
      var vector3 = MgVector3.Cross(vector, vector2);
      MgMatrix result;
      result.M11 = vector2.X;
      result.M12 = vector2.Y;
      result.M13 = vector2.Z;
      result.M14 = 0f;
      result.M21 = vector3.X;
      result.M22 = vector3.Y;
      result.M23 = vector3.Z;
      result.M24 = 0f;
      result.M31 = vector.X;
      result.M32 = vector.Y;
      result.M33 = vector.Z;
      result.M34 = 0f;
      result.M41 = position.X;
      result.M42 = position.Y;
      result.M43 = position.Z;
      result.M44 = 1f;
      return result;
    }

    public static void CreateWorld(ref MgVector3 position, ref MgVector3 forward, ref MgVector3 up, out MgMatrix result)
    {
      var vector = MgVector3.Normalize(-forward);
      var vector2 = MgVector3.Normalize(MgVector3.Cross(up, vector));
      var vector3 = MgVector3.Cross(vector, vector2);
      result.M11 = vector2.X;
      result.M12 = vector2.Y;
      result.M13 = vector2.Z;
      result.M14 = 0f;
      result.M21 = vector3.X;
      result.M22 = vector3.Y;
      result.M23 = vector3.Z;
      result.M24 = 0f;
      result.M31 = vector.X;
      result.M32 = vector.Y;
      result.M33 = vector.Z;
      result.M34 = 0f;
      result.M41 = position.X;
      result.M42 = position.Y;
      result.M43 = position.Z;
      result.M44 = 1f;
    }

    public override string ToString()
    {
      var currentCulture = CultureInfo.CurrentCulture;
      return string.Concat(new[]
      {
        "{ ",
        string.Format(currentCulture, "{{M11:{0} M12:{1} M13:{2} M14:{3}}} ", new object[]
        {
          this.M11.ToString(
            currentCulture),
          this.M12.ToString(
            currentCulture),
          this.M13.ToString(
            currentCulture),
          this.M14.ToString(
            currentCulture)
        }),
        string.Format(currentCulture, "{{M21:{0} M22:{1} M23:{2} M24:{3}}} ", new object[]
        {
          this.M21.ToString(
            currentCulture),
          this.M22.ToString(
            currentCulture),
          this.M23.ToString(
            currentCulture),
          this.M24.ToString(
            currentCulture)
        }),
        string.Format(currentCulture, "{{M31:{0} M32:{1} M33:{2} M34:{3}}} ", new object[]
        {
          this.M31.ToString(
            currentCulture),
          this.M32.ToString(
            currentCulture),
          this.M33.ToString(
            currentCulture),
          this.M34.ToString(
            currentCulture)
        }),
        string.Format(currentCulture, "{{M41:{0} M42:{1} M43:{2} M44:{3}}} ", new object[]
        {
          this.M41.ToString(
            currentCulture),
          this.M42.ToString(
            currentCulture),
          this.M43.ToString(
            currentCulture),
          this.M44.ToString(
            currentCulture)
        }),
        "}"
      });
    }

    public override bool Equals(object obj)
    {
      var result = false;
      if (obj is MgMatrix)
      {
        result = this.Equals((MgMatrix)obj);
      }
      return result;
    }

    public override int GetHashCode()
    {
      return this.M11.GetHashCode() + this.M12.GetHashCode() + this.M13.GetHashCode() + this.M14.GetHashCode() +
        this.M21.GetHashCode() + this.M22.GetHashCode() + this.M23.GetHashCode() + this.M24.GetHashCode() +
        this.M31.GetHashCode() + this.M32.GetHashCode() + this.M33.GetHashCode() + this.M34.GetHashCode() +
        this.M41.GetHashCode() + this.M42.GetHashCode() + this.M43.GetHashCode() + this.M44.GetHashCode();
    }

    public static MgMatrix Transpose(MgMatrix matrix)
    {
      MgMatrix result;
      result.M11 = matrix.M11;
      result.M12 = matrix.M21;
      result.M13 = matrix.M31;
      result.M14 = matrix.M41;
      result.M21 = matrix.M12;
      result.M22 = matrix.M22;
      result.M23 = matrix.M32;
      result.M24 = matrix.M42;
      result.M31 = matrix.M13;
      result.M32 = matrix.M23;
      result.M33 = matrix.M33;
      result.M34 = matrix.M43;
      result.M41 = matrix.M14;
      result.M42 = matrix.M24;
      result.M43 = matrix.M34;
      result.M44 = matrix.M44;
      return result;
    }

    public static void Transpose(ref MgMatrix matrix, out MgMatrix result)
    {
      var m = matrix.M11;
      var m2 = matrix.M12;
      var m3 = matrix.M13;
      var m4 = matrix.M14;
      var m5 = matrix.M21;
      var m6 = matrix.M22;
      var m7 = matrix.M23;
      var m8 = matrix.M24;
      var m9 = matrix.M31;
      var m10 = matrix.M32;
      var m11 = matrix.M33;
      var m12 = matrix.M34;
      var m13 = matrix.M41;
      var m14 = matrix.M42;
      var m15 = matrix.M43;
      var m16 = matrix.M44;
      result.M11 = m;
      result.M12 = m5;
      result.M13 = m9;
      result.M14 = m13;
      result.M21 = m2;
      result.M22 = m6;
      result.M23 = m10;
      result.M24 = m14;
      result.M31 = m3;
      result.M32 = m7;
      result.M33 = m11;
      result.M34 = m15;
      result.M41 = m4;
      result.M42 = m8;
      result.M43 = m12;
      result.M44 = m16;
    }

    public float Determinant()
    {
      var m = this.M11;
      var m2 = this.M12;
      var m3 = this.M13;
      var m4 = this.M14;
      var m5 = this.M21;
      var m6 = this.M22;
      var m7 = this.M23;
      var m8 = this.M24;
      var m9 = this.M31;
      var m10 = this.M32;
      var m11 = this.M33;
      var m12 = this.M34;
      var m13 = this.M41;
      var m14 = this.M42;
      var m15 = this.M43;
      var m16 = this.M44;
      var num = m11 * m16 - m12 * m15;
      var num2 = m10 * m16 - m12 * m14;
      var num3 = m10 * m15 - m11 * m14;
      var num4 = m9 * m16 - m12 * m13;
      var num5 = m9 * m15 - m11 * m13;
      var num6 = m9 * m14 - m10 * m13;
      return m * (m6 * num - m7 * num2 + m8 * num3) - m2 * (m5 * num - m7 * num4 + m8 * num5) + m3 * (m5 * num2 - m6 * num4 + m8 * num6) -
        m4 * (m5 * num3 - m6 * num5 + m7 * num6);
    }

    public static MgMatrix Invert(MgMatrix matrix)
    {
      var m = matrix.M11;
      var m2 = matrix.M12;
      var m3 = matrix.M13;
      var m4 = matrix.M14;
      var m5 = matrix.M21;
      var m6 = matrix.M22;
      var m7 = matrix.M23;
      var m8 = matrix.M24;
      var m9 = matrix.M31;
      var m10 = matrix.M32;
      var m11 = matrix.M33;
      var m12 = matrix.M34;
      var m13 = matrix.M41;
      var m14 = matrix.M42;
      var m15 = matrix.M43;
      var m16 = matrix.M44;
      var num = m11 * m16 - m12 * m15;
      var num2 = m10 * m16 - m12 * m14;
      var num3 = m10 * m15 - m11 * m14;
      var num4 = m9 * m16 - m12 * m13;
      var num5 = m9 * m15 - m11 * m13;
      var num6 = m9 * m14 - m10 * m13;
      var num7 = m6 * num - m7 * num2 + m8 * num3;
      var num8 = -(m5 * num - m7 * num4 + m8 * num5);
      var num9 = m5 * num2 - m6 * num4 + m8 * num6;
      var num10 = -(m5 * num3 - m6 * num5 + m7 * num6);
      var num11 = 1f / (m * num7 + m2 * num8 + m3 * num9 + m4 * num10);
      MgMatrix result;
      result.M11 = num7 * num11;
      result.M21 = num8 * num11;
      result.M31 = num9 * num11;
      result.M41 = num10 * num11;
      result.M12 = -(m2 * num - m3 * num2 + m4 * num3) * num11;
      result.M22 = (m * num - m3 * num4 + m4 * num5) * num11;
      result.M32 = -(m * num2 - m2 * num4 + m4 * num6) * num11;
      result.M42 = (m * num3 - m2 * num5 + m3 * num6) * num11;
      var num12 = m7 * m16 - m8 * m15;
      var num13 = m6 * m16 - m8 * m14;
      var num14 = m6 * m15 - m7 * m14;
      var num15 = m5 * m16 - m8 * m13;
      var num16 = m5 * m15 - m7 * m13;
      var num17 = m5 * m14 - m6 * m13;
      result.M13 = (m2 * num12 - m3 * num13 + m4 * num14) * num11;
      result.M23 = -(m * num12 - m3 * num15 + m4 * num16) * num11;
      result.M33 = (m * num13 - m2 * num15 + m4 * num17) * num11;
      result.M43 = -(m * num14 - m2 * num16 + m3 * num17) * num11;
      var num18 = m7 * m12 - m8 * m11;
      var num19 = m6 * m12 - m8 * m10;
      var num20 = m6 * m11 - m7 * m10;
      var num21 = m5 * m12 - m8 * m9;
      var num22 = m5 * m11 - m7 * m9;
      var num23 = m5 * m10 - m6 * m9;
      result.M14 = -(m2 * num18 - m3 * num19 + m4 * num20) * num11;
      result.M24 = (m * num18 - m3 * num21 + m4 * num22) * num11;
      result.M34 = -(m * num19 - m2 * num21 + m4 * num23) * num11;
      result.M44 = (m * num20 - m2 * num22 + m3 * num23) * num11;
      return result;
    }

    public static void Invert(ref MgMatrix matrix, out MgMatrix result)
    {
      var m = matrix.M11;
      var m2 = matrix.M12;
      var m3 = matrix.M13;
      var m4 = matrix.M14;
      var m5 = matrix.M21;
      var m6 = matrix.M22;
      var m7 = matrix.M23;
      var m8 = matrix.M24;
      var m9 = matrix.M31;
      var m10 = matrix.M32;
      var m11 = matrix.M33;
      var m12 = matrix.M34;
      var m13 = matrix.M41;
      var m14 = matrix.M42;
      var m15 = matrix.M43;
      var m16 = matrix.M44;
      var num = m11 * m16 - m12 * m15;
      var num2 = m10 * m16 - m12 * m14;
      var num3 = m10 * m15 - m11 * m14;
      var num4 = m9 * m16 - m12 * m13;
      var num5 = m9 * m15 - m11 * m13;
      var num6 = m9 * m14 - m10 * m13;
      var num7 = m6 * num - m7 * num2 + m8 * num3;
      var num8 = -(m5 * num - m7 * num4 + m8 * num5);
      var num9 = m5 * num2 - m6 * num4 + m8 * num6;
      var num10 = -(m5 * num3 - m6 * num5 + m7 * num6);
      var num11 = 1f / (m * num7 + m2 * num8 + m3 * num9 + m4 * num10);
      result.M11 = num7 * num11;
      result.M21 = num8 * num11;
      result.M31 = num9 * num11;
      result.M41 = num10 * num11;
      result.M12 = -(m2 * num - m3 * num2 + m4 * num3) * num11;
      result.M22 = (m * num - m3 * num4 + m4 * num5) * num11;
      result.M32 = -(m * num2 - m2 * num4 + m4 * num6) * num11;
      result.M42 = (m * num3 - m2 * num5 + m3 * num6) * num11;
      var num12 = m7 * m16 - m8 * m15;
      var num13 = m6 * m16 - m8 * m14;
      var num14 = m6 * m15 - m7 * m14;
      var num15 = m5 * m16 - m8 * m13;
      var num16 = m5 * m15 - m7 * m13;
      var num17 = m5 * m14 - m6 * m13;
      result.M13 = (m2 * num12 - m3 * num13 + m4 * num14) * num11;
      result.M23 = -(m * num12 - m3 * num15 + m4 * num16) * num11;
      result.M33 = (m * num13 - m2 * num15 + m4 * num17) * num11;
      result.M43 = -(m * num14 - m2 * num16 + m3 * num17) * num11;
      var num18 = m7 * m12 - m8 * m11;
      var num19 = m6 * m12 - m8 * m10;
      var num20 = m6 * m11 - m7 * m10;
      var num21 = m5 * m12 - m8 * m9;
      var num22 = m5 * m11 - m7 * m9;
      var num23 = m5 * m10 - m6 * m9;
      result.M14 = -(m2 * num18 - m3 * num19 + m4 * num20) * num11;
      result.M24 = (m * num18 - m3 * num21 + m4 * num22) * num11;
      result.M34 = -(m * num19 - m2 * num21 + m4 * num23) * num11;
      result.M44 = (m * num20 - m2 * num22 + m3 * num23) * num11;
    }

    public static MgMatrix Lerp(MgMatrix matrix1, MgMatrix matrix2, float amount)
    {
      MgMatrix result;
      result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
      result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
      result.M13 = matrix1.M13 + (matrix2.M13 - matrix1.M13) * amount;
      result.M14 = matrix1.M14 + (matrix2.M14 - matrix1.M14) * amount;
      result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
      result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
      result.M23 = matrix1.M23 + (matrix2.M23 - matrix1.M23) * amount;
      result.M24 = matrix1.M24 + (matrix2.M24 - matrix1.M24) * amount;
      result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
      result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
      result.M33 = matrix1.M33 + (matrix2.M33 - matrix1.M33) * amount;
      result.M34 = matrix1.M34 + (matrix2.M34 - matrix1.M34) * amount;
      result.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41) * amount;
      result.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42) * amount;
      result.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43) * amount;
      result.M44 = matrix1.M44 + (matrix2.M44 - matrix1.M44) * amount;
      return result;
    }

    public static void Lerp(ref MgMatrix matrix1, ref MgMatrix matrix2, float amount, out MgMatrix result)
    {
      result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
      result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
      result.M13 = matrix1.M13 + (matrix2.M13 - matrix1.M13) * amount;
      result.M14 = matrix1.M14 + (matrix2.M14 - matrix1.M14) * amount;
      result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
      result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
      result.M23 = matrix1.M23 + (matrix2.M23 - matrix1.M23) * amount;
      result.M24 = matrix1.M24 + (matrix2.M24 - matrix1.M24) * amount;
      result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
      result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
      result.M33 = matrix1.M33 + (matrix2.M33 - matrix1.M33) * amount;
      result.M34 = matrix1.M34 + (matrix2.M34 - matrix1.M34) * amount;
      result.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41) * amount;
      result.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42) * amount;
      result.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43) * amount;
      result.M44 = matrix1.M44 + (matrix2.M44 - matrix1.M44) * amount;
    }

    public static MgMatrix Negate(MgMatrix matrix)
    {
      MgMatrix result;
      result.M11 = -matrix.M11;
      result.M12 = -matrix.M12;
      result.M13 = -matrix.M13;
      result.M14 = -matrix.M14;
      result.M21 = -matrix.M21;
      result.M22 = -matrix.M22;
      result.M23 = -matrix.M23;
      result.M24 = -matrix.M24;
      result.M31 = -matrix.M31;
      result.M32 = -matrix.M32;
      result.M33 = -matrix.M33;
      result.M34 = -matrix.M34;
      result.M41 = -matrix.M41;
      result.M42 = -matrix.M42;
      result.M43 = -matrix.M43;
      result.M44 = -matrix.M44;
      return result;
    }

    public static void Negate(ref MgMatrix matrix, out MgMatrix result)
    {
      result.M11 = -matrix.M11;
      result.M12 = -matrix.M12;
      result.M13 = -matrix.M13;
      result.M14 = -matrix.M14;
      result.M21 = -matrix.M21;
      result.M22 = -matrix.M22;
      result.M23 = -matrix.M23;
      result.M24 = -matrix.M24;
      result.M31 = -matrix.M31;
      result.M32 = -matrix.M32;
      result.M33 = -matrix.M33;
      result.M34 = -matrix.M34;
      result.M41 = -matrix.M41;
      result.M42 = -matrix.M42;
      result.M43 = -matrix.M43;
      result.M44 = -matrix.M44;
    }

    public static MgMatrix Add(MgMatrix matrix1, MgMatrix matrix2)
    {
      MgMatrix result;
      result.M11 = matrix1.M11 + matrix2.M11;
      result.M12 = matrix1.M12 + matrix2.M12;
      result.M13 = matrix1.M13 + matrix2.M13;
      result.M14 = matrix1.M14 + matrix2.M14;
      result.M21 = matrix1.M21 + matrix2.M21;
      result.M22 = matrix1.M22 + matrix2.M22;
      result.M23 = matrix1.M23 + matrix2.M23;
      result.M24 = matrix1.M24 + matrix2.M24;
      result.M31 = matrix1.M31 + matrix2.M31;
      result.M32 = matrix1.M32 + matrix2.M32;
      result.M33 = matrix1.M33 + matrix2.M33;
      result.M34 = matrix1.M34 + matrix2.M34;
      result.M41 = matrix1.M41 + matrix2.M41;
      result.M42 = matrix1.M42 + matrix2.M42;
      result.M43 = matrix1.M43 + matrix2.M43;
      result.M44 = matrix1.M44 + matrix2.M44;
      return result;
    }

    public static void Add(ref MgMatrix matrix1, ref MgMatrix matrix2, out MgMatrix result)
    {
      result.M11 = matrix1.M11 + matrix2.M11;
      result.M12 = matrix1.M12 + matrix2.M12;
      result.M13 = matrix1.M13 + matrix2.M13;
      result.M14 = matrix1.M14 + matrix2.M14;
      result.M21 = matrix1.M21 + matrix2.M21;
      result.M22 = matrix1.M22 + matrix2.M22;
      result.M23 = matrix1.M23 + matrix2.M23;
      result.M24 = matrix1.M24 + matrix2.M24;
      result.M31 = matrix1.M31 + matrix2.M31;
      result.M32 = matrix1.M32 + matrix2.M32;
      result.M33 = matrix1.M33 + matrix2.M33;
      result.M34 = matrix1.M34 + matrix2.M34;
      result.M41 = matrix1.M41 + matrix2.M41;
      result.M42 = matrix1.M42 + matrix2.M42;
      result.M43 = matrix1.M43 + matrix2.M43;
      result.M44 = matrix1.M44 + matrix2.M44;
    }

    public static MgMatrix Subtract(MgMatrix matrix1, MgMatrix matrix2)
    {
      MgMatrix result;
      result.M11 = matrix1.M11 - matrix2.M11;
      result.M12 = matrix1.M12 - matrix2.M12;
      result.M13 = matrix1.M13 - matrix2.M13;
      result.M14 = matrix1.M14 - matrix2.M14;
      result.M21 = matrix1.M21 - matrix2.M21;
      result.M22 = matrix1.M22 - matrix2.M22;
      result.M23 = matrix1.M23 - matrix2.M23;
      result.M24 = matrix1.M24 - matrix2.M24;
      result.M31 = matrix1.M31 - matrix2.M31;
      result.M32 = matrix1.M32 - matrix2.M32;
      result.M33 = matrix1.M33 - matrix2.M33;
      result.M34 = matrix1.M34 - matrix2.M34;
      result.M41 = matrix1.M41 - matrix2.M41;
      result.M42 = matrix1.M42 - matrix2.M42;
      result.M43 = matrix1.M43 - matrix2.M43;
      result.M44 = matrix1.M44 - matrix2.M44;
      return result;
    }

    public static void Subtract(ref MgMatrix matrix1, ref MgMatrix matrix2, out MgMatrix result)
    {
      result.M11 = matrix1.M11 - matrix2.M11;
      result.M12 = matrix1.M12 - matrix2.M12;
      result.M13 = matrix1.M13 - matrix2.M13;
      result.M14 = matrix1.M14 - matrix2.M14;
      result.M21 = matrix1.M21 - matrix2.M21;
      result.M22 = matrix1.M22 - matrix2.M22;
      result.M23 = matrix1.M23 - matrix2.M23;
      result.M24 = matrix1.M24 - matrix2.M24;
      result.M31 = matrix1.M31 - matrix2.M31;
      result.M32 = matrix1.M32 - matrix2.M32;
      result.M33 = matrix1.M33 - matrix2.M33;
      result.M34 = matrix1.M34 - matrix2.M34;
      result.M41 = matrix1.M41 - matrix2.M41;
      result.M42 = matrix1.M42 - matrix2.M42;
      result.M43 = matrix1.M43 - matrix2.M43;
      result.M44 = matrix1.M44 - matrix2.M44;
    }

    public static MgMatrix Multiply(MgMatrix matrix1, MgMatrix matrix2)
    {
      MgMatrix result;
      result.M11 = matrix1.M11 * matrix2.M11 + matrix1.M12 * matrix2.M21 + matrix1.M13 * matrix2.M31 + matrix1.M14 * matrix2.M41;
      result.M12 = matrix1.M11 * matrix2.M12 + matrix1.M12 * matrix2.M22 + matrix1.M13 * matrix2.M32 + matrix1.M14 * matrix2.M42;
      result.M13 = matrix1.M11 * matrix2.M13 + matrix1.M12 * matrix2.M23 + matrix1.M13 * matrix2.M33 + matrix1.M14 * matrix2.M43;
      result.M14 = matrix1.M11 * matrix2.M14 + matrix1.M12 * matrix2.M24 + matrix1.M13 * matrix2.M34 + matrix1.M14 * matrix2.M44;
      result.M21 = matrix1.M21 * matrix2.M11 + matrix1.M22 * matrix2.M21 + matrix1.M23 * matrix2.M31 + matrix1.M24 * matrix2.M41;
      result.M22 = matrix1.M21 * matrix2.M12 + matrix1.M22 * matrix2.M22 + matrix1.M23 * matrix2.M32 + matrix1.M24 * matrix2.M42;
      result.M23 = matrix1.M21 * matrix2.M13 + matrix1.M22 * matrix2.M23 + matrix1.M23 * matrix2.M33 + matrix1.M24 * matrix2.M43;
      result.M24 = matrix1.M21 * matrix2.M14 + matrix1.M22 * matrix2.M24 + matrix1.M23 * matrix2.M34 + matrix1.M24 * matrix2.M44;
      result.M31 = matrix1.M31 * matrix2.M11 + matrix1.M32 * matrix2.M21 + matrix1.M33 * matrix2.M31 + matrix1.M34 * matrix2.M41;
      result.M32 = matrix1.M31 * matrix2.M12 + matrix1.M32 * matrix2.M22 + matrix1.M33 * matrix2.M32 + matrix1.M34 * matrix2.M42;
      result.M33 = matrix1.M31 * matrix2.M13 + matrix1.M32 * matrix2.M23 + matrix1.M33 * matrix2.M33 + matrix1.M34 * matrix2.M43;
      result.M34 = matrix1.M31 * matrix2.M14 + matrix1.M32 * matrix2.M24 + matrix1.M33 * matrix2.M34 + matrix1.M34 * matrix2.M44;
      result.M41 = matrix1.M41 * matrix2.M11 + matrix1.M42 * matrix2.M21 + matrix1.M43 * matrix2.M31 + matrix1.M44 * matrix2.M41;
      result.M42 = matrix1.M41 * matrix2.M12 + matrix1.M42 * matrix2.M22 + matrix1.M43 * matrix2.M32 + matrix1.M44 * matrix2.M42;
      result.M43 = matrix1.M41 * matrix2.M13 + matrix1.M42 * matrix2.M23 + matrix1.M43 * matrix2.M33 + matrix1.M44 * matrix2.M43;
      result.M44 = matrix1.M41 * matrix2.M14 + matrix1.M42 * matrix2.M24 + matrix1.M43 * matrix2.M34 + matrix1.M44 * matrix2.M44;
      return result;
    }

    public static void Multiply(ref MgMatrix matrix1, ref MgMatrix matrix2, out MgMatrix result)
    {
      var m = matrix1.M11 * matrix2.M11 + matrix1.M12 * matrix2.M21 + matrix1.M13 * matrix2.M31 + matrix1.M14 * matrix2.M41;
      var m2 = matrix1.M11 * matrix2.M12 + matrix1.M12 * matrix2.M22 + matrix1.M13 * matrix2.M32 + matrix1.M14 * matrix2.M42;
      var m3 = matrix1.M11 * matrix2.M13 + matrix1.M12 * matrix2.M23 + matrix1.M13 * matrix2.M33 + matrix1.M14 * matrix2.M43;
      var m4 = matrix1.M11 * matrix2.M14 + matrix1.M12 * matrix2.M24 + matrix1.M13 * matrix2.M34 + matrix1.M14 * matrix2.M44;
      var m5 = matrix1.M21 * matrix2.M11 + matrix1.M22 * matrix2.M21 + matrix1.M23 * matrix2.M31 + matrix1.M24 * matrix2.M41;
      var m6 = matrix1.M21 * matrix2.M12 + matrix1.M22 * matrix2.M22 + matrix1.M23 * matrix2.M32 + matrix1.M24 * matrix2.M42;
      var m7 = matrix1.M21 * matrix2.M13 + matrix1.M22 * matrix2.M23 + matrix1.M23 * matrix2.M33 + matrix1.M24 * matrix2.M43;
      var m8 = matrix1.M21 * matrix2.M14 + matrix1.M22 * matrix2.M24 + matrix1.M23 * matrix2.M34 + matrix1.M24 * matrix2.M44;
      var m9 = matrix1.M31 * matrix2.M11 + matrix1.M32 * matrix2.M21 + matrix1.M33 * matrix2.M31 + matrix1.M34 * matrix2.M41;
      var m10 = matrix1.M31 * matrix2.M12 + matrix1.M32 * matrix2.M22 + matrix1.M33 * matrix2.M32 + matrix1.M34 * matrix2.M42;
      var m11 = matrix1.M31 * matrix2.M13 + matrix1.M32 * matrix2.M23 + matrix1.M33 * matrix2.M33 + matrix1.M34 * matrix2.M43;
      var m12 = matrix1.M31 * matrix2.M14 + matrix1.M32 * matrix2.M24 + matrix1.M33 * matrix2.M34 + matrix1.M34 * matrix2.M44;
      var m13 = matrix1.M41 * matrix2.M11 + matrix1.M42 * matrix2.M21 + matrix1.M43 * matrix2.M31 + matrix1.M44 * matrix2.M41;
      var m14 = matrix1.M41 * matrix2.M12 + matrix1.M42 * matrix2.M22 + matrix1.M43 * matrix2.M32 + matrix1.M44 * matrix2.M42;
      var m15 = matrix1.M41 * matrix2.M13 + matrix1.M42 * matrix2.M23 + matrix1.M43 * matrix2.M33 + matrix1.M44 * matrix2.M43;
      var m16 = matrix1.M41 * matrix2.M14 + matrix1.M42 * matrix2.M24 + matrix1.M43 * matrix2.M34 + matrix1.M44 * matrix2.M44;
      result.M11 = m;
      result.M12 = m2;
      result.M13 = m3;
      result.M14 = m4;
      result.M21 = m5;
      result.M22 = m6;
      result.M23 = m7;
      result.M24 = m8;
      result.M31 = m9;
      result.M32 = m10;
      result.M33 = m11;
      result.M34 = m12;
      result.M41 = m13;
      result.M42 = m14;
      result.M43 = m15;
      result.M44 = m16;
    }

    public static MgMatrix Multiply(MgMatrix matrix1, float scaleFactor)
    {
      MgMatrix result;
      result.M11 = matrix1.M11 * scaleFactor;
      result.M12 = matrix1.M12 * scaleFactor;
      result.M13 = matrix1.M13 * scaleFactor;
      result.M14 = matrix1.M14 * scaleFactor;
      result.M21 = matrix1.M21 * scaleFactor;
      result.M22 = matrix1.M22 * scaleFactor;
      result.M23 = matrix1.M23 * scaleFactor;
      result.M24 = matrix1.M24 * scaleFactor;
      result.M31 = matrix1.M31 * scaleFactor;
      result.M32 = matrix1.M32 * scaleFactor;
      result.M33 = matrix1.M33 * scaleFactor;
      result.M34 = matrix1.M34 * scaleFactor;
      result.M41 = matrix1.M41 * scaleFactor;
      result.M42 = matrix1.M42 * scaleFactor;
      result.M43 = matrix1.M43 * scaleFactor;
      result.M44 = matrix1.M44 * scaleFactor;
      return result;
    }

    public static void Multiply(ref MgMatrix matrix1, float scaleFactor, out MgMatrix result)
    {
      result.M11 = matrix1.M11 * scaleFactor;
      result.M12 = matrix1.M12 * scaleFactor;
      result.M13 = matrix1.M13 * scaleFactor;
      result.M14 = matrix1.M14 * scaleFactor;
      result.M21 = matrix1.M21 * scaleFactor;
      result.M22 = matrix1.M22 * scaleFactor;
      result.M23 = matrix1.M23 * scaleFactor;
      result.M24 = matrix1.M24 * scaleFactor;
      result.M31 = matrix1.M31 * scaleFactor;
      result.M32 = matrix1.M32 * scaleFactor;
      result.M33 = matrix1.M33 * scaleFactor;
      result.M34 = matrix1.M34 * scaleFactor;
      result.M41 = matrix1.M41 * scaleFactor;
      result.M42 = matrix1.M42 * scaleFactor;
      result.M43 = matrix1.M43 * scaleFactor;
      result.M44 = matrix1.M44 * scaleFactor;
    }

    public static MgMatrix Divide(MgMatrix matrix1, MgMatrix matrix2)
    {
      MgMatrix result;
      result.M11 = matrix1.M11 / matrix2.M11;
      result.M12 = matrix1.M12 / matrix2.M12;
      result.M13 = matrix1.M13 / matrix2.M13;
      result.M14 = matrix1.M14 / matrix2.M14;
      result.M21 = matrix1.M21 / matrix2.M21;
      result.M22 = matrix1.M22 / matrix2.M22;
      result.M23 = matrix1.M23 / matrix2.M23;
      result.M24 = matrix1.M24 / matrix2.M24;
      result.M31 = matrix1.M31 / matrix2.M31;
      result.M32 = matrix1.M32 / matrix2.M32;
      result.M33 = matrix1.M33 / matrix2.M33;
      result.M34 = matrix1.M34 / matrix2.M34;
      result.M41 = matrix1.M41 / matrix2.M41;
      result.M42 = matrix1.M42 / matrix2.M42;
      result.M43 = matrix1.M43 / matrix2.M43;
      result.M44 = matrix1.M44 / matrix2.M44;
      return result;
    }

    public static void Divide(ref MgMatrix matrix1, ref MgMatrix matrix2, out MgMatrix result)
    {
      result.M11 = matrix1.M11 / matrix2.M11;
      result.M12 = matrix1.M12 / matrix2.M12;
      result.M13 = matrix1.M13 / matrix2.M13;
      result.M14 = matrix1.M14 / matrix2.M14;
      result.M21 = matrix1.M21 / matrix2.M21;
      result.M22 = matrix1.M22 / matrix2.M22;
      result.M23 = matrix1.M23 / matrix2.M23;
      result.M24 = matrix1.M24 / matrix2.M24;
      result.M31 = matrix1.M31 / matrix2.M31;
      result.M32 = matrix1.M32 / matrix2.M32;
      result.M33 = matrix1.M33 / matrix2.M33;
      result.M34 = matrix1.M34 / matrix2.M34;
      result.M41 = matrix1.M41 / matrix2.M41;
      result.M42 = matrix1.M42 / matrix2.M42;
      result.M43 = matrix1.M43 / matrix2.M43;
      result.M44 = matrix1.M44 / matrix2.M44;
    }

    public static MgMatrix Divide(MgMatrix matrix1, float divider)
    {
      var num = 1f / divider;
      MgMatrix result;
      result.M11 = matrix1.M11 * num;
      result.M12 = matrix1.M12 * num;
      result.M13 = matrix1.M13 * num;
      result.M14 = matrix1.M14 * num;
      result.M21 = matrix1.M21 * num;
      result.M22 = matrix1.M22 * num;
      result.M23 = matrix1.M23 * num;
      result.M24 = matrix1.M24 * num;
      result.M31 = matrix1.M31 * num;
      result.M32 = matrix1.M32 * num;
      result.M33 = matrix1.M33 * num;
      result.M34 = matrix1.M34 * num;
      result.M41 = matrix1.M41 * num;
      result.M42 = matrix1.M42 * num;
      result.M43 = matrix1.M43 * num;
      result.M44 = matrix1.M44 * num;
      return result;
    }

    public static void Divide(ref MgMatrix matrix1, float divider, out MgMatrix result)
    {
      var num = 1f / divider;
      result.M11 = matrix1.M11 * num;
      result.M12 = matrix1.M12 * num;
      result.M13 = matrix1.M13 * num;
      result.M14 = matrix1.M14 * num;
      result.M21 = matrix1.M21 * num;
      result.M22 = matrix1.M22 * num;
      result.M23 = matrix1.M23 * num;
      result.M24 = matrix1.M24 * num;
      result.M31 = matrix1.M31 * num;
      result.M32 = matrix1.M32 * num;
      result.M33 = matrix1.M33 * num;
      result.M34 = matrix1.M34 * num;
      result.M41 = matrix1.M41 * num;
      result.M42 = matrix1.M42 * num;
      result.M43 = matrix1.M43 * num;
      result.M44 = matrix1.M44 * num;
    }

    public static MgMatrix operator -(MgMatrix matrix1)
    {
      MgMatrix result;
      result.M11 = -matrix1.M11;
      result.M12 = -matrix1.M12;
      result.M13 = -matrix1.M13;
      result.M14 = -matrix1.M14;
      result.M21 = -matrix1.M21;
      result.M22 = -matrix1.M22;
      result.M23 = -matrix1.M23;
      result.M24 = -matrix1.M24;
      result.M31 = -matrix1.M31;
      result.M32 = -matrix1.M32;
      result.M33 = -matrix1.M33;
      result.M34 = -matrix1.M34;
      result.M41 = -matrix1.M41;
      result.M42 = -matrix1.M42;
      result.M43 = -matrix1.M43;
      result.M44 = -matrix1.M44;
      return result;
    }

    public static bool operator ==(MgMatrix matrix1, MgMatrix matrix2)
    {
      return matrix1.M11 == matrix2.M11 && matrix1.M22 == matrix2.M22 && matrix1.M33 == matrix2.M33 &&
        matrix1.M44 == matrix2.M44 && matrix1.M12 == matrix2.M12 && matrix1.M13 == matrix2.M13 &&
        matrix1.M14 == matrix2.M14 && matrix1.M21 == matrix2.M21 && matrix1.M23 == matrix2.M23 &&
        matrix1.M24 == matrix2.M24 && matrix1.M31 == matrix2.M31 && matrix1.M32 == matrix2.M32 &&
        matrix1.M34 == matrix2.M34 && matrix1.M41 == matrix2.M41 && matrix1.M42 == matrix2.M42 &&
        matrix1.M43 == matrix2.M43;
    }

    public static bool operator !=(MgMatrix matrix1, MgMatrix matrix2)
    {
      return matrix1.M11 != matrix2.M11 || matrix1.M12 != matrix2.M12 || matrix1.M13 != matrix2.M13 ||
        matrix1.M14 != matrix2.M14 || matrix1.M21 != matrix2.M21 || matrix1.M22 != matrix2.M22 ||
        matrix1.M23 != matrix2.M23 || matrix1.M24 != matrix2.M24 || matrix1.M31 != matrix2.M31 ||
        matrix1.M32 != matrix2.M32 || matrix1.M33 != matrix2.M33 || matrix1.M34 != matrix2.M34 ||
        matrix1.M41 != matrix2.M41 || matrix1.M42 != matrix2.M42 || matrix1.M43 != matrix2.M43 ||
        matrix1.M44 != matrix2.M44;
    }

    public static MgMatrix operator +(MgMatrix matrix1, MgMatrix matrix2)
    {
      MgMatrix result;
      result.M11 = matrix1.M11 + matrix2.M11;
      result.M12 = matrix1.M12 + matrix2.M12;
      result.M13 = matrix1.M13 + matrix2.M13;
      result.M14 = matrix1.M14 + matrix2.M14;
      result.M21 = matrix1.M21 + matrix2.M21;
      result.M22 = matrix1.M22 + matrix2.M22;
      result.M23 = matrix1.M23 + matrix2.M23;
      result.M24 = matrix1.M24 + matrix2.M24;
      result.M31 = matrix1.M31 + matrix2.M31;
      result.M32 = matrix1.M32 + matrix2.M32;
      result.M33 = matrix1.M33 + matrix2.M33;
      result.M34 = matrix1.M34 + matrix2.M34;
      result.M41 = matrix1.M41 + matrix2.M41;
      result.M42 = matrix1.M42 + matrix2.M42;
      result.M43 = matrix1.M43 + matrix2.M43;
      result.M44 = matrix1.M44 + matrix2.M44;
      return result;
    }

    public static MgMatrix operator -(MgMatrix matrix1, MgMatrix matrix2)
    {
      MgMatrix result;
      result.M11 = matrix1.M11 - matrix2.M11;
      result.M12 = matrix1.M12 - matrix2.M12;
      result.M13 = matrix1.M13 - matrix2.M13;
      result.M14 = matrix1.M14 - matrix2.M14;
      result.M21 = matrix1.M21 - matrix2.M21;
      result.M22 = matrix1.M22 - matrix2.M22;
      result.M23 = matrix1.M23 - matrix2.M23;
      result.M24 = matrix1.M24 - matrix2.M24;
      result.M31 = matrix1.M31 - matrix2.M31;
      result.M32 = matrix1.M32 - matrix2.M32;
      result.M33 = matrix1.M33 - matrix2.M33;
      result.M34 = matrix1.M34 - matrix2.M34;
      result.M41 = matrix1.M41 - matrix2.M41;
      result.M42 = matrix1.M42 - matrix2.M42;
      result.M43 = matrix1.M43 - matrix2.M43;
      result.M44 = matrix1.M44 - matrix2.M44;
      return result;
    }

    public static MgMatrix operator *(MgMatrix matrix1, MgMatrix matrix2)
    {
      MgMatrix result;
      result.M11 = matrix1.M11 * matrix2.M11 + matrix1.M12 * matrix2.M21 + matrix1.M13 * matrix2.M31 + matrix1.M14 * matrix2.M41;
      result.M12 = matrix1.M11 * matrix2.M12 + matrix1.M12 * matrix2.M22 + matrix1.M13 * matrix2.M32 + matrix1.M14 * matrix2.M42;
      result.M13 = matrix1.M11 * matrix2.M13 + matrix1.M12 * matrix2.M23 + matrix1.M13 * matrix2.M33 + matrix1.M14 * matrix2.M43;
      result.M14 = matrix1.M11 * matrix2.M14 + matrix1.M12 * matrix2.M24 + matrix1.M13 * matrix2.M34 + matrix1.M14 * matrix2.M44;
      result.M21 = matrix1.M21 * matrix2.M11 + matrix1.M22 * matrix2.M21 + matrix1.M23 * matrix2.M31 + matrix1.M24 * matrix2.M41;
      result.M22 = matrix1.M21 * matrix2.M12 + matrix1.M22 * matrix2.M22 + matrix1.M23 * matrix2.M32 + matrix1.M24 * matrix2.M42;
      result.M23 = matrix1.M21 * matrix2.M13 + matrix1.M22 * matrix2.M23 + matrix1.M23 * matrix2.M33 + matrix1.M24 * matrix2.M43;
      result.M24 = matrix1.M21 * matrix2.M14 + matrix1.M22 * matrix2.M24 + matrix1.M23 * matrix2.M34 + matrix1.M24 * matrix2.M44;
      result.M31 = matrix1.M31 * matrix2.M11 + matrix1.M32 * matrix2.M21 + matrix1.M33 * matrix2.M31 + matrix1.M34 * matrix2.M41;
      result.M32 = matrix1.M31 * matrix2.M12 + matrix1.M32 * matrix2.M22 + matrix1.M33 * matrix2.M32 + matrix1.M34 * matrix2.M42;
      result.M33 = matrix1.M31 * matrix2.M13 + matrix1.M32 * matrix2.M23 + matrix1.M33 * matrix2.M33 + matrix1.M34 * matrix2.M43;
      result.M34 = matrix1.M31 * matrix2.M14 + matrix1.M32 * matrix2.M24 + matrix1.M33 * matrix2.M34 + matrix1.M34 * matrix2.M44;
      result.M41 = matrix1.M41 * matrix2.M11 + matrix1.M42 * matrix2.M21 + matrix1.M43 * matrix2.M31 + matrix1.M44 * matrix2.M41;
      result.M42 = matrix1.M41 * matrix2.M12 + matrix1.M42 * matrix2.M22 + matrix1.M43 * matrix2.M32 + matrix1.M44 * matrix2.M42;
      result.M43 = matrix1.M41 * matrix2.M13 + matrix1.M42 * matrix2.M23 + matrix1.M43 * matrix2.M33 + matrix1.M44 * matrix2.M43;
      result.M44 = matrix1.M41 * matrix2.M14 + matrix1.M42 * matrix2.M24 + matrix1.M43 * matrix2.M34 + matrix1.M44 * matrix2.M44;
      return result;
    }

    public static MgMatrix operator *(MgMatrix matrix, float scaleFactor)
    {
      MgMatrix result;
      result.M11 = matrix.M11 * scaleFactor;
      result.M12 = matrix.M12 * scaleFactor;
      result.M13 = matrix.M13 * scaleFactor;
      result.M14 = matrix.M14 * scaleFactor;
      result.M21 = matrix.M21 * scaleFactor;
      result.M22 = matrix.M22 * scaleFactor;
      result.M23 = matrix.M23 * scaleFactor;
      result.M24 = matrix.M24 * scaleFactor;
      result.M31 = matrix.M31 * scaleFactor;
      result.M32 = matrix.M32 * scaleFactor;
      result.M33 = matrix.M33 * scaleFactor;
      result.M34 = matrix.M34 * scaleFactor;
      result.M41 = matrix.M41 * scaleFactor;
      result.M42 = matrix.M42 * scaleFactor;
      result.M43 = matrix.M43 * scaleFactor;
      result.M44 = matrix.M44 * scaleFactor;
      return result;
    }

    public static MgMatrix operator *(float scaleFactor, MgMatrix matrix)
    {
      MgMatrix result;
      result.M11 = matrix.M11 * scaleFactor;
      result.M12 = matrix.M12 * scaleFactor;
      result.M13 = matrix.M13 * scaleFactor;
      result.M14 = matrix.M14 * scaleFactor;
      result.M21 = matrix.M21 * scaleFactor;
      result.M22 = matrix.M22 * scaleFactor;
      result.M23 = matrix.M23 * scaleFactor;
      result.M24 = matrix.M24 * scaleFactor;
      result.M31 = matrix.M31 * scaleFactor;
      result.M32 = matrix.M32 * scaleFactor;
      result.M33 = matrix.M33 * scaleFactor;
      result.M34 = matrix.M34 * scaleFactor;
      result.M41 = matrix.M41 * scaleFactor;
      result.M42 = matrix.M42 * scaleFactor;
      result.M43 = matrix.M43 * scaleFactor;
      result.M44 = matrix.M44 * scaleFactor;
      return result;
    }

    public static MgMatrix operator /(MgMatrix matrix1, MgMatrix matrix2)
    {
      MgMatrix result;
      result.M11 = matrix1.M11 / matrix2.M11;
      result.M12 = matrix1.M12 / matrix2.M12;
      result.M13 = matrix1.M13 / matrix2.M13;
      result.M14 = matrix1.M14 / matrix2.M14;
      result.M21 = matrix1.M21 / matrix2.M21;
      result.M22 = matrix1.M22 / matrix2.M22;
      result.M23 = matrix1.M23 / matrix2.M23;
      result.M24 = matrix1.M24 / matrix2.M24;
      result.M31 = matrix1.M31 / matrix2.M31;
      result.M32 = matrix1.M32 / matrix2.M32;
      result.M33 = matrix1.M33 / matrix2.M33;
      result.M34 = matrix1.M34 / matrix2.M34;
      result.M41 = matrix1.M41 / matrix2.M41;
      result.M42 = matrix1.M42 / matrix2.M42;
      result.M43 = matrix1.M43 / matrix2.M43;
      result.M44 = matrix1.M44 / matrix2.M44;
      return result;
    }

    public static MgMatrix operator /(MgMatrix matrix1, float divider)
    {
      var num = 1f / divider;
      MgMatrix result;
      result.M11 = matrix1.M11 * num;
      result.M12 = matrix1.M12 * num;
      result.M13 = matrix1.M13 * num;
      result.M14 = matrix1.M14 * num;
      result.M21 = matrix1.M21 * num;
      result.M22 = matrix1.M22 * num;
      result.M23 = matrix1.M23 * num;
      result.M24 = matrix1.M24 * num;
      result.M31 = matrix1.M31 * num;
      result.M32 = matrix1.M32 * num;
      result.M33 = matrix1.M33 * num;
      result.M34 = matrix1.M34 * num;
      result.M41 = matrix1.M41 * num;
      result.M42 = matrix1.M42 * num;
      result.M43 = matrix1.M43 * num;
      result.M44 = matrix1.M44 * num;
      return result;
    }
  }
}