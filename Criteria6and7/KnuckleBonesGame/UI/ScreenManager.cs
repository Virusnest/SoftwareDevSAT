using System.Numerics;
using Foster.Framework;
using KnuckleBonesGame.UI.Widgets;
using KnuckleBonesGame.Util.Math;
using KnuckleBonesGame.Util.Math.Extentions;


namespace KnuckleBonesGame.UI;

/// <summary>
/// Manages different screens and handles scaling and rendering
/// </summary>
public class ScreenManager {
  private int _virtualScale = 1;
  public Matrix4x4 ProjectionMatrix;
  public int UIScale = 4;
  public Matrix4x4 ViewMatrix;
  private float virtualWidth, virtualHeight;
  public float Width, Height;


  public void Pause(Screen From, bool pause = true) {
    SetAllExcept(CurrentScreen, w => w == From, pause);
  }

  void SetAllExcept(Widget root, Predicate<Widget> excludePredicate, bool value) {
    if (excludePredicate(root))
      return;

    root.InputBlocked = value;

    foreach (var child in root.Children) {
      SetAllExcept(child, excludePredicate, value);
    }
  }

  public ScreenManager(float width, float height) {
    Width = width;
    Height = height;
    Resize(width, height);
  }

  public Screen? CurrentScreen { get; private set; }
  public MatrixStack MatrixStack { get; } = new();

  public void Resize(float width, float height) {
    _virtualScale = CalcualteUIScale(UIScale, (int)width, (int)height);
    Width = width;
    Height = height;
    virtualHeight = (int)Height / _virtualScale;
    virtualWidth = (int)Width / _virtualScale;
    if (CurrentScreen != null) CurrentScreen.Size = new Vector2(virtualWidth, virtualHeight);
    ViewMatrix = Matrix4x4.CreateScale(_virtualScale + 0.001f);
  }


  public void SetScreen(Screen? screen) {
    CurrentScreen = screen;
    if (!screen?.hasInitialized ?? false) {
      CurrentScreen?.Initialize();
      screen.hasInitialized = true;
    }

    if (CurrentScreen != null) CurrentScreen.Size = new Vector2(virtualWidth, virtualHeight);
  }

  public void Update() {
    MatrixStack.Clear();
    CurrentScreen?.WidgetUpdate(MatrixStack, ref _virtualScale);
  }

  public void UpdateUIScale(int scale) {
    UIScale = scale;
    _virtualScale = CalcualteUIScale(scale, (int)Width, (int)Height);
    virtualHeight = (int)Height / _virtualScale;
    virtualWidth = (int)Width / _virtualScale;
    if (CurrentScreen != null) CurrentScreen.Size = new Vector2(virtualWidth, virtualHeight);
    ViewMatrix = Matrix4x4.CreateScale(_virtualScale + 0.001f);
  }

  
  private int CalcualteUIScale(int scale, int Width, int Height, int minWidth = 400, int minHeight = 300) {
    int i;
    for (i = 1; i != scale && Width / (i + 1) > minWidth && Width / (i + 1) > minHeight; i++) ;
    return i;
  }

  public void Draw(float delta) {
    MatrixStack.Clear();
    CurrentScreen?.WidgetRender(MatrixStack, delta, ref _virtualScale);
    float inv = 1f / (_virtualScale + 0.001f);
    SystemRegistry.Batcher.Render(Program.Game.Window,
      Matrix4x4.CreateOrthographicOffCenter(0, Width * inv, Height * inv, 0, -1, 1));
    SystemRegistry.Batcher.Clear();
  }
}