namespace KnuckleBonesGame.Util;

public class Timer {
  public float Time { get; private set; }
  private float _updateInterval;
  private float _accumulator;
  private bool _compleated=true;
  

  public Action? Tick;
  public Action? Complete;
  

  public void Update(float deltaTime) {
    if (_compleated) return;
    if (_updateInterval <= 0) return;
    _accumulator += deltaTime;
    while (_accumulator >= _updateInterval) {
      _accumulator -= _updateInterval;
      Time -= _updateInterval;
      Tick?.Invoke();
    }
    if (Time<=0) {
      Complete?.Invoke();
      _compleated = true;
    }

  }
  public void Start(float time, float updateInterval = 0.1f) {
    _compleated = false;
    Time = time;
    _updateInterval = updateInterval;
    _accumulator = 0;
  }
  public bool IsComplete => _compleated;
}