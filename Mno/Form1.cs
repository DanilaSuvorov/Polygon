using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mno
{
    public partial class Form1 : Form
    {
        int shape = 1;
        Mno.Vertex shaper;
        List<Mno.Vertex> vert = new List<Mno.Vertex>();

        int x1 = 100;
        int i = 0;
        int y1 = 100;
        bool up;
        bool down;

        public Form1()
        {
            shaper = new Mno.circle(0, 0);

            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            x1 = e.X;
            y1 = e.Y;
            if (e.Button == MouseButtons.Left)
            {
                if (shape == 1)
                {
                    shaper = new Mno.circle(y1, x1);

                    vert.Add(shaper);
                }
                else
                {
                    if (shape == 2)
                    {
                        shaper = new Mno.square(y1, x1);

                        vert.Add(shaper);
                    }
                    if (shape == 3)
                    {
                        shaper = new Mno.triangle(y1, x1);

                        vert.Add(shaper);
                    }
                }
                vert[i].e = 1;
                this.Invalidate();
                i++;
            }
            if (e.Button == MouseButtons.Right)
            {
                for (int j = 0; j < i; j++)
                {
                    if (vert[j].check(e.X, e.Y)) vert[j].e = 0;
                }
                this.Invalidate();
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Mno.Vertex shaper in vert)
            {
                shaper.l = false;
                if (shaper.e == 1) shaper.dr(e.Graphics);
            }
            for (int i = 0; i < vert.Count; i++)
            {
                for (int j = i + 1; j < vert.Count; j++)
                {
                    up = false;
                    down = false;
                    for (int k = 0; k < vert.Count; k++)
                    {
                        if (k != i && k != j && i != j)
                        {
                            if (((vert[k].y1 - vert[i].y1) * (vert[j].x1 - vert[i].x1)) > ((vert[k].x1 - vert[i].x1) * (vert[j].y1 - vert[i].y1))) up = true;
                            else down = true;
                        }
                    }
                    if(!up || !down)
                    {
                        vert[i].l = true;
                        vert[j].l = true;
                        e.Graphics.DrawLine(new Pen(Color.Black), vert[i].x1, vert[i].y1, vert[j].x1, vert[j].y1);
                    }
                }
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < vert.Count; i++)
            {
                if (!vert[i].l)
                {
                    vert[i].e = 0;
                    this.Invalidate();
                }
                this.Invalidate();
            }
        }
        private void треугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shape = 3;

        }
        private void кругToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shape = 1;
        }
        private void квадратToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            shape = 2;
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            for (int j = 0; j < i; j++)
            {
                if (e.Button == MouseButtons.Left && vert[j].check(e.X, e.Y))
                {
                    vert[j].x1 = e.X;
                    vert[j].y1 = e.Y; 
                    this.Invalidate();
                }
            }
        }
    }
}