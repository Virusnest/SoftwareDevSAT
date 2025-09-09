using System.Numerics;
using Foster.Framework;
using KnuckleBonesGame.GameLoop;
using KnuckleBonesGame.Registry;
using KnuckleBonesGame.UI.Widgets;
using KnuckleBonesGame.Util;
using KnuckleBonesGame.Util.Math;
using TileGame.UI.Widgets;

namespace KnuckleBonesGame.UI.Screens;

public class TitleScreen : Screen {
  private readonly SpriteWidget _titleImage = new SpriteWidget(SystemRegistry.AssetManager.LoadAsset<SpriteAsset>(new ResourceLocation("Sprites/Title.png")).Texture,
    new Vector2(0, 100));
  private readonly ButtonWidget _exitButton = new("Exit", new Vector2(0, 0), new Vector2(200, 25)); // Exit button

  private readonly PanelWidget
    _listWidget = new(new Vector2(0, 0), new Vector2(100, 100)); // List widget for displaying items

  private readonly ButtonWidget
    _optionsButton = new("Options", new Vector2(0, 0), new Vector2(200, 25)); // Options button

  private readonly ButtonWidget _playButton = new("New Game", new Vector2(0, 0), new Vector2(200, 25));

  public TitleScreen() {
    AddChild(_titleImage);
    _titleImage.Anchor = Anchor.TopCenter;
    _titleImage.Scale = Vector2.One * 4f;
    _listWidget.Anchor = Anchor.Center;
    _listWidget.AddChild(_playButton); // Add the play button to the screen
    _listWidget.AddChild(_optionsButton); // Add the options button to the screen
    _listWidget.AddChild(_exitButton); // Add the exit button to the screen
    AddChild(_listWidget); // Add the list widget to the screen
    _playButton.Anchor = Anchor.TopCenter;
    _optionsButton.Anchor = Anchor.Center; // Set the anchor for the options button
    _exitButton.Anchor = Anchor.BottomCenter; // Set the anchor for the exit button5
    _playButton.OnClick += () => {
      // Handle play button click
      SystemRegistry.ScreenManager.SetScreen(new GameSettingsScreen(this));
    };
    _optionsButton.OnClick += () => {
      // Handle options button click
      SystemRegistry.ScreenManager.SetScreen(new GlobalSettingsScreen(this));
    };
    _exitButton.OnClick += () => {
      // Handle exit button click
      Console.WriteLine("Exit button clicked!");
      Environment.Exit(0); // Exit the application
    };
  }

  public override void HandleInput() {
    // throw new NotImplementedException();
  }

  public override void Initialize() {
    SystemRegistry.SoundSystem.PlayMusic(SystemRegistry.AssetManager.LoadAsset<Sound>(new ResourceLocation("Sounds/music.wav")));
    TWEENER.TweenStaggered(_listWidget.Children, x => x.Position.X - 1000, x => x.Position.X, 0.6f, 0.1f,
      MaterialEasings.ExpressiveSlowSpatial, (x, f) => {
        x.Position.X = f;
  }, 1);

  TWEENER.TweenStaggered(_listWidget.Children, x => 0, x => x.BackgroundColour.A, 0.3f, 0.1f, MaterialEasings.ExpressiveSlowEffects,
      (x, f) => x.BackgroundColour.A = (byte)f, 1);
    // Initialize the screen and add widgets

  }

  public override void Render(MatrixStack matrixStack, float delta) {
    // Render the title screen with a semi-transparent background
    // Stupid Hack because I cant write a fucking ui system
    SystemRegistry.Batcher.PushBlend(BlendMode.Premultiply);
    SystemRegistry.Batcher.Rect(new Rect(Vector2.Zero, new Vector2(1920*4, 1080*4)), new Color(0, 0, 0, 0.5f)); // Draw the background
    SystemRegistry.Batcher.PushBlend(BlendMode.NonPremultiplied);

  }

  public override void Update(Vector2 mousePos) {
  }
}