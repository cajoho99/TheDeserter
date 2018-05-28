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

        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            MovementSpeed = 20f;
        }

        public void Jump()
        {

        }

        public void MovementInput()
        {
            if(InputManager.Instance.KeyDown(Keys.A, Keys.Left))
            {
                Velocity -= Vector2.UnitX * MovementSpeed;
            }

            if (InputManager.Instance.KeyDown(Keys.D, Keys.Right))
            {
                Velocity += Vector2.UnitX * MovementSpeed;
            }

            if (InputManager.Instance.KeyDown(Keys.Space))
            {
                Jump();
            }
        }

        

    }
}
