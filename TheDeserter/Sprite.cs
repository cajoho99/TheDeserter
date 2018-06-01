using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDeserter
{
    /// <summary>
    /// A drawable sprite
    /// </summary>
    class Sprite
    {
        #region Variables
        private Vector2 _position; // Position of the sprite
        private Texture2D _texture; // Texture to be drawn
        #endregion
        #region Parameters
        //Position Parameter
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        //Texture Parameter
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        #endregion

        /// <summary>
        /// Sprite Constructor
        /// </summary>
        /// <param name="texture">the texture that should be drawn</param>
        /// <param name="position">the desired starting position</param>
        public Sprite(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        /// <summary>
        /// Main draw function for sprites
        /// </summary>
        /// <param name="spriteBatch">The sprite batch object</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        /// <summary>
        /// Draw function used for inherited objects
        /// </summary>
        /// <param name="spriteBatch">Sprite Batch</param>
        /// <param name="gameTime">Game Time</param>
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) { }
    }
}
