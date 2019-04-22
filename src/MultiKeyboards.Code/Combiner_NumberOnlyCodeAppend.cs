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

        static string NumberOnlyCodeAppend(Keys key, out bool isPackageFinished)
        {
            switch (key)
            {
                case Keys.D1:
                    isPackageFinished = false;
                    return "1";
                case Keys.D2:
                    isPackageFinished = false;
                    return "2";
                case Keys.D3:
                    isPackageFinished = false;
                    return "3";
                case Keys.D4:
                    isPackageFinished = false;
                    return "4";
                case Keys.D5:
                    isPackageFinished = false;
                    return "5";
                case Keys.D6:
                    isPackageFinished = false;
                    return "6";
                case Keys.D7:
                    isPackageFinished = false;
                    return "7";
                case Keys.D8:
                    isPackageFinished = false;
                    return "8";
                case Keys.D9:
                    isPackageFinished = false;
                    return "9";
                case Keys.D0:
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
