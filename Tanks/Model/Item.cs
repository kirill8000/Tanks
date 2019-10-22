using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Model
{
    abstract class Item
    {
        protected Rectangle Rectangle;
        public int Size => Rectangle.Width;
        public int X => Rectangle.X;
        public int Y => Rectangle.Y;
        public string Name => nameof(Item);
        public Point Location => Rectangle.Location;

        public bool IntersectsWith(Item item)
        {
            return item.Rectangle.IntersectsWith(Rectangle);
        }

        protected Item(int size, Point location)
        {
            Rectangle = new Rectangle(location, new Size(size, size));
        }
    }
}
