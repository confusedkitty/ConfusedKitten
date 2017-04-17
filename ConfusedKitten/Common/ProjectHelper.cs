using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;

namespace ConfusedKitten.Common
{
    public static class ProjectHelper
    {
        /// <summary>
        ///  获取应用程序域配置文件
        /// </summary>
        private static readonly string Config = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

        /// <summary>
        ///  获取应用程序域配置文件
        /// </summary>
        private static readonly string ConfigName = Config.Substring(Config.LastIndexOf("\\", StringComparison.Ordinal) + 1);

        /// <summary>
        ///  是否未web项目
        /// </summary>
        public static readonly bool IsWeb = ConfigName.ToLower().Equals("web.config");

        /// <summary>
        ///  获取当前项目路径
        /// </summary>
        /// <returns></returns>

        public static string ProjectDirectory()
        {
            try
            {
                if (IsWeb)
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

        /// <summary>
        ///  获取程序运行所在机器内网IP
        /// </summary>
        /// <returns></returns>
        public static string F_GetInternalIP()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = String.Empty;
            try
            {
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(result))
                {
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (string.IsNullOrEmpty(result))
                {
                    result = HttpContext.Current.Request.UserHostAddress;
                }
                if (string.IsNullOrEmpty(result) || !IsIP(result))
                {
                    return "127.0.0.1";
                }
            }
            catch { }
            return result;

        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
    }
}
