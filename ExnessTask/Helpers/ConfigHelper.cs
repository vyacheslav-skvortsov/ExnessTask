using System;
using System.Configuration;

namespace ExnessTask.Helpers
{
    public class ConfigHelper
    {
        public static string GetSetting(string key)
        {
            string t = ConfigurationManager.AppSettings[key];
            return !String.IsNullOrEmpty(t) ? t : null;
        }

        public static string GetSetting(string key, string defaultValue)
        {
            return GetSetting<string>(key, defaultValue);
        }

        public static T GetSetting<T>(string key)
        {
            string t = GetSetting(key);
            return !String.IsNullOrEmpty(t) ? GenericHelper.Convert<T>(t) : default(T);
        }

        public static T GetSetting<T>(string key, T defaultValue)
        {
            string t = GetSetting(key);
            return !String.IsNullOrEmpty(t) ? GenericHelper.Convert<T>(t) : defaultValue;
        }
    }
}
