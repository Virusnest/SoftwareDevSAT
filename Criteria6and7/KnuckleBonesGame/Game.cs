
using System.Numerics;
using Foster.Framework;
using KnuckleBonesGame.UI.Screens;
using KnuckleBonesGame.Util.Math.Extentions;

namespace KnuckleBonesGame;

public class Game : App
{ 
  public static Camera Camera;
  public static Controls Controls;
  public static Batcher Batcher => SystemRegistry.Batcher;
  public static Vector2 MousePosition;

  public static RenderMatrix Matrix;

  public struct RenderMatrix {
    public Matrix4x4 ViewMatrix;
    public Matrix4x4 ProjectionMatrix;
    public Matrix4x4 ViewProjectionMatrix => ViewMatrix * ProjectionMatrix;
    public Matrix3x2 ViewProjectionMatrix2D => ViewProjectionMatrix.ToMatrix3x2();
    public RenderMatrix(Matrix4x4 view, Matrix4x4 projection)
    {
      ViewMatrix = view;
       ProjectionMatrix = projection;
      
    }
    
    
  }
  public Game(string name, int width, int height) : base(name, width, height)
  {
    // Initialize the batcher for rendering
    Controls = new Controls(Input);
    Camera = new Camera(GraphicsDevice, Controls);
    Window.OnResize += Window_OnResize;

  }
  
  private void Window_OnResize()
  {
    // Update the camera and screen manager on window resize
    SystemRegistry.ScreenManager.Resize(Window.Width, Window.Height);
    SystemRegistry.SceneManager.Resize(Window.Width, Window.Height);
  }

  protected override void Render()
  {
   
    Window.Clear(Color.FromHexStringRGB("2E2B27"));
    SystemRegistry.SceneManager.Render(Time.Delta);
    Batcher.Render(Window);
    
    SystemRegistry.Batcher?.Clear();
    SystemRegistry.ScreenManager.Draw(Time.Delta);
    Batcher.Render(Window,Matrix.ViewProjectionMatrix);
    Batcher.Render(Window);
    Batcher.Clear();
  }

  protected override void Shutdown()
  {
  }

  protected override void Startup()
  {
    Window_OnResize();
    SystemRegistry.GraphicsDevice = GraphicsDevice;
    SystemRegistry.Input = Input;
    SystemRegistry.Controls = new Controls(Input);
    SystemRegistry.Batcher = new Batcher(GraphicsDevice);
    SystemRegistry.AssetManager=new AssetManager();
    SystemRegistry.ScreenManager.SetScreen(new TitleScreen());

  }

  protected override void Update()
  {
    SystemRegistry.ScreenManager.Update();
    SystemRegistry.SceneManager.Update(Time.Delta);
    MousePosition= Input.Mouse.Position;
    // Update the camera based on controls 
    Camera.Update();
  }
}