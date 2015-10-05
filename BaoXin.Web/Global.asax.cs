using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BaoXin.BLL;
using BaoXin.DAL;
using BaoXin.Entity;

namespace BaoXin.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            //定时检测用户
            SchedulerAgent.StartAgent(); ; 

           

        }

        protected   void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码

        }
        protected void Session_End(object sender, EventArgs e) {
            var user = new UsersEntity();
            if (Session["User"] != null) {
                user = Session["User"] as UsersEntity;
            }
            UsersBLL uBll = new UsersBLL();
            if (user != null)
            {
                user.IsOnline = 0;
                uBll.Update(user, null);
            }

        }

        protected void Application_End(Object sender, EventArgs e)
        {
            SchedulerAgent.Stop();
        }


      
    }
}