using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using Gma.System.MouseKeyHook;//https://github.com/gmamaladze/globalmousekeyhook/blob/vNext/examples/FormsExample/Main.cs
using System.ComponentModel;
using System.Drawing.Imaging;

namespace ClickRecorder
{


    public partial class Form1 : Form
    {
        private IKeyboardMouseEvents m_Events;
        Point click;
        List<ClickPackage> clicks = new List<ClickPackage>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Main_Closing(object sender, CancelEventArgs e)
        {
            Unsubscribe();
        }

        private void SubscribeGlobal()
        {
            Unsubscribe();
            Subscribe(Hook.GlobalEvents());

        }

        private void Subscribe(IKeyboardMouseEvents events)
        {
            m_Events = events;
            m_Events.KeyDown += OnKeyDown;
            m_Events.KeyUp += OnKeyUp;
            m_Events.KeyPress += HookManager_KeyPress;

            m_Events.MouseClick += OnMouseClick;
            m_Events.MouseDoubleClick += OnMouseDoubleClick;

            m_Events.MouseMove += HookManager_MouseMove;

            m_Events.MouseDragStarted += OnMouseDragStarted;
            m_Events.MouseDragFinished += OnMouseDragFinished;

        }

        private void Unsubscribe()
        {
            if (m_Events == null) return;
            //m_Events.KeyDown -= OnKeyDown;
            //m_Events.KeyUp -= OnKeyUp;
            //m_Events.KeyPress -= HookManager_KeyPress;

            m_Events.MouseClick -= OnMouseClick;
            m_Events.MouseDoubleClick -= OnMouseDoubleClick;

            //m_Events.MouseMove -= HookManager_MouseMove;

            //m_Events.MouseDragStarted -= OnMouseDragStarted;
            //m_Events.MouseDragFinished -= OnMouseDragFinished;

            m_Events.Dispose();
            m_Events = null;

        }

        private void HookManager_Supress(object sender, MouseEventExtArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                Console.WriteLine(string.Format("MouseDown \t\t {0}\n", e.Button));
                return;
            }

            Console.WriteLine(string.Format("MouseDown \t\t {0} Suppressed\n", e.Button));
            e.Handled = true;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(string.Format("KeyDown  \t\t {0}\n", e.KeyCode));
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(string.Format("KeyUp  \t\t {0}\n", e.KeyCode));
        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine(string.Format("KeyPress \t\t {0}\n", e.KeyChar));
        }

        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            lbl_message.Text = string.Format("x={0:0000}; y={1:0000}", e.X, e.Y);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine(string.Format("MouseDown \t\t {0}\n", e.Button));

        }

        private Point GetOnMouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine(string.Format("MouseDown \t\t {0}\n", e.Button));
            Point p = new Point(e.X, e.Y);
            return p;

        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {

            if (!Helper.ApplicationIsFocused())
            {
                click = new Point(e.X, e.Y);
                FlowLayoutPanel panel = new FlowLayoutPanel { AutoSize = true, FlowDirection = FlowDirection.LeftToRight };
                clicks.Add(new ClickPackage(clicks.Count, click.X, click.Y, panel));
                flowLayoutPanel1.Controls.Add(clicks[clicks.Count - 1].panel);
            }

            Console.WriteLine(string.Format("MouseClick \t\t {0}\n", e.Button));
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine(string.Format("MouseDoubleClick \t\t {0}\n", e.Button));
        }

        private void OnMouseDragStarted(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseDragStarted\n");
        }

        private void OnMouseDragFinished(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseDragFinished\n");
        }

        private void HookManager_MouseHWheel(object sender, MouseEventArgs e)
        {
            lbl_message.Text = string.Format("HWheel={0:000}", e.Delta);
        }

        private void HookManager_MouseHWheelExt(object sender, MouseEventExtArgs e)
        {
            lbl_message.Text = string.Format("HWheel={0:000}", e.Delta);
            Console.WriteLine("Horizontal Mouse Wheel Move Suppressed.\n");
            e.Handled = true;
        }

        Boolean recording = false;
        private void Btn_Record_Click(object sender, EventArgs e)
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod());

            recording = !recording;//toggle
            if (recording)
            {
                btn_record.Text = "Recording...";
                SubscribeGlobal();
            }
            else
            {
                btn_record.Text = "Record";
                Unsubscribe();
            }

        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod());
            btn_save.Text = "Saving...";
            Unsubscribe();//Stop recording if the user hasn't already before saving.

            for (int i = clicks.Count - 1; i >= 0; i--)
            {
                if (clicks[i].deleted)
                {
                    clicks.Remove(clicks[i]);
                }
            }
            for (int i = 0; i < clicks.Count; i++)
            {

                clicks[i].fileName = i.ToString("D3") + ".png";
                clicks[i].info = clicks[i].getText();
                string image_path = Path.Combine("data", i.ToString("D3") + ".png");
                clicks[i].bmp.Save(image_path, ImageFormat.Png);
            }
            string json = JsonConvert.SerializeObject(clicks);
            string path = Path.Combine("data", "clicks.json");
            File.WriteAllText(path, json);
            System.Environment.Exit(1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lbl_message_Click(object sender, EventArgs e)
        {

        }



    }




}
