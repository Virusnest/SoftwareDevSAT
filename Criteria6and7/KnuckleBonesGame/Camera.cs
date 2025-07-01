using System.Numerics;
using Foster.Framework;

public class Camera(GraphicsDevice graphicsDevice, Controls controls)
{
  public GraphicsDevice GraphicsDevice { get; } = graphicsDevice;
  public Controls Controls { get; } = controls;

  // Camera properties
  public Vector2 Position { get; set; } = Vector2.Zero;
  public float Zoom { get; set; } = 1.0f;
  public float Rotation { get; set; } = 0.0f;

  // View matrix for rendering
  public Matrix4x4 ViewMatrix => GetViewMatrix();
  public Matrix4x4 GetViewMatrix()
  {

    return Matrix4x4.CreateTranslation(-new Vector3(Position, 0)) *
           Matrix4x4.CreateRotationX(Rotation) *
           Matrix4x4.CreateScale(Zoom);
  }
  public void Update()
  {
    // Update camera position based on controls
    var MoveVector = Controls.Move.Value;
    if (MoveVector != Vector2.Zero)
    {
      Position += MoveVector * 0.5f; // Move speed
    }
  }
}