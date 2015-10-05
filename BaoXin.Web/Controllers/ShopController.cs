using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoXin.Web.Framework;

namespace BaoXin.Web.Controllers
{
    public class ShopController : BaseWebController
    {
        //
        // GET: /Shop/

        #region 开店视图
        public ActionResult SetUpStore()
        {
            return View();
        } 
        #endregion

    }
}
