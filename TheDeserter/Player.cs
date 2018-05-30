using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheDeserter.Content;

namespace TheDeserter
{
    class Player : Entity
    {
        private Vector2 _tilePosition;
        private float _jumpHeight;
        private float reactivity;
        SpriteEffects textureDirection = SpriteEffects.None;
        private bool isGrounded = true;
        private bool hasJumped = false;

        Texture2D idleTexture;
        Texture2D runningTexture;

        public Vector2 TilePosition
        {
            get { return _tilePosition; }
            set { _tilePosition = value; }
        }
        public float JumpHeight
        {
            get { return _jumpHeight; }
            set { _jumpHeight = value; }
        }

        public Player(Texture2D idleTexture, Texture2D runningTexture, Vector2 position) : base(idleTexture, position)
        {
            MovementSpeed = 150f;
            reactivity = 0.5f;
            JumpHeight = 200f;
            this.idleTexture = idleTexture;
            this.runningTexture = runningTexture;
        }

        public void Jump()
        {
            if (!hasJumped )
            {
                Velocity = new Vector2(Velocity.X, -JumpHeight);
                hasJumped = true;
                isGrounded = false;
            }
        }

        public override void Move(GameTime gameTime)
        {
            base.Move(gameTime);

            TilePosition = Position / Constants.TileSize;

            CheckWorldConstraints();
            CheckNeighbouringTiles();
            if (!InputManager.Instance.KeyDown(Keys.A, Keys.D, Keys.Left, Keys.Right))
            {
                Velocity -= new Vector2(Constants.FrictionForce * Velocity.X / MovementSpeed * (float)gameTime.ElapsedGameTime.Milliseconds / 1000, 0);

            }
            if(Math.Abs(Velocity.X) < 0.5f)
            {
                Velocity = new Vector2(0, Velocity.Y);
            }

            if(Velocity.X > 0)
            {
                Texture = runningTexture;
                textureDirection = SpriteEffects.None;
                TIMER = 100f;

            }
            else if(Velocity.X < 0)
            {
                Texture = runningTexture;
                textureDirection = SpriteEffects.FlipHorizontally;
                TIMER = 100f;
            }
            else
            {
                Texture = idleTexture;
                TIMER = 500f;
            }
        }

        public void MovementInput()
        {
            if(InputManager.Instance.KeyDown(Keys.A, Keys.Left))
            {
                if (Velocity.X > 0)
                {
                    Velocity -= Vector2.UnitX * MovementSpeed * (1 + reactivity);
                }
                else
                {
                    Velocity -= Vector2.UnitX * MovementSpeed;
                }
            }

            if (InputManager.Instance.KeyDown(Keys.D, Keys.Right))
            {
                if(Velocity.X < 0)
                {
                    Velocity += Vector2.UnitX * MovementSpeed * (1 + reactivity);
                }
                else
                {
                    Velocity += Vector2.UnitX * MovementSpeed;
                }
            }

            if (InputManager.Instance.KeyDown(Keys.Space))
            {
                Jump();
            }

            if(Math.Abs(Velocity.X) > MovementSpeed)
            {
                Velocity *= new Vector2(Math.Abs(MovementSpeed / Velocity.X), 1);
            }
        }

        private void CheckWorldConstraints()
        {
            if (Position.X < 0 || Position.X > (World.MapWidth - 1) * Constants.TileSize)
            {
                Position = new Vector2(oldPosition.X, Position.Y);
                Velocity = new Vector2(0, Velocity.Y);
            }

            if (Position.Y < 0 || Position.Y > (World.MapHeight - 1) * Constants.TileSize)
            {
                if(Position.Y > (World.MapHeight - 1) * Constants.TileSize)
                {
                    isGrounded = true;
                }
                Position = new Vector2(Position.X, oldPosition.Y);
                Velocity = new Vector2(Velocity.X, 0);
            }
        }

        private void CheckNeighbouringTiles()
        {
            for(int i = (int)Math.Floor(TilePosition.X) - 1; i <= (int)Math.Floor(TilePosition.X) + 1; i++)
            {
                for(int j = (int)Math.Floor(TilePosition.Y) - 1; j <= (int)Math.Floor(TilePosition.Y) + 1; j++)
                {
                    if (!((i == (int)Math.Floor(TilePosition.X)) && (j == (int)Math.Floor(TilePosition.Y))))
                    {
                        if(!(i < 0 || j < 0 || i > (World.MapWidth - 1) || j > (World.MapHeight - 1)))
                        {
                            if (World.CollisionTileMap.LayerTileMap[i, j].CheckCollision(Position))
                            {
                                if (World.CollisionTileMap.LayerTileMap[(int)Math.Floor(TilePosition.X), (int)Math.Floor(TilePosition.Y) + 1].CheckCollision(Position))
                                {
                                    isGrounded = true;
                                }
                                Position = new Vector2(oldPosition.X, Position.Y);
                                Velocity = new Vector2(0, Velocity.Y);
                            }
                            if (World.CollisionTileMap.LayerTileMap[i, j].CheckCollision(Position))
                            {
                                 Position = new Vector2(Position.X, oldPosition.Y);
                                 Velocity = new Vector2(Velocity.X, 0);

                            }
                           
                        }
                    }                    
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            if (isGrounded && hasJumped)
                hasJumped = false;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Animate(gameTime);
            spriteBatch.Draw(Texture, Position, new Rectangle(16 * CurrentFrame, 0, 16, 16), Color.White, 0, Vector2.Zero, 1, textureDirection, 0);
        }

    }
}
