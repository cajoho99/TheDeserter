using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
    /// The game world
    /// </summary>
    class World
    {
        #region Variables
        public Texture2D tileset;

        //Static Variables

        public static List<TileMap> Layers;

        public static CollisionTileMap CollisionTileMap;

        private static int _mapWidth;
        private static int _mapHeight;
        private static int _tileCount;
        private static int _columns;
        private static Vector2[] _sourcePositions;
        #endregion
        #region Properties

        // Static Properties

        public static int MapWidth
        {
            get { return _mapWidth; }
        }
        public static int MapHeight
        {
            get { return _mapHeight; }
        }
        public static int TileCount
        {
            get { return _tileCount; }
        }
        public static int Columns
        {
            get { return _columns; }
        }
        public static Vector2[] SourcePositions
        {
            get { return _sourcePositions; }
        }
        #endregion

        /// <summary>
        /// Main constuctor for the world
        /// </summary>
        public World()
        {
            Layers = new List<TileMap>();
        }

        /// <summary>
        /// Loading the world
        /// </summary>
        /// <param name="tilemap">Filename of the tilemao</param>
        /// <param name="Content">The ContentManager used</param>
        public void LoadWorld(string tilemap, ContentManager Content)
        {
            tileset = Content.Load<Texture2D>("industrial"); //Loading the tilemap
            //Converting the file to an XDocument
            XDocument xDoc = XDocument.Load("Content/" + tilemap + ".tmx");
            //Getting the main information from the root element
            _mapWidth = int.Parse(xDoc.Root.Attribute("width").Value);
            _mapHeight = int.Parse(xDoc.Root.Attribute("height").Value);
            _tileCount = int.Parse(xDoc.Root.Element("tileset").Attribute("tilecount").Value);
            _columns = int.Parse(xDoc.Root.Element("tileset").Attribute("columns").Value);

            //Get all the layers
            var tileMapLayers = xDoc.Root.Elements("layer");

            //Assigns a value to all the cells in the tilesheet
            int key = 0;
            _sourcePositions = new Vector2[TileCount];
            for(int y = 0; y < TileCount / Columns; y++)
            {
                for(int x = 0; x < Columns; x++)
                {
                    _sourcePositions[key] = new Vector2(x * 16, y * 16);
                    key++;
                }
            }

            //Creates the TileMaps for each layer
            foreach (var tml in tileMapLayers)
            {
                //Creates a Collider Tile map if the name of the layer is "Colliders"
                if(tml.Attribute("name").Value == "Colliders")
                {
                    CollisionTileMap = new CollisionTileMap(tml, tileset);
                    CollisionTileMap.LoadData();
                }
                else
                {
                    Layers.Add(new TileMap(tml, tileset));
                }
            }

            //Loads all the data in all of the layers
            foreach (TileMap tm in Layers)
            {
                tm.LoadData();
            }
        }

        /// <summary>
        /// Draws all the layers
        /// </summary>
        /// <param name="spriteBatch">The sprite batch object</param>
        public void DrawLayers(SpriteBatch spriteBatch)
        {
            foreach( TileMap tilemap in Layers)
            {
                tilemap.DrawTilemap(spriteBatch);
            }
        }
    }
}
