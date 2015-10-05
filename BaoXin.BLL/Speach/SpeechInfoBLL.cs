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
    public partial class SpeechInfoBLL
    {

        /// <summary>
        /// 根据userid获取全部对象
        /// </summary>
        /// <param name="tran">事务</param>
        /// <returns>对象实体</returns>
        public TResult<List<SpeechInfoPart>> GetSpeechInfoListByFromId(Guid fromUser, SqlTransaction tran)
        {
            TResult<List<SpeechInfoPart>> result = new TResult<List<SpeechInfoPart>>();

            List<SpeechInfoPart> list = SpeechInfoDAL.GetSpeechInfoListByFromId(fromUser, tran);
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

        public int GetSpeechCount(Guid userid)
        {
           return  SpeechInfoDAL.GetSpeechCount(userid);
        }

    }
}
