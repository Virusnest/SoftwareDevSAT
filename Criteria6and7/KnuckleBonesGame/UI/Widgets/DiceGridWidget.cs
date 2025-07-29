using System.Numerics;
using Foster.Framework;
using KnuckleBonesGame.UI.Widgets;
using KnuckleBonesGame.Util.Math;

namespace KnuckleBonesGame.GameLoop;

public class DiceGridWidget : Widget {
  public int Width;
  public int Height;
  public float CellSize;
  private GameBoard board;
  bool Flipped;

  public DiceGridWidget(GameBoard board, int width, int height, float cellSize = 50,bool flipped = false) : base() {
    this.board = board;
    Width = width;
    Height = height;
    Flipped = flipped;
    CellSize = cellSize;
  }
  
  public event Action<int,int>? OnCellClicked;
  public override void Render(MatrixStack matrixStack, float delta) {
    for (int i = 0; i < Width; i++) {
      SystemRegistry.Batcher.Line(new Vector2(i*CellSize,0),new Vector2(i*CellSize,CellSize*Height),5,BackgroundColour);
      
    }
    SystemRegistry.Batcher.Line(new Vector2(Width*CellSize,0),new Vector2(Width*CellSize,CellSize*Height),5,BackgroundColour);


    for (int i = 0; i < Height; i++) {
      SystemRegistry.Batcher.Line(new Vector2(0,i*CellSize),new Vector2(CellSize*Width,i*CellSize),5,BackgroundColour);
    }
    SystemRegistry.Batcher.Line(new Vector2(0,Height*CellSize),new Vector2(CellSize*Width,Height*CellSize),5,BackgroundColour);

    
    for (int i = 0; i < Width; i++) {
      for (int j = 0; j < Height; j++) {
        var y = j;
        if (Flipped) {
          y = Height - 1 - j;
        }
        int number = (int)board.GetCell(i, j);
        Vector2 size = SystemRegistry.AssetManager.SpriteFont.SizeOf(number.ToString());
        Vector2 offset = new Vector2(i*CellSize, y*CellSize);
        Vector2 CellOffset = new Vector2(CellSize / 2, CellSize / 2);
        if(number == 0) continue; // Skip empty cells
        SystemRegistry.Batcher.Text(SystemRegistry.AssetManager.SpriteFont, number.ToString(),offset+CellOffset-size/2,Color);
      }
    }
  }
  public override void Update(Vector2 mousePos) {
    // Update the size of the widget based on the number of cells and cell size
    Size = new Vector2(Width * CellSize, Height * CellSize);
    
    // Optionally, you can update the position if needed
    Position = new Vector2(Position.X, Position.Y);
    
    Vector2 MouseCell = (mousePos - Position) / CellSize;
    if (MouseCell.X >= 0 && MouseCell.X < Width && MouseCell.Y >= 0 && MouseCell.Y < Height) {
      int cellX = (int)MouseCell.X;
      int cellY = (int)MouseCell.Y;
      
      // Check if the mouse is clicked on a cell
      if (IsMouseDown() == 1) {
        OnCellClicked?.Invoke(cellX, cellY);
      }
    }
  }

  public override void HandleInput() {
  }
}