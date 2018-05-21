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
    class World
    {
        public List<TileMap> Layers;
        public Texture2D tileset;

        private static int _mapWidth;
        private static int _mapHeight;
        private static int _tileCount;
        private static int _columns;
        private static Vector2[] _sourcePositions;


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

        public World()
        {
            Layers = new List<TileMap>();
        }

        public void LoadWorld(string tilemap, ContentManager Content)
        {
            tileset = Content.Load<Texture2D>("industrial");
            XDocument xDoc = XDocument.Load("Content/" + tilemap + ".tmx");
            _mapWidth = int.Parse(xDoc.Root.Attribute("width").Value);
            _mapHeight = int.Parse(xDoc.Root.Attribute("height").Value);
            _tileCount = int.Parse(xDoc.Root.Element("tileset").Attribute("tilecount").Value);
            _columns = int.Parse(xDoc.Root.Element("tileset").Attribute("columns").Value);

            var tileMapLayers = xDoc.Root.Elements("layer");

            int key = 0;
            _sourcePositions = new Vector2[TileCount];
            for(int x = 0; x < TileCount / Columns; x++)
            {
                for(int y = 0; y < Columns; y++)
                {
                    _sourcePositions[key] = new Vector2(x * 16, y * 16);
                    key++;
                }
            }

            foreach (var tml in tileMapLayers)
            {
                if(tml.Attribute("name").Value == "collision")
                {

                }
                else
                {
                    Layers.Add(new TileMap(tml, tileset));

                }
            }

            foreach (TileMap tm in Layers)
            {
                tm.LoadData();
            }
        }

        public void DrawLayers(SpriteBatch spriteBatch)
        {
            foreach( TileMap tilemap in Layers)
            {
                tilemap.DrawTilemap(spriteBatch);
            }
        }
    }
}
