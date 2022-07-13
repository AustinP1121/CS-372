using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Window1 : Form
    {
        int counter = 0; 
        private Point point1, point2, point3,
            point4;

        public Point Point1 { get => point1; set => point1=value; }
        public Point Point2 { get => point2; set => point2=value; }
        public Point Point3 { get => point3; set => point3=value; }
        public Point Point4 { get => point4; set => point4=value; }

        public Window1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Paint += new PaintEventHandler(pictureBox1_Paint);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Refresh();
        }

        private void Window1_MouseClick(object sender, MouseEventArgs e)
        {
            if (counter == 0)
            {
                Point1 = MousePosition;
                label1.Text = "Point 1 Coordinates: "+Convert.ToString(point1);
                counter++;
            }
            else if (counter == 1)
            {
                Point2 = MousePosition;
                label2.Text = "Point 2 Coordinates: "+Convert.ToString(point2);
                counter++;
            }
            else if(counter == 2)
            {
                Point3 = MousePosition;
                label3.Text = "Point 3 Coordinates: "+Convert.ToString(point3);
                counter++;
            }
            else if( counter == 3)
            {
                Point4 = MousePosition;
                label4.Text = "Point 4 Coordinates: "+Convert.ToString(point4);
                counter++;
            }
            else 
            { 
                //do nothing
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int pX1 = point1.X;
            int pY1 = point1.Y;

            int pX2 = point2.X;
            int pY2 = point2.Y;

            int pX3 = point3.X;
            int pY3 = point3.Y;

            int pX4 = point4.X;
            int pY4 = point4.Y; 

            Pen drawingTool = new Pen(Color.Black, 4);
            e.Graphics.DrawLine(drawingTool, pX1, pY1, pX2, pY2);
            e.Graphics.DrawLine(drawingTool, pX3, pY3, pX4, pY4);

        }
    }
}
