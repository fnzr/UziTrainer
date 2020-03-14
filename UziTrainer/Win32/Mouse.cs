using System;
using System.Threading;

namespace UziTrainer.Win32
{
    public class Mouse
    {
        const int Step = 8;
        Random random = new Random();
        readonly int MessageHWND;
        System.Diagnostics.Process Process;

        public Mouse(int messageHWND)
        {
            MessageHWND = messageHWND;
            Process = new System.Diagnostics.Process();
            var info = new System.Diagnostics.ProcessStartInfo();
            info.UseShellExecute = true;
            info.CreateNoWindow = true;
            info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;            
            info.FileName = @"C:\Users\master\AppData\Local\Android\Sdk\platform-tools\adb.exe";
            Process.StartInfo = info;
        }

        private static int GetLParam(int x, int y)
        {
            //y -= 31; //default_title_barWindow offset
            //x -= 8;
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

        private void RButtonDown(int x, int y)
        {
            Message.PostMessage(MessageHWND, Message.WM_RBUTTONDOWN, 0x0002, GetLParam(x, y));
        }

        private void RButtonUp(int x, int y)
        {
            Message.PostMessage(MessageHWND, Message.WM_RBUTTONUP, 0x0000, GetLParam(x, y));
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

        public void Scroll()
        {
            int x = 400, y = 400;
            var lparam = GetLParam(x, y);
            int wparam = (-120 << 16) | (0x0008 & 0xFFFF);
            for (int _= 0; _ < 20; _++)
            {
                Message.PostMessage(MessageHWND, Message.WM_MOUSEWHEEL, wparam, lparam);
                Thread.Sleep(10);
            }
            
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
                Thread.Sleep(10);
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
                Thread.Sleep(10);
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
                Thread.Sleep(10);
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
                Thread.Sleep(10);
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
                DragRightToLeft(300, 1250, 800);
                Thread.Sleep(300);
            }
            MiddleButtonDown(700, 400);
            Thread.Sleep(10);
            MiddleButtonUp(700, 400);
            Thread.Sleep(10);
        }

        public void Click(int x, int y)
        {
            try
            {
                Process.StartInfo.Arguments = $"shell input tap {x} {y}";                
                Process.Start();
                Process.WaitForExit();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            var sleep = random.Next(300, 800);
            Thread.Sleep(sleep);
        }
    }
}
