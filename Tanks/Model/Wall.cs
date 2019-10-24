using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Model
{
    enum WallType
    {
        Destructible,
        ShotThrough,
        Capital
    }
    class Wall : Item
    {
        public WallType WallType { get; }
        public Wall(int size, Point location, WallType wallType = WallType.Capital) : base(size, location)
        {
            WallType = wallType;
        }
    }
}
