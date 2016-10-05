using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LumberJack_bot_forms
{
    public partial class Form1 : Form
    {

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

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

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            workFunc();
        }

        async static void workFunc()
        {
            while (true)
            {
                if ((checkPixel(540, 610) == 0) && (checkPixel(540, 620) == 0) && (checkPixel(540, 630) == 0) && (checkPixel(540, 640) == 0) && (checkPixel(540, 650) == 0) && (checkPixel(540, 660) == 0) && (checkPixel(540, 670) == 0) && (checkPixel(540, 680) == 0))  { SendKeys.Send("{RIGHT}"); } else { SendKeys.Send("{LEFT}"); }
                await Task.Delay(150);
            }
        }

        static int checkPixel(int x, int y)
        {
            IntPtr hDC = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hDC, x, y);
            ReleaseDC(IntPtr.Zero, hDC);
            byte r = (byte)(pixel & 0x000000FF);
            byte g = (byte)((pixel & 0x0000FF00) >> 8);
            byte b = (byte)((pixel & 0x00FF0000) >> 16);
            if (r == 161 && g == 116 && b == 56) { return 1; }
            return 0;
        }

        /*
        static void keyPress(string txt)
        {
            IntPtr handle = FindWindow(null, "LumberJack - Mozilla Firefox");

            if (handle == null) Console.Write("null handle");
            SendMessage(handle, WM_CHAR, (int)'!', null);
            //Console.ReadKey();
            
            for (int i = 65; i < 91; i++) { SendMessage(handle, WM_CHAR, i, null); //Console.ReadKey(); 
            }

            //Console.ReadKey();
        }
        */
    }
}
