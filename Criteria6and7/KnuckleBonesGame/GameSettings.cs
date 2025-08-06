namespace KnuckleBonesGame;

public struct GameSettings {
  public int Width=3;
  public int Height=3;
  public bool IsAgainstAI {
    get => AI != null;
  }
  public int AILevel=1;
  public IKnuckleAI? AI = null;
  public GameSettings() {
  }
}