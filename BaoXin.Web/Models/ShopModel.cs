using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaoXin.Web.Models
{
    public class ShopModel
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string PhotoURL { get; set; }
    }

    public class BuyGoods
    {
        public int StoreId { get; set; }  
        public string StoreName { get; set; }
        public Nullable<int> PhotoId { get; set; }
        public Nullable<int> PeopleId_buy { get; set; }  
        public Nullable<int> PeopleId { get; set; }
        public Nullable<int> GoodsId { get; set; }
        public Nullable<bool> BoolPass { get; set; }
        public Nullable<int> StoreOrde { get; set; } 
        public string PeopleNum { get; set; }
        public string PeopleName { get; set; }
        public string PeopleAdd { get; set; }
        public string PeopleEmail { get; set; }
        public string PeoplePhone { get; set; }
        public string PeoplePay { get; set; }
        public string PhotoURL { get; set; }
        public string GoodsName { get; set; }
        public decimal GoodsPrice { get; set; }
        public Nullable<short> Goodsnumber { get; set; }
        public string StoreUser { get; set; }
        public string AddTime { get; set; }
        public int ShopOrderID { get; set; }
        public int buyNum { get; set; }
        public string butTime { get; set; }
        public Nullable<bool> BoolSeng { get; set; }
      
    }


}