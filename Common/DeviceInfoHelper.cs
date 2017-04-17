using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace ConfusedKitten.Common
{
    public static class DeviceInfoHelper
    {
        #region 取得IP地址

        /// <summary>
        /// 取得IP地址.
        /// </summary>
        /// <returns>本机IP地址</returns>
        public static string GetMachineIpAddress()
        {
            // 定义要返回的Ip地址.
            string strIpAddress = string.Empty;

            try
            {
                foreach (IPAddress ipAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    if (ipAddress.AddressFamily.ToString() == "InterNetwork")
                    {
                        strIpAddress = ipAddress.ToString();
                        break;
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

            // 返回Ip地址.
            return strIpAddress;
        }

        #endregion

        #region 取得Mac地址

        /// <summary>
        /// 取得Mac地址.
        /// </summary>
        /// <returns>本机Mac地址</returns>
        public static string GetMachineMacAddress()
        {
            // 定义要返回的Mac地址.
            ProcessStartInfo startInfo = new ProcessStartInfo("ipconfig", "/all");
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            Process p = Process.Start(startInfo);
            bool flag = false;
            //截取输出流
            if (p != null)
            {
                StreamReader reader = p.StandardOutput;
                string line = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        line = line.Trim();

                        if (line.StartsWith("以太网适配器"))
                        {
                            flag = true;
                        }
                        if (flag && line.StartsWith("物理地址"))
                        {
                            //等待程序执行完退出进程
                            p.WaitForExit();
                            p.Close();
                            reader.Close();

                            return line.Substring(line.IndexOf(":", StringComparison.Ordinal) + 1).Trim();
                        }
                    }
                    line = reader.ReadLine();
                }
            }
            return string.Empty;
        }

        #endregion
    }
}
