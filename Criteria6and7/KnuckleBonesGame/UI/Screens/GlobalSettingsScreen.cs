using System.Numerics;
using KnuckleBonesGame.UI.Widgets;
using KnuckleBonesGame.Util.Math;
using TileGame.UI.Widgets;

namespace KnuckleBonesGame.UI.Screens;

public class GlobalSettingsScreen :Screen{
  
  public GlobalSettingsScreen(Screen previousScreen) : base() {
    // Initialize the screen with the previous screen
    PreviousScreen = previousScreen;
  }
  
  Screen PreviousScreen;
  ButtonWidget VsyncButton = new ButtonWidget("Vsync", new Vector2(0, 60), new Vector2(100, 50));
  ButtonWidget FullscreenButton = new ButtonWidget("Fullscreen", new Vector2(0, 0), new Vector2(100, 50));
  ButtonWidget BackButton = new ButtonWidget("Back", new Vector2(0, 10), new Vector2(100, 50));
  ButtonWidget UIScaleButton = new ButtonWidget("UI Scale", new Vector2(0, -60), new Vector2(100, 50));
  PanelWidget SettingsPanel = new PanelWidget(new Vector2(0, 0), new Vector2(100, 200));
  public override void Render(MatrixStack matrixStack, float delta) {
    
  }

  public override void Update(Vector2 mousePos) {
  }

  public override void HandleInput() {
  }

  public override void Initialize() {
    AddChild(SettingsPanel);
    SettingsPanel.Anchor = Anchor.Center;
    SettingsPanel.AddChild(VsyncButton);
    SettingsPanel.AddChild(FullscreenButton);
    SettingsPanel.AddChild(UIScaleButton);
    AddChild(BackButton);
    BackButton.Anchor = Anchor.BottomCenter;
    VsyncButton.OnClick += () => {
      GameConfig.VSync.Value = !GameConfig.VSync;
      VsyncButton.Text = "Vsync: " + (GameConfig.VSync ? "On" : "Off");
    };    
    FullscreenButton.OnClick += () => {
      GameConfig.FullScreen.Value = !GameConfig.FullScreen;
      FullscreenButton.Text = "Fullscreen: " + (GameConfig.FullScreen ? "On" : "Off");
    };
    UIScaleButton.OnClick += () => {
      GameConfig.UIScale.Value = (((GameConfig.UIScale)+1 )%3)+1;
      UIScaleButton.Text = "UI Scale: " + GameConfig.UIScale;
    };
    BackButton.OnClick += () => {
      SystemRegistry.ScreenManager.SetScreen(PreviousScreen);
    };
    
  }
}