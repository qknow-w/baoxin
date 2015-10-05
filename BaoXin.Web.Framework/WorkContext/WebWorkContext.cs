using System;
using System.Collections.Generic;

using BaoXin.Core;

namespace BaoXin.Web.Framework
{
    /// <summary>
    /// 商城前台工作上下文类
    /// </summary>
    public class WebWorkContext
    {

        public bool IsHttpAjax;//当前请求是否为ajax请求

        public string IP;//用户ip

        public int RegionId;//区域id
     
        public string CityName;//城市
        public string Url;//当前url

        public string UrlReferrer;//上一次访问的url

        public string Sid;//用户sid

        public Guid Uid;//用户id

        public string UserName;//用户名

        public string UserEmail;//用户邮箱

        public string UserMobile;//用户手机号

        public string NickName;//用户昵称

        public string Avatar;//用户头像

        public string Password;//用户密码

        public string EncryptPwd;//加密密码
        //      public PartUserInfo PartUserInfo;//用户信
        public string Controller;//控制器

        public string Action;//动作方法
    }
}