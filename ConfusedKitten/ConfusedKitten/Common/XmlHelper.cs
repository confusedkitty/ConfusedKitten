using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ConfusedKitten.Common
{
    public static class XmlHelper
    {
        /// <summary>
        ///  序列化方法
        /// </summary>
        /// <typeparam name="T">待序列化类</typeparam>
        /// <param name="t">序列化实体</param>
        /// <param name="strFileName">文件路径</param>
        /// <returns>序列化结果</returns>
        public static void XmlSerializerFileMethod<T>(T t, string strFileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;// 缩进换行
            using (XmlWriter sw = XmlWriter.Create(strFileName, settings))
            {
                //创建XML命名空间
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                serializer.Serialize(sw, t, ns);
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="xmlFile">XML文件路径</param>
        /// <returns></returns>
        public static object DeserializeFile<T>(string xmlFile)
        {
            try
            {
                using (StreamReader sr = new StreamReader(xmlFile))
                {
                    XmlSerializer xmldes = new XmlSerializer(typeof(T));
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
