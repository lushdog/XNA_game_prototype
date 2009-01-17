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
        private List<Player> players;
        private List<Target> targets;
        
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
            players = new List<Player>();
            targets = new List<Target>();
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
            LoadPlayers();
            LoadTargets();
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
            // TODO: Add your update logic here
            foreach (Player player in players)
            {                
                player.UpdatePlayerIsActive();
                if (player.PlayerActorStates.Contains(PlayerActorState.Active))
                {
                    player.UpdatePauseState();
                    if (player.PlayerActorStates.Contains(PlayerActorState.Paused))
                        this.Exit();

                    player.UpdateFiringState();
                    if (player.PlayerActorStates.Contains(PlayerActorState.Firing))
                    {
                        foreach (Target target in targets)
                        {
                            //TODO: upgrade this from ghetto hit detection to alpha sprite based hit detection
                            if (target.BoundingBox.Contains(new Rectangle((int)player.Position.X, (int)player.Position.Y, 1, 1)))
                            {                                
                                target.AIActorStates.Remove(AIActorState.Active);
                            }
                        }
                    }

                    player.UpdatePlayerPosition();
                    //TODO: here is where we'd check for collisions or something and reupdate player pos
                    player.MoveTo((int)player.Position.X, (int)player.Position.Y);
                }
            }

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
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.None);
            spriteBatch.Draw(backgroundTexture, viewportRectangle, null, Color.White, 0.0f, new Vector2(0,0), SpriteEffects.None, 1.0f );

            //Draw players
            foreach (Player player in players)
            {
                if (player.PlayerActorStates.Contains(PlayerActorState.Active))
                {
                    Color playerColor = Color.White;
                    if (player.PlayerActorStates.Contains(PlayerActorState.Firing))
                    {
                        playerColor = Color.Red;
                    }
                    spriteBatch.Draw(player.Sprite, player.Position, null, playerColor, player.Rotation, player.Origin, 1.0f, SpriteEffects.None, 0.0f);
                }
            }

            //update targets
            foreach (AIActor target in targets)
            {
                if (target.AIActorStates.Contains(AIActorState.Active))
                {   
                    spriteBatch.Draw(target.Sprite, target.Position, null, Color.White, target.Rotation, target.Origin, 1.0f, SpriteEffects.None, 0.5f);
                }
            }
                
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void LoadPlayers()
        {
#if !XBOX
            PlayerInput[] playerInputs = LoadPCPlayerInputs(); // move to Initialize?
#endif
#if XBOX
            PlayerInput[] playerInputs = LoadXBOXPlayerInputs(); // move to Initialize?
#endif
            int playerNumber = 1;
            foreach (PlayerInput playerInput in playerInputs)
            {
                players.Add(LoadPlayer(playerInput, playerNumber));
                playerNumber += 1;
            }
        }

        private Player LoadPlayer(PlayerInput playerInput, int playerNumber)
        {
            Texture2D playerTexture = this.Content.Load<Texture2D>("sprites\\crosshair");
            Player playerActor = new Player(playerInput, playerTexture, playerNumber, new Vector2(viewportRectangle.Width, viewportRectangle.Height));
            playerActor.Position = new Vector2(playerActor.MaxPosition.X / 2, playerActor.MaxPosition.Y / 2);
            return playerActor;
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
                        playerInputs[i] = new KeyboardInput(scrollSpeed);
                    }
                    else if (String.Compare(activeInputAttribute, "Wiimote", true) == 0)
                    {
                        //if we have Wiimote, Mouse, Wiimote, 
                        //the last Wiimote is player 3 but Wiimote index 1.
                        int numWiimotePlayers = 0;
                        foreach (PlayerInput input in playerInputs)
                        {
                            if (input is WiiInput)
                            {
                                numWiimotePlayers += 1;
                            }
                        }
                        playerInputs[i] = new WiiInput(numWiimotePlayers);
                    }
                    else if (String.Compare(activeInputAttribute, "Mouse", true) == 0)
                    {
                        playerInputs[i] = new MouseInput();
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
                        playerInputs[i] = new GamepadInput(NumToEnum<PlayerIndex>(numGamepadPlayers), scrollSpeed);
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
                    playerInputs[i] = new GamepadInput(NumToEnum<PlayerIndex>(i), scrollSpeed);
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

        //TODO: Different Targets will inherit from AIActor and Levels will contain multiple Waves which are made up of targets
        private void LoadTargets()
        {
            targets.Add(LoadTarget());
        }

        private Target LoadTarget()
        {
            AlienTarget target = new AlienTarget(new Vector2(viewportRectangle.Width, viewportRectangle.Height));
            target.Sprite = this.Content.Load<Texture2D>(target.SpritePath);
            target.Position = new Vector2(200, 400);
            target.AIActorStates.Add(AIActorState.Active);
            return target;
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
