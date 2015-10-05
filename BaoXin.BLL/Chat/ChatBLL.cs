using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaoXin.Entity;
using BaoXin.DAL.Chat;
namespace BaoXin.BLL.Chat
{
   public class ChatBLL
    {
       public List<ChatContent> GetUserChat(string fromuid,Guid toid)
       {
           return ChatInfoDAL.GetUserChat(fromuid, toid);
            
           
       }

       public int SaveChat(ChatInfo ct)
       {
           return ChatInfoDAL.SaveChat(ct);
       }
    }
}
