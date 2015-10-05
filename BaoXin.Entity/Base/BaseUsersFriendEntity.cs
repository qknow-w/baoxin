/*-------------------------------------------------------------------------
 * 版权所有：自动生成，请勿手动修改
 * 作者：zengteng
 * 操作：创建
 * 操作时间：2015/1/26 21:57:10
 * 版本号：v1.0
 *  -------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

//using HungryWolf.Core;
//using HungryWolf.Core.Attrbutes;

namespace BaoXin.Entity
{
    /// <summary>
    /// (UsersFriend)实体类
    /// </summary>
	[Serializable]
    public partial class  UsersFriendEntity
    {
        
        private System.Guid _Id;
        
		/// <summary>
        /// 
        /// </summary>
		public System.Guid Id{
            get{ return _Id; }
            set{ _Id = value; }
        }	
        
        
        private System.Guid _UserId;
        
		/// <summary>
        /// 
        /// </summary>
		public System.Guid UserId{
            get{ return _UserId; }
            set{ _UserId = value; }
        }	
        
        
        private System.Guid _FriendId;
        
		/// <summary>
        /// 
        /// </summary>
		public System.Guid FriendId{
            get{ return _FriendId; }
            set{ _FriendId = value; }
        }	
        
        
        private System.DateTime _AddTime;
        
		/// <summary>
        /// 
        /// </summary>
		public System.DateTime AddTime{
            get{ return _AddTime; }
            set{ _AddTime = value; }
        }	
        
        
        private bool _IsDel;
        
		/// <summary>
        /// 
        /// </summary>
		public bool IsDel{
            get{ return _IsDel; }
            set{ _IsDel = value; }
        }	
        
    }
}