using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UziTrainer.Win32
{
    public class Mouse
    {
        const int Step = 8;
        Random random = new Random();        
        readonly int WindowHWND;
        readonly int MessageHWND;

        public Mouse(int windowHWND, int messageHWND)
        {
            WindowHWND = windowHWND;
            MessageHWND = messageHWND;                   
        }

        private static int GetLParam(int x, int y)
        {
            //return (x & 0xFFFF) | ((y & 0xFFFF) << 16);
            return (y << 16) | (x & 0xFFFF);            
        }

        private void LButtonDown(int x, int y)
        {
            Message.PostMessage(MessageHWND, Message.WM_LBUTTONDOWN, 0x0001, GetLParam(x, y));
        }

        private void LButtonUp(int x, int y)
        {
            Message.PostMessage(MessageHWND, Message.WM_LBUTTONUP, 0x0000, GetLParam(x, y));
        }

        private void MiddleButtonDown(int x, int y)
        {
            var lparam = GetLParam(x, y);
            Message.PostMessage(MessageHWND, Message.WM_MBUTTONDOWN, 0x0010, lparam);
        }

        private void MiddleButtonUp(int x, int y)
        {
            var lparam = GetLParam(x, y);
            Message.PostMessage(MessageHWND, Message.WM_MBUTTONUP, 0x0000, lparam);
        }

        private void MouseMove(int x, int y)
        {
            var lparam = GetLParam(x, y);
            Message.PostMessage(MessageHWND, Message.WM_MOUSEMOVE, 0x0001, lparam);
        }

        public void DragUpToDown(int x, int y_start, int y_end)
        {
            x = random.Next(x - 5, x + 5);
            y_start = random.Next(y_start - 5, y_start + 5);
            y_end = random.Next(y_end - 5, y_end + 5);
            LButtonDown(x, y_start);
            var y = y_start;
            while (y < y_end)
            {
                y = y + Step;
                MouseMove(x, y);
                Thread.Sleep(5);
            }
            LButtonUp(x, y_end);
            Thread.Sleep(300);
        }

        //Finger on bottom of the screen, and drag up
        public void DragDownToUp(int x, int y_start, int y_end)
        {
            x = random.Next(x - 5, x + 5);
            y_start = random.Next(y_start - 5, y_start + 5);
            y_end = random.Next(y_end - 5, y_end + 5);
            LButtonDown(x, y_start);
            var y = y_start;
            while (y > y_end)
            {
                y = y - Step;
                MouseMove(x, y);
                Thread.Sleep(5);
            }
            LButtonUp(x, y_end);
            Thread.Sleep(300);
        }

        //Finger on right of the screen, and drag left
        public void DragRightToLeft(int y, int x_start, int x_end)
        {
            y = random.Next(y - 5, y + 5);
            x_start = random.Next(x_start - 5, x_start + 5);
            x_end = random.Next(x_end - 5, x_end + 5);
            LButtonDown(x_start, y);
            var x = x_start;
            while (x > x_end)
            {
                x = x - Step;
                MouseMove(x, y);
                Thread.Sleep(5);
            }
            LButtonUp(x_end, y);
            Thread.Sleep(300);
        }

        //Finger on left of the screen, and drag right
        public void DragLeftToRight(int y, int x_start, int x_end)
        {
            y = random.Next(y - 5, y + 5);
            x_start = random.Next(x_start - 5, x_start + 5);
            x_end = random.Next(x_end - 5, x_end + 5);
            LButtonDown(x_start, y);
            var x = x_start;
            while (x < x_end)
            {
                x = x + Step;
                MouseMove(x, y);
                Thread.Sleep(5);
            }
            LButtonUp(x_end, y);
            Thread.Sleep(300);
        }

        public void ZoomOut(int repeatCount = 2)
        {
            MiddleButtonDown(700, 400);
            Thread.Sleep(10);
            MiddleButtonUp(700, 400);
            for (var i = 0; i < repeatCount; i++)
            {
                DragRightToLeft(300, 1250, 850);
                Thread.Sleep(50);
            }
            MiddleButtonDown(700, 400);
            Thread.Sleep(10);
            MiddleButtonUp(700, 400);
            Thread.Sleep(10);
        }

        public void Click(int x, int y)
        {
            LButtonDown(x, y);
            Thread.Sleep(10);
            LButtonUp(x, y);
        }
    }
}
