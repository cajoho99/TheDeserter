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
    /// <summary>
    /// Collision map 
    /// </summary>
    class CollisionTileMap : TileMap
    {
        //bool drawColliders;
        /// <summary>
        /// Constructor of the collision tilemap
        /// </summary>
        /// <param name="layerData">The raw data from the tiled file</param>
        /// <param name="tileset"></param>
        public CollisionTileMap(XElement layerData, Texture2D tileset) : base(layerData, tileset)
        {
            //drawColliders = false;
        }

        #region Functions
        /// <summary>
        /// Reads and interpretes the raw data
        /// </summary>
        public override void LoadData()
        {
            //Parses the data and puts it in a 2d aray
            int[,] intID = ParseData(this.layerData);

            //The Tile array being initialized
            LayerTileMap = new Tile[World.MapWidth, World.MapHeight];

            //Loops through and checks for values in the array.
            for (int x = 0; x < World.MapWidth; x++)
            {
                for (int y = 0; y < World.MapHeight; y++)
                {
                    //0 is empty which means no collider
                    if (intID[x,y] == 0)
                    {
                        LayerTileMap[x, y] = new Tile(
                        new Vector2(x * 16, y * 16),
                        tileset,
                        new Rectangle((int)World.SourcePositions[intID[x, y]].X, (int)World.SourcePositions[intID[x, y]].Y, 16, 16),
                        false // isSolid = false
                        );
                    }
                    else
                    {
                        LayerTileMap[x, y] = new Tile(
                        new Vector2(x * 16, y * 16),
                        tileset,
                        new Rectangle((int)World.SourcePositions[intID[x, y]].X, (int)World.SourcePositions[intID[x, y]].Y, 16, 16),
                        true // isSolid = true
                        );
                    }
                    
                }
            }
        }

        public override void DrawTilemap(SpriteBatch spriteBatch)
        {
            //if (drawColliders)
              //  return;
            base.DrawTilemap(spriteBatch);
        }
        #endregion
    }
}
