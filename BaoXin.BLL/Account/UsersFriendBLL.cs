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
    public partial class UsersFriendBLL
    {

        /// <summary>
        /// 获取全部对象
        /// </summary>
        /// <param name="tran">事务</param>
        /// <returns>对象实体</returns>
        public TResult<List<UsersFriendEntity>> GetUsersFriendListByUserId(SqlTransaction tran, Guid userId)
        {
            TResult<List<UsersFriendEntity>> result = new TResult<List<UsersFriendEntity>>();

            List<UsersFriendEntity> list = UsersFriendDAL.GetUsersFriendListByUserId(userId, tran);
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


        /// <summary>
        /// 根据nickName查询数据
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="tran">事务</param>
        /// <returns>对象实体</returns>
        public TResult<int> ExistFriend(Guid userId, Guid friendId)
        {
            TResult<int> result = new TResult<int>();

            int i = Convert.ToInt32(UsersFriendDAL.ExistFriend(userId, friendId, null));
            if (i > 0)
            {
                result.IsSuccess = true;
                result.Message = "已经存在！";
                return result;
            }
            result.IsSuccess = false;
            result.TData = i;
            return result;
        }
    }
}
