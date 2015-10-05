using BaoXin.BLL;
using BaoXin.Core;
using BaoXin.Entity;
using BaoXin.Entity.Result;
using BaoXin.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BaoXin.Web.Controllers
{

    public class SpeachController : BaseWebController
    {
        public ActionResult SendDialog()
        {
            return View();
        }


        /// <summary>
        /// 打开发言页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Book()
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

                var city = "";
                //统计当前发言人的发言次数
                //if (Session["User"] != null)
                //{
                //    UsersEntity user = Session["User"] as UsersEntity;

                //    //获取当前这个人的这个小时发言的数量
                string uid = string.Empty;
                if (Session["User"] != null)
                {
                    UsersEntity user = Session["User"] as UsersEntity;
                    uid = user.Id.ToString();
                }
                else
                {
                    uid = ShopUtils.GetBSPCookie("bsp");
                    if (string.IsNullOrEmpty(uid.ToString()))
                    {
                        uid = Guid.NewGuid().ToString();
                        ShopUtils.SetBSPCookie("bsp", uid);
                    }
                    
                     
                }
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
                var citys = bll.GetCitys();
                if (citys.FirstOrDefault(s => s.CityName == ct) == null)
                {
                    ct = "重庆市";
                }

              

                SpeechInfoBLL bill = new SpeechInfoBLL();
                if (!string.IsNullOrEmpty(uid))
                    {
                        int num = bill.GetSpeechCount(new Guid(uid));
                        if (num >= 3)
                        {
                            return AjaxResult("err", "发送失败一小时内最多能发3条！", false);
                        }
                    }
                    model.SourceCity = sourceaddr;
                    model.FromUser = new Guid(uid);
                    model.IsVip = Convert.ToByte(Guid.Empty.Equals(new Guid(uid)) ? 0 : 1);
                    model.SumbitTime = DateTime.Now;
                    model.State = 1; //-1为删除状态，1表示正常
                    model.City = ct;
                   // SpeechInfoBLL bll = new SpeechInfoBLL();
                    if (bll.Insert(model, null))
                    {
                        return AjaxResult("ok", "发送成功！", false);
                    }
                    else
                    {
                        return AjaxResult("ok", "发送失败！", false);

                    }
                //}

                //return AjaxResult("err", "请登陆后发信息！", false);


            }
            catch (Exception ex)
            {
                //return AjaxResult("err", "发送失败！" + ex.ToString(), false);
                throw;
            }

        }

        /// <summary>
        /// 获取发言记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRecords()
        {
            var list = new List<Entity.SpeechInfoPart>();
            SpeechInfoBLL bll = new SpeechInfoBLL();
            StringBuilder sb = new StringBuilder();
            var result = new TResult<List<SpeechInfoPart>>();
            if (!Guid.Empty.Equals(WorkContext.Uid))
            {
                result = bll.GetSpeechInfoListByFromId(WorkContext.Uid, null);
                if (result.IsSuccess && result.TData.Count > 0)
                {
                    list = result.TData;
                }
            }
            return Json(result);
        }
    }
}
