using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaoXin.Web.Models
{
    public class GoodsModel
    {
        public int GoodsId { get; set; }

        public string GoodsName { get; set; }
        public Nullable<decimal> GoodsPrice { get; set; }
        public Int16 Goodsnumber { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<int> PhotoId { get; set; }
        public string StoreName { get; set; }
        public string PhotoURL { get; set; }

    }

    public class Speech
    {
        public DateTime DelTime { get; set; }
    }
}