using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UziTrainer
{
    class Window
    {
        int windowHWND;
        int messageHWND;

        public Window(
            String screenWindowClass,
            String screenWindowTitle,            
            String messageWindowClass,
            String messageWindowTitle)
        {
            windowHWND = Win32.FindWindow(screenWindowClass, screenWindowTitle);
            if (windowHWND <= 0) {
                throw new ArgumentException(String.Format("No HWND for window [{0}]", screenWindowTitle));
            }
            messageHWND = Win32.FindWindowEx(windowHWND, 0, messageWindowClass, messageWindowTitle);
        }
    }
}
