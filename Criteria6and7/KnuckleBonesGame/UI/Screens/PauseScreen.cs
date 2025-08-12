using System;
using System.Numerics;
using Foster.Framework;
using KnuckleBonesGame.UI.Widgets;
using KnuckleBonesGame.Util.Math;
using TileGame.UI.Widgets;

namespace KnuckleBonesGame.UI.Screens;

public class PauseScreen : Screen {

  public ButtonWidget Options = new ButtonWidget("Options", new Vector2(0, 0), new Vector2(100, 50));
  public ButtonWidget Quit = new ButtonWidget("Quit", new Vector2(0, 0), new Vector2(100, 50));
  public ButtonWidget BackButton = new ButtonWidget("Back", new Vector2(0, 10), new Vector2(100, 50));

  public PanelWidget OptionsPanel = new PanelWidget(new Vector2(0, 10), new Vector2(100, 110));
  public override void HandleInput() {
  }

  public override void Initialize() {
  }
  
  public PauseScreen(Screen? last) : base() {
    LastScreen = last;
    Options.Anchor = Anchor.TopCenter;
    Quit.Anchor = Anchor.BottomCenter;
    BackButton.Anchor = Anchor.BottomCenter;
    // Options.IsEnabled = false;

    Options.OnClick += () => {
      SystemRegistry.ScreenManager.SetScreen(new GlobalSettingsScreen(Parent as Screen));
    };
    
    Quit.OnClick += () => {
      SystemRegistry.ScreenManager.SetScreen(new TitleScreen());
    };
    
    BackButton.OnClick += () => {
      if (Parent is GamePlayHudScreen p) {
        p.PauseGame(false);
      }
    };

    OptionsPanel.AddChild(Options);
    OptionsPanel.AddChild(Quit);
    OptionsPanel.Anchor = Anchor.Center;
    
    AddChild(OptionsPanel);
    AddChild(BackButton);
  }

  public override void Render(MatrixStack matrixStack, float delta) {
    SystemRegistry.Batcher.PushBlend(BlendMode.Premultiply);
    SystemRegistry.Batcher.Rect(new Rect(Vector2.Zero, new Vector2(1920*4, 1080*4)), new Color(0, 0, 0, 0.5f)); // Draw the background
    SystemRegistry.Batcher.PushBlend(BlendMode.NonPremultiplied);
  }

  public override void Update(Vector2 mousePos) {
    Size = new Vector2(Parent?.Size.X??800, Parent?.Size.Y??600);
  }
}
