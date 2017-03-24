namespace ConfusedKitten.DataBase
{
    public static class Dao
    {
        /// <summary>
        ///  返回数据库实例
        /// </summary>
        /// <param name="connect">连接字符串 </param>
        /// <returns>数据库实例</returns>
        public static MysqlHelper Create(string connect)
        {
            return new MysqlHelper(connect);
        }
    }
}
