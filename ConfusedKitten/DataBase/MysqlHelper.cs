using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace ConfusedKitten.DataBase
{
    /// <summary>
    ///  mysql相关方法类
    /// </summary>
    public class MysqlHelper
    {
        /// <summary>
        ///  数据库连接字符串
        /// </summary>
        private static string _connectionString;

        /// <summary>
        ///  初始化sql连接字符串
        /// </summary>
        public MysqlHelper()
        {
        }

        /// <summary>
        ///  初始化sql连接字符串
        /// </summary>
        /// <param name="connectionString">sql连接字符串</param>
        public MysqlHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        ///  （无参）返回执行的行数(删除修改更新)  
        /// </summary>
        /// <param name="safeSql">待执行sql文</param>
        /// <returns>返回执行的行数</returns>
        public int ExecuteCommand(string safeSql)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(safeSql, connection))
                {
                    cmd.CommandTimeout = 6000;
                    try
                    {
                        connection.Open();
                        int result = cmd.ExecuteNonQuery();

                        return result;
                    }
                    catch (MySqlException ex)
                    {
                        connection.Close();
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        ///  返回执行的行数(删除修改更新)  
        /// </summary>
        /// <param name="safeSql">待执行sql文</param>
        /// <param name="newConnection">数据库连接字符串</param>
        /// <returns>返回执行的行数</returns>
        public int ExecuteCommand(string safeSql, string newConnection)
        {
            var getConnectio = string.IsNullOrEmpty(newConnection) ? _connectionString : newConnection;

            using (MySqlConnection connection = new MySqlConnection(getConnectio))
            {
                using (MySqlCommand cmd = new MySqlCommand(safeSql, connection))
                {
                    cmd.CommandTimeout = 6000;
                    try
                    {
                        connection.Open();
                        int result = cmd.ExecuteNonQuery();

                        return result;
                    }
                    catch (MySqlException ex)
                    {
                        connection.Close();
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        ///  返回执行的行数(删除修改更新)  (含参数)
        /// </summary>
        /// <param name="sql">sql文</param>
        /// <param name="values">参数集合</param>
        /// <returns>返回执行的行数</returns>
        public int ExecuteCommand(string sql, params MySqlParameter[] values)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.Parameters.Add(values);
                        return cmd.ExecuteNonQuery();
                    }
                    catch (MySqlException ex)
                    {
                        connection.Close();
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        ///  查询
        /// </summary>
        /// <param name="safeSql">sql文</param>
        /// <returns>查询结果</returns>
        public DataTable GetDataTable(string safeSql)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(safeSql, connection))
                {
                    try
                    {
                        connection.Open();
                        DataSet ds = new DataSet();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(ds);
                        return ds.Tables[0];
                    }
                    catch (MySqlException ex)
                    {
                        connection.Close();
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        ///  查询
        /// </summary>
        /// <param name="safeSql">sql文</param>
        /// <param name="newConnection">新数据库连接字符串</param>
        /// <returns>查询结果</returns>
        public DataTable GetDataTable(string safeSql, string newConnection)
        {
            var getConnectio = string.IsNullOrEmpty(newConnection) ? _connectionString : newConnection;

            using (MySqlConnection connection = new MySqlConnection(getConnectio))
            {
                using (MySqlCommand cmd = new MySqlCommand(safeSql, connection))
                {
                    try
                    {
                        connection.Open();
                        DataTable dt = new DataTable();
                        //DataSet ds = new DataSet();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                        return dt;
                    }
                    catch (MySqlException ex)
                    {
                        connection.Close();
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        ///  查询（带参数）
        /// </summary>
        /// <param name="sql">sql文</param>
        /// <param name="values">参数集合</param>
        /// <returns>查询结果</returns>
        public DataTable GetDataTable(string sql, params MySqlParameter[] values)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        DataSet ds = new DataSet();
                        cmd.Parameters.Add(values);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(ds);
                        return ds.Tables[0];
                    }
                    catch (MySqlException ex)
                    {
                        connection.Close();
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行事务，并返回本次执行的所有sql语句影响数据改变行数
        /// </summary>
        /// <param name="sqlStringList"></param>
        /// <returns></returns>
        public int ExecuteTransaction(List<string> sqlStringList)
        {
            var result = 0;
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand { Connection = conn };
                MySqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    foreach (var strsql in sqlStringList)
                    {
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            result += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (Exception)
                {
                    tx.Rollback();
                    result = -1;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
                return result;
            }
        }
    }
}
