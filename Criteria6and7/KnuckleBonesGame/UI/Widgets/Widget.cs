
using System.Numerics;
using Foster.Framework;
using KnuckleBonesGame.Util.Math;
using KnuckleBonesGame.Util.Math.Extentions;

namespace KnuckleBonesGame.UI.Widgets;

public abstract class Widget
{
  public Anchor Anchor = Anchor.TopLeft; // The anchor point of the widget
  public Color BackgroundColour = Color.White; // The background color of the widget
  public List<Widget> Children = []; // List of child widgets

  public Color Color = Color.Black; // The color of the widget
  public bool IsEnabled = true; // Flag to determine if the widget is enabled
  public bool IsVisible = true; // Flag to determine if the widget is visible
  public float Opacity; // The opacity of the widget (0.0 to 1.0)
  public Widget? Parent; // The parent widget
  public Vector2 Position;
  public Vector2 Size; // The size of the widget
  public abstract void Render(MatrixStack matrixStack, float delta); // Abstract method to render the widget
  public abstract void Update(Vector2 mousePos); // Abstract method to update the widget state
  public abstract void HandleInput(); // Abstract method to handle input events

  private Vector2 GetOffset()
  {
    // Calculate the position based on the anchor point and size
    if (Parent == null) return new Vector2(0, 0); // If no parent, return zero offset
    var parentSize = Parent?.Size ?? new Vector2(0, 0); // Get the size of the parent widget

    return Anchor switch
    {
      Anchor.TopLeft => new Vector2(0, 0),
      Anchor.TopCenter => new Vector2(parentSize.X / 2 - Size.X / 2, 0),
      Anchor.TopRight => new Vector2(parentSize.X - Size.X, 0),
      Anchor.LeftCenter => new Vector2(0, parentSize.Y / 2 - Size.Y / 2),
      Anchor.Center => new Vector2(parentSize.X / 2 - Size.X / 2, parentSize.Y / 2 - Size.Y / 2),
      Anchor.RightCenter => new Vector2(parentSize.X - Size.X, parentSize.Y / 2 - Size.Y / 2),
      Anchor.BottomLeft => new Vector2(0, parentSize.Y - Size.Y),
      Anchor.BottomCenter => new Vector2(parentSize.X / 2 - Size.X / 2, parentSize.Y - Size.Y),
      Anchor.BottomRight => new Vector2(parentSize.X - Size.X, parentSize.Y - Size.Y),
      _ => new Vector2(0, 0)
    };
  }

  private Vector2 GetOffsetFlip()
  {
    // Calculate the position based on the anchor point and size
    return Anchor switch
    {
      Anchor.TopLeft => new Vector2(1, 1),
      Anchor.TopCenter => new Vector2(1, 1),
      Anchor.TopRight => new Vector2(-1, 1),
      Anchor.LeftCenter => new Vector2(1, 1),
      Anchor.Center => new Vector2(1, 1),
      Anchor.RightCenter => new Vector2(-1, 1),
      Anchor.BottomLeft => new Vector2(1, -1),
      Anchor.BottomCenter => new Vector2(1, -1),
      Anchor.BottomRight => new Vector2(-1, -1),
      _ => new Vector2(1, 1)
    };
  }

  private Vector2 GetPosition()
  {
    return Position * GetOffsetFlip() + GetOffset(); // Get the position of the widget relative to its parent
  }

  private Vector2 GetMousePos()
  {
     return Game.MousePosition;
  }

  public virtual void WidgetUpdate(MatrixStack matrixStack, ref int scale)
  {
    if (!IsEnabled) return; // If the widget is not enabled, skip the update
    if (!IsVisible) return; // If the widget is not visible, skip the update
    matrixStack.Push(); // Push the current matrix onto the stack
    matrixStack.Translate(GetPosition()); // Translate the matrix to the widget's position
    Matrix4x4.Invert(matrixStack.Peek() * Matrix4x4.CreateScale(scale),
      out var inv); // Invert the matrix for hit testing
    Update(GetMousePos().Transformed(inv)); // Call the update method
    HandleInput(); // Call the input handling method
    foreach (var child in Children) child.WidgetUpdate(matrixStack, ref scale); // Update each child widget
    matrixStack.Pop(); // Pop the matrix off the stack
  }

  public virtual void WidgetRender(MatrixStack matrixStack, float delta, ref int scale)
  {
    if (IsVisible)
    {
      matrixStack.Push(); // Push the current matrix onto the stack
      matrixStack.Translate(GetPosition()); // Translate the matrix to the widget's position
      SystemRegistry.Batcher?.PushMatrix(
        matrixStack.Peek().ToMatrix3x2(),false); // Set the model matrix uniform
      Render(matrixStack, delta); // Call the render method if the widget is visible
      foreach (var child in Children) child.WidgetRender(matrixStack, delta, ref scale);
      SystemRegistry.Batcher?.PopMatrix(); // Pop the model matrix uniform.
      matrixStack.Pop(); // Pop the matrix off the stack
    }
  }

  protected bool isMouseHover(Vector2 mousePos)
  {
    // Check if the mouse is hovering over the widget
    if (mousePos.X >= 0 && mousePos.X <= Size.X &&
        mousePos.Y >= 0 && mousePos.Y <= Size.Y)
      return true;
    return false;
  }

  protected int IsMouseDown()
  {
    // Check if the mouse is clicked
    var mouse = SystemRegistry.Input.Mouse.LeftDown;
    var mouse2 = SystemRegistry.Input.Mouse.RightDown;
    return (mouse?1:0) | (mouse2?1:0);
  }

  public void AddChild(Widget child)
  {
    // Add a child widget to the current widget
    child.Parent = this; // Set the parent of the child widget
    Children.Add(child); // Add the child widget to the list of children
  }

  public void RemoveChild(Widget child)
  {
    // Remove a child widget from the current widget
    child.Parent = null; // Clear the parent of the child widget
    Children.Remove(child); // Remove the child widget from the list of children
  }
}