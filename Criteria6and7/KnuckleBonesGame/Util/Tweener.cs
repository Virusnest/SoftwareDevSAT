namespace KnuckleBonesGame.Util;

public class Tweener {
  private readonly List<Tween> _tweens = new();

  public void TweenVal(float startValue, float endValue, float duration, EasingFunction easingFunction,
    Action<float> setter,
    Action? onComplete = null,
    bool flip = true,
    int loopCount = 0,
    float delay = 0
  ) {
    var tween = new Tween {
      StartValue = startValue,
      EndValue = endValue,
      Flip = flip,
      LoopCount = loopCount,
      Duration = duration,
      EasingFunction = easingFunction,
      Setter = setter,
      OnComplete = onComplete,
      Delay = delay
    };
    _tweens.Add(tween);
  }

  public void TweenStaggered<T>(
    IList<T> items,
    Func<T, float> getStart,
    Func<T, float> getEnd,
    float duration,
    float delayStep,
    EasingFunction easing,
    Action<T, float> setter,
    float delayOffset = 0
  ) {
    for (var i = 0; i < items.Count; i++) {
      var item = items[i];
      var startValue = getStart(item);
      var endValue = getEnd(item);
      var delay = delayStep * i + delayOffset;
      TweenVal(startValue, endValue, duration, easing, x => setter(item, x), null, false, 0, delay);
    }
  }


  public void Update(double delta) {
    if (_tweens.Count == 0) return;

    for (var i = _tweens.Count - 1; i >= 0; i--) {
      var tween = _tweens[i];
      tween.ElapsedTime += (float)delta;
      if (tween.ElapsedTime >= tween.Duration + tween.Delay) {
        if (tween.LoopCount > 0) {
          tween.LoopCount--;
          if (tween.Flip) (tween.StartValue, tween.EndValue) = (tween.EndValue, tween.StartValue);
          tween.ElapsedTime = tween.Delay;
        }
        else if (tween.LoopCount < 0) {
          if (tween.Flip) (tween.StartValue, tween.EndValue) = (tween.EndValue, tween.StartValue);
          tween.ElapsedTime = tween.Delay;
        }
        else {
          tween.ElapsedTime = tween.Duration + tween.Delay;
          tween.OnComplete?.Invoke();
          _tweens.RemoveAt(i);
        }
      }


      var t = (tween.ElapsedTime - tween.Delay) / tween.Duration;
      t = System.Math.Clamp(t, 0f, 1f);
      var value = tween.StartValue + (tween.EndValue - tween.StartValue) * tween.EasingFunction(t);

      tween.Setter(value);
    }
  }

  public void Clear() {
    _tweens.Clear();
  }

  private class Tween {
    public float Delay;
    public float Duration;
    public EasingFunction? EasingFunction;
    public float ElapsedTime;
    public bool Flip;
    public Func<float>? Getter;
    public int LoopCount;
    public Action? OnComplete;
    public Action<float>? Setter;
    public float StartValue, EndValue;
  }
}