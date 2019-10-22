using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Model
{
    class Bullet : Enity
    {
        public void Move(int c)
        {
            Offset(c);
        }
        public Bullet(int size, Enity parent) : base(size, new Point(parent.X + parent.Size / 2 - 1, parent.Y + parent.Size / 2 - 1), parent.Directon)
        {
        }
    }
}
