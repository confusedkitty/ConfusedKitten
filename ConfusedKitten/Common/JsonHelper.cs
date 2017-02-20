using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;

namespace ConfusedKitten.Common
{
    /// <summary>
    /// Json与对象转换
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        ///  将对象转换成json
        /// </summary>
        /// <param name="obj">待转换对象</param>
        /// <returns>转换结果</returns>
        public static string ToJson(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///  将json转换成对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="str">待转换字符串</param>
        /// <returns>转换结果</returns>
        public static T ToObject<T>(string str)
        {
            try
            {
                return (T)JsonConvert.DeserializeObject(str, typeof(T));
            }
            catch
            {
                return (T)System.Activator.CreateInstance(typeof(T));
                // T t = (T)System.Activator.CreateInstance(typeof(T), args); 创建带参数的实例
            }
        }

        /// <summary>
        /// 对象转换成json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonObject">需要格式化的对象</param>
        /// <returns>Json字符串</returns>
        public static string DataContractJsonSerialize<T>(T jsonObject)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream()) //定义一个stream用来存发序列化之后的内容
                {
                    serializer.WriteObject(ms, jsonObject);
                    var json = Encoding.UTF8.GetString(ms.GetBuffer());
                    return json;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// json字符串转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">要转换成对象的json字符串</param>
        /// <returns></returns>
        public static T DataContractJsonDeserialize<T>(string json)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    var obj = (T) serializer.ReadObject(ms);
                    return obj;
                }
            }
            catch
            {
                return (T)System.Activator.CreateInstance(typeof(T));
            }
        }
    }
}