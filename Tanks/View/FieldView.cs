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
    class FieldView
    {
        public Form1 Form { get; private set; }
        public Statictic statistic;

        private Label score;
        public PlayingField PlayingField { get; }
        private void CreateBitmap(int width, int height)
        {
            Form.PictureBox = new PictureBox();
            Form.PictureBox.Size = new Size(width, height);
            Form.Controls.Add(Form.PictureBox);
            Form.Height = height + 55;
            Form.Width = width + 20;
            Form.PictureBox.Image = new Bitmap(width, height);
        }

        public void Draw()
        {
            var bitmap = new Bitmap(PlayingField.Size.Width, PlayingField.Size.Height);
            var graphics = Graphics.FromImage(bitmap);

            foreach (var apple in PlayingField.Apples)
            {
                AppleView.Instance.Draw(graphics, apple);
            }

            KolobokView.Instance.Draw(graphics, PlayingField.Kolobok);

            foreach (var bullet in PlayingField.TanksBullets)
            {
                BulletView.Instance.Draw(graphics, bullet);
            }

            foreach (var tank in PlayingField.Tanks)
            {
                TankView.Instance.Draw(graphics, tank);
            }

            foreach (var bullet in PlayingField.KolobokBullets)
            {
                BulletView.Instance.Draw(graphics, bullet);
            }

            foreach (var wall in PlayingField.Wall)
            {
                WallView.Instance.Draw(graphics, wall);
            }

            KolobokView.Instance.Draw(graphics, PlayingField.Kolobok);

            if (!Form.IsDisposed && !Form.PictureBox.IsDisposed)
                {
                    Form.Invoke((MethodInvoker)delegate
                    {
                        Form.PictureBox.Image = bitmap;
                        Form.PictureBox.Invalidate();
                    });
                }
        }

        public void UpdateScore(int val)
        {
            Form.Invoke((MethodInvoker)delegate
            {
                score.Text = $"Score: {val}";
            });
        }

        public FieldView(PlayingField field, Size size)
        {
            statistic = new Statictic();
            statistic.Show();
            Form = new Form1();
            CreateBitmap(size.Width, size.Height);
            score = new Label();
            score.Text = "Score: 0";
            score.Location = new Point(0, Form.PictureBox.Height);
            Form.Controls.Add(score);
            PlayingField = field;
            field.ScoreUpdated += (o, s) =>
            {
                UpdateScore(field.Score);
            };
            Form.Timer.Interval = field.UpdateInterval;
            Form.Timer.Tick += (o, s) =>
            {
                PlayingField.Update();
                Draw();
                statistic.Update(field.Tanks);
            };
        }
    }
}
