using System;

namespace KnuckleBonesGame.GameLoop;


/// <summary>
/// Handles the main game logic, including turns, Events, state management, and win conditions.
/// </summary>
public class KnuckleGame
{
  public bool HasStarted => State == GameState.InProgress;
  public GameBoard BoardA { get; private set; }
  public GameBoard BoardB { get; private set; }
  public int BoardWidth { get; private set; }
  public int BoardHeight { get; private set; }
  public GameState State { get; private set; }
  public bool IsPlayerATurn { get; private set; } = true;
  Random random = new Random();
  

  public KnuckleGame(int boardWidth, int boardHeight)
  {
    BoardWidth = boardWidth;
    BoardHeight = boardHeight;
    BoardA = new GameBoard(boardWidth, boardHeight);
    BoardB = new GameBoard(boardWidth, boardHeight);
    State = GameState.NotStarted;
  }

  public void StartGame()
  {
    State = GameState.InProgress;
    ClearBoards();
    OnGameStarted?.Invoke();
    OnTurnStart?.Invoke(false);
    IsPlayerATurn = true;
  }

  public bool PlaceDieFace(int x, SixDieFaces dieFace, bool isBoardA)
  {
    if (isBoardA)
    {
      if (BoardA.InsetToColumn(x, dieFace)) BoardB.RemoveDiceOfInColumn(x, dieFace);
      else return false;
    }
    else
    {
      if (BoardB.InsetToColumn(x, dieFace)) BoardA.RemoveDiceOfInColumn(x, dieFace);
      else return false;
    }
    return true;
  }
  private SixDieFaces RollDie()
  {
    SixDieFaces dieFace = (SixDieFaces)random.Next(1, 7);
    OnDieRolled?.Invoke(dieFace);
    return dieFace;
  }

  public void ClearBoards()
  {
    BoardA.ClearBoard();
    BoardB.ClearBoard();
  }
  public bool CheckWinCondition()
  {
    return BoardA.IsFull() | BoardB.IsFull();
  }

  public void FindWinner()
  {
    int boardAScore = BoardA.CalculateScore();
    int boardBScore = BoardB.CalculateScore();
    if (boardAScore > boardBScore)
    {
      SetGameState(GameState.PlayerAWon);
      OnGameEnded?.Invoke(GameState.PlayerAWon, boardAScore);
    }
    else if (boardBScore > boardAScore)
    {
      SetGameState(GameState.PlayerBWon);
      OnGameEnded?.Invoke(GameState.PlayerBWon, boardBScore);
    }
    else
    {
      SetGameState(GameState.Draw);
      OnGameEnded?.Invoke(GameState.Draw, boardAScore);
    }
  }
  
  public bool TakeTurn(SixDieFaces roll, int column)
  {
    bool placedDice= PlaceDieFace(column, roll, IsPlayerATurn);
    if (!placedDice)
      return false; // If the die could not be placed, return false
    OnTurnStart?.Invoke(IsPlayerATurn);
    IsPlayerATurn = !IsPlayerATurn;
    OnTurnEnd?.Invoke(IsPlayerATurn);
    if (CheckWinCondition())
    {
      FindWinner();
    }
    return true;
  }

  public void SetGameState(GameState state)
  {
    State = state;
    OnStateChanged?.Invoke(state);
  }

  /// <summary>
  /// Event that is triggered when the game state changes: GameState
  /// </summary>
  public event Action<GameState> OnStateChanged;
  /// <summary>
  /// Event that is triggered before a turn is taken: IsPlayerATurn
  /// </summary>
  public event Action<bool> OnTurnStart;
  /// <summary>
  /// Event that is triggered after a turn is taken: IsPlayerATurn
  /// </summary>
  public event Action<bool> OnTurnEnd;

  /// <summary>
  /// Event that is triggered when the game starts: Game Started
  /// </summary>
  public event Action OnGameStarted;
  /// <summary>
  /// Event that is triggered when a die is rolled: Dice Rolled
  /// </summary>
  public event Action<SixDieFaces> OnDieRolled;
  /// <summary>
  /// Event that is triggered when the game ends: final GameState, Score
  /// </summary>
  public event Action<GameState, int> OnGameEnded;


}
