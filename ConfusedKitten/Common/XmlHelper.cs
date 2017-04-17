using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ConfusedKitten.Model;

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
        public static void XmlSerializerFileMethod<T>(T t, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;// 缩进换行
            using (XmlWriter sw = XmlWriter.Create(path, settings))
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

        /// <summary>
        ///  取得指定xml节点的值
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="section">区域</param>
        /// <param name="name">节点名称</param>
        /// <returns></returns>
        public static string FetchNode(string path, string section, string name, string attribute = null)
        {
            //使用XmlDocument读取XML
            XmlDocument xdoc = new XmlDocument();
            if (!File.Exists(path)) return null;
            //相对路径
            xdoc.Load(path);
            XmlNode node = xdoc.SelectSingleNode("Sections")?.SelectSingleNode(section);
            if (node == null) return null;
            if (string.IsNullOrEmpty(attribute)) return node.SelectSingleNode(name).InnerText;
            return node.SelectSingleNode(name).Attributes[attribute].Value;
        }
    }
}
