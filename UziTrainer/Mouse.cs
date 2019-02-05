using System;
using System.Drawing;
using System.Threading;

namespace UziTrainer
{
    class Mouse
    {
        private const int DEFAULT_VARIANCE = 10;
        private const int Step = 3;

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

        private static void MiddleButtonDown(int x, int y)
        {
            var lparam = GetLParam(x, y);
            Message.PostMessage(Window.MessageHWND, Message.WM_MBUTTONDOWN, 0x0010, lparam);
        }

        private static void MiddleButtonUp(int x, int y)
        {
            var lparam = GetLParam(x, y);
            Message.PostMessage(Window.MessageHWND, Message.WM_MBUTTONUP, 0x0010, lparam);
        }

        private static void MouseMove(int x, int y)
        {
            var lparam = GetLParam(x, y);
            Message.PostMessage(Window.MessageHWND, Message.WM_MOUSEMOVE, 0x0001, lparam);
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

        public static void Click(Point p)
        {
            Click(p.X, p.Y);
        }

        public static void DragUpToDown(int x, int y_start, int y_end)
        {
            x = random.Next(x - 5, x + 5);
            y_start = random.Next(y_start - 5, y_start + 5);
            y_end = random.Next(y_end - 5, y_end + 5);
            LButtonDown(x, y_start);
            var y = y_start;
            while (y >= y_end)
            {
                y = y + Step;
                MouseMove(x, y);
                Thread.Sleep(1);
            }
            LButtonUp(x, y_end);
            Thread.Sleep(300);
        }

        //Finger on bottom of the screen, and drag up
        public static void DragDownToUp(int x, int y_start, int y_end)
        {
            x = random.Next(x - 5, x + 5);
            y_start = random.Next(y_start - 5, y_start + 5);
            y_end = random.Next(y_end - 5, y_end + 5);
            LButtonDown(x, y_start);
            var y = y_start;
            while (y <= y_end)
            {
                y = y - Step;
                MouseMove(x, y);
                Thread.Sleep(1);
            }
            LButtonUp(x, y_end);
            Thread.Sleep(300);
        }

        //Finger on right of the screen, and drag left
        public static void DragRightToLeft(int y, int x_start, int x_end)
        {
            y = random.Next(y - 5, y + 5);
            x_start = random.Next(x_start - 5, x_start + 5);
            x_end = random.Next(x_end - 5, x_end + 5);
            LButtonDown(x_start, y);
            var x = x_start;
            while (x <= x_end)
            {
                x = x - Step;
                MouseMove(x, y);
                Thread.Sleep(1);
            }
            LButtonUp(x_end, y);
            Thread.Sleep(300);
        }

        //Finger on left of the screen, and drag right
        public static void DragLeftToRight(int y, int x_start, int x_end)
        {
            y = random.Next(y - 5, y + 5);
            x_start = random.Next(x_start - 5, x_start + 5);
            x_end = random.Next(x_end - 5, x_end + 5);
            LButtonDown(x_start, y);
            var x = x_start;
            while (x >= x_end)
            {
                x = x + Step;
                MouseMove(x, y);
                Thread.Sleep(1);
            }
            LButtonUp(x_end, y);
            Thread.Sleep(300);
        }

        public static void ZoomOut(int repeatCount = 3)
        {
            MiddleButtonDown(700, 400);
            MiddleButtonUp(700, 400);
            for (var i = 0; i < repeatCount; i++)
            {
                //Nox is wonky if you try to zoom too much too fast
                DragRightToLeft(300, 1250, 850);
                Thread.Sleep(100);
            }
            MiddleButtonDown(700, 400);
            MiddleButtonUp(700, 400);
            Thread.Sleep(10);
        }
    }
}
