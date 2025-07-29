// See https://aka.ms/new-console-template for more information

using Foster.Framework;
using KnuckleBonesGame.Registry;
using KnuckleBonesGame.UI.Screens;

namespace KnuckleBonesGame.GameLoop;

public class GameplayScene : Scene {
  public KnuckleGame Game = new KnuckleGame(3,3);
  public List<GameObject> Objects = new List<GameObject>();
  public override void Update(double deltaTime)
  {
  }
  
  public override void Init() {
    Game.StartGame();
    SystemRegistry.ScreenManager.SetScreen(new GamePlayHudScreen(this));
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

  public int RollDice() {
    SystemRegistry.SoundSystem.PlaySFX( SystemRegistry.AssetManager.LoadAsset<Sound>(new ResourceLocation("Sounds/vine-boom.mp3")));
    return  SystemRegistry.Rng.Int(1,7);
    
  }
  public override void Resize(int width, int height) {
  }
}