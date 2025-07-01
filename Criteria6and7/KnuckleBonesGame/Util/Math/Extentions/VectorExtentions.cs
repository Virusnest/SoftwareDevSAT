using System.Drawing;
using System.Numerics;

namespace KnuckleBonesGame.Util.Math.Extentions;

public static class VectorExtentions {
  public static Vector2 Transformed(this Vector2 pos, Matrix4x4 mat) {
    return new Vector2(
      pos.X * mat.M11 + pos.Y * mat.M21 + mat.M31 + mat.M41,
      pos.X * mat.M12 + pos.Y * mat.M22 + mat.M32 + mat.M42
    );
  }

  public static Vector2 Floor(this Vector2 pos) {
    return new Vector2(MathF.Floor(pos.X), MathF.Floor(pos.Y));
  }

  public static Vector2 Ceil(this Vector2 pos) {
    return new Vector2(MathF.Floor(pos.X), MathF.Floor(pos.Y));
  }

  public static Vector2 Round(this Vector2 pos) {
    return new Vector2(MathF.Round(pos.X), MathF.Round(pos.Y));
  }

  public static Vector3 ToVector3(this Color color) {
    return new Vector3((float)color.R / 255, (float)color.G / 255, (float)color.B / 255);
  }

  public static Vector3 ScaledFloor(this Vector3 pos, float scale) {
    return new Vector3(MathF.Floor(pos.X * scale) / scale, MathF.Floor(pos.Y * scale) / scale,
      MathF.Floor(pos.Z * scale) / scale);
  }

  public static Vector2 ScaledFloor(this Vector2 pos, float scale) {
    return new Vector2(MathF.Floor(pos.X * scale) / scale, MathF.Floor(pos.Y * scale) / scale);
  }
  public static float Angle(this Vector2 pos) {
    return MathF.Atan2(pos.Y, pos.X);
  }
  public static Vector2 Rotated(this Vector2 pos, float angle) {
    var cos = MathF.Cos(angle);
    var sin = MathF.Sin(angle);
    return new Vector2(
      pos.X * cos - pos.Y * sin,
      pos.X * sin + pos.Y * cos
    );
  }
}