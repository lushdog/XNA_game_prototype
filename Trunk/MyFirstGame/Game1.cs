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
using MyFirstGame.InputObject;
using MyFirstGame.LevelObject;
using MyFirstGame.References;

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
        //private List<Target> targets;
		private List<Sprite> sprites;
        private List<Level> levels;
        private int currentLevel;

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
            sprites = new List<Sprite>();
            levels = new List<Level>();

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

            //TODO: this is good as we only load up the references to the textures once, but could be better
            //maybe we could do this via reflection and custom class atrributes so sprite path is not hard coded
            //in logic
            //TODO:refactor to LoadTextures()
            Textures.Instance.AlienTexture = this.Content.Load<Texture2D>("sprites//alien");

            LoadBackground();
            LoadPlayers();
            LoadSprites();
            LoadLevels();            
			
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
            //TODO: this will not be hardcoded
            if (!levels[0].IsStarted)
            {
                levels[0].StartLevel(ref gameTime);
            }

            //TODO: JOE: If the game stops updating then its because of this, you are not crazy...
            if (!levels[0].IsEnded)
            {
                //TOREMOVE: splooge ah inheritance and polymorphism jizz all over the screen
                levels[0].UpdateLevel();

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
                            foreach (Target target in levels[0].Waves[levels[0].CurrentWave].Targets)
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
                    Color playerColor = player.SpriteColor;
                    if (player.PlayerActorStates.Contains(PlayerActorState.Firing))
                    {
                        playerColor = Color.Red;
                    }
                    spriteBatch.Draw(player.Sprite, player.Position, null, playerColor, player.Rotation, player.Origin, 1.0f, SpriteEffects.None, 0.0f);
                }
            }

            //update targets
            //TOREMOVE: This fucking line is where all the hierarchy and polymorphism makes me jizz
            //TODO: of course this has to be the current level blah blah
            foreach (Target target in levels[0].Waves[levels[0].CurrentWave].Targets)
            {
                if (target.AIActorStates.Contains(AIActorState.Active))
                {   
                    spriteBatch.Draw(target.Sprite, target.Position, null, Color.White, target.Rotation, target.Origin, 1.0f, SpriteEffects.None, 0.5f);
                }
            }
            
			//draw static sprites
			foreach (Sprite sprite in sprites)
			{
				spriteBatch.Draw(sprite.Image, sprite.Location, Color.White);
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
            //TODO: this should be done via different sprites
            Color playerColor = Color.White;
            switch (playerNumber)
            {
                case 1:
                    playerColor = Color.Brown;
                    break;
                case 2:
                    playerColor = Color.Blue;
                    break;
                case 3:
                    playerColor = Color.DimGray;
                    break;
                case 4:
                    playerColor = Color.Green;
                    break;
            }
            Player player = new Player(playerInput, playerColor, playerNumber, new Vector2(viewportRectangle.Width, viewportRectangle.Height));
            player.Sprite = this.Content.Load<Texture2D>(player.SpritePath);
            player.Position = new Vector2(player.MaxPosition.X / 2, player.MaxPosition.Y / 2);
            return player;
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

       	private void LoadSprites()
		{
			sprites.Add(LoadSprite());
		}

		private Sprite LoadSprite()
		{
			Sprite sprite = new Sprite();
			sprite.ImagePath = "sprites\\testimage";
			sprite.Image = this.Content.Load<Texture2D>(sprite.ImagePath);
			sprite.X = 300;
			sprite.Y = 20;
			return sprite;
		}

        private void LoadLevels()
        {
            levels.Add(new FirstLevel());
        }

        //private void LoadTextures()
        //{
        //    alienTexture = backgroundTexture = this.Content.Load<Texture2D>("sprites\\alien");
        //}

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
