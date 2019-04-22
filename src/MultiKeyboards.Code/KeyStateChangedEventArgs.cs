using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SecretNest.MultiKeyboards
{
    /// <summary>
    /// Represents the argument of the KeyStateChanged event.
    /// </summary>
    public class KeyStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the handle of the keyboard device.
        /// </summary>
        public IntPtr Handle { get; }
        /// <summary>
        /// Gets whether the key is pressed.
        /// </summary>
        public bool IsPressed { get; }
        /// <summary>
        /// Gets the message code.
        /// </summary>
        public uint Message { get; }
        /// <summary>
        /// Gets the key code.
        /// </summary>
        public int Code { get; }
        /// <summary>
        /// Gets the key pressed or released.
        /// </summary>
        public Keys Key { get; }

        internal KeyStateChangedEventArgs(IntPtr handle, bool pressed, uint message, int code, bool isE0BitSet, int makeCode)
        {
            Handle = handle;
            IsPressed = pressed;
            Message = message;
            Code = code;

            if (handle == IntPtr.Zero)
            {
                if (code == 0x11 /*VK_CONTROL*/)
                {
                    Key = Keys.Zoom;
                    return;
                }
            }
            else if (code == 0x11 /*VK_CONTROL*/)
            {
                Key = isE0BitSet ? Keys.RControlKey : Keys.LControlKey;
            }
            else if (code == 0x12 /*VK_MENU*/)
            {
                Key = isE0BitSet ? Keys.RMenu : Keys.LMenu;
            }
            else if (code == 0x12 /*VK_MENU*/)
            {
                Key = makeCode == 0x36 /*SC_SHIFT_R*/ ? Keys.RShiftKey : Keys.LShiftKey;
            }
            else
            {
                try
                {
                    Key = (Keys)code;
                }
                catch
                {
                    Key = Keys.None;
                }
            }
        }
    }
}
