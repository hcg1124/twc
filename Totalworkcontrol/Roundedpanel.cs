using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Totalworkcontrol
{
    // ▼▼▼ 둥근 테두리와 왼쪽 라벨만 가진 패널입니다 ▼▼▼
    public class RoundedPanel : Panel
    {
        // 기존 속성들
        private int _cornerRadius = 10;
        private Color _borderColor = Color.LightGray;
        private string _labelText = "Label";
        private int _labelWidth = 80;
        private Color _labelBackColor = Color.FromArgb(240, 240, 240);

        public int CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = value; Invalidate(); }
        }

        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        public string LabelText
        {
            get { return _labelText; }
            set { _labelText = value; Invalidate(); }
        }

        public int LabelWidth
        {
            get { return _labelWidth; }
            set { _labelWidth = value; Invalidate(); }
        }

        public Color LabelBackColor
        {
            get { return _labelBackColor; }
            set { _labelBackColor = value; Invalidate(); }
        }

        public RoundedPanel()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = new GraphicsPath())
            {
                Rectangle rect = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
                int radius = _cornerRadius * 2;
                if (radius <= 0) radius = 1;

                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();

                e.Graphics.SetClip(path);

                using (SolidBrush brush = new SolidBrush(this.BackColor))
                {
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    e.Graphics.FillRectangle(brush, ClientRectangle);
                }

                using (SolidBrush labelBrush = new SolidBrush(_labelBackColor))
                {
                    e.Graphics.FillRectangle(labelBrush, 0, 0, _labelWidth, this.Height);
                }

                using (Pen separatorPen = new Pen(_borderColor))
                {
                    e.Graphics.DrawLine(separatorPen, _labelWidth, 0, _labelWidth, this.Height);
                }

                TextRenderer.DrawText(e.Graphics, _labelText, this.Font,
                    new Rectangle(0, 0, _labelWidth, this.Height),
                    this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Graphics.ResetClip();

                using (Pen pen = new Pen(_borderColor, 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }
    }
}
