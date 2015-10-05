/*-------------------------------------------------------------------------
 * 版权所有：自动生成，请勿手动修改
 * 作者：zengteng
 * 操作：创建
 * 操作时间：2015/1/23 14:14:13
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
    public partial class SpeechInfoDAL
    {

        /// <summary>
        /// 获取全部对象
        /// </summary>
        /// <param name="tran">事务</param>
        /// <returns>对象实体</returns>
        public static List<SpeechInfoPart> GetSpeechInfoListByFromId(Guid fromUser, SqlTransaction tran)
        {
            string sql = @"SELECT t.Id
      ,[FromUser]
      ,[SpeachContent]
      ,SpeechImage
      ,[IsVip]
      ,[State]
      ,[SumbitTime]
      ,j.NickName
        FROM [SpeechInfo] t left join Users j on t.FromUser=j.id where  t.IsVip=1 and t.FromUser=@fromUser order by t.SumbitTime desc";
            var parameters = new List<SqlParameter>();
            parameters.Add(SqlServerHelper.CreateInputParameter("@fromUser", SqlDbType.UniqueIdentifier, fromUser));

            if (tran == null)
            {
                return GetBindDataList(SqlServerHelper.ExecuteReader(CommandType.Text, sql, parameters.ToArray()));
            }

            return GetBindDataList(SqlServerHelper.ExecuteReader(tran, CommandType.Text, sql, parameters.ToArray()));
        }


        /// <summary>
        /// 实体集合Reader
        /// </summary>
        /// <returns>实体集合</returns>
        protected static List<SpeechInfoPart> GetBindDataList(SqlDataReader dataReader)
        {

            List<SpeechInfoPart> speechInfoList = new List<SpeechInfoPart>();

            while (dataReader.Read())
            {
                SpeechInfoPart speechInfoEntity = new SpeechInfoPart();

                if (dataReader["Id"] != DBNull.Value)
                {
                    speechInfoEntity.Id = new Guid(dataReader["Id"].ToString());
                }
                if (dataReader["FromUser"] != DBNull.Value)
                {
                    speechInfoEntity.FromUser = new Guid(dataReader["FromUser"].ToString());
                }
                if (dataReader["SpeechImage"] != DBNull.Value)
                {
                    speechInfoEntity.SpeechImage = Convert.ToString(dataReader["SpeechImage"]);
                }
                if (dataReader["SpeachContent"] != DBNull.Value)
                {
                    speechInfoEntity.SpeachContent = Convert.ToString(dataReader["SpeachContent"]);
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

                if (dataReader["NickName"] != DBNull.Value)
                {
                    speechInfoEntity.NickName = (dataReader["NickName"]).ToString();
                }

                speechInfoList.Add(speechInfoEntity);
            }

            dataReader.Close();
            dataReader.Dispose();

            return speechInfoList;

        }


        public static int GetSpeechCount(Guid uid)
        {
            string sql =
                @"SELECT COUNT(1) as num FROM dbo.SpeechInfo WHERE   DATEDIFF(minute,SumbitTime,GETDATE())<60 AND FromUser='{0}'";
            sql = string.Format(sql, uid.ToString());
            var obj = SqlServerHelper.ExecuteReader(CommandType.Text, sql, null);
            int rtnNum = 0;
            while (obj.Read())
            {

                if (obj["num"] != DBNull.Value)
                {
                    rtnNum = Convert.ToInt32(obj["num"].ToString());
                }
              
            }
            obj.Close();
            obj.Dispose();
            return rtnNum;

        }

    }
}
