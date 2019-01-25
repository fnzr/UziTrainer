using System;
using System.Drawing;

namespace UziTrainer
{
    class Mouse
    {
        private static Random random = new Random();

        private static int GetLParam(int x, int y)
        {
            return (x & 0xFFFF) | (y & 0xFFFF) << 16;
        }

        private static void LButtonDown(int x, int y)
        {            
            Win32.PostMessage(Window.MessageHWND, Win32.WM_LBUTTONDOWN, 0x0001, GetLParam(x, y));
        }

        private static void LButtonUp(int x, int y)
        {
            Win32.PostMessage(Window.MessageHWND, Win32.WM_LBUTTONUP, 0x0000, GetLParam(x, y));
        }

        private static void _Click(int x, int y)
        {
            LButtonDown(x, y);
            LButtonUp(x, y);
        }

        public static void Click(int x, int y)
        {
            var rx = random.Next(x - variance, x + variance);
            var ry = random.Next(y - variance, y + variance);
            Click(rx, ry);
        }

        public static void Click(int x, int y, int variance)
        {
            var rx = random.Next(x - variance, x + variance);
            var ry = random.Next(y - variance, y + variance);
            Click(rx, ry);
        }
    }
}
