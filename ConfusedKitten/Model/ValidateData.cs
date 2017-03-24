using System;
using System.Reflection;

namespace ConfusedKitten.Model
{
    /// <summary>
    ///  数据验证方法
    /// </summary>
    public static class ValidateData
    {
        /// <summary>
        ///  验证结果
        /// </summary>
        /// <param name="entityObject">待验证实体</param>
        /// <returns>验证结果</returns>
        public static string ValidateResult(object entityObject)
        {
            if (entityObject == null) return "对象为空";
            Type type = entityObject.GetType();
            PropertyInfo[] properties = type.GetProperties();
            string validateResult = string.Empty;
            foreach (PropertyInfo property in properties)
            {
                #region 验证
                 
                validateResult = string.Empty;
                //获取验证特性
                object[] validateContent = property.GetCustomAttributes(typeof(ValidateAttribute), true);
                {
                    //获取属性的值 
                    object value = property.GetValue(entityObject, null);
                    foreach (ValidateAttribute validateAttribute in validateContent)
                    {
                        string message = validateAttribute.Message;
                        bool mesFlag = string.IsNullOrEmpty(message);
                        string proName = property.Name;

                        #region 验证方法

                        switch (validateAttribute.ValidateType)
                        {
                            #region 不为空验证

                            //验证元素是否为空字串
                            case ValidateType.IsEmpty:
                                if (null == value || value.ToString().Length < 1)
                                {
                                    validateResult = mesFlag ? $"元素 {proName} 不能为空" : message;
                                }
                                break;

                            #endregion

                            #region 最小长度验证

                            //验证元素的长度是否小于指定最小长度
                            case ValidateType.MinLength:
                                if (null == value || value.ToString().Length < 1) break;
                                if (value.ToString().Length < validateAttribute.MinLength)
                                {
                                    validateResult = mesFlag ? $"元素 {proName} 的长度不能小于{validateAttribute.MinLength}" : message;
                                }
                                break;

                            #endregion

                            #region 最大长度验证

                            //验证元素的长度是否大于指定最大长度
                            case ValidateType.MaxLength:
                                if (null == value || value.ToString().Length < 1) break;
                                if (value.ToString().Length > validateAttribute.MaxLength)
                                {
                                    validateResult = mesFlag ? $"元素 {proName} 的长度不能大于{validateAttribute.MinLength}" : message;
                                }
                                break;

                            #endregion

                            #region 长度范围验证

                            //验证元素的长度是否符合指定的最大长度和最小长度的范围
                            case ValidateType.MinLength | ValidateType.MaxLength:
                                if (null == value || value.ToString().Length < 1) break;
                                if (value.ToString().Length > validateAttribute.MaxLength ||
                                    value.ToString().Length < validateAttribute.MinLength)
                                {
                                    validateResult = mesFlag ? $"元素 {proName} 不符合指定的最小长度和最大长度的范围,应该在{validateAttribute.MinLength}与{validateAttribute.MaxLength}之间" : message;
                                }
                                break;

                            #endregion

                            #region 数字验证

                            //验证元素的值是否为值类型
                            case ValidateType.IsNumber:
                                if (null == value || value.ToString().Length < 1) break;
                                if (!System.Text.RegularExpressions.Regex.IsMatch(value.ToString(), @"^\d+$"))
                                {
                                    validateResult = mesFlag ? $"元素 {proName} 的值不是值类型" : message;
                                }
                                break;

                            #endregion

                            #region 时间格式验证

                            //验证元素的值是否为正确的时间格式
                            case ValidateType.IsDateTime:
                                if (null == value || value.ToString().Length < 1) break;
                                if (
                                    !System.Text.RegularExpressions.Regex.IsMatch(value.ToString(),
                                        @"(\d{2,4})[-/]?([0]?[1-9]|[1][12])[-/]?([0][1-9] |[12]\d|[3][01])\s*([01]\d|[2][0-4])?[:]?([012345]?\d)?[:]?([012345]? \d)?"))
                                {
                                    validateResult = mesFlag ? $"元素 {proName} 不是正确的时间格式" : message;
                                }
                                break;

                            #endregion

                            #region 金额验证

                            //验证元素的值是否为正确的浮点格式
                            case ValidateType.IsDecimal:
                                if (null == value || value.ToString().Length < 1) break;
                                if (!System.Text.RegularExpressions.Regex.IsMatch(value.ToString(), @"^\d+[.]?\d+$"))
                                {
                                    validateResult = mesFlag ? $"元素 {proName} 不是正确的金额格式" : message;
                                }
                                break;

                                #endregion
                        }
                        #endregion
                    }
                }
                if (!string.IsNullOrEmpty(validateResult))
                    break;

                #endregion
            }
            return validateResult;
        }
    }
}
