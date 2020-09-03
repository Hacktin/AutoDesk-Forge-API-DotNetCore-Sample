using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace forgeSampleAPI_DotNetCore.Core.Configurations
{
    public static class AppSettings
    {
        public static string GetAppSetting(string appSettingKey)
        {
            return Environment.GetEnvironmentVariable(appSettingKey).Trim();
        }
    }
}
