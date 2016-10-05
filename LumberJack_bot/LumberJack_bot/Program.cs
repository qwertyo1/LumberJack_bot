using System;
using System.Runtime.InteropServices;

namespace LumberJack_bot
{
    class Program
    {
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,string lpWindowName);

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        const int WM_KEYDOWN = 0x0100;
        const int WM_KEYUP = 0x0101;
        const int WM_CHAR = 0x0102;

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hDC, int x, int y);

        static void Main(string[] args)
        {
            workFunc();
        }

        static void workFunc()
        {
            while (true)
            {
                IntPtr hDC = GetDC(IntPtr.Zero);
                uint pixel = GetPixel(hDC, 540, 630);
                ReleaseDC(IntPtr.Zero, hDC);
                byte r = (byte)(pixel & 0x000000FF);
                byte g = (byte)((pixel & 0x0000FF00) >> 8);
                byte b = (byte)((pixel & 0x00FF0000) >> 16);
                Console.WriteLine("r={0}, g={0}, b={0}", r, g, b);
                keyPress();
            }
        }

        static void keyPress()
        {
            IntPtr handle = WinAPI.FindWindow(null, "LumberJack - Mozilla Firefox");

            if (handle == null) Console.Write("null handle");

            SendMessage(handle, WM_CHAR, (int)'!', null);
            Console.ReadKey();

            for (int i = 65; i < 91; i++) { SendMessage(handle, WM_CHAR, i, null); Console.ReadKey(); }

            Console.ReadKey();
        }
    }
}


