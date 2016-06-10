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

using System.Configuration;
using WrLib.Utils.StringExpander.Core;

namespace WrLib.Utils.StringExpander.StringResolver
{
    public class AppSettingsResolver: IStringKeyValueResolver
    {
        private static Configuration _openConfiguration(string appConfigFilePath)
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap { ExeConfigFilename = appConfigFilePath };
            return ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
        }

        private readonly KeyValueConfigurationCollection _settings;

        public AppSettingsResolver(string appConfigFilePath)
            : this(_openConfiguration(appConfigFilePath))
        {
        }

        public AppSettingsResolver(Configuration configuration)
            : this(configuration.AppSettings.Settings)
        {

        }

        public AppSettingsResolver(KeyValueConfigurationCollection settings)
        {
            this._settings = settings;
        }

        public string Resolve(string key)
        {
            var element = this._settings[key];
            if (element != null)
            {
                return element.Value;
            }
            return null;
        }
    }
}
