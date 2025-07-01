using Foster.Framework;
using KnuckleBonesGame.UI;

namespace KnuckleBonesGame;

public static class SystemRegistry {
  
  public static Input? Input;
  public static Controls? Controls;
  public static readonly SceneManager SceneManager;
  public static readonly ScreenManager ScreenManager;
  public static AssetManager AssetManager;
  public static GraphicsDevice? GraphicsDevice;
  public static Batcher? Batcher;
  static SystemRegistry() {
    ScreenManager = new ScreenManager(800, 600);
    SceneManager = new SceneManager();
  }
  
  
}