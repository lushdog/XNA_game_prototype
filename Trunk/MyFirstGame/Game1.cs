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
using MyFirstGame.GameObject;
using MyFirstGame.GameInput;

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
        private SpriteBatch spriteBatch;
        private List<PlayerActor> players; 
        
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
            players = new List<PlayerActor>();
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
#if !XBOX
            PlayerInput[] playerInputs = LoadPCPlayerInputs(); // move to Initialize?
#endif
#if XBOX
            PlayerInput[] playerInputs = LoadXBOXPlayerInputs(); // move to Initialize?
#endif
            //TODO: we shouldn't load player until they press start
            foreach(PlayerInput playerInput in playerInputs)
            {
                players.Add(LoadPlayer(playerInput));
            }
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

            foreach (PlayerActor player in players)
            {
                player.UpdateInput(viewportRectangle.Width, viewportRectangle.Height);
            }
            
            //TODO: refactor this to GetInputState()
            
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

            //Draw players
            //TODO: Only draw active players
            foreach (PlayerActor player in players)
            {
                spriteBatch.Draw(player.Sprite, player.Position, null, Color.White, player.Rotation, player.Origin, 2.0f, SpriteEffects.None, 0);
            }
                
            spriteBatch.End();

            base.Draw(gameTime);
        }

#if !XBOX
        private PlayerInput[] LoadPCPlayerInputs()
        {
            PlayerInput[] playerInputs = new PlayerInput[4];
            XmlDocument configDocument = new XmlDocument();
            configDocument.Load(".//Config//PCconfig.xml");
            XmlNodeList inputNodes = configDocument.SelectNodes("/config/input");
            try
            {
                for (int i = 0; i < playerInputs.Length; i++)
                {
                    XmlNode inputNode = inputNodes[i];
                    string activeInputAttribute = inputNode.Attributes["activeInput"].Value;
                    float scrollSpeed; 

                    if (String.Compare(activeInputAttribute, "Keyboard", true) == 0)
                    {
                        scrollSpeed = float.Parse(inputNode.Attributes["scrollSpeed"].Value);
                        playerInputs[i] = new KeyboardInput(i + 1, scrollSpeed);
                    }
                    else if (String.Compare(activeInputAttribute, "Wiimote", true) == 0)
                    {
                        //if we have Wiimote, Mouse, Wiimote, 
                        //the last Wiimote is player 3 but Wiimote index 1.
                        int numWiimotePlayers = 0;
                        foreach(PlayerInput input in playerInputs)
                        {
                            if (input is WiiInput)
                            {
                                numWiimotePlayers += 1;
                            }
                        }
                        playerInputs[i] = new WiiInput(i + 1, numWiimotePlayers);
                    }
                    else if (String.Compare(activeInputAttribute, "Mouse", true) == 0)
                    {
                        playerInputs[i] = new MouseInput(i + 1);
                    }
                    else
                    {
                        //if we have Gamepad, Mouse, Gamepad
                        //the last Gamepad is player 3 but Gamepad PlayerIndex.Two.
                        int numGamepadPlayers = 0;
                        foreach (PlayerInput input in playerInputs)
                        {
                            if (input is GamepadInput)
                            {
                                numGamepadPlayers += 1;
                            }
                        }
                        scrollSpeed = float.Parse(inputNode.Attributes["scrollSpeed"].Value);
                        playerInputs[i] = new GamepadInput(i + 1, NumToEnum<PlayerIndex>(numGamepadPlayers), scrollSpeed);
                    }
                }
                return playerInputs;                
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //TODO: do we have to dispose of XmlDocument?
            }
        }
#endif

#if XBOX
        private PlayerInput[] LoadXBOXPlayerInputs()
        {
            PlayerInput[] playerInputs = new PlayerInput[4];
            XmlDocument configDocument = new XmlDocument();
            configDocument.Load(".//Config//XBOXconfig.xml");
            XmlNodeList inputNodes = configDocument.SelectNodes("/config/input");
            try
            {
                for (int i = 0; i < playerInputs.Length; i++)
                {
                    XmlNode inputNode = inputNodes[i];
                    float scrollSpeed = float.Parse(inputNode.Attributes["scrollSpeed"].Value);
                    playerInputs[i] = new GamepadInput(i + 1, NumToEnum<PlayerIndex>(i), scrollSpeed);
                }
                return playerInputs;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //TODO: do we have to dispose of XmlDocument?
            }
        }

#endif

        private PlayerActor LoadPlayer(PlayerInput playerInput)
        {
            Texture2D playerTexture = this.Content.Load<Texture2D>("sprites\\crosshair");
            PlayerActor playerActor = new PlayerActor(playerInput, playerTexture);
            playerActor.Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            return playerActor;
        }               

        //TODO: Extend this to load backgrounds for new levels or in response to actions
        private void LoadBackground()
        {
            viewportRectangle = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            backgroundTexture = this.Content.Load<Texture2D>("sprites\\background");
        }

        public T NumToEnum<T>(int number)
        {
            return (T)Enum.ToObject(typeof(T), number);
        }
    }
}
