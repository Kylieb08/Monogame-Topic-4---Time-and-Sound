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
        Rectangle bombRect;
        Texture2D bombTexture;
        float seconds;
        SoundEffect explosion;
        MouseState mouseState;

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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                seconds = 0;
            }

            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (seconds > 10)
            {
                explosion.Play();
                seconds = 0;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(bombTexture, bombRect, Color.White);
            _spriteBatch.DrawString(bombFont, (10 - seconds).ToString("0:00"), new Vector2(270, 200), Color.Black);         

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
