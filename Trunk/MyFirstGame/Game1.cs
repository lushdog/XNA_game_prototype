using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace MyFirstGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        Rectangle viewportRectangle;
        Texture2D backgroundTexture;
        Texture2D crosshairTexture;
        SpriteBatch spriteBatch;
        GameObject crosshair;
        Input activeInput;
        float SCROLL_SPEED = 4.0f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            if (true)
            {
                activeInput = new GamepadInput();
            }
#if !XBOX
            else if (false)
            {
                activeInput = new KeyboardInput();
            }
            else
            {
                activeInput = new WiiInput();
            }
#endif

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            viewportRectangle = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            backgroundTexture = this.Content.Load<Texture2D>("sprites\\background");

            crosshairTexture = this.Content.Load<Texture2D>("sprites\\crosshair");
            crosshair = new GameObject(crosshairTexture);
            crosshair.position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit (Don't remove or you are stuck in XBOX360)
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            //TODO: refactor input processing
            float posX = 0.0f;
            float posY = 0.0f;
            float moveX = activeInput.GetX();
            float moveY = activeInput.GetY();

            if (activeInput is KeyboardInput)
            {
                posX = crosshair.position.X - (moveX * SCROLL_SPEED);
                posY = crosshair.position.Y - (moveY * SCROLL_SPEED);
            }
            else if (activeInput is GamepadInput)
            {
                posX = crosshair.position.X + (moveX * SCROLL_SPEED);
                posY = crosshair.position.Y - (moveY * SCROLL_SPEED);
            }
#if !XBOX
            else if (activeInput is WiiInput)
            {
                posX = moveX * (float)viewportRectangle.Width;
                posY = moveY * (float)viewportRectangle.Height;
            }
#endif
            crosshair.position.X = MathHelper.Clamp(posX, 0.0f, viewportRectangle.Width);
            crosshair.position.Y = MathHelper.Clamp(posY, 0.0f, viewportRectangle.Height);

#if DEBUG
            Console.WriteLine("MoveX = " + moveX.ToString());
            Console.WriteLine("MoveY = " + moveY.ToString());
            Console.WriteLine("PosX = " + posX.ToString());
            Console.WriteLine("PosY = " + posY.ToString());
            Console.WriteLine("CrosshairX = " + crosshair.position.X.ToString());
            Console.WriteLine("CrosshairY = " + crosshair.position.Y.ToString());
#endif

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            spriteBatch.Draw(backgroundTexture, viewportRectangle, Color.White);
            spriteBatch.Draw(crosshair.sprite, crosshair.position, null, Color.White, crosshair.rotation, crosshair.center, 2.0f, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
