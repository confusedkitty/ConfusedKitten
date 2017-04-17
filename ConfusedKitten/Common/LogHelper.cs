
using System;
using System.IO;
using System.Text;

namespace ConfusedKitten.Common
{
    public static class LogHelper
    {
        /// <summary>
        ///  项目路径
        /// </summary>
        private static string _path = ProjectHelper.ProjectDirectory();

        /// <summary>
        ///  文件大小
        /// </summary>
        private static long filesiza = Convert.ToInt64(XmlHelper.FetchNode(_path + "\\BaseInfo.xml", "LogInfo", "FileSize") ?? "50");

        /// <summary>
        ///  项目路径
        /// </summary>
        private static object log = new object();

        /// <summary>
        ///  日志方法
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="title">标题</param>
        /// <param name="message">信息</param>
        public static void Write(string path, string title, string message)
        {
            // 文件路径
            path = $"{_path}\\{path}";
            StringBuilder sbContent = new StringBuilder();
            sbContent.Append($"{title}：");
            sbContent.Append(message);
            path = JustFileSize(path);

            try
            {
                // 打印日志
                WriteLine(path, sbContent.ToString());
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        ///  根据文件大小获取最新文件路径
        /// </summary>
        /// <param name="path">原路径</param>
        /// <returns>校验后路径</returns>
        private static string JustFileSize(string path)
        {
            #region 判断文件大小

            // 获取不含扩展名的文件名
            string filename = Path.GetFileNameWithoutExtension(path);
            string filepath = Path.GetDirectoryName(path);
            string extension = Path.GetExtension(path);

            for (int i = 0; i >= 0; i++)
            {
                #region 判断原文件

                if (i == 0)
                {
                    if (File.Exists(path))
                    {
                        // 实例文件 
                        FileInfo file = new FileInfo(path);
                        if (file.Length >= 50 * 1024 * 1024)
                        {
                            continue;
                        }
                        else
                        {
                            return path;
                        }
                    }
                    else
                    {
                        return path;
                    }
                }

                #endregion

                #region 判断，追加文件

                else
                {
                    string newpath = $"{filepath}\\{filename}_{i}{extension}";
                    if (File.Exists(newpath))
                    {
                        // 实例文件 
                        FileInfo file = new FileInfo(newpath);
                        if (file.Length >= 50 * 1024 * 1024)
                        {
                            continue;
                        }
                        else
                        {
                            return newpath;
                        }
                    }
                    else
                    {
                        return newpath;
                    }
                }

                #endregion
            }

            #endregion
            return path;
        }

        /// <summary>
        ///  写入文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="message">信息</param>
        private static void WriteLine(string path, string message)
        {
            lock (log)
            {
                try
                {
                     string filepath = Path.GetDirectoryName(path);
                    // 如果不存在就创建file文件夹
                    if (!Directory.Exists(filepath))
                    {
                        Directory.CreateDirectory(filepath);
                    }
                    StringBuilder content = new StringBuilder();
                    content.Append($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}---");
                    if (ProjectHelper.IsWeb)
                    {
                        content.Append($"服务器：{ProjectHelper.F_GetInternalIP()}---");
                        content.Append($"访问者：{ProjectHelper.GetIP()??"获取失败"}---");
                    }
                    content.Append(message);
                    content.Append(Environment.NewLine);
                    File.AppendAllText(path, content.ToString());
                }
                catch
                {
                }
            }
        }
    }
}
