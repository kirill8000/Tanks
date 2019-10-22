using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tanks.Model;
using Tanks.View;

namespace Tanks.Controller
{
    class PacmanController
    {
        public PacmanController(PlayingField field, FieldView view)
        {
            view.Form.KeyDown += (o, s) =>
            {
                switch (s.KeyCode)
                {
                    case Keys.Up:
                        field.Kolobok.Directon = Directon.UP;
                        break;
                    case Keys.Down:
                        field.Kolobok.Directon = Directon.DOWN;
                        break;
                    case Keys.Left:
                        field.Kolobok.Directon = Directon.LEFT;
                        break;
                    case Keys.Right:
                        field.Kolobok.Directon = Directon.RIGHT;
                        break;
                    case Keys.Space:
                        field.Fire();
                        break;
                }

            };

            field.Begin();

            field.GameOver += (o, s) =>
            {
                field.Stop();
                MessageBox.Show("Game over", "Info");
            };

            field.ScoreUpdated += (o, s) =>
            {
                view.UpdateScore(field.Score);
            };

            field.Updated += (o, s) =>
            {

                view.statistic.Update(new BindingList<Tank>(field.Tanks));
                view.Draw((PlayingField)o);
            };

            view.Form.FormClosing += (o, s) =>
            {
                field.Stop();
            };
        }
    }
}
