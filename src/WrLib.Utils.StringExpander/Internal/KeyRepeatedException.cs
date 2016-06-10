using System;

namespace WrLib.Utils.StringExpander.Internal
{
    internal class KeyRepeatedException: InvalidOperationException
    {
        public readonly string KeyName;

        public KeyRepeatedException(string keyName)
        {
            this.KeyName = keyName;
        }
    }
}