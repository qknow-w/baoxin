using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using BaoXin.Web.EF;

namespace BaoXin.Web.Models
{
    public partial class Shop_Photo
    {
        db_baoxinEntities db=new db_baoxinEntities();

        #region 上传到数据库
        public int Upload(EF.Shop_Photo photo)
        {

            try
            {
                DbEntityEntry<EF.Shop_Photo> entry = db.Entry<EF.Shop_Photo>(photo);
                entry.State = EntityState.Added;
                int result = db.SaveChanges();

                if (result > 1)
                {
                    return result;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {

                return 0;
            }
        } 
        #endregion


       
    }
}