using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaoXin.Web.Models;

namespace BaoXin.Web.Factory
{
    public class BuildFactory
    {
        public static Shop_Photo PhotoFactory()
        {
            return new Shop_Photo();
        }
        public static ApplyStroe AppylyStroeFactory()
        {
            return new ApplyStroe();
        }
        public static Models.Shop GoodsFactory()
        {
            return new Shop();
        }
    }
}