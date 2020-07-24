using System;
using System.Globalization;

namespace MLA.Graphics
{
  [Serializable]
  public struct MgVector3 : IEquatable<MgVector3>
  {
    private static MgVector3 _zero = default(MgVector3);
    private static MgVector3 _one = new MgVector3(1f, 1f, 1f);
    private static MgVector3 _unitX = new MgVector3(1f, 0f, 0f);
    private static MgVector3 _unitY = new MgVector3(0f, 1f, 0f);
    private static MgVector3 _unitZ = new MgVector3(0f, 0f, 1f);
    private static MgVector3 _up = new MgVector3(0f, 1f, 0f);
    private static MgVector3 _down = new MgVector3(0f, -1f, 0f);
    private static MgVector3 _right = new MgVector3(1f, 0f, 0f);
    private static MgVector3 _left = new MgVector3(-1f, 0f, 0f);
    private static MgVector3 _forward = new MgVector3(0f, 0f, -1f);
    private static MgVector3 _backward = new MgVector3(0f, 0f, 1f);

    public float X;
    public float Y;
    public float Z;

    public MgVector3(float x, float y, float z)
    {
      this.X = x;
      this.Y = y;
      this.Z = z;
    }

    public MgVector3(float value)
    {
      this.Z = value;
      this.Y = value;
      this.X = value;
    }

    public MgVector3(MgVector2 value, float z)
    {
      this.X = value.X;
      this.Y = value.Y;
      this.Z = z;
    }

    public static MgVector3 Zero
    {
      get { return _zero; }
    }

    public static MgVector3 One
    {
      get { return _one; }
    }

    public static MgVector3 UnitX
    {
      get { return _unitX; }
    }

    public static MgVector3 UnitY
    {
      get { return _unitY; }
    }

    public static MgVector3 UnitZ
    {
      get { return _unitZ; }
    }

    public static MgVector3 Up
    {
      get { return _up; }
    }

    public static MgVector3 Down
    {
      get { return _down; }
    }

    public static MgVector3 Right
    {
      get { return _right; }
    }

    public static MgVector3 Left
    {
      get { return _left; }
    }

    public static MgVector3 Forward
    {
      get { return _forward; }
    }

    public static MgVector3 Backward
    {
      get { return _backward; }
    }

    #region IEquatable<Vector3> Members

    public bool Equals(MgVector3 other)
    {
      return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
    }

    #endregion

    public override string ToString()
    {
      var currentCulture = CultureInfo.CurrentCulture;
      return string.Format(currentCulture, "{{X:{0} Y:{1} Z:{2}}}", new object[]
      {
        this.X.ToString(currentCulture),
        this.Y.ToString(currentCulture),
        this.Z.ToString(currentCulture)
      });
    }

    public override bool Equals(object obj)
    {
      var result = false;
      if (obj is MgVector3)
      {
        result = this.Equals((MgVector3)obj);
      }
      return result;
    }

    public override int GetHashCode()
    {
      return this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode();
    }

    public float Length()
    {
      var num = this.X * this.X + this.Y * this.Y + this.Z * this.Z;
      return (float)Math.Sqrt(num);
    }

    public float LengthSquared()
    {
      return this.X * this.X + this.Y * this.Y + this.Z * this.Z;
    }

    public static float Distance(MgVector3 value1, MgVector3 value2)
    {
      var num = value1.X - value2.X;
      var num2 = value1.Y - value2.Y;
      var num3 = value1.Z - value2.Z;
      var num4 = num * num + num2 * num2 + num3 * num3;
      return (float)Math.Sqrt(num4);
    }

    public static void Distance(ref MgVector3 value1, ref MgVector3 value2, out float result)
    {
      var num = value1.X - value2.X;
      var num2 = value1.Y - value2.Y;
      var num3 = value1.Z - value2.Z;
      var num4 = num * num + num2 * num2 + num3 * num3;
      result = (float)Math.Sqrt(num4);
    }

    public static float DistanceSquared(MgVector3 value1, MgVector3 value2)
    {
      var num = value1.X - value2.X;
      var num2 = value1.Y - value2.Y;
      var num3 = value1.Z - value2.Z;
      return num * num + num2 * num2 + num3 * num3;
    }

    public static void DistanceSquared(ref MgVector3 value1, ref MgVector3 value2, out float result)
    {
      var num = value1.X - value2.X;
      var num2 = value1.Y - value2.Y;
      var num3 = value1.Z - value2.Z;
      result = num * num + num2 * num2 + num3 * num3;
    }

    public static float Dot(MgVector3 vector1, MgVector3 vector2)
    {
      return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
    }

    public static void Dot(ref MgVector3 vector1, ref MgVector3 vector2, out float result)
    {
      result = vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
    }

    public void Normalize()
    {
      var num = this.X * this.X + this.Y * this.Y + this.Z * this.Z;
      var num2 = 1f / (float)Math.Sqrt(num);
      this.X *= num2;
      this.Y *= num2;
      this.Z *= num2;
    }

    public static MgVector3 Normalize(MgVector3 value)
    {
      var num = value.X * value.X + value.Y * value.Y + value.Z * value.Z;
      var num2 = 1f / (float)Math.Sqrt(num);
      MgVector3 result;
      result.X = value.X * num2;
      result.Y = value.Y * num2;
      result.Z = value.Z * num2;
      return result;
    }

    public static void Normalize(ref MgVector3 value, out MgVector3 result)
    {
      var num = value.X * value.X + value.Y * value.Y + value.Z * value.Z;
      var num2 = 1f / (float)Math.Sqrt(num);
      result.X = value.X * num2;
      result.Y = value.Y * num2;
      result.Z = value.Z * num2;
    }

    public static MgVector3 Cross(MgVector3 vector1, MgVector3 vector2)
    {
      MgVector3 result;
      result.X = vector1.Y * vector2.Z - vector1.Z * vector2.Y;
      result.Y = vector1.Z * vector2.X - vector1.X * vector2.Z;
      result.Z = vector1.X * vector2.Y - vector1.Y * vector2.X;
      return result;
    }

    public static void Cross(ref MgVector3 vector1, ref MgVector3 vector2, out MgVector3 result)
    {
      var x = vector1.Y * vector2.Z - vector1.Z * vector2.Y;
      var y = vector1.Z * vector2.X - vector1.X * vector2.Z;
      var z = vector1.X * vector2.Y - vector1.Y * vector2.X;
      result.X = x;
      result.Y = y;
      result.Z = z;
    }

    public static MgVector3 Reflect(MgVector3 vector, MgVector3 normal)
    {
      var num = vector.X * normal.X + vector.Y * normal.Y + vector.Z * normal.Z;
      MgVector3 result;
      result.X = vector.X - 2f * num * normal.X;
      result.Y = vector.Y - 2f * num * normal.Y;
      result.Z = vector.Z - 2f * num * normal.Z;
      return result;
    }

    public static void Reflect(ref MgVector3 vector, ref MgVector3 normal, out MgVector3 result)
    {
      var num = vector.X * normal.X + vector.Y * normal.Y + vector.Z * normal.Z;
      result.X = vector.X - 2f * num * normal.X;
      result.Y = vector.Y - 2f * num * normal.Y;
      result.Z = vector.Z - 2f * num * normal.Z;
    }

    public static MgVector3 Min(MgVector3 value1, MgVector3 value2)
    {
      MgVector3 result;
      result.X = ((value1.X < value2.X) ? value1.X : value2.X);
      result.Y = ((value1.Y < value2.Y) ? value1.Y : value2.Y);
      result.Z = ((value1.Z < value2.Z) ? value1.Z : value2.Z);
      return result;
    }

    public static void Min(ref MgVector3 value1, ref MgVector3 value2, out MgVector3 result)
    {
      result.X = ((value1.X < value2.X) ? value1.X : value2.X);
      result.Y = ((value1.Y < value2.Y) ? value1.Y : value2.Y);
      result.Z = ((value1.Z < value2.Z) ? value1.Z : value2.Z);
    }

    public static MgVector3 Max(MgVector3 value1, MgVector3 value2)
    {
      MgVector3 result;
      result.X = ((value1.X > value2.X) ? value1.X : value2.X);
      result.Y = ((value1.Y > value2.Y) ? value1.Y : value2.Y);
      result.Z = ((value1.Z > value2.Z) ? value1.Z : value2.Z);
      return result;
    }

    public static void Max(ref MgVector3 value1, ref MgVector3 value2, out MgVector3 result)
    {
      result.X = ((value1.X > value2.X) ? value1.X : value2.X);
      result.Y = ((value1.Y > value2.Y) ? value1.Y : value2.Y);
      result.Z = ((value1.Z > value2.Z) ? value1.Z : value2.Z);
    }

    public static MgVector3 Clamp(MgVector3 value1, MgVector3 min, MgVector3 max)
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
      MgVector3 result;
      result.X = num;
      result.Y = num2;
      result.Z = num3;
      return result;
    }

    public static void Clamp(ref MgVector3 value1, ref MgVector3 min, ref MgVector3 max, out MgVector3 result)
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
      result.X = num;
      result.Y = num2;
      result.Z = num3;
    }

    public static MgVector3 Lerp(MgVector3 value1, MgVector3 value2, float amount)
    {
      MgVector3 result;
      result.X = value1.X + (value2.X - value1.X) * amount;
      result.Y = value1.Y + (value2.Y - value1.Y) * amount;
      result.Z = value1.Z + (value2.Z - value1.Z) * amount;
      return result;
    }

    public static void Lerp(ref MgVector3 value1, ref MgVector3 value2, float amount, out MgVector3 result)
    {
      result.X = value1.X + (value2.X - value1.X) * amount;
      result.Y = value1.Y + (value2.Y - value1.Y) * amount;
      result.Z = value1.Z + (value2.Z - value1.Z) * amount;
    }

    public static MgVector3 Barycentric(MgVector3 value1, MgVector3 value2, MgVector3 value3, float amount1, float amount2)
    {
      MgVector3 result;
      result.X = value1.X + amount1 * (value2.X - value1.X) + amount2 * (value3.X - value1.X);
      result.Y = value1.Y + amount1 * (value2.Y - value1.Y) + amount2 * (value3.Y - value1.Y);
      result.Z = value1.Z + amount1 * (value2.Z - value1.Z) + amount2 * (value3.Z - value1.Z);
      return result;
    }

    public static void Barycentric(ref MgVector3 value1, ref MgVector3 value2, ref MgVector3 value3, float amount1,
      float amount2, out MgVector3 result)
    {
      result.X = value1.X + amount1 * (value2.X - value1.X) + amount2 * (value3.X - value1.X);
      result.Y = value1.Y + amount1 * (value2.Y - value1.Y) + amount2 * (value3.Y - value1.Y);
      result.Z = value1.Z + amount1 * (value2.Z - value1.Z) + amount2 * (value3.Z - value1.Z);
    }

    public static MgVector3 SmoothStep(MgVector3 value1, MgVector3 value2, float amount)
    {
      amount = ((amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount));
      amount = amount * amount * (3f - 2f * amount);
      MgVector3 result;
      result.X = value1.X + (value2.X - value1.X) * amount;
      result.Y = value1.Y + (value2.Y - value1.Y) * amount;
      result.Z = value1.Z + (value2.Z - value1.Z) * amount;
      return result;
    }

    public static void SmoothStep(ref MgVector3 value1, ref MgVector3 value2, float amount, out MgVector3 result)
    {
      amount = ((amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount));
      amount = amount * amount * (3f - 2f * amount);
      result.X = value1.X + (value2.X - value1.X) * amount;
      result.Y = value1.Y + (value2.Y - value1.Y) * amount;
      result.Z = value1.Z + (value2.Z - value1.Z) * amount;
    }

    public static MgVector3 CatmullRom(MgVector3 value1, MgVector3 value2, MgVector3 value3, MgVector3 value4, float amount)
    {
      var num = amount * amount;
      var num2 = amount * num;
      MgVector3 result;
      result.X = 0.5f *
        (2f * value2.X + (-value1.X + value3.X) * amount + (2f * value1.X - 5f * value2.X + 4f * value3.X - value4.X) * num +
          (-value1.X + 3f * value2.X - 3f * value3.X + value4.X) * num2);
      result.Y = 0.5f *
        (2f * value2.Y + (-value1.Y + value3.Y) * amount + (2f * value1.Y - 5f * value2.Y + 4f * value3.Y - value4.Y) * num +
          (-value1.Y + 3f * value2.Y - 3f * value3.Y + value4.Y) * num2);
      result.Z = 0.5f *
        (2f * value2.Z + (-value1.Z + value3.Z) * amount + (2f * value1.Z - 5f * value2.Z + 4f * value3.Z - value4.Z) * num +
          (-value1.Z + 3f * value2.Z - 3f * value3.Z + value4.Z) * num2);
      return result;
    }

    public static void CatmullRom(ref MgVector3 value1, ref MgVector3 value2, ref MgVector3 value3, ref MgVector3 value4,
      float amount, out MgVector3 result)
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
    }

    public static MgVector3 Hermite(MgVector3 value1, MgVector3 tangent1, MgVector3 value2, MgVector3 tangent2, float amount)
    {
      var num = amount * amount;
      var num2 = amount * num;
      var num3 = 2f * num2 - 3f * num + 1f;
      var num4 = -2f * num2 + 3f * num;
      var num5 = num2 - 2f * num + amount;
      var num6 = num2 - num;
      MgVector3 result;
      result.X = value1.X * num3 + value2.X * num4 + tangent1.X * num5 + tangent2.X * num6;
      result.Y = value1.Y * num3 + value2.Y * num4 + tangent1.Y * num5 + tangent2.Y * num6;
      result.Z = value1.Z * num3 + value2.Z * num4 + tangent1.Z * num5 + tangent2.Z * num6;
      return result;
    }

    public static void Hermite(ref MgVector3 value1, ref MgVector3 tangent1, ref MgVector3 value2, ref MgVector3 tangent2,
      float amount, out MgVector3 result)
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
    }

    public static MgVector3 Transform(MgVector3 position, MgMatrix matrix)
    {
      var x = position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41;
      var y = position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42;
      var z = position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43;
      MgVector3 result;
      result.X = x;
      result.Y = y;
      result.Z = z;
      return result;
    }

    public static void Transform(ref MgVector3 position, ref MgMatrix matrix, out MgVector3 result)
    {
      var x = position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41;
      var y = position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42;
      var z = position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43;
      result.X = x;
      result.Y = y;
      result.Z = z;
    }

    public static MgVector3 TransformNormal(MgVector3 normal, MgMatrix matrix)
    {
      var x = normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31;
      var y = normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32;
      var z = normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33;
      MgVector3 result;
      result.X = x;
      result.Y = y;
      result.Z = z;
      return result;
    }

    public static void TransformNormal(ref MgVector3 normal, ref MgMatrix matrix, out MgVector3 result)
    {
      var x = normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31;
      var y = normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32;
      var z = normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33;
      result.X = x;
      result.Y = y;
      result.Z = z;
    }

    public static void Transform(MgVector3[] sourceArray, ref MgMatrix matrix, MgVector3[] destinationArray)
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
        destinationArray[i].X = x * matrix.M11 + y * matrix.M21 + z * matrix.M31 + matrix.M41;
        destinationArray[i].Y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32 + matrix.M42;
        destinationArray[i].Z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33 + matrix.M43;
      }
    }

    public static void Transform(MgVector3[] sourceArray, int sourceIndex, ref MgMatrix matrix, MgVector3[] destinationArray,
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
        destinationArray[destinationIndex].X = x * matrix.M11 + y * matrix.M21 + z * matrix.M31 + matrix.M41;
        destinationArray[destinationIndex].Y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32 + matrix.M42;
        destinationArray[destinationIndex].Z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33 + matrix.M43;
        sourceIndex++;
        destinationIndex++;
        length--;
      }
    }

    public static void TransformNormal(MgVector3[] sourceArray, ref MgMatrix matrix, MgVector3[] destinationArray)
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
        destinationArray[i].X = x * matrix.M11 + y * matrix.M21 + z * matrix.M31;
        destinationArray[i].Y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32;
        destinationArray[i].Z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33;
      }
    }

    public static void TransformNormal(MgVector3[] sourceArray, int sourceIndex, ref MgMatrix matrix,
      MgVector3[] destinationArray, int destinationIndex, int length)
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
        destinationArray[destinationIndex].X = x * matrix.M11 + y * matrix.M21 + z * matrix.M31;
        destinationArray[destinationIndex].Y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32;
        destinationArray[destinationIndex].Z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33;
        sourceIndex++;
        destinationIndex++;
        length--;
      }
    }

    public static MgVector3 Negate(MgVector3 value)
    {
      MgVector3 result;
      result.X = -value.X;
      result.Y = -value.Y;
      result.Z = -value.Z;
      return result;
    }

    public static void Negate(ref MgVector3 value, out MgVector3 result)
    {
      result.X = -value.X;
      result.Y = -value.Y;
      result.Z = -value.Z;
    }

    public static MgVector3 Add(MgVector3 value1, MgVector3 value2)
    {
      MgVector3 result;
      result.X = value1.X + value2.X;
      result.Y = value1.Y + value2.Y;
      result.Z = value1.Z + value2.Z;
      return result;
    }

    public static void Add(ref MgVector3 value1, ref MgVector3 value2, out MgVector3 result)
    {
      result.X = value1.X + value2.X;
      result.Y = value1.Y + value2.Y;
      result.Z = value1.Z + value2.Z;
    }

    public static MgVector3 Subtract(MgVector3 value1, MgVector3 value2)
    {
      MgVector3 result;
      result.X = value1.X - value2.X;
      result.Y = value1.Y - value2.Y;
      result.Z = value1.Z - value2.Z;
      return result;
    }

    public static void Subtract(ref MgVector3 value1, ref MgVector3 value2, out MgVector3 result)
    {
      result.X = value1.X - value2.X;
      result.Y = value1.Y - value2.Y;
      result.Z = value1.Z - value2.Z;
    }

    public static MgVector3 Multiply(MgVector3 value1, MgVector3 value2)
    {
      MgVector3 result;
      result.X = value1.X * value2.X;
      result.Y = value1.Y * value2.Y;
      result.Z = value1.Z * value2.Z;
      return result;
    }

    public static void Multiply(ref MgVector3 value1, ref MgVector3 value2, out MgVector3 result)
    {
      result.X = value1.X * value2.X;
      result.Y = value1.Y * value2.Y;
      result.Z = value1.Z * value2.Z;
    }

    public static MgVector3 Multiply(MgVector3 value1, float scaleFactor)
    {
      MgVector3 result;
      result.X = value1.X * scaleFactor;
      result.Y = value1.Y * scaleFactor;
      result.Z = value1.Z * scaleFactor;
      return result;
    }

    public static void Multiply(ref MgVector3 value1, float scaleFactor, out MgVector3 result)
    {
      result.X = value1.X * scaleFactor;
      result.Y = value1.Y * scaleFactor;
      result.Z = value1.Z * scaleFactor;
    }

    public static MgVector3 Divide(MgVector3 value1, MgVector3 value2)
    {
      MgVector3 result;
      result.X = value1.X / value2.X;
      result.Y = value1.Y / value2.Y;
      result.Z = value1.Z / value2.Z;
      return result;
    }

    public static void Divide(ref MgVector3 value1, ref MgVector3 value2, out MgVector3 result)
    {
      result.X = value1.X / value2.X;
      result.Y = value1.Y / value2.Y;
      result.Z = value1.Z / value2.Z;
    }

    public static MgVector3 Divide(MgVector3 value1, float value2)
    {
      var num = 1f / value2;
      MgVector3 result;
      result.X = value1.X * num;
      result.Y = value1.Y * num;
      result.Z = value1.Z * num;
      return result;
    }

    public static void Divide(ref MgVector3 value1, float value2, out MgVector3 result)
    {
      var num = 1f / value2;
      result.X = value1.X * num;
      result.Y = value1.Y * num;
      result.Z = value1.Z * num;
    }

    public static MgVector3 operator -(MgVector3 value)
    {
      MgVector3 result;
      result.X = -value.X;
      result.Y = -value.Y;
      result.Z = -value.Z;
      return result;
    }

    public static bool operator ==(MgVector3 value1, MgVector3 value2)
    {
      return value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z;
    }

    public static bool operator !=(MgVector3 value1, MgVector3 value2)
    {
      return value1.X != value2.X || value1.Y != value2.Y || value1.Z != value2.Z;
    }

    public static MgVector3 operator +(MgVector3 value1, MgVector3 value2)
    {
      MgVector3 result;
      result.X = value1.X + value2.X;
      result.Y = value1.Y + value2.Y;
      result.Z = value1.Z + value2.Z;
      return result;
    }

    public static MgVector3 operator -(MgVector3 value1, MgVector3 value2)
    {
      MgVector3 result;
      result.X = value1.X - value2.X;
      result.Y = value1.Y - value2.Y;
      result.Z = value1.Z - value2.Z;
      return result;
    }

    public static MgVector3 operator *(MgVector3 value1, MgVector3 value2)
    {
      MgVector3 result;
      result.X = value1.X * value2.X;
      result.Y = value1.Y * value2.Y;
      result.Z = value1.Z * value2.Z;
      return result;
    }

    public static MgVector3 operator *(MgVector3 value, float scaleFactor)
    {
      MgVector3 result;
      result.X = value.X * scaleFactor;
      result.Y = value.Y * scaleFactor;
      result.Z = value.Z * scaleFactor;
      return result;
    }

    public static MgVector3 operator *(float scaleFactor, MgVector3 value)
    {
      MgVector3 result;
      result.X = value.X * scaleFactor;
      result.Y = value.Y * scaleFactor;
      result.Z = value.Z * scaleFactor;
      return result;
    }

    public static MgVector3 operator /(MgVector3 value1, MgVector3 value2)
    {
      MgVector3 result;
      result.X = value1.X / value2.X;
      result.Y = value1.Y / value2.Y;
      result.Z = value1.Z / value2.Z;
      return result;
    }

    public static MgVector3 operator /(MgVector3 value, float divider)
    {
      var num = 1f / divider;
      MgVector3 result;
      result.X = value.X * num;
      result.Y = value.Y * num;
      result.Z = value.Z * num;
      return result;
    }
  }
}