/*-------------------------------------------------------------------------
 * 版权所有：自动生成，请勿手动修改
 * 作者：zengteng
 * 操作：创建
 * 操作时间：2015/1/25 12:18:22
 * 版本号：v1.0
 *  -------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
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
	public partial class  SpeechInfoDAL
    {
		#region SQL
        
		// 根据ID添加
		private const string SqlInsert = @"INSERT INTO SpeechInfo 
                                                  (

                                                    FromUser, 
                                                    SpeachContent, 
                                                    SpeechImage, 
                                                    IsVip, 
                                                    State, 
                                                    SumbitTime,
                                                    cityname,
                                                    Sourcecity
                                                    )   
                                           VALUES(

                                                    @FromUser,
                                                    @SpeachContent,
                                                    @SpeechImage,
                                                    @IsVip,
                                                    @State,
                                                    @SumbitTime,@City,@Sourcecity)";
                                                    
        // 根据ID添加返回ID
		private const string SqlInsertReturnID = @"DECLARE @myid uniqueidentifier
                                           SET @myid = NEWID()
                                           INSERT INTO SpeechInfo 
                                                  (ID, 

                                                    FromUser, 
                                                    SpeachContent, 
                                                    SpeechImage, 
                                                    IsVip, 
                                                    State, 
                                                    SumbitTime,cityname,Sourcecity)   
                                           VALUES(@myid, 

                                                    @FromUser,
                                                    @SpeachContent,
                                                    @SpeechImage,
                                                    @IsVip,
                                                    @State,
                                                    @SumbitTime,@City,@Sourcecity) ;SELECT @myid";
		
        // 根据ID更新
		private const string SqlUpdate = @"Update TOP(1) SpeechInfo 
                                           SET 

                                               FromUser = @FromUser,
                                               SpeachContent = @SpeachContent,
                                               SpeechImage = @SpeechImage,
                                               IsVip = @IsVip,
                                               State = @State,
                                               SumbitTime = @SumbitTime
                                           WHERE [id] = @id
										  ";
		
        
		// 删除SQL
        private const string SqlDelete = @"update  SpeechInfo set IsDelete=1,DelUserId=@userid,DelTime=getdate() WHERE [id]=@id";
		
		// 根据编号获取对象
		private const string SqlSpeechInfoEntityByID = @"SELECT TOP 1 * 
                                                      FROM SpeechInfo 
    WHERE [id]=@id";
		
		// 获取全部对象
        private const string SqlGetAllSpeechInfoList = @"SELECT TOP 30 * 
                                                      FROM SpeechInfo where IsDelete=0 ";
		
		#endregion
		
		#region 公有方法
		
		/// <summary>
        /// 创建  增加用户积分
        /// </summary>
        /// <param name="speechInfoEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public static bool Insert( SpeechInfoEntity speechInfoEntity , SqlTransaction tran)
		{			
            List<SqlParameter> parameters = new List<SqlParameter>();
			
                parameters.Add(SqlServerHelper.CreateInputParameter("@FromUser", SqlDbType.UniqueIdentifier, speechInfoEntity.FromUser));
                parameters.Add(SqlServerHelper.CreateInputParameter("@SpeachContent", SqlDbType.NVarChar, speechInfoEntity.SpeachContent));
                parameters.Add(SqlServerHelper.CreateInputParameter("@SpeechImage", SqlDbType.NVarChar, speechInfoEntity.SpeechImage));
                parameters.Add(SqlServerHelper.CreateInputParameter("@IsVip", SqlDbType.TinyInt, speechInfoEntity.IsVip));
                parameters.Add(SqlServerHelper.CreateInputParameter("@State", SqlDbType.TinyInt, speechInfoEntity.State));
                parameters.Add(SqlServerHelper.CreateInputParameter("@SumbitTime", SqlDbType.DateTime, speechInfoEntity.SumbitTime));
                parameters.Add(SqlServerHelper.CreateInputParameter("@City", SqlDbType.NVarChar, speechInfoEntity.City));
                parameters.Add(SqlServerHelper.CreateInputParameter("@Sourcecity", SqlDbType.NVarChar, speechInfoEntity.SourceCity));
             
                SqlServerHelper.ExecuteNonQuery(CommandType.Text, SqlInsert, parameters.ToArray());
                var edm = new BaoXinEntities();
                var user = edm.Users.FirstOrDefault(s => s.Id == speechInfoEntity.FromUser);
                if (user != null)
                {
                    user.RankCredits = user.RankCredits + 1;
                    edm.SaveChanges();
                }
                return true;  
		}
        
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="speechInfoEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public static Guid InsertReturnID( SpeechInfoEntity speechInfoEntity , SqlTransaction tran)
		{
            List<SqlParameter> parameters = new List<SqlParameter>();
			
                parameters.Add(SqlServerHelper.CreateInputParameter("@FromUser", SqlDbType.UniqueIdentifier, speechInfoEntity.FromUser));
                parameters.Add(SqlServerHelper.CreateInputParameter("@SpeachContent", SqlDbType.NVarChar, speechInfoEntity.SpeachContent));
                parameters.Add(SqlServerHelper.CreateInputParameter("@SpeechImage", SqlDbType.NVarChar, speechInfoEntity.SpeechImage));
                parameters.Add(SqlServerHelper.CreateInputParameter("@IsVip", SqlDbType.TinyInt, speechInfoEntity.IsVip));
                parameters.Add(SqlServerHelper.CreateInputParameter("@State", SqlDbType.TinyInt, speechInfoEntity.State));
                parameters.Add(SqlServerHelper.CreateInputParameter("@SumbitTime", SqlDbType.DateTime, speechInfoEntity.SumbitTime));
                parameters.Add(SqlServerHelper.CreateInputParameter("@City", SqlDbType.NVarChar, speechInfoEntity.City));
			
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
        /// <param name="speechInfoEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public static bool Update( SpeechInfoEntity speechInfoEntity , SqlTransaction tran)
		{
            List<SqlParameter> parameters = new List<SqlParameter>();
			
                parameters.Add(SqlServerHelper.CreateInputParameter("@Id", SqlDbType.UniqueIdentifier, speechInfoEntity.Id));
                parameters.Add(SqlServerHelper.CreateInputParameter("@FromUser", SqlDbType.UniqueIdentifier, speechInfoEntity.FromUser));
                parameters.Add(SqlServerHelper.CreateInputParameter("@SpeachContent", SqlDbType.NVarChar, speechInfoEntity.SpeachContent));
                parameters.Add(SqlServerHelper.CreateInputParameter("@SpeechImage", SqlDbType.NVarChar, speechInfoEntity.SpeechImage));
                parameters.Add(SqlServerHelper.CreateInputParameter("@IsVip", SqlDbType.TinyInt, speechInfoEntity.IsVip));
                parameters.Add(SqlServerHelper.CreateInputParameter("@State", SqlDbType.TinyInt, speechInfoEntity.State));
                parameters.Add(SqlServerHelper.CreateInputParameter("@SumbitTime", SqlDbType.DateTime, speechInfoEntity.SumbitTime));
			
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
        /// 删除数据  2更新用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool Delete(Guid id, Guid userid)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SqlServerHelper.CreateInputParameter("@id", SqlDbType.UniqueIdentifier, id));
            parameters.Add(SqlServerHelper.CreateInputParameter("@userid", SqlDbType.UniqueIdentifier, userid));
            int rtn=  SqlServerHelper.ExecuteNonQuery(CommandType.Text, SqlDelete, parameters.ToArray());
            BaoXinEntities edm=new BaoXinEntities();
            //1删除别人的人需要更新为已经删除
            var user=edm.Users.FirstOrDefault(s => s.Id == userid);
            if (user != null)
            {
                user.IsCanDel = true;
                user.DelTime = DateTime.Now;
                edm.SaveChanges();
            }
            //2被删除的人 需要更新为今天被删除
            var spinfo = edm.SpeechInfo.FirstOrDefault(s => s.Id == id);
            if (spinfo != null)
            {
                var buser = edm.Users.FirstOrDefault(s => s.Id == spinfo.FromUser);
                if (buser != null)
                {
                    buser.IsOtherDel = true;
                    buser.OtherDelTime = DateTime.Now;
                    edm.SaveChanges();
                }
            }



            if (rtn > 0)
            {
                return true;
            }
            
            
            return false;


        }
		/// <summary>
        /// 根据编号获取实体
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="tran">事务</param>
		/// <returns>对象实体</returns>
		public static  SpeechInfoEntity GetSpeechInfoEntityByID(Guid id, SqlTransaction tran)
		{
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(SqlServerHelper.CreateInputParameter("@id", SqlDbType.UniqueIdentifier, id));

            if (tran == null)
            {
                return GetBindTData(SqlServerHelper.ExecuteReader(CommandType.Text, SqlSpeechInfoEntityByID, parameters.ToArray()));
            }

            return GetBindTData(SqlServerHelper.ExecuteReader(tran, CommandType.Text, SqlSpeechInfoEntityByID, parameters.ToArray()));
		}
		
		/// <summary>
        /// 获取全部对象
        /// </summary>
        /// <param name="tran">事务</param>
		/// <returns>对象实体</returns>
		public static List< SpeechInfoEntity> GetAllSpeechInfoList(string key,string city,SqlTransaction tran)
		{
		    string   sql = "SELECT TOP 200 *  FROM SpeechInfo where IsDelete=0  ";
             
		  
		    if (!string.IsNullOrEmpty(key))
		    {
                key = key.Replace("'", "‘");
              
		        sql += " and SpeachContent like '%" + key + "%'";
		    }
            
            if (!string.IsNullOrEmpty(city))
            {
                city = city.Replace("'", "‘");

                sql += " and cityname like '%" + city + "%'";
            }

		    if (tran == null)
            {
                return GetBindTDataList(SqlServerHelper.ExecuteReader(CommandType.Text, sql, null));
            }

            return GetBindTDataList(SqlServerHelper.ExecuteReader(tran, CommandType.Text, sql, null));
		}
		
		#endregion
		
		#region 私有方法

        /// <summary>
        /// 实体集合Reader
        /// </summary>
        /// <returns>实体集合</returns>
        protected static List< SpeechInfoEntity> GetBindTDataList(SqlDataReader dataReader)
        {
            
            List< SpeechInfoEntity> speechInfoList = new List< SpeechInfoEntity>();


            while (dataReader.Read())
            {
                 SpeechInfoEntity speechInfoEntity = new  SpeechInfoEntity();
		
                if (dataReader["Id"] != DBNull.Value)
                {
                speechInfoEntity.Id = new Guid(dataReader["Id"].ToString());
                }
                if (dataReader["FromUser"] != DBNull.Value)
                {
                speechInfoEntity.FromUser = new Guid(dataReader["FromUser"].ToString());
                }
                if (dataReader["SpeachContent"] != DBNull.Value)
                {
                speechInfoEntity.SpeachContent = Convert.ToString(dataReader["SpeachContent"]);
                }
                if (dataReader["SpeechImage"] != DBNull.Value)
                {
                speechInfoEntity.SpeechImage = Convert.ToString(dataReader["SpeechImage"]);
                }
                if (dataReader["IsVip"] != DBNull.Value)
                {
                speechInfoEntity.IsVip = Convert.ToByte(dataReader["IsVip"]);
                }
                if (dataReader["State"] != DBNull.Value)
                {
                speechInfoEntity.State = Convert.ToByte(dataReader["State"]);
                }
                if (dataReader["SumbitTime"] != DBNull.Value)
                {
                speechInfoEntity.SumbitTime = Convert.ToDateTime(dataReader["SumbitTime"]);
                }
                if (dataReader["cityname"] != DBNull.Value)
                {
                    speechInfoEntity.City = Convert.ToString(dataReader["cityname"]);
                }
                speechInfoList.Add(speechInfoEntity);
            }
            
            dataReader.Close();
            dataReader.Dispose();

            return speechInfoList;

        }
		
		/// <summary>
        /// 实体Reader
        /// </summary>
        /// <returns>实体对象</returns>
        protected static SpeechInfoEntity GetBindTData(SqlDataReader dataReader)
		{

             SpeechInfoEntity speechInfoEntity = null;
            
            while (dataReader.Read())
            {
                speechInfoEntity = new  SpeechInfoEntity();
		
                if (dataReader["Id"] != DBNull.Value)
                {
                speechInfoEntity.Id = new Guid(dataReader["Id"].ToString());
                }
                if (dataReader["FromUser"] != DBNull.Value)
                {
                speechInfoEntity.FromUser = new Guid(dataReader["FromUser"].ToString());
                }
                if (dataReader["SpeachContent"] != DBNull.Value)
                {
                speechInfoEntity.SpeachContent = Convert.ToString(dataReader["SpeachContent"]);
                }
                if (dataReader["SpeechImage"] != DBNull.Value)
                {
                speechInfoEntity.SpeechImage = Convert.ToString(dataReader["SpeechImage"]);
                }
                if (dataReader["IsVip"] != DBNull.Value)
                {
                speechInfoEntity.IsVip = Convert.ToByte(dataReader["IsVip"]);
                }
                if (dataReader["State"] != DBNull.Value)
                {
                speechInfoEntity.State = Convert.ToByte(dataReader["State"]);
                }
                if (dataReader["SumbitTime"] != DBNull.Value)
                {
                speechInfoEntity.SumbitTime = Convert.ToDateTime(dataReader["SumbitTime"]);
                }

            }
            
            dataReader.Close();
            dataReader.Dispose();

            return speechInfoEntity;
		}
		
		#endregion


        public static List<SpeechInfo> GetSpeechList(string key,string city)
	    {
	        var edm = new BaoXinEntities();

            var query= edm.SpeechInfo.Where(s=>s.IsDelete==
                false);

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(s => s.SpeachContent.IndexOf(key) >= 0);
            }
            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(s => s.cityname.IndexOf(city) >= 0);
            }
            return query.ToList();


	    }

        public static List<City> GetCitys() {
            var edm = new BaoXinEntities();
           return edm.City.OrderByDescending(s=>s.orderid).ToList();
        }
        public static bool GetUser(string id)
        {
           SpeechInfoEntity entity= GetSpeechInfoEntityByID(new Guid(id),null);
            if (entity != null)
            {
                var edm = new BaoXinEntities();
                return edm.Users.FirstOrDefault(s => s.Id == entity.FromUser) != null;
            }
            return false;
        }
    }
}
