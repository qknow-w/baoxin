/*-------------------------------------------------------------------------
 * 版权所有：自动生成，请勿手动修改
 * 作者：zengteng
 * 操作：创建
 * 操作时间：2015/1/25 12:18:22
 * 版本号：v1.0
 *  -------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

//using HungryWolf.Core;
//using HungryWolf.Core.Attrbutes;
using BaoXin.Entity;
using BaoXin.Entity.Result;
using BaoXin.DAL;


namespace BaoXin.BLL
{
	/// <summary>
    /// 数据访问类
    /// </summary>
	public partial class  SpeechInfoBLL
    {		
		
		#region 公有方法
		
		/// <summary>
        /// 创建
        /// </summary>
        /// <param name=" speechInfoEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public bool Insert( SpeechInfoEntity speechInfoEntity , SqlTransaction  tran)
		{
            return  SpeechInfoDAL.Insert(speechInfoEntity, tran);
		}
        
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name=" speechInfoEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public Guid InsertReturnID( SpeechInfoEntity speechInfoEntity , SqlTransaction tran)
		{
            return  SpeechInfoDAL.InsertReturnID(speechInfoEntity, tran);
		}
		
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name=" speechInfoEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public bool Update( SpeechInfoEntity speechInfoEntity , SqlTransaction tran)
		{
            return  SpeechInfoDAL.Update(speechInfoEntity, tran);
		}

		/// <summary>
        /// 根据编号删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="tran">事务</param>
		  /// <returns>是否成功</returns>
        public bool Delete(Guid id, SqlTransaction tran)
		{
            return  SpeechInfoDAL.Delete(id, tran);
		}
        public bool Delete(Guid id, Guid userid)
        {
            return SpeechInfoDAL.Delete(id, userid);
        }
		
		/// <summary>
        /// 根据编号获取实体
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="tran">事务</param>
		/// <returns>对象实体</returns>
		public TResult<SpeechInfoEntity> GetSpeechInfoEntityByID(Guid id, SqlTransaction tran)
		{
            TResult< SpeechInfoEntity> result = new TResult< SpeechInfoEntity>();
            
             SpeechInfoEntity entity =  SpeechInfoDAL.GetSpeechInfoEntityByID(id, tran);
            if (entity == null)
            {
                result.IsSuccess = false;
                result.Message = "不存在";

                return result;
            }

            result.IsSuccess = true;
            result.TData = entity;
            result.Message = "获取成功";
            return result;
		}
		
		/// <summary>
        /// 获取全部对象
        /// </summary>
        /// <param name="tran">事务</param>
		/// <returns>对象实体</returns>
		public TResult<List< SpeechInfoEntity>> GetAllSpeechInfoList(string key,string city,SqlTransaction tran)
		{
            TResult<List< SpeechInfoEntity>> result = new TResult<List< SpeechInfoEntity>>();

            List<SpeechInfoEntity> list = SpeechInfoDAL.GetAllSpeechInfoList(key,city, tran);
            if (list == null)
            {
                result.IsSuccess = false;
                result.Message = "不存在";

                return result;
            }

            result.IsSuccess = true;
            result.TData = list;
            result.Message = "获取成功";
            return result;
		}
		
		#endregion

	    public List<SpeechInfo> GetSpeechList(string key="",string city="")
	    {
	        return SpeechInfoDAL.GetSpeechList(key,city);
	    }


        public List<City> GetCitys()
        {
            return SpeechInfoDAL.GetCitys();
        }

	    public bool GetUser(string id)
	    {
	        return SpeechInfoDAL.GetUser(id);
	    }


    }
}
