using SecretNest.MultiKeyboards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Demo
{
    public partial class Form1 : Form
    {
        //If this is changed to false, keys will be captured no matter this window is active in foreground or not.
        const bool CaptureWhileActiveOnly = true;

        public Form1()
        {
            InitializeComponent();
        }

        Keyboards keyboards;
        Combiner combiner;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Start catching first.
            Keyboards.StartCatching(Handle, CaptureWhileActiveOnly);

            //combiner = new Combiner(); //Use numeric only
            combiner = new Combiner(AsciiCodeAppender.CodeAppending);

            combiner.TextCombined += Combiner_TextCombined;

            //Key processing is started when new instance created.
            keyboards = new Keyboards(Handle);
            keyboards.KeyStateChanged += Processor_KeyStateChanged;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Dispose resources
            keyboards.Dispose();
        }

        private void Processor_KeyStateChanged(object sender, KeyStateChangedEventArgs e)
        {
            //Send to combiner
            combiner.AppendKey(e.Handle, e.KeyInfo);
        }

        private void Combiner_TextCombined(object sender, TextCombinedEventArgs e)
        {
            ProcessOneCode(e.Handle, e.Text);
        }

        #region Code Recognize
        /*
         * Code Format:
         * 00xx: Set Name to xx (xx is not 00)
         * 99xx: Validate device name (xx is not 00)
         * 0000: Remove device
         * 99xx: Validate device is not linked
         * Other: Normal Output
         */


        Dictionary<IntPtr, string> deviceNames = new Dictionary<IntPtr, string>();

        void ProcessOneCode(IntPtr handle, string text)
        {
            lock (deviceNames)
            {
                if (text.Length == 4)
                {
                    var high2 = text.Substring(0, 2);
                    var low2 = text.Substring(2);
                    if (high2 == "00")
                    {
                        if (low2 == "00")
                        {
                            //Remove device
                            deviceNames.Remove(handle);
                            OutputText(string.Format("Pass : Keyboard Removed: {0}", handle));
                        }
                        else
                        {
                            //Set Name to low2
                            deviceNames[handle] = low2;
                            OutputText(string.Format("Pass : Keyboard {0} is named as {1}.", handle, low2));
                        }
                        return;
                    }
                    else if (high2 == "99")
                    {
                        if (low2 == "00")
                        {
                            //Validate device is not linked
                            if (deviceNames.ContainsKey(handle))
                            {
                                OutputText(string.Format("Error: Keyboard {0} is linked already.", handle));
                            }
                            else
                            {
                                OutputText(string.Format("Pass : Keyboard {0} is not linked.", handle));
                            }
                        }
                        else
                        {
                            //Validate device name
                            if (deviceNames.TryGetValue(handle, out string name))
                            {
                                if (name == low2)
                                {
                                    OutputText(string.Format("Pass : Keyboard {0} is confirmed as {1}.", handle, low2));
                                }
                                else
                                {
                                    OutputText(string.Format("Error: Keyboard {0} is named as {1}, not {2}.", handle, name, low2));
                                }
                            }
                            else
                            {
                                OutputText(string.Format("Error: Keyboard {0} is not linked.", handle));
                            }
                        }
                        return;
                    }
                }

                //Normal Output
                if (deviceNames.TryGetValue(handle, out string deviceName))
                {
                    OutputText(string.Format("Code : Device: {0}, Text: {1}", deviceName, text));
                }
                else
                {
                    OutputText(string.Format("Error: Keyboard {0} is not linked.", handle));
                }
            }
        }

        void OutputText(string text)
        {
            if (listBox1.Items.Count == 100)
                listBox1.Items.RemoveAt(99);

            string item = string.Format("Time:{0:HH:mm:ss.fff} {1}", DateTime.Now, text);
            listBox1.Items.Insert(0, item);
        }

        #endregion
    }
}
