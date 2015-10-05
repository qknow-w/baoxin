using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Transactions;
using System.Web;
using BaoXin.Web.EF;

namespace BaoXin.Web.Models
{
    public class Shop
    {
        db_baoxinEntities db = new db_baoxinEntities();
        #region 添加商品
        public bool Apply(EF.Shop_Goods goods,EF.Shop_Photo photo)
        {
            //TransactionScope有错误自己回滚
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    DbEntityEntry<EF.Shop_Photo> entry0 = db.Entry<EF.Shop_Photo>(photo);
                    entry0.State = EntityState.Added;
                    db.SaveChanges();
                    goods.PhotoId = photo.PhotoId;



                    goods.BoolYX = true;
                    goods.AddTime = DateTime.Now;
                    DbEntityEntry<EF.Shop_Goods> entry = db.Entry<EF.Shop_Goods>(goods);
                    entry.State = EntityState.Added;
                    db.SaveChanges();                   
                    tran.Complete();
                    return true;

                }
                catch (Exception)
                {
                    tran.Dispose();

                    return false;
                    throw;
                }
            }

        }
        #endregion


        public bool Modify(EF.Shop_Goods goods, EF.Shop_Photo photo)
        {
            //TransactionScope有错误自己回滚
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    if (photo.PhotoURL!="")
                    {
                        DbEntityEntry<EF.Shop_Photo> entry0 = db.Entry<EF.Shop_Photo>(photo);
                        entry0.State = EntityState.Added;
                        db.SaveChanges();
                        goods.PhotoId = photo.PhotoId;
                        goods.BoolYX = true;
                        goods.AddTime = DateTime.Now;
                        DbEntityEntry<EF.Shop_Goods> entry = db.Entry<EF.Shop_Goods>(goods);
                        entry.State = EntityState.Modified;
                        db.SaveChanges();
                        tran.Complete();
                        return true;
                    }
                    else
                    {
                        goods.PhotoId = photo.PhotoId;
                        goods.BoolYX = true;
                        goods.AddTime = DateTime.Now;
                        DbEntityEntry<EF.Shop_Goods> entry = db.Entry<EF.Shop_Goods>(goods);
                        entry.State = EntityState.Modified;
                        db.SaveChanges();
                        tran.Complete();
                        return true;
                        
                    }

                    



                   

                }
                catch (Exception)
                {
                    tran.Dispose();

                    return false;
                    throw;
                }
            }

        }
        #region 所有商品的数量
        public int Count(int id)
        {
            return db.Shop_Goods.Count(a => a.StoreId==id);
        } 
        #endregion
        #region 分页查询商品信息
        public List<Models.GoodsModel> PagGoods(int page, int rows,int storeid)
        {
            //使用linq  (标准查询运算符)
           // List<Models.sys_log> data = (from a in db.sys_log select a).OrderByDescending(a => a.LogID).Take(rows * (page-1)).Skip(page * rows).ToList();            
            List<Models.GoodsModel> data =
                db.Database.SqlQuery<Models.GoodsModel>(
                    "select * from ( SELECT  Shop_Photo.PhotoURL, Shop_Goods.GoodsId,Shop_Goods.AddTime, Shop_Goods.GoodsName,Shop_Goods.Goodsnumber, Shop_Goods.GoodsPrice, Shop_Goods.PhotoId,ROW_NUMBER() OVER(Order by GoodsId) as rownum FROM         Shop_Goods INNER JOIN " +
                    " Shop_Photo ON Shop_Goods.PhotoId = Shop_Photo.PhotoId where Shop_Goods.StoreId={0} and Shop_Goods.BoolYX=1 ) as t where t.rownum between {1} and {2}  order by t.AddTime", storeid,
                    (page - 1)*rows+1, page*rows).ToList();
            return data;

        } 
        #endregion

        public Models.BuyGoods Buyer(int id)
        {
            Models.BuyGoods data =
                db.Database.SqlQuery<Models.BuyGoods>(
                    "SELECT     Shop_Store.StoreId, Shop_Store.StoreName, Shop_People.PeopleName, Shop_Store.PeopleId, Shop_Store.PhotoId, Shop_People.PeopleAdd,  " +
                    "                      Shop_People.PeoplePhone, Shop_People.PeoplePay, Shop_Photo.PhotoURL " +
                    "FROM         Shop_Store INNER JOIN                       Shop_People ON Shop_Store.PeopleId = Shop_People.PeopleId INNER JOIN   Shop_Photo ON Shop_Store.PhotoId = Shop_Photo.PhotoId" +
                    "  where Shop_Store.StoreId={0} ",
                    id).FirstOrDefault();
            return data;
        }
        public Models.BuyGoods GoodsDatil(int id)
        {
            Models.BuyGoods data =
                db.Database.SqlQuery<Models.BuyGoods>(
                    "SELECT     Shop_Goods.GoodsId, Shop_Goods.GoodsPrice, Shop_Photo.PhotoURL, Shop_Goods.GoodsName,Goodsnumber  FROM         Shop_Goods INNER JOIN " +
                    "                       Shop_Photo ON Shop_Goods.PhotoId = Shop_Photo.PhotoId " +
                    "  where  Shop_Goods.GoodsId={0} ",
                    id).FirstOrDefault();
            return data;
        }


        public Models.BuyGoods GetBuyTotl(int id)
        {
            Models.BuyGoods data =
                db.Database.SqlQuery<Models.BuyGoods>("SELECT   ISNULL(sum(buyNum),0)  AS buyNum FROM         Shop_Order where BoolYX=0 and GoodsId={0}", id).FirstOrDefault();
            return data;
        }


        public bool GenOrder(Shop_People people,Shop_Store store,Shop_Order order)
        {
            using (TransactionScope transaction=new TransactionScope())
            {
                try
                {
                    DbEntityEntry<Shop_People> entity = db.Entry(people);
                    entity.State = EntityState.Added;
                    db.SaveChanges();
                    order.PeopleId_buy = people.PeopleId;
                    order.StoreId = store.StoreId;
                    order.BoolYX = false;
                    order.butTime = DateTime.Now;
                    order.BoolSeng = false;
                    DbEntityEntry<Shop_Order> entity1 = db.Entry(order);
                    entity1.State = EntityState.Added;
                    db.SaveChanges();
                    transaction.Complete();
                    return true;


                }
                catch (Exception)
                {
                    transaction.Dispose();
                    return false;
                    throw;
                }
            }

        }



        public ShopModel ExamExit(string user)
        {
            ShopModel data =
               db.Database.SqlQuery<ShopModel>(
                   "SELECT StoreId   FROM         Shop_Store where StoreUser={0}", user).FirstOrDefault();
            return data;
        }


        public bool DeleteGood(int id)
        {
            try
            {
                db.Database.ExecuteSqlCommand("update Shop_Goods set BoolYX=0 where GoodsId={0}", id);
                return true;
            }
            catch (Exception)
            {
                
                throw;
                return false;
            }
        }

        public GoodsModel ModifyGoods(int id)
        {
            GoodsModel data =
              db.Database.SqlQuery<GoodsModel>(
                  "SELECT     GoodsId, GoodsName, GoodsPrice, Goodsnumber,PhotoId,StoreId FROM         Shop_Goods  where GoodsId={0}", id).FirstOrDefault();
            return data;
        }

    }
}