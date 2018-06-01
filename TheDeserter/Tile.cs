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
    /// A Tile in a tile array
    /// </summary>
    class Tile : Sprite
    {
        #region Variables
        private Rectangle _sourceRectangle; // The Rectange from the sprite sheet that should be used
        private Rectangle _collider;// The collider of the tile
        private Boolean _isSolid; //Flags if the tile should be detected by the Collision detection system
        #endregion

        #region Properties
        /// <summary>
        /// Properies for the variables listed above
        /// </summary>
        public Rectangle SourceRectangle
        {
            get { return _sourceRectangle; }
            set { _sourceRectangle = value; }
        }
        public Boolean IsSolid
        {
            get { return _isSolid; }
            set { _isSolid = value; }
        }
        public Rectangle Collider
        {
            get { return _collider; }
            set { _collider = value; }
        }
        #endregion

        /// <summary>
        /// Constructor for the tile
        /// </summary>
        /// <param name="pos">The starting position</param>
        /// <param name="tex">The tilesheet</param>
        /// <param name="sourceRect">The rectangle on the tilesheet</param>
        /// <param name="isSolid">IsSolid flag</param>
        public Tile(Vector2 pos, Texture2D tex, Rectangle sourceRect, bool isSolid) : base(tex, pos)
        {
            Position = pos;
            Texture = tex;
            SourceRectangle = sourceRect;
            IsSolid = isSolid;
            Collider = new Rectangle((int)Position.X, (int)Position.Y, Constants.TileSize, Constants.TileSize);
        }

        /// <summary>
        /// Checks if the rectangle formed by tilesize and the vector parameter intersects with this tiles 
        /// </summary>
        /// <param name="position">The position parameter of the checked object</param>
        /// <returns></returns>
        public bool CheckCollision(Vector2 position)
        {
            //Checks if x overlaps
            bool xOverlap = Collider.X < position.X + Constants.TileSize && 
                Collider.Right > position.X;

            //Checks if Y overlaps
            bool yOverlap = Collider.Y < position.Y + Constants.TileSize &&
                Collider.Bottom > position.Y;


            if(xOverlap && yOverlap && IsSolid)
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// Main Draw function for the tile
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SourceRectangle, Color.White);
        }

    }
}                    
