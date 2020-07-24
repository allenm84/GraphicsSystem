using System;
using System.Globalization;

namespace MLA.Graphics
{
  [Serializable]
  public struct MgVector2 : IEquatable<MgVector2>
  {
    private static MgVector2 _zero = default(MgVector2);
    private static MgVector2 _one = new MgVector2(1f, 1f);
    private static MgVector2 _unitX = new MgVector2(1f, 0f);
    private static MgVector2 _unitY = new MgVector2(0f, 1f);
    public float X;
    public float Y;

    public MgVector2(float x, float y)
    {
      this.X = x;
      this.Y = y;
    }

    public MgVector2(float value)
    {
      this.Y = value;
      this.X = value;
    }

    public static MgVector2 Zero
    {
      get { return _zero; }
    }

    public static MgVector2 One
    {
      get { return _one; }
    }

    public static MgVector2 UnitX
    {
      get { return _unitX; }
    }

    public static MgVector2 UnitY
    {
      get { return _unitY; }
    }

    #region IEquatable<Vector2> Members

    public bool Equals(MgVector2 other)
    {
      return this.X == other.X && this.Y == other.Y;
    }

    #endregion

    public override string ToString()
    {
      return string.Format(CultureInfo.CurrentCulture, "{{X:{0} Y:{1}}}", this.X, this.Y);
    }

    public override bool Equals(object obj)
    {
      var result = false;
      if (obj is MgVector2)
      {
        result = this.Equals((MgVector2)obj);
      }
      return result;
    }

    public override int GetHashCode()
    {
      return this.X.GetHashCode() + this.Y.GetHashCode();
    }

    public float Length()
    {
      var num = this.X * this.X + this.Y * this.Y;
      return (float)Math.Sqrt(num);
    }

    public float LengthSquared()
    {
      return this.X * this.X + this.Y * this.Y;
    }

    public static float Distance(MgVector2 value1, MgVector2 value2)
    {
      var num = value1.X - value2.X;
      var num2 = value1.Y - value2.Y;
      var num3 = num * num + num2 * num2;
      return (float)Math.Sqrt(num3);
    }

    public static void Distance(ref MgVector2 value1, ref MgVector2 value2, out float result)
    {
      var num = value1.X - value2.X;
      var num2 = value1.Y - value2.Y;
      var num3 = num * num + num2 * num2;
      result = (float)Math.Sqrt(num3);
    }

    public static float DistanceSquared(MgVector2 value1, MgVector2 value2)
    {
      var num = value1.X - value2.X;
      var num2 = value1.Y - value2.Y;
      return num * num + num2 * num2;
    }

    public static void DistanceSquared(ref MgVector2 value1, ref MgVector2 value2, out float result)
    {
      var num = value1.X - value2.X;
      var num2 = value1.Y - value2.Y;
      result = num * num + num2 * num2;
    }

    public static float Dot(MgVector2 value1, MgVector2 value2)
    {
      return value1.X * value2.X + value1.Y * value2.Y;
    }

    public static void Dot(ref MgVector2 value1, ref MgVector2 value2, out float result)
    {
      result = value1.X * value2.X + value1.Y * value2.Y;
    }

    public void Normalize()
    {
      var num = this.X * this.X + this.Y * this.Y;
      var num2 = 1f / (float)Math.Sqrt(num);
      this.X *= num2;
      this.Y *= num2;
    }

    public static MgVector2 Normalize(MgVector2 value)
    {
      var num = value.X * value.X + value.Y * value.Y;
      var num2 = 1f / (float)Math.Sqrt(num);
      MgVector2 result;
      result.X = value.X * num2;
      result.Y = value.Y * num2;
      return result;
    }

    public static void Normalize(ref MgVector2 value, out MgVector2 result)
    {
      var num = value.X * value.X + value.Y * value.Y;
      var num2 = 1f / (float)Math.Sqrt(num);
      result.X = value.X * num2;
      result.Y = value.Y * num2;
    }

    public static MgVector2 Reflect(MgVector2 vector, MgVector2 normal)
    {
      var num = vector.X * normal.X + vector.Y * normal.Y;
      MgVector2 result;
      result.X = vector.X - 2f * num * normal.X;
      result.Y = vector.Y - 2f * num * normal.Y;
      return result;
    }

    public static void Reflect(ref MgVector2 vector, ref MgVector2 normal, out MgVector2 result)
    {
      var num = vector.X * normal.X + vector.Y * normal.Y;
      result.X = vector.X - 2f * num * normal.X;
      result.Y = vector.Y - 2f * num * normal.Y;
    }

    public static MgVector2 Min(MgVector2 value1, MgVector2 value2)
    {
      MgVector2 result;
      result.X = ((value1.X < value2.X) ? value1.X : value2.X);
      result.Y = ((value1.Y < value2.Y) ? value1.Y : value2.Y);
      return result;
    }

    public static void Min(ref MgVector2 value1, ref MgVector2 value2, out MgVector2 result)
    {
      result.X = ((value1.X < value2.X) ? value1.X : value2.X);
      result.Y = ((value1.Y < value2.Y) ? value1.Y : value2.Y);
    }

    public static MgVector2 Max(MgVector2 value1, MgVector2 value2)
    {
      MgVector2 result;
      result.X = ((value1.X > value2.X) ? value1.X : value2.X);
      result.Y = ((value1.Y > value2.Y) ? value1.Y : value2.Y);
      return result;
    }

    public static void Max(ref MgVector2 value1, ref MgVector2 value2, out MgVector2 result)
    {
      result.X = ((value1.X > value2.X) ? value1.X : value2.X);
      result.Y = ((value1.Y > value2.Y) ? value1.Y : value2.Y);
    }

    public static MgVector2 Clamp(MgVector2 value1, MgVector2 min, MgVector2 max)
    {
      var num = value1.X;
      num = ((num > max.X) ? max.X : num);
      num = ((num < min.X) ? min.X : num);
      var num2 = value1.Y;
      num2 = ((num2 > max.Y) ? max.Y : num2);
      num2 = ((num2 < min.Y) ? min.Y : num2);
      MgVector2 result;
      result.X = num;
      result.Y = num2;
      return result;
    }

    public static void Clamp(ref MgVector2 value1, ref MgVector2 min, ref MgVector2 max, out MgVector2 result)
    {
      var num = value1.X;
      num = ((num > max.X) ? max.X : num);
      num = ((num < min.X) ? min.X : num);
      var num2 = value1.Y;
      num2 = ((num2 > max.Y) ? max.Y : num2);
      num2 = ((num2 < min.Y) ? min.Y : num2);
      result.X = num;
      result.Y = num2;
    }

    public static MgVector2 Lerp(MgVector2 value1, MgVector2 value2, float amount)
    {
      MgVector2 result;
      result.X = value1.X + (value2.X - value1.X) * amount;
      result.Y = value1.Y + (value2.Y - value1.Y) * amount;
      return result;
    }

    public static void Lerp(ref MgVector2 value1, ref MgVector2 value2, float amount, out MgVector2 result)
    {
      result.X = value1.X + (value2.X - value1.X) * amount;
      result.Y = value1.Y + (value2.Y - value1.Y) * amount;
    }

    public static MgVector2 Barycentric(MgVector2 value1, MgVector2 value2, MgVector2 value3, float amount1, float amount2)
    {
      MgVector2 result;
      result.X = value1.X + amount1 * (value2.X - value1.X) + amount2 * (value3.X - value1.X);
      result.Y = value1.Y + amount1 * (value2.Y - value1.Y) + amount2 * (value3.Y - value1.Y);
      return result;
    }

    public static void Barycentric(ref MgVector2 value1, ref MgVector2 value2, ref MgVector2 value3, float amount1,
      float amount2, out MgVector2 result)
    {
      result.X = value1.X + amount1 * (value2.X - value1.X) + amount2 * (value3.X - value1.X);
      result.Y = value1.Y + amount1 * (value2.Y - value1.Y) + amount2 * (value3.Y - value1.Y);
    }

    public static MgVector2 SmoothStep(MgVector2 value1, MgVector2 value2, float amount)
    {
      amount = ((amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount));
      amount = amount * amount * (3f - 2f * amount);
      MgVector2 result;
      result.X = value1.X + (value2.X - value1.X) * amount;
      result.Y = value1.Y + (value2.Y - value1.Y) * amount;
      return result;
    }

    public static void SmoothStep(ref MgVector2 value1, ref MgVector2 value2, float amount, out MgVector2 result)
    {
      amount = ((amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount));
      amount = amount * amount * (3f - 2f * amount);
      result.X = value1.X + (value2.X - value1.X) * amount;
      result.Y = value1.Y + (value2.Y - value1.Y) * amount;
    }

    public static MgVector2 CatmullRom(MgVector2 value1, MgVector2 value2, MgVector2 value3, MgVector2 value4, float amount)
    {
      var num = amount * amount;
      var num2 = amount * num;
      MgVector2 result;
      result.X = 0.5f *
        (2f * value2.X + (-value1.X + value3.X) * amount + (2f * value1.X - 5f * value2.X + 4f * value3.X - value4.X) * num +
          (-value1.X + 3f * value2.X - 3f * value3.X + value4.X) * num2);
      result.Y = 0.5f *
        (2f * value2.Y + (-value1.Y + value3.Y) * amount + (2f * value1.Y - 5f * value2.Y + 4f * value3.Y - value4.Y) * num +
          (-value1.Y + 3f * value2.Y - 3f * value3.Y + value4.Y) * num2);
      return result;
    }

    public static void CatmullRom(ref MgVector2 value1, ref MgVector2 value2, ref MgVector2 value3, ref MgVector2 value4,
      float amount, out MgVector2 result)
    {
      var num = amount * amount;
      var num2 = amount * num;
      result.X = 0.5f *
        (2f * value2.X + (-value1.X + value3.X) * amount + (2f * value1.X - 5f * value2.X + 4f * value3.X - value4.X) * num +
          (-value1.X + 3f * value2.X - 3f * value3.X + value4.X) * num2);
      result.Y = 0.5f *
        (2f * value2.Y + (-value1.Y + value3.Y) * amount + (2f * value1.Y - 5f * value2.Y + 4f * value3.Y - value4.Y) * num +
          (-value1.Y + 3f * value2.Y - 3f * value3.Y + value4.Y) * num2);
    }

    public static MgVector2 Hermite(MgVector2 value1, MgVector2 tangent1, MgVector2 value2, MgVector2 tangent2, float amount)
    {
      var num = amount * amount;
      var num2 = amount * num;
      var num3 = 2f * num2 - 3f * num + 1f;
      var num4 = -2f * num2 + 3f * num;
      var num5 = num2 - 2f * num + amount;
      var num6 = num2 - num;
      MgVector2 result;
      result.X = value1.X * num3 + value2.X * num4 + tangent1.X * num5 + tangent2.X * num6;
      result.Y = value1.Y * num3 + value2.Y * num4 + tangent1.Y * num5 + tangent2.Y * num6;
      return result;
    }

    public static void Hermite(ref MgVector2 value1, ref MgVector2 tangent1, ref MgVector2 value2, ref MgVector2 tangent2,
      float amount, out MgVector2 result)
    {
      var num = amount * amount;
      var num2 = amount * num;
      var num3 = 2f * num2 - 3f * num + 1f;
      var num4 = -2f * num2 + 3f * num;
      var num5 = num2 - 2f * num + amount;
      var num6 = num2 - num;
      result.X = value1.X * num3 + value2.X * num4 + tangent1.X * num5 + tangent2.X * num6;
      result.Y = value1.Y * num3 + value2.Y * num4 + tangent1.Y * num5 + tangent2.Y * num6;
    }

    public static MgVector2 Transform(MgVector2 position, MgMatrix matrix)
    {
      var x = position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41;
      var y = position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42;
      MgVector2 result;
      result.X = x;
      result.Y = y;
      return result;
    }

    public static void Transform(ref MgVector2 position, ref MgMatrix matrix, out MgVector2 result)
    {
      var x = position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41;
      var y = position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42;
      result.X = x;
      result.Y = y;
    }

    public static MgVector2 TransformNormal(MgVector2 normal, MgMatrix matrix)
    {
      var x = normal.X * matrix.M11 + normal.Y * matrix.M21;
      var y = normal.X * matrix.M12 + normal.Y * matrix.M22;
      MgVector2 result;
      result.X = x;
      result.Y = y;
      return result;
    }

    public static void TransformNormal(ref MgVector2 normal, ref MgMatrix matrix, out MgVector2 result)
    {
      var x = normal.X * matrix.M11 + normal.Y * matrix.M21;
      var y = normal.X * matrix.M12 + normal.Y * matrix.M22;
      result.X = x;
      result.Y = y;
    }

    public static void Transform(MgVector2[] sourceArray, ref MgMatrix matrix, MgVector2[] destinationArray)
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
        destinationArray[i].X = x * matrix.M11 + y * matrix.M21 + matrix.M41;
        destinationArray[i].Y = x * matrix.M12 + y * matrix.M22 + matrix.M42;
      }
    }

    public static void Transform(MgVector2[] sourceArray, int sourceIndex, ref MgMatrix matrix, MgVector2[] destinationArray,
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
        destinationArray[destinationIndex].X = x * matrix.M11 + y * matrix.M21 + matrix.M41;
        destinationArray[destinationIndex].Y = x * matrix.M12 + y * matrix.M22 + matrix.M42;
        sourceIndex++;
        destinationIndex++;
        length--;
      }
    }

    public static void TransformNormal(MgVector2[] sourceArray, ref MgMatrix matrix, MgVector2[] destinationArray)
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
        destinationArray[i].X = x * matrix.M11 + y * matrix.M21;
        destinationArray[i].Y = x * matrix.M12 + y * matrix.M22;
      }
    }

    public static void TransformNormal(MgVector2[] sourceArray, int sourceIndex, ref MgMatrix matrix,
      MgVector2[] destinationArray, int destinationIndex, int length)
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
        destinationArray[destinationIndex].X = x * matrix.M11 + y * matrix.M21;
        destinationArray[destinationIndex].Y = x * matrix.M12 + y * matrix.M22;
        sourceIndex++;
        destinationIndex++;
        length--;
      }
    }

    public static MgVector2 Negate(MgVector2 value)
    {
      MgVector2 result;
      result.X = -value.X;
      result.Y = -value.Y;
      return result;
    }

    public static void Negate(ref MgVector2 value, out MgVector2 result)
    {
      result.X = -value.X;
      result.Y = -value.Y;
    }

    public static MgVector2 Add(MgVector2 value1, MgVector2 value2)
    {
      MgVector2 result;
      result.X = value1.X + value2.X;
      result.Y = value1.Y + value2.Y;
      return result;
    }

    public static void Add(ref MgVector2 value1, ref MgVector2 value2, out MgVector2 result)
    {
      result.X = value1.X + value2.X;
      result.Y = value1.Y + value2.Y;
    }

    public static MgVector2 Subtract(MgVector2 value1, MgVector2 value2)
    {
      MgVector2 result;
      result.X = value1.X - value2.X;
      result.Y = value1.Y - value2.Y;
      return result;
    }

    public static void Subtract(ref MgVector2 value1, ref MgVector2 value2, out MgVector2 result)
    {
      result.X = value1.X - value2.X;
      result.Y = value1.Y - value2.Y;
    }

    public static MgVector2 Multiply(MgVector2 value1, MgVector2 value2)
    {
      MgVector2 result;
      result.X = value1.X * value2.X;
      result.Y = value1.Y * value2.Y;
      return result;
    }

    public static void Multiply(ref MgVector2 value1, ref MgVector2 value2, out MgVector2 result)
    {
      result.X = value1.X * value2.X;
      result.Y = value1.Y * value2.Y;
    }

    public static MgVector2 Multiply(MgVector2 value1, float scaleFactor)
    {
      MgVector2 result;
      result.X = value1.X * scaleFactor;
      result.Y = value1.Y * scaleFactor;
      return result;
    }

    public static void Multiply(ref MgVector2 value1, float scaleFactor, out MgVector2 result)
    {
      result.X = value1.X * scaleFactor;
      result.Y = value1.Y * scaleFactor;
    }

    public static MgVector2 Divide(MgVector2 value1, MgVector2 value2)
    {
      MgVector2 result;
      result.X = value1.X / value2.X;
      result.Y = value1.Y / value2.Y;
      return result;
    }

    public static void Divide(ref MgVector2 value1, ref MgVector2 value2, out MgVector2 result)
    {
      result.X = value1.X / value2.X;
      result.Y = value1.Y / value2.Y;
    }

    public static MgVector2 Divide(MgVector2 value1, float divider)
    {
      var num = 1f / divider;
      MgVector2 result;
      result.X = value1.X * num;
      result.Y = value1.Y * num;
      return result;
    }

    public static void Divide(ref MgVector2 value1, float divider, out MgVector2 result)
    {
      var num = 1f / divider;
      result.X = value1.X * num;
      result.Y = value1.Y * num;
    }

    public static MgVector2 operator -(MgVector2 value)
    {
      MgVector2 result;
      result.X = -value.X;
      result.Y = -value.Y;
      return result;
    }

    public static bool operator ==(MgVector2 value1, MgVector2 value2)
    {
      return value1.X == value2.X && value1.Y == value2.Y;
    }

    public static bool operator !=(MgVector2 value1, MgVector2 value2)
    {
      return value1.X != value2.X || value1.Y != value2.Y;
    }

    public static MgVector2 operator +(MgVector2 value1, MgVector2 value2)
    {
      MgVector2 result;
      result.X = value1.X + value2.X;
      result.Y = value1.Y + value2.Y;
      return result;
    }

    public static MgVector2 operator -(MgVector2 value1, MgVector2 value2)
    {
      MgVector2 result;
      result.X = value1.X - value2.X;
      result.Y = value1.Y - value2.Y;
      return result;
    }

    public static MgVector2 operator *(MgVector2 value1, MgVector2 value2)
    {
      MgVector2 result;
      result.X = value1.X * value2.X;
      result.Y = value1.Y * value2.Y;
      return result;
    }

    public static MgVector2 operator *(MgVector2 value, float scaleFactor)
    {
      MgVector2 result;
      result.X = value.X * scaleFactor;
      result.Y = value.Y * scaleFactor;
      return result;
    }

    public static MgVector2 operator *(float scaleFactor, MgVector2 value)
    {
      MgVector2 result;
      result.X = value.X * scaleFactor;
      result.Y = value.Y * scaleFactor;
      return result;
    }

    public static MgVector2 operator /(MgVector2 value1, MgVector2 value2)
    {
      MgVector2 result;
      result.X = value1.X / value2.X;
      result.Y = value1.Y / value2.Y;
      return result;
    }

    public static MgVector2 operator /(MgVector2 value1, float divider)
    {
      var num = 1f / divider;
      MgVector2 result;
      result.X = value1.X * num;
      result.Y = value1.Y * num;
      return result;
    }
  }
}