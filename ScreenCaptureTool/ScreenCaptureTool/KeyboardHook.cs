using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenCaptureTool
{
    public class KeyboardHook
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        public event EventHandler<KeyControlEventArgs> KeyPressed;

        public KeyboardHook()
        {
            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }

        public void Unhook()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private bool Control;
        private bool LShiftKey;

        private bool isKeyDownProcessing = false;
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_KEYUP))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;
                KeyControlEventArgs keyEventArgs = new KeyControlEventArgs(key);
                if (wParam == (IntPtr)WM_KEYDOWN)
                {
                    if (key == Keys.LControlKey || key == Keys.RControlKey || key == Keys.Control) Control = true;
                    if (key == Keys.LShiftKey) LShiftKey = true;
                }
                else if (wParam == (IntPtr)WM_KEYUP)
                {
                    if (key == Keys.LControlKey || key == Keys.RControlKey || key == Keys.Control) Control = false;
                    if (key == Keys.LShiftKey) LShiftKey = false;
                }

                if (!isKeyDownProcessing)
                {
                    isKeyDownProcessing = true;
                    keyEventArgs.Control = this.Control;
                    keyEventArgs.LShiftKey = this.LShiftKey;
                    KeyPressed?.Invoke(this, keyEventArgs);
                    isKeyDownProcessing = false;
                }

                if (keyEventArgs.Handled)
                {
                    return (IntPtr)1; // 阻止传递事件给其他处理程序
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        #region Win32 API

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion
    }

    public class KeyControlEventArgs : EventArgs
    {
        public bool Control { get; set; }
        public bool LShiftKey { get; set; }
        public Keys KeyCode { get; private set; }
        public bool Handled { get; set; }

        public KeyControlEventArgs(Keys keyCode)
        {
            KeyCode = keyCode;
            Handled = false;
        }
    }
}
