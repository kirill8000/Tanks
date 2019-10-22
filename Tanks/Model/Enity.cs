using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Model
{
    enum Directon
    {
        UP,
        RIGHT,
        DOWN,
        LEFT
    }
    abstract class Enity : Item
    {
        public Directon Directon
        {
            get;
            set;
        }
        private Point LastPosition;
        protected void Offset(int delta)
        {
            LastPosition = new Point(Rectangle.X, Rectangle.Y);
            switch (Directon)
            {
                case Directon.UP:
                    LastPosition = new Point(0, -delta);
                    break;
                case Directon.DOWN:
                    LastPosition = new Point(0, delta);
                    break;
                case Directon.LEFT:
                    LastPosition = new Point(-delta, 0);
                    break;
                case Directon.RIGHT:
                    LastPosition = new Point(delta, 0);
                    break;
            }

            Rectangle.Offset(LastPosition);
        }

        public void Move()
        {
            Offset(1);
        }

        public void MoveBack()
        {
            Rectangle.Offset(new Point(-LastPosition.X, -LastPosition.Y));
        }

        public void Rotate180()
        {
            if (Directon == Directon.DOWN)
                Directon = Directon.UP;
            else if (Directon == Directon.UP)
                Directon = Directon.DOWN;
            else if (Directon == Directon.LEFT)
                Directon = Directon.RIGHT;
            else if (Directon == Directon.RIGHT)
                Directon = Directon.LEFT;
            Offset(1);
        }

        protected Enity(int size, Point location, Directon directon) : base(size, location)
        {
            Directon = directon;
        }
    }
}
