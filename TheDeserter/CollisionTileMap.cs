using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TheDeserter
{
    class CollisionTileMap : TileMap
    {
        public CollisionTileMap(XElement layerData, Texture2D texture) : base(layerData, texture)
        {

        }
    }
}
