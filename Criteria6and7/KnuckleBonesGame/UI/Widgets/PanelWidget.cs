using System;
using System.Numerics;
using KnuckleBonesGame.UI.Widgets;
using KnuckleBonesGame.Util.Math;

namespace TileGame.UI.Widgets;

public class PanelWidget : Widget {

  public PanelWidget(Vector2 position, Vector2 size) {
    Position = position;
    Size = size;
  }
  public override void HandleInput() {
  }

  public override void Render(MatrixStack matrixStack, float delta) {
  }

  public override void Update(Vector2 mousePos) {
  }
}

