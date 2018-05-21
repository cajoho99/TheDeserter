using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TheDeserter
{
    class TileMap
    {

        private Tile[,] _layerTileMap;
        private XContainer layerData;
        private string _name;
        private Texture2D tileset;

        public Tile[,] LayerTileMap
        {
            get { return _layerTileMap; }
            set { _layerTileMap = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public TileMap(XElement layerData, Texture2D texture)
        {
            this.layerData = layerData;
            Name = layerData.Attribute("name").Value;
            tileset = texture;
        }

        public void LoadData()
        {
            string rawData = layerData.Element("data").Value;
            string[] splitArray = rawData.Split(',');

            int[,] intID = new int[World.MapWidth, World.MapHeight];

            for(int x = 0; x < World.MapWidth; x++)
            {
                for(int y = 0; y < World.MapHeight; y++)
                {
                    intID[x, y] = int.Parse(splitArray[x + y * World.MapWidth]);
                }
            }

            LayerTileMap = new Tile[World.MapWidth, World.MapHeight];
            for(int x = 0; x < World.MapWidth; x++)
            {
                for(int y = 0; y < World.MapHeight; y++)
                {
                    LayerTileMap[x, y] = new Tile(
                        new Vector2(x * 16, y * 16),
                        tileset,
                        new Rectangle((int)World.SourcePositions[intID[x, y]].X, (int)World.SourcePositions[intID[x, y]].Y, 16, 16)
                        );
                }
            }
        }

        public void DrawTilemap(SpriteBatch spriteBatch)
        {
            for(int x = 0; x < World.MapWidth; x++)
            {
                for(int y = 0; y < World.MapHeight; y++)
                {
                    LayerTileMap[x, y].Draw(spriteBatch);
                }
            }
        }

        public void CheckCollisions ()
        {
            
        }

    }
}
