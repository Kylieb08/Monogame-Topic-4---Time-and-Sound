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
        Rectangle bombRect, explosionRect, redButtonRect, pliersRect, redWireRect, greenWireRect;
        Texture2D bombTexture, explosionTexture, pliersTexture, bombRedWireCutTexture, bombGreenWireCutTexture;
        float seconds;
        SoundEffect explosion;
        SoundEffectInstance explosionInstance;
        MouseState mouseState;
        bool exploded = false, disabled = false, greenWireCut = false;

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

            IsMouseVisible = false;

            bombRect = new Rectangle(50, 50, 700, 400);
            explosionRect = new Rectangle(0, 0, 800, 500);
            redButtonRect = new Rectangle(253, 133, 10, 13);
            pliersRect = new Rectangle(0, 0, 150, 125);
            redWireRect = new Rectangle(490, 180, 38, 15);
            greenWireRect = new Rectangle(496, 157, 100, 22);

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
            explosionInstance = explosion.CreateInstance();
            explosionTexture = Content.Load<Texture2D>("explosion no bg");
            pliersTexture = Content.Load<Texture2D>("Pliers");
            //Images for cut wires were made by Sarah
            bombRedWireCutTexture = Content.Load<Texture2D>("bombRedWireCut");
            bombGreenWireCutTexture = Content.Load<Texture2D>("bombGreenWireCut");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (exploded == true && explosionInstance.State == SoundState.Stopped)
                Exit();

            // TODO: Add your update logic here
            mouseState = Mouse.GetState();
            pliersRect.Location = mouseState.Position;

            this.Window.Title = "x = " + mouseState.X + ", y = " + mouseState.Y;

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (redButtonRect.Contains(mouseState.Position))
                {
                    seconds = 0;
                    exploded = false;
                }

                if (redWireRect.Contains(mouseState.Position))
                {
                    disabled = true;
                }

               if (greenWireRect.Contains(mouseState.Position))
               {
                    greenWireCut = true;
               }
            }

            if (!exploded && !disabled)
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (seconds > 10)
            {
                explosionInstance.Play();
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

            if (!exploded && !disabled)
            {
                _spriteBatch.Draw(bombTexture, bombRect, Color.White);
                _spriteBatch.DrawString(bombFont, (10 - seconds).ToString("0:00"), new Vector2(270, 200), Color.Black);
                _spriteBatch.Draw(pliersTexture, pliersRect, Color.White);
            }

            else if (!exploded && disabled)
            {
                _spriteBatch.Draw(bombRedWireCutTexture, bombRect, Color.White);
                _spriteBatch.DrawString(bombFont, (10 - seconds).ToString("0:00"), new Vector2(270, 200), Color.Black);
                _spriteBatch.Draw(pliersTexture, pliersRect, Color.White);
            }

            else if (greenWireCut && !exploded && !disabled)
            {
                seconds = 8 + ((float)gameTime.ElapsedGameTime.TotalSeconds);
                _spriteBatch.Draw(bombGreenWireCutTexture, bombRect, Color.White);
                _spriteBatch.DrawString(bombFont, (10 - seconds).ToString("0:00"), new Vector2(270, 200), Color.Black);
                _spriteBatch.Draw(pliersTexture, pliersRect, Color.White);
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
