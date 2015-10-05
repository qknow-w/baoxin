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
	public partial class  UsersBLL
    {		
		
		#region 公有方法
		
		/// <summary>
        /// 创建
        /// </summary>
        /// <param name=" usersEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public bool Insert( UsersEntity usersEntity , SqlTransaction  tran)
		{
            return  UsersDAL.Insert(usersEntity, tran);
		}
        
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name=" usersEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public Guid InsertReturnID( UsersEntity usersEntity , SqlTransaction tran)
		{
            return  UsersDAL.InsertReturnID(usersEntity, tran);
		}
		
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name=" usersEntity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns> 
		public bool Update( UsersEntity usersEntity , SqlTransaction tran)
		{
            return  UsersDAL.Update(usersEntity, tran);
		}

		/// <summary>
        /// 根据编号删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="tran">事务</param>
		  /// <returns>是否成功</returns>
        public bool Delete(Guid id, SqlTransaction tran)
		{
            return  UsersDAL.Delete(id, tran);
		}
		
		/// <summary>
        /// 根据编号获取实体
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="tran">事务</param>
		/// <returns>对象实体</returns>
		public TResult< UsersEntity> GetUsersEntityByID(Guid id, SqlTransaction tran)
		{
            TResult< UsersEntity> result = new TResult< UsersEntity>();
            
             UsersEntity entity =  UsersDAL.GetUsersEntityByID(id, tran);
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
		public TResult<List< UsersEntity>> GetAllUsersList(SqlTransaction tran)
		{
            TResult<List< UsersEntity>> result = new TResult<List< UsersEntity>>();
            
            List< UsersEntity> list =  UsersDAL.GetAllUsersList(tran);
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

	    public Users GetUserById(Guid id)
	    {
            return UsersDAL.GetUserById(id);
	    }


    }
}
