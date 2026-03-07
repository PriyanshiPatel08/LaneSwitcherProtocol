# LaneSwitcherProtocol

Minimal MonoGame sample demonstrating lane-switching and sprite rendering.

## Project structure

- `Program.cs` — application entry point (creates and runs `Game1`).
- `Game1.cs` — main game class (input, update, draw, asset loading).
- `Objects/LaneSwitcher.cs` — simple lane switching logic used by `Game1`.
- `LaneSwitcher.csproj` — project file with MonoGame package references.
- `Content/` — optional folder for raw assets (not checked into repo).

## Requirements

- .NET SDK (the project targets `net6.0`).
- MonoGame DesktopGL packages are referenced in the project (`MonoGame.Framework.DesktopGL`).
- A PNG sprite named `actionFigure.png` (optional; the game uses a magenta placeholder if missing).

## Build & run

Open `LaneSwitcherProtocol.csproj` or 'Game1.cs' in Visual Studio, set the project as the startup project, build, and run 

## Controls

- Left / Right arrow keys — move lanes
- Esc — exit

## Ai usage
Used Github copilot to identify the error and suggest a fix.

## Note 
I have added a video to show that it is working in my pc. 

