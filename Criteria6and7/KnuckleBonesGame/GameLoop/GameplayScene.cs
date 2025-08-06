// See https://aka.ms/new-console-template for more information

using Foster.Framework;
using KnuckleBonesGame.Registry;
using KnuckleBonesGame.UI.Screens;

namespace KnuckleBonesGame.GameLoop;

public class GameplayScene : Scene {
  public KnuckleGame Game;
  public List<GameObject> Objects = new List<GameObject>();
  public override void Update(double deltaTime)
  {
  }
  
  public override void Init() {
    var settings = new GameSettings {Width = 3,Height = 3};
    Game = new KnuckleGame(settings.Width,settings.Height);
    Game.StartGame();
  }
  public override void FixedUpdate(double deltaTime) {
  }

  public override void Render(double deltaTime) {
    foreach (var obj in Objects) {
      obj.Draw((float)deltaTime);
    }
  }

  public override void Dispose() {
  }


  public override void Resize(int width, int height) {
  }
}