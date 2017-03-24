namespace ConfusedKitten.Model
{
    /// <summary>
    ///  实体基本信息类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>  
        /// 类的名称  
        /// </summary>  
        public string ClassName { get; set; }

        /// <summary>  
        /// 类的说明  
        /// </summary>  
        public string ClassExplain { get; set; }

        /// <summary>  
        /// 类的创建者  
        /// </summary>  
        public string ClassAuthor { get; set; }

        /// <summary>  
        /// 类的命名空间  
        /// </summary>  
        public string NameSpace { get; set; }
    }
}
