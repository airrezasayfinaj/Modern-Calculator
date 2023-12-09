using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CtlButton
{
    public class CtlBut : Button
    {
        private int bordersize = 0;
        private int borderradios = 60;
        private Color bordercolor = Color.FromArgb(32, 32, 32);
        public int Bordersize
        {
            get => bordersize;
            set { bordersize = value; Invalidate(); }
        }
        public int BorderRadios
        {
            get => borderradios;
            set { borderradios = (value <= Height)? value:Height; Invalidate(); }
        }
        public Color BackgroundColor
        {
            get => BackColor; set { BackColor = value; }
        }
        public Color TextColor
        {
            get => ForeColor; set { ForeColor = value; }
        }
        public CtlBut()
        {
            Size = new Size(200, 100);
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            BackColor = Color.FromArgb(32, 32, 32);
            ForeColor = Color.White;
            Resize += new EventHandler(Button_Resize);
        }

        private void Button_Resize(object sender, EventArgs e)
        {
            if (borderradios > Height) borderradios = Height;
        }

        private GraphicsPath GetFigurePath(RectangleF rectangle,float raduis)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.StartFigure();
            graphicsPath.AddArc(rectangle.X, rectangle.Y, raduis, raduis, 180, 90);
            graphicsPath.AddArc(rectangle.Width -raduis, rectangle.Y, raduis,raduis, 270, 90);
            graphicsPath.AddArc(rectangle.Width - raduis,rectangle.Height - raduis,raduis,raduis, 0, 90);
            graphicsPath.AddArc(rectangle.X, rectangle.Height- raduis, raduis, raduis, 90, 90);
            graphicsPath.CloseFigure();
            return graphicsPath;
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            RectangleF rectanglesurface = new RectangleF(0, 0, Width, Height);
            RectangleF rectangleborder = new RectangleF(1, 1, Width-0.5f, Height-1);
            if (BorderRadios > 1)
            {
                using (GraphicsPath graphicsPathsurface = GetFigurePath(rectanglesurface, borderradios))
                using (GraphicsPath graphicsPathborder = GetFigurePath(rectangleborder, borderradios-1f))
                using (Pen pensurface=new Pen(Parent.BackColor,2))
                using(Pen penborder =new Pen(bordercolor,bordersize))
                {
                    penborder.Alignment = PenAlignment.Inset;
                    Region = new Region(graphicsPathsurface);
                    pevent.Graphics.DrawPath(penborder, graphicsPathsurface);
                    if (bordersize >= 1)
                    {
                        pevent.Graphics.DrawPath(penborder, graphicsPathborder);
                    }
                }
            }
            else
            {
                Region = new Region(rectanglesurface);
                if (bordersize >= 1)
                {
                    using (Pen penborder=new Pen(bordercolor, bordersize))
                    {
                        penborder.Alignment=PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penborder, 0, 0, Width - 1, Height - 1);
                    }
                }
            }

        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }

        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            if (DesignMode) Invalidate();
        }
    }
}
