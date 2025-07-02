using System.Numerics;
using Foster.Framework;

namespace KnuckleBonesGame.GameLoop;

public class DiceGridObject : GameObject {
  public int Width=3;
  public int Height=3;
  public float CellSize=100;
  private GameBoard board;

  public DiceGridObject(GameBoard board, int width, int height, float cellSize = 100) {
    this.board = board;
  }
  public override void Update(float delta) {
    
  }

  public override void Draw(float delta) {
    for (int i = 0; i < Width; i++) {
      SystemRegistry.Batcher.Line(new Vector2(i*CellSize,Position.Y),new Vector2(i*CellSize,Position.Y+CellSize*Height),10,Color.Blue);
      
    }
    SystemRegistry.Batcher.Line(new Vector2(Width*CellSize,Position.Y),new Vector2(Width*CellSize,Position.Y+CellSize*Height),10,Color.Blue);


    for (int i = 0; i < Height; i++) {
      SystemRegistry.Batcher.Line(new Vector2(Position.X,i*CellSize),new Vector2(Position.X+CellSize*Width,i*CellSize),10,Color.Blue);
    }
    SystemRegistry.Batcher.Line(new Vector2(Position.X,Height*CellSize),new Vector2(Position.X+CellSize*Width,Height*CellSize),10,Color.Blue);

    for (int i = 0; i < Width; i++) {
      for (int j = 0; j < Height; j++) {
        int number = (int)board.GetCell(i, j);
        Vector2 Size = SystemRegistry.AssetManager.SpriteFont.SizeOf(number.ToString());
        Vector2 CellOffset = new Vector2(j*CellSize, -i*CellSize);
        SystemRegistry.Batcher.Text(SystemRegistry.AssetManager.SpriteFont, number.ToString(),CellOffset+Size/2,Color.Red);

      }
    }
  }

  public override void Spawn() {
    
  }
}