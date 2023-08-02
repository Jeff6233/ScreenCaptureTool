using ScreenCaptureTool.Properties;
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
    public delegate void OnDrawTypeChangeHandler(object sender, DrawType drawType);
    public delegate void OnTopChangeHandler(object sender, bool top);
    public partial class frmToolBar : Form
    {
        public event EventHandler OnCopyClick;
        public event EventHandler OnSaveClick;
        public event EventHandler OnCloseClick;
        public event OnDrawTypeChangeHandler OnDrawTypeChange;
        public event OnTopChangeHandler OnTopChange;

        public event PenColorChangeHandler PenColorChange;
        private DrawType _drawType;
        public DrawType DrawType
        {
            get { return _drawType; }
            set
            {
                _drawType = value;
                OnDrawTypeChange?.Invoke(this, value);
            }
        }
        private bool _onTop;
        public bool OnTop
        {
            get { return _onTop; }
            set
            {
                _onTop = value;
                OnTopChange?.Invoke(this, value);
            }
        }

        public void HideEx()
        {
            this.Hide();
            penBar.Hide();
        }
        public void ShowEx()
        {
            penBar.Show();
            this.Show();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            penBar.Close();
            base.OnFormClosed(e);
        }

        private bool OnDraw = false;

        frmPenBar penBar;
        public frmToolBar()
        {
            InitializeComponent();

            penBar = new frmPenBar();
            penBar.Owner= this;
            penBar.ShowInTaskbar = false;
            penBar.TopMost = true;
            penBar.TopLevel = true;
            penBar.StartPosition = FormStartPosition.Manual;
            penBar.PenColorChange += (e) =>
            {
                PenColorChange(e);
            };
            this.picDraw.Click += (s, e) =>
            {
                OnDraw = !OnDraw;
                if (!OnDraw)
                {
                    picDraw.Image = Resources.pen;
                    penBar.Hide();

                    DrawType = DrawType.None;
                }
                else
                {
                    picDraw.Image = Resources.pen;
                    penBar.Location = new Point(this.Location.X, this.Location.Y + this.Height + 1);
                    penBar.Show();

                    DrawType = DrawType.Pen;
                }
            };

            this.picTop.Click += (s, e) =>
            {
                OnTop = !OnTop;
                if (!OnTop)
                {
                    picTop.Image = Resources.top;
                }
                else
                {
                    picTop.Image = Resources.top;
                }
            };

            this.picCopy.Click += (s, e) =>
            {
                OnCopyClick?.Invoke(s, e);
            };

            this.picSave.Click += (s, e) =>
            {
                OnSaveClick?.Invoke(s, e);
            };
            this.picClose.Click += (s, e) =>
            {
                OnCloseClick?.Invoke(s, e);
                penBar.Close();
            };

            
        }
    }
}
