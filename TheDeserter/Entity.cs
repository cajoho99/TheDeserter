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
    class Entity : AnimatedSprite
    {
        private Vector2 _velocity;
        private float _movementSpeed;
        private float _health;
        protected Vector2 oldPosition;
        protected Rectangle _hitbox;


        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }
        public float MovementSpeed
        {
            get { return _movementSpeed; }
            set { _movementSpeed = value; }
        }
        public float Health
        {
            get { return _health; }
            set { _health = value; }
        }
        public Rectangle Hitbox
        {
            get { return _hitbox; }
            set { _hitbox = value; }
        }

        public Entity(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Constants.TileSize, Constants.TileSize);
        }

        public virtual void Move(GameTime gameTime)
        {
            oldPosition = Position;
            Velocity += Constants.Gravity * 100 * (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            Position += Velocity * (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
        }


        public virtual void Update(GameTime gameTime)
        {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Constants.TileSize, Constants.TileSize);
        }
        

        public void TakeDamage(float damage)
        {
            Health -= damage;
        }
    }
}
