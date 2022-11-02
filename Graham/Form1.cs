using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Drawing.Drawing2D;

namespace Graham
{
	public partial class Form1 : Form
	{
		Graphics g;
		List<Point> bPoints = new List<Point>();

		Color LineColor = Color.WhiteSmoke;

		int current = 0;
		List<Point> gPoints;
		Stack<Point> stack = new Stack<Point>();

		public Form1()
		{
			InitializeComponent();
			pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			g = Graphics.FromImage(pictureBox1.Image);
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void CreatePoint(Point x)
		{
			bPoints.Add(x);
			DrawPoint(x, LineColor);
		}

		private void DrawPoint(Point x,Color color)
		{
			Pen pen = new Pen(color);
			g.DrawEllipse(pen, x.X - 1, x.Y - 1, 3, 3);
			pictureBox1.Invalidate();
		}

		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			CreatePoint(e.Location);
		}

		private void ExecButton_Click(object sender, EventArgs e)
		{
			if (bPoints.Count < 3)
				return;

			g.Clear(pictureBox1.BackColor);

			gPoints = new List<Point>();

			Point mn = bPoints.Min(new PointComparer());
			gPoints.Add(mn);

			foreach (Point p in bPoints)
			{
				if (p != mn)
				{
					DrawPoint(p, LineColor);
					gPoints.Add(p);
				}
			}

			DrawPoint(mn, Color.Red);

			gPoints.Sort(1, gPoints.Count-1, new PointRotateComparer(mn));

			current = 2;

			stack.Clear();

			stack.Push(gPoints[0]);
			stack.Push(gPoints[1]);

			Stepf();
			
		}

		

		private void Stepf()
		{
			if (current >= gPoints.Count)
				return;

			//!!REWRITE
			while (Rotate(stack.ElementAt(1), stack.Peek(), gPoints[current]) > 0)
			{
				stack.Pop();
			}

			stack.Push(gPoints[current]);
			++current;

			g.Clear(pictureBox1.BackColor);

			foreach (Point p in gPoints)
				DrawPoint(p, LineColor);

			g.DrawLine(new Pen(LineColor), gPoints[0], stack.Peek());
			g.DrawLine(new Pen(LineColor), gPoints[0], stack.Last());

			for (int i = 1; i < stack.Count; ++i)
			{
				g.DrawLine(new Pen(LineColor), stack.ElementAt(i), stack.ElementAt(i - 1));
			}

			pictureBox1.Invalidate();
		}


		class PointComparer : IComparer<Point>
		{
			public int Compare(Point a, Point b)
			{
				if (a.X < b.X)
					return -1;
				if (a.X > b.X)
					return 1;
				if (a.Y < b.Y)
					return -1;
				if (a.Y > b.Y)
					return 1;
				return 0;
			}
		}

		public static float Rotate(Point A, Point B, Point C)
		{
			return (B.X - A.X) * (C.Y - B.Y) - (B.Y - A.Y) * (C.X - B.X);
		}

		class PointRotateComparer : IComparer<Point>
		{
			private Point rPoint;
			public PointRotateComparer(Point x)
			{
				rPoint = x;
			}

			public int Compare(Point a, Point b)
			{
				float r1 = Rotate(rPoint, a, b);
				float r2 = Rotate(rPoint, b, a);

				if (r1 < r2)
				{
					return -1;
				}
				if (r1 > r2)
				{
					return 1;
				}
				return 0;
			}
		}

		void Clear()
		{
			g.Clear(pictureBox1.BackColor);
			gPoints.Clear();
			bPoints.Clear();
		}

		private void StepButton_Click(object sender, EventArgs e)
		{
			Stepf();
		}

		private void ClearButton_Click(object sender, EventArgs e)
		{
			Clear();
		}
	}
}