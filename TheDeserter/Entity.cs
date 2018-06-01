using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDeserter.Content
{
    /// <summary>
    /// An entity
    /// </summary>
    class Entity : AnimatedSprite
    {
        #region Variables
        private Vector2 _velocity; //Velocity of the object
        private float _movementSpeed; //MovementSpeed, Max 
        private float _health; //Health of the entity
        protected Vector2 oldPosition; // the position before the last move
        protected Rectangle _hitbox; //the hitbox of the player
        #endregion

        #region Properties
        //Velocity Property 
        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }
        //MovementSpeed Property
        public float MovementSpeed
        {
            get { return _movementSpeed; }
            set { _movementSpeed = value; }
        }
        //Health Property
        public float Health
        {
            get { return _health; }
            set { _health = value; }
        }
        //Hitbox Property
        public Rectangle Hitbox
        {
            get { return _hitbox; }
            set { _hitbox = value; }
        }
        #endregion
        /// <summary>
        /// Constructer of the entity
        /// </summary>
        /// <param name="texture">The texture to be used by the entity</param>
        /// <param name="position">The position of the entity</param>
        public Entity(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Constants.TileSize, Constants.TileSize);
        }

        #region Functions

        /// <summary>
        /// Moves the entity
        /// </summary>
        /// <param name="gameTime">game time in the project</param>
        public virtual void Move(GameTime gameTime)
        {
            //Set the current position as the old position
            oldPosition = Position; 
            //Add gravity
            Velocity += Constants.Gravity * 100 * (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            //Move the entity
            Position += Velocity * (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
        }

        /// <summary>
        /// Main Update of the entity 
        /// </summary>
        /// <param name="gameTime">game time</param>
        public virtual void Update(GameTime gameTime)
        {
            //setting the hitbox's position to the entity's position
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Constants.TileSize, Constants.TileSize);
        }
        
        /// <summary>
        /// Reduces health
        /// </summary>
        /// <param name="damage">The amount of health you want to reduce</param>
        public void TakeDamage(float damage)
        {
            Health -= damage;
        }

        #endregion
    }
}
