using KnuckleBonesGame.GameLoop;

namespace KnuckleBonesGame;

public interface IKnuckleAI {
  public int GetNextMove(KnuckleGame game, int dice, int aiLevel = 1, bool isPlayerA = false);
}

public class KnuckleAI : IKnuckleAI {
  private Random random = new Random();

  public int GetNextMove(KnuckleGame game, int dice, int aiLevel, bool isPlayerA = false) {
    switch (aiLevel) {
      case 1:
        // Level 1 AI: Randomly selects a valid column
        return GetRandomNextMove(game, dice, isPlayerA);
      case 2:
        // Level 2 AI: Selects the shortest column
        return GetSimpleNextMove(game, dice, isPlayerA);
      default:
        return 1;
    }
  }

  private int GetRandomNextMove(KnuckleGame game, int dice, bool isPlayerA = false) {
    List<int> validColumns = isPlayerA ? game.BoardA.GetValidColumns(dice) : game.BoardB.GetValidColumns(dice);
    if (validColumns.Count == 0) {
      return -1; // No valid columns available
    }

    return validColumns[random.Next(validColumns.Count)];
  }

  private int GetSimpleNextMove(KnuckleGame game, int _, bool isPlayerA = false) {
    return GetShortestColumn(isPlayerA ? game.BoardA : game.BoardB);
  }

  //UNUSED 
  private int GetPersonalStrategyMove(KnuckleGame game, int dice, bool isPlayerA = false) {
    var myBoard = isPlayerA ? game.BoardA : game.BoardB;
    var opponentBoard = isPlayerA ? game.BoardB : game.BoardA;
    //Get Invalid columns for the opponent
    List<int> invalidColumns = opponentBoard.GetInvalidColumns(dice);
    List<int> myValidColumns = myBoard.GetValidColumns(dice);
    // find my valid columns that are the invalid columns of the opponent
    List<int> bestColumns = new List<int>();
    foreach (var column in myValidColumns) {
      if (invalidColumns.Contains(column)) {
        bestColumns.Add(column);
      }
    }

    foreach (var column in bestColumns) {
      if (invalidColumns.Count != 0) {
        if (dice > 3) {
          if (!opponentBoard.GetColumn(column).Contains((SixDieFaces)dice)) {
            return column;
          }
        }
      }
    }

    return GetShortestColumn(myBoard); // Fallback to the first valid column
  }

  public float[] diceWeights = [0.1f, 0.2f, 0.4f, 0.7f, 0.9f, 1];
  public float[] diceWeightsFlipped = [1, 0.9f, 0.7f, 0.4f, 0.2f, 0.1f];


  public float RankColumn(SixDieFaces[] myColumn, SixDieFaces[] theirColumn, int dice) {
    float score = 0;
    float opFillScore = 0;
    foreach (var die in theirColumn) {
      if (die != SixDieFaces.None) {
        opFillScore++;
      }
    }

    float myfillScore = 0;
    foreach (var die in myColumn) {
      if (die == SixDieFaces.None) {
        myfillScore++;
      }
    }

    float removeScore = 0;
    foreach (var die in theirColumn) {
      if ((int)die == dice) {
        removeScore++;
      }
    }

    float ValueScore = 0;
    foreach (var die in theirColumn) {
      if (die != SixDieFaces.None) {
        ValueScore += diceWeights[(int)die - 1];
      }
    }

    return score;
  }

  private int GetShortestColumn(GameBoard board) {
    int smallest = 0;
    for (int i = 0; i < board.BoardWidth; i++) {
      int total = 0;
      for (int j = 0; j < board.BoardHeight; j++) {
        if (board.GetCell(i, j) != SixDieFaces.None) {
          total++;
        }
      }

      if (i == 0 || total < smallest) {
        smallest = total;
      }
    }

    return smallest;
  }
}