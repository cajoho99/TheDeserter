﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDeserter
{
    class Sprite
    {
        #region Variables
        private Vector2 _position;
        private Texture2D _texture;
        #endregion
        #region Parameters
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        #endregion
    }
}