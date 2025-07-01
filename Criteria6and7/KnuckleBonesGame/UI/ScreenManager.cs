using System.Numerics;
using Foster.Framework;
using KnuckleBonesGame.Util.Math;
using KnuckleBonesGame.Util.Math.Extentions;


namespace KnuckleBonesGame.UI;

public class ScreenManager
{
  private int _virtualScale = 1;
  public Matrix4x4 ProjectionMatrix;
  public int UIScale = 4;
  public Matrix4x4 ViewMatrix;
  private float virtualWidth, virtualHeight;
  public float Width, Height;

  public ScreenManager(float width, float height)
  {
    Width = width;
    Height = height;
    Resize(width, height);
  }

  public Screen? CurrentScreen { get; private set; }
  public MatrixStack MatrixStack { get; } = new();

  public void Resize(float width, float height)
  {
    _virtualScale= CalcualteUIScale(UIScale, (int)width, (int)height);
    Width = width;
    Height = height;
    virtualHeight = (int)Height / _virtualScale;
    virtualWidth = (int)Width / _virtualScale;
    if (CurrentScreen != null) CurrentScreen.Size = new Vector2(virtualWidth, virtualHeight);
    ViewMatrix = Matrix4x4.CreateScale(_virtualScale + 0.001f);

    Console.WriteLine(_virtualScale);
    Console.WriteLine($"{Width}, {Height}");
  }

  public void SetScreen(Screen? screen)
  {
    CurrentScreen = screen;
    CurrentScreen?.Initialize();
    if (CurrentScreen != null) CurrentScreen.Size = new Vector2(virtualWidth, virtualHeight);
  }
  public void Update()
  {
    MatrixStack.Clear();
    CurrentScreen?.WidgetUpdate(MatrixStack, ref _virtualScale);
  }

  private int CalcualteUIScale(int scale,int Width,int Height, int minWidth = 400, int minHeight = 300)
  {
    int i;
    for (i = 1; i != scale && Width / (i + 1) > minWidth && Width / (i + 1) > minHeight; i++) ;
    return i;

  }

  public void Draw(float delta)
  {
    MatrixStack.Clear();
    CurrentScreen?.WidgetRender(MatrixStack, delta, ref _virtualScale);
    float inv = 1f/(_virtualScale+ 0.001f);
    SystemRegistry.Batcher.Render(Program.Game.Window,Matrix4x4.CreateOrthographicOffCenter(0, Width*inv, Height*inv, 0, -1, 1));
    SystemRegistry.Batcher.Clear();
  }
}