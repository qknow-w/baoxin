using BaoXin.BLL;
using BaoXin.BLL.Chat;
using BaoXin.DAL.Chat;
using BaoXin.Entity;
using BaoXin.Entity.Result;
using BaoXin.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaoXin.Web.Controllers
{

    public class ChatController : BaseWebController
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Room(string id)
        {
            var model = new TResult<UsersEntity>();
           
            if (!string.Empty.Equals(id))
            {
                Guid userId = new Guid(id);
                UsersBLL bll = new UsersBLL();
                model = bll.GetUsersEntityByID(userId, null);
            }
            

            return View(model);
        }

        public JsonResult LoadOnlineUser() {
            List<Users> users = new List<Users>();
            UsersBLL bll = new UsersBLL();
            users=bll.LoadOnlineUser();
            return this.Json(users,JsonRequestBehavior.AllowGet);
        }



        public JsonResult LoadUserTalk(string uid) {
            List<ChatContent> chats = new List<ChatContent>();
            string to = string.Empty;
            UsersEntity user = new UsersEntity();
            ChatBLL cbll = new ChatBLL();
            if (Session["User"] != null)
            {
                user = Session["User"] as UsersEntity;
                //uid ==fromuid ,user.id ==currentuser
                chats = cbll.GetUserChat(uid,user.Id);
            }


            
          
           return this.Json(chats, JsonRequestBehavior.AllowGet);
        }
        [ValidateInput(false)]
        public JsonResult SendChat(string tid,string ctx) {
            ChatInfo cinfo = new ChatInfo();
            if (CurrentUser != null)
            {
                cinfo.user_from = CurrentUser.Id.ToString();
                cinfo.user_to = tid;
                cinfo.sendtime = DateTime.Now;
                cinfo.content = ctx;
                cinfo.ispublic = true;
                ChatBLL cbll = new ChatBLL();
                cbll.SaveChat(cinfo);

            }
            return this.Json("success", JsonRequestBehavior.AllowGet);
        
        }


    }
}
