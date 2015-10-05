/*-------------------------------------------------------------------------
 * 版权所有：自动生成，请勿手动修改
 * 作者：zengteng
 * 操作：创建
 * 操作时间：2015/1/24 16:30:58
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
	public partial class  UsersDAL
    {
		#region SQL
        
		// 根据ID添加
		private const string SqlInsert = @"INSERT INTO Users 
                                                  (

                                                    Email, 
                                                    NickName, 
                                                    Password, 
                                                    Contactqq, 
                                                    Contactaddr, 
                                                    UserName, 
                                                    Mobile, 
                                                    avatar, 
                                                    RankCredits, 
                                                    IsOnline, 
                                                    AddTime, 
                                                    HeadImage, 
                                                    HeadSmallImage)   
                                           VALUES(

                                                    @Email,
                                                    @NickName,
                                                    @Password,
                                                    @Contactqq,
                                                    @Contactaddr,
                                                    @UserName,
                                                    @Mobile,
                                                    @avatar,
                                                    @RankCredits,
                                                    @IsOnline,
                                                    @AddTime,
                                                    @HeadImage,
                                                    @HeadSmallImage)";
                                                    
        // 根据ID添加返回ID
		private const string SqlInsertReturnID = @"DECLARE @myid uniqueidentifier
                                           SET @myid = NEWID()
                                           INSERT INTO Users 
                                                  (ID, 
                                                    Email, 
                                                    NickName, 
                                                    Password, 
                                                    Contactqq, 
                                                    Contactaddr, 
                                                    UserName, 
                                                    Mobile, 
                                                    avatar, 
                                                    RankCredits, 
                                                    IsOnline, 
                                                    AddTime, 
                                                    HeadImage, 
                                                    HeadSmallImage)   
                                           VALUES(@myid, 

                                                    @Email,
                                                    @NickName,
                                                    @Password,
                                                    @Contactqq,
                                                    @Contactaddr,
                                                    @UserName,
                                                    @Mobile,
                                                    @avatar,
                                                    @RankCredits,
                                                    @IsOnline,
                                                    @AddTime,
                                                    @HeadImage,
                                                    @HeadSmallImage) ;SELECT @myid";
		
        // 根据ID更新
		private const string SqlUpdate = @"Update TOP(1) Users 
                                           SET 

                                               Email = @Email,
                                               NickName = @NickName,
                                               Password = @Password,
                                               Contactqq = @Contactqq,
                                               Contactaddr = @Contactaddr,
                                               UserName = @UserName,
                                               Mobile = @Mobile,
                                               avatar = @avatar,
                                               RankCredits = @RankCredits,
                                               IsOnline = @IsOnline,
                                               AddTime = @AddTime,
                                               HeadImage = @HeadImage,
                                               HeadSmallImage = @HeadSmallImage
                                           WHERE [id] = @id
										  ";
		
        
		// 删除SQL
		private const string SqlDelete = @"DELETE TOP(1) FROM Users WHERE [id]=@id";
		
		// 根据编号获取对象
		private const string SqlUsersEntityByID = @"SELECT TOP 1 * 
                                                      FROM Users 
    WHERE [id]=@id";
		
		// 获取全部对象
		private const string SqlGetAllUsersList = @"SELECT * 
                                                      FROM Users";
		
		#endregion
		
		#region 公有方法
		
		/// <summary>
        /// 创建
        /// </summary>
        /// <param name="usersEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public static bool Insert( UsersEntity usersEntity , SqlTransaction tran)
		{			
            List<SqlParameter> parameters = new List<SqlParameter>();
			
                parameters.Add(SqlServerHelper.CreateInputParameter("@Email", SqlDbType.Char, usersEntity.Email));
                parameters.Add(SqlServerHelper.CreateInputParameter("@NickName", SqlDbType.NChar, usersEntity.NickName));
                parameters.Add(SqlServerHelper.CreateInputParameter("@Password", SqlDbType.Char, usersEntity.Password));
                parameters.Add(SqlServerHelper.CreateInputParameter("@Contactqq", SqlDbType.Char, usersEntity.Contactqq));
                parameters.Add(SqlServerHelper.CreateInputParameter("@Contactaddr", SqlDbType.Char, usersEntity.Contactaddr));
                parameters.Add(SqlServerHelper.CreateInputParameter("@UserName", SqlDbType.NChar, usersEntity.UserName));
                parameters.Add(SqlServerHelper.CreateInputParameter("@Mobile", SqlDbType.Char, usersEntity.Mobile));
                parameters.Add(SqlServerHelper.CreateInputParameter("@avatar", SqlDbType.Char, usersEntity.Avatar));
                parameters.Add(SqlServerHelper.CreateInputParameter("@RankCredits", SqlDbType.Int, usersEntity.RankCredits));
                parameters.Add(SqlServerHelper.CreateInputParameter("@IsOnline", SqlDbType.TinyInt, usersEntity.IsOnline));
                parameters.Add(SqlServerHelper.CreateInputParameter("@AddTime", SqlDbType.DateTime, usersEntity.AddTime));
                parameters.Add(SqlServerHelper.CreateInputParameter("@HeadImage", SqlDbType.NVarChar, usersEntity.HeadImage));
                parameters.Add(SqlServerHelper.CreateInputParameter("@HeadSmallImage", SqlDbType.NVarChar, usersEntity.HeadSmallImage));
			
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
        /// <param name="usersEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public static Guid InsertReturnID( UsersEntity usersEntity , SqlTransaction tran)
		{
            List<SqlParameter> parameters = new List<SqlParameter>();
			
                parameters.Add(SqlServerHelper.CreateInputParameter("@Email", SqlDbType.Char, usersEntity.Email));
                parameters.Add(SqlServerHelper.CreateInputParameter("@NickName", SqlDbType.NChar, usersEntity.NickName));
                parameters.Add(SqlServerHelper.CreateInputParameter("@Password", SqlDbType.Char, usersEntity.Password));
                parameters.Add(SqlServerHelper.CreateInputParameter("@Contactqq", SqlDbType.Char, usersEntity.Contactqq));
                parameters.Add(SqlServerHelper.CreateInputParameter("@Contactaddr", SqlDbType.Char, usersEntity.Contactaddr));
                parameters.Add(SqlServerHelper.CreateInputParameter("@UserName", SqlDbType.NChar, usersEntity.UserName));
                parameters.Add(SqlServerHelper.CreateInputParameter("@Mobile", SqlDbType.Char, usersEntity.Mobile));
                parameters.Add(SqlServerHelper.CreateInputParameter("@avatar", SqlDbType.Char, usersEntity.Avatar));
                parameters.Add(SqlServerHelper.CreateInputParameter("@RankCredits", SqlDbType.Int, usersEntity.RankCredits));
                parameters.Add(SqlServerHelper.CreateInputParameter("@IsOnline", SqlDbType.TinyInt, usersEntity.IsOnline));
                parameters.Add(SqlServerHelper.CreateInputParameter("@AddTime", SqlDbType.DateTime, usersEntity.AddTime));
                parameters.Add(SqlServerHelper.CreateInputParameter("@HeadImage", SqlDbType.NVarChar, usersEntity.HeadImage));
                parameters.Add(SqlServerHelper.CreateInputParameter("@HeadSmallImage", SqlDbType.NVarChar, usersEntity.HeadSmallImage));
			
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
        /// <param name="usersEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public static bool Update( UsersEntity usersEntity , SqlTransaction tran)
		{
            List<SqlParameter> parameters = new List<SqlParameter>();
			
                parameters.Add(SqlServerHelper.CreateInputParameter("@Id", SqlDbType.UniqueIdentifier, usersEntity.Id));
                parameters.Add(SqlServerHelper.CreateInputParameter("@Email", SqlDbType.Char, usersEntity.Email));
                parameters.Add(SqlServerHelper.CreateInputParameter("@NickName", SqlDbType.NChar, usersEntity.NickName));
                parameters.Add(SqlServerHelper.CreateInputParameter("@Password", SqlDbType.Char, usersEntity.Password));
                parameters.Add(SqlServerHelper.CreateInputParameter("@Contactqq", SqlDbType.Char, usersEntity.Contactqq));
                parameters.Add(SqlServerHelper.CreateInputParameter("@Contactaddr", SqlDbType.Char, usersEntity.Contactaddr));
                parameters.Add(SqlServerHelper.CreateInputParameter("@UserName", SqlDbType.NChar, usersEntity.UserName));
                parameters.Add(SqlServerHelper.CreateInputParameter("@Mobile", SqlDbType.Char, usersEntity.Mobile));
                parameters.Add(SqlServerHelper.CreateInputParameter("@avatar", SqlDbType.Char, usersEntity.Avatar));
                parameters.Add(SqlServerHelper.CreateInputParameter("@RankCredits", SqlDbType.Int, usersEntity.RankCredits));
                parameters.Add(SqlServerHelper.CreateInputParameter("@IsOnline", SqlDbType.TinyInt, usersEntity.IsOnline));
                parameters.Add(SqlServerHelper.CreateInputParameter("@AddTime", SqlDbType.DateTime, usersEntity.AddTime));
                parameters.Add(SqlServerHelper.CreateInputParameter("@HeadImage", SqlDbType.NVarChar, usersEntity.HeadImage));
                parameters.Add(SqlServerHelper.CreateInputParameter("@HeadSmallImage", SqlDbType.NVarChar, usersEntity.HeadSmallImage));
			
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
		public static  UsersEntity GetUsersEntityByID(Guid id, SqlTransaction tran)
		{
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SqlServerHelper.CreateInputParameter("@id", SqlDbType.UniqueIdentifier, id));

            if (tran == null)
            {
                return GetBindTData(SqlServerHelper.ExecuteReader(CommandType.Text, SqlUsersEntityByID, parameters.ToArray()));
            }

            return GetBindTData(SqlServerHelper.ExecuteReader(tran, CommandType.Text, SqlUsersEntityByID, parameters.ToArray()));
		}

	    public static int UpdateAll()
	    {
            string sql = "UPDATE Users SET IsCanDel=0,DelTime='',IsOtherDel=0,OtherDelTime='' ";
	        return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, null);
	    }

	    /// <summary>
        /// 获取全部对象
        /// </summary>
        /// <param name="tran">事务</param>
		/// <returns>对象实体</returns>
		public static List< UsersEntity> GetAllUsersList(SqlTransaction tran)
		{
            if (tran == null)
            {
			    return GetBindTDataList(SqlServerHelper.ExecuteReader(CommandType.Text, SqlGetAllUsersList, null));
            }

            return  GetBindTDataList(SqlServerHelper.ExecuteReader(tran, CommandType.Text, SqlGetAllUsersList, null));
		}
		
		#endregion
		
		#region 私有方法

        /// <summary>
        /// 实体集合Reader
        /// </summary>
        /// <returns>实体集合</returns>
        protected static List< UsersEntity> GetBindTDataList(SqlDataReader dataReader)
        {
            
            List< UsersEntity> usersList = new List< UsersEntity>();


            while (dataReader.Read())
            {
                 UsersEntity usersEntity = new  UsersEntity();
		
                if (dataReader["Id"] != DBNull.Value)
                {
                usersEntity.Id = new Guid(dataReader["Id"].ToString());
                }
                if (dataReader["Email"] != DBNull.Value)
                {
                usersEntity.Email = Convert.ToString(dataReader["Email"]);
                }
                if (dataReader["NickName"] != DBNull.Value)
                {
                usersEntity.NickName = Convert.ToString(dataReader["NickName"]);
                }
                if (dataReader["Password"] != DBNull.Value)
                {
                usersEntity.Password = Convert.ToString(dataReader["Password"]);
                }
                if (dataReader["Contactqq"] != DBNull.Value)
                {
                usersEntity.Contactqq = Convert.ToString(dataReader["Contactqq"]);
                }
                if (dataReader["Contactaddr"] != DBNull.Value)
                {
                usersEntity.Contactaddr = Convert.ToString(dataReader["Contactaddr"]);
                }
                if (dataReader["UserName"] != DBNull.Value)
                {
                usersEntity.UserName = Convert.ToString(dataReader["UserName"]);
                }
                if (dataReader["Mobile"] != DBNull.Value)
                {
                usersEntity.Mobile = Convert.ToString(dataReader["Mobile"]);
                }
                if (dataReader["avatar"] != DBNull.Value)
                {
                usersEntity.Avatar = Convert.ToString(dataReader["avatar"]);
                }
                if (dataReader["RankCredits"] != DBNull.Value)
                {
                usersEntity.RankCredits = Convert.ToInt32(dataReader["RankCredits"]);
                }
                if (dataReader["IsOnline"] != DBNull.Value)
                {
                usersEntity.IsOnline = Convert.ToByte(dataReader["IsOnline"]);
                }
                if (dataReader["AddTime"] != DBNull.Value)
                {
                usersEntity.AddTime = Convert.ToDateTime(dataReader["AddTime"]);
                }
                if (dataReader["HeadImage"] != DBNull.Value)
                {
                usersEntity.HeadImage = Convert.ToString(dataReader["HeadImage"]);
                }
                if (dataReader["HeadSmallImage"] != DBNull.Value)
                {
                usersEntity.HeadSmallImage = Convert.ToString(dataReader["HeadSmallImage"]);
                }
                
                usersList.Add(usersEntity);
            }
            
            dataReader.Close();
            dataReader.Dispose();

            return usersList;

        }
		
		/// <summary>
        /// 实体Reader
        /// </summary>
        /// <returns>实体对象</returns>
        protected static UsersEntity GetBindTData(SqlDataReader dataReader)
		{

             UsersEntity usersEntity = null;
            
            while (dataReader.Read())
            {
                usersEntity = new  UsersEntity();
		
                if (dataReader["Id"] != DBNull.Value)
                {
                usersEntity.Id = new Guid(dataReader["Id"].ToString());
                }
                if (dataReader["Email"] != DBNull.Value)
                {
                usersEntity.Email = Convert.ToString(dataReader["Email"]);
                }
                if (dataReader["NickName"] != DBNull.Value)
                {
                usersEntity.NickName = Convert.ToString(dataReader["NickName"]);
                }
                if (dataReader["Password"] != DBNull.Value)
                {
                usersEntity.Password = Convert.ToString(dataReader["Password"]);
                }
                if (dataReader["Contactqq"] != DBNull.Value)
                {
                usersEntity.Contactqq = Convert.ToString(dataReader["Contactqq"]);
                }
                if (dataReader["Contactaddr"] != DBNull.Value)
                {
                usersEntity.Contactaddr = Convert.ToString(dataReader["Contactaddr"]);
                }
                if (dataReader["UserName"] != DBNull.Value)
                {
                usersEntity.UserName = Convert.ToString(dataReader["UserName"]);
                }
                if (dataReader["Mobile"] != DBNull.Value)
                {
                usersEntity.Mobile = Convert.ToString(dataReader["Mobile"]);
                }
                if (dataReader["avatar"] != DBNull.Value)
                {
                usersEntity.Avatar = Convert.ToString(dataReader["avatar"]);
                }
                if (dataReader["RankCredits"] != DBNull.Value)
                {
                usersEntity.RankCredits = Convert.ToInt32(dataReader["RankCredits"]);
                }
                if (dataReader["IsOnline"] != DBNull.Value)
                {
                usersEntity.IsOnline = Convert.ToByte(dataReader["IsOnline"]);
                }
                if (dataReader["AddTime"] != DBNull.Value)
                {
                usersEntity.AddTime = Convert.ToDateTime(dataReader["AddTime"]);
                }
                if (dataReader["HeadImage"] != DBNull.Value)
                {
                usersEntity.HeadImage = Convert.ToString(dataReader["HeadImage"]);
                }
                if (dataReader["HeadSmallImage"] != DBNull.Value)
                {
                usersEntity.HeadSmallImage = Convert.ToString(dataReader["HeadSmallImage"]);
                }

            }
            
            dataReader.Close();
            dataReader.Dispose();

            return usersEntity;
		}
		
		#endregion
	}
}
