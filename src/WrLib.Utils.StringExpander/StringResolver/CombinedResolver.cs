/*
 * Copyright 2016 Nattapon Artsarikorn
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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
