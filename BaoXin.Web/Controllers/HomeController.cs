using System.Web.UI.WebControls;
using BaoXin.BLL;
using BaoXin.Entity;
using BaoXin.Entity.Result;
using BaoXin.Web.Factory;
using BaoXin.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoXin.Core;

namespace BaoXin.Web.Controllers
{
    public class HomeController : BaseWebController
    {
        /// <summary>
        /// 首页显示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {

            //IPScaner objScan = new IPScaner();
            //string ip = Request.UserHostAddress.ToString();
            //objScan.DataPath = Server.MapPath("/App_data/qqwry.Dat");
            //objScan.IP = "58.19.17.114";
            ////"113.200.29.90";
            //string addre = "湖北省武汉市";
            //int IndexofA = addre.IndexOf("省") + 1;
            //var ct = addre.Substring(0,IndexofA);

            //Response.Write("58.19.17.114====" + addre + "========" + ct);

            var str = string.Empty;
            var city = string.Empty;
            if (Request["key"] != null)
            {
                str = Request["key"];
            }
            if (Request["c"] != null)
            {
                city = Request["c"];
            }
            var result = new TResult<List<Entity.SpeechInfoEntity>>();
            SpeechInfoBLL bll = new SpeechInfoBLL();
            ViewBag.Citys = citys;
            result = bll.GetAllSpeechInfoList(str, city, null);

          ;

            return View(result);
        }

        public ActionResult Index_New()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Index(string Searchkey, string city)
        {


            var str = Searchkey;
             var result = new TResult<List<Entity.SpeechInfoEntity>>();
            SpeechInfoBLL bll = new SpeechInfoBLL();
            ViewBag.Citys = citys;
            ViewBag.Sel = city;
            ViewBag.Key = Searchkey;
            result = bll.GetAllSpeechInfoList(str, city, null);
            return View(result);
        }
        
        public ActionResult Index1(string Searchkey, string city)
        {


            var str = Searchkey;
            var result = new TResult<List<Entity.SpeechInfoEntity>>();
            SpeechInfoBLL bll = new SpeechInfoBLL();
            ViewBag.Citys = citys;
            ViewBag.Sel = city;
            ViewBag.Key = Searchkey;
            result = bll.GetAllSpeechInfoList(str, city, null);
            return View("Index",result);
        }

        /// <summary>
        /// 中间1.	发言
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSpeachList()
        {
            var result = new TResult<List<Entity.SpeechInfoEntity>>();
            SpeechInfoBLL bll = new SpeechInfoBLL();
            //result = bll.GetAllSpeechInfoList(null);
            return View(result);
        }

         [HttpGet]
        public ActionResult DelSpeechInfo()
        {
            var result = new List<SpeechInfo>();
            SpeechInfoBLL bll = new SpeechInfoBLL();
            ViewBag.Citys = citys;
          
            result = bll.GetSpeechList();
            return View(result);
        }
         [HttpPost]
         public ActionResult DelSpeechInfo(string key, string city) 
         {
             var str = key;
             var result = new List<SpeechInfo>();
             SpeechInfoBLL bll = new SpeechInfoBLL();
             ViewBag.Citys = citys;
             ViewBag.Sel = city;
             ViewBag.Key = key;
             result = bll.GetSpeechList(key,city);
             return View(result);
         }


        public ContentResult DelOneSpeeInfo(string id)
        {
            if (Session["User"] != null)
            {
                var User = Session["User"] as UsersEntity;
                UsersBLL ubill = new UsersBLL();
                if (User != null)
                {

                    var quser = ubill.GetUserById(User.Id);

                    if (quser.isadmin == true)
                    {
                        Guid itemid = new Guid(id);
                        SpeechInfoBLL bll = new SpeechInfoBLL();
                        if (bll.Delete(itemid, User.Id))
                        {
                            return Content("操作成功");
                        }
                    }
                    else
                    {



                        if (BuildFactory.AppylyStroeFactory().QueryDateTime(User.Id) != null)
                        {
                            var dateTime = BuildFactory.AppylyStroeFactory().QueryDateTime(User.Id).DelTime;
                            DateTime dt1 = (DateTime)dateTime;

                            DateTime dt2 = DateTime.Now;
                            TimeSpan ts = dt2 - dt1;
                            if (ts.Days == 0 && ts.Hours == 0)
                            {
                                return Content("对不起，你在一个小时内不能再删其他用户的数据");
                            }
                            else
                            {

                                Guid itemid = new Guid(id);
                                SpeechInfoBLL bll = new SpeechInfoBLL();
                                if (bll.Delete(itemid, User.Id))
                                {
                                    return Content("操作成功");
                                }
                            }
                        }
                        else
                        {
                            Guid itemid = new Guid(id);
                            SpeechInfoBLL bll = new SpeechInfoBLL();
                            if (bll.Delete(itemid, User.Id))
                            {
                                return Content("操作成功");
                            }
                        }

                    }
                   












                    
                }
            }
            else
            {
                return Content("对不起，请先登陆了再删除");
                
            }


            return Content("");
        }


        public ActionResult SendDialog()
        {
            return View();
        }

        /// <summary>
        /// 保存发言
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult Save(SpeechInfoEntity model)
        {
            try
            {

                SpeechInfoBLL bll = new SpeechInfoBLL();
                IPScaner objScan = new IPScaner();
                string ip = Request.UserHostAddress.ToString();
                objScan.DataPath = Server.MapPath("/App_data/qqwry.Dat");
                objScan.IP = WorkContext.IP;
                //"113.200.29.90";
                string addre = objScan.IPLocation();
                int IndexofA = addre.IndexOf("省") + 1;
                var ct = addre.Substring(0,IndexofA);
                var sourceaddr = addre;
               var citys= bll.GetCitys();
               if (citys.FirstOrDefault(s => s.CityName == ct) == null)
               {
                   ct = "重庆市";
               }
                model.SourceCity = sourceaddr;
                model.City = ct;
                model.FromUser = WorkContext.Uid;
                model.IsVip = Convert.ToByte(Guid.Empty.Equals(WorkContext.Uid) ? 0 : 1);
                model.SumbitTime = DateTime.Now;
                model.State = 1;//-1为删除状态，1表示正常

              
                if (bll.Insert(model, null))
                {
                    return AjaxResult("ok", "发送成功！", false);
                }
                else
                {
                    return AjaxResult("err", "发送失败！", false);

                }
            }
            catch (Exception ex)
            {
                return AjaxResult("err", "发送失败！" + ex.ToString(), false);
                throw;
            }

        }


        public ActionResult SendContent(string id)
        {
            SpeechInfoBLL bill=new SpeechInfoBLL();
           var tresult= bill.GetSpeechInfoEntityByID(new Guid(id), null);
            ViewBag.IsShowSend = bill.GetUser(id);

           return View(tresult);
        }

        public ActionResult LoginOut(string uid)
        {
            var guid = new Guid(uid);
            UsersBLL uBll=new UsersBLL();
            var user = uBll.GetPartUserByUidAndPwd(guid);
            if (user != null)
            {
                user.IsOnline = 0;
                uBll.Update(user, null);
            }
            return Content("");

        }

        public ActionResult Test()
        {
            return View();

        }


    }
}
