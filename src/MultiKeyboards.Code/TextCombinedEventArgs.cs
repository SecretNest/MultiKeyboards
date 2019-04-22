using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNest.MultiKeyboards
{
    /// <summary>
    /// Represents the argument of the TextCombined event.
    /// </summary>
    public class TextCombinedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the handle of the source keyboard device.
        /// </summary>
        public IntPtr Handle { get; }
        /// <summary>
        /// Gets the combined text.
        /// </summary>
        public string Text { get; }
        internal TextCombinedEventArgs(IntPtr handle, string text)
        {
            Handle = handle;
            Text = text;
        }
    }
}
