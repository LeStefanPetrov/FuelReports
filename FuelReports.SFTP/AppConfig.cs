using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FuelReports.Cli
{
    public class AppConfig
    {
        public static string GetAppConfigValue(string appConfigKey)
        {
            return ConfigurationManager.AppSettings[appConfigKey];
        }
    }
}
