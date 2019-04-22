using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNest.MultiKeyboards
{
    /// <summary>
    /// Represents information of a keyboard device.
    /// </summary>
    [Serializable]
    public class KeyboardDeviceInfo
    {
        /// <summary>
        /// Gets the handle of the keyboard device.
        /// </summary>
        public IntPtr Handle { get; }
        /// <summary>
        /// Gets the device name of the keyboard device.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Gets the type of the keyboard device.
        /// </summary>
        public KeyboardDeviceType Type { get; }
        /// <summary>
        /// Gets the description of the keyboard device from registry.
        /// </summary>
        public string Description { get; }


        internal KeyboardDeviceInfo(IntPtr handle, string name, KeyboardDeviceType type, string description)
        {
            Handle = handle;
            Name = name;
            Type = type;
            Description = description;
        }

        public override string ToString()
        {
            return string.Format("Name: {0}\nType: {1}\nHandle: {2}\nDescription: {3}", Name, Type, Handle.ToInt64().ToString("X"), Description);
        }
    }

    public enum KeyboardDeviceType
    {
        Keyboard,
        Hid,
        Other
    }
}
