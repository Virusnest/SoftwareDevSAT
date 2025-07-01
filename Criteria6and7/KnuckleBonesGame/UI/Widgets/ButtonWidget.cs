using System.Numerics;
using Foster.Framework;
using KnuckleBonesGame.Util.Math;

namespace KnuckleBonesGame.UI.Widgets;

public class ButtonWidget : Widget {
  private bool hasPressed; // Track if the button was clicked
  private bool Hovering; // Track if the mouse has entered the button area
  private bool startedClickingOut;

  public string Text; // The text displayed on the button
  
  public ButtonWidget(string text, Vector2 position, Vector2 size) {
    Text = text;
    Position = position;
    Size = size;
  }

  public event Action? OnClick; // The action to perform when the button is clicked

  public override void HandleInput() {
    // throw new NotImplementedException();
  }

  public override void Render(MatrixStack matrixStack, float delta) {
    
    SystemRegistry.Batcher.RectRounded(new Rect(Vector2.Zero, Size),5, BackgroundColour); // Draw the button background
    var TextOffset = new Vector2(
      Size.X / 2 - SystemRegistry.AssetManager.SpriteFont.WidthOf(Text) / 2, // Center the text horizontally
      Size.Y / 2 - SystemRegistry.AssetManager.SpriteFont.HeightOf(Text) / 2 // Center the text vertically
    );

    // Offset for the text position
    
    SystemRegistry.Batcher.Text(SystemRegistry.AssetManager.SpriteFont,Text,TextOffset, Color); // Draw the button text
  }

  public override void Update(Vector2 mousePos) {
    // Check if the mouse is over the button and if it is clicked
    Hovering = false;

    if (isMouseHover(mousePos)) Hovering = true;
    if (IsMouseDown() == 1) {
      if (Hovering && !startedClickingOut)
        hasPressed = true;
      else
        startedClickingOut = true;
    }

    if (IsMouseDown() != 1) {
      if (hasPressed && Hovering) OnClick?.Invoke();
      startedClickingOut = false;
      hasPressed = false;
    }
  }
}