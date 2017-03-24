using System;

namespace ConfusedKitten.Model
{
    /// <summary>
    ///  为元素添加验证信息的特性类
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ValidateAttribute : Attribute
    {
        /// <summary>
        ///  验证类型
        /// </summary>
        private ValidateType _validateType;

        /// <summary>
        ///  最小长度
        /// </summary>
        private int _minLength;

        /// <summary>
        ///  最大长度
        /// </summary>
        private int _maxLength;

        /// <summary>
        /// 自定义数据源　
        /// </summary>
        private string[] _customArray;

        /// <summary>
        ///  消息
        /// </summary>
        private string _message;

        /// <summary>
        ///  提示信息
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// 验证类型
        /// </summary>
        public ValidateType ValidateType
        {
            get { return _validateType; }
        }

        /// <summary>
        ///  最小长度
        /// </summary>
        public int MinLength
        {
            get { return _minLength; }
            set { _minLength = value; }
        }

        /// <summary>
        ///  最大长度
        /// </summary>
        public int MaxLength
        {
            get { return _maxLength; }
            set { _maxLength = value; }
        }

        /// <summary>
        ///  自定义数据源
        /// </summary>
        public string[] CustomArray
        {
            get { return _customArray; }
            set { _customArray = value; }
        }

        /// <summary>
        ///  指定采取何种验证方式来验证元素的有效性
        /// </summary>
        /// <param name="validateType"></param>
        public ValidateAttribute(ValidateType validateType)
        {
            _validateType = validateType;
        }
    }
}
