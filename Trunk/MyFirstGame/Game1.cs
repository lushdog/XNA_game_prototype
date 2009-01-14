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
using System.Xml;

namespace MyFirstGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private Rectangle viewportRectangle;
        private Texture2D backgroundTexture;
        private Texture2D crosshairTexture;
        private SpriteBatch spriteBatch;
        private GameObject crosshair;
        private Input activeInput;
        private float scroll_speed = 4.0f;  //TODO: move to config file, move to property of Crosshair object
       
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
            LoadConfigFile();
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

            LoadBackground();

            //TODO: refactor to LoadCrosshair()
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
            
            //TODO: refactor this to GetInputState()
            float moveX = activeInput.GetX(); 
            float moveY = activeInput.GetY();

            //TODO: movement is handled by GameObject class
            float posX = 0.0f;
            float posY = 0.0f;

            if (activeInput is GamepadInput)
            {
                posX = crosshair.position.X + (moveX * scroll_speed);
                posY = crosshair.position.Y - (moveY * scroll_speed);
            }
#if !XBOX
            else if (activeInput is MouseInput)
            {
                posX = moveX;
                posY = moveY;
            }
            else if (activeInput is KeyboardInput)
            {
                posX = crosshair.position.X - (moveX * scroll_speed);
                posY = crosshair.position.Y - (moveY * scroll_speed);
            }
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
            //Console.WriteLine("PosX = " + posX.ToString());
            //Console.WriteLine("PosY = " + posY.ToString());
            //Console.WriteLine("CrosshairX = " + crosshair.position.X.ToString());
            //Console.WriteLine("CrosshairY = " + crosshair.position.Y.ToString());
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

        /// <summary>
        /// Loads 'config.xml' file from executing assembly's directory
        /// </summary>
        private void LoadConfigFile()
        {
            XmlDocument configDocument = new XmlDocument();
            configDocument.Load("config.xml");
            XmlNode inputNode = configDocument.SelectSingleNode("/config/input");
            try
            {
#if XBOX    
                activeInput = new GamepadInput();
#endif
#if !XBOX
                string activeInputAttribute = inputNode.Attributes["activeInput"].Value;
                if (String.Compare(activeInputAttribute, "Keyboard", true) == 0)
                {
                    activeInput = new KeyboardInput();
                }
                else if (String.Compare(activeInputAttribute, "Wiimote", true) == 0)
                {
                    activeInput = new WiiInput();
                }
                else if (String.Compare(activeInputAttribute, "Mouse", true) == 0)
                {
                    activeInput = new MouseInput();
                }
                else
                {
                    activeInput = new GamepadInput();
                }
#endif
                scroll_speed = float.Parse(inputNode.Attributes["scrollSpeed"].Value);
            }
            catch (Exception ex)
            {
                throw new Exception("config.xml does not contain the 'input' attribute.");
            }
            finally
            {
                //TODO: do we have to dispose of XmlDocument?
            }
        }

        //TODO: Extend this to load backgrounds for new levels or in response to actions
        private void LoadBackground()
        {
            viewportRectangle = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            backgroundTexture = this.Content.Load<Texture2D>("sprites\\background");
        }

    }
}
