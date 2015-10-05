using BaoXin.DAL;
using BaoXin.Entity;
using BaoXin.Entity.Result;
using BaoXin.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BaoXin.DAL
{
    public partial class UsersDAL
    {
        /// <summary>
        /// 根据Email获取实体
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="tran">事务</param>
        /// <returns>对象实体</returns>
        public static UsersEntity GetUsersEntityByEmail(string email, SqlTransaction tran)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SqlServerHelper.CreateInputParameter("@email", SqlDbType.NVarChar, email));
            // 根据编号获取对象
            string sql = @"SELECT TOP 1 * 
                                                      FROM Users 
    WHERE [Email]=@email";
            if (tran == null)
            {
                return GetBindTData(SqlServerHelper.ExecuteReader(CommandType.Text, sql, parameters.ToArray()));
            }

            return GetBindTData(SqlServerHelper.ExecuteReader(tran, CommandType.Text, sql, parameters.ToArray()));
        }


        public static UsersEntity GetUsersEntityByEmail1(string email, SqlTransaction tran)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SqlServerHelper.CreateInputParameter("@email", SqlDbType.NVarChar, email));
            // 根据编号获取对象
            string sql = @"SELECT TOP 1 * 
                                                      FROM Users 
    WHERE [Email]=@email and isadmin=1";
            if (tran == null)
            {
                return GetBindTData(SqlServerHelper.ExecuteReader(CommandType.Text, sql, parameters.ToArray()));
            }

            return GetBindTData(SqlServerHelper.ExecuteReader(tran, CommandType.Text, sql, parameters.ToArray()));
        }
        /// <summary>
        /// 检查昵称是否重复：
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static object GetListByNickName(string nickName, SqlTransaction tran)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SqlServerHelper.CreateInputParameter("@nickName", SqlDbType.NVarChar, nickName));
            // 根据编号获取对象
            string sql = @"SELECT count(*)
                                                      FROM Users 
    WHERE NickName=@nickName";
            if (tran == null)
            {
                return SqlServerHelper.ExecuteScalar(CommandType.Text, sql, parameters.ToArray());
            }

            return SqlServerHelper.ExecuteScalar(tran, CommandType.Text, sql, parameters.ToArray());

        }
        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static List<UsersEntity> GetFriendUsersListByUserId(Guid userId, SqlTransaction tran)
        {
            List<UsersEntity> onlineusers = new List<UsersEntity>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SqlServerHelper.CreateInputParameter("@userId", SqlDbType.UniqueIdentifier, userId));
            // 根据编号获取对象
            string sql = @"select * from  Users ";
            var onlines = GetBindTDataList(SqlServerHelper.ExecuteReader(CommandType.Text, sql, parameters.ToArray()));
            foreach (var on in onlines)
            {
                if (onlineusers.FirstOrDefault(s => s.Id == on.Id) == null)
                {
                    var sqlhs = "SELECT  * FROM ChatInfo WHERE user_from='"+on.Id+"' AND user_to='"+userId+"' and IsOpen=0";
                    //Console.WriteLine(sqlhs);
                    var da = SqlServerHelper.ReadTable(CommandType.Text, sqlhs);
                    if (da.Rows.Count > 0)
                    {
                        on.IsHasNews = true;
                    }
                    else {
                        on.IsHasNews = false;
                    }

                    onlineusers.Add(on);
                }
            }
            return onlineusers;
            //if (tran == null)
            //{
            //    return GetBindTDataList(SqlServerHelper.ExecuteReader(CommandType.Text, sql, parameters.ToArray()));
            //}

            //return GetBindTDataList(SqlServerHelper.ExecuteReader(tran, CommandType.Text, sql, parameters.ToArray()));

        }

        public static Users GetUserById(Guid uid)

        {
            var edm = new BaoXinEntities();
           return edm.Users.FirstOrDefault(s => s.Id == uid);
        }

        public static List<Users> LoadOnlineUser() {
            var edm = new BaoXinEntities();
            return edm.Users.Where(s => s.IsOnline == 1).ToList();
        }

    }
}
