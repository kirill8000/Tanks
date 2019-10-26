using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tanks.Controller;
using Tanks.Model;
using Tanks.View;

namespace Tanks
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            PlayingField playingField = new PlayingField(new Size(10, 10), 27, 50, 5);
            FieldView fieldView = new FieldView(playingField, playingField.Size);
            PacmanController pacmanContorller = new PacmanController(playingField, fieldView);

            Application.Run(fieldView.Form);
        }
    }
}
