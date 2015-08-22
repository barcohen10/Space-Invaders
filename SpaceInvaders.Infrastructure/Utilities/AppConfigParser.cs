using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SpaceInvaders.Infrastructure.Utilities
{
    public class AppConfigParser
    {
        public static T Create<T>() where T : new()
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            T configurationSettingsInstance = new T();
            PropertyInfo[] properties = configurationSettingsInstance.GetType().GetProperties();

            foreach (var propertyInfo in properties)
            {
                string key = configurationSettingsInstance.GetType().Name + "." + propertyInfo.Name;
                string v = appSettings[key];

                if (v == null)
                {
                    continue;
                }

                object targetValue = TypeDescriptor.GetConverter(propertyInfo.PropertyType).ConvertFrom(v);
                propertyInfo.SetValue(configurationSettingsInstance, targetValue, null);
            }

            return configurationSettingsInstance;

        }


    }
}
