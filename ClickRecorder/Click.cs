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
            this.box.Location = new Point(16, 128 + (this.index * 64));
            //this.box.Size = new Size(256, 32);
            this.box.Text = "Screenshot " + this.index;
            this.box.Name = "MyGroupbox";
            this.box.AutoSize = true;


            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                bmp.Save("screenshot.png");  // saves the image
            }

            PictureBox picture = new PictureBox();
            picture.Location = new Point(0, 0);
            picture.Width = 64;
            picture.Dock = DockStyle.Left;
            picture.Height = 64;
            picture.Image = bmp.GetThumbnailImage(64, 64, null, new System.IntPtr());
            box.Controls.Add(picture);


            TextBox text = new TextBox();
            text.Location = new Point(70, (this.index * 64));
            text.Width = this.box.Width -64;
            text.BackColor = Color.White;
            text.ForeColor = Color.Red;
            text.AutoSize = true;
            this.info = "X: " + this.mouseX + " Y: " + this.mouseY;
            text.Text = this.info;
            text.Name = "text_box1";
            box.Controls.Add(text);


        }

    }

}
