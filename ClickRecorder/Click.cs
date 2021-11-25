using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickRecorder
{

    //https://stackoverflow.com/questions/46316025/how-to-resize-controls-inside-groupbox-without-overlapping
    public class Click
    {
        public int index { get; set; }
        public int mouseX { get; set; }
        public int mouseY { get; set; }
        public String info { get; set; }
        public GroupBox box;
        public Click(int _index, int _mouseX, int _mouseY)
        {
            this.index = _index;
            this.mouseX = _mouseX;
            this.mouseY = _mouseY;
            //Configure the GroupBox
            this.box = new GroupBox();
            this.box.Location = new Point(16, 96 + (this.index * 96));
            //this.box.Size = new Size(256, 64);
            this.box.Text = "Screenshot " + this.index;
            this.box.AutoSize = true;
            this.box.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //this.box.Padding = new Padding(0);
            //this.box.Margin = new Padding(0);

            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                bmp.Save("screenshot.png");  // saves the image
            }

            PictureBox picture = new PictureBox();
            //picture.Location = new Point(16, 64 + (this.index * 64));
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.Dock = DockStyle.Left;
            //picture.ClientSize = new Size(48,48);
            //picture.Image = bmp.GetThumbnailImage(64, 64, null, new System.IntPtr());
            picture.Image = bmp;
            //picture.Padding = new Padding(16);
            box.Controls.Add(picture);


            TextBox text = new TextBox();
            //text.Location = new Point(64, 48 + (this.index * 48));
            text.Height = 16;
            text.Width = this.box.Width;
            //text.Anchor = AnchorStyles.Right;
            //text.BackColor = Color.White;
            //text.ForeColor = Color.Black;
            //this.box.AutoSize = true;
            //this.box.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.info = "X: " + this.mouseX + " Y: " + this.mouseY;
            text.Text = this.info;
            box.Controls.Add(text);


        }
        public Click() { }
    }

}
