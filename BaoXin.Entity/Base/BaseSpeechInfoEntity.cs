/*-------------------------------------------------------------------------
 * 版权所有：自动生成，请勿手动修改
 * 作者：zengteng
 * 操作：创建
 * 操作时间：2015/1/25 12:18:21
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
    /// (SpeechInfo)实体类
    /// </summary>
	[Serializable]
    public partial class  SpeechInfoEntity
    {
        
        private System.Guid _Id;
        
		/// <summary>
        /// 
        /// </summary>
		public System.Guid Id{
            get{ return _Id; }
            set{ _Id = value; }
        }	
        
        
        private System.Guid _FromUser;
        
		/// <summary>
        /// 
        /// </summary>
		public System.Guid FromUser{
            get{ return _FromUser; }
            set{ _FromUser = value; }
        }	
        
        
        private string _SpeachContent;
        
		/// <summary>
        /// 
        /// </summary>
		public string SpeachContent{
            get{ return _SpeachContent; }
            set{ _SpeachContent = value; }
        }	
        
        
        private string _SpeechImage;
        
		/// <summary>
        /// 
        /// </summary>
		public string SpeechImage{
            get{ return _SpeechImage; }
            set{ _SpeechImage = value; }
        }	
        
        
        private byte _IsVip;
        
		/// <summary>
        /// 
        /// </summary>
		public byte IsVip{
            get{ return _IsVip; }
            set{ _IsVip = value; }
        }	
        
        
        private byte _State;
        
		/// <summary>
        /// 
        /// </summary>
		public byte State{
            get{ return _State; }
            set{ _State = value; }
        }	
        
        
        private System.DateTime _SumbitTime;
        
		/// <summary>
        /// 
        /// </summary>
		public System.DateTime SumbitTime{
            get{ return _SumbitTime; }
            set{ _SumbitTime = value; }
        }

        public string City { get; set; }
        public string SourceCity { get; set; }

    }
}