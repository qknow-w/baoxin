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

//using HungryWolf.Core;
//using HungryWolf.Core.Attrbutes;

namespace BaoXin.Entity
{
    /// <summary>
    /// (Users)实体类
    /// </summary>
	[Serializable]
    public partial class  UsersEntity
    {
        
        private System.Guid _Id;
        
		/// <summary>
        /// 
        /// </summary>
		public System.Guid Id{
            get{ return _Id; }
            set{ _Id = value; }
        }	
        
        
        private string _Email;
        
		/// <summary>
        /// 
        /// </summary>
		public string Email{
            get{ return _Email; }
            set{ _Email = value; }
        }	
        
        
        private string _NickName;
        
		/// <summary>
        /// 
        /// </summary>
		public string NickName{
            get{ return _NickName; }
            set{ _NickName = value; }
        }	
        
        
        private string _Password;
        
		/// <summary>
        /// 
        /// </summary>
		public string Password{
            get{ return _Password; }
            set{ _Password = value; }
        }	
        
        
        private string _Contactqq;
        
		/// <summary>
        /// 
        /// </summary>
		public string Contactqq{
            get{ return _Contactqq; }
            set{ _Contactqq = value; }
        }	
        
        
        private string _Contactaddr;
        
		/// <summary>
        /// 
        /// </summary>
		public string Contactaddr{
            get{ return _Contactaddr; }
            set{ _Contactaddr = value; }
        }	
        
        
        private string _UserName;
        
		/// <summary>
        /// 
        /// </summary>
		public string UserName{
            get{ return _UserName; }
            set{ _UserName = value; }
        }	
        
        
        private string _Mobile;
        
		/// <summary>
        /// 
        /// </summary>
		public string Mobile{
            get{ return _Mobile; }
            set{ _Mobile = value; }
        }	
        
        
        private string _Avatar;
        
		/// <summary>
        /// 
        /// </summary>
		public string Avatar{
            get{ return _Avatar; }
            set{ _Avatar = value; }
        }	
        
        
        private int _RankCredits;
        
		/// <summary>
        /// 
        /// </summary>
		public int RankCredits{
            get{ return _RankCredits; }
            set{ _RankCredits = value; }
        }


        private int _IsOnline;
        
		/// <summary>
        /// //是否在线：1在线，0离线
        /// </summary>
		public int IsOnline{
            get{ return _IsOnline; }
            set{ _IsOnline = value; }
        }	
        
        
        private System.DateTime _AddTime;
        
		/// <summary>
        /// 
        /// </summary>
		public System.DateTime AddTime{
            get{ return _AddTime; }
            set{ _AddTime = value; }
        }	
        
        
        private string _HeadImage;
        
		/// <summary>
        /// 
        /// </summary>
		public string HeadImage{
            get{ return _HeadImage; }
            set{ _HeadImage = value; }
        }	
        
        
        private string _HeadSmallImage;
        
		/// <summary>
        /// 
        /// </summary>
		public string HeadSmallImage{
            get{ return _HeadSmallImage; }
            set{ _HeadSmallImage = value; }
        }



        public bool IsCanDel { get; set; }

        public DateTime DelTime { get; set; }

        public bool IsHasNews { get; set; }



    }
}