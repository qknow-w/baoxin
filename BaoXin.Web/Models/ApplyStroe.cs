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
    public class ApplyStroe
    {
        db_baoxinEntities db = new db_baoxinEntities();
        #region 添加申请店铺信息
        public bool Apply(EF.Shop_Photo photo, EF.Shop_People people, EF.Shop_Store shop)
        {
            //TransactionScope有错误自己回滚
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    DbEntityEntry<EF.Shop_Photo> entry = db.Entry<EF.Shop_Photo>(photo);
                    entry.State = EntityState.Added;
                    db.SaveChanges();
                    shop.PhotoId = photo.PhotoId;

                    people.BoolLX = true;
                    DbEntityEntry<EF.Shop_People> entry1 = db.Entry<EF.Shop_People>(people);
                    entry1.State = EntityState.Added;
                    db.SaveChanges();
                    shop.PeopleId = people.PeopleId;



                    shop.BoolPass = false;
                    DbEntityEntry<EF.Shop_Store> entry2 = db.Entry<EF.Shop_Store>(shop);
                    entry2.State = EntityState.Added;
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


        public List<Models.ShopModel> QueryShop()
        {
            List<Models.ShopModel> queryable = db.Database.SqlQuery<Models.ShopModel>("SELECT     Shop_Store.StoreId, Shop_Store.StoreName, Shop_Photo.PhotoURL  FROM         Shop_Store INNER JOIN" +
                                                                                                                   " Shop_Photo ON Shop_Store.PhotoId = Shop_Photo.PhotoId  where Shop_Store.BoolYX=1 and Shop_Store.BoolPass=1 ORDER BY Shop_Store.PassTime").ToList();

            return queryable;
        }

        public ShopModel QueryStoreidBy(string str)
        {
            ShopModel data = db.Database.SqlQuery<ShopModel>(" SELECT     StoreId  FROM         Shop_Store WHERE StoreUser={0}",
                str).FirstOrDefault();
            return data;
        }


        public List<BuyGoods> QueryShop1()
        {
            List<BuyGoods> queryable = db.Database.SqlQuery<Models.BuyGoods>("SELECT   Shop_Store.BoolPass,  Shop_Store.StoreId, Shop_Store.StoreName, Shop_Store.StoreUser, Shop_People.PeopleAdd, Shop_People.PeopleName, Shop_People.PeoplePhone, " +
                                                                             " Shop_People.PeopleEmail, Shop_People.PeoplePay, CONVERT(char(20),Shop_Store.AddTime,20)  as AddTime  FROM         Shop_Store INNER JOIN  Shop_People ON Shop_Store.PeopleId = Shop_People.PeopleId " +
                                                                             " where  Shop_Store.BoolYX=1  ORDER BY BoolPass, Shop_Store.AddTime DESC").ToList();

            return queryable;
        }


        public List<BuyGoods> QueryShop2()
        {
            List<BuyGoods> queryable = db.Database.SqlQuery<Models.BuyGoods>("SELECT     Shop_Store.StoreId, Shop_Store.StoreName, Shop_Store.StoreUser, Shop_People.PeopleAdd, Shop_People.PeopleName, Shop_People.PeoplePhone, " +
                                                                             " Shop_People.PeopleEmail, Shop_People.PeoplePay, CONVERT(char(20),Shop_Store.AddTime,20)  as AddTime  FROM         Shop_Store INNER JOIN  Shop_People ON Shop_Store.PeopleId = Shop_People.PeopleId " +
                                                                             " where Shop_Store.boolPass=1 and Shop_Store.BoolYX=1  ORDER BY Shop_Store.AddTime DESC").ToList();

            return queryable;
        }

        public bool Pass(int id)
        {
            try
            {
                int i = db.Database.ExecuteSqlCommand("UPDATE    Shop_Store SET   BoolPass =1 ,PassTime={0} where Shop_Store.StoreId={1}",DateTime.Now, id);
                if (i>0)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
                return false;
               
            }
            return false;
        }
        public bool Dodelete(int id)
        {
            try
            {
                int i = db.Database.ExecuteSqlCommand("DELETE  FROM   Shop_Store  where Shop_Store.StoreId={0}", id);
                if (i > 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
                return false;

            }
            return false;
        }

        public List<BuyGoods> GoodsMana(string Username)
        {
            List<BuyGoods> queryable = db.Database.SqlQuery<Models.BuyGoods>("SELECT  Shop_Order.PeopleId_buy,   Shop_Order.ShopOrderID, Shop_Order.PeopleId_buy, Shop_Order.StoreId, Shop_Order.GoodsId, Shop_Order.buyNum, CONVERT(char(20),Shop_Order.butTime,20)  as butTime, Shop_Order.BoolSeng,  " +
                                                                             "        Shop_Store.StoreName, Shop_People.PeopleName, Shop_People.PeopleAdd, Shop_People.PeopleEmail, Shop_People.PeoplePhone, Shop_People.PeoplePay,  " +
                                                                             "Shop_Goods.GoodsName, Shop_Goods.GoodsPrice, Shop_Store.StoreUser  FROM         Shop_Order INNER JOIN   Shop_Store ON Shop_Order.StoreId = Shop_Store.StoreId INNER JOIN " +
                                                                             " Shop_People ON Shop_Order.PeopleId_buy = Shop_People.PeopleId INNER JOIN  Shop_Goods ON Shop_Order.GoodsId = Shop_Goods.GoodsId where StoreUser={0} ORDER BY Shop_Order.butTime DESC ",Username).ToList();

            return queryable;
        }
        public List<BuyGoods> GoodsMana1()
        {
            List<BuyGoods> queryable = db.Database.SqlQuery<Models.BuyGoods>("SELECT  Shop_Order.PeopleId_buy,   Shop_Order.ShopOrderID, Shop_Order.PeopleId_buy, Shop_Order.StoreId, Shop_Order.GoodsId, Shop_Order.buyNum, CONVERT(char(20),Shop_Order.butTime,20)  as butTime, Shop_Order.BoolSeng,  " +
                                                                             "        Shop_Store.StoreName, Shop_People.PeopleName, Shop_People.PeopleAdd, Shop_People.PeopleEmail, Shop_People.PeoplePhone, Shop_People.PeoplePay,  " +
                                                                             "Shop_Goods.GoodsName, Shop_Goods.GoodsPrice, Shop_Store.StoreUser  FROM         Shop_Order INNER JOIN   Shop_Store ON Shop_Order.StoreId = Shop_Store.StoreId INNER JOIN " +
                                                                             " Shop_People ON Shop_Order.PeopleId_buy = Shop_People.PeopleId INNER JOIN  Shop_Goods ON Shop_Order.GoodsId = Shop_Goods.GoodsId  ORDER BY Shop_Order.butTime DESC ").ToList();

            return queryable;
        }

        public bool Pass1(int id)
        {

            using (TransactionScope transaction=new TransactionScope())
            {
                try
                {
                    db.Database.ExecuteSqlCommand(
                        "UPDATE    Shop_Goods SET              Goodsnumber =Goodsnumber-(SELECT     buyNum  FROM         Shop_Order " +
                        " where ShopOrderID={0}  ) where Shop_Goods.GoodsId=(SELECT     GoodsId  FROM         Shop_Order where ShopOrderID={1}  )",
                        id, id);




                    int i = db.Database.ExecuteSqlCommand("UPDATE    Shop_Order SET              BoolSeng =1 where ShopOrderID={0}", id);
                    transaction.Complete();
                    if (i > 0)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    transaction.Dispose();
                    throw;
                    return false;

                }
            }
           
            return false;
        }

        public Models.BuyGoods QueryPeople(int id)
        {
            Models.BuyGoods queryable =
                db.Database.SqlQuery<Models.BuyGoods>(
                    "SELECT     PeopleId, PeopleName, PeopleAdd, PeopleEmail, PeoplePhone, PeoplePay FROM         Shop_People where PeopleId={0}",id)
                    .FirstOrDefault();

            return queryable;
        }

        public Speech QueryDateTime(Guid str)
        {
            Speech queryable =
               db.Database.SqlQuery<Speech>(
                   "SELECT    top 1  DelTime FROM         SpeechInfo where DelUserId={0} ORDER BY DelTime DESC " +
                   " ", str)
                   .FirstOrDefault();

            return queryable;
        }
    }
}