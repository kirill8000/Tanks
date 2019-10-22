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
        
        private void CreateBitmap(int width, int height)
        {
            Form.PictureBox = new PictureBox();
            Form.PictureBox.Size = new Size(width, height);
            Form.Controls.Add(Form.PictureBox);
            Form.Height = height + 55;
            Form.Width = width + 20;
            Form.PictureBox.Image = new Bitmap(width, height);
        }

        public void Draw(PlayingField field)
        {
            try
            {
                if (!Form.IsDisposed && !Form.PictureBox.IsDisposed)
                {
                    Form.Invoke((MethodInvoker)delegate
                    {

                        Graphics.FromImage(Form.PictureBox.Image).Clear(Color.White);

                        foreach (var apple in field.Apples)
                        {
                            AppleView.Instance.Draw(Form.PictureBox, apple);
                        }

                        KolobokView.Instance.Draw(Form.PictureBox, field.Kolobok);
                        
                        foreach (var bullet in field.TanksBullets)
                        {
                            BulletView.Instance.Draw(Form.PictureBox, bullet);
                        }

                        foreach (var tank in field.Tanks)
                        {
                            TankView.Instance.Draw(Form.PictureBox, tank);
                        }

                        foreach (var bullet in field.KolobokBullets)
                        {
                            BulletView.Instance.Draw(Form.PictureBox, bullet);
                        }

                        foreach (var wall in field.Wall)
                        {
                            WallView.Instance.Draw(Form.PictureBox, wall);
                        }

                        KolobokView.Instance.Draw(Form.PictureBox, field.Kolobok);
                        Form.PictureBox.Invalidate();

                    });
                }
            }
            catch
            {

            }
        }

        public void UpdateScore(int val)
        {
            Form.Invoke((MethodInvoker)delegate
            {
                score.Text = $"Score: {val}";
            });
        }

        public FieldView(Size size)
        {
            statistic = new Statictic();
            statistic.Show();
            Form = new Form1();
            CreateBitmap(size.Width, size.Height);
            score = new Label();
            score.Text = "Score: 0";
            score.Location = new Point(0, Form.PictureBox.Height);
            Form.Controls.Add(score);
        }
    }
}
