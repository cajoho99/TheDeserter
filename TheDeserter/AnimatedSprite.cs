using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDeserter
{
    class AnimatedSprite : Sprite
    {
        private int _currentFrame = 0;
        private float animationTimer = 500f;
        private const float TIMER = 500f;

        public int CurrentFrame { get => _currentFrame; set => _currentFrame = value; }

        public AnimatedSprite(Texture2D texture, Vector2 position) : base(texture, position)
        {
                
        }

        public void Animate(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.Milliseconds;
            animationTimer -= elapsed;
            if(animationTimer <= 0)
            {
                CurrentFrame++;
                if(CurrentFrame >= Texture.Width / 16)
                {
                    CurrentFrame = 0;
                }
                animationTimer = TIMER;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Animate(gameTime);
            spriteBatch.Draw(Texture, Position, new Rectangle(16 * CurrentFrame, 0, 16, 16), Color.White);
        }
    }
}
