namespace KnuckleBonesGame;

public class SceneManager
{
  public Scene? CurrentScene { get; private set; }

  public void SetScene(Scene scene)
  {
    CurrentScene?.Dispose();
    CurrentScene = scene;
    CurrentScene.Init();
  }

  public void Update(double deltaTime)
  {
    CurrentScene?.Update(deltaTime);
  }

  public void FixedUpdate(double deltaTime)
  {
    CurrentScene?.FixedUpdate(deltaTime);
  }

  public void Render(double deltaTime)
  {
    CurrentScene?.Render(deltaTime);
  }

  public void Resize(int width, int height)
  {
    CurrentScene?.Resize(width, height);
  }

  public T? GetScene<T>() where T : Scene
  {
    if (CurrentScene is T scene) return scene;
    return null;
  }
}