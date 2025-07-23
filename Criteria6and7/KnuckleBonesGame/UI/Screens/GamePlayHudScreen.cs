using System.Numerics;
using KnuckleBonesGame.GameLoop;
using KnuckleBonesGame.UI.Widgets;
using KnuckleBonesGame.Util;
using KnuckleBonesGame.Util.Math;
using Timer = KnuckleBonesGame.Util.Timer;

namespace KnuckleBonesGame.UI.Screens;

public class GamePlayHudScreen:Screen {
  GameplayScene Round;
  public int RolledDiceA = 0;
  public int RolledDiceB = 0;
  
  public bool HasClickedDiceA = false;
  public bool HasClickedDiceB = false;
  
  public LabelWidget TextPopupLabel = new LabelWidget("Test", new Vector2(0, 0));

  public ButtonWidget DiceRollButtonA = new ButtonWidget("1", new Vector2(10, 10), new Vector2(50, 50));
  public ButtonWidget DiceRollButtonB = new ButtonWidget("1", new Vector2(10, 10), new Vector2(50, 50));

  public DiceGridWidget GridA;
  public DiceGridWidget GridB;
  
  public Timer DiceTimerA = new Timer();
  public Timer DiceTimerB = new Timer();

  public GamePlayHudScreen(GameplayScene round) : base() {
    // Initialize the screen with the round data
    Round = round;
    GridA = new DiceGridWidget(Round.Game.BoardA, Round.Game.BoardA.BoardWidth, Round.Game.BoardA.BoardHeight);
    GridB = new DiceGridWidget(Round.Game.BoardB, Round.Game.BoardB.BoardWidth, Round.Game.BoardB.BoardHeight,flipped:true);
    Round.Game.OnTurnStart += (turn) => {
      if (turn) {
        PlayPopupAnimation("Player A's Turn");
      }
      else {
        PlayPopupAnimation("Player B's Turn");
      }

    };
  }
  public override void Render(MatrixStack matrixStack, float delta) {
    DiceTimerA.Update(delta);
    DiceTimerB.Update(delta);
  }

  public override void Update(Vector2 mousePos) {
  }

  public override void HandleInput() {
  }
  
  public void RollDice(bool IsPlayerA = true) {
    // Logic to roll the dice
    if (IsPlayerA) {
      if (!DiceTimerA.IsComplete) return;
      RolledDiceA = Round.RollDice();
      DiceTimerA.Start(1.0f); // Start the timer for 1 second
    } else {
      if (!DiceTimerB.IsComplete) return;
      RolledDiceB = Round.RollDice();
      DiceTimerB.Start(1.0f); // Start the timer for 1 second
    }
  }

  public void PlayPopupAnimation(string text) {
    // Logic to play the popup animation
    TextPopupLabel.Text = text;
    TextPopupLabel.Size = SystemRegistry.AssetManager.SpriteFont.SizeOf(text);
    TWEENER.Clear();
    TWEENER.TweenVal(SystemRegistry.ScreenManager.Height/4, 0, 3f, StandardEasings.EaseOutExpo, (val) => {
      TextPopupLabel.Position.Y = val; // Example animation effect
    },onComplete: () => {;
      // Reset the label position after the animation
      TWEENER.TweenVal(0, SystemRegistry.ScreenManager.Height/2, 1f, StandardEasings.EaseInExpo, (val) => {
        TextPopupLabel.Position.Y = val; // Reset position
      },delay:5f);
    });
    
  }
  public override void Initialize() {
    
    AddChild(DiceRollButtonA);
    AddChild(DiceRollButtonB);
    AddChild(GridA);
    AddChild(GridB);
    AddChild(TextPopupLabel);
    GridA.Anchor = Anchor.BottomCenter;
    GridB.Anchor = Anchor.TopCenter;
    DiceRollButtonA.Anchor = Anchor.BottomRight;
    DiceRollButtonB.Anchor = Anchor.TopLeft;
    TextPopupLabel.Anchor = Anchor.Center;
    DiceRollButtonA.OnClick += () => {
      if (!Round.Game.IsPlayerATurn||HasClickedDiceA) return;
      RollDice();
      TWEENER.TweenVal(10,20,0.3f,StandardEasings.EaseInOutBack, (val) => {
        DiceRollButtonA.Position.Y= val;
      },loopCount:1);
      HasClickedDiceA = true;
    };
    DiceTimerA.Complete= () => DiceRollButtonA.Text = RolledDiceA.ToString();
    DiceTimerA.Tick = () => {
      DiceRollButtonA.Text=Random.Shared.Next(6).ToString();
    };
    DiceRollButtonB.OnClick += () => {
      if (Round.Game.IsPlayerATurn||HasClickedDiceB) return;
      RollDice(false);
      TWEENER.TweenVal(10,20,0.3f,StandardEasings.EaseInOutBack, (val) => {
        DiceRollButtonB.Position.Y= val;
      },loopCount:1);
      HasClickedDiceB = true;
    };
    DiceTimerB.Complete= () => DiceRollButtonB.Text = RolledDiceB.ToString();
    DiceTimerB.Tick = () => {
      DiceRollButtonB.Text=Random.Shared.Next(6).ToString();
    };
    GridA.OnCellClicked += (x, _) => {
      // Handle cell click for Grid A
      if (!Round.Game.IsPlayerATurn||!HasClickedDiceA) return;
      if(!Round.Game.TakeTurn((SixDieFaces)RolledDiceA, x))
        TWEENER.TweenVal(0,0.3f,0.1f,StandardEasings.EaseInOutBack, (val) => {
          GridA.Rotation= val;
        },loopCount:1);
      HasClickedDiceA = false;
    };
    GridB.OnCellClicked += (x, _) => {
      // Handle cell click for Grid B
      if (Round.Game.IsPlayerATurn||!HasClickedDiceB) return;
      if(!Round.Game.TakeTurn((SixDieFaces)RolledDiceB, x))
        TWEENER.TweenVal(0,0.3f,0.1f,StandardEasings.EaseInOutBack, (val) => {
          GridB.Rotation= val;
        },loopCount:1);
      HasClickedDiceB = false;
    };

  }
}