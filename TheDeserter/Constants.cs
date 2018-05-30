using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDeserter
{
    public static class Constants
    {
        public static readonly Vector2 Gravity = new Vector2(0, 9.82f);
        public static readonly int TileSize = 16;
        public static readonly float FrictionForce = 1600f;
    }
}
