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
