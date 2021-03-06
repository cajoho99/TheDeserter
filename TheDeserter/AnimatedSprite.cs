﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDeserter
{
    /// <summary>
    /// An animated sprite object
    /// </summary>
    class AnimatedSprite : Sprite
    {
        #region Variables
        private int _currentFrame = 0; // The frame that is being displayed
        protected float animationTimer = 500f; // The countdown timer between frames
        protected float TIMER = 500f; // The static timer
        #endregion

        #region Properties
        //Current frame Property. Generated by Visual Studio. Looks pretty cool.
        public int CurrentFrame { get => _currentFrame; set => _currentFrame = value; }
        #endregion

        /// <summary>
        /// Costructor for the Animated sprite
        /// </summary>
        /// <param name="texture">The texturemap that is going to be rendered</param>
        /// <param name="position">The position of the </param>
        public AnimatedSprite(Texture2D texture, Vector2 position) : base(texture, position)
        {
                
        }
        #region Functions
        /// <summary>
        /// Loops through the animations
        /// </summary>
        /// <param name="gameTime">The gametime of the program</param>
        public void Animate(GameTime gameTime)
        {
            //The time elapsed since the last update
            float elapsed = (float)gameTime.ElapsedGameTime.Milliseconds;
            animationTimer -= elapsed;
            //Switch texture if the timer has run out
            if(animationTimer <= 0)
            {
                CurrentFrame++;
                //Restart the loop if all images has been shown
                if(CurrentFrame >= Texture.Width / 16)
                {
                    CurrentFrame = 0;
                }
                animationTimer = TIMER;
            }
        }

        /// <summary>
        /// Draws the animated object to the screen
        /// </summary>
        /// <param name="spriteBatch">The sprite batch </param>
        /// <param name="gameTime">The game time</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Animate(gameTime);
            //Drawing the current frame
            spriteBatch.Draw(Texture, Position, new Rectangle(16 * CurrentFrame, 0, 16, 16), Color.White);
        }
        #endregion
    }
}
