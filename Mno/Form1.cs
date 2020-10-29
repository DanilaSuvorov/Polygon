using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mno
{
    public partial class Form1 : Form
    {
        static int R;
        static Color col;
        MyShape shape;
        int sshape;
        int x, y;
        int delX, delY;
        bool Draw = false; //существование вершины
        bool Dragger = false;
        static Form1()
        {
            R = 70;
            col = Color.Green;
        }
        public Form1()
        {
            InitializeComponent();
            delX = 0;
            delY = 0;
            Dragger = false;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if(Draw) shape.Draw(e.Graphics);
        }
        private void треугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sshape = 1;
        }

        private void квадратToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sshape = 2;
        }
        private void кругToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sshape = 3;
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragger)
            {
                shape.SetX = e.X + delX;
                shape.SetY = e.Y + delY;
            }
            this.Invalidate();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Dragger = false;
            this.Invalidate();
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (Draw && shape.Check(e.X, e.Y))
            {
                if (e.Button != MouseButtons.Left) 
                {
                    // фиксируем расстояние 
                    delX = e.X - shape.SetX;
                    delY = e.Y - shape.SetY;
                    Dragger = true;
                }
                else if (e.Button != MouseButtons.Right)
                {
                    this.Invalidate();
                }
            }
            else
            {
                Draw = true;
                switch (sshape)
                {
                    case 1: shape = new Triangle(e.X, e.Y); break;
                    case 2: shape = new Square(e.X, e.Y); break;
                    case 3: shape = new Circle(e.X, e.Y); break;
                    default: shape = new Circle(e.X, e.Y); break;
                }
            }
            this.Invalidate();
            //this.Refresh();
        }
    }

    abstract class MyShape
    {
        protected int x0;
        protected int y0;
        public static int r { set; get; }
        public static Color c { set; get; }

        public int SetX { set { x0 = value; } get { return x0; } }
        public int SetY { set { y0 = value; } get { return y0; } }

        public MyShape()
        {
            x0 = 100;
            y0 = 100;
        }
        public MyShape(int x, int y)
        {
            this.x0 = x;
            this.y0 = y;
        }
        static MyShape()
        {
            r = 50;
            c = Color.Green;
        }

        public abstract void Draw(Graphics g);
        public abstract bool Check(int x, int y);
    }
    class Circle : MyShape
    {
        public Circle() : base() { }
        public Circle(int x, int y) : base(x, y) { }

        override public void Draw(Graphics g)
        { 
            g.FillEllipse(new SolidBrush(c), x0 - r, y0 - r, 2 * r, 2 * r);
        }

        override public bool Check(int x, int y) 
        {
            //посчитать катеты, сравнить гипотенузу
            return true;
        }
    }
    class Square : MyShape 
    {
        public Square() : base() { }
        public Square(int x, int y) : base(x, y) { }

        override public void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(c), x0 - r, y0 - r, 2 * r, 2 * r);
        }

        override public bool Check(int x, int y)
        {
            //посчитать 
            return true;
        }
    }
    class Triangle : MyShape 
    {
        public Triangle() : base() { }
        public Triangle(int x, int y) : base(x, y) { }

        override public void Draw(Graphics g)
        {
            Point points1 = new Point(x0 - r, y0 - r / 2);
            Point points2 = new Point(x0, y0 + r / 2);
            Point points3 = new Point(x0 + r, y0 - r / 2);
            Point[] curvePoints = { points1, points2, points3 };
            g.FillPolygon(new SolidBrush(c), curvePoints);
        }

        override public bool Check(int x, int y)
        {
            //посчитать
            return true;
        }
    }
}
