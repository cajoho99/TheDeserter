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

        public void LoadWorld(string tilemap, ContentManager Content)
        {
            XDocument xDoc = XDocument.Load("");
            int mapWidth = int.Parse(xDoc.Root.Attribute("width").Value);
            int mapHeight = int.Parse(xDoc.Root.Attribute("height").Value);
            int tileCount = int.Parse(xDoc.Root.Element("tileset").Attribute("tilecount").Value);
            int columns = int.Parse(xDoc.Root.Element("tileset").Attribute("columns").Value);

            var tileMapLayers = xDoc.Root.Descendants("layer");
        }
    }
}
