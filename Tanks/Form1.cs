using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tanks.Model;
using Tanks.View;

namespace Tanks
{
    public partial class Form1 : Form
    {
        public PictureBox PictureBox;
        public Timer Timer = new Timer();
        public Form1()
        {
            InitializeComponent();
        }
    }
}
