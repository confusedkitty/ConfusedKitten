using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace ConfusedKitten.Down
{
    public static class Download
    {
        /// <summary>
        /// 下载文件保留字
        /// </summary>
        public static string PersistExp = ".tefi";

        /// <summary>
        ///  普通下载
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="loadPath">下载路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="flag">是否备份文件，默认备份名，原文件+.back</param>
        /// <returns></returns>
        public static bool DownloadFile(string savePath, string loadPath, string fileName = null, bool flag = true)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = Path.GetFileName(loadPath);
            }
            using (WebClient client = new WebClient())
            {
                try
                {
                    JustFileAndDic(savePath, true);
                    client.DownloadFile(loadPath, savePath + fileName);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        ///  断点续传
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="loadPath">下载路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="flag">备份区分，默认备份名，原文件+.back</param>
        /// <returns></returns>
        public static bool ResumeDownloadFile(string savePath, string loadPath, string fileName = null, bool flag = true)
        {
            // 自定义文件名为空的情况
            if (string.IsNullOrEmpty(fileName))
            {
                // 取得下载文件名
                fileName = Path.GetFileName(loadPath);
            }
            savePath = savePath + fileName;
            // 判断保存路径是否存在，备份原文件
            JustFileAndDic(savePath, true);
            // 定义临时下载路径
            string strLocalPathTemp = savePath + PersistExp;
            // 文件下载结果.
            bool bDownload = true;

            //打开上次下载的文件或新建文件 
            long lStartPos = 0;
            FileStream fs;
            if (File.Exists(strLocalPathTemp))
            {
                fs = File.OpenWrite(strLocalPathTemp);
                lStartPos = fs.Length;
                fs.Seek(lStartPos, SeekOrigin.Current); //移动文件流中的当前指针 
            }
            else
            {
                fs = new FileStream(strLocalPathTemp, FileMode.Create);
                lStartPos = 0;
            }

            //打开网络连接 
            try
            {
                // 定义验证证书的回调函数.
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(loadPath);
                if (lStartPos > 0)
                    request.AddRange((int)lStartPos); //设置Range值

                //向服务器请求，获得服务器回应数据流 
                Stream ns = request.GetResponse().GetResponseStream();

                byte[] nbytes = new byte[512];
                int nReadSize = 0;
                if (ns != null)
                {
                    nReadSize = ns.Read(nbytes, 0, 512);
                    while (nReadSize > 0)
                    {
                        fs.Write(nbytes, 0, nReadSize);
                        nReadSize = ns.Read(nbytes, 0, 512);
                    }
                    fs.Close();
                    ns.Close();
                }
                // 将文件复制到指定文件夹
                File.Copy(strLocalPathTemp, savePath);
                // 删除临时文件
                File.Delete(strLocalPathTemp);
            }
            catch (Exception)
            {
                fs.Close();
                // 设定下载结果为false.
                bDownload = false;
            }

            // 返回下载结果.
            return bDownload;
        }

        /// <summary>
        ///  压缩下载
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="loadPath">下载路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="flag">是否备份文件，默认备份名，原文件+.back</param>
        /// <returns></returns>
        public static void DownloadGip(string savePath, string loadPath, string fileName = null, bool flag = true)
        {
            // 自定义文件名为空的情况
            if (string.IsNullOrEmpty(fileName))
            {
                // 取得下载文件名
                fileName = Path.GetFileName(loadPath);
            }
            savePath = savePath + fileName;
            // 判断保存路径是否存在，备份原文件
            JustFileAndDic(savePath, true);
            FileStream fs;
            MemoryStream ms;
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.Headers.Add("Accept-Encoding", "gzip,deflate");
                    byte[] byteArray = client.DownloadData(loadPath);
                    // 处理　gzip 
                    string sContentEncoding = client.ResponseHeaders["Content-Encoding"];
                    if (sContentEncoding == "gzip")
                    {
                        ms = new MemoryStream(byteArray);
                        fs = new FileStream(savePath, FileMode.Create);
                        int count = 0;
                        // 解压
                        GZipStream gzip = new GZipStream(ms, CompressionMode.Decompress);
                        byte[] buf = new byte[512];
                        while ((count = gzip.Read(buf, 0, buf.Length)) > 0)
                        {
                            fs.Write(buf, 0, count);
                        }
                        fs.Close();
                        ms.Close();
                    }
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch
                {
                }
            }
        }

        #region 方法

        #region 创建文件夹

        /// <summary>
        ///  文件夹不存在，创建文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        public static void JustFileAndDic(string path)
        {
            try
            {
                string folderPath = path.Substring(0, path.LastIndexOf("\\", StringComparison.Ordinal));
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }
        }

        /// <summary>
        ///  文件夹不存在，创建文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="flag">备份文件</param>
        public static void JustFileAndDic(string path, bool flag)
        {
            try
            {
                string folderPath = path.Substring(0, path.LastIndexOf("\\", StringComparison.Ordinal));
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                if (flag)
                {
                    BackUpFile(path);
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }
        }

        #endregion

        #region 备份文件

        /// <summary>
        ///   备份删除文件（.back）
        /// </summary>
        /// <param name="savePath">保存路径</param>
        public static void BackUpFile(string savePath)
        {
            if (File.Exists(savePath))
            {
                // 覆盖文件
                File.Copy(savePath, $"{savePath}.back", true);
                File.Delete(savePath);
            }
        }

        #endregion

        /// <summary>
        ///  总是接受
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

        #endregion

    }
}
