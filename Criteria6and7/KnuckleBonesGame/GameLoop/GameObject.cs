using System.Numerics;
using Foster.Framework;

namespace KnuckleBonesGame.GameLoop;

public class GameObject {
  public Vector2 Position;
  public Mesh? Mesh;
  public Material? Material;
  public float Rotation;
  public bool IsActive = true;
  public bool IsVisible = true;
}