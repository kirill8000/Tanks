using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.View
{
    class BulletView : EnityView
    {
        private static BulletView _instance;
        public static BulletView Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BulletView();
                    _instance._img = Image.FromFile("Resources\\bullet.jpg");
                }

                return _instance;
            }
        }
    }
}
