using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace UziTrainer.Win32
{
	/// <summary>
	/// Summary description for Win32.
	/// </summary>
	public class Message
	{
		// The WM_COMMAND message is sent when the user selects a command item from a menu, 
		// when a control sends a notification message to its parent window, or when an 
		// accelerator keystroke is translated.
        public const int WM_COMMAND = 0x111;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_MOUSEMOVE = 0x200;
        public const int WM_MBUTTONDOWN = 0x207;
        public const int WM_MBUTTONUP = 0x208;
        public const int WM_LBUTTONDBLCLK = 0x203;
        public const int WM_RBUTTONDOWN = 0x204;
        public const int WM_RBUTTONUP = 0x205;
        public const int WM_RBUTTONDBLCLK = 0x206;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const uint SW_RESTORE = 0x09;
        public const int WM_MOUSEWHEEL = 0x020A;        

        public const uint GW_HWNDPREV = 3;

        public delegate bool EnumChildWindowsCallBack(int hwnd, int lParam);

        public static bool Callback(int hwnd, int lParam)
        {
            Trace.WriteLine($"Window handle: {hwnd}");
            return true;
        }

        // The FindWindow function retrieves a handle to the top-level window whose class name
        // and window name match the specified strings. This function does not search child windows.
        // This function does not perform a case-sensitive search.
        [DllImport("User32.dll")]
		public static extern int FindWindow(string strClassName, string strWindowName);

		// The FindWindowEx function retrieves a handle to a window whose class name 
		// and window name match the specified strings. The function searches child windows, beginning
		// with the one following the specified child window. This function does not perform a case-sensitive search.
		[DllImport("User32.dll")]
		public static extern int FindWindowEx(int hwndParent, int hwndChildAfter, string strClassName, string strWindowName);

        [DllImport("User32.dll")]
        public static extern int GetWindow(int hwnd, uint relationship);

        [DllImport("User32.dll")]
        public static extern bool EnumChildWindows(int hWndParent, EnumChildWindowsCallBack lpEnumFunc, int lParam);

        // The SendMessage function sends the specified message to a 
        // window or windows. It calls the window procedure for the specified 
        // window and does not return until the window procedure has processed the message. 
        [DllImport("User32.dll")]
		public static extern Int32 PostMessage(
			int hWnd,               // handle to destination window
			int Msg,                // message
			int wParam,             // first message parameter
			[MarshalAs(UnmanagedType.LPStr)] string lParam); // second message parameter

		[DllImport("User32.dll")]
		public static extern Int32 PostMessage(
			int hWnd,               // handle to destination window
			int Msg,                // message
			int wParam,             // first message parameter
			int lParam);			// second message parameter

        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(
            int hWnd,               // handle to destination window
            int Msg,                // message
            int wParam,             // first message parameter
            int lParam);            // second message parameter

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern int ShowWindow(IntPtr hWnd, uint Msg);

        [DllImport("user32.dll")]
        public static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

    }
}
