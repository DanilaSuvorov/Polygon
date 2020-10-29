using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mno
{
    public partial class Form1 : Form
    {
        MyShape shape;
        int x, y;
        static int R;
        int sshape;
        static Color col;
        int delX, delY;
        bool Draw = false;
        bool Dragger = false;
        static Form1()
        {
            R = 70;
            col = Color.Green;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (Draw) shape.Draw(e.Graphics);
        }
        private void треугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sshape = 1;
        }

        private void квадратToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sshape = 2;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            shape.SetX = e.X + delX;
            shape.SetY = e.Y + delY;
            this.Invalidate();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Dragger = false;
        }

        private void кругToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sshape = 3;
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (Draw && shape.Check(e.X, e.Y))
            {
                //e.Button = MouseButtons.Left;
                //флаг на перетасиквание
                Dragger = true;
                // фиксируем расстояние 
                delX = e.X - shape.SetX;
                delY = e.Y - shape.SetY;
            }
            else
            {
                Draw = true;
                switch (sshape)
                {
                    case 0: shape = new Circle(e.X, e.Y); break;
                    default: shape = new Circle(e.X, e.Y); break;
                }
            }

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
        { }
        static MyShape()
        { }

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
            //g.FillEllipse(new SolidBrush(c), x0 - r, y0 - r, 2 * r, 2 * r);
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
            //g.FillEllipse(new SolidBrush(c), x0 - r, y0 - r, 2 * r, 2 * r);
        }

        override public bool Check(int x, int y)
        {
            //посчитать
            return true;
        }
    }
}
