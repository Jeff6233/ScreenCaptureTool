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
    public delegate void ColorChangeHandler(string color, bool on);
    public delegate void PenColorChangeHandler(Pen pen);
    public partial class frmPenBar : Form
    {
        protected event ColorChangeHandler RedChange;
        protected event ColorChangeHandler GreenChange;
        protected event ColorChangeHandler BlueChange;
        public event PenColorChangeHandler PenColorChange;

        private bool red;
        private bool green;
        private bool blue;
        protected bool Red
        {
            get { return red; }
            set { red = value; RedChange?.Invoke("Red", value); }
        }
        protected bool Green
        {
            get { return green; }
            set { green = value; GreenChange?.Invoke("Green", value); }
        }
        protected bool Blue
        {
            get { return blue; }
            set { blue = value; BlueChange?.Invoke("Blue", value); }
        }

        public frmPenBar()
        {
            InitializeComponent();
            Red = true;
            Green = false;
            Blue = false;
            this.picRed.Image = Resources.e_red;

            RedChange += (s, e) =>
            {
                if (e)
                    this.picRed.Image = Resources.e_red;
                else
                    this.picRed.Image = Resources.f_red;
            };

            BlueChange += (s, e) =>
            {
                if (e)
                    this.picBlue.Image = Resources.e_blue;
                else
                    this.picBlue.Image = Resources.f_blue;
            };

            GreenChange += (s, e) =>
            {
                if (e)
                    this.picGreen.Image = Resources.e_green;
                else
                    this.picGreen.Image = Resources.f_green;
            };

            this.picRed.Click += (s, e) =>
            {
                this.Red = true;
                this.Green = false;
                this.Blue = false;
                PenColorChange?.Invoke(Pens.Red);
            };

            this.picBlue.Click += (s, e) =>
            {
                this.Red = false;
                this.Green = false;
                this.Blue = true;
                PenColorChange?.Invoke(Pens.Blue);
            };

            this.picGreen.Click += (s, e) =>
            {
                this.Red = false;
                this.Green = true;
                this.Blue = false;
                PenColorChange?.Invoke(Pens.Green);
            };



        }
    }
}
