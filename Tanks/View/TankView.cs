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
    class TankView : EnityView
    {
        private static TankView _instance;
        public static TankView Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TankView();
                    _instance._img = Image.FromFile("Resources\\tank.jpg");
                }

                return _instance;
            }
        }
    }
}
