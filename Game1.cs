using System;
using System.IO;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LaneSwitcherProtocol.Objects;

namespace LaneSwitcherProtocol
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Sprite
        private Texture2D actionFigure;

        // Object under test
        private LaneSwitcher laneSwitcher;

        // Keyboard tracking
        private KeyboardState previousKeyboard;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Initialize keyboard state to avoid NullReferenceException on first Update
            previousKeyboard = Keyboard.GetState();

            // Create the LaneSwitcher object
            laneSwitcher = new LaneSwitcher(startLane: 1, laneWidth: 150);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // 1) Try to load via MonoGame Content pipeline (expects an XNB with asset name "actionFigure")
            try
            {
                actionFigure = Content.Load<Texture2D>("actionFigure"); // asset name, no extension
                Debug.WriteLine("Loaded actionFigure via Content.Load");
                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Content.Load failed for 'actionFigure': {ex.Message}");
            }

            // 2) Fallback: attempt to load the raw PNG file from disk (works if PNG is copied to output)
            try
            {
                string[] candidates = new[]
                {
                    Path.Combine(AppContext.BaseDirectory, Content.RootDirectory, "actionFigure.png"),
                    Path.Combine(AppContext.BaseDirectory, "Content", "actionFigure.png"),
                    Path.Combine(AppContext.BaseDirectory, "actionFigure.png"),
                    Path.Combine(Environment.CurrentDirectory, "Content", "actionFigure.png"),
                    "Content/actionFigure.png",
                    "actionFigure.png"
                };

                string found = null;
                foreach (var c in candidates)
                {
                    if (File.Exists(c))
                    {
                        found = c;
                        break;
                    }
                }

                if (found != null)
                {
                    using var stream = File.OpenRead(found);
                    actionFigure = Texture2D.FromStream(GraphicsDevice, stream);
                    Debug.WriteLine($"Loaded actionFigure.png from file: {found}");
                    return;
                }

                Debug.WriteLine("Raw PNG not found in any candidate path.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load actionFigure.png from disk: {ex.Message}");
            }

            // 3) Final fallback: visible placeholder so the game continues to run
            const int size = 64;
            actionFigure = new Texture2D(GraphicsDevice, size, size);
            var data = new Color[size * size];
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Magenta;
            actionFigure.SetData(data);
            Debug.WriteLine("Using magenta placeholder for actionFigure.");
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboard = Keyboard.GetState();

            // Exit
            if (currentKeyboard.IsKeyDown(Keys.Escape))
                Exit();

            // Guard against laneSwitcher being null (defensive)
            if (laneSwitcher != null)
            {
                // INPUT → OBJECT MUTATORS
                if (currentKeyboard.IsKeyDown(Keys.Left) && previousKeyboard.IsKeyUp(Keys.Left))
                {
                    laneSwitcher.MoveLeft();
                }

                if (currentKeyboard.IsKeyDown(Keys.Right) && previousKeyboard.IsKeyUp(Keys.Right))
                {
                    laneSwitcher.MoveRight();
                }

                // Update object (cooldown)
                laneSwitcher.Update(gameTime.ElapsedGameTime.TotalSeconds);
            }

            previousKeyboard = currentKeyboard;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            int centerX = _graphics.PreferredBackBufferWidth / 2;
            int xPosition = laneSwitcher?.GetXPosition(centerX) ?? centerX;
            int yPosition = 400;

            if (actionFigure != null)
            {
                // 1) Scale the sprite to a reasonable on-screen size
                //    Choose target width/height in pixels and scale uniformly to preserve aspect ratio.
                const float targetWidth = 96f;
                const float targetHeight = 128f;
                float scaleX = targetWidth / actionFigure.Width;
                float scaleY = targetHeight / actionFigure.Height;
                float scale = Math.Min(scaleX, scaleY);

                // 2) Draw from the sprite's center (origin) so the xPosition/yPosition represent the sprite center
                Vector2 origin = new Vector2(actionFigure.Width * 0.5f, actionFigure.Height * 0.5f);

                _spriteBatch.Draw(
                    actionFigure,
                    new Vector2(xPosition, yPosition),
                    null,                 // source rectangle (null = whole texture)
                    Color.White,
                    0f,                   // rotation
                    origin,               // origin -> center
                    scale,                // uniform scale
                    SpriteEffects.None,
                    0f                    // layer depth
                );
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
