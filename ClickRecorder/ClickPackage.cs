using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ClickRecorder
{

    //https://stackoverflow.com/questions/46316025/how-to-resize-controls-inside-groupbox-without-overlapping
    public class ClickPackage
    {
        public int index { get; set; }
        public String fileName { get; set; }
        public int mouseX { get; set; }
        public int mouseY { get; set; }
        public String info { get; set; }
        [JsonIgnore]
        public FlowLayoutPanel panel;
        [JsonIgnore]
        private PictureBox picture;
        [JsonIgnore]
        private TextBox textBox;
        [JsonIgnore]
        private Bitmap bmp;
        public ClickPackage(int _index, int _mouseX, int _mouseY, FlowLayoutPanel _panel)
        {
            this.panel = _panel;
            this.index = _index;
            this.mouseX = _mouseX;
            this.mouseY = _mouseY;

            bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
            }

            this.picture = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = bmp
            };
            panel.Controls.Add(this.picture);

            this.info = "Type a useful description...";
            this.textBox = new TextBox
            {
                AutoSize = true,
                Height=32,
                Text = this.info
            };
            panel.Controls.Add(textBox);


            //this.picture.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
            //this.textBox.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
            this.textBox.Width = this.textBox.Parent.Width;

        }

        public String getText()
        {
            return this.textBox.Text;
        }

        public void saveImage()
        {
            string image_path = Path.Combine("data", this.index.ToString("D3") + ".png");
            bmp.Save(image_path, ImageFormat.Png);  // saves the image THIS IS WHERE I LEFT OFF THE IMAGES ARE NOT SAVING
        }

        public ClickPackage() { }
    }

}
