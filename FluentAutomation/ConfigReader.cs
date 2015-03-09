using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace FluentAutomation
{
    public static class ConfigReader
    {
        public static string GetEnvironmentVariableOrAppSetting(string key)
        {
            return Environment.GetEnvironmentVariable(key) ?? ConfigurationManager.AppSettings[key];
        }

        public static bool? GetEnvironmentVariableOrAppSettingAsBoolean(string key)
        {
            string strValue = GetEnvironmentVariableOrAppSetting(key);
            bool value;

            if (bool.TryParse(strValue, out value))
            {
                return value;
            }

            return null;
        }

        public static int? GetEnvironmentVariableOrAppSettingAsInteger(string key)
        {
            string strValue = GetEnvironmentVariableOrAppSetting(key);
            int value;

            if (int.TryParse(strValue, out value))
            {
                return value;
            }

            return null;
        }
    }
}
