using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenCaptureTool
{
    public partial class frmMain : Form
    {
        private Rectangle screenshotRect; // 截图矩形区域
        private Bitmap screenshot; // 保存截图

        private Point startPoint; // 线路起点
        private Point endPoint; // 线路终点
        private Bitmap drawingBitmap; // 绘制的图像
        private Graphics drawingGraphics; // 绘图对象
        private Form form;

        //private bool IsPrimary = true;

        private KeyboardHook keyboardHook;


        protected override void OnLoad(EventArgs e)
        {
            InitialTray();
            this.TopMost = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }

        public frmMain()
        {
            InitializeComponent();
            
            // 初始化键盘钩子
            keyboardHook = new KeyboardHook();
            keyboardHook.KeyPressed += KeyboardHook_KeyPressed;
        }

        private void KeyboardHook_KeyPressed(object sender, KeyControlEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D1)
            {
                Console.WriteLine("ScreenCapture");
                ScreenCapture();
                e.Handled = true;
            }

        }

        private void ScreenCapture()
        {
            this.Hide();
            // 创建一个全屏窗体

            if (form != null)
            {
                form.Close();
            }


            form = new Form();
            form.TopMost= true;
            form.TopLevel= true;
            form.Opacity = 0.2;
            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.Manual;
            form.Width = Screen.AllScreens.Sum(i => i.Bounds.Width);
            form.Height = Screen.PrimaryScreen.Bounds.Height;

            // 创建一个 PictureBox 控件用于绘制线路
            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.BackColor = Color.White;
            pictureBox.Paint += (senger, e) => e.Graphics.DrawImage(drawingBitmap, Point.Empty);
            pictureBox.BorderStyle = BorderStyle.FixedSingle;

            // 在窗体上添加 PictureBox 控件
            form.Controls.Add(pictureBox);

            // 创建绘图对象
            drawingBitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            drawingGraphics = Graphics.FromImage(drawingBitmap);

            form.Resize += (sender, e) =>
            {
                drawingBitmap = new Bitmap(form.Width, form.Height);
                drawingGraphics = Graphics.FromImage(drawingBitmap);
            };

            form.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    form.Close();
                    //this.Show();
                }
            };

            // 注册鼠标按下事件
            pictureBox.MouseDown += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    // 记录鼠标按下的起点坐标
                    startPoint = e.Location;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    form.Close();
                    //this.Show();
                }
            };

            pictureBox.MouseMove += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        drawingGraphics.Clear(Color.White);
                        endPoint = e.Location;
                        DrawSquare(drawingGraphics, startPoint, endPoint);
                        (sender as PictureBox).Invalidate();
                        Console.WriteLine("start:" + startPoint.ToString() + ",end:" + endPoint.ToString());
                    }
                }
            };

            // 注册鼠标抬起事件
            pictureBox.MouseUp += (sender, e) =>
            {

                if (e.Button == MouseButtons.Left)
                {
                    startPoint.X += 2;
                    startPoint.Y += 2;
                    Point leftTop = new Point(startPoint.X, startPoint.Y);
                    Point rightTop = new Point(endPoint.X, startPoint.Y);
                    Point leftBottom = new Point(startPoint.X, endPoint.Y);
                    Point rightBottom = new Point(endPoint.X, endPoint.Y);

                    // 计算截图的矩形区域
                    int width = Math.Abs(e.X - startPoint.X);
                    int height = Math.Abs(e.Y - startPoint.Y);


                    screenshotRect = new Rectangle(Math.Min(startPoint.X, e.X), Math.Min(startPoint.Y, e.Y), width, height);
                    Console.WriteLine($"X:{screenshotRect.X} Y:{screenshotRect.Y}");
                    Console.WriteLine($"鼠标 X:{e.X} Y:{e.Y}");
                    if (screenshotRect.Width == 0 || screenshotRect.Height == 0)
                        return;

                    // 进行屏幕截图
                    screenshot = new Bitmap(screenshotRect.Width, screenshotRect.Height);
                    using (Graphics g = Graphics.FromImage(screenshot))
                    {
                        form.Opacity = 0;
                        g.CopyFromScreen(screenshotRect.Location, Point.Empty, screenshotRect.Size);
                    }

                    //this.Show();
                    form.Hide();


                    frmPreview frmPreview = new frmPreview(startPoint, endPoint, screenshot, screenshotRect);
                    frmPreview.Show();
                }

            };
            form.ShowDialog();
        }


        static void DrawSquare(Graphics g, Point start, Point end)
        {
            Point leftTop = new Point(start.X, start.Y);
            Point rightTop = new Point(end.X, start.Y);
            Point leftBottom = new Point(start.X, end.Y);
            Point rightBottom = new Point(end.X, end.Y);

            Pen pen = new Pen(Color.Red);
            pen.Width = 2f;

            g.DrawLine(pen, leftTop, rightTop);
            g.DrawLine(pen, rightTop, rightBottom);
            g.DrawLine(pen, rightBottom, leftBottom);
            g.DrawLine(pen, leftBottom, leftTop);
        }


        private static void EnableTitleBarDrag(Form form, Control control)
        {
            Point curP = new Point(0, 0);
            control.MouseDown += delegate (object sender, MouseEventArgs e)
            {
                control.Cursor = Cursors.SizeAll;
                curP = new Point(e.X, e.Y);
                control.MouseMove += Moved;
            };
            control.MouseUp += delegate (object sender, MouseEventArgs e)
            {
                control.Cursor = Cursors.Default;
                control.MouseMove -= Moved;
            };

            void Moved(object sender, MouseEventArgs e)
            {
                form.Location = new Point(form.Location.X + (e.X - curP.X), form.Location.Y + (e.Y - curP.Y));
            }
        }

        private void chkMainScreen_CheckedChanged(object sender, EventArgs e)
        {
            //IsPrimary = chkMainScreen.Checked;
        }

        private NotifyIcon notifyIcon = null;
        private void InitialTray()
        {
            //隐藏主窗体
            this.Hide();

            //实例化一个NotifyIcon对象
            notifyIcon = new NotifyIcon();
            //托盘图标气泡显示的内容
            notifyIcon.BalloonTipText = "正在后台运行";
            //托盘图标显示的内容
            notifyIcon.Text = "截图工具";
            //注意：下面的路径可以是绝对路径、相对路径。但是需要注意的是：文件必须是一个.ico格式
            notifyIcon.Icon = this.Icon;
            //true表示在托盘区可见，false表示在托盘区不可见
            notifyIcon.Visible = true;
            //气泡显示的时间（单位是毫秒）
            notifyIcon.ShowBalloonTip(2000);
            notifyIcon.MouseDoubleClick += delegate (object sender, System.Windows.Forms.MouseEventArgs e)
            {
                //鼠标左键单击
                //if (e.Button == MouseButtons.Left)
                //{
                //    //如果窗体是可见的，那么鼠标左击托盘区图标后，窗体为不可见
                //    if (this.Visible == true)
                //    {
                //        this.Visible = false;
                //    }
                //    else
                //    {
                //        this.Visible = true;
                //        this.Activate();
                //    }
                //}
            };

            //设置二级菜单
            //MenuItem setting1 = new MenuItem("二级菜单1");
            //MenuItem setting2 = new MenuItem("二级菜单2");
            //MenuItem setting = new MenuItem("一级菜单", new MenuItem[]{setting1,setting2});

            //MenuItem help = new MenuItem("打开");
            //help.Click += delegate (object sender, EventArgs e)
            //{
            //    this.Show();
            //};

            //退出菜单项
            MenuItem exit = new MenuItem("退出");
            exit.Click += delegate (object sender, EventArgs e)
            {
                //退出程序
                System.Environment.Exit(0);
            };

            //关联托盘控件
            //注释的这一行与下一行的区别就是参数不同，setting这个参数是为了实现二级菜单
            //MenuItem[] childen = new MenuItem[] { setting, help, about, exit };
            MenuItem[] childen = new MenuItem[] { exit };
            notifyIcon.ContextMenu = new ContextMenu(childen);

            //窗体关闭时触发
            this.FormClosing += delegate (object sender, FormClosingEventArgs e)
            {
                e.Cancel = true;
                //通过这里可以看出，这里的关闭其实不是真正意义上的“关闭”，而是将窗体隐藏，实现一个“伪关闭”
                this.Hide();
            };
        }
    }
}