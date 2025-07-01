using System.Numerics;

namespace KnuckleBonesGame.Util.Math.Extentions;

public static class MatrixExtentions {
  public static Matrix3x2 ToMatrix3x2(this Matrix4x4 matrix) {
    return new Matrix3x2(
      matrix.M11, matrix.M12, matrix.M21, matrix.M22,
      matrix.M41, matrix.M42
    );
  }
  
}