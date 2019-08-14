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
        Combiner directOutputCombiner;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Start catching first.
            Keyboards.StartCatching(Handle, CaptureWhileActiveOnly);

            //combiner = new Combiner(); //Use numeric only
            combiner = new Combiner(AsciiCodeAppender.CodeAppending);
            directOutputCombiner = new Combiner(SendKeysAppender.CodeAppending);

            combiner.TextCombined += Combiner_TextCombined;
            directOutputCombiner.TextCombined += DirectOutputCombiner_TextCombined;

            //Key processing is started when new instance created.
            keyboards = new Keyboards(Handle);
            keyboards.KeyStateChanged += Processor_KeyStateChanged;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Dispose resources
            keyboards.Dispose();
        }

        private void Processor_KeyStateChanged(object sender, KeyStateChangedEventArgs e)
        {
            if (directOutput.Contains(e.Handle))
            {
                //If this key is from a special keyboard.
                directOutputCombiner.AppendKey(e.Handle, e.KeyInfo);
            }
            else
            {
                //Send to combiner
                combiner.AppendKey(e.Handle, e.KeyInfo);
            }
        }

        private void Combiner_TextCombined(object sender, TextCombinedEventArgs e)
        {
            ProcessOneCode(e.Handle, e.Text);
        }

        private void DirectOutputCombiner_TextCombined(object sender, TextCombinedEventArgs e)
        {
            textBox1.Text += e.Text;
        }

        #region Code Recognize
        /*
         * Code Format:
         * 00xxx: Set Name to xxx (xxx is not x00)
         * 99xxx: Validate device name (xxx is not x00)
         * 00000: Remove device
         * 99000: Validate device is not linked
         * =====: Set this device to direct output mode (for keyboard).
         * Other: Normal Output
         */

        //You may want to backup this dictionary to save the mappings for future instances.
        Dictionary<IntPtr, string> deviceNames = new Dictionary<IntPtr, string>();

        //All keyboards in this list will be sent to window directly.
        HashSet<IntPtr> directOutput = new HashSet<IntPtr>();

        void ProcessOneCode(IntPtr handle, string text)
        {
            lock (deviceNames)
            {
                if (text.Length == 5)
                {
                    var high2 = text.Substring(0, 2);
                    var low2 = text.Substring(2);
                    if (high2 == "00")
                    {
                        if (low2 == "000")
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
                        if (low2 == "000")
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
                    else if (text == "=====")
                    {
                        directOutput.Add(handle);
                        OutputText(string.Format("Pass : Keyboard {0} is set to direct output mode.", handle));
                    }
                    return;
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
