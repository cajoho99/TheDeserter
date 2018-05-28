using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
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
        bool drawColliders;
        public CollisionTileMap(XElement layerData, Texture2D tileset) : base(layerData, tileset)
        {
            drawColliders = false;
        }

        public override void LoadData()
        {
            int[,] intID = ParseData(this.layerData);

            LayerTileMap = new Tile[World.MapWidth, World.MapHeight];
            for (int x = 0; x < World.MapWidth; x++)
            {
                for (int y = 0; y < World.MapHeight; y++)
                {
                    if (intID[x,y] == 0)
                    {
                        LayerTileMap[x, y] = new Tile(
                        new Vector2(x * 16, y * 16),
                        tileset,
                        new Rectangle((int)World.SourcePositions[intID[x, y]].X, (int)World.SourcePositions[intID[x, y]].Y, 16, 16),
                        false
                        );
                    }
                    else
                    {
                        LayerTileMap[x, y] = new Tile(
                        new Vector2(x * 16, y * 16),
                        tileset,
                        new Rectangle((int)World.SourcePositions[intID[x, y]].X, (int)World.SourcePositions[intID[x, y]].Y, 16, 16),
                        true
                        );
                    }
                    
                }
            }
        }

        public override void DrawTilemap(SpriteBatch spriteBatch)
        {
            if (drawColliders)
                return;
            base.DrawTilemap(spriteBatch);
        }

    }
}
