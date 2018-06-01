using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheDeserter.Content;

namespace TheDeserter
{
    /// <summary>
    /// The main controllable character
    /// </summary>
    class Player : Entity
    {
        #region Singleton
        /// <summary>
        /// Singleton allows the player to be accessed from anywhere
        /// </summary>
        private static Player _instance;
        public static Player Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        #region Variables
        
        private Vector2 _tilePosition; //The position in the tile grid
        private float _jumpStrength; // The strength of the jump
        private float reactivity; // A percentage that makes it easier to turn
        SpriteEffects textureDirection = SpriteEffects.None; // A SpriteEffect that makes flipping the texture possible
        private bool isGrounded = true; //Checks if the character is standing on some type of ground
        private bool hasJumped = false; //Checks if the character has jumped / fell off an object

        private float LEDGETIMER = 500f; // The time before you cant jump after fallen off a ledge
        private float ledgeTimer; // The timer component

        Texture2D idleTexture; // The animation that plays when the player is idle
        Texture2D runningTexture; // The animation that plays when the player is running


        #endregion
        #region Properties
        //Property for the players tile position
        public Vector2 TilePosition
        {
            get { return _tilePosition; }
            set { _tilePosition = value; }
        }
        //Property for the players jump strength
        public float JumpStrength
        {
            get { return _jumpStrength; }
            set { _jumpStrength = value; }
        }
        #endregion 

        /// <summary>
        /// Constructor of the player
        /// </summary>
        /// <param name="idleTexture">the texture used when idling</param>
        /// <param name="runningTexture">the texture used when running</param>
        /// <param name="position">the starting position of the </param>
        public Player(Texture2D idleTexture, Texture2D runningTexture, Vector2 position) : base(idleTexture, position)
        {
            MovementSpeed = 150f; 
            reactivity = 0.5f; 
            JumpStrength = 400f;
            this.idleTexture = idleTexture;
            this.runningTexture = runningTexture;
            ledgeTimer = LEDGETIMER;
            //Setting the singleton
            _instance = this;
        }
        #region Functions
        /// <summary>
        /// Jumping 
        /// </summary>
        public void Jump()
        {
            //Makes sure that i cant jump again when ive already jumped
            if (!hasJumped)
            {
                Velocity = new Vector2(Velocity.X, -JumpStrength);
                hasJumped = true;
                isGrounded = false;
            }
        }

        /// <summary>
        /// Checks if the jump button has been released during the jump
        /// </summary>
        public void JumpReleased()
        {
            //Makes sure that the player actually has jumped
            if (hasJumped)
            {
                if(Velocity.Y < 0)
                {
                    //Could be smoother with a timed deceleration
                    Velocity = new Vector2(Velocity.X, 0);
                }
            }
        }

        /// <summary>
        /// responsible for moving the character
        /// </summary>
        /// <param name="gameTime">the game time of the program</param>
        public override void Move(GameTime gameTime)
        {
            base.Move(gameTime);

            //The tile position is the position divided by the tile size
            TilePosition = Position / Constants.TileSize;

            CheckWorldConstraints(); // Checks if the player is outside the world and corrects the position
            CheckNeighbouringTiles(gameTime); // checks if the neigbouring tiles are colliding with the character

            //Adds friction if no movement buttons are pressed
            if (!InputManager.Instance.KeyDown(Keys.A, Keys.D, Keys.Left, Keys.Right))
            {
                Velocity -= new Vector2(Constants.FrictionForce * Velocity.X / MovementSpeed * (float)gameTime.ElapsedGameTime.Milliseconds / 1000, 0);

            }
            //Sets the velocity to 0 if you are moving to slow
            if(Math.Abs(Velocity.X) < 0.5f)
            {
                Velocity = new Vector2(0, Velocity.Y);
            }
            //Displays running to the right texture
            if(Velocity.X > 0)
            {
                Texture = runningTexture;
                textureDirection = SpriteEffects.None;
                TIMER = 100f;

            }
            //Displays running to the left
            else if(Velocity.X < 0)
            {
                Texture = runningTexture;
                textureDirection = SpriteEffects.FlipHorizontally;
                TIMER = 100f;
            }
            //Display idle
            else
            {
                Texture = idleTexture;
                TIMER = 500f;
            }
        }

        /// <summary>
        /// Checks for inputs from the user in the purpose of moving the character
        /// </summary>
        public void MovementInput()
        {
            //Checks movement to the left
            if(InputManager.Instance.KeyDown(Keys.A, Keys.Left))
            {
                // If the character is moving to the right, then their directionchange is helped a little bit by the reactivity
                if (Velocity.X > 0)
                {
                    Velocity -= Vector2.UnitX * MovementSpeed * (1 + reactivity);
                }
                else
                {
                    Velocity -= Vector2.UnitX * MovementSpeed;
                }
            }

            //Checks movement to the right
            if (InputManager.Instance.KeyDown(Keys.D, Keys.Right))
            {
                if(Velocity.X < 0)
                {
                    // If the character is moving to the right, then their directionchange is helped a little bit by the reactivity
                    Velocity += Vector2.UnitX * MovementSpeed * (1 + reactivity);
                }
                else
                {
                    Velocity += Vector2.UnitX * MovementSpeed;
                }
            }

            //Checks for jumps
            if (InputManager.Instance.KeyDown(Keys.Space))
            {
                Jump();
            }
            //Checks if the jump has been aborted early
            if (InputManager.Instance.KeyReleased(Keys.Space))
            {
                JumpReleased();
            }

            //Limits the Velocity in the X and Y directions
            if(Math.Abs(Velocity.X) > MovementSpeed)
            {
                Velocity *= new Vector2(Math.Abs(MovementSpeed / Velocity.X), 1);
            }
            if(Math.Abs(Velocity.Y) > JumpStrength)
            {
                Velocity *= new Vector2(1, Math.Abs(JumpStrength / Velocity.Y));
            }
        }

        /// <summary>
        /// Checks if the player is leaving the game world and contains them
        /// </summary>
        private void CheckWorldConstraints()
        {
            //Checks position X
            if (Position.X < 0 || Position.X > (World.MapWidth - 1) * Constants.TileSize)
            {
                Position = new Vector2(oldPosition.X, Position.Y);
                Velocity = new Vector2(0, Velocity.Y);
            }

            //checks position y
            if (Position.Y < 0 || Position.Y > (World.MapHeight - 1) * Constants.TileSize)
            {
                //The character is standing at the bottom of the screen
                if(Position.Y > (World.MapHeight - 1) * Constants.TileSize)
                {
                    isGrounded = true;
                    ledgeTimer = LEDGETIMER;
                }
                Position = new Vector2(Position.X, oldPosition.Y);
                Velocity = new Vector2(Velocity.X, 0);
            }
        }

        /// <summary>
        /// Checks the tiles surrounding the character for collisions.
        /// </summary>
        /// <param name="gameTime">The gametime object</param>
        private void CheckNeighbouringTiles(GameTime gameTime)
        {
            //Debug.WriteLine("Velocity.X = " + Velocity.X * (float)gameTime.ElapsedGameTime.Milliseconds / 1000);
            //Debug.WriteLine("Velocity.Y = " + Velocity.Y * (float)gameTime.ElapsedGameTime.Milliseconds / 1000);

            //Checks the tile undernieth the tile that the character is currently located in
            if (World.CollisionTileMap.LayerTileMap[(int)Math.Floor(TilePosition.X), (int)Math.Floor(TilePosition.Y) + 1].CheckCollision(Position))
            {
                isGrounded = true;
                ledgeTimer = LEDGETIMER;
            }
            //Applies a delay to setting isGround = true
            else
            {
                ledgeTimer -= gameTime.ElapsedGameTime.Milliseconds;
                if(ledgeTimer <= 0)
                    isGrounded = false;
            }

            /*Loops through every tile in a surrounding grid like:
             *                  [1]  [4]  [7]
             *                  [2]  [@]  [8]
             *                  [3]  [6]  [9]
             * Where @ is the main charcter
             */
            for (int i = (int)Math.Floor(TilePosition.X) - 1; i <= (int)Math.Floor(TilePosition.X) + 1; i++)
            {
                for(int j = (int)Math.Floor(TilePosition.Y) - 1; j <= (int)Math.Floor(TilePosition.Y) + 1; j++)
                {
                    //Checks if the coordinates are the same as the ones that the character is located in
                    if (!((i == (int)Math.Floor(TilePosition.X)) && (j == (int)Math.Floor(TilePosition.Y))))
                    {
                        //Makes sure that i and j are not outside the layer tile map array
                        if(!(i < 0 || j < 0 || i > (World.MapWidth - 1) || j > (World.MapHeight - 1)))
                        {
                            //Check collision X movement
                            if (World.CollisionTileMap.LayerTileMap[i, j].CheckCollision(new Vector2(Position.X, oldPosition.Y)))
                            {  
                                Position = new Vector2(oldPosition.X, Position.Y);
                                Velocity = new Vector2(0, Velocity.Y);
                            }
                            //Check collision Y Movement
                            if (World.CollisionTileMap.LayerTileMap[i, j].CheckCollision(new Vector2(oldPosition.X, Position.Y)))
                            {
                                Position = new Vector2(Position.X, oldPosition.Y);
                                Velocity = new Vector2(Velocity.X, 0);
                            } 
                        }
                    }                    
                }
            }
        }

        /// <summary>
        /// Main update function of the player
        /// </summary>
        /// <param name="gameTime">yhe gametime object</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //Checks if the character is grounded and has jumped then they should be able to jump again
            if (isGrounded && hasJumped)
                hasJumped = false;
            else if (!isGrounded)
                hasJumped = true;
        }
        /// <summary>
        /// The main draw function for the Player
        /// </summary>
        /// <param name="spriteBatch">The sprite batch object</param>
        /// <param name="gameTime">The gametime object</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Animate(gameTime);
            spriteBatch.Draw(Texture, Position, new Rectangle(16 * CurrentFrame, 0, 16, 16), Color.White, 0, Vector2.Zero, 1, textureDirection, 0);
        }
        #endregion
    }
}
