using System.Numerics;
using Foster.Framework;
using KnuckleBonesGame.Util.Math;

namespace KnuckleBonesGame.UI.Widgets;

public class Label : Widget {
  public string Text { get; set; }
  public override void Render(MatrixStack matrixStack, float delta) {
    SystemRegistry.Batcher.Text(SystemRegistry.AssetManager.SpriteFont, Text,Vector2.Zero, Color.White);
  }

  public override void Update(Vector2 mousePos) {
    Size= SystemRegistry.AssetManager.SpriteFont.SizeOf(Text);
  }

  public override void HandleInput() {
  }
}