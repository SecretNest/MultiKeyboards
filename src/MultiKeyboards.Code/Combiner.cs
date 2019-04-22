using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SecretNest.MultiKeyboards
{
    /// <summary>
    /// Building text from key input.
    /// </summary>
    public partial class Combiner
    {
        ConcurrentDictionary<IntPtr, StringBuilder> buffers = new ConcurrentDictionary<IntPtr, StringBuilder>();

        /// <summary>
        /// Occurs while a text is combined.
        /// </summary>
        public event EventHandler<TextCombinedEventArgs> TextCombined;

        /// <summary>
        /// Clear all buffers.
        /// </summary>
        public void ClearAllBuffers()
        {
            buffers.Clear();
        }

        /// <summary>
        /// Clear buffer of the handle specified.
        /// </summary>
        /// <param name="handle">Handle of the buffer to be cleared.</param>
        public void ClearBuffer(params IntPtr[] handle)
        {
            foreach (var item in handle)
                buffers.TryRemove(item, out _);
        }

        CodeAppendingCallback codeAppendCallback;

        /// <summary>
        /// Initializes an instance of Combiner, using user defined strategy.
        /// </summary>
        /// <param name="codeAppendCallback"></param>
        public Combiner(CodeAppendingCallback codeAppendCallback)
        {
            this.codeAppendCallback = codeAppendCallback;
        }

        /// <summary>
        /// Append a key to a buffer.
        /// </summary>
        /// <param name="handle">Handle of the buffer to be appended on.</param>
        /// <param name="key">Key to be appended.</param>
        public void AppendKey(IntPtr handle, Keys key)
        {
            var buffer = buffers.GetOrAdd(handle, i => new StringBuilder());
            buffer.Append(codeAppendCallback(key, out var isPackageFinished));
            if (isPackageFinished)
            {
                string result = buffer.ToString();
                buffer.Clear();
                TextCombined?.Invoke(this, new TextCombinedEventArgs(handle, result));
            }
        }

    }
}
