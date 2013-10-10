using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPen;
using System.Runtime.InteropServices;

namespace ConsoleApplication1
{
    class Program
    {

        [DllImport("kernel32.dll")]
        public static extern bool SetConsoleTextAttribute(IntPtr hConsoleOutput,int wAttributes);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetStdHandle(uint nStdHandle);
        static SmartPen.Pen _pen;
        static IntPtr hConsole;
        static void Main(string[] args)
        {
            uint STD_OUTPUT_HANDLE = 0xfffffff5;
            hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
            
            _pen = new SmartPen.Pen();
            _pen.EraserSelected += _pen_EraserSelected;
            _pen.PenSelected += _pen_PenSelected;
            _pen.FingerSelected += _pen_FingerSelected;
            while (true)
            { }
        }

        static void _pen_FingerSelected(object sender, Pen.PenEventArgs args)
        {
            SetConsoleTextAttribute(hConsole, 15);
            Console.WriteLine("Finger Selected.");
        }

        static void _pen_PenSelected(object sender, Pen.PenEventArgs args)
        {
            System.Drawing.Color col = args.Color;
            if (col.R == 255 && col.G == 0 && col.B == 0)
            {
                //Red
                SetConsoleTextAttribute(hConsole, 12);
            } else if (col.R == 0 && col.G == 147 && col.B == 0)
            {
                //Green
                SetConsoleTextAttribute(hConsole, 10);
            }
            else if (col.R == 0 && col.G == 0 && col.B == 255)
            {
                //Blue
                SetConsoleTextAttribute(hConsole, 249);
            }
            else
            {
                SetConsoleTextAttribute(hConsole, 15);
            }
            Console.WriteLine("Pen Selected.");
        }

        static void _pen_EraserSelected(object sender, Pen.PenEventArgs args)
        {
            SetConsoleTextAttribute(hConsole, 15);
            Console.WriteLine("Eraser Selected.");
        }

    }
}
