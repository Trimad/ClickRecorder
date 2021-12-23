using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClickRecorder
{
    static class Helper
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        public static Boolean ApplicationIsFocused()
        {
            var activatedHandle = GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero)
            {
                return false;       // No window is currently activated
            }
            else
            {
                Console.WriteLine("Application is focused!");
            }

            var procId = Process.GetCurrentProcess().Id;
            int activeProcId;
            GetWindowThreadProcessId(activatedHandle, out activeProcId);

            return activeProcId == procId;

        }
        //public static FileStream NewFile(this string folderpath, string filename, string extension)
        //{



        //return File.Create($"{folderpath}{filename}{extension}");
        //}

        //public static SaveImage(String s)
        //{
        //    Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod());
        //    GCHandle bitsHandle = GCHandle.Alloc(canvas, GCHandleType.Pinned);
        //    Bitmap image = new Bitmap(width, height, width * 4, PixelFormat.Format32bppArgb, bitsHandle.AddrOfPinnedObject());
        //    string image_path = Path.Combine(savePath, name + ".png");
        //    Console.WriteLine(image_path);
        //    image.Save(image_path, ImageFormat.Png);
        //    return this;
        //}

    }


}
