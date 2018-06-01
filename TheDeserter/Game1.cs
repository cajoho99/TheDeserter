using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml.Linq;

namespace TheDeserter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        #region Enumerations
        public enum GameState
        {
            Running,
            MainMenu
        };
        #endregion

        #region Variables
        //The main graphics device manager in the game
        GraphicsDeviceManager graphics;
        //The main spritebatch for the game
        SpriteBatch spriteBatch;
        //The target image that is rendered to
        RenderTarget2D target;
        RenderTarget2D mainMenuTarget;

        //The main character player object
        Player mainCharacter;

        //The background color
        private Color backgroundColor;

        //The main camera
        Camera mainCamera;

        //The game world
        private World world;

        //The current Game state
        public GameState gameState;

        //Main menu
        MainMenu mainMenu;
        #endregion
        /// <summary>
        /// Constructor for the main game object
        /// </summary>
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
            //Set the starting location of the game to the top left corner and removes the title
            var form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(this.Window.Handle);
            form.Location = new System.Drawing.Point(0, 0);
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            gameState = GameState.MainMenu;

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

            //Initialize main menu
            mainMenu = new MainMenu(Content);



            //Initializing the World
            world = new World();
            world.LoadWorld("level2", Content);

           
            //Setting the render target
            target = new RenderTarget2D(GraphicsDevice, World.MapWidth * 16, World.MapHeight * 16);
            mainMenuTarget = new RenderTarget2D(GraphicsDevice, mainMenu.MenuSize.Width, mainMenu.MenuSize.Height);
            //Changing the window size
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            //Uncomment this for true fullscreen
            //graphics.IsFullScreen = true; 
            //Applies all the graphic changes
            graphics.ApplyChanges();

            //Initialize the camera
            mainCamera = new Camera();

            //Initialize the Player
            mainCharacter = new Player(Content.Load<Texture2D>("playerIdle"), Content.Load<Texture2D>("runningPlayer"), new Vector2(3, 3) * Constants.TileSize);

            //assign the background color
            backgroundColor = new Color(29, 33, 45);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Exits the game on esc...
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                gameState = GameState.MainMenu;

            //Update the Input manager
            InputManager.Instance.Update();

            //Only update character and camera if the game is running
            if(gameState == GameState.Running)
            {
                //Update the main character
                mainCharacter.MovementInput();
                mainCharacter.Move(gameTime);
                mainCharacter.Update(gameTime);

                //Update the camera
                mainCamera.Update(GraphicsDevice);
            }
            else if(gameState == GameState.MainMenu)
            {
                mainMenu.Update(this);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if(gameState == GameState.Running)
            {
                //Drawing to the image
                GraphicsDevice.SetRenderTarget(target);
                // Draw with the camera
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, mainCamera.Transform);
                GraphicsDevice.Clear(backgroundColor);
                world.DrawLayers(spriteBatch); // Draw the world
                mainCharacter.Draw(spriteBatch, gameTime); //Draw the main character
                spriteBatch.End();
                GraphicsDevice.SetRenderTarget(null);

                //Draw the background color
                GraphicsDevice.Clear(backgroundColor);
                //Draw the target to the screen
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
                spriteBatch.Draw(target, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height), Color.White);
                spriteBatch.End();
            }
            else if(gameState == GameState.MainMenu)
            {
                //Drawing to the image
                GraphicsDevice.SetRenderTarget(mainMenuTarget);
                // Draw with the camera
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
                GraphicsDevice.Clear(backgroundColor);
                mainMenu.Draw(spriteBatch);
                spriteBatch.End();
                GraphicsDevice.SetRenderTarget(null);

                //Draw the background color
                GraphicsDevice.Clear(backgroundColor);
                //Draw the target to the screen
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
                spriteBatch.Draw(mainMenuTarget, new Rectangle(
                    (GraphicsDevice.DisplayMode.Width - mainMenu.MenuSize.Width) / 2, 
                    (GraphicsDevice.DisplayMode.Height - mainMenu.MenuSize.Height) / 2,
                    mainMenu.MenuSize.Width, 
                    mainMenu.MenuSize.Height), 
                    Color.White);
                spriteBatch.End();
            }
            

            base.Draw(gameTime);
        }
    }
}
