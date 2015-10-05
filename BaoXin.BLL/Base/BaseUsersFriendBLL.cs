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
	public partial class  UsersFriendBLL
    {		
		
		#region 公有方法
		
		/// <summary>
        /// 创建
        /// </summary>
        /// <param name=" usersFriendEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public bool Insert( UsersFriendEntity usersFriendEntity , SqlTransaction  tran)
		{
            return  UsersFriendDAL.Insert(usersFriendEntity, tran);
		}
        
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name=" usersFriendEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public Guid InsertReturnID( UsersFriendEntity usersFriendEntity , SqlTransaction tran)
		{
            return  UsersFriendDAL.InsertReturnID(usersFriendEntity, tran);
		}
		
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name=" usersFriendEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public bool Update( UsersFriendEntity usersFriendEntity , SqlTransaction tran)
		{
            return  UsersFriendDAL.Update(usersFriendEntity, tran);
		}

		/// <summary>
        /// 根据编号删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="tran">事务</param>
		  /// <returns>是否成功</returns>
        public bool Delete(Guid id, SqlTransaction tran)
		{
            return  UsersFriendDAL.Delete(id, tran);
		}
		
		/// <summary>
        /// 根据编号获取实体
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="tran">事务</param>
		/// <returns>对象实体</returns>
		public TResult< UsersFriendEntity> GetUsersFriendEntityByID(Guid id, SqlTransaction tran)
		{
            TResult< UsersFriendEntity> result = new TResult< UsersFriendEntity>();
            
             UsersFriendEntity entity =  UsersFriendDAL.GetUsersFriendEntityByID(id, tran);
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
		public TResult<List< UsersFriendEntity>> GetAllUsersFriendList(SqlTransaction tran)
		{
            TResult<List< UsersFriendEntity>> result = new TResult<List< UsersFriendEntity>>();
            
            List< UsersFriendEntity> list =  UsersFriendDAL.GetAllUsersFriendList(tran);
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
		
	}
}
