using BaoXin.Core.Security;
using BaoXin.DAL;
using BaoXin.Entity;
using BaoXin.Entity.Result;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BaoXin.BLL
{
    public partial class UsersBLL
    {
        /// <summary>
        /// 根据Email获取实体
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="tran">事务</param>
        /// <returns>对象实体</returns>
        public TResult<UsersEntity> GetUsersEntityByEmail(string email, SqlTransaction tran)
        {
            TResult<UsersEntity> result = new TResult<UsersEntity>();

            UsersEntity entity = UsersDAL.GetUsersEntityByEmail(email, tran);
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



        public TResult<UsersEntity> GetUsersEntityByEmail1(string email, SqlTransaction tran)
        {
            TResult<UsersEntity> result = new TResult<UsersEntity>();

            UsersEntity entity = UsersDAL.GetUsersEntityByEmail1(email, tran);
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
        /// 根据Email获取实体
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="tran">事务</param>
        /// <returns>对象实体</returns>
        public TResult<UsersEntity> GetUsersEntityByMobile(string email, SqlTransaction tran)
        {
            TResult<UsersEntity> result = new TResult<UsersEntity>();

            UsersEntity entity = UsersDAL.GetUsersEntityByEmail(email, tran);
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
        public TResult<UsersEntity> GetUsersEntityByMobile1(string email, SqlTransaction tran)
        {
            TResult<UsersEntity> result = new TResult<UsersEntity>();

            UsersEntity entity = UsersDAL.GetUsersEntityByEmail1(email, tran);
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
        /// 根据nickName查询数据
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="tran">事务</param>
        /// <returns>对象实体</returns>
        public TResult<int> GetListByNickName(string nickName)
        {
            TResult<int> result = new TResult<int>();

            int i = Convert.ToInt32(UsersDAL.GetListByNickName(nickName, null));
            if (i > 0)
            {
                result.IsSuccess = false;
                result.Message = "昵称已经存在！";
                return result;
            }

            result.IsSuccess = true;
            result.TData = i;
            result.Message = "获取成功";
            return result;
        }

        public UsersEntity GetPartUserByUidAndPwd(Guid uid)
        {
            UsersEntity entity = new UsersEntity();
            entity = UsersDAL.GetUsersEntityByID(uid, null);
            if (entity != null)
                return entity;
            else
                return null;
        }

       /// <summary>
       /// 获取好友列表
       /// </summary>
       /// <param name="tran"></param>
       /// <param name="userId"></param>
       /// <returns></returns>
        public TResult<List<UsersEntity>> GetFriendUsersListByUserId(SqlTransaction tran, Guid userId)
        {
            TResult<List<UsersEntity>> result = new TResult<List<UsersEntity>>();

            List<UsersEntity> list = UsersDAL.GetFriendUsersListByUserId(userId, tran);
            if (list == null)
            {
                result.IsSuccess = false;
                result.Message = "不存在";

                return result;
            }

            result.IsSuccess = true;
            result.TData = list.OrderByDescending(s=>s.IsHasNews).OrderByDescending(s=>s.IsOnline).ToList();
            result.Message = "获取成功";
            return result;
        }


        public void ResetUserCanDel()
        {
        }

        public List<Users> LoadOnlineUser() {
            return UsersDAL.LoadOnlineUser();
        }

    }
}
