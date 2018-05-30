using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDeserter
{
    class Tile : Sprite
    {
        private Rectangle _sourceRectangle;
        private Rectangle _collider;
        private Boolean _isSolid;

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



        public Tile(Vector2 pos, Texture2D tex, Rectangle sourceRect, bool isSolid) : base(tex, pos)
        {
            Position = pos;
            Texture = tex;
            SourceRectangle = sourceRect;
            IsSolid = isSolid;
            Collider = new Rectangle((int)Position.X, (int)Position.Y, Constants.TileSize, Constants.TileSize);
        }

        public bool CheckCollision(Vector2 position)
        {
            bool xOverlap = Collider.X < position.X + Constants.TileSize && 
                Collider.Right > position.X;

            bool yOverlap = Collider.Y < position.Y + Constants.TileSize &&
                Collider.Bottom > position.Y;

            if(xOverlap && yOverlap && IsSolid)
            {
                return true;
            }
            return false;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SourceRectangle, Color.White);
        }

    }
}                    
