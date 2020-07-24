using System;
using System.Globalization;

namespace MLA.Graphics
{
  [Serializable]
  public struct MgVector4 : IEquatable<MgVector4>
  {
    private static MgVector4 _zero = default(MgVector4);
    private static MgVector4 _one = new MgVector4(1f, 1f, 1f, 1f);
    private static MgVector4 _unitX = new MgVector4(1f, 0f, 0f, 0f);
    private static MgVector4 _unitY = new MgVector4(0f, 1f, 0f, 0f);
    private static MgVector4 _unitZ = new MgVector4(0f, 0f, 1f, 0f);
    private static MgVector4 _unitW = new MgVector4(0f, 0f, 0f, 1f);

    public float W;
    public float X;
    public float Y;
    public float Z;

    public MgVector4(float x, float y, float z, float w)
    {
      this.X = x;
      this.Y = y;
      this.Z = z;
      this.W = w;
    }

    public MgVector4(MgVector2 value, float z, float w)
    {
      this.X = value.X;
      this.Y = value.Y;
      this.Z = z;
      this.W = w;
    }

    public MgVector4(MgVector3 value, float w)
    {
      this.X = value.X;
      this.Y = value.Y;
      this.Z = value.Z;
      this.W = w;
    }

    public MgVector4(float value)
    {
      this.W = value;
      this.Z = value;
      this.Y = value;
      this.X = value;
    }

    public static MgVector4 Zero
    {
      get { return _zero; }
    }

    public static MgVector4 One
    {
      get { return _one; }
    }

    public static MgVector4 UnitX
    {
      get { return _unitX; }
    }

    public static MgVector4 UnitY
    {
      get { return _unitY; }
    }

    public static MgVector4 UnitZ
    {
      get { return _unitZ; }
    }

    public static MgVector4 UnitW
    {
      get { return _unitW; }
    }

    #region IEquatable<Vector4> Members

    public bool Equals(MgVector4 other)
    {
      return this.X == other.X && this.Y == other.Y && this.Z == other.Z && this.W == other.W;
    }

    #endregion

    public override string ToString()
    {
      var currentCulture = CultureInfo.CurrentCulture;
      return string.Format(currentCulture, "{{X:{0} Y:{1} Z:{2} W:{3}}}", X, Y, Z, W);
    }

    public override bool Equals(object obj)
    {
      var result = false;
      if (obj is MgVector4)
      {
        result = this.Equals((MgVector4)obj);
      }
      return result;
    }

    public override int GetHashCode()
    {
      return this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode() + this.W.GetHashCode();
    }

    public float Length()
    {
      var num = this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
      return (float)Math.Sqrt(num);
    }

    public float LengthSquared()
    {
      return this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
    }

    public static float Distance(MgVector4 value1, MgVector4 value2)
    {
      var num = value1.X - value2.X;
      var num2 = value1.Y - value2.Y;
      var num3 = value1.Z - value2.Z;
      var num4 = value1.W - value2.W;
      var num5 = num * num + num2 * num2 + num3 * num3 + num4 * num4;
      return (float)Math.Sqrt(num5);
    }

    public static void Distance(ref MgVector4 value1, ref MgVector4 value2, out float result)
    {
      var num = value1.X - value2.X;
      var num2 = value1.Y - value2.Y;
      var num3 = value1.Z - value2.Z;
      var num4 = value1.W - value2.W;
      var num5 = num * num + num2 * num2 + num3 * num3 + num4 * num4;
      result = (float)Math.Sqrt(num5);
    }

    public static float DistanceSquared(MgVector4 value1, MgVector4 value2)
    {
      var num = value1.X - value2.X;
      var num2 = value1.Y - value2.Y;
      var num3 = value1.Z - value2.Z;
      var num4 = value1.W - value2.W;
      return num * num + num2 * num2 + num3 * num3 + num4 * num4;
    }

    public static void DistanceSquared(ref MgVector4 value1, ref MgVector4 value2, out float result)
    {
      var num = value1.X - value2.X;
      var num2 = value1.Y - value2.Y;
      var num3 = value1.Z - value2.Z;
      var num4 = value1.W - value2.W;
      result = num * num + num2 * num2 + num3 * num3 + num4 * num4;
    }

    public static float Dot(MgVector4 vector1, MgVector4 vector2)
    {
      return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
    }

    public static void Dot(ref MgVector4 vector1, ref MgVector4 vector2, out float result)
    {
      result = vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
    }

    public void Normalize()
    {
      var num = this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
      var num2 = 1f / (float)Math.Sqrt(num);
      this.X *= num2;
      this.Y *= num2;
      this.Z *= num2;
      this.W *= num2;
    }

    public static MgVector4 Normalize(MgVector4 vector)
    {
      var num = vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z + vector.W * vector.W;
      var num2 = 1f / (float)Math.Sqrt(num);
      MgVector4 result;
      result.X = vector.X * num2;
      result.Y = vector.Y * num2;
      result.Z = vector.Z * num2;
      result.W = vector.W * num2;
      return result;
    }

    public static void Normalize(ref MgVector4 vector, out MgVector4 result)
    {
      var num = vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z + vector.W * vector.W;
      var num2 = 1f / (float)Math.Sqrt(num);
      result.X = vector.X * num2;
      result.Y = vector.Y * num2;
      result.Z = vector.Z * num2;
      result.W = vector.W * num2;
    }

    public static MgVector4 Min(MgVector4 value1, MgVector4 value2)
    {
      MgVector4 result;
      result.X = ((value1.X < value2.X) ? value1.X : value2.X);
      result.Y = ((value1.Y < value2.Y) ? value1.Y : value2.Y);
      result.Z = ((value1.Z < value2.Z) ? value1.Z : value2.Z);
      result.W = ((value1.W < value2.W) ? value1.W : value2.W);
      return result;
    }

    public static void Min(ref MgVector4 value1, ref MgVector4 value2, out MgVector4 result)
    {
      result.X = ((value1.X < value2.X) ? value1.X : value2.X);
      result.Y = ((value1.Y < value2.Y) ? value1.Y : value2.Y);
      result.Z = ((value1.Z < value2.Z) ? value1.Z : value2.Z);
      result.W = ((value1.W < value2.W) ? value1.W : value2.W);
    }

    public static MgVector4 Max(MgVector4 value1, MgVector4 value2)
    {
      MgVector4 result;
      result.X = ((value1.X > value2.X) ? value1.X : value2.X);
      result.Y = ((value1.Y > value2.Y) ? value1.Y : value2.Y);
      result.Z = ((value1.Z > value2.Z) ? value1.Z : value2.Z);
      result.W = ((value1.W > value2.W) ? value1.W : value2.W);
      return result;
    }

    public static void Max(ref MgVector4 value1, ref MgVector4 value2, out MgVector4 result)
    {
      result.X = ((value1.X > value2.X) ? value1.X : value2.X);
      result.Y = ((value1.Y > value2.Y) ? value1.Y : value2.Y);
      result.Z = ((value1.Z > value2.Z) ? value1.Z : value2.Z);
      result.W = ((value1.W > value2.W) ? value1.W : value2.W);
    }

    public static MgVector4 Clamp(MgVector4 value1, MgVector4 min, MgVector4 max)
    {
      var num = value1.X;
      num = ((num > max.X) ? max.X : num);
      num = ((num < min.X) ? min.X : num);
      var num2 = value1.Y;
      num2 = ((num2 > max.Y) ? max.Y : num2);
      num2 = ((num2 < min.Y) ? min.Y : num2);
      var num3 = value1.Z;
      num3 = ((num3 > max.Z) ? max.Z : num3);
      num3 = ((num3 < min.Z) ? min.Z : num3);
      var num4 = value1.W;
      num4 = ((num4 > max.W) ? max.W : num4);
      num4 = ((num4 < min.W) ? min.W : num4);
      MgVector4 result;
      result.X = num;
      result.Y = num2;
      result.Z = num3;
      result.W = num4;
      return result;
    }

    public static void Clamp(ref MgVector4 value1, ref MgVector4 min, ref MgVector4 max, out MgVector4 result)
    {
      var num = value1.X;
      num = ((num > max.X) ? max.X : num);
      num = ((num < min.X) ? min.X : num);
      var num2 = value1.Y;
      num2 = ((num2 > max.Y) ? max.Y : num2);
      num2 = ((num2 < min.Y) ? min.Y : num2);
      var num3 = value1.Z;
      num3 = ((num3 > max.Z) ? max.Z : num3);
      num3 = ((num3 < min.Z) ? min.Z : num3);
      var num4 = value1.W;
      num4 = ((num4 > max.W) ? max.W : num4);
      num4 = ((num4 < min.W) ? min.W : num4);
      result.X = num;
      result.Y = num2;
      result.Z = num3;
      result.W = num4;
    }

    public static MgVector4 Lerp(MgVector4 value1, MgVector4 value2, float amount)
    {
      MgVector4 result;
      result.X = value1.X + (value2.X - value1.X) * amount;
      result.Y = value1.Y + (value2.Y - value1.Y) * amount;
      result.Z = value1.Z + (value2.Z - value1.Z) * amount;
      result.W = value1.W + (value2.W - value1.W) * amount;
      return result;
    }

    public static void Lerp(ref MgVector4 value1, ref MgVector4 value2, float amount, out MgVector4 result)
    {
      result.X = value1.X + (value2.X - value1.X) * amount;
      result.Y = value1.Y + (value2.Y - value1.Y) * amount;
      result.Z = value1.Z + (value2.Z - value1.Z) * amount;
      result.W = value1.W + (value2.W - value1.W) * amount;
    }

    public static MgVector4 Barycentric(MgVector4 value1, MgVector4 value2, MgVector4 value3, float amount1, float amount2)
    {
      MgVector4 result;
      result.X = value1.X + amount1 * (value2.X - value1.X) + amount2 * (value3.X - value1.X);
      result.Y = value1.Y + amount1 * (value2.Y - value1.Y) + amount2 * (value3.Y - value1.Y);
      result.Z = value1.Z + amount1 * (value2.Z - value1.Z) + amount2 * (value3.Z - value1.Z);
      result.W = value1.W + amount1 * (value2.W - value1.W) + amount2 * (value3.W - value1.W);
      return result;
    }

    public static void Barycentric(ref MgVector4 value1, ref MgVector4 value2, ref MgVector4 value3, float amount1,
      float amount2, out MgVector4 result)
    {
      result.X = value1.X + amount1 * (value2.X - value1.X) + amount2 * (value3.X - value1.X);
      result.Y = value1.Y + amount1 * (value2.Y - value1.Y) + amount2 * (value3.Y - value1.Y);
      result.Z = value1.Z + amount1 * (value2.Z - value1.Z) + amount2 * (value3.Z - value1.Z);
      result.W = value1.W + amount1 * (value2.W - value1.W) + amount2 * (value3.W - value1.W);
    }

    public static MgVector4 SmoothStep(MgVector4 value1, MgVector4 value2, float amount)
    {
      amount = ((amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount));
      amount = amount * amount * (3f - 2f * amount);
      MgVector4 result;
      result.X = value1.X + (value2.X - value1.X) * amount;
      result.Y = value1.Y + (value2.Y - value1.Y) * amount;
      result.Z = value1.Z + (value2.Z - value1.Z) * amount;
      result.W = value1.W + (value2.W - value1.W) * amount;
      return result;
    }

    public static void SmoothStep(ref MgVector4 value1, ref MgVector4 value2, float amount, out MgVector4 result)
    {
      amount = ((amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount));
      amount = amount * amount * (3f - 2f * amount);
      result.X = value1.X + (value2.X - value1.X) * amount;
      result.Y = value1.Y + (value2.Y - value1.Y) * amount;
      result.Z = value1.Z + (value2.Z - value1.Z) * amount;
      result.W = value1.W + (value2.W - value1.W) * amount;
    }

    public static MgVector4 CatmullRom(MgVector4 value1, MgVector4 value2, MgVector4 value3, MgVector4 value4, float amount)
    {
      var num = amount * amount;
      var num2 = amount * num;
      MgVector4 result;
      result.X = 0.5f *
        (2f * value2.X + (-value1.X + value3.X) * amount + (2f * value1.X - 5f * value2.X + 4f * value3.X - value4.X) * num +
          (-value1.X + 3f * value2.X - 3f * value3.X + value4.X) * num2);
      result.Y = 0.5f *
        (2f * value2.Y + (-value1.Y + value3.Y) * amount + (2f * value1.Y - 5f * value2.Y + 4f * value3.Y - value4.Y) * num +
          (-value1.Y + 3f * value2.Y - 3f * value3.Y + value4.Y) * num2);
      result.Z = 0.5f *
        (2f * value2.Z + (-value1.Z + value3.Z) * amount + (2f * value1.Z - 5f * value2.Z + 4f * value3.Z - value4.Z) * num +
          (-value1.Z + 3f * value2.Z - 3f * value3.Z + value4.Z) * num2);
      result.W = 0.5f *
        (2f * value2.W + (-value1.W + value3.W) * amount + (2f * value1.W - 5f * value2.W + 4f * value3.W - value4.W) * num +
          (-value1.W + 3f * value2.W - 3f * value3.W + value4.W) * num2);
      return result;
    }

    public static void CatmullRom(ref MgVector4 value1, ref MgVector4 value2, ref MgVector4 value3, ref MgVector4 value4,
      float amount, out MgVector4 result)
    {
      var num = amount * amount;
      var num2 = amount * num;
      result.X = 0.5f *
        (2f * value2.X + (-value1.X + value3.X) * amount + (2f * value1.X - 5f * value2.X + 4f * value3.X - value4.X) * num +
          (-value1.X + 3f * value2.X - 3f * value3.X + value4.X) * num2);
      result.Y = 0.5f *
        (2f * value2.Y + (-value1.Y + value3.Y) * amount + (2f * value1.Y - 5f * value2.Y + 4f * value3.Y - value4.Y) * num +
          (-value1.Y + 3f * value2.Y - 3f * value3.Y + value4.Y) * num2);
      result.Z = 0.5f *
        (2f * value2.Z + (-value1.Z + value3.Z) * amount + (2f * value1.Z - 5f * value2.Z + 4f * value3.Z - value4.Z) * num +
          (-value1.Z + 3f * value2.Z - 3f * value3.Z + value4.Z) * num2);
      result.W = 0.5f *
        (2f * value2.W + (-value1.W + value3.W) * amount + (2f * value1.W - 5f * value2.W + 4f * value3.W - value4.W) * num +
          (-value1.W + 3f * value2.W - 3f * value3.W + value4.W) * num2);
    }

    public static MgVector4 Hermite(MgVector4 value1, MgVector4 tangent1, MgVector4 value2, MgVector4 tangent2, float amount)
    {
      var num = amount * amount;
      var num2 = amount * num;
      var num3 = 2f * num2 - 3f * num + 1f;
      var num4 = -2f * num2 + 3f * num;
      var num5 = num2 - 2f * num + amount;
      var num6 = num2 - num;
      MgVector4 result;
      result.X = value1.X * num3 + value2.X * num4 + tangent1.X * num5 + tangent2.X * num6;
      result.Y = value1.Y * num3 + value2.Y * num4 + tangent1.Y * num5 + tangent2.Y * num6;
      result.Z = value1.Z * num3 + value2.Z * num4 + tangent1.Z * num5 + tangent2.Z * num6;
      result.W = value1.W * num3 + value2.W * num4 + tangent1.W * num5 + tangent2.W * num6;
      return result;
    }

    public static void Hermite(ref MgVector4 value1, ref MgVector4 tangent1, ref MgVector4 value2, ref MgVector4 tangent2,
      float amount, out MgVector4 result)
    {
      var num = amount * amount;
      var num2 = amount * num;
      var num3 = 2f * num2 - 3f * num + 1f;
      var num4 = -2f * num2 + 3f * num;
      var num5 = num2 - 2f * num + amount;
      var num6 = num2 - num;
      result.X = value1.X * num3 + value2.X * num4 + tangent1.X * num5 + tangent2.X * num6;
      result.Y = value1.Y * num3 + value2.Y * num4 + tangent1.Y * num5 + tangent2.Y * num6;
      result.Z = value1.Z * num3 + value2.Z * num4 + tangent1.Z * num5 + tangent2.Z * num6;
      result.W = value1.W * num3 + value2.W * num4 + tangent1.W * num5 + tangent2.W * num6;
    }

    public static MgVector4 Transform(MgVector2 position, MgMatrix matrix)
    {
      var x = position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41;
      var y = position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42;
      var z = position.X * matrix.M13 + position.Y * matrix.M23 + matrix.M43;
      var w = position.X * matrix.M14 + position.Y * matrix.M24 + matrix.M44;
      MgVector4 result;
      result.X = x;
      result.Y = y;
      result.Z = z;
      result.W = w;
      return result;
    }

    public static void Transform(ref MgVector2 position, ref MgMatrix matrix, out MgVector4 result)
    {
      var x = position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41;
      var y = position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42;
      var z = position.X * matrix.M13 + position.Y * matrix.M23 + matrix.M43;
      var w = position.X * matrix.M14 + position.Y * matrix.M24 + matrix.M44;
      result.X = x;
      result.Y = y;
      result.Z = z;
      result.W = w;
    }

    public static MgVector4 Transform(MgVector3 position, MgMatrix matrix)
    {
      var x = position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41;
      var y = position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42;
      var z = position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43;
      var w = position.X * matrix.M14 + position.Y * matrix.M24 + position.Z * matrix.M34 + matrix.M44;
      MgVector4 result;
      result.X = x;
      result.Y = y;
      result.Z = z;
      result.W = w;
      return result;
    }

    public static void Transform(ref MgVector3 position, ref MgMatrix matrix, out MgVector4 result)
    {
      var x = position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41;
      var y = position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42;
      var z = position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43;
      var w = position.X * matrix.M14 + position.Y * matrix.M24 + position.Z * matrix.M34 + matrix.M44;
      result.X = x;
      result.Y = y;
      result.Z = z;
      result.W = w;
    }

    public static MgVector4 Transform(MgVector4 vector, MgMatrix matrix)
    {
      var x = vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41;
      var y = vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + vector.W * matrix.M42;
      var z = vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + vector.W * matrix.M43;
      var w = vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + vector.W * matrix.M44;
      MgVector4 result;
      result.X = x;
      result.Y = y;
      result.Z = z;
      result.W = w;
      return result;
    }

    public static void Transform(ref MgVector4 vector, ref MgMatrix matrix, out MgVector4 result)
    {
      var x = vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41;
      var y = vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + vector.W * matrix.M42;
      var z = vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + vector.W * matrix.M43;
      var w = vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + vector.W * matrix.M44;
      result.X = x;
      result.Y = y;
      result.Z = z;
      result.W = w;
    }

    public static void Transform(MgVector4[] sourceArray, ref MgMatrix matrix, MgVector4[] destinationArray)
    {
      if (sourceArray == null)
      {
        throw new ArgumentNullException("sourceArray");
      }
      if (destinationArray == null)
      {
        throw new ArgumentNullException("destinationArray");
      }
      if (destinationArray.Length < sourceArray.Length)
      {
        throw new ArgumentException("Not enough target size");
      }
      for (var i = 0; i < sourceArray.Length; i++)
      {
        var x = sourceArray[i].X;
        var y = sourceArray[i].Y;
        var z = sourceArray[i].Z;
        var w = sourceArray[i].W;
        destinationArray[i].X = x * matrix.M11 + y * matrix.M21 + z * matrix.M31 + w * matrix.M41;
        destinationArray[i].Y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32 + w * matrix.M42;
        destinationArray[i].Z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33 + w * matrix.M43;
        destinationArray[i].W = x * matrix.M14 + y * matrix.M24 + z * matrix.M34 + w * matrix.M44;
      }
    }

    public static void Transform(MgVector4[] sourceArray, int sourceIndex, ref MgMatrix matrix, MgVector4[] destinationArray,
      int destinationIndex, int length)
    {
      if (sourceArray == null)
      {
        throw new ArgumentNullException("sourceArray");
      }
      if (destinationArray == null)
      {
        throw new ArgumentNullException("destinationArray");
      }
      if (sourceArray.Length < sourceIndex + (long)length)
      {
        throw new ArgumentException("Not enough source size");
      }
      if (destinationArray.Length < destinationIndex + (long)length)
      {
        throw new ArgumentException("Not enough target size");
      }
      while (length > 0)
      {
        var x = sourceArray[sourceIndex].X;
        var y = sourceArray[sourceIndex].Y;
        var z = sourceArray[sourceIndex].Z;
        var w = sourceArray[sourceIndex].W;
        destinationArray[destinationIndex].X = x * matrix.M11 + y * matrix.M21 + z * matrix.M31 + w * matrix.M41;
        destinationArray[destinationIndex].Y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32 + w * matrix.M42;
        destinationArray[destinationIndex].Z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33 + w * matrix.M43;
        destinationArray[destinationIndex].W = x * matrix.M14 + y * matrix.M24 + z * matrix.M34 + w * matrix.M44;
        sourceIndex++;
        destinationIndex++;
        length--;
      }
    }

    public static MgVector4 Negate(MgVector4 value)
    {
      MgVector4 result;
      result.X = -value.X;
      result.Y = -value.Y;
      result.Z = -value.Z;
      result.W = -value.W;
      return result;
    }

    public static void Negate(ref MgVector4 value, out MgVector4 result)
    {
      result.X = -value.X;
      result.Y = -value.Y;
      result.Z = -value.Z;
      result.W = -value.W;
    }

    public static MgVector4 Add(MgVector4 value1, MgVector4 value2)
    {
      MgVector4 result;
      result.X = value1.X + value2.X;
      result.Y = value1.Y + value2.Y;
      result.Z = value1.Z + value2.Z;
      result.W = value1.W + value2.W;
      return result;
    }

    public static void Add(ref MgVector4 value1, ref MgVector4 value2, out MgVector4 result)
    {
      result.X = value1.X + value2.X;
      result.Y = value1.Y + value2.Y;
      result.Z = value1.Z + value2.Z;
      result.W = value1.W + value2.W;
    }

    public static MgVector4 Subtract(MgVector4 value1, MgVector4 value2)
    {
      MgVector4 result;
      result.X = value1.X - value2.X;
      result.Y = value1.Y - value2.Y;
      result.Z = value1.Z - value2.Z;
      result.W = value1.W - value2.W;
      return result;
    }

    public static void Subtract(ref MgVector4 value1, ref MgVector4 value2, out MgVector4 result)
    {
      result.X = value1.X - value2.X;
      result.Y = value1.Y - value2.Y;
      result.Z = value1.Z - value2.Z;
      result.W = value1.W - value2.W;
    }

    public static MgVector4 Multiply(MgVector4 value1, MgVector4 value2)
    {
      MgVector4 result;
      result.X = value1.X * value2.X;
      result.Y = value1.Y * value2.Y;
      result.Z = value1.Z * value2.Z;
      result.W = value1.W * value2.W;
      return result;
    }

    public static void Multiply(ref MgVector4 value1, ref MgVector4 value2, out MgVector4 result)
    {
      result.X = value1.X * value2.X;
      result.Y = value1.Y * value2.Y;
      result.Z = value1.Z * value2.Z;
      result.W = value1.W * value2.W;
    }

    public static MgVector4 Multiply(MgVector4 value1, float scaleFactor)
    {
      MgVector4 result;
      result.X = value1.X * scaleFactor;
      result.Y = value1.Y * scaleFactor;
      result.Z = value1.Z * scaleFactor;
      result.W = value1.W * scaleFactor;
      return result;
    }

    public static void Multiply(ref MgVector4 value1, float scaleFactor, out MgVector4 result)
    {
      result.X = value1.X * scaleFactor;
      result.Y = value1.Y * scaleFactor;
      result.Z = value1.Z * scaleFactor;
      result.W = value1.W * scaleFactor;
    }

    public static MgVector4 Divide(MgVector4 value1, MgVector4 value2)
    {
      MgVector4 result;
      result.X = value1.X / value2.X;
      result.Y = value1.Y / value2.Y;
      result.Z = value1.Z / value2.Z;
      result.W = value1.W / value2.W;
      return result;
    }

    public static void Divide(ref MgVector4 value1, ref MgVector4 value2, out MgVector4 result)
    {
      result.X = value1.X / value2.X;
      result.Y = value1.Y / value2.Y;
      result.Z = value1.Z / value2.Z;
      result.W = value1.W / value2.W;
    }

    public static MgVector4 Divide(MgVector4 value1, float divider)
    {
      var num = 1f / divider;
      MgVector4 result;
      result.X = value1.X * num;
      result.Y = value1.Y * num;
      result.Z = value1.Z * num;
      result.W = value1.W * num;
      return result;
    }

    public static void Divide(ref MgVector4 value1, float divider, out MgVector4 result)
    {
      var num = 1f / divider;
      result.X = value1.X * num;
      result.Y = value1.Y * num;
      result.Z = value1.Z * num;
      result.W = value1.W * num;
    }

    public static MgVector4 operator -(MgVector4 value)
    {
      MgVector4 result;
      result.X = -value.X;
      result.Y = -value.Y;
      result.Z = -value.Z;
      result.W = -value.W;
      return result;
    }

    public static bool operator ==(MgVector4 value1, MgVector4 value2)
    {
      return value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z && value1.W == value2.W;
    }

    public static bool operator !=(MgVector4 value1, MgVector4 value2)
    {
      return value1.X != value2.X || value1.Y != value2.Y || value1.Z != value2.Z || value1.W != value2.W;
    }

    public static MgVector4 operator +(MgVector4 value1, MgVector4 value2)
    {
      MgVector4 result;
      result.X = value1.X + value2.X;
      result.Y = value1.Y + value2.Y;
      result.Z = value1.Z + value2.Z;
      result.W = value1.W + value2.W;
      return result;
    }

    public static MgVector4 operator -(MgVector4 value1, MgVector4 value2)
    {
      MgVector4 result;
      result.X = value1.X - value2.X;
      result.Y = value1.Y - value2.Y;
      result.Z = value1.Z - value2.Z;
      result.W = value1.W - value2.W;
      return result;
    }

    public static MgVector4 operator *(MgVector4 value1, MgVector4 value2)
    {
      MgVector4 result;
      result.X = value1.X * value2.X;
      result.Y = value1.Y * value2.Y;
      result.Z = value1.Z * value2.Z;
      result.W = value1.W * value2.W;
      return result;
    }

    public static MgVector4 operator *(MgVector4 value1, float scaleFactor)
    {
      MgVector4 result;
      result.X = value1.X * scaleFactor;
      result.Y = value1.Y * scaleFactor;
      result.Z = value1.Z * scaleFactor;
      result.W = value1.W * scaleFactor;
      return result;
    }

    public static MgVector4 operator *(float scaleFactor, MgVector4 value1)
    {
      MgVector4 result;
      result.X = value1.X * scaleFactor;
      result.Y = value1.Y * scaleFactor;
      result.Z = value1.Z * scaleFactor;
      result.W = value1.W * scaleFactor;
      return result;
    }

    public static MgVector4 operator /(MgVector4 value1, MgVector4 value2)
    {
      MgVector4 result;
      result.X = value1.X / value2.X;
      result.Y = value1.Y / value2.Y;
      result.Z = value1.Z / value2.Z;
      result.W = value1.W / value2.W;
      return result;
    }

    public static MgVector4 operator /(MgVector4 value1, float divider)
    {
      var num = 1f / divider;
      MgVector4 result;
      result.X = value1.X * num;
      result.Y = value1.Y * num;
      result.Z = value1.Z * num;
      result.W = value1.W * num;
      return result;
    }
  }
}