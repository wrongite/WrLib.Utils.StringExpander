/*
 * Copyright 2016 wrongite
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using WrLib.Utils.StringExpander.Core;
using WrLib.Utils.StringExpander.Internal;

namespace WrLib.Utils.StringExpander
{
    public class StringExpander : IStringExpander
    {
        private static readonly Regex _pattern = new Regex(@"\$(\$|\((\w+(\.\w+)*)\))", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private readonly Func<string, string> _resolver;
        private readonly Dictionary<string, string> _resolvedCache = new Dictionary<string, string>();
        private readonly Stack<string> _resolvingKeysStack = new Stack<string>();

        public StringExpander(Func<string, string> resolver)
        {
            this._resolver = resolver;
        }

        public StringExpander(IStringKeyValueResolver resolver)
        {
            this._resolver = resolver.Resolve;
        }

        public StringExpander(IDictionary<string, string> dict)
        {
            this._resolver = key =>
            {
                string result;
                if (dict.TryGetValue(key, out result))
                {
                    return result;
                }
                return null;
            };
        }

        private bool TryResolve(string keyName, out string result)
        {
            try
            {
                result = _resolver(keyName);
                if (result != null)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                result = null;
            }
            return false;
        }

        public string Resolve(string keyName)
        {
            string result;
            if (TryResolve(keyName, out result))
            {
                return Expand(result);
            }
            return null;
        }

        private string _matchEval(Match match)
        {
            string raw = match.Groups[0].Value;
            string keyName = match.Groups[2].Value;

            if (raw.StartsWith("$$"))
            {
                return raw.Substring(1);
            }
            else if (_resolvedCache.ContainsKey(keyName))
            {
                return _resolvedCache[keyName];
            }
            else if(!this._resolvingKeysStack.Contains(keyName))
            {
                string result = null;

                this._resolvingKeysStack.Push(keyName);
                try
                {
                    string tempResult;

                    if (TryResolve(keyName, out tempResult))
                    {
                        try
                        {
                            result = Expand(tempResult);
                        }
                        catch (KeyRepeatedException exc)
                        {
                            result = tempResult;
                            if (exc.KeyName != keyName)
                            {
                                throw exc;
                            }
                        }
                    }
                    else
                    {
                        result = match.Groups[0].Value;
                    }

                    return result;
                }
                finally
                {
                    Debug.Assert(result != null);
                    this._resolvedCache[keyName] = result;
                    this._resolvingKeysStack.Pop();
                }
            }
            else
            {
                throw new KeyRepeatedException(keyName);
            }
        }

        public string Expand(string s)
        {
            if (s == null)
            {
                return null;
            }

            return _pattern.Replace(s, _matchEval);
        }
    }
}
