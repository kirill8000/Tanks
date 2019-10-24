using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tanks.Model;

namespace Tanks.View
{
    class WallView
    {
        private Image _img;
        private static WallView _instance;
        public static WallView Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WallView();
                    _instance._img = Image.FromFile("Resources\\wall.jpg");
                }

                return _instance;
            }
        }

        public void Draw(Graphics graphics, Wall enity)
        {
            graphics.DrawImage(_img, enity.Location);
        }
    }
}
