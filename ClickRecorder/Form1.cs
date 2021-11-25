using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

using System.Diagnostics;
using System.Runtime.InteropServices;
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
            MouseHook mouseHook = new MouseHook();

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            lbl_message.Text = "Saving...";
            System.Environment.Exit(1);
        }

        private void btn_record_Click(object sender, EventArgs e)
        {

            lbl_message.Text = "Recording...";

            // Capture the events
            mouseHook.MouseDown += new MouseEventHandler(mouseHook_MouseDown);
            mouseHook.MouseUp += new MouseEventHandler(mouseHook_MouseUp);
            mouseHook.MouseWheel += new MouseEventHandler(mouseHook_MouseWheel);

            // Start watching for mouse events
            mouseHook.Start();


        }

        private void btn_debug_click_Click(object sender, EventArgs e)
        {
            lbl_message.Text = "Debugging...";

            int X = MouseSimulator.X;
            int Y = MouseSimulator.Y;

            clicks.Add(new Click(clicks.Count, X, Y));
            this.Controls.Add(clicks[clicks.Count - 1].box);

        }



        void mouseHook_MouseWheel(object sender, MouseEventArgs e)
        {

            lbl_message.Text = e.Delta.ToString();

            int X = MouseSimulator.X;
            int Y = MouseSimulator.Y;

            clicks.Add(new Click(clicks.Count, X, Y));
            this.Controls.Add(clicks[clicks.Count - 1].box);

        }

        void mouseHook_MouseUp(object sender, MouseEventArgs e)
        {

            lbl_message.Text = e.Button.ToString() + ", " + e.X.ToString() + ", " + e.Y.ToString();


            int X = MouseSimulator.X;
            int Y = MouseSimulator.Y;

            clicks.Add(new Click(clicks.Count, X, Y));
            this.Controls.Add(clicks[clicks.Count - 1].box);

        }

        void mouseHook_MouseDown(object sender, MouseEventArgs e)
        {


            lbl_message.Text = e.Button.ToString() + ", " + e.X.ToString() + ", " + e.Y.ToString();

            int X = MouseSimulator.X;
            int Y = MouseSimulator.Y;

            clicks.Add(new Click(clicks.Count, X, Y));
            this.Controls.Add(clicks[clicks.Count - 1].box);


        }

    }



}
