using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace FluentAutomation
{
    public static class ConfigReader
    {
        public static string GetSetting(string key, string externalConfigFile = null)
        {
            return Environment.GetEnvironmentVariable(string.Format("bamboo_{0}", key))
                ?? Environment.GetEnvironmentVariable(key)
                ?? GetConfigurationFileSetting(string.Format("WbTstr:{0}", key), externalConfigFile)
                ?? GetConfigurationFileSetting(key, externalConfigFile);
        }

        public static bool? GetSettingAsBoolean(string key)
        {
            string strValue = GetSetting(key);
            bool value;

            if (bool.TryParse(strValue, out value))
            {
                return value;
            }

            return null;
        }

        public static int? GetSettingAsInteger(string key)
        {
            string strValue = GetSetting(key);
            int value;

            if (int.TryParse(strValue, out value))
            {
                return value;
            }

            return null;
        }

        private static string GetConfigurationFileSetting(string key, string externalConfigFile = null)
        {
            string configFile = externalConfigFile ?? GetExternalConfigurationFilePath();
            if (!string.IsNullOrEmpty(configFile) && File.Exists(configFile))
            {
                NameValueCollection settings = GetNameValueCollectionSection("settings", configFile);
                string valueFromConfigFile = settings[key];

                if (!string.IsNullOrEmpty(valueFromConfigFile))
                {
                    return valueFromConfigFile;
                }
            }
            return ConfigurationManager.AppSettings[key];
        }

        private static NameValueCollection GetNameValueCollectionSection(string section, string filePath)
        {
            string file = filePath;
            XmlDocument doc = new XmlDocument();
            NameValueCollection nameValueColl = new NameValueCollection();

            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = file;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            string xml = config.GetSection(section).SectionInformation.GetRawXml();
            doc.LoadXml(xml);

            XmlNode list = doc.ChildNodes[0];
            foreach (XmlNode node in list)
            {
                nameValueColl.Add(node.Attributes[0].Value, node.Attributes[1].Value);

            }

            return nameValueColl;
        }

        private static string GetExternalConfigurationFilePath()
        {
            return ConfigurationManager.AppSettings["WbTstr:ConfigFile"];
        }
    }
}
