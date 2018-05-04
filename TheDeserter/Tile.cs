﻿using Microsoft.Xna.Framework;
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

        public bool CheckCollision(Rectangle characterHitbox)
        {
            bool xOverlap = SourceRectangle.X < (characterHitbox.X + characterHitbox.Width) && 
                SourceRectangle.X + SourceRectangle.Width > characterHitbox.X;
            bool yOverlap = SourceRectangle.Y < (characterHitbox.Y + characterHitbox.Height) &&
                (SourceRectangle.Y + SourceRectangle.Height) > characterHitbox.Y;
            if(xOverlap && yOverlap)
            {
                return true;
            }
            return false;

        }

    }
}                    