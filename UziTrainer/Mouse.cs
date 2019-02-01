using System;
using System.Drawing;

namespace UziTrainer
{
    class Mouse
    {
        private const int DEFAULT_VARIANCE = 10;
        private static Random random = new Random();

        private static int GetLParam(int x, int y)
        {
            return (x & 0xFFFF) | (y & 0xFFFF) << 16;
        }

        private static void LButtonDown(int x, int y)
        {            
            Message.PostMessage(Window.MessageHWND, Message.WM_LBUTTONDOWN, 0x0001, GetLParam(x, y));
        }

        private static void LButtonUp(int x, int y)
        {
            Message.PostMessage(Window.MessageHWND, Message.WM_LBUTTONUP, 0x0000, GetLParam(x, y));
        }

        private static void _Click(int x, int y)
        {
            LButtonDown(x, y);
            LButtonUp(x, y);
        }

        public static void Click(int x, int y, int varianceX, int varianceY)
        {
            var rx = random.Next(x - varianceX, x + varianceX);
            var ry = random.Next(y - varianceY, y + varianceY);
            _Click(rx, ry);
        }

        public static void Click(int x, int y, int variance)
        {
            Click(x, y, variance, variance);
        }

        public static void Click(int x, int y)
        {
            Click(x, y, Mouse.DEFAULT_VARIANCE);
        }
    }
}
