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

        public TileMap(XElement layerData)
        {
            this.layerData = layerData;
            Name = layerData.Attribute("name").Value;
        }

        public void CheckCollisions ()
        {
            
        }

    }
}
