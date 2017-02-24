using System.Linq;
using System.Reflection;

namespace ConfusedKitten.Common
{
    public static class ModelHelper
    {
        /// <summary>
        ///  将实体A的属性赋值给实体B （属性相同赋值，属性不区分大小写）
        /// </summary>
        /// <typeparam name="T1">类型（数据源）</typeparam>
        /// <typeparam name="T2">类型（赋值实体）</typeparam>
        /// <param name="tFrom">数据源</param>
        /// <param name="tTo">赋值实体</param>
        /// <returns></returns>
        public static T2 ExchangeClass<T1, T2>(T1 tFrom, T2 tTo)
        {
            // 取得属性集合
            PropertyInfo[] proFrom = tFrom.GetType().GetProperties();
            PropertyInfo[] proTo = tTo.GetType().GetProperties();

            try
            {
                // 遍历数据源属性
                foreach (PropertyInfo info in proFrom)
                {
                    // 获取属性值
                    string proValue = info.GetValue(tFrom, null)?.ToString();
                    if (string.IsNullOrEmpty(proValue))
                    {
                        continue;
                    }
                    // 取得赋值实体相同名称属性(不区分大小写)
                    PropertyInfo toTemp = proTo.FirstOrDefault(p => p.Name.ToLower().Equals(info.Name.ToLower()));
                    if (toTemp != null)
                    {
                        // 设置新实体属性值
                        toTemp.SetValue(tTo, proValue, null);
                    }
                }
                return tTo;
            }
            catch
            {
                // 返回新实例
                return tTo;
            }
        }
    }
}
