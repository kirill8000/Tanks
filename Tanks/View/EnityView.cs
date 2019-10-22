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
    class EnityView
    {
        protected Image _img;

        private Directon CurrentDirection;
        private void Rotate(Directon newDdirecton)
        {
            if (CurrentDirection == Directon.DOWN)
            {
                if (newDdirecton == Directon.LEFT)
                    _img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                else if (newDdirecton == Directon.UP)
                    _img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                else if (newDdirecton == Directon.RIGHT)
                    _img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            else if (CurrentDirection == Directon.UP)
            {
                if (newDdirecton == Directon.LEFT)
                    _img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                else if (newDdirecton == Directon.DOWN)
                    _img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                else if (newDdirecton == Directon.RIGHT)
                    _img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else if (CurrentDirection == Directon.LEFT)
            {
                if (newDdirecton == Directon.UP)
                    _img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                if (newDdirecton == Directon.DOWN)
                    _img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                if (newDdirecton == Directon.RIGHT)
                    _img.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }
            else if (CurrentDirection == Directon.RIGHT)
            {
                if (newDdirecton == Directon.LEFT)
                    _img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                else if (newDdirecton == Directon.DOWN)
                    _img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                else if (newDdirecton == Directon.UP)
                    _img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }

            CurrentDirection = newDdirecton;
        }
        public void Draw(PictureBox pictureBox, Enity enity)
        {
            Rotate(enity.Directon);
            Graphics graphics = Graphics.FromImage(pictureBox.Image);
            graphics.DrawImage(_img, enity.Location);
        }
    }
}
