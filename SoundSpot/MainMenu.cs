using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundSpot
{
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();

        }
        private void button1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.FillRectangle(
                new LinearGradientBrush(PointF.Empty, new PointF(0, button1.Height), Color.Black, Color.Green),
                new RectangleF(PointF.Empty, button1.Size));
            g.DrawString("Вход за преподавателя", new Font("Arial", 10), System.Drawing.Brushes.White, new Point(10, 3));

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
