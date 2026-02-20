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

From a terminal in the repository root:

```bash
dotnet restore
dotnet run --project "LaneSwitcher.csproj"
```

Or open the folder in Visual Studio: set the project (not an individual file) as the Startup Project, build, and run.

Note: `Program.cs` contains the `Main` entry point — do not set a single `.cs` file as the startup item.

## Assets

To use a real sprite instead of the magenta placeholder:

1. Put `actionFigure.png` into the `Content` folder.
2. In Solution Explorer, right-click the file ? Properties ? set `Copy to Output Directory` to `Copy if newer`.

Alternatively integrate the MonoGame Content Pipeline and an `.mgcb` if you want XNB content.

## Controls

- Left / Right arrow keys — move lanes
- Esc — exit

## Troubleshooting

- If `dotnet build` reports missing packages, run `dotnet restore`.
- If you see only a magenta square, confirm `Content/actionFigure.png` is present and copied to the output directory, or build an XNB with the content pipeline.
- If native DLL errors occur (SDL2, OpenAL, etc.), ensure the DesktopGL native dependencies are present in the build output. Using Visual Studio and the MonoGame NuGet packages normally places them correctly.

## License

This repository is provided as-is.
