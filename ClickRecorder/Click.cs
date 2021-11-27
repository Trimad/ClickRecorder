using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ClickRecorder
{

    //https://stackoverflow.com/questions/46316025/how-to-resize-controls-inside-groupbox-without-overlapping
    public class Click
    {
        public int index { get; set; }
        public String fileName { get; set; }
        public int mouseX { get; set; }
        public int mouseY { get; set; }
        public String info { get; set; }
        [JsonIgnore]
        public GroupBox groupBox;
        [JsonIgnore]
        private PictureBox picture;
        [JsonIgnore]
        private TextBox textBox;
        public Click(int _index, int _mouseX, int _mouseY)
        {
            this.index = _index;
            this.mouseX = _mouseX;
            this.mouseY = _mouseY;
            //Configure the GroupBox
            this.groupBox = new GroupBox();
            this.groupBox.Location = new Point(16, 96 + (this.index * 96));
            //this.box.Size = new Size(256, 64);
            this.groupBox.Text = "Screenshot " + this.index;
            this.groupBox.AutoSize = true;
            this.groupBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //this.box.Padding = new Padding(0);
            //this.box.Margin = new Padding(0);

            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                string image_path = Path.Combine("data", this.index.ToString("D3") + ".png");
                bmp.Save(image_path, ImageFormat.Png);  // saves the image
                

            }

            this.picture = new PictureBox();
            //picture.Location = new Point(16, 64 + (this.index * 64));
            this.picture.SizeMode = PictureBoxSizeMode.StretchImage;
            this.picture.Dock = DockStyle.Left;
            //picture.ClientSize = new Size(48,48);
            //picture.Image = bmp.GetThumbnailImage(64, 64, null, new System.IntPtr());
            this.picture.Image = bmp;
            //picture.Padding = new Padding(16);
            groupBox.Controls.Add(this.picture);


            this.textBox = new TextBox();
            //text.Location = new Point(64, 48 + (this.index * 48));
            textBox.Height = 16;
            textBox.Width = this.groupBox.Width;
            //text.Anchor = AnchorStyles.Right;
            //text.BackColor = Color.White;
            //text.ForeColor = Color.Black;
            //this.box.AutoSize = true;
            //this.box.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.info = "X: " + this.mouseX + " Y: " + this.mouseY;
            textBox.Text = this.info;
            groupBox.Controls.Add(textBox);


        }

        public String getText()
        {
            return this.textBox.Text;
        }

        public Click() { }
    }

}
