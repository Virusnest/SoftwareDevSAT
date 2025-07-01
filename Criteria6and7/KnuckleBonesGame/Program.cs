using Foster.Framework;

namespace KnuckleBonesGame;

public class Program {
  public static Game? Game; 
  public static void Main(string[] args)
  {
    Game = new Game("Knuckle Bones", 800, 600);
    Game.Run();
  }

}