using System.Net;
using System.Text;

namespace ConfusedKitten.MyWebRequest
{
    public static class RequestMethod
    {
        /// <summary>
        ///  post提交方法
        /// </summary>
        /// <param name="url">路径</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static string PostMethod(string url, string data)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    //编码，尤其是汉字，事先要看下抓取网页的编码方式  
                    byte[] postData = Encoding.GetEncoding("UTF-8").GetBytes(data);
                    //采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    //得到返回字符流  
                    byte[] responseData = webClient.UploadData(url, "POST", postData);
                    //解码 
                    string srcString = Encoding.GetEncoding("UTF-8").GetString(responseData);
                    return srcString;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///  get提交方法
        /// </summary>
        /// <param name="url">路径</param>
        /// <returns></returns>
        public static string GetMethod(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    webClient.Encoding = Encoding.UTF8;
                    string srcString = webClient.DownloadString(url);
                    return srcString;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
