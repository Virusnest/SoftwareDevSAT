using System.Numerics;
using KnuckleBonesGame.GameLoop;
using KnuckleBonesGame.UI.Widgets;
using KnuckleBonesGame.Util;
using KnuckleBonesGame.Util.Math;
using Timer = KnuckleBonesGame.Util.Timer;

namespace KnuckleBonesGame.UI.Screens;

public class GamePlayHudScreen:Screen {
  GameplayScene Round;
  public int RolledDice = 0;
  
  public ButtonWidget DiceRollButton = new ButtonWidget("1", new Vector2(10, 10), new Vector2(50, 50));
  public Timer DiceTimer = new Timer();
  public GamePlayHudScreen(GameplayScene round) : base() {
    // Initialize the screen with the round data
    Round = round;
  }
  public override void Render(MatrixStack matrixStack, float delta) {
    DiceTimer.Update(delta);
  }

  public override void Update(Vector2 mousePos) {
  }

  public override void HandleInput() {
  }
  
  public void RollDice() {
    // Logic to roll the dice
    if (!DiceTimer.IsComplete) return;
    RolledDice = Round.RollDice();
    DiceTimer.Start(1.0f); // Start the timer for 1 second
  }
  public override void Initialize() {
    AddChild(DiceRollButton);
    DiceRollButton.Anchor = Anchor.BottomRight;
    DiceRollButton.OnClick += () => {
      RollDice();
      TWEENER.TweenVal(10,20,0.3f,StandardEasings.EaseInOutBack, (val) => {
        DiceRollButton.Position.Y= val;
      },loopCount:1);
    };
    DiceTimer.Complete= () => DiceRollButton.Text = RolledDice.ToString();
    DiceTimer.Tick = () => {
      DiceRollButton.Text=Random.Shared.Next(6).ToString();
    };

  }
}