# SRS

## Requirements

**Functional:**

- Local Multiplayer
- Main Gameplay Loop
- Win and loose state
- Minimum asssets
- Basic Sounds

**Non Functional:**

- Animations
- Sound design/SFX
- AI Opponent
- Shader Effects
- Range of Alternate Gamemodes

## Constraints

### Econmics

- No use of external funds
- Compleate by 13th of september

### Techincal

- Multiplayer should stay local to prevent dealing with time-consuming networking code
- C# has some language limitations, altho those should not be an issue due to my experience with it.
- Ui should have a max depth to allow for faster navigation

### Ethical

- Collect and store as little data as possible

## Scope

- Implement Basic Gameplay
- Add Alternate Gamemodes ( At least one )
- Add UI ( Title Screen, Player & Gamemode select )
- Add Settings ( Also add UI for settings )
- Add Graphical Improvement ( Better art, screenshake, shaders, "Juice" )

## User Characteristic

My software’s users would be those who enjoy playing video games, with fun twists and a variety of game modes. The game is focused on both casual and “hardcore” gamers

## Technical Enviroment

**Targets:**

- Windows
- Linux
- Macos
  
**Engine/Framework:**

- Custom C# Framework
- OpenGL

**Resoning:**
  
  I have chosen Windows, Macos and Linux with OpenGL to provide as much crossplatform support as possible within reson.

  Linux and Macos are priority as they are my primary development platforms and it would be pointless developing on a platform that can't be used to debug / test.

  I have chosen to use my own c# custom framework as it will reduce development time, allowing for faster itteration and debugging due to
  being familar with C# as the language I use for development the most and having written the framework.
