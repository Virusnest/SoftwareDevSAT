using System.Numerics;
using Foster.Framework;

namespace KnuckleBonesGame.GameLoop;

public abstract class GameObject {
  public Vector2 Position;
  public Mesh? Mesh;
  public Material? Material;
  public float Rotation;
  public bool IsActive = true;
  public bool IsVisible = true;

  public abstract void Update(float delta);
  public abstract void Draw(float delta);

  public abstract void Spawn();
}