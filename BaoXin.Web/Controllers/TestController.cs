using BaoXin.Core;
using BaoXin.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaoXin.Web.Controllers
{
    public class TestController : BaseWebController
    {

        public ActionResult GetIp()
        {
            //测试地址搜索#region 测试地址搜索 
            IPScaner objScan = new IPScaner();
            string ip = Request.UserHostAddress.ToString();
            objScan.DataPath = Server.MapPath("/App_data/qqwry.Dat");
            objScan.IP = "113.200.29.90";
            string addre = objScan.IPLocation();
            int IndexofA = addre.IndexOf("省")+1;

            string cityName = addre.Substring(IndexofA);
            //  string add1=objScan
            //string err = objScan.ErrMsg;
            return Content(cityName);
        }

    }
}
