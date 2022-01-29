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
        public String fileName { get; set; }
        public int mouseX { get; set; }
        public int mouseY { get; set; }
        public String info { get; set; }
        [JsonIgnore]
        public Boolean deleted = false;
        [JsonIgnore]
        public FlowLayoutPanel panel;
        [JsonIgnore]
        private PictureBox picture;
        [JsonIgnore]
        private TextBox textBox;
        [JsonIgnore]
        private Button deleteButton;
        [JsonIgnore]
        public Bitmap bmp;
        
        public ClickPackage(int _index, int _mouseX, int _mouseY, FlowLayoutPanel _panel)
        {
            this.panel = _panel;
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
                Height = 32,
                Text = this.info
            };
            panel.Controls.Add(textBox);


            this.deleteButton = new Button
            {
                Text = "Remove",
                BackColor = Color.Red
            };

            this.deleteButton.Click += new EventHandler(DeleteEvent);

            panel.Controls.Add(deleteButton);

            //this.picture.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
            //this.textBox.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
            this.textBox.Width = this.textBox.Parent.Width;

        }

        private void DeleteEvent(object sender, EventArgs e) { panel.Visible = false; bmp = null; deleted = true; }
        public String getText()
        {
            return this.textBox.Text;
        }

        public ClickPackage() { }
    }

}
