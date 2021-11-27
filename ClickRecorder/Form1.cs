using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using MouseKeyboardLibrary;

namespace ClickRecorder
{


    public partial class Form1 : Form
    {

        MouseHook mouseHook = new MouseHook();
        List<Click> clicks = new List<Click>();
        public Form1()
        {
            InitializeComponent();
            // Create the mouse hook
            //MouseHook mouseHook = new MouseHook();

        }

        private void Btn_save_Click(object sender, EventArgs e)
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod());
            lbl_message.Text = "Saving...";

            List<Click> json_formatted_clicks = new List<Click>();

            foreach (Click c in clicks)
            {
                c.fileName = c.index.ToString("D3")+".png";
                c.info = c.getText();
            }
            
            string json = JsonConvert.SerializeObject(clicks);
            string path = Path.Combine("data", "clicks.json");
            File.WriteAllText(path, json);
            System.Environment.Exit(1);
        }

        private void Btn_record_Click(object sender, EventArgs e)
        {

            lbl_message.Text = "Recording...";

            // Capture the events
            mouseHook.MouseDown += new MouseEventHandler(MouseHook_MouseDown);
            mouseHook.MouseUp += new MouseEventHandler(MouseHook_MouseUp);
            mouseHook.MouseWheel += new MouseEventHandler(MouseHook_MouseWheel);

            // Start watching for mouse events
            mouseHook.Start();


        }

        private void Btn_debug_click_Click(object sender, EventArgs e)
        {
            lbl_message.Text = "Debugging...";

            int X = MouseSimulator.X;
            int Y = MouseSimulator.Y;

            clicks.Add(new Click(clicks.Count, X, Y));
            this.Controls.Add(clicks[clicks.Count - 1].groupBox);

        }

        void MouseHook_MouseWheel(object sender, MouseEventArgs e)
        {
            lbl_message.Text = e.Delta.ToString();
            int X = MouseSimulator.X;
            int Y = MouseSimulator.Y;
            clicks.Add(new Click(clicks.Count, X, Y));
            this.Controls.Add(clicks[clicks.Count - 1].groupBox);
        }

        void MouseHook_MouseDown(object sender, MouseEventArgs e)
        {
            lbl_message.Text = e.Button.ToString() + ", " + e.X.ToString() + ", " + e.Y.ToString();
            int X = MouseSimulator.X;
            int Y = MouseSimulator.Y;
            clicks.Add(new Click(clicks.Count, X, Y));
            this.Controls.Add(clicks[clicks.Count - 1].groupBox);
        }
        void MouseHook_MouseUp(object sender, MouseEventArgs e)
        {
            lbl_message.Text = e.Button.ToString() + ", " + e.X.ToString() + ", " + e.Y.ToString();
            //int X = MouseSimulator.X;
            //int Y = MouseSimulator.Y;
            //clicks.Add(new Click(clicks.Count, X, Y));
            //this.Controls.Add(clicks[clicks.Count - 1].box);
        }

    }


   
}
