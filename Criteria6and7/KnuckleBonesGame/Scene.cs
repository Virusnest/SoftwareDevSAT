namespace KnuckleBonesGame;

public abstract class Scene
{
  public abstract void Init();
  public abstract void Update(double deltaTime);

  public abstract void FixedUpdate(double deltaTime);
  public abstract void Render(double deltaTime);
  public abstract void Dispose();
  public abstract void Resize(int width, int height);
}