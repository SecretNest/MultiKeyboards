using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SecretNest.MultiKeyboards
{
    /// <summary>
    /// Represents an action for appending text to the buffer.
    /// </summary>
    /// <param name="key">New key to be added.</param>
    /// <param name="isPackageFinished">Whether the bar code is finished.</param>
    /// <returns>The text to be appended to the buffer.</returns>
    public delegate string CodeAppendingCallback(Keys key, out bool isPackageFinished);

}
