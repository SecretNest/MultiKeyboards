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
        public IntPtr Handle => KeyInfo.Handle;

        /// <summary>
        /// Gets whether the key is pressed.
        /// </summary>
        public bool IsPressed => KeyInfo.IsPressed;

        /// <summary>
        /// Gets the scan code from the key depression.
        /// </summary>
        public ushort MakeCode => KeyInfo.MakeCode;

        /// <summary>
        /// Gets the flags, combining one or more of RI_KEY_MAKE, RI_KEY_BREAK, RI_KEY_E0, RI_KEY_E1
        /// </summary>
        public ushort Flags => KeyInfo.Flags;

        /// <summary>
        /// Gets the corresponding Windows message for example (WM_KEYDOWN, WM_SYASKEYDOWN etc).
        /// </summary>
        public uint Message => KeyInfo.Message;

        /// <summary>
        /// Gets the virtual Key Code.
        /// </summary>
        public ushort VKey => KeyInfo.VKey;

        /// <summary>
        /// Gets the key pressed or released.
        /// </summary>
        public Keys Key { get; }

        /// <summary>
        /// Gets the key message.
        /// </summary>
        public KeyInfo KeyInfo { get; } 

        internal KeyStateChangedEventArgs(KeyInfo keyInfo)
        {
            KeyInfo = keyInfo;
        }
    }
}
