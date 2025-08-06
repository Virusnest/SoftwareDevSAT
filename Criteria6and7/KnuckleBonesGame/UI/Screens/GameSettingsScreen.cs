using System.Numerics;
using KnuckleBonesGame.GameLoop;
using KnuckleBonesGame.UI.Widgets;
using KnuckleBonesGame.Util.Math;
using TileGame.UI.Widgets;

namespace KnuckleBonesGame.UI.Screens;

public class GameSettingsScreen :Screen  {

  public GameSettingsScreen(Screen? last) {
    LastScreen = last;
    BackButton.Anchor = Anchor.BottomCenter;
    PlayButton.Anchor = Anchor.TopCenter;
    BackButton.OnClick += () => {
      if (LastScreen != null) {
        SystemRegistry.ScreenManager.SetScreen(LastScreen);
      }
    };
    PlayButton.OnClick += () => {
      var settings = new GameSettings() {
        Width = (int)BoardWidthSlider.Value,
        Height = (int)BoardHeightSlider.Value,
        AILevel = (int)AIDifficultySldier.Value,
      };
      var screen = new GamePlayHudScreen(new KnuckleGame(settings.Width, settings.Height), settings);
      SystemRegistry.ScreenManager.SetScreen(screen);
      
    };
    SettingsPanel.AddChild(AIDifficultySldier);
    SettingsPanel.AddChild(BoardWidthSlider);
    SettingsPanel.AddChild(BoardHeightSlider);
    SettingsPanel.Anchor = Anchor.Center;
    AIDifficultySldier.Anchor = Anchor.TopCenter;
    BoardWidthSlider.Anchor = Anchor.Center;
    BoardHeightSlider.Anchor = Anchor.BottomCenter;
    
    AddChild(SettingsPanel);
    AddChild(BackButton);
    AddChild(PlayButton);

  }
  private Screen? LastScreen;
  ButtonWidget BackButton = new ButtonWidget("Back", new Vector2(0, 0), new Vector2(100, 50));
  ButtonWidget PlayButton = new ButtonWidget("Play", new Vector2(0, 0), new Vector2(100, 50));
  PanelWidget SettingsPanel = new PanelWidget(new Vector2(0, 0), new Vector2(100, 200));
  SliderWidget AIDifficultySldier = new SliderWidget(new Vector2(0, 0), new Vector2(100, 50), 0, 5, text:"AI Difficulty");
  SliderWidget BoardWidthSlider = new SliderWidget( new Vector2(0, 0), new Vector2(100, 50), 3, 10, text:"Board Width");
  SliderWidget BoardHeightSlider = new SliderWidget(new Vector2(0, 0), new Vector2(100, 50), 3, 10, text:"Board Height");
  public override void Render(MatrixStack matrixStack, float delta) {
    
  }

  public override void Update(Vector2 mousePos) {
  }

  public override void HandleInput() {
  }

  public override void Initialize() {
    
  }
}