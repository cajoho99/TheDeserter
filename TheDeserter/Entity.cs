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

        public virtual void Move(GameTime gameTime)
        {
            oldPosition = Position;
            Velocity += Constants.Gravity * 100 * (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            Position += Velocity * (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
        }

        

        public void TakeDamage(float damage)
        {
            Health -= damage;
        }
    }
}
