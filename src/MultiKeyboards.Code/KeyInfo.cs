using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SecretNest.MultiKeyboards
{

    /// <summary>
    /// Represents an key message.
    /// </summary>
    [Serializable]
    public class KeyInfo
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
        /// Gets the scan code from the key depression.
        /// </summary>
        public ushort MakeCode { get; }

        /// <summary>
        /// Gets the flags, combining one or more of RI_KEY_MAKE, RI_KEY_BREAK, RI_KEY_E0, RI_KEY_E1
        /// </summary>
        public ushort Flags { get; }

        /// <summary>
        /// Gets the corresponding Windows message for example (WM_KEYDOWN, WM_SYASKEYDOWN etc).
        /// </summary>
        public uint Message { get; }

        /// <summary>
        /// Gets the virtual Key Code.
        /// </summary>
        public ushort VKey { get; }

        /// <summary>
        /// Gets the key pressed or released.
        /// </summary>
        public Keys Key { get; }

        /// <summary>
        /// Initializes an instance of KeyInfo.
        /// </summary>
        /// <param name="handle">Handle of the keyboard device</param>
        /// <param name="makeCode">Scan code from the key depression</param>
        /// <param name="flags">One or more of RI_KEY_MAKE, RI_KEY_BREAK, RI_KEY_E0, RI_KEY_E1</param>
        /// <param name="vkey">Virtual key code</param>
        /// <param name="message">Corresponding Windows message for example (WM_KEYDOWN, WM_SYASKEYDOWN etc)</param>
        public KeyInfo(IntPtr handle, ushort makeCode, ushort flags, ushort vkey, uint message)
        {
            Handle = handle;
            IsPressed = (flags & 0x01 /*RI_KEY_BREAK*/) == 0;
            MakeCode = makeCode;
            Flags = flags;
            VKey = vkey;
            Message = message;

            var isE0BitSet = ((flags & 0x02 /*RI_KEY_E0*/) != 0);
            if (handle == IntPtr.Zero)
            {
                if (vkey == 0x11 /*VK_CONTROL*/)
                {
                    Key = Keys.Zoom;
                    return;
                }
            }
            else if (vkey == 0x11 /*VK_CONTROL*/)
            {
                Key = isE0BitSet ? Keys.RControlKey : Keys.LControlKey;
            }
            else if (vkey == 0x12 /*VK_MENU*/)
            {
                Key = isE0BitSet ? Keys.RMenu : Keys.LMenu;
            }
            else if (vkey == 0x10 /*VK_SHIFT*/)
            {
                Key = makeCode == 0x36 /*SC_SHIFT_R*/ ? Keys.RShiftKey : Keys.LShiftKey;
            }
            else
            {
                try
                {
                    Key = (Keys)vkey;
                }
                catch
                {
                    Key = Keys.None;
                }
            }
        }
    }
}
