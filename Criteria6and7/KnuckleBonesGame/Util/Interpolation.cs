using System;
using System.Numerics;

namespace KnuckleBonesGame.Util;

public static class Interpolation {

  public static float CubicBezier(float t, float x1, float y1, float x2, float y2) {
    float u = 1.0f - t;
    float tt = t * t;
    float uu = u * u;

    // Bernstein polynomial form for cubic bezier (fixed P0=(0,0), P3=(1,1))
    float y =
      3 * uu * t * y1 +     // P1
      3 * u * tt * y2 +     // P2
      tt * t;               // P3 (1.0)

    return y;
  }

  public static float InterpolateWithEasing(float t, float a, float b, EasingFunction easingFunction) {
    return a + (b - a) * easingFunction(t);
  }
  public static Vector2 InterpolateWithEasing(Vector2 a, Vector2 b, float t, EasingFunction easingFunction) {
    return new Vector2(
      InterpolateWithEasing(t, a.X, b.X, easingFunction),
      InterpolateWithEasing(t, a.Y, b.Y, easingFunction)
    );
  }
  public static void Vector2InterpolateWithEasing(ref Vector2 a, ref Vector2 b, float t, EasingFunction easingFunction) {
    a.X = InterpolateWithEasing(t, a.X, b.X, easingFunction);
    a.Y = InterpolateWithEasing(t, a.Y, b.Y, easingFunction);

  }

}

public static class StandardEasings {
  public static EasingFunction Linear = a => a;
  public static EasingFunction EaseInSine = t =>
    1f - MathF.Cos(t * MathF.PI / 2f);

  public static EasingFunction EaseOutSine = t => MathF.Sin(t * MathF.PI / 2f);
  public static EasingFunction EaseInOutSine = t => -(MathF.Cos(MathF.PI * t) - 1f) / 2f;
  public static EasingFunction EaseInQuad = t => t * t;
  public static EasingFunction EaseOutQuad = t => 1f - (1f - t) * (1f - t);

  public static EasingFunction EaseInOutQuad = t => t < 0.5f ? 2f * t * t : 1f - MathF.Pow(-2f * t + 2f, 2f) / 2f;

  public static EasingFunction EaseInCubic = t => t * t * t;
  public static EasingFunction EaseOutCubic = t => 1f - MathF.Pow(1f - t, 3f);

  public static EasingFunction EaseInOutCubic = t => t < 0.5f ? 4f * t * t * t : 1f - MathF.Pow(-2f * t + 2f, 3f) / 2f;

  public static EasingFunction EaseInQuart = t => t * t * t * t;
  public static EasingFunction EaseOutQuart = t => 1f - MathF.Pow(1f - t, 4f);

  public static EasingFunction EaseInOutQuart =
    t => t < 0.5f ? 8f * t * t * t * t : 1f - MathF.Pow(-2f * t + 2f, 4f) / 2f;

  public static EasingFunction EaseInQuint = t => t * t * t * t * t;
  public static EasingFunction EaseOutQuint = t => 1f - MathF.Pow(1f - t, 5f);

  public static EasingFunction EaseInOutQuint =
    t => t < 0.5f ? 16f * t * t * t * t * t : 1f - MathF.Pow(-2f * t + 2f, 5f) / 2f;

  public static EasingFunction EaseInExpo = t => t == 0f ? 0f : MathF.Pow(2f, 10f * t - 10f);
  public static EasingFunction EaseOutExpo = t => t == 1f ? 1f : 1f - MathF.Pow(2f, -10f * t);

  public static EasingFunction EaseInOutExpo = t => t == 0f ? 0f
    : t == 1f ? 1f
    : t < 0.5f ? MathF.Pow(2f, 20f * t - 10f) / 2f
    : (2f - MathF.Pow(2f, -20f * t + 10f)) / 2f;

  public static EasingFunction EaseInCirc = t => 1f - MathF.Sqrt(1f - t * t);
  public static EasingFunction EaseOutCirc = t => MathF.Sqrt(1f - MathF.Pow(t - 1f, 2f));

  public static EasingFunction EaseInOutCirc = t =>
    t < 0.5f
      ? (1f - MathF.Sqrt(1f - 4f * t * t)) / 2f
      : (MathF.Sqrt(1f - MathF.Pow(-2f * t + 2f, 2f)) + 1f) / 2f;

  public static EasingFunction EaseInBack = t => 2.70158f * t * t * t - 1.70158f * t * t;

  public static EasingFunction EaseOutBack =
    t => 1f + 2.70158f * MathF.Pow(t - 1f, 3f) + 1.70158f * MathF.Pow(t - 1f, 2f);

  public static EasingFunction EaseInOutBack = t =>
    t < 0.5f
      ? MathF.Pow(2f * t, 2f) * ((2.5949095f + 1f) * 2f * t - 2.5949095f) / 2f
      : (MathF.Pow(2f * t - 2f, 2f) * ((2.5949095f + 1f) * (t * 2f - 2f) + 2.5949095f) + 2f) / 2f;
}

public static class MaterialEasings {
  public static EasingFunction ExpressiveFastSpatial = t => Interpolation.CubicBezier(t, 0.42f, 1.67f, 0.21f, 0.90f);
  public static EasingFunction ExpressiveDefaultSpatial = t => Interpolation.CubicBezier(t, 0.38f, 1.21f, 0.22f, 1.00f);
  public static EasingFunction ExpressiveSlowSpatial = t => Interpolation.CubicBezier(t, 0.39f, 1.29f, 0.35f, 0.98f);
  public static EasingFunction ExpressiveFastEffects = t => Interpolation.CubicBezier(t, 0.31f, 0.94f, 0.34f, 1.00f);
  public static EasingFunction ExpressiveDefaultEffects = t => Interpolation.CubicBezier(t, 0.34f, 0.80f, 0.34f, 1.00f);
  public static EasingFunction ExpressiveSlowEffects = t => Interpolation.CubicBezier(t, 0.34f, 0.88f, 0.34f, 1.00f);
  public static EasingFunction StandardFastSpatial = t => Interpolation.CubicBezier(t, 0.27f, 1.06f, 0.18f, 1.00f);
  public static EasingFunction StandardDefaultSpatial = t => Interpolation.CubicBezier(t, 0.27f, 1.06f, 0.18f, 1.00f);
  public static EasingFunction StandardSlowSpatial = t => Interpolation.CubicBezier(t, 0.27f, 1.06f, 0.18f, 1.00f);
  public static EasingFunction StandardFastEffects = t => Interpolation.CubicBezier(t, 0.31f, 0.94f, 0.34f, 1.00f);
  public static EasingFunction StandardDefaultEffects = t => Interpolation.CubicBezier(t, 0.34f, 0.80f, 0.34f, 1.00f);
  public static EasingFunction StandardSlowEffects = t => Interpolation.CubicBezier(t, 0.34f, 0.88f, 0.34f, 1.00f);
  public static EasingFunction StandardDecelerate = t => Interpolation.CubicBezier(t, 0.2f, 0.0f, 0f, 1.0f);
  public static EasingFunction StandardAccelerate = t => Interpolation.CubicBezier(t, 0f, 0f, 0f, 1f);
  public static EasingFunction StandardEaseInOut = t => Interpolation.CubicBezier(t, 0.3f, 0f, 1f, 1f);
  public static EasingFunction EmphasizedDecelerate = t => Interpolation.CubicBezier(t, 0.05f, 0.7f, 0.1f, 1.0f);
  public static EasingFunction EmphasizedAccelerate = t => Interpolation.CubicBezier(t, 0.3f, 0.0f, 0.8f, 0.15f);
}
public delegate float EasingFunction(float a);

public static class EasingExt {
  public static float ToEasingFunction(this string EasingFunction) {

    return 0;
  }

}

