using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenCaptureTool
{
    public delegate void OnFrmPreviewMoveEvent(object sender, Point location);
    public partial class frmPreview : Form
    {
        public event OnFrmPreviewMoveEvent OnFrmPreviewMove;
        private Point startPoint; // 线路起点
        private Point endPoint; // 线路终点
        private Bitmap drawingBitmap; // 绘制的图像
        private Graphics drawingGraphics; // 绘图对象

        //private bool IsEnableBarDarg = true;
        private DrawType DrawType=DrawType.None;

        PictureBox pictureBox;
        frmToolBar frmToolBar;
        Pen penColor;

        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        public frmPreview(Point startPoint, Point endPoint, Bitmap bitmap, Rectangle rectangle)
        {
            LoadMenus(this);

            this.TopMost = true;
            this.ShowInTaskbar = false;

            this.Size = rectangle.Size;
            this.FormBorderStyle = FormBorderStyle.None;
            //this.StartPosition = FormStartPosition.CenterScreen;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y));
            Init(bitmap);
            ToolBar();
        }

        //protected override void OnLoad(EventArgs e)
        //{
        //    drawingBitmap = new Bitmap(this.Width, this.Height);
        //    drawingGraphics = Graphics.FromImage(drawingBitmap);

        //}

        private void ToolBar()
        {
            frmToolBar = new frmToolBar();
            frmToolBar.ShowInTaskbar = false;
            frmToolBar.Owner = this;
            frmToolBar.TopMost = true;
            frmToolBar.OnTop = true;
            frmToolBar.PenColorChange += (e) =>
            {
                penColor = e;
            };
            frmToolBar.OnDrawTypeChange += (s, e) =>
            {
                //IsEnableBarDarg = !e;
                DrawType = e;
            };
            frmToolBar.OnTopChange += (s, e) =>
            {
                this.TopMost = e;
                frmToolBar.TopMost = e;
            };
            frmToolBar.OnCopyClick += (s, e) =>
            {
                Clipboard.SetImage(drawingBitmap);
            };
            frmToolBar.OnSaveClick += (s, e) =>
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Title = "屏幕截图保存路径";
                fileDialog.FileName = "截图";
                fileDialog.Filter = "image Files(*.bmp)|*.bmp";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    drawingBitmap.Save(fileDialog.FileName, ImageFormat.Bmp);
                }
            };
            frmToolBar.OnCloseClick += (s, e) =>
            {
                this.Close();
                frmToolBar.Close();
            };
            this.FormClosed += (s, e) =>
            {
                frmToolBar.Close();
            };
            frmToolBar.FormBorderStyle = FormBorderStyle.None;
            frmToolBar.StartPosition = FormStartPosition.Manual;
            frmToolBar.Location = new Point(this.Location.X, this.Location.Y + this.Height + 1);
            OnFrmPreviewMove += delegate (object sender, Point location)
            {
                frmToolBar.Location = new Point(location.X, location.Y + this.Height + 1); ;
            };
            frmToolBar.Show();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder3D(e.Graphics, this.ClientRectangle, Border3DStyle.Raised);
        }

        private void LoadMenus(Form previewForm)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            ToolStripMenuItem menuItem = new ToolStripMenuItem("工具栏")
            {
                Checked = true
            };

            menuItem.Click += delegate (object s1, EventArgs e1)
            {
                menuItem.Checked = !menuItem.Checked;
                if (!menuItem.Checked)
                {
                    frmToolBar.HideEx();
                }
                else
                    frmToolBar.ShowEx();
            };


            contextMenuStrip.Items.Add(menuItem);

            previewForm.ContextMenuStrip = contextMenuStrip;
        }

        private void Init(Bitmap bitmap)
        {
            penColor = new Pen(Color.Red, 3f);

            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.BackColor = Color.White;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;

            drawingBitmap = bitmap;
            drawingGraphics = Graphics.FromImage(bitmap);

            pictureBox.Invalidate();

            pictureBox.Paint += (sender, e) =>
            {
                e.Graphics.DrawImage(drawingBitmap, Point.Empty);
            };

            pictureBox.MouseMove += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left && DrawType!=DrawType.None)
                {
                    if (DrawType == DrawType.Pen)
                    {
                        endPoint = e.Location;

                        // 创建自定义的不透明的 Pen 对象
                        Pen pen = new Pen(penColor.Color, 3f);
                        pen.Alignment = PenAlignment.Center;

                        // 绘制线路
                        drawingGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                        drawingGraphics.DrawLine(pen, startPoint, endPoint);
                        startPoint = endPoint;

                        // 释放自定义的 Pen 对象
                        pen.Dispose();

                        (sender as PictureBox).Invalidate();
                    }
                }
            };

            pictureBox.MouseDown += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    startPoint = e.Location;
                }
            };


            EnableTitleBarDrag(this, pictureBox);

            // 在窗体上添加 PictureBox 控件
            this.Controls.Add(pictureBox);
        }

        private void EnableTitleBarDrag(Form form, Control control)
        {
            Point curP = new Point(0, 0);
            control.MouseDown += delegate (object sender, MouseEventArgs e)
            {
                if (DrawType != DrawType.None)
                {
                    return;
                }
                control.Cursor = Cursors.SizeAll;
                curP = new Point(e.X, e.Y);
                control.MouseMove += Moved;
            };
            control.MouseUp += delegate (object sender, MouseEventArgs e)
            {
                if (DrawType != DrawType.None)
                {
                    return;
                }
                control.Cursor = Cursors.Default;
                control.MouseMove -= Moved;
            };

            void Moved(object sender, MouseEventArgs e)
            {
                if (DrawType != DrawType.None)
                {
                    return;
                }
                form.Location = new Point(form.Location.X + (e.X - curP.X), form.Location.Y + (e.Y - curP.Y));
                OnFrmPreviewMove?.Invoke(form, form.Location);
            }
        }
    }
}
