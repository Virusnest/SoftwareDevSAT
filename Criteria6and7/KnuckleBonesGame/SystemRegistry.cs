using Foster.Framework;
using KnuckleBonesGame.UI;

namespace KnuckleBonesGame;

public static class SystemRegistry {
  
  public static Input? Input;
  public static Rng Rng;
  public static Controls? Controls;
  public static readonly ScreenManager ScreenManager;
  public static AssetManager AssetManager;
  public static GraphicsDevice? GraphicsDevice;
  public static Batcher? Batcher;
  public static SoundSystem SoundSystem;
  static SystemRegistry() {
    ScreenManager = new ScreenManager(800, 600);
    SoundSystem = new SoundSystem();
    Rng = new Rng(DateTime.Now.Millisecond);
  }
  
  
}