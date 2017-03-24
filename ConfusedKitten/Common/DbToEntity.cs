using System;
using System.Data;
using System.Globalization;
using System.IO;
using ConfusedKitten.DataBase;
using ConfusedKitten.Model;

namespace ConfusedKitten.Common
{
    public static class DbToEntity
    {
        /// <summary>
        ///  创建实体方法
        /// </summary>
        /// <param name="path">实体保存路径</param>
        /// <param name="baseEntity">实体类基本信息</param>
        /// <param name="tableName">表名</param>
        public static void CreateEntity(string path, BaseEntity baseEntity, string tableName = null)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = baseEntity.ClassName;
            }
            MysqlHelper mysqlHelper = new MysqlHelper();
            string query = $"show full fields from {tableName}";
            DataTable dt = mysqlHelper.GetDataTable(query);
            Write(fs, dt, baseEntity);
        }

        /// <summary>
        ///  写实体类文件
        /// </summary>
        /// <param name="fs">文件流</param>
        /// <param name="dt">表</param>
        /// <param name="baseEntity">实体基本信息</param>
        /// <returns>保存结果</returns>
        public static bool Write(FileStream fs, DataTable dt, BaseEntity baseEntity)
        {
            using (fs)
            {
                //向流中写入字符  
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    //写入类的头部信息  
                    sw.WriteLine("/*\n" + "*作者：" + baseEntity.ClassAuthor + "\n" + "*创建时间：" +
                                 DateTime.Now.ToString(CultureInfo.InvariantCulture) + " \n" + "*类说明：" +
                                 baseEntity.ClassExplain + "\n" + "*/");
                    //判断命名空间  
                    if (baseEntity.NameSpace != "")
                    {
                        //写入命名空间  
                        sw.WriteLine("namespace " + baseEntity.NameSpace + "\n{");
                        //写入类说明  
                        sw.WriteLine("    /// <summary>\n" + "    /// " + baseEntity.ClassExplain +
                                     "\n    /// </summary>");
                        //写类的定义  
                        sw.WriteLine("    public class " + baseEntity.ClassName);
                        sw.WriteLine("    {");
                        //写属性  
                        for (int i = 0; i < dt.Rows.Count - 1; i++)
                        {
                            sw.WriteLine("       /// <summary>\n" + "       /// " + dt.Rows[i]["Comment"] + "\n" +
                                         "       /// </summary>\n" +
                                         "       public string " + dt.Rows[i][0] + " { get ; set; }\n"); 
                        }
                        sw.Write("    }\n");
                        sw.WriteLine("}");
                    }
                    return true;
                }
            }
        }
    }
}
