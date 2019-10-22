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

namespace Tanks.View
{
    partial class Statictic : Form
    {
        public void Update(BindingList<Tank> items)
        {
            Invoke((MethodInvoker)delegate
            {

                 dataGridView1.DataSource = items;
                 dataGridView1.Invalidate();


            });
        }
        public Statictic()
        {
            InitializeComponent();
            dataGridView1.DataError += (o, s) =>
            {

            };
        }
    }
}
