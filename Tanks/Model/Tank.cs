using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Model
{
    class Tank : Enity
    {
        private static Random _random = new Random();

        public void RandomRotate()
        {
            Directon = (Directon)_random.Next(0, 4);
        }

        public Tank(int size, Point location, Directon directon) : base (size, location, directon)
        {
        }
    }
}
