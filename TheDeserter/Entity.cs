using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private Vector2 oldPosition;

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

        public Entity(Texture2D texture, Vector2 position) : base(texture, position)
        {

        }

        public void Move(GameTime gameTime)
        {
            oldPosition = Position;
            Velocity += Constants.Gravity * (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            Position += _velocity * (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            if(Position.X < 0 || Position.X > (World.MapWidth - 1) * Constants.GridSize)
            {
                Position = new Vector2(oldPosition.X, Position.Y);
            }

            if (Position.Y < 0 || Position.Y > (World.MapHeight - 1) * Constants.GridSize)
            {
                Position = new Vector2(Position.X, oldPosition.Y);
            }

        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
        }
    }
}
