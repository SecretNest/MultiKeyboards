using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SecretNest.MultiKeyboards
{
    public partial class Combiner
    {
        /// <summary>
        /// Initializes an instance of Combiner, using number only bar code strategy.
        /// </summary>
        public Combiner() : this(NumberOnlyCodeAppend)
        {
        }

        static string NumberOnlyCodeAppend(KeyInfo keyInfo, out bool isPackageFinished)
        {
            if (keyInfo.IsPressed)
            {
                isPackageFinished = false;
                return "";
            }
            switch (keyInfo.Key)
            {
                case Keys.D1:
                case Keys.NumPad1:
                    isPackageFinished = false;
                    return "1";
                case Keys.D2:
                case Keys.NumPad2:
                    isPackageFinished = false;
                    return "2";
                case Keys.D3:
                case Keys.NumPad3:
                    isPackageFinished = false;
                    return "3";
                case Keys.D4:
                case Keys.NumPad4:
                    isPackageFinished = false;
                    return "4";
                case Keys.D5:
                case Keys.NumPad5:
                    isPackageFinished = false;
                    return "5";
                case Keys.D6:
                case Keys.NumPad6:
                    isPackageFinished = false;
                    return "6";
                case Keys.D7:
                case Keys.NumPad7:
                    isPackageFinished = false;
                    return "7";
                case Keys.D8:
                case Keys.NumPad8:
                    isPackageFinished = false;
                    return "8";
                case Keys.D9:
                case Keys.NumPad9:
                    isPackageFinished = false;
                    return "9";
                case Keys.D0:
                case Keys.NumPad0:
                    isPackageFinished = false;
                    return "0";
                case Keys.Enter:
                    isPackageFinished = true;
                    return "";
                default:
                    isPackageFinished = false;
                    return "";
            }
        }
    }
}