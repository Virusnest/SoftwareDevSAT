using System.Numerics;
using KnuckleBonesGame.Registry;
using KnuckleBonesGame.UI.Widgets;
using KnuckleBonesGame.Util.Math;
using TileGame.UI.Widgets;

namespace KnuckleBonesGame.UI.Screens;

public class WinScreen: Screen{
  public PanelWidget BackgroundPanel = new PanelWidget(new Vector2(0, 0), new Vector2(200, 100));
  public LabelWidget WinLabel = new LabelWidget("You Win!", new Vector2(0, 0));
  public ButtonWidget TitleScreenButton = new ButtonWidget("Return to Title Screen", new Vector2(0, 0), new Vector2(150, 50));
  public override void Render(MatrixStack matrixStack, float delta) {
  }

  public override void Update(Vector2 mousePos) {
  }
  
  public void SetWinText(bool PlayerAWon, int score) {
    if (PlayerAWon) {
      WinLabel.Text = "Player A Wins! With a Score of " + score;
    } else {
      WinLabel.Text = "Player B Wins! With a Score of " + score;
    }
  }

  public WinScreen(bool PlayerAWon, int score) {
    // Initialize the win screen with the winner's text
    SetWinText(PlayerAWon,score);
    
    // Initialize the title screen button
    TitleScreenButton.OnClick += () => {
      SystemRegistry.ScreenManager.SetScreen(new TitleScreen());
    };
  }

  public override void HandleInput() {
  }

  public override void Initialize() {
    SystemRegistry.SoundSystem.PlaySFX(SystemRegistry.AssetManager.LoadAsset<Sound>(new ResourceLocation("sounds/yippee-tbh.mp3")));
    // Initialize the win screen with the background panel and win label
    BackgroundPanel.Anchor = Anchor.Center;

    WinLabel.Anchor = Anchor.TopCenter;
    
    TitleScreenButton.Anchor = Anchor.BottomCenter;
    
    // Add widgets to the screen
    AddChild(BackgroundPanel);
    BackgroundPanel.AddChild(WinLabel);
    BackgroundPanel.AddChild(TitleScreenButton);
  }
}