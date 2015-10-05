using BaoXin.Entity;
using BaoXin.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BaoXin.DAL.Chat
{
    public class ChatInfoDAL
    {
        public static List<ChatContent> GetUserChat(string fromuid, Guid toid)
        {
            var sql = @"SELECT DISTINCT a.user_from,a.user_to,a.content,a.sendtime FROM dbo.ChatInfo a LEFT join users b 
                        ON (a.user_from=b.Id OR a.user_to=b.Id)  WHERE 1=1 ";
            if (!string.IsNullOrEmpty(fromuid))
            {
                sql += string.Format(" and ((a.user_from='{0}' OR a.user_from='{1}') ", fromuid, toid.ToString());
                sql += string.Format(" and (a.user_to='{1}' OR a.user_to='{0}')) ", fromuid, toid.ToString());
            }
            sql += " ORDER BY a.sendtime";

            var sqludp = "Update ChatInfo set IsOpen=1 where user_to='" + toid.ToString() + "' and user_from='"+fromuid+"'";

            try { SqlServerHelper.ExecuteNonQuery(sqludp, null); }
            catch (Exception ex) { }
            var reader = SqlServerHelper.ExecuteReader(CommandType.Text, sql, null);
            return GetBindTDataList(reader);

        }

        public static int SaveChat(ChatInfo ctx)
        {
           // var edm = new BaoXinEntities();
           // ChatInfo info = new ChatInfo();
             
           // info.user_from = ctx.user_from;
           // info.user_to = ctx.user_to;
           // info.sendtime = ctx.sendtime;
           // info.ispublic = true;
           // info.content = ctx.content;
           // info.IsOpen = false;
           // //DbEntityEntry<ChatInfo> entry = edm.Entry<ChatInfo>(info);
           // //entry.State = System.Data.EntityState.Added;
           //// edm.ChatInfo.Add(info);
           // edm.Entry(info).State = EntityState.Added;
           // return edm.SaveChanges();

            if (!string.IsNullOrEmpty(ctx.content))
            {
                ctx.content = ctx.content.Replace("'", "‘").Replace("'", "‘");
            }


            var sql = "Insert Into ChatInfo(user_from,user_to,sendtime,ispublic,content,IsOpen)values('"+ctx.user_from+"','"+ctx.user_to+"',getdate(),1,'"+ctx.content+"',0)";
        return    SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, null);


        }


        protected static List<ChatContent> GetBindTDataList(SqlDataReader dataReader)
        { 
          List< ChatContent> chatlist = new List< ChatContent>();


          while (dataReader.Read())
          {
              ChatContent chat = new ChatContent();

              if (dataReader["user_from"] != DBNull.Value)
              {
                  chat.fromid = dataReader["user_from"].ToString().ToLower();
              }
              if (dataReader["user_to"] != DBNull.Value)
              {
                  chat.toid = dataReader["user_to"].ToString().ToLower();
              }
              if (dataReader["content"] != DBNull.Value)
              {
                  chat.content = dataReader["content"].ToString().Replace("<br/>","");
              }
              //if (dataReader["NickName"] != DBNull.Value)
              //{
              //    chat.nickname = dataReader["NickName"].ToString();
              //}
              if (dataReader["sendtime"] != DBNull.Value)
              {
                  chat.sendTime = Convert.ToDateTime( dataReader["sendtime"].ToString());
              }
              chatlist.Add(chat);
          }
          dataReader.Close();
          dataReader.Dispose();
          return chatlist;
        }

          

    }



    public class ChatContent {
        public string fromid { get; set; }
        public string toid { get; set; }
        public string content { get; set; }
        public DateTime sendTime { get; set; }
        public string nickname { get; set; }
    }

   
}
