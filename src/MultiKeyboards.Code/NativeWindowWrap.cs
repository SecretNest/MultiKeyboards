using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SecretNest.MultiKeyboards
{
    class NativeWindowWrap : NativeWindow, IDisposable
    {
        Action<IntPtr> processKeyCallback;
        public NativeWindowWrap(IntPtr handle, Action<IntPtr> processKeyCallback)
        {
            this.processKeyCallback = processKeyCallback;
            AssignHandle(handle);
        }

        public void Dispose()
        {
            ReleaseHandle();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x00FF) //WM_INPUT
            {
                processKeyCallback(m.LParam);
            }
            base.WndProc(ref m);
        }
    }
}
