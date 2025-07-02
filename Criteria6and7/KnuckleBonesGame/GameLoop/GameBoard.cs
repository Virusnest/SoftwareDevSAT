using System;


namespace KnuckleBonesGame.GameLoop;

public class GameBoard
{
  public int BoardWidth { get; private set; }
  public int BoardHeight { get; private set; }

  public SixDieFaces[,] Board { get; private set; }

  public GameBoard(int boardWidth, int boardHeight)
  {
    BoardWidth = boardWidth;
    BoardHeight = boardHeight;
    Board = new SixDieFaces[boardWidth, boardHeight];
  }

  public SixDieFaces GetCell(int x, int y) {
    return Board[x, y];
  }

  public void SetDieFace(int x, int y, SixDieFaces dieFace)
  {
    Board[x, y] = dieFace;
  }

  public void ClearBoard()
  {
    Board = new SixDieFaces[BoardWidth, BoardHeight];
  }

  public SixDieFaces[] GetColumn(int x)
  {
    SixDieFaces[] column = new SixDieFaces[BoardHeight];
    for (int y = 0; y < BoardHeight; y++)
    {
      column[y] = Board[x, y];
    }
    return column;
  }
  public SixDieFaces[] GetRow(int y)
  {
    SixDieFaces[] row = new SixDieFaces[BoardWidth];
    for (int x = 0; x < BoardWidth; x++)
    {
      row[x] = Board[x, y];
    }
    return row;
  }
  public void RestackColumn(int x)
  {
    SixDieFaces[] column = GetColumn(x);
    ClearColumn(x);
    int count = 0;
    foreach (SixDieFaces dieFace in column)
    {
      if (dieFace != SixDieFaces.None)
      {
        Board[x, count] = dieFace;
        count++;
      }
    }
  }

  private void ClearColumn(int x)
  {
    for (int y = 0; y < BoardHeight; y++)
    {
      Board[x, y] = SixDieFaces.None;
    }
  }

  public void RemoveDieFace(int x, int y)
  {
    Board[x, y] = SixDieFaces.None;
  }

  public int CalculateScore()
  {
    int total = 0;
    for (int y = 0; y < BoardHeight; y++)
    {
      for (int x = 0; x < BoardWidth; x++)
      {
        total += (int)Board[x, y];
      }
    }
    return total;
  }
  public bool IsFull()
  {
    for (int y = 0; y < BoardHeight; y++)
    {
      for (int x = 0; x < BoardWidth; x++)
      {
        if (Board[x, y] == SixDieFaces.None)
        {
          return false;
        }
      }
    }
    return true;
  }
  public bool InsetToColumn(int x, SixDieFaces dieFace)
  {
    for (int y = 0; y < BoardHeight; y++)
    {
      if (Board[x, y] == SixDieFaces.None)
      {
        Board[x, y] = dieFace;
        return true;
      }
    }
    return false;
  }

  public void RemoveDiceOfInColumn(int x, SixDieFaces dieFace)
  {
    for (int y = 0; y < BoardHeight; y++)
    {
      if (Board[x, y] == dieFace)
      {
        RemoveDieFace(x, y);
      }
    }
    RestackColumn(x);
  }


}
