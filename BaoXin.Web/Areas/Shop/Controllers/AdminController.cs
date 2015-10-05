using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using BaoXin.BLL;
using BaoXin.Core;
using BaoXin.Core.Security;
using BaoXin.Entity;
using BaoXin.Web.Factory;
using BaoXin.Web.Models;
using 飞机订票系统MVC.Areas.Admin.Utilities.Validate;

namespace BaoXin.Web.Areas.Shop.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Shop/Admin/

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Loginn()
        {
            return View();
        }

        public ActionResult DoLogin(LoginModel users)
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
                    user = bll.GetUsersEntityByEmail1(loginName, null).TData;
                }
                else
                {
                    if (ValidateHelper.IsPhone(loginName))
                    {
                        user = bll.GetUsersEntityByMobile1(loginName, null).TData;
                    }
                    else
                    {
                        return Content("<script>alert('请输入正确的邮箱或者电话号码！！');location.href = '/shop/admin/login';</script>");
                      
                    }
                }
                if (user != null && user.Password.Equals(SecurityUtil.HashPassword(password)))
                {
                    //统一存cookie
                    ShopUtils.SetUserCookie(user, 2);
                    Session["User"] = user;
                    user.IsOnline = 1;
                    bll.Update(user, null);
                    return Content("<script>location.href = '/shop/admin/pass';</script>");
                    
                }

                else
                    return Content("<script>alert('用户名或密码不正确！！');location.href = '/shop/admin/login';</script>");
                   
            }
            catch (Exception ex)
            {
                return Content("<script>alert('登陆出错！！');location.href = '/shop/admin/login';</script>");
               
                throw;
            }
        }
        public ActionResult DoLoginn(LoginModel users)
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
                        return Content("<script>alert('请输入正确的邮箱或者电话号码！！');location.href = '/shop/admin/loginn';</script>");

                    }
                }
                if (user != null && user.Password.Equals(SecurityUtil.HashPassword(password)))
                {
                    //统一存cookie
                    ShopUtils.SetUserCookie(user, 2);
                    Session["User"] = user;
                    user.IsOnline = 1;
                    bll.Update(user, null);
                    return Content("<script>location.href = '/shop/admin/GoodsMana';</script>");

                }

                else
                    return Content("<script>alert('用户名或密码不正确！！');location.href = '/shop/admin/loginn';</script>");

            }
            catch (Exception ex)
            {
                return Content("<script>alert('登陆出错！！');location.href = '/shop/admin/loginn';</script>");

                throw;
            }
        }


        public ActionResult Pass()
        {
            if (Session["User"]==null)
            {
                return View("Login");
            }
            else
            {
                return View();
            }
           
        }
        #region 生成验证码
        public ActionResult GetValidateCode(string time = null)
        {
            ValidateImg vCode = new ValidateImg();
            string code = vCode.CreateValidateCode(5);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }


        #region 店铺数据
        public JsonResult ShopJson()
        {
            return Json(BuildFactory.AppylyStroeFactory().QueryShop1(), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion

         public JsonResult ShopJson1()
        {
            return Json(BuildFactory.AppylyStroeFactory().QueryShop2(), JsonRequestBehavior.AllowGet);
        }
     
        
        public ActionResult PassExam(int id,string email)
        {

            if (BuildFactory.AppylyStroeFactory().Pass(id))
            {
                try
                {
                    if (Send(email))
                    {
                        return Content("<script>alert('已通过审核,并发送邮件通知');location.href = '/shop/admin/pass';</script>");
                    }
                    else
                    {
                        return Content("<script>alert('邮箱地址有误,邮件将无法送达');location.href = '/shop/admin/pass';</script>");
                    }
                   ;
                   
                }
                catch (Exception)
                {

                 
                }
              
            }
            ;

            return Content("<script>alert('审核失败，请重新审核');location.href = '/shop/admin/pass';</script>");
        }

        public ActionResult GoodsMana()
        {
            if (Session["User"] == null)
            {
                return View("Loginn");
            }
            else
            {
                return View();
            }
        }

          public JsonResult GoodsJson()
        {


            return Json(BuildFactory.AppylyStroeFactory().GoodsMana(((UsersEntity)Session["User"]).NickName), JsonRequestBehavior.AllowGet);
        }


          public JsonResult GoodsJson1()
          {


              return Json(BuildFactory.AppylyStroeFactory().GoodsMana1(), JsonRequestBehavior.AllowGet);
          }
          public ActionResult PassExam1(int id)
          {

              if (BuildFactory.AppylyStroeFactory().Pass1(id))
              {

                  return Content("<script>alert('已发货');location.href = '/shop/admin/GoodsMana';</script>");
              }
              ;

              return Content("<script>alert('发货失败，请重新发货');location.href = '/shop/admin/GoodsMana';</script>");
          }


        public ActionResult DeleteShop()
        {
            if (Session["User"] == null)
            {
                return View("Login");
            }
            else
            {
                return View();
            }
        }

        public ActionResult dodelete(int id)
        {
            if (BuildFactory.AppylyStroeFactory().Dodelete(id))
            {

                return Content("<script>alert('删除成功！！');location.href = '/shop/admin/DeleteShop';</script>");
            }
            ;

            return Content("<script>alert('删除失败，请重新删除');location.href = '/shop/admin/DeleteShop';</script>");
        }



        public ActionResult OrderDetial(int id,int id1,int id2,int id3)
        {
            ViewBag.goodsid = id;
            ViewBag.StoreId = id1;
            ViewBag.PeopleId_buy = id2;
            ViewBag.buyNum = id3;
            return View();
        }

        #region 得到店信息
        public JsonResult GetBuyJosn(int id)
        {


            return Json(BuildFactory.GoodsFactory().Buyer(id), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 得到商品信息
        public JsonResult GetGoodsJosn(int id)
        {


            return Json(BuildFactory.GoodsFactory().GoodsDatil(id), JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region 得到商品信息
        public JsonResult GetBuyPeopleJosn(int id)
        {


            return Json(BuildFactory.AppylyStroeFactory().QueryPeople(id), JsonRequestBehavior.AllowGet);
        }
        #endregion


        public ActionResult Order()
        {
            return View();

        }

      
        public bool Send(string str)
        {




            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.From = new MailAddress("3201989179@qq.com", "爆信网");
            //message.From = new MailAddress("grandmass@sina.com", "阔众");
            message.To.Add(new MailAddress(str));
            message.Subject = "客户留言邮件";
            message.CC.Add(new MailAddress(str));
            message.Bcc.Add(new MailAddress(str));
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.ASCIIEncoding.UTF8;
            //message.Body = "welcome to here!";
            message.Body = "邮件来自公司：爆信网" + "你申请的店铺已通过";
            message.Priority = System.Net.Mail.MailPriority.High;
            SmtpClient client = new SmtpClient("smtp.qq.com", 587);
            client.Credentials = new System.Net.NetworkCredential("3201989179@qq.com", "2015bysoft");
            client.EnableSsl = true;
            try
            {
                client.Send(message);
                return true;

            }
            catch (Exception ee)
            {
                return false;
            }




            //str = "503945930@qq.com";
            //System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            //message.From = new MailAddress(str);
            ////message.From = new MailAddress("grandmass@sina.com", "阔众");
            //message.To.Add(new MailAddress(str));
            //message.Subject = "客户留言邮件";
            //message.CC.Add(new MailAddress(str));
            //message.Bcc.Add(new MailAddress(str));
            //message.IsBodyHtml = true;
            //message.BodyEncoding = System.Text.ASCIIEncoding.UTF8;
            ////message.Body = "welcome to here!";
            //message.Body = "邮件来自公司：爆信网" +"你申请的店铺已通过";
            //message.Priority = System.Net.Mail.MailPriority.High;
            //SmtpClient client = new SmtpClient("smtp.qq.com", 587);
            //client.Credentials = new System.Net.NetworkCredential("3161894345@qq.com", "18381334402wy");
            //client.EnableSsl = true;
            //try
            //{
            //    client.Send(message);
                
            //}
            //catch (Exception )
            //{
            //    throw;
            //}
        }
    }
}
