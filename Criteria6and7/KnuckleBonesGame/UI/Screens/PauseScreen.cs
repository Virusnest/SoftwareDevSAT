using System;
using System.Numerics;
using KnuckleBonesGame.UI.Widgets;
using KnuckleBonesGame.Util.Math;
using TileGame.UI.Widgets;

namespace KnuckleBonesGame.UI.Screens;

public class PauseScreen : Screen {

  public ButtonWidget Options = new ButtonWidget("Vsync", new Vector2(0, 0), new Vector2(100, 50));
  public ButtonWidget Quit = new ButtonWidget("Fullscreen", new Vector2(0, 0), new Vector2(100, 50));
  public ButtonWidget BackButton = new ButtonWidget("Back", new Vector2(0, 0), new Vector2(100, 50));

  public PanelWidget OptionsPanel = new PanelWidget(new Vector2(0, 0), new Vector2(100, 200));
  public override void HandleInput() {
  }

  public override void Initialize() {
    throw new NotImplementedException();
  }

  public override void Render(MatrixStack matrixStack, float delta) {
    throw new NotImplementedException();
  }

  public override void Update(Vector2 mousePos) {
    throw new NotImplementedException();
  }
}
