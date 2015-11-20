using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reflector {
    public partial class Form1 : Form {
        Bitmap image, reflected;
        public Form1() {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Jpeg (.jpg)|*.jpg";
            dialog.ShowDialog();
            image = (Bitmap)Image.FromFile(dialog.FileName);

            trkSlider.SetRange(0, image.Width);
            trkSlider.Value = image.Width / 2;

            if (rbLeft.Checked)
                reflectLeft();
            else
                reflectRight();
            picImage.Image = reflected;
            trkSlider.Width = image.Width;
        }

        private void reflectLeft() {
            int mirror = trkSlider.Value;
            reflected = new Bitmap(mirror * 2, image.Height);
            for(int i = 0; i < reflected.Height; ++i) {
                int j = mirror - 1 , k = mirror;
                while(j >=0 && k < reflected.Width) {
                    reflected.SetPixel(j, i, image.GetPixel(j, i));
                    reflected.SetPixel(k, i, image.GetPixel(j, i));
                    --j;
                    ++k;
                }
            }
        }

        private void reflectRight() {
            int mirror = trkSlider.Value;
            reflected = new Bitmap(mirror * 2, image.Height);
            for (int i = 0; i < reflected.Height; ++i) {
                int j = mirror - 1, k = mirror;
                while (j >= 0 && k < reflected.Width && k < image.Width) {
                    reflected.SetPixel(j, i, image.GetPixel(k, i));
                    reflected.SetPixel(k, i, image.GetPixel(k, i));
                    --j;
                    ++k;
                }
            }
        }

        private void trkSlider_Scroll(object sender, EventArgs e) {
            if (rbLeft.Checked)
                reflectLeft();
            else
                reflectRight();
            picImage.Image = reflected;
        }

        private void rbLeft_CheckedChanged(object sender, EventArgs e) {
            if (rbLeft.Checked)
                reflectLeft();
            else
                reflectRight();
            picImage.Image = reflected;
        }

        private void rbRight_CheckedChanged(object sender, EventArgs e) {
            if (rbLeft.Checked)
                reflectLeft();
            else
                reflectRight();
            picImage.Image = reflected;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Jpeg (.jpg)|*.jpg";
            save.ShowDialog();

            reflected.Save(save.FileName);
        }
    }
}
