using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;

using BaoXin.Core;
using BaoXin.Entity;
using BaoXin.BLL;

namespace BaoXin.Web.Framework
{
    /// <summary>
    /// 商城前台基础控制器类
    /// </summary>
    public class BaseWebController : Controller
    {
        public UsersEntity CurrentUser { get; set; }

        public List<City> citys { get; set; }


        //工作上下文
        public WebWorkContext WorkContext = new WebWorkContext();

        /// <summary>
        /// 说明：初始化调用构造函数后可能不可用的数据。
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            SpeechInfoBLL bll = new SpeechInfoBLL();
            citys = bll.GetCitys();

            WorkContext.IsHttpAjax = WebHelper.IsAjax();
            WorkContext.Url = WebHelper.GetUrl();
            WorkContext.UrlReferrer = WebHelper.GetUrlReferrer();
            WorkContext.IP = WebHelper.GetIP();
            // WebHelper.GetIP();

            if (string.IsNullOrWhiteSpace(WorkContext.IP) || WorkContext.IP == "127.0.0.1")
            {
                WorkContext.CityName = "重庆市";
            }
            else
            {
                //测试地址搜索#region 测试地址搜索 
                IPScaner objScan = new IPScaner();
                string ip = Request.UserHostAddress.ToString();
                objScan.DataPath = Server.MapPath("/App_data/qqwry.Dat");
                objScan.IP = WorkContext.IP;
                //"113.200.29.90";
                string addre = objScan.IPLocation();
                int IndexofA = addre.IndexOf("省") + 1;
                WorkContext.CityName = addre.Substring(IndexofA);
            }
            UsersEntity userInfo = new UsersEntity();
            Guid uid = ShopUtils.GetUidCookie();
            //获得保存在cookie中的密码
            string encryptPwd = ShopUtils.GetCookiePassword();
            UsersBLL userBll = new UsersBLL();
            if (Session["User"] != null)
            {
                CurrentUser = Session["User"] as UsersEntity;
                userInfo = CurrentUser;
            }
            else
            {

                userInfo = userBll.GetPartUserByUidAndPwd(uid);
              //  requestContext.HttpContext.Response.Write("<script>alert('登录超时！');window.location.reload();</script>");
            }

            if (userInfo != null && !Guid.Empty.Equals(userInfo.Id))
            {
                WorkContext.Uid = userInfo.Id;
                WorkContext.Password = userInfo.Password;
                WorkContext.UserEmail = userInfo.Email;
                WorkContext.NickName = string.IsNullOrWhiteSpace(userInfo.NickName) ? userInfo.Email : userInfo.NickName;
            }

            //从cookie中获取用户的基本信息
        }

        /// <summary>
        /// 提示信息视图
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        protected ViewResult PromptView(string message)
        {
            return View("prompt", new PromptModel(message));
        }

        /// <summary>
        /// 提示信息视图
        /// </summary>
        /// <param name="backUrl">返回地址</param>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        protected ViewResult PromptView(string backUrl, string message)
        {
            return View("prompt", new PromptModel(backUrl, message));
        }

        /// <summary>
        /// ajax请求结果
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        protected ActionResult AjaxResult(string state, string content)
        {
            return AjaxResult(state, content, false);
        }

        /// <summary>
        /// ajax请求结果
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="content">内容</param>
        /// <param name="isObject">是否为对象</param>
        /// <returns></returns>
        protected ActionResult AjaxResult(string state, string content, bool isObject)
        {
            return Content(string.Format("{0}\"state\":\"{1}\",\"content\":{2}{3}{4}{5}", "{", state, isObject ? "" : "\"", content, isObject ? "" : "\"", "}"));
        }

        /// <summary>
        /// ajax请求结果
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="content">内容</param>
        /// <param name="isObject">是否为对象</param>
        /// <returns></returns>
        protected ActionResult AjaxResult(string state, object content, bool isObject)
        {
            return Content(string.Format("{0}\"state\":\"{1}\",\"content\":{2}{3}{4}{5}", "{", state, isObject ? "" : "\"", content, isObject ? "" : "\"", "}"));
        }
    }
}
