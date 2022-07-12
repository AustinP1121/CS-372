using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Window1 : Form
    {
        int counter = 0; 
        Point point1, point2;

        public Point Point1 { get => point1; set => point1=value; }
        public Point Point2 { get => point2; set => point2=value; }

        public Window1()
        {
            InitializeComponent();
        }

        private void ConstructLines(object sender, MouseEventArgs e, PaintEventArgs a)
        {
            GraphicsPath graphics = new GraphicsPath();
            Pen drawingTool = new Pen(Color.Black, 2);

            Point point1, point2;

            point1 = MousePosition;
            point2 = MousePosition;

            graphics.AddLine(point1, point2);
            a.Graphics.DrawPath(drawingTool, graphics);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Invalidate();
        }

        private void Window1_MouseClick(object sender, MouseEventArgs e)
        {
            if (counter == 0)
            {
                Point1 = MousePosition;
                counter++;
            }
            else 
            {
                Point2 = MousePosition;
            }
        }

        //function works
        private void Window1_MouseClick(object sender, System.EventArgs e)
        {
            if (counter == 0)
            {
                Point1 = MousePosition;
                label1.Text = Convert.ToString(Point1); 
                counter++;
            }
            else
            {
                Point2 = MousePosition;
                label2.Text = Convert.ToString(point2);
            }
        }

        //assume issue lies here?
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath graphics = new GraphicsPath();
            Pen drawingTool = new Pen(Color.Black, 2);

            e.Graphics.DrawLine(drawingTool, point1, point2);
        }
    }
}
