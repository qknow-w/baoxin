using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoXin.Web.Framework;
using BaoXin.Entity.Models.Account;
using BaoXin.Core;
using BaoXin.Entity;
using BaoXin.Core.Security;
using BaoXin.BLL;
using System.Text;
using BaoXin.Entity.Result;

namespace BaoXin.Web.Controllers
{
    public class AccountController : BaseWebController
    {


        #region 注册
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserRegister userReg)
        {
            BLL.UsersBLL userBll = new BLL.UsersBLL();
            string returnUrl = WebHelper.GetQueryString("returnUrl", string.Empty);
            if (returnUrl.Length <= 0)
                returnUrl = "/";
            //1.0判断用户是否重复：
            if ((userBll.GetUsersEntityByEmail(userReg.Email, null).TData) != null)//
            {
                ModelState.AddModelError("Email", "该邮箱已注册");
            }
            //if (string.IsNullOrWhiteSpace(userReg.Mobile) && (userBll.GetUsersEntityByMobile(userReg.Mobile, null).TData) != null)//
            //{
            //    ModelState.AddModelError("Mobile", "该电话号码已注册");
            //}
            //判断昵称是否重复：
            if (!(userBll.GetListByNickName(userReg.Nickname).IsSuccess))
            {
                ModelState.AddModelError("Nickname", "该昵称已经存在");
            }
            if (ModelState.IsValid)//
            {//2.0保存数据
                var user = new UsersEntity()
                {
                    Email = userReg.Email,
                    Password = SecurityUtil.HashPassword(userReg.Password),
                    Mobile = userReg.Mobile,
                    NickName = userReg.Nickname.Trim(),
                    Contactaddr = userReg.Contactaddr,
                    HeadImage = userReg.ImageUrl,
                    HeadSmallImage = string.Empty,
                    UserName = string.Empty,
                    AddTime = DateTime.Now,
                    Avatar = string.Empty,
                    IsOnline=1,
                    Contactqq = userReg.Contactqq,
                    RankCredits = 0,

                };
                Guid userId = userBll.InsertReturnID(user, null);
                if (!Guid.Empty.Equals(userId))
                {
                    user.Id = userId;
                    //数据存于cookie中
                    //将用户信息写入cookie
                    ShopUtils.SetUserCookie(user, 2);
                    //3.0注册成功跳转
                    WorkContext.Uid = userId;
                    WorkContext.UserEmail = user.Email;
                     Session["User"] = user;
                    return Redirect(returnUrl);
                }
                return AjaxResult("fail", "注册失败!", false);
            }
            return View();
        }
        #endregion

        #region 登录
        [HttpPost]
        public ActionResult Login()
        {
            try
            {
                var loginName = WebHelper.GetFormString("loginName", string.Empty);
                var password = WebHelper.GetFormString("password", string.Empty);
                UsersBLL bll = new UsersBLL();
                var user = new UsersEntity();
                //首先判断是邮箱还是电话：
                if (ValidateHelper.IsEmail(loginName))
                {
                    user = bll.GetUsersEntityByEmail(loginName, null).TData;
                }
                else
                {
                    if (ValidateHelper.IsPhone(loginName))
                    {
                        user = bll.GetUsersEntityByMobile(loginName, null).TData;
                    }
                    else
                    {
                        return AjaxResult("valicationErr", "请输入正确的邮箱或者电话号码！！", false);
                    }
                }
                if (user != null && user.Password.Equals(SecurityUtil.HashPassword(password)))
                {
                    //统一存cookie
                    ShopUtils.SetUserCookie(user, 2);
                    Session["User"] = user;
                    user.IsOnline = 1;
                    bll.Update(user,null);

                    return AjaxResult("success", "登录成功！!", false);
                }

                else
                    return AjaxResult("error", "用户名或密码不正确！！", false);
            }
            catch (Exception ex)
            {
                return AjaxResult("error", ex.ToString(), false);
                throw;
            }


        }
        #endregion

        #region 退出
        /// <summary>
        /// 退出
        /// </summary>
        public ActionResult Logout()
        {
            if (!WorkContext.Uid.Equals(Guid.Empty))
            {
                WebHelper.DeleteCookie("bsp");
                if (Session["User"] != null)
                {
                    UsersBLL userBLL = new UsersBLL();
                    var user = Session["User"] as UsersEntity;
                    if (user != null)
                    {
                        user.IsOnline = 0;
                        userBLL.Update(user, null);
                    }
                }
                Session["User"] = null;
                Session.Abandon();
             
                //  Sessions.RemoverSession(WorkContext.Sid);
                //   OnlineUsers.DeleteOnlineUserBySid(WorkContext.Sid);
            }
            return Redirect("/");
        }
        #endregion

        #region 获取用户好友
        [HttpPost]
        public ActionResult GetUserFriends()
        {
            var result = new TResult<List<UsersEntity>>();
            UsersBLL userBLL = new UsersBLL();
            if (CurrentUser!=null)
            {
                var friendList = userBLL.GetFriendUsersListByUserId(null,  CurrentUser.Id);
                result = friendList;
            }
            return Json(result);
        }
        #endregion


        #region 添加好友AddFriend
        [HttpPost]
        public ActionResult AddFriend()
        {
            BLL.UsersFriendBLL bll = new UsersFriendBLL();
            try
            {
                var FriendId = WebHelper.GetFormString("FriendId");
                if (FriendId.Equals(WorkContext.Uid.ToString()))//不能加自己为好友
                {
                    return AjaxResult("self", "", false);
                }
                var model = new Entity.UsersFriendEntity();
                model.FriendId = new Guid(FriendId);
                model.UserId = WorkContext.Uid;
                model.AddTime = DateTime.Now;
                model.IsDel = false;
                //如果已经是好友，则不添加：
                if (bll.ExistFriend(model.UserId, model.FriendId).IsSuccess)
                {
                    return AjaxResult("ok", "", false);
                }
                else
                {
                    if (bll.Insert(model, null))
                    {
                        return AjaxResult("ok", "", false);
                    }
                    else
                    {
                        return AjaxResult("err", "添加好友失败！", false);
                    }
                }

            }
            catch (Exception ex)
            {
                return AjaxResult("err", "添加好友失败！" + ex.ToString(), false);
                throw;
            }


        }
        #endregion

    }
}
