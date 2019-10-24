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
    class AppleView
    {
        protected Image _img;
        private static AppleView _instance;
        public static AppleView Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppleView();
                    _instance._img = Image.FromFile("Resources\\apple.jpg");
                }

                return _instance;
            }
        }

        public void Draw(Graphics graphics, Apple enity)
        {
            graphics.DrawImage(_img, enity.Location);
        }
    }
}
