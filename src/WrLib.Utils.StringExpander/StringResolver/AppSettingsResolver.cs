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
