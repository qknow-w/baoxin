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
	public partial class  UsersFriendDAL
    {
		#region SQL
        
		// 根据ID添加
		private const string SqlInsert = @"INSERT INTO UsersFriend 
                                                  (

                                                    UserId, 
                                                    FriendId, 
                                                    AddTime, 
                                                    IsDel)   
                                           VALUES(

                                                    @UserId,
                                                    @FriendId,
                                                    @AddTime,
                                                    @IsDel)";
                                                    
        // 根据ID添加返回ID
		private const string SqlInsertReturnID = @"DECLARE @myid uniqueidentifier
                                           SET @myid = NEWID()
                                           INSERT INTO UsersFriend 
                                                  (ID, 

                                                    UserId, 
                                                    FriendId, 
                                                    AddTime, 
                                                    IsDel)   
                                           VALUES(@myid, 

                                                    @UserId,
                                                    @FriendId,
                                                    @AddTime,
                                                    @IsDel) ;SELECT @myid";
		
        // 根据ID更新
		private const string SqlUpdate = @"Update TOP(1) UsersFriend 
                                           SET 

                                               UserId = @UserId,
                                               FriendId = @FriendId,
                                               AddTime = @AddTime,
                                               IsDel = @IsDel
                                           WHERE [id] = @id
										  ";
		
        
		// 删除SQL
		private const string SqlDelete = @"DELETE TOP(1) FROM UsersFriend WHERE [id]=@id";
		
		// 根据编号获取对象
		private const string SqlUsersFriendEntityByID = @"SELECT TOP 1 * 
                                                      FROM UsersFriend 
    WHERE [id]=@id";
		
		// 获取全部对象
		private const string SqlGetAllUsersFriendList = @"SELECT * 
                                                      FROM UsersFriend";
		
		#endregion
		
		#region 公有方法
		
		/// <summary>
        /// 创建
        /// </summary>
        /// <param name="usersFriendEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public static bool Insert( UsersFriendEntity usersFriendEntity , SqlTransaction tran)
		{			
            List<SqlParameter> parameters = new List<SqlParameter>();
			
                parameters.Add(SqlServerHelper.CreateInputParameter("@UserId", SqlDbType.UniqueIdentifier, usersFriendEntity.UserId));
                parameters.Add(SqlServerHelper.CreateInputParameter("@FriendId", SqlDbType.UniqueIdentifier, usersFriendEntity.FriendId));
                parameters.Add(SqlServerHelper.CreateInputParameter("@AddTime", SqlDbType.DateTime, usersFriendEntity.AddTime));
                parameters.Add(SqlServerHelper.CreateInputParameter("@IsDel", SqlDbType.Bit, usersFriendEntity.IsDel));
			
            if (tran == null)
            {
                SqlServerHelper.ExecuteNonQuery(CommandType.Text, SqlInsert, parameters.ToArray());
                return true;
            }
            
            SqlServerHelper.ExecuteNonQuery(tran, CommandType.Text, SqlInsert, parameters.ToArray());
            return true;
		}
        
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="usersFriendEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public static Guid InsertReturnID( UsersFriendEntity usersFriendEntity , SqlTransaction tran)
		{
            List<SqlParameter> parameters = new List<SqlParameter>();
			
                parameters.Add(SqlServerHelper.CreateInputParameter("@UserId", SqlDbType.UniqueIdentifier, usersFriendEntity.UserId));
                parameters.Add(SqlServerHelper.CreateInputParameter("@FriendId", SqlDbType.UniqueIdentifier, usersFriendEntity.FriendId));
                parameters.Add(SqlServerHelper.CreateInputParameter("@AddTime", SqlDbType.DateTime, usersFriendEntity.AddTime));
                parameters.Add(SqlServerHelper.CreateInputParameter("@IsDel", SqlDbType.Bit, usersFriendEntity.IsDel));
			
            Guid result = Guid.Empty;
            
            if (tran == null)
            {
                Guid.TryParse(SqlServerHelper.ExecuteScalar(CommandType.Text, SqlInsertReturnID, parameters.ToArray()).ToString(), out result);
            }
            else
            {
                Guid.TryParse(SqlServerHelper.ExecuteScalar(tran, CommandType.Text, SqlInsertReturnID, parameters.ToArray()).ToString(), out result);
            }

            return result;
		}
		
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="usersFriendEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public static bool Update( UsersFriendEntity usersFriendEntity , SqlTransaction tran)
		{
            List<SqlParameter> parameters = new List<SqlParameter>();
			
                parameters.Add(SqlServerHelper.CreateInputParameter("@Id", SqlDbType.UniqueIdentifier, usersFriendEntity.Id));
                parameters.Add(SqlServerHelper.CreateInputParameter("@UserId", SqlDbType.UniqueIdentifier, usersFriendEntity.UserId));
                parameters.Add(SqlServerHelper.CreateInputParameter("@FriendId", SqlDbType.UniqueIdentifier, usersFriendEntity.FriendId));
                parameters.Add(SqlServerHelper.CreateInputParameter("@AddTime", SqlDbType.DateTime, usersFriendEntity.AddTime));
                parameters.Add(SqlServerHelper.CreateInputParameter("@IsDel", SqlDbType.Bit, usersFriendEntity.IsDel));
			
            if (tran == null)
            {
                SqlServerHelper.ExecuteNonQuery(CommandType.Text, SqlUpdate, parameters.ToArray());
                return true;
            }
			
            SqlServerHelper.ExecuteNonQuery(tran, CommandType.Text, SqlUpdate, parameters.ToArray());
            return true;
		}

		/// <summary>
        /// 根据编号删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="tran">事务</param>
		  /// <returns>是否成功</returns>
        public static bool Delete(Guid id, SqlTransaction tran)
		{
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SqlServerHelper.CreateInputParameter("@id", SqlDbType.UniqueIdentifier, id));
			
			if (tran == null)
            {
                SqlServerHelper.ExecuteNonQuery(CommandType.Text, SqlDelete, parameters.ToArray());
                return true;
            }
            
            SqlServerHelper.ExecuteNonQuery(tran, CommandType.Text, SqlDelete, parameters.ToArray());
            return true;
		}
		
		/// <summary>
        /// 根据编号获取实体
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="tran">事务</param>
		/// <returns>对象实体</returns>
		public static  UsersFriendEntity GetUsersFriendEntityByID(Guid id, SqlTransaction tran)
		{
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SqlServerHelper.CreateInputParameter("@id", SqlDbType.UniqueIdentifier, id));

            if (tran == null)
            {
                return GetBindTData(SqlServerHelper.ExecuteReader(CommandType.Text, SqlUsersFriendEntityByID, parameters.ToArray()));
            }

            return GetBindTData(SqlServerHelper.ExecuteReader(tran, CommandType.Text, SqlUsersFriendEntityByID, parameters.ToArray()));
		}
		
		/// <summary>
        /// 获取全部对象
        /// </summary>
        /// <param name="tran">事务</param>
		/// <returns>对象实体</returns>
		public static List< UsersFriendEntity> GetAllUsersFriendList(SqlTransaction tran)
		{
            if (tran == null)
            {
			    return GetBindTDataList(SqlServerHelper.ExecuteReader(CommandType.Text, SqlGetAllUsersFriendList, null));
            }

            return  GetBindTDataList(SqlServerHelper.ExecuteReader(tran, CommandType.Text, SqlGetAllUsersFriendList, null));
		}
		
		#endregion
		
		#region 私有方法

        /// <summary>
        /// 实体集合Reader
        /// </summary>
        /// <returns>实体集合</returns>
        protected static List< UsersFriendEntity> GetBindTDataList(SqlDataReader dataReader)
        {
            
            List< UsersFriendEntity> usersFriendList = new List< UsersFriendEntity>();


            while (dataReader.Read())
            {
                 UsersFriendEntity usersFriendEntity = new  UsersFriendEntity();
		
                if (dataReader["Id"] != DBNull.Value)
                {
                usersFriendEntity.Id = new Guid(dataReader["Id"].ToString());
                }
                if (dataReader["UserId"] != DBNull.Value)
                {
                usersFriendEntity.UserId = new Guid(dataReader["UserId"].ToString());
                }
                if (dataReader["FriendId"] != DBNull.Value)
                {
                usersFriendEntity.FriendId = new Guid(dataReader["FriendId"].ToString());
                }
                if (dataReader["AddTime"] != DBNull.Value)
                {
                usersFriendEntity.AddTime = Convert.ToDateTime(dataReader["AddTime"]);
                }
                if (dataReader["IsDel"] != DBNull.Value)
                {
                usersFriendEntity.IsDel = Convert.ToBoolean(dataReader["IsDel"]);
                }
                
                usersFriendList.Add(usersFriendEntity);
            }
            
            dataReader.Close();
            dataReader.Dispose();

            return usersFriendList;

        }
		
		/// <summary>
        /// 实体Reader
        /// </summary>
        /// <returns>实体对象</returns>
        protected static UsersFriendEntity GetBindTData(SqlDataReader dataReader)
		{

             UsersFriendEntity usersFriendEntity = null;
            
            while (dataReader.Read())
            {
                usersFriendEntity = new  UsersFriendEntity();
		
                if (dataReader["Id"] != DBNull.Value)
                {
                usersFriendEntity.Id = new Guid(dataReader["Id"].ToString());
                }
                if (dataReader["UserId"] != DBNull.Value)
                {
                usersFriendEntity.UserId = new Guid(dataReader["UserId"].ToString());
                }
                if (dataReader["FriendId"] != DBNull.Value)
                {
                usersFriendEntity.FriendId = new Guid(dataReader["FriendId"].ToString());
                }
                if (dataReader["AddTime"] != DBNull.Value)
                {
                usersFriendEntity.AddTime = Convert.ToDateTime(dataReader["AddTime"]);
                }
                if (dataReader["IsDel"] != DBNull.Value)
                {
                usersFriendEntity.IsDel = Convert.ToBoolean(dataReader["IsDel"]);
                }

            }
            
            dataReader.Close();
            dataReader.Dispose();

            return usersFriendEntity;
		}
		
		#endregion
	}
}
