using SecretNest.MultiKeyboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Demo
{
    static class SendKeysAppender
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        static HashSet<IntPtr> lShifts = new HashSet<IntPtr>();
        static HashSet<IntPtr> rShifts = new HashSet<IntPtr>();
        static HashSet<IntPtr> lCtrls = new HashSet<IntPtr>();
        static HashSet<IntPtr> rCtrls = new HashSet<IntPtr>();
        static HashSet<IntPtr> lAlts = new HashSet<IntPtr>();
        static HashSet<IntPtr> rAlts = new HashSet<IntPtr>();

        static bool CheckShift(IntPtr handle)
        {
            return lShifts.Contains(handle) || rShifts.Contains(handle);
        }

        static bool CheckCtrl(IntPtr handle)
        {
            return lCtrls.Contains(handle) || rCtrls.Contains(handle);
        }

        static bool CheckAlt(IntPtr handle)
        {
            return lAlts.Contains(handle) || rAlts.Contains(handle);
        }

        static string GetPrefix(IntPtr handle)
        {
            string prefix = "";
            if (CheckShift(handle))
                prefix = "+";
            if (CheckCtrl(handle))
                prefix += "^";
            if (CheckAlt(handle))
                prefix += "%";
            return prefix;
        }

        static void CheckCapital(IntPtr handle, out bool shifted, out bool capital)
        {
            var capsLock = CheckCapLock();
            shifted = CheckShift(handle);
            if (capsLock)
                capital = !shifted;
            else
                capital = shifted;
        }

        static bool CheckCapLock()
        {
            return (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
        }

        static bool CheckNumLock()
        {
            return (((ushort)GetKeyState(0x90)) & 0xffff) != 0;
        }

        internal static string CodeAppending(KeyInfo keyInfo, out bool isPackageFinished)
        {
            //Will not lock shifts due to this method will not be called parallelly in this demo.

            if (keyInfo.Key == Keys.LShiftKey)
            {
                isPackageFinished = false;
                if (keyInfo.IsPressed)
                {
                    lShifts.Add(keyInfo.Handle);
                }
                else
                {
                    lShifts.Remove(keyInfo.Handle);
                }
                return "";
            }
            else if (keyInfo.Key == Keys.RShiftKey)
            {
                isPackageFinished = false;
                if (keyInfo.IsPressed)
                {
                    rShifts.Add(keyInfo.Handle);
                }
                else
                {
                    rShifts.Remove(keyInfo.Handle);
                }
                return "";
            }
            else if (keyInfo.Key == Keys.LControlKey)
            {
                isPackageFinished = false;
                if (keyInfo.IsPressed)
                {
                    lCtrls.Add(keyInfo.Handle);
                }
                else
                {
                    lCtrls.Remove(keyInfo.Handle);
                }
                return "";
            }
            else if (keyInfo.Key == Keys.RControlKey)
            {
                isPackageFinished = false;
                if (keyInfo.IsPressed)
                {
                    rCtrls.Add(keyInfo.Handle);
                }
                else
                {
                    rCtrls.Remove(keyInfo.Handle);
                }
                return "";
            }
            else if (keyInfo.Key == Keys.LMenu)
            {
                isPackageFinished = false;
                if (keyInfo.IsPressed)
                {
                    lAlts.Add(keyInfo.Handle);
                }
                else
                {
                    lAlts.Remove(keyInfo.Handle);
                }
                return "";
            }
            else if (keyInfo.Key == Keys.RMenu)
            {
                isPackageFinished = false;
                if (keyInfo.IsPressed)
                {
                    rAlts.Add(keyInfo.Handle);
                }
                else
                {
                    rAlts.Remove(keyInfo.Handle);
                }
                return "";
            }

            CheckCapital(keyInfo.Handle, out bool shifted, out bool capital);
            
            isPackageFinished = true;
            if (!keyInfo.IsPressed) return "";

            if (keyInfo.Key == Keys.OemBackslash || keyInfo.Key == Keys.Back)
            {
                return GetPrefix(keyInfo.Handle) + "{BACKSPACE}";
            }
            else if (keyInfo.Key == Keys.Cancel)
            {
                return GetPrefix(keyInfo.Handle) + "{BREAK}";
            }
            else if (keyInfo.Key == Keys.Delete)
            {
                return GetPrefix(keyInfo.Handle) + "{DELETE}";
            }
            else if (keyInfo.Key == Keys.Down)
            {
                return GetPrefix(keyInfo.Handle) + "{DOWN}";
            }
            else if (keyInfo.Key == Keys.End)
            {
                return GetPrefix(keyInfo.Handle) + "{END}";
            }
            else if (keyInfo.Key == Keys.Enter)
            {
                return GetPrefix(keyInfo.Handle) + "{ENTER}";
            }
            else if (keyInfo.Key == Keys.Escape)
            {
                return GetPrefix(keyInfo.Handle) + "{ESC}";
            }
            else if (keyInfo.Key == Keys.Help)
            {
                return GetPrefix(keyInfo.Handle) + "{HELP}";
            }
            else if (keyInfo.Key == Keys.Home)
            {
                return GetPrefix(keyInfo.Handle) + "{HOME}";
            }
            else if (keyInfo.Key == Keys.Insert)
            {
                return GetPrefix(keyInfo.Handle) + "{INSERT}";
            }
            else if (keyInfo.Key == Keys.Left)
            {
                return GetPrefix(keyInfo.Handle) + "{LEFT}";
            }
            else if (keyInfo.Key == Keys.PageDown)
            {
                return GetPrefix(keyInfo.Handle) + "{PGDN}";
            }
            else if (keyInfo.Key == Keys.PageUp)
            {
                return GetPrefix(keyInfo.Handle) + "{PGUP}";
            }
            else if (keyInfo.Key == Keys.Right)
            {
                return GetPrefix(keyInfo.Handle) + "{RIGHT}";
            }
            else if (keyInfo.Key == Keys.Tab)
            {
                return GetPrefix(keyInfo.Handle) + "{TAB}";
            }
            else if (keyInfo.Key == Keys.Up)
            {
                return GetPrefix(keyInfo.Handle) + "{UP}";
            }
            else if (keyInfo.Key == Keys.F1)
            {
                return GetPrefix(keyInfo.Handle) + "{F1}";
            }
            else if (keyInfo.Key == Keys.F2)
            {
                return GetPrefix(keyInfo.Handle) + "{F2}";
            }
            else if (keyInfo.Key == Keys.F3)
            {
                return GetPrefix(keyInfo.Handle) + "{F3}";
            }
            else if (keyInfo.Key == Keys.F4)
            {
                return GetPrefix(keyInfo.Handle) + "{F4}";
            }
            else if (keyInfo.Key == Keys.F5)
            {
                return GetPrefix(keyInfo.Handle) + "{F5}";
            }
            else if (keyInfo.Key == Keys.F6)
            {
                return GetPrefix(keyInfo.Handle) + "{F6}";
            }
            else if (keyInfo.Key == Keys.F7)
            {
                return GetPrefix(keyInfo.Handle) + "{F7}";
            }
            else if (keyInfo.Key == Keys.F8)
            {
                return GetPrefix(keyInfo.Handle) + "{F8}";
            }
            else if (keyInfo.Key == Keys.F9)
            {
                return GetPrefix(keyInfo.Handle) + "{F9}";
            }
            else if (keyInfo.Key == Keys.F10)
            {
                return GetPrefix(keyInfo.Handle) + "{F10}";
            }
            else if (keyInfo.Key == Keys.F11)
            {
                return GetPrefix(keyInfo.Handle) + "{F11}";
            }
            else if (keyInfo.Key == Keys.F12)
            {
                return GetPrefix(keyInfo.Handle) + "{F12}";
            }
            else if (keyInfo.Key == Keys.F13)
            {
                return GetPrefix(keyInfo.Handle) + "{F13}";
            }
            else if (keyInfo.Key == Keys.F14)
            {
                return GetPrefix(keyInfo.Handle) + "{F14}";
            }
            else if (keyInfo.Key == Keys.F15)
            {
                return GetPrefix(keyInfo.Handle) + "{F15}";
            }
            else if (keyInfo.Key == Keys.F16)
            {
                return GetPrefix(keyInfo.Handle) + "{F16}";
            }
            else if (keyInfo.Key == Keys.Add)
            {
                return GetPrefix(keyInfo.Handle) + "{ADD}";
            }
            else if (keyInfo.Key == Keys.Subtract)
            {
                return GetPrefix(keyInfo.Handle) + "{SUBTRACT}";
            }
            else if (keyInfo.Key == Keys.Multiply)
            {
                return GetPrefix(keyInfo.Handle) + "{MULTIPLY}";
            }
            else if (keyInfo.Key == Keys.Divide)
            {
                return GetPrefix(keyInfo.Handle) + "{DIVIDE}";
            }
            else if (keyInfo.Key == Keys.Space)
            {
                return GetPrefix(keyInfo.Handle) + " ";
            }

            if (!CheckNumLock())
            {
                if (keyInfo.Key == Keys.NumPad2)
                {
                    return GetPrefix(keyInfo.Handle) + "{DOWN}";
                }
                else if (keyInfo.Key == Keys.NumPad1)
                {
                    return GetPrefix(keyInfo.Handle) + "{END}";
                }
                else if (keyInfo.Key == Keys.NumPad7)
                {
                    return GetPrefix(keyInfo.Handle) + "{HOME}";
                }
                else if (keyInfo.Key == Keys.NumPad0)
                {
                    return GetPrefix(keyInfo.Handle) + "{INSERT}";
                }
                else if (keyInfo.Key == Keys.NumPad4)
                {
                    return GetPrefix(keyInfo.Handle) + "{LEFT}";
                }
                else if (keyInfo.Key == Keys.NumPad3)
                {
                    return GetPrefix(keyInfo.Handle) + "{PGDN}";
                }
                else if (keyInfo.Key == Keys.NumPad9)
                {
                    return GetPrefix(keyInfo.Handle) + "{PGUP}";
                }
                else if (keyInfo.Key == Keys.NumPad6)
                {
                    return GetPrefix(keyInfo.Handle) + "{RIGHT}";
                }
                else if (keyInfo.Key == Keys.NumPad8)
                {
                    return GetPrefix(keyInfo.Handle) + "{UP}";
                }
            }
                

            if (keyInfo.VKey >= 96 && keyInfo.VKey <= 105) ////Char:0-9 ASCII:48-57 (Numeric Panel)
            {
                char c = (char)(keyInfo.VKey - 48);
                return c.ToString();
            }
            else if (keyInfo.VKey >= 106 && keyInfo.VKey <= 111) //Char:*+,-./ ASCII: 42-47
            {
                char c = (char)(keyInfo.VKey - 64);
                return c.ToString();
            }

            if (keyInfo.VKey >= 65 && keyInfo.VKey <= 90) //Char:A-Z ASCII:65-90
            {
                if (capital)
                {
                    char c = (char)keyInfo.VKey;
                    return c.ToString();
                }
                else
                {
                    //Char:A-Z=>a-z ASCII:65-90=>97-122
                    char c = (char)(keyInfo.VKey + 32);
                    return c.ToString();
                }
            }

            if (shifted)
            {
                if (keyInfo.VKey == 49) //Char:! ASCII:33
                {
                    return "!";
                }
                else if (keyInfo.VKey == 222) //Char:" ASCII:34
                {
                    return "\"";
                }
                else if (keyInfo.VKey >= 51 && keyInfo.VKey <= 53) //Char:#$% ASCII: 35-37
                {
                    char c = (char)(keyInfo.VKey - 16);
                    return c.ToString();
                }
                else if (keyInfo.VKey == 55) //Char:& ASCII:38
                {
                    return "&";
                }
                else if (keyInfo.VKey == 57) //Char:( ASCII:40
                {
                    return "(";
                }
                else if (keyInfo.VKey == 48) //Char:) ASCII:41
                {
                    return ")";
                }
                else if (keyInfo.VKey == 56) //Char:* ASCII:42
                {
                    return "*";
                }
                else if (keyInfo.VKey == 187) //Char:+ ASCII:43
                {
                    return "+";
                }
                else if (keyInfo.VKey == 186) //Char:: ASCII:58
                {
                    return ":";
                }
                else if (keyInfo.VKey == 188) //Char:< ASCII:60
                {
                    return "<";
                }
                else if (keyInfo.VKey == 190) //Char:> ASCII:62
                {
                    return ">";
                }
                else if (keyInfo.VKey == 191) //Char:? ASCII:63
                {
                    return "?";
                }
                else if (keyInfo.VKey == 50) //Char:@ ASCII:64
                {
                    return "@";
                }
                else if (keyInfo.VKey == 54) //Char:^ ASCII:94
                {
                    return "^";
                }
                else if (keyInfo.VKey == 189) //Char:_ ASCII:95
                {
                    return "_";
                }
                else if (keyInfo.VKey >= 219 && keyInfo.VKey <= 221) //Char:{|} ASCII: 123-125
                {
                    char c = (char)(keyInfo.VKey - 96);
                    return c.ToString();
                }
                else if (keyInfo.VKey == 192) //Char:~ ASCII:126
                {
                    return "~";
                }
            }
            else
            {
                if (keyInfo.VKey >= 48 && keyInfo.VKey <= 57) //Char:0-9 ASCII:48-57
                {
                    char c = (char)keyInfo.VKey;
                    return c.ToString();
                }
                else if (keyInfo.VKey >= 188 && keyInfo.VKey <= 191) //Char:,-./ ASCII: 44-47
                {
                    char c = (char)(keyInfo.VKey - 144);
                    return c.ToString();
                }
                else if (keyInfo.VKey == 186) //Char:; ASCII:59
                {
                    return ";";
                }
                else if (keyInfo.VKey == 187) //Char:= ASCII:61
                {
                    return "=";
                }
                else if (keyInfo.VKey == 222) //Char:' ASCII:39
                {
                    return "'";
                }
                else if (keyInfo.VKey >= 219 && keyInfo.VKey <= 221) //Char:[\] ASCII: 91-93
                {
                    char c = (char)(keyInfo.VKey - 128);
                    return c.ToString();
                }
                else if (keyInfo.VKey == 192) //Char:` ASCII:96
                {
                    return "`";
                }
            }
            return "";
        }
    }
}
