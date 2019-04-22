using SecretNest.MultiKeyboards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        //If this is changed to false, keys will be captured no matter this window is active in foreground or not.
        const bool CaptureWhileActiveOnly = true;
        Keyboards keyboards;
        Combiner combiner;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Keyboards.StartCatching(Handle, CaptureWhileActiveOnly);

            combiner = new Combiner();
            combiner.TextCombined += Combiner_TextCombined;

            keyboards = new Keyboards(Handle);
            keyboards.KeyStateChanged += Processor_KeyStateChanged;

            refreshDeviceList.PerformClick();
        }

        private void Combiner_TextCombined(object sender, TextCombinedEventArgs e)
        {
            ProcessOneCode(e.Handle, e.Text);
        }

        private void Processor_KeyStateChanged(object sender, KeyStateChangedEventArgs e)
        {
            if (!e.IsPressed)
                ProcessOneKey(e.Handle, e.Key);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            keyboards.Dispose();
        }

        #region Devices
        object singleThreadLock = new object();
        private void RefreshDeviceList_Click(object sender, EventArgs e)
        {
            lock (singleThreadLock)
            {
                deviceList.BeginUpdate();

                deviceList.Items.Clear();

                var devices = Keyboards.Get().ToArray();

                if (devices.Length > 0)
                {
                    ListViewItem[] items = Array.ConvertAll(devices, i => new ListViewItem(new string[] { i.Handle.ToInt64().ToString(), i.Name, i.Type.ToString(), i.Description }));

                    deviceList.Items.AddRange(items);
                }

                deviceList.EndUpdate();
            }
        }
        #endregion

        void ProcessOneKey(IntPtr handle, Keys key)
        {
            //Send to combiner
            combiner.AppendKey(handle, key);

            //Output To ListBox
            lock (listBox_AllKeys)
            {
                if (listBox_AllKeys.Items.Count == 100)
                    listBox_AllKeys.Items.RemoveAt(99);

                string item = string.Format("{0:HHmmssfff} {1}: {2}", DateTime.Now, handle, key);
                listBox_AllKeys.Items.Insert(0, item);
            }
        }

        void ProcessOneCode(IntPtr handle, string text)
        {

            //Output To ListBox
            lock (listBox_Code)
            {
                if (listBox_Code.Items.Count == 100)
                    listBox_Code.Items.RemoveAt(99);

                string item = string.Format("{0:HHmmssfff} {1}: {2}", DateTime.Now, handle, text);
                listBox_Code.Items.Insert(0, item);
            }
        }
    }
}
