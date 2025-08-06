using System.Globalization;
using System.Numerics;
using Foster.Framework;
using KnuckleBonesGame.Util.Math;

namespace KnuckleBonesGame.UI.Widgets;

public class SliderWidget : Widget {
  private bool Sliding;
  public float Value { get; private set; }
  private float MinValue;
  private float MaxValue;
  public float Step;
  public string Text;
  private float Percentage => (Value - MinValue) / (MaxValue - MinValue);
  private float PositionX => Percentage * Size.X;
  
  public SliderWidget(Vector2 position, Vector2 size, float minValue, float maxValue, float step = 1,string text = "") {
    Text = text;
    Position = position;
    Size = size;
    MinValue = minValue;
    MaxValue = maxValue;
    Step = step;
    Value = minValue; // Initialize with the minimum value
  }
  public override void Render(MatrixStack matrixStack, float delta) {
    // Render the slider background
    SystemRegistry.Batcher.RectRounded(0,0,Size.X,Size.Y,5, Colors.MainBlockColor);
    
    // Calculate the position of the slider handle based on the value
    
    // Render the slider track
    SystemRegistry.Batcher.RectRounded(0, 0, PositionX, Size.Y, 5, BackgroundColour);
    
    // Render the slider handle
    SystemRegistry.Batcher.Circle(new Vector2(PositionX,Size.Y/2),5, 10, Colors.Accent);
    string txt = !string.IsNullOrEmpty(Text) ? $"{Text}: {Value}" : Value.ToString(CultureInfo.CurrentCulture);
    Vector2 textSize = SystemRegistry.AssetManager.SpriteFont.SizeOf(txt);
    Vector2 centeredPosition = new Vector2(Size.X / 2 - textSize.X / 2, Size.Y / 2 - textSize.Y / 2);
    SystemRegistry.Batcher.Text(SystemRegistry.AssetManager.SpriteFont,txt,
      centeredPosition, Color);
  }

  public override void Update(Vector2 mousePos) {
    if (IsMouseDown() == 1) {
      if (isMouseHover(mousePos)) {
        Sliding = true;
      }
    } else {
      Sliding = false;
    }
    if (Sliding) {
      // Calculate the new value based on the mouse position
      float newValue = MinValue + (mousePos.X / Size.X) * (MaxValue - MinValue);
      // Clamp the value to the defined range
      Value = Math.Clamp(newValue, MinValue, MaxValue);
      // Round to the nearest step
      Value = MathF.Round(Value / Step) * Step;
    }
  }

  public override void HandleInput() {
  }
}