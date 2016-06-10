using System.Collections.Generic;
using WrLib.Utils.StringExpander.Core;

namespace WrLib.Utils.StringExpander.StringResolver
{
    public class CombinedResolver : IStringKeyValueResolver
    {
        private readonly IEnumerable<IStringKeyValueResolver> _resolvers;

        public CombinedResolver(IEnumerable<IStringKeyValueResolver> resolvers)
        {
            this._resolvers = resolvers;
        }

        public CombinedResolver(params IStringKeyValueResolver[] resolvers)
            : this((IEnumerable<IStringKeyValueResolver>)resolvers)
        {
        }

        public string Resolve(string key)
        {
            foreach (var x in this._resolvers)
            {
                try
                {
                    var result = x.Resolve(key);
                    if (result != null)
                    {
                        return result;
                    }
                }
                catch
                {
                    // ignored
                }
            }

            return null;
        }
    }
}
