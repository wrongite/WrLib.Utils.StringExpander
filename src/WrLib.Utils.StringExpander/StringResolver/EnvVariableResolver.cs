using System;
using WrLib.Utils.StringExpander.Core;

namespace WrLib.Utils.StringExpander.StringResolver
{
    public class EnvironmentVariableResolver : IStringKeyValueResolver
    {
        public string Resolve(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }
    }
}
