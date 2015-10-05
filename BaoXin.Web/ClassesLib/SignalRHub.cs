using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BaoXin.Web.ClassesLib
{
    public class SignalRHub : Hub
    {
        //声明静态变量存储当前在线用户
        public static class UserHandler
        {
            public static Dictionary<string, string> ConnectedIds = new Dictionary<string, string>();
        }
        //用户进入页面时执行的（连接操作）
        public void userConnected(string nickName)
        {
            //进行编码，防止XSS攻击
            nickName = HttpUtility.HtmlEncode(nickName);
            string message = "用户 " + nickName + " 登录";
            //发送信息给其他人
            Clients.Others.addList(Context.ConnectionId, nickName);
            Clients.Others.hello(message);
            //新增目前使用者上线清单
            UserHandler.ConnectedIds.Add(Context.ConnectionId, nickName);
            //发送信息给自己，并显示上线清单
            Clients.Caller.getList(UserHandler.ConnectedIds.Select(p => new { id = p.Key, name = p.Value }).ToList());
        }

        //发送信息给所有人
        public void sendAllMessage(string message)
        {
            message = HttpUtility.HtmlEncode(message);
            var name = UserHandler.ConnectedIds.Where(p => p.Key == Context.ConnectionId).FirstOrDefault().Value;
            message = name + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "：" + message;
            Clients.All.sendAllMessge(message);
        }


        //发送信息给特定人
        public void sendMessage(string ToId, string message)
        {
            message = HttpUtility.HtmlEncode(message);
            var fromName = UserHandler.ConnectedIds.Where(p => p.Key == Context.ConnectionId).FirstOrDefault().Value;
            message = fromName + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + ":" + message;
            Clients.Client(ToId).sendMessage(message);
        }

        //当使用者断线时执行
        public override Task OnDisconnected()
        {
            //当使用者离开时，移除在清单内的ConnectionId
            Clients.All.removeList(Context.ConnectionId);
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            return base.OnDisconnected();
        }
    }
}