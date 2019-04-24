using SecretNest.MultiKeyboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Demo
{
    static class AsciiCodeAppender
    {
        static HashSet<IntPtr> lShifts = new HashSet<IntPtr>();
        static HashSet<IntPtr> rShifts = new HashSet<IntPtr>();

        static bool CheckShift(IntPtr handle)
        {
            return lShifts.Contains(handle) || rShifts.Contains(handle);
        }


        internal static string CodeAppending(KeyInfo keyInfo, out bool isPackageFinished)
        {
            //Will not lock shifts due to this method will not be called parallelly in this demo.

            if (keyInfo.Key == Keys.Enter)
            {
                isPackageFinished = keyInfo.IsPressed;
                return "";
            }

            isPackageFinished = false;

            if (keyInfo.Key == Keys.Space)
            {
                if (keyInfo.IsPressed) return "";
                else return " ";
            }
            else if (keyInfo.VKey >= 96 && keyInfo.VKey <= 105) ////Char:0-9 ASCII:48-57 (Numeric Panel)
            {
                if (keyInfo.IsPressed) return "";
                char c = (char)(keyInfo.VKey - 48);
                return c.ToString();
            }
            else if (keyInfo.VKey >= 106 && keyInfo.VKey <= 111) //Char:*+,-./ ASCII: 42-47
            {
                if (keyInfo.IsPressed) return "";
                char c = (char)(keyInfo.VKey - 64);
                return c.ToString();
            }

            if (keyInfo.Key == Keys.LShiftKey)
            {
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

            if (!keyInfo.IsPressed) return "";

            bool shiftSet = CheckShift(keyInfo.Handle);
            if (shiftSet)
            {
                if (keyInfo.VKey >= 65 && keyInfo.VKey <= 90) //Char:A-Z ASCII:65-90
                {
                    char c = (char)keyInfo.VKey;
                    return c.ToString();
                }
                else if (keyInfo.VKey == 49) //Char:! ASCII:33
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
                else if (keyInfo.VKey >= 65 && keyInfo.VKey <= 90) //Char:A-Z=>a-z ASCII:65-90=>97-122
                {
                    char c = (char)(keyInfo.VKey + 32);
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
