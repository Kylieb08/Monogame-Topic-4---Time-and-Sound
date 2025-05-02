using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Topic_4___Time_and_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SpriteFont bombFont;
        Rectangle bombRect, explosionRect, redButtonRect;
        Texture2D bombTexture, explosionTexture;
        float seconds;
        SoundEffect explosion;
        MouseState mouseState;
        bool exploded = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;

            bombRect = new Rectangle(50, 50, 700, 400);
            explosionRect = new Rectangle(0, 0, 800, 500);
            redButtonRect = new Rectangle(253, 133, 10, 13);

            seconds = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bombTexture = Content.Load<Texture2D>("bomb");
            bombFont = Content.Load<SpriteFont>("bombFont");
            explosion = Content.Load<SoundEffect>("explosion");
            explosionTexture = Content.Load<Texture2D>("explosion no bg");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (exploded == true)
            {
                
                Exit();
            }

            // TODO: Add your update logic here
            mouseState = Mouse.GetState();
            this.Window.Title = "x = " + mouseState.X + ", y = " + mouseState.Y;

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (redButtonRect.Contains(mouseState.Position))
                {
                    seconds = 0;
                    exploded = false;
                }
            }

            if (! exploded)
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (seconds > 10)
            {
                explosion.Play();
                exploded = true;
                seconds = 10;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (!exploded)
            {
                _spriteBatch.Draw(bombTexture, bombRect, Color.White);
                _spriteBatch.DrawString(bombFont, (10 - seconds).ToString("0:00"), new Vector2(270, 200), Color.Black);
            }
            else
            {
                _spriteBatch.Draw(explosionTexture, explosionRect, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
