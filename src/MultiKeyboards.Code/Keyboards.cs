using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SecretNest.MultiKeyboards
{
    /// <summary>
    /// Provides methods to get all keyboard devices and start catching keyboard input messages.
    /// </summary>
    public class Keyboards : IDisposable
    {
        #region Get Device List

        [DllImport("User32.dll", SetLastError = true)]
        static extern uint GetRawInputDeviceList(IntPtr pRawInputDeviceList, ref uint numberDevices, uint size);
        [DllImport("User32.dll", SetLastError = true)]
        static extern uint GetRawInputDeviceInfo(IntPtr hDevice, uint command, IntPtr pData, ref uint size);

        [StructLayout(LayoutKind.Sequential)]
        struct InputDevice
        {
            public IntPtr Handle;
            public uint Type;
        }

        readonly static object singleThreadLock = new object();
        readonly static int inputDeviceElementSize = (Marshal.SizeOf(typeof(InputDevice)));

        static string GetDeviceDescription(string deviceName)
        {
            var subStrings = deviceName.Substring(4).Split('#');

            try
            {
                using (var regKey = Registry.LocalMachine.OpenSubKey(string.Format(@"System\CurrentControlSet\Enum\{0}\{1}\{2}", subStrings[0], subStrings[1], subStrings[2])))
                {
                    var value = regKey.GetValue("DeviceDesc").ToString();
                    value = value.Substring(value.IndexOf(';') + 1);
                    return value;
                }
            }
            catch
            {
                return "<Unable to get description.>";
            }
        }

        /// <summary>
        /// Gets the list of all keyboard devices.
        /// </summary>
        /// <returns>List of all keyboard devices</returns>
        public static IEnumerable<KeyboardDeviceInfo> Get()
        {
            lock (singleThreadLock)
            {
                uint deviceCount = 0;
                if (GetRawInputDeviceList(IntPtr.Zero, ref deviceCount, (uint)inputDeviceElementSize) == 0)
                {
                    var pRawInputDeviceList = Marshal.AllocHGlobal((int)(inputDeviceElementSize * deviceCount));
                    GetRawInputDeviceList(pRawInputDeviceList, ref deviceCount, (uint)inputDeviceElementSize);

                    for (var i = 0; i < deviceCount; i++)
                    {
                        uint pcbSize = 0;

                        // On Window 8 64bit when compiling against .Net > 3.5 using .ToInt32 you will generate an arithmetic overflow. Leave as it is for 32bit/64bit applications
                        var inputDevice = (InputDevice)Marshal.PtrToStructure(new IntPtr((pRawInputDeviceList.ToInt64() + (inputDeviceElementSize * i))), typeof(InputDevice));

                        GetRawInputDeviceInfo(inputDevice.Handle, 0x20000007 /*RIDI_DEVICENAME*/, IntPtr.Zero, ref pcbSize);

                        if (pcbSize <= 0) continue;

                        IntPtr pData = IntPtr.Zero;
                        KeyboardDeviceType deviceType = KeyboardDeviceType.Other;
                        if (inputDevice.Type == 1)
                            deviceType = KeyboardDeviceType.Keyboard;
                        else if (inputDevice.Type == 2)
                            deviceType = KeyboardDeviceType.Hid;

                        try
                        {
                            pData = Marshal.AllocHGlobal((int)pcbSize);
                            GetRawInputDeviceInfo(inputDevice.Handle, 0x20000007 /*RIDI_DEVICENAME*/, pData, ref pcbSize);
                            var deviceName = Marshal.PtrToStringAnsi(pData);

                            if (deviceType != KeyboardDeviceType.Other)
                            {
                                var name = Marshal.PtrToStringAnsi(pData);
                                var description = GetDeviceDescription(deviceName);

                                var device = new KeyboardDeviceInfo(inputDevice.Handle, name, deviceType, description);

                                yield return device;
                            }
                        }
                        finally
                        {
                            if (pData != IntPtr.Zero)
                                Marshal.FreeHGlobal(pData);
                        }
                    }
                }
            }
        }
        #endregion

        #region Start Catching
        [DllImport("User32.dll", SetLastError = true)]
        static extern bool RegisterRawInputDevices(RawInputDevice[] pRawInputDevice, uint numberDevices, uint size);

        readonly static uint rawInputDeviceElementSize = (uint)Marshal.SizeOf(typeof(RawInputDevice));

        [StructLayout(LayoutKind.Sequential)]
        struct RawInputDevice
        {
            ushort UsagePage;
            ushort Usage;
            int Flags;
            internal IntPtr Target;

            public RawInputDevice(IntPtr handle, bool captureWhileActiveOnly)
            {
                UsagePage = 0x01; //GENERIC
                Usage = 0x06; //Keyboard
                if (captureWhileActiveOnly)
                {
                    Flags = 0x00002000; //DEVNOTIFY
                }
                else
                {
                    Flags = 0x00002100; //DEVNOTIFYO | INPUTSINK
                }
                Target = handle;
            }
        }

        /// <summary>
        /// Starts catching input messages from keyboards.
        /// </summary>
        /// <param name="handle">The handle of the receiver (form) which will process the caught messages.</param>
        /// <param name="captureWhileActiveOnly">Whether the catching should be taken while the receiver window is active in foreground only.</param>
        public static void StartCatching(IntPtr handle, bool captureWhileActiveOnly)
        {
            var devices = new RawInputDevice[] { new RawInputDevice(handle, captureWhileActiveOnly) };
            if (!RegisterRawInputDevices(devices, 1, rawInputDeviceElementSize))
            {
                throw new ApplicationException("Failed to register raw input device.");
            }
        }
        #endregion

        #region Process Key Message
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, IntPtr notificationFilter, int flags /*DeviceNotification*/);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool UnregisterDeviceNotification(IntPtr handle);
        [DllImport("User32.dll", SetLastError = true)]
        static extern int GetRawInputData(IntPtr hRawInput, uint command /*DataCommand*/, [Out] IntPtr pData, [In, Out] ref int size, int sizeHeader);
        [DllImport("User32.dll", SetLastError = true)]
        static extern int GetRawInputData(IntPtr hRawInput, uint command /*DataCommand*/, [Out] out InputData buffer, [In, Out] ref int size, int cbSizeHeader);

        /// <summary>
        /// Occurs while a key state is changed.
        /// </summary>
        public event EventHandler<KeyStateChangedEventArgs> KeyStateChanged;

        #region Message Filter
        class MessageFilter : IMessageFilter
        {
            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg == 0x0100) //WM_KEYDOWN
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        MessageFilter messageFilter;
        #endregion

        static readonly Guid multiKeyboardDeviceId = new Guid("63820540-93C6-4F0D-A92C-E95820A085DE"); //Unique Id for this application. Can be changed.
        IntPtr deviceNotificationHandle;
        static readonly int sizeOfBroadcastDeviceInterface = Marshal.SizeOf(typeof(BroadcastDeviceInterface));

        struct BroadcastDeviceInterface
        {
            public int DbccSize;
            public int BroadcastDeviceType;
            public int DbccReserved;
            public Guid DbccClassGuid;
            public char DbccName;

            public BroadcastDeviceInterface(Guid deviceId)
            {
                DbccSize = sizeOfBroadcastDeviceInterface;
                BroadcastDeviceType = 5; //DBT_DEVTYP_DEVICEINTERFACE
                DbccClassGuid = deviceId;
                DbccReserved = default;
                DbccName = default;
            }
        }

        NativeWindowWrap nativeWindowWrap;

        /// <summary>
        /// Initializes an instance of Keyboards.
        /// </summary>
        /// <param name="handle">The handle of the receiver (form) which will process the caught messages.</param>
        public Keyboards(IntPtr handle)
        {
            messageFilter = new MessageFilter();
            Application.AddMessageFilter(messageFilter);
            nativeWindowWrap = new NativeWindowWrap(handle, ProcessKey);

            var broadcastDeviceInterface = new BroadcastDeviceInterface(multiKeyboardDeviceId);

            var mem = IntPtr.Zero;
            try
            {
                mem = Marshal.AllocHGlobal(sizeOfBroadcastDeviceInterface);
                Marshal.StructureToPtr(broadcastDeviceInterface, mem, false);
                deviceNotificationHandle = RegisterDeviceNotification(handle, mem, 0 /*DEVICE_NOTIFY_WINDOW_HANDLE*/);
            }
            finally
            {
                Marshal.FreeHGlobal(mem);
            }

            if (deviceNotificationHandle == IntPtr.Zero)
            {
                throw new InvalidOperationException(string.Format("Registration for device notifications Failed. Error: {0}", Marshal.GetLastWin32Error()));
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        struct InputData
        {
            public RawInputHeader Header;           // 64 bit header size: 24  32 bit the header size: 16
            public RawKeyboard Data;                    // Creating the rest in a struct allows the header size to align correctly for 32/64 bit
        }

        static readonly int sizeOfRawInputHeader = Marshal.SizeOf(typeof(RawInputHeader));
        [StructLayout(LayoutKind.Sequential)]
        struct RawInputHeader
        {
            public uint dwType;                     // Type of raw input (RIM_TYPEHID 2, RIM_TYPEKEYBOARD 1, RIM_TYPEMOUSE 0)
            public uint dwSize;                     // Size in bytes of the entire input packet of data. This includes RAWINPUT plus possible extra input reports in the RAWHID variable length array. 
            public IntPtr hDevice;                  // A handle to the device generating the raw input data. 
            public IntPtr wParam;                   // RIM_INPUT 0 if input occurred while application was in the foreground else RIM_INPUTSINK 1 if it was not.
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RawKeyboard
        {
            public ushort Makecode;                 // Scan code from the key depression
            public ushort Flags;                    // One or more of RI_KEY_MAKE, RI_KEY_BREAK, RI_KEY_E0, RI_KEY_E1
            private readonly ushort Reserved;       // Always 0    
            public ushort VKey;                     // Virtual Key Code
            public uint Message;                    // Corresponding Windows message for example (WM_KEYDOWN, WM_SYASKEYDOWN etc)
            public uint ExtraInformation;           // The device-specific addition information for the event (seems to always be zero for keyboards)
        }

        void ProcessKey(IntPtr handle)
        {
            if (KeyStateChanged == null) return;

            var dwSize = 0;
            GetRawInputData(handle, 0x10000003 /*RID_INPUT*/, IntPtr.Zero, ref dwSize, sizeOfRawInputHeader);

            InputData rawBuffer;

            if (dwSize != GetRawInputData(handle, 0x10000003 /*RID_INPUT*/, out rawBuffer, ref dwSize, sizeOfRawInputHeader))
            {
                return; //Cannot get the buffer.
            }

            int virtualKey = rawBuffer.Data.VKey;
            int flags = rawBuffer.Data.Flags;

            if (virtualKey == 0xFF /*KEYBOARD_OVERRUN_MAKE_CODE*/) return;

            var isE0BitSet = ((flags & 0x02 /*RI_KEY_E0*/) != 0);
            var isBreakBitSet = ((flags & 0x01 /*RI_KEY_BREAK*/) != 0);

            KeyStateChangedEventArgs e = new KeyStateChangedEventArgs(rawBuffer.Header.hDevice, !isBreakBitSet, rawBuffer.Data.Message, virtualKey, isE0BitSet, rawBuffer.Data.Makecode);

            KeyStateChanged(this, e);
        }


        /// <summary>
        /// Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            UnregisterDeviceNotification(deviceNotificationHandle);
            Application.RemoveMessageFilter(messageFilter);
            nativeWindowWrap.Dispose();
            nativeWindowWrap = null;
        }
        #endregion
    }
}
