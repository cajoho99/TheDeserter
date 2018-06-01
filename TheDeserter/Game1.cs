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
        #region Variables
        //The main graphics device manager in the game
        GraphicsDeviceManager graphics;
        //The main spritebatch for the game
        SpriteBatch spriteBatch;
        //The target image that is rendered to
        RenderTarget2D target;

        //The main character player object
        Player mainCharacter;

        //The background color
        private Color backgroundColor;

        //The main camera
        Camera mainCamera;

        //The game world
        private World world;

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
            //Initializing the World
            world = new World();
            world.LoadWorld("level2", Content);

           
            //Setting the render target
            target = new RenderTarget2D(GraphicsDevice, World.MapWidth * 16, World.MapHeight * 16);
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
                Exit();

            //Update the Input manager
            InputManager.Instance.Update();

            //Update the main character
            mainCharacter.MovementInput();
            mainCharacter.Move(gameTime);
            mainCharacter.Update(gameTime);

            //Update the camera
            mainCamera.Update(GraphicsDevice);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
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

            base.Draw(gameTime);
        }
    }
}
