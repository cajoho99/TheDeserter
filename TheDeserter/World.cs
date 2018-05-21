using Microsoft.Xna.Framework.Content;
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

        private static int _mapWidth;
        private static int _mapHeight;
        private static int _tileCount;
        private static int _columns;

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
        public void LoadWorld(string tilemap, ContentManager Content)
        {
            XDocument xDoc = XDocument.Load("Content/loadTest.tmx");
            _mapWidth = int.Parse(xDoc.Root.Attribute("width").Value);
            _mapHeight = int.Parse(xDoc.Root.Attribute("height").Value);
            _tileCount = int.Parse(xDoc.Root.Element("tileset").Attribute("tilecount").Value);
            _columns = int.Parse(xDoc.Root.Element("tileset").Attribute("columns").Value);

            var tileMapLayers = xDoc.Root.Elements("layer");

            foreach (var tml in tileMapLayers)
            {
                if(tml.Attribute("name").Value != "collision")
                {

                }
                else
                {
                    Layers.Add(new TileMap(tml));
                }
            }

        }
    }
}
