namespace ScreenCaptureTool
{
    partial class frmToolBar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picClose = new System.Windows.Forms.PictureBox();
            this.picTop = new System.Windows.Forms.PictureBox();
            this.picSave = new System.Windows.Forms.PictureBox();
            this.picCopy = new System.Windows.Forms.PictureBox();
            this.picDraw = new System.Windows.Forms.PictureBox();
            this.pic4 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCopy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDraw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic4)).BeginInit();
            this.SuspendLayout();
            // 
            // picClose
            // 
            this.picClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose.Image = global::ScreenCaptureTool.Properties.Resources.close;
            this.picClose.Location = new System.Drawing.Point(187, 4);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(20, 20);
            this.picClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picClose.TabIndex = 9;
            this.picClose.TabStop = false;
            // 
            // picTop
            // 
            this.picTop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picTop.Image = global::ScreenCaptureTool.Properties.Resources.top;
            this.picTop.Location = new System.Drawing.Point(127, 4);
            this.picTop.Name = "picTop";
            this.picTop.Size = new System.Drawing.Size(20, 20);
            this.picTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTop.TabIndex = 8;
            this.picTop.TabStop = false;
            // 
            // picSave
            // 
            this.picSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSave.Image = global::ScreenCaptureTool.Properties.Resources.save;
            this.picSave.Location = new System.Drawing.Point(88, 4);
            this.picSave.Name = "picSave";
            this.picSave.Size = new System.Drawing.Size(20, 20);
            this.picSave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSave.TabIndex = 7;
            this.picSave.TabStop = false;
            // 
            // picCopy
            // 
            this.picCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCopy.Image = global::ScreenCaptureTool.Properties.Resources.copy;
            this.picCopy.Location = new System.Drawing.Point(48, 4);
            this.picCopy.Name = "picCopy";
            this.picCopy.Size = new System.Drawing.Size(20, 20);
            this.picCopy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCopy.TabIndex = 6;
            this.picCopy.TabStop = false;
            // 
            // picDraw
            // 
            this.picDraw.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDraw.Image = global::ScreenCaptureTool.Properties.Resources.pen;
            this.picDraw.Location = new System.Drawing.Point(9, 4);
            this.picDraw.Name = "picDraw";
            this.picDraw.Size = new System.Drawing.Size(20, 20);
            this.picDraw.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDraw.TabIndex = 5;
            this.picDraw.TabStop = false;
            // 
            // pic4
            // 
            this.pic4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic4.Image = global::ScreenCaptureTool.Properties.Resources.split;
            this.pic4.Location = new System.Drawing.Point(157, 4);
            this.pic4.Name = "pic4";
            this.pic4.Size = new System.Drawing.Size(20, 20);
            this.pic4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic4.TabIndex = 10;
            this.pic4.TabStop = false;
            // 
            // frmToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(218, 28);
            this.Controls.Add(this.pic4);
            this.Controls.Add(this.picClose);
            this.Controls.Add(this.picTop);
            this.Controls.Add(this.picSave);
            this.Controls.Add(this.picCopy);
            this.Controls.Add(this.picDraw);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(1, 1);
            this.Name = "frmToolBar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmToolBar";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCopy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDraw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox picDraw;
        private System.Windows.Forms.PictureBox picCopy;
        private System.Windows.Forms.PictureBox picTop;
        private System.Windows.Forms.PictureBox picSave;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.PictureBox pic4;
    }
}