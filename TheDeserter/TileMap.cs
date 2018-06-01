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
    /// <summary>
    /// The normal tilemap
    /// </summary>
    class TileMap
    {
        #region Variables
        protected Tile[,] _layerTileMap; // the output layer map
        protected XContainer layerData; // the raw data
        protected string _name; // the name of the layer
        protected Texture2D tileset; // the tileset used
        #endregion
        #region Properties
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
        #endregion

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="layerData">The raw layer data</param>
        /// <param name="texture">The tileset</param>
        public TileMap(XElement layerData, Texture2D texture)
        {
            this.layerData = layerData;
            Name = layerData.Attribute("name").Value;
            tileset = texture;
        }


        /// <summary>
        /// Reading and parsing the raw data
        /// </summary>
        public virtual void LoadData()
        {
            //Parsing the data
            int[,] intID = ParseData(this.layerData);
            //adding the tiles to the tile array
            LayerTileMap = new Tile[World.MapWidth, World.MapHeight];
            for(int x = 0; x < World.MapWidth; x++)
            {
                for(int y = 0; y < World.MapHeight; y++)
                {
                    LayerTileMap[x, y] = new Tile(
                        new Vector2(x * 16, y * 16),
                        tileset,
                        new Rectangle((int)World.SourcePositions[intID[x, y]].X, (int)World.SourcePositions[intID[x, y]].Y, 16, 16),
                        false //isSolid = false
                        );
                }
            }
        }

        /// <summary>
        /// Parses the raw data into an int array
        /// </summary>
        /// <param name="data">Raw data</param>
        /// <returns>Grid with the tileset location</returns>
        protected int[,] ParseData(XContainer data)
        {
            //Extracts the raw data from the XContainer
            string rawData = data.Element("data").Value;
            //Splits the array
            string[] splitArray = rawData.Split(',');

            //Creating the output array
            int[,] intID = new int[World.MapWidth, World.MapHeight];

            //assigning values to the array
            for (int x = 0; x < World.MapWidth; x++)
            {
                for (int y = 0; y < World.MapHeight; y++)
                {
                    //Quickfix for loaderror
                    if (int.Parse(splitArray[x + y * World.MapWidth]) - 1 < 0)
                    {
                        intID[x, y] = 0;
                    }
                    else
                    {
                        intID[x, y] = int.Parse(splitArray[x + y * World.MapWidth]) - 1;
                    }
                }
            }

            return intID;
        }

        /// <summary>
        /// Loops through every tile in the map and calls the draw function
        /// </summary>
        /// <param name="spriteBatch">The sprite batch object</param>
        public virtual void DrawTilemap(SpriteBatch spriteBatch)
        {
            for(int x = 0; x < World.MapWidth; x++)
            {
                for(int y = 0; y < World.MapHeight; y++)
                {
                    LayerTileMap[x, y].Draw(spriteBatch);
                }
            }
        }

        /// <summary>
        /// Never used
        /// </summary>
        /// <param name="position"></param>
        public void CheckCollisions(Vector2 position)
        {
            
        }

    }
}
