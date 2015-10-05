/*-------------------------------------------------------------------------
 * 版权所有：自动生成，请勿手动修改
 * 作者：zengteng
 * 操作：创建
 * 操作时间：2015/1/26 21:57:13
 * 版本号：v1.0
 *  -------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

//using HungryWolf.DBUtility;
//using HungryWolf.Core;
//using HungryWolf.Core.Attrbutes;
using BaoXin.SqlHelper;
using BaoXin.Entity;

namespace BaoXin.DAL
{
    /// <summary>
    /// 数据访问类
    /// </summary>
    public partial class UsersFriendDAL
    {
        public static List<UsersFriendEntity> GetUsersFriendListByUserId(Guid userId, SqlTransaction tran)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SqlServerHelper.CreateInputParameter("@userId", SqlDbType.UniqueIdentifier, userId));
            // 根据编号获取对象
            string sql = @"SELECT *
     FROM UsersFriend 
    WHERE UserId=@userId";
            if (tran == null)
            {
                return GetBindTDataList(SqlServerHelper.ExecuteReader(CommandType.Text, sql, parameters.ToArray()));
            }

            return GetBindTDataList(SqlServerHelper.ExecuteReader(tran, CommandType.Text, sql, parameters.ToArray()));

        }

        /// <summary>
        /// 检查好友已经存在：
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static object ExistFriend(Guid UserId, Guid FriendId, SqlTransaction tran)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SqlServerHelper.CreateInputParameter("@UserId", SqlDbType.UniqueIdentifier, UserId));
            parameters.Add(SqlServerHelper.CreateInputParameter("@FriendId", SqlDbType.UniqueIdentifier, FriendId));

            // 根据编号获取对象
            string sql = @"select COUNT(*) from UsersFriend 
where UserId=@UserId and FriendId=@FriendId";
            if (tran == null)
            {
                return SqlServerHelper.ExecuteScalar(CommandType.Text, sql, parameters.ToArray());
            }
            return SqlServerHelper.ExecuteScalar(tran, CommandType.Text, sql, parameters.ToArray());

        }
    }


}
