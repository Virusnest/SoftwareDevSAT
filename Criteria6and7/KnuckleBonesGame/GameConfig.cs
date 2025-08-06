namespace KnuckleBonesGame;

public static class GameConfig {
  public static WatchedVariable<int> UIScale;
  public static WatchedVariable<bool> VSync;
  public static WatchedVariable<bool> FullScreen;
  public static WatchedVariable<bool> ShowFPS;
  public static WatchedVariable<float> Volume;

  static GameConfig() {
    UIScale.Value = 4;
    VSync.Value = true;
    FullScreen.Value = false;
    ShowFPS.Value = false;
    Volume.Value = 1.0f;
    UIScale.OnValueChanged += value => SystemRegistry.ScreenManager.UpdateUIScale(value);
    VSync.OnValueChanged += value => SystemRegistry.GraphicsDevice.VSync= value;
    FullScreen.OnValueChanged += value => SystemRegistry.GraphicsDevice.App.Window.Fullscreen= value;
    ShowFPS.OnValueChanged += value => Console.WriteLine($"ShowFPS changed to {value}");
    Volume.OnValueChanged += value => Console.WriteLine($"Volume changed to {value}");
    
  } 

}

public struct WatchedVariable<T> {
  private T _value;

  public T Value {
    get => _value;
    set {
      if (!EqualityComparer<T>.Default.Equals(_value, value)) {
        _value = value;
        OnValueChanged?.Invoke(value);
      }
    }
  }

  public event Action<T> OnValueChanged;
  public static implicit operator T(WatchedVariable<T> variable) {
    return variable.Value;
  }

  public WatchedVariable(T value=default) {
    Value = value;
    OnValueChanged?.Invoke(value);
  }
  

  public override string ToString() {
    return Value.ToString();
  }
}