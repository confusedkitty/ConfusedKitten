using System;
using System.Web.Hosting;

namespace ConfusedKitten.Common
{
    public static class ProjectHelper
    {
        private static readonly string Config = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

        /// <summary>
        ///  获取当前项目路径
        /// </summary>
        /// <returns></returns>

        public static string ProjectDirectory()
        {
            try
            {
                string configName = Config.Substring(Config.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                if (configName.ToLower().Equals("web.config"))
                {
                    return HostingEnvironment.ApplicationPhysicalPath;
                }
                return Environment.CurrentDirectory;
            }
            catch
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }
    }
}
