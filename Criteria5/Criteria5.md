# Class Structures

The structures for all the game's clases

## Game Manager

Manages Game State and Gameloop Logic

### Methods

- `TakeTurn()` Takes a turn for the given player
- `RollDie()` Rolls a virtural die
- `CheckForWinCondition()` Checks the board for a win condition

### Properties

- `Boards[GameBoard]` Object represengint the board's state for each player
- `GameState<GameState>` Enum Representing the Gameplay State (P1 Turn, P2 Turn, P2 Win, P1 Win, Tie)

## GameBoard

Holds the state of a player board

### Methods

- `PlaceDice()` Places a die in a column
- `Restack()` Restacks a column's dice to leave no empty gaps
- `Clear()` Clears the board
  
### Properties

- `Cells[int][int]`
- `Size<Width,Height>`

# Psudo Code

```
BEGIN MAIN
    SET game ß new KnuckleGame(3, 3)

    REGISTER event handlers for:
        - die rolled
        - game start
        - turn start
        - turn end
        - game end

    CALL game.StartGame()

    WHILE game.State = InProgress DO
        CALL game.TakeTurn(PromptForColumn)
    END WHILE
END MAIN


PROCEDURE StartGame
BEGIN
    SET State ß InProgress
    CALL ClearBoards()
    CALL OnGameStarted()
    SET IsPlayerATurn ß TRUE
END


FUNCTION TakeTurn(PickColumnCallback)
BEGIN
    CALL OnTurnStart(IsPlayerATurn)
    SET dieFace ß RollDie()

    WHILE NOT PlaceDieFace(PickColumnCallback(), dieFace, IsPlayerATurn) DO
        // keep asking until placement succeeds
    END WHILE

    CALL OnTurnEnd(IsPlayerATurn)
    SET IsPlayerATurn ß NOT IsPlayerATurn

    IF CheckWinCondition() THEN
        CALL FindWinner()
    END IF

    RETURN TRUE
END


FUNCTION PlaceDieFace(x, dieFace, isBoardA)
BEGIN
    IF isBoardA THEN
        IF BoardA.InsetToColumn(x, dieFace) THEN
            CALL BoardB.RemoveDiceOfInColumn(x, dieFace)
            RETURN TRUE
        ELSE
            RETURN FALSE
        END IF
    ELSE
        IF BoardB.InsetToColumn(x, dieFace) THEN
            CALL BoardA.RemoveDiceOfInColumn(x, dieFace)
            RETURN TRUE
        ELSE
            RETURN FALSE
        END IF
    END IF
END


PROCEDURE FindWinner
BEGIN
    SET boardAScore ß BoardA.CalculateScore()
    SET boardBScore ß BoardB.CalculateScore()

    IF boardAScore > boardBScore THEN
        CALL SetGameState(PlayerAWon)
        CALL OnGameEnded(PlayerAWon, boardAScore)
    ELSE IF boardBScore > boardAScore THEN
        CALL SetGameState(PlayerBWon)
        CALL OnGameEnded(PlayerBWon, boardBScore)
    ELSE
        CALL SetGameState(Draw)
        CALL OnGameEnded(Draw, boardAScore)
    END IF
END


FUNCTION InsetToColumn(x, dieFace)
BEGIN
    FOR y ß 0 TO BoardHeight - 1 DO
        IF Board[x, y] = None THEN
            SET Board[x, y] ß dieFace
            RETURN TRUE
        END IF
    END FOR

    RETURN FALSE
END


PROCEDURE RemoveDiceOfInColumn(x, dieFace)
BEGIN
    FOR y ß 0 TO BoardHeight - 1 DO
        IF Board[x, y] = dieFace THEN
            SET Board[x, y] ß None
        END IF
    END FOR

    CALL RestackColumn(x)
END


PROCEDURE RestackColumn(x)
BEGIN
    SET temp ß empty list

    FOR y ß 0 TO BoardHeight - 1 DO
        IF Board[x, y] ≠ None THEN
            ADD Board[x, y] TO temp
        END IF
    END FOR

    CALL ClearColumn(x)

    FOR i ß 0 TO LENGTH(temp) - 1 DO
        SET Board[x, i] ß temp[i]
    END FOR
END

```

## 🗂 Data Dictionary

| **Name**        | **Data Type**      | **Description**                                    | **Data Source**        |
| --------------- | ------------------ | -------------------------------------------------- | ---------------------- |
| `KnuckleGame`   | Class              | Controls game flow, turn logic, and event handling | Game logic             |
| `GameBoard`     | Class              | Represents the grid of dice for one player         | Game logic             |
| `BoardA`        | GameBoard          | Player A's game board                              | Initialized in code    |
| `BoardB`        | GameBoard          | Player B's game board                              | Initialized in code    |
| `BoardWidth`    | Integer            | Number of columns                                  | Provided at game start |
| `BoardHeight`   | Integer            | Number of rows                                     | Provided at game start |
| `IsPlayerATurn` | Boolean            | Tracks whose turn it is                            | Managed internally     |
| `dieFace`       | Enum (SixDieFaces) | Result from rolling a die (1–6)                    | Randomly generated     |
| `x`             | Integer            | Column index where die is placed                   | From player input      |
| `State`         | Enum (GameState)   | Current game state (e.g., InProgress, Draw)        | Controlled by logic    |
| `Board[x, y]`   | SixDieFaces        | The die value at a specific board location         | GameBoard state        |
| `Random`        | Object             | Used to generate random die rolls                  | System library         |

---

## 🔁 IPO Chart

### **Game Initialization**

| **Input**        | **Process**                            | **Output**          |
| ---------------- | -------------------------------------- | ------------------- |
| Board dimensions | Initialize game state and empty boards | Ready game instance |

---

### **Rolling a Die**

| **Input** | **Process**               | **Output**      |
| --------- | ------------------------- | --------------- |
| None      | Generate random value 1–6 | Rolled die face |

---

### **Taking a Turn**

| **Input**                 | **Process**                                                                                      | **Output**                    |
| ------------------------- | ------------------------------------------------------------------------------------------------ | ----------------------------- |
| Column selection callback | Roll die → Attempt placement → Remove matches from other board → Switch turn → Check for endgame | Updated boards and game state |

---

### **Placing a Die**

| **Input**                        | **Process**                                                                | **Output**    |
| -------------------------------- | -------------------------------------------------------------------------- | ------------- |
| Column, die face, board selector | Try to place die in column; if success, remove matches from opponent board | TRUE or FALSE |

---

### **Checking Win Condition**

| **Input**   | **Process**                   | **Output**    |
| ----------- | ----------------------------- | ------------- |
| Game boards | Check if either board is full | TRUE or FALSE |

---

### **Finding Winner**

| **Input**               | **Process**                          | **Output**            |
| ----------------------- | ------------------------------------ | --------------------- |
| Scores from both boards | Compare and determine winner or draw | Final state and score |
