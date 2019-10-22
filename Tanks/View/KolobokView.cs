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
    class KolobokView : EnityView
    {

        private static KolobokView _instance;
        public static KolobokView Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new KolobokView();
                    _instance._img = Image.FromFile("Resources\\tank1.jpg");
                }

                return _instance;
            }
        }
    }
}
