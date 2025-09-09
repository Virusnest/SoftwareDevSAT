using System.Numerics;
using KnuckleBonesGame.GameLoop;
using KnuckleBonesGame.Registry;
using KnuckleBonesGame.UI.Widgets;
using KnuckleBonesGame.Util;
using KnuckleBonesGame.Util.Math;
using Timer = KnuckleBonesGame.Util.Timer;

namespace KnuckleBonesGame.UI.Screens;
/// <summary>
/// Horrible mess of a class that handles the main game play HUD screen
///
/// Never do this again.
/// </summary>
// TODO : Refactor this class into smaller, more manageable components

public class GamePlayHudScreen:Screen { 
  KnuckleGame Round;
  private GameSettings Settings;
  
  // GAME STATE //
  
  public int RolledDiceA = 0;
  public int RolledDiceB = 0;
  
  public bool HasClickedDiceA = false;
  public bool HasClickedDiceB = false;
  
  // WIDGETS //
  
  public LabelWidget TextPopupLabel = new LabelWidget("Test", new Vector2(0, 0));
  
  public LabelWidget PlayerScoreLabelA = new LabelWidget("Player A Score: 0", new Vector2(10, 10));
  public LabelWidget PlayerScoreLabelB = new LabelWidget("Player B Score: 0", new Vector2(10, 10));

  public ButtonWidget DiceRollButtonA = new ButtonWidget("1", new Vector2(10, 10), new Vector2(50, 50));
  public ButtonWidget DiceRollButtonB = new ButtonWidget("1", new Vector2(10, 10), new Vector2(50, 50));

  public DiceGridWidget GridA;
  public DiceGridWidget GridB;
  
  public Timer DiceTimerA = new Timer();
  public Timer DiceTimerB = new Timer();

  public GamePlayHudScreen(KnuckleGame round, GameSettings settings) : base() {
    // Initialize the screen with the round data
    Round = round;
    Settings = settings;
    GridA = new DiceGridWidget(Round.BoardA, Settings.Width, Settings.Height);
    GridB = new DiceGridWidget(Round.BoardB, Round.BoardB.BoardWidth, Round.BoardB.BoardHeight,flipped:true);
    GridA.Position = new Vector2(0, 5);
    GridB.Position = new Vector2(0, 5);

    LoadWidgets();
    Round.OnTurnStart += (turn) => {
      PlayPopupAnimation(!turn ? "Player A's Turn" : "Player B's Turn");
      
    };
    Round.OnTurnEnd += (turn) => {
      if ((!turn)&&Settings.IsAgainstAI) {
        TWEENER.TweenVal(10,20,0.3f,StandardEasings.EaseInOutBack, (val) => {
          DiceRollButtonB.Position.Y= val;
        },loopCount:1);
        RollDice(false);
      }
    };
    Round.OnGameEnded += (state, score) => {
      SystemRegistry.ScreenManager.SetScreen(new WinScreen(state==GameState.PlayerAWon, score));
    };
  }
  public override void Render(MatrixStack matrixStack, float delta) {
    DiceTimerA.Update(delta);
    DiceTimerB.Update(delta);
  }

  public override void Update(Vector2 mousePos) {
    UpdateScoreLabels();
    if (Round.IsPlayerATurn) {
      PlayerScoreLabelA.Color=Colors.TextPrimary;
      PlayerScoreLabelB.Color=Colors.TextSecondary;
    }
    else {
      
      PlayerScoreLabelA.Color=Colors.TextSecondary;
      PlayerScoreLabelB.Color=Colors.TextPrimary;
    }
  }

  public override void HandleInput() {
    if (SystemRegistry.Controls.Pause.Pressed) {
      PauseGame(true);
    }
  }

  private PauseScreen pauseScreen = new PauseScreen(null);
  public void PauseGame(bool isPaused) {
    // Logic to pause the game
    pauseScreen.Anchor = Anchor.Center;
    if (isPaused) {
      AddChild(pauseScreen);
      SystemRegistry.ScreenManager.Pause(pauseScreen,isPaused);
    }
    else {
      RemoveChild(pauseScreen);
      SystemRegistry.ScreenManager.Pause(pauseScreen,isPaused);

    }
  }

  /// <summary>
  /// Takes the AI's turn if it's the AI's turn to play
  /// </summary>
  public void TakeAiTurn() {
    if (Round.IsPlayerATurn || !Settings.IsAgainstAI) return;
    if (Settings.AI == null) return;
    
    var move = Settings.AI.GetNextMove(Round,RolledDiceB);
    if (move == -1) {
      PlayPopupAnimation("AI skipped turn");
      return;
    }
    Round.TakeTurn((SixDieFaces)RolledDiceB, move);
    
  }
  
  /// <summary>
  /// Rolls the dice for the specified player
  /// </summary>
  /// <param name="IsPlayerA"></param>
  public void RollDice(bool IsPlayerA = true) {
    // Logic to roll the dice
    if (IsPlayerA) {
      if (!DiceTimerA.IsComplete) return;
      RolledDiceA = RollDice();
      DiceTimerA.Start(1.0f); // Start the timer for 1 second
    } else {
      if (!DiceTimerB.IsComplete) return;
      RolledDiceB = RollDice();
      DiceTimerB.Start(1.0f); // Start the timer for 1 second
    }
  }
  public int RollDice() {
    SystemRegistry.SoundSystem.PlaySFX( SystemRegistry.AssetManager.LoadAsset<Sound>(new ResourceLocation("Sounds/dice.wav")));
    return  SystemRegistry.Rng.Int(1,7);
    
  }
/// <summary>
/// Plays a popup animation with the specified text
/// </summary>
/// <param name="text"> Text to Display </param>
  public void PlayPopupAnimation(string text) {
    // Logic to play the popup animation
    TextPopupLabel.Text = text;
    TextPopupLabel.Size = SystemRegistry.AssetManager.SpriteFont.SizeOf(text);
    TWEENER.TweenVal(TextPopupLabel.Position.Y, 0, 1f, StandardEasings.EaseOutExpo, (val) => {
      TextPopupLabel.Position.Y = val; // Example animation effect
    },onComplete: () => {;
      
      // Reset the label position after the animation
      
    },id:1,allowDuplicates:true);
    TWEENER.TweenVal(TextPopupLabel.Position.Y, SystemRegistry.ScreenManager.Height/2, 0.5f, StandardEasings.EaseInExpo, (val) => {
      TextPopupLabel.Position.Y = val; // Reset position
    },delay:1.25f,id:2,allowDuplicates:true);

    
  }
  public void UpdateScoreLabels() {
    // Update the score labels based on the current game state
    PlayerScoreLabelA.Text = $"Player A Score: {Round.BoardA.CalculateScore()}";
    PlayerScoreLabelB.Text = $"Player B Score: {Round.BoardB.CalculateScore()}";
  }
  /// <summary>
  /// THis METHOD IS A MESS AND SHOULD NEVER BE DONE AGAIN
  /// </summary>
  private void LoadWidgets() {
    AddChild(DiceRollButtonA);
    AddChild(DiceRollButtonB);
    AddChild(GridA);
    AddChild(GridB);
    AddChild(TextPopupLabel);
    AddChild(PlayerScoreLabelA);
    AddChild(PlayerScoreLabelB);
    PlayerScoreLabelA.Anchor = Anchor.BottomLeft;
    PlayerScoreLabelB.Anchor = Anchor.TopRight;
    GridA.Anchor = Anchor.BottomCenter;
    GridB.Anchor = Anchor.TopCenter;
    DiceRollButtonA.Anchor = Anchor.BottomRight;
    DiceRollButtonB.Anchor = Anchor.TopLeft;
    TextPopupLabel.Anchor = Anchor.Center;
    DiceRollButtonA.Silent= true; // Disable sound for the dice roll button
    DiceRollButtonB.Silent = true; // Disable sound for the dice roll button
    DiceRollButtonA.OnClick += () => {
      if (!Round.IsPlayerATurn||HasClickedDiceA) return;
      RollDice(true);
      TWEENER.TweenVal(10,20,0.3f,StandardEasings.EaseInOutBack, (val) => {
        DiceRollButtonA.Position.Y= val;
      },loopCount:1);
      HasClickedDiceA = true;
    };
    DiceTimerA.Complete += () => DiceRollButtonA.Text = RolledDiceA.ToString();
    DiceTimerA.Tick += () => {
      DiceRollButtonA.Text=Random.Shared.Next(6).ToString();
    };
    DiceTimerB.Complete += () => {
      if (Settings.IsAgainstAI) {
        TakeAiTurn();
      }
      DiceRollButtonB.Text = RolledDiceB.ToString();
    };
    DiceTimerB.Tick += () => { DiceRollButtonB.Text = Random.Shared.Next(6).ToString(); };
    GridA.OnCellClicked += (x, _) => {
      // Handle cell click for Grid A
      if (!Round.IsPlayerATurn||!HasClickedDiceA) return;
      if(!Round.TakeTurn((SixDieFaces)RolledDiceA, x))
        TWEENER.TweenVal(0,0.3f,0.1f,StandardEasings.EaseInOutBack, (val) => {
          GridA.Rotation= val;
        },loopCount:1);
      HasClickedDiceA = false;
    };
    if (!Settings.IsAgainstAI) {
      DiceRollButtonB.OnClick += () => {
        if (Round.IsPlayerATurn || HasClickedDiceB) return;
        RollDice(false);
        TWEENER.TweenVal(10, 20, 0.3f, StandardEasings.EaseInOutBack, (val) => { DiceRollButtonB.Position.Y = val; },
          loopCount: 1);
        HasClickedDiceB = true;
      };

      GridB.OnCellClicked += (x, _) => {
        // Handle cell click for Grid B
        if (Round.IsPlayerATurn || !HasClickedDiceB) return;
        if (!Round.TakeTurn((SixDieFaces)RolledDiceB, x))
          TWEENER.TweenVal(0, 0.3f, 0.1f, StandardEasings.EaseInOutBack, (val) => { GridB.Rotation = val; },
            loopCount: 1);
        HasClickedDiceB = false;
      };
    }
    // Load the widgets for the game play HUD screen
  }
  public override void Initialize() {
    if (!Round.HasStarted)
      Round.StartGame();
  }
}