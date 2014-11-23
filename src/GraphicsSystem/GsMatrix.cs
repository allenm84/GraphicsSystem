using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsSystem
{
  public struct GsMatrix
  {
    public static GsMatrix Identity
    {
      get { return new GsMatrix(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f); }
    }

    public float
      M11, M12, M13, M14,
      M21, M22, M23, M24,
      M31, M32, M33, M34,
      M41, M42, M43, M44;

    public GsMatrix(
      float m11, float m12, float m13, float m14, 
      float m21, float m22, float m23, float m24,
      float m31, float m32, float m33, float m34, 
      float m41, float m42, float m43, float m44)
    {
      M11 = m11;
      M12 = m12;
      M13 = m13;
      M14 = m14;
      M21 = m21;
      M22 = m22;
      M23 = m23;
      M24 = m24;
      M31 = m31;
      M32 = m32;
      M33 = m33;
      M34 = m34;
      M41 = m41;
      M42 = m42;
      M43 = m43;
      M44 = m44;
    }

    public static GsMatrix CreateScale(float xScale, float yScale, float zScale)
    {
      GsMatrix returnMatrix = GsMatrix.Identity;

      returnMatrix.M11 = xScale;
      returnMatrix.M22 = yScale;
      returnMatrix.M33 = zScale;

      return returnMatrix;
    }

    public static GsMatrix CreateTranslation(float xPosition, float yPosition, float zPosition)
    {
      GsMatrix returnMatrix = GsMatrix.Identity;

      returnMatrix.M41 = xPosition;
      returnMatrix.M42 = yPosition;
      returnMatrix.M43 = zPosition;

      return returnMatrix;
    }

    public static GsMatrix CreateRotationX(float radians)
    {
      GsMatrix returnMatrix = GsMatrix.Identity;

      returnMatrix.M22 = (float)Math.Cos(radians);
      returnMatrix.M23 = (float)Math.Sin(radians);
      returnMatrix.M32 = -returnMatrix.M23;
      returnMatrix.M33 = returnMatrix.M22;

      return returnMatrix;
    }

    public static GsMatrix CreateRotationY(float radians)
    {
      GsMatrix returnMatrix = GsMatrix.Identity;

      returnMatrix.M11 = (float)Math.Cos(radians);
      returnMatrix.M13 = (float)Math.Sin(radians);
      returnMatrix.M31 = -returnMatrix.M13;
      returnMatrix.M33 = returnMatrix.M11;

      return returnMatrix;
    }

    public static GsMatrix CreateRotationZ(float radians)
    {
      GsMatrix returnMatrix = GsMatrix.Identity;

      returnMatrix.M11 = (float)Math.Cos(radians);
      returnMatrix.M12 = (float)Math.Sin(radians);
      returnMatrix.M21 = -returnMatrix.M12;
      returnMatrix.M22 = returnMatrix.M11;

      return returnMatrix;
    }

    public static GsMatrix CreateScale(float scale)
    {
      GsMatrix returnMatrix = GsMatrix.Identity;

      returnMatrix.M11 = scale;
      returnMatrix.M22 = scale;
      returnMatrix.M33 = scale;

      return returnMatrix;
    }

    public static GsMatrix Invert(GsMatrix matrix)
    {
      Invert(ref matrix, out matrix);
      return matrix;
    }


    public static void Invert(ref GsMatrix matrix, out GsMatrix result)
    {
      //
      // Use Laplace expansion theorem to calculate the inverse of a 4x4 matrix
      // 
      // 1. Calculate the 2x2 determinants needed and the 4x4 determinant based on the 2x2 determinants 
      // 2. Create the adjugate matrix, which satisfies: A * adj(A) = det(A) * I
      // 3. Divide adjugate matrix with the determinant to find the inverse

      float det1 = matrix.M11 * matrix.M22 - matrix.M12 * matrix.M21;
      float det2 = matrix.M11 * matrix.M23 - matrix.M13 * matrix.M21;
      float det3 = matrix.M11 * matrix.M24 - matrix.M14 * matrix.M21;
      float det4 = matrix.M12 * matrix.M23 - matrix.M13 * matrix.M22;
      float det5 = matrix.M12 * matrix.M24 - matrix.M14 * matrix.M22;
      float det6 = matrix.M13 * matrix.M24 - matrix.M14 * matrix.M23;
      float det7 = matrix.M31 * matrix.M42 - matrix.M32 * matrix.M41;
      float det8 = matrix.M31 * matrix.M43 - matrix.M33 * matrix.M41;
      float det9 = matrix.M31 * matrix.M44 - matrix.M34 * matrix.M41;
      float det10 = matrix.M32 * matrix.M43 - matrix.M33 * matrix.M42;
      float det11 = matrix.M32 * matrix.M44 - matrix.M34 * matrix.M42;
      float det12 = matrix.M33 * matrix.M44 - matrix.M34 * matrix.M43;

      float detMatrix = (float)(det1 * det12 - det2 * det11 + det3 * det10 + det4 * det9 - det5 * det8 + det6 * det7);

      float invDetMatrix = 1f / detMatrix;

      GsMatrix ret; // Allow for matrix and result to point to the same structure

      ret.M11 = (matrix.M22 * det12 - matrix.M23 * det11 + matrix.M24 * det10) * invDetMatrix;
      ret.M12 = (-matrix.M12 * det12 + matrix.M13 * det11 - matrix.M14 * det10) * invDetMatrix;
      ret.M13 = (matrix.M42 * det6 - matrix.M43 * det5 + matrix.M44 * det4) * invDetMatrix;
      ret.M14 = (-matrix.M32 * det6 + matrix.M33 * det5 - matrix.M34 * det4) * invDetMatrix;
      ret.M21 = (-matrix.M21 * det12 + matrix.M23 * det9 - matrix.M24 * det8) * invDetMatrix;
      ret.M22 = (matrix.M11 * det12 - matrix.M13 * det9 + matrix.M14 * det8) * invDetMatrix;
      ret.M23 = (-matrix.M41 * det6 + matrix.M43 * det3 - matrix.M44 * det2) * invDetMatrix;
      ret.M24 = (matrix.M31 * det6 - matrix.M33 * det3 + matrix.M34 * det2) * invDetMatrix;
      ret.M31 = (matrix.M21 * det11 - matrix.M22 * det9 + matrix.M24 * det7) * invDetMatrix;
      ret.M32 = (-matrix.M11 * det11 + matrix.M12 * det9 - matrix.M14 * det7) * invDetMatrix;
      ret.M33 = (matrix.M41 * det5 - matrix.M42 * det3 + matrix.M44 * det1) * invDetMatrix;
      ret.M34 = (-matrix.M31 * det5 + matrix.M32 * det3 - matrix.M34 * det1) * invDetMatrix;
      ret.M41 = (-matrix.M21 * det10 + matrix.M22 * det8 - matrix.M23 * det7) * invDetMatrix;
      ret.M42 = (matrix.M11 * det10 - matrix.M12 * det8 + matrix.M13 * det7) * invDetMatrix;
      ret.M43 = (-matrix.M41 * det4 + matrix.M42 * det2 - matrix.M43 * det1) * invDetMatrix;
      ret.M44 = (matrix.M31 * det4 - matrix.M32 * det2 + matrix.M33 * det1) * invDetMatrix;

      result = ret;
    }

    public static GsMatrix operator +(GsMatrix matrix1, GsMatrix matrix2)
    {
      matrix1.M11 += matrix2.M11;
      matrix1.M12 += matrix2.M12;
      matrix1.M13 += matrix2.M13;
      matrix1.M14 += matrix2.M14;
      matrix1.M21 += matrix2.M21;
      matrix1.M22 += matrix2.M22;
      matrix1.M23 += matrix2.M23;
      matrix1.M24 += matrix2.M24;
      matrix1.M31 += matrix2.M31;
      matrix1.M32 += matrix2.M32;
      matrix1.M33 += matrix2.M33;
      matrix1.M34 += matrix2.M34;
      matrix1.M41 += matrix2.M41;
      matrix1.M42 += matrix2.M42;
      matrix1.M43 += matrix2.M43;
      matrix1.M44 += matrix2.M44;
      return matrix1;
    }

    public static GsMatrix operator /(GsMatrix matrix1, GsMatrix matrix2)
    {
      GsMatrix inverse = GsMatrix.Invert(matrix2), result;

      result.M11 = matrix1.M11 * inverse.M11 + matrix1.M12 * inverse.M21 + matrix1.M13 * inverse.M31 + matrix1.M14 * inverse.M41;
      result.M12 = matrix1.M11 * inverse.M12 + matrix1.M12 * inverse.M22 + matrix1.M13 * inverse.M32 + matrix1.M14 * inverse.M42;
      result.M13 = matrix1.M11 * inverse.M13 + matrix1.M12 * inverse.M23 + matrix1.M13 * inverse.M33 + matrix1.M14 * inverse.M43;
      result.M14 = matrix1.M11 * inverse.M14 + matrix1.M12 * inverse.M24 + matrix1.M13 * inverse.M34 + matrix1.M14 * inverse.M44;

      result.M21 = matrix1.M21 * inverse.M11 + matrix1.M22 * inverse.M21 + matrix1.M23 * inverse.M31 + matrix1.M24 * inverse.M41;
      result.M22 = matrix1.M21 * inverse.M12 + matrix1.M22 * inverse.M22 + matrix1.M23 * inverse.M32 + matrix1.M24 * inverse.M42;
      result.M23 = matrix1.M21 * inverse.M13 + matrix1.M22 * inverse.M23 + matrix1.M23 * inverse.M33 + matrix1.M24 * inverse.M43;
      result.M24 = matrix1.M21 * inverse.M14 + matrix1.M22 * inverse.M24 + matrix1.M23 * inverse.M34 + matrix1.M24 * inverse.M44;

      result.M31 = matrix1.M31 * inverse.M11 + matrix1.M32 * inverse.M21 + matrix1.M33 * inverse.M31 + matrix1.M34 * inverse.M41;
      result.M32 = matrix1.M31 * inverse.M12 + matrix1.M32 * inverse.M22 + matrix1.M33 * inverse.M32 + matrix1.M34 * inverse.M42;
      result.M33 = matrix1.M31 * inverse.M13 + matrix1.M32 * inverse.M23 + matrix1.M33 * inverse.M33 + matrix1.M34 * inverse.M43;
      result.M34 = matrix1.M31 * inverse.M14 + matrix1.M32 * inverse.M24 + matrix1.M33 * inverse.M34 + matrix1.M34 * inverse.M44;

      result.M41 = matrix1.M41 * inverse.M11 + matrix1.M42 * inverse.M21 + matrix1.M43 * inverse.M31 + matrix1.M44 * inverse.M41;
      result.M42 = matrix1.M41 * inverse.M12 + matrix1.M42 * inverse.M22 + matrix1.M43 * inverse.M32 + matrix1.M44 * inverse.M42;
      result.M43 = matrix1.M41 * inverse.M13 + matrix1.M42 * inverse.M23 + matrix1.M43 * inverse.M33 + matrix1.M44 * inverse.M43;
      result.M44 = matrix1.M41 * inverse.M14 + matrix1.M42 * inverse.M24 + matrix1.M43 * inverse.M34 + matrix1.M44 * inverse.M44;

      return result;
    }

    public static GsMatrix operator /(GsMatrix matrix1, float divider)
    {
      float inverseDivider = 1.0f / divider;

      matrix1.M11 = matrix1.M11 * inverseDivider;
      matrix1.M12 = matrix1.M12 * inverseDivider;
      matrix1.M13 = matrix1.M13 * inverseDivider;
      matrix1.M14 = matrix1.M14 * inverseDivider;
      matrix1.M21 = matrix1.M21 * inverseDivider;
      matrix1.M22 = matrix1.M22 * inverseDivider;
      matrix1.M23 = matrix1.M23 * inverseDivider;
      matrix1.M24 = matrix1.M24 * inverseDivider;
      matrix1.M31 = matrix1.M31 * inverseDivider;
      matrix1.M32 = matrix1.M32 * inverseDivider;
      matrix1.M33 = matrix1.M33 * inverseDivider;
      matrix1.M34 = matrix1.M34 * inverseDivider;
      matrix1.M41 = matrix1.M41 * inverseDivider;
      matrix1.M42 = matrix1.M42 * inverseDivider;
      matrix1.M43 = matrix1.M43 * inverseDivider;
      matrix1.M44 = matrix1.M44 * inverseDivider;

      return matrix1;
    }

    public static bool operator ==(GsMatrix matrix1, GsMatrix matrix2)
    {
      return (matrix1.M11 == matrix2.M11) && (matrix1.M12 == matrix2.M12) &&
             (matrix1.M13 == matrix2.M13) && (matrix1.M14 == matrix2.M14) &&
             (matrix1.M21 == matrix2.M21) && (matrix1.M22 == matrix2.M22) &&
             (matrix1.M23 == matrix2.M23) && (matrix1.M24 == matrix2.M24) &&
             (matrix1.M31 == matrix2.M31) && (matrix1.M32 == matrix2.M32) &&
             (matrix1.M33 == matrix2.M33) && (matrix1.M34 == matrix2.M34) &&
             (matrix1.M41 == matrix2.M41) && (matrix1.M42 == matrix2.M42) &&
             (matrix1.M43 == matrix2.M43) && (matrix1.M44 == matrix2.M44);
    }

    public static bool operator !=(GsMatrix matrix1, GsMatrix matrix2)
    {
      return (matrix1.M11 != matrix2.M11) || (matrix1.M12 != matrix2.M12) ||
             (matrix1.M13 != matrix2.M13) || (matrix1.M14 != matrix2.M14) ||
             (matrix1.M21 != matrix2.M21) || (matrix1.M22 != matrix2.M22) ||
             (matrix1.M23 != matrix2.M23) || (matrix1.M24 != matrix2.M24) ||
             (matrix1.M31 != matrix2.M31) || (matrix1.M32 != matrix2.M32) ||
             (matrix1.M33 != matrix2.M33) || (matrix1.M34 != matrix2.M34) ||
             (matrix1.M41 != matrix2.M41) || (matrix1.M42 != matrix2.M42) ||
             (matrix1.M43 != matrix2.M43) || (matrix1.M44 != matrix2.M44);
    }

    public static GsMatrix operator *(GsMatrix matrix1, GsMatrix matrix2)
    {
      GsMatrix result;

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

    public static GsMatrix operator *(GsMatrix matrix, float scaleFactor)
    {
      matrix.M11 = matrix.M11 * scaleFactor;
      matrix.M12 = matrix.M12 * scaleFactor;
      matrix.M13 = matrix.M13 * scaleFactor;
      matrix.M14 = matrix.M14 * scaleFactor;
      matrix.M21 = matrix.M21 * scaleFactor;
      matrix.M22 = matrix.M22 * scaleFactor;
      matrix.M23 = matrix.M23 * scaleFactor;
      matrix.M24 = matrix.M24 * scaleFactor;
      matrix.M31 = matrix.M31 * scaleFactor;
      matrix.M32 = matrix.M32 * scaleFactor;
      matrix.M33 = matrix.M33 * scaleFactor;
      matrix.M34 = matrix.M34 * scaleFactor;
      matrix.M41 = matrix.M41 * scaleFactor;
      matrix.M42 = matrix.M42 * scaleFactor;
      matrix.M43 = matrix.M43 * scaleFactor;
      matrix.M44 = matrix.M44 * scaleFactor;
      return matrix;
    }

    public static GsMatrix operator *(float scaleFactor, GsMatrix matrix)
    {
      matrix.M11 = matrix.M11 * scaleFactor;
      matrix.M12 = matrix.M12 * scaleFactor;
      matrix.M13 = matrix.M13 * scaleFactor;
      matrix.M14 = matrix.M14 * scaleFactor;
      matrix.M21 = matrix.M21 * scaleFactor;
      matrix.M22 = matrix.M22 * scaleFactor;
      matrix.M23 = matrix.M23 * scaleFactor;
      matrix.M24 = matrix.M24 * scaleFactor;
      matrix.M31 = matrix.M31 * scaleFactor;
      matrix.M32 = matrix.M32 * scaleFactor;
      matrix.M33 = matrix.M33 * scaleFactor;
      matrix.M34 = matrix.M34 * scaleFactor;
      matrix.M41 = matrix.M41 * scaleFactor;
      matrix.M42 = matrix.M42 * scaleFactor;
      matrix.M43 = matrix.M43 * scaleFactor;
      matrix.M44 = matrix.M44 * scaleFactor;
      return matrix;
    }

    public static GsMatrix operator -(GsMatrix matrix1, GsMatrix matrix2)
    {
      matrix1.M11 -= matrix2.M11;
      matrix1.M12 -= matrix2.M12;
      matrix1.M13 -= matrix2.M13;
      matrix1.M14 -= matrix2.M14;
      matrix1.M21 -= matrix2.M21;
      matrix1.M22 -= matrix2.M22;
      matrix1.M23 -= matrix2.M23;
      matrix1.M24 -= matrix2.M24;
      matrix1.M31 -= matrix2.M31;
      matrix1.M32 -= matrix2.M32;
      matrix1.M33 -= matrix2.M33;
      matrix1.M34 -= matrix2.M34;
      matrix1.M41 -= matrix2.M41;
      matrix1.M42 -= matrix2.M42;
      matrix1.M43 -= matrix2.M43;
      matrix1.M44 -= matrix2.M44;
      return matrix1;
    }

    public static GsMatrix operator -(GsMatrix matrix)
    {
      matrix.M11 = -matrix.M11;
      matrix.M12 = -matrix.M12;
      matrix.M13 = -matrix.M13;
      matrix.M14 = -matrix.M14;
      matrix.M21 = -matrix.M21;
      matrix.M22 = -matrix.M22;
      matrix.M23 = -matrix.M23;
      matrix.M24 = -matrix.M24;
      matrix.M31 = -matrix.M31;
      matrix.M32 = -matrix.M32;
      matrix.M33 = -matrix.M33;
      matrix.M34 = -matrix.M34;
      matrix.M41 = -matrix.M41;
      matrix.M42 = -matrix.M42;
      matrix.M43 = -matrix.M43;
      matrix.M44 = -matrix.M44;
      return matrix;
    }
  }
}
