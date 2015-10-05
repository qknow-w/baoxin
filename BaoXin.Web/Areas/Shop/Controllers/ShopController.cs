using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoXin.BLL;
using BaoXin.Entity;
using BaoXin.Entity.Result;
using BaoXin.Web.EF;
using BaoXin.Web.Factory;
using BaoXin.Web.Framework;
using BaoXin.Web.Models;
using Shop_Photo = BaoXin.Web.EF.Shop_Photo;
using Shop_Store = BaoXin.Web.EF.Shop_Store;

namespace BaoXin.Web.Areas.Shop.Controllers
{
    public class ShopController : BaseWebController
    {
        //
        // GET: /Shop/Shop/

        #region 申请开店视图
        public ActionResult Apply()
        {

            if (BuildFactory.GoodsFactory().ExamExit(((UsersEntity)Session["User"]).NickName)!=null)
            {
                return Content("<script>alert('你已申请过店铺，请务重复申请！！！');location.href = '/index';</script>");
            }
            else
            {
                return View();
            }

           
        } 
        #endregion
        #region 申请信息上传到数据库
        [HttpPost]
        public ContentResult DoApply(Shop_Photo photo, Shop_People people, Shop_Store shop)
        {
            #region 上传图片
            //上传和返回(保存到数据库中)的路径
            string uppath = string.Empty;
            string savepath = string.Empty;
            if (Request.Files.Count > 0)
            {
                //  HttpPostedFileBase imgFile = Request.Files["imgName"];
                //if (imgFile != null)
                //{
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    //创建图片新的名称
                    string nameImg = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    //获得上传图片的路径
                    // string strPath = imgFile.FileName[i].ToString();
                    var httpPostedFileBase = Request.Files[i];
                    if (httpPostedFileBase != null)
                    {
                        string strPath = httpPostedFileBase.FileName.ToString();
                        //获得上传图片的类型(后缀名)
                        string type = strPath.Substring(strPath.LastIndexOf(".") + 1).ToLower();
                        if (ValidateImg(type))
                        {
                            //拼写数据库保存的相对路径字符串
                            savepath = "../../Uploads/Files/";
                            savepath += nameImg + "." + type;
                            //拼写上传图片的路径
                            uppath = Server.MapPath("~/Uploads/Files/");
                            uppath += nameImg + "." + type;
                            //上传图片
                            httpPostedFileBase.SaveAs(uppath);
                        }
                        else
                        {
                            return Content("<script>alert('请上传正确的格式图片');location.href = '/shop/shop/apply';</script>");
                        }


                    }
                    photo.PhotoURL = savepath;
                    photo.PhontName = nameImg;
                    photo.BoolYX = true;

                    //}

                }

            }
            #endregion

            shop.StoreUser =((UsersEntity)Session["User"]).NickName;
            shop.AddTime = DateTime.Now;
            people.BoolLX = true;
            shop.BoolYX = true;
            shop.StoreOrde = 0;
            if (BuildFactory.AppylyStroeFactory().Apply(photo, people, shop))
            {
                return Content("<script>alert('请等待管理员审核通知审核结果将发至您的邮箱,请注意查收.');location.href = '/index';</script>");
            }
            ;

            return Content("<script>alert('申请失败，请重新申请');location.href = '/shop/shop/apply';</script>");

            // return null();
        } 
        #endregion


        #region 店铺选择视图
        public ActionResult SelectShop()
        {
            ViewBag.count = BuildFactory.AppylyStroeFactory().QueryShop().Count;


            var str = string.Empty;
            var city = string.Empty;
            if (Request["key"] != null)
            {
                str = Request["key"];
            }
            if (Request["c"] != null)
            {
                city = Request["c"];
            }
            var result = new TResult<List<Entity.SpeechInfoEntity>>();
            SpeechInfoBLL bll = new SpeechInfoBLL();
            ViewBag.Citys = citys;
            result = bll.GetAllSpeechInfoList(str, city, null);

            ;

            return View(result);
          
        } 
        #endregion


        #region 店铺数据
        public JsonResult ShopJson()
        {
            return Json(BuildFactory.AppylyStroeFactory().QueryShop(), JsonRequestBehavior.AllowGet);
        } 
        #endregion

        #region   录入商品视图
        public ActionResult AddGoods()
        {
            if (Session["User"] == null)
            {
                return Redirect("/index");
            }
            else
            {
                return View();
            }
        }
        #endregion
        #region   录入商品
        public ActionResult DoAddGoods(Shop_Photo photo,Shop_Goods goods)
        {
            #region 上传图片
            //上传和返回(保存到数据库中)的路径
            string uppath = string.Empty;
            string savepath = string.Empty;
            if (Request.Files.Count > 0)
            {
                //  HttpPostedFileBase imgFile = Request.Files["imgName"];
                //if (imgFile != null)
                //{
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    //创建图片新的名称
                    string nameImg = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    //获得上传图片的路径
                    // string strPath = imgFile.FileName[i].ToString();
                    var httpPostedFileBase = Request.Files[i];
                    if (httpPostedFileBase != null)
                    {
                        string strPath = httpPostedFileBase.FileName.ToString();
                        //获得上传图片的类型(后缀名)
                        string type = strPath.Substring(strPath.LastIndexOf(".") + 1).ToLower();
                        if (ValidateImg(type))
                        {
                            //拼写数据库保存的相对路径字符串
                            savepath = "../../../Uploads/Files/";
                            savepath += nameImg + "." + type;
                            //拼写上传图片的路径
                            uppath = Server.MapPath("~/Uploads/Files/");
                            uppath += nameImg + "." + type;
                            //上传图片
                            httpPostedFileBase.SaveAs(uppath);
                        }
                        else
                        {
                            return Content("<script>alert('请上传正确的格式图片');location.href = '/shop/shop/apply';</script>");
                        }


                    }
                    photo.PhotoURL = savepath;
                    photo.PhontName = nameImg;
                    photo.BoolYX = true;

                    //}

                }

            }
            #endregion

            goods.StoreId = Convert.ToInt32(Session["stroeid"]);
            if (BuildFactory.GoodsFactory().Apply(goods,photo))
            {
                return Content("<script>alert('添加成功');window.opener=null;window.close();</script>");
            }
            ;

            return Content("<script>alert('添加失败，请重新申请');location.href = '/shop/shop/addgoods';</script>");



        }
        #endregion


        #region   我的店铺
        public ActionResult MyShop(int id)
        {


            if (Session["User"] == null)
            {
                return Redirect("/index");
            }
           
            double pageCount = (double)(BuildFactory.GoodsFactory().Count(id)) / 12;//每页10
            pageCount = Math.Ceiling(pageCount);
            ViewBag.PageCout = pageCount;//一共多少页 

            ViewBag.Count = BuildFactory.GoodsFactory().Count(id);
            Session["stroeid"] = id;




            ViewBag.count = BuildFactory.AppylyStroeFactory().QueryShop().Count;


            var str = string.Empty;
            var city = string.Empty;
            if (Request["key"] != null)
            {
                str = Request["key"];
            }
            if (Request["c"] != null)
            {
                city = Request["c"];
            }
            var result = new TResult<List<Entity.SpeechInfoEntity>>();
            SpeechInfoBLL bll = new SpeechInfoBLL();
            ViewBag.Citys = citys;
            result = bll.GetAllSpeechInfoList(str, city, null);

            ;

            return View(result);
           
        }



        public ActionResult MyShop1(int id)
        {
            double pageCount = (double)(BuildFactory.GoodsFactory().Count(id)) / 12;//每页10
            pageCount = Math.Ceiling(pageCount);
            ViewBag.PageCout = pageCount;//一共多少页 

            ViewBag.Count = BuildFactory.GoodsFactory().Count(id);
            Session["stroeid"] = id;


            ViewBag.count = BuildFactory.AppylyStroeFactory().QueryShop().Count;


            var str = string.Empty;
            var city = string.Empty;
            if (Request["key"] != null)
            {
                str = Request["key"];
            }
            if (Request["c"] != null)
            {
                city = Request["c"];
            }
            var result = new TResult<List<Entity.SpeechInfoEntity>>();
            SpeechInfoBLL bll = new SpeechInfoBLL();
            ViewBag.Citys = citys;
            result = bll.GetAllSpeechInfoList(str, city, null);

            ;

            return View(result);
            
        }
        #endregion


        #region 分页得到商品信息
        public JsonResult GetGoods(int page = 1, int rows = 12)
        {


            return Json(BuildFactory.GoodsFactory().PagGoods(page, rows, Convert.ToInt32(Session["stroeid"])), JsonRequestBehavior.AllowGet);
        } 
	  #endregion


        #region   商品详情视图
        public ActionResult Buy(int id)
        {
            ViewBag.goodsid = id;
            return View();
        }
        #endregion

        public ActionResult Exam()
        {

          ShopModel shopModel=  BuildFactory.AppylyStroeFactory().QueryStoreidBy(((UsersEntity) Session["User"]).NickName);
          if (shopModel!=null&&shopModel.StoreId > 0)
          {
              return Redirect("/shop/shop/MyShop/" + shopModel.StoreId);
          }
          else
          {
              return Content("<script>alert('请先申请开店！！！');location.href = '/index';</script>"); 
          }
            ;
        }

        #region 得到店信息
        public JsonResult GetBuyJosn()
        {


            return Json(BuildFactory.GoodsFactory().Buyer(Convert.ToInt32(Session["stroeid"])), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 得到商品信息
        public JsonResult GetGoodsJosn(int id)
        {


            return Json(BuildFactory.GoodsFactory().GoodsDatil(id), JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        public ActionResult DoOrder(Shop_Goods goods, Shop_Store store, Shop_People people, Shop_Order order, int stock)
        {
            BuyGoods buyGoods= BuildFactory.GoodsFactory().GetBuyTotl(goods.GoodsId);

            if (buyGoods != null && (buyGoods.buyNum + order.buyNum) > stock)
            {

                return Content("<script>alert('订单数已超库存，库存尚余" + (stock - buyGoods.buyNum) + "个，请调整订单数或联系商家！');;</script>");
            }
           
           






            if (BuildFactory.GoodsFactory().GenOrder(people,store,order))
            {
                return Content("<script>alert('已发送购买信息，卖家收到信息后会进行联系确认');location.href = 'https://www.alipay.com/'</script>");
            }
            ;

            return Content("<script>alert('购买信息填写出错，请重新填写');;</script>");
        }


        [HttpPost]
        public ActionResult DeleteGoods(int id)
        {
            if (BuildFactory.GoodsFactory().DeleteGood(id))
            {
                return Content("OK");
            }
            ;

            return Content("FAILE");
        }


        public ActionResult ModifyGoods(int id)
        {
            ViewBag.goodsid = id;
            return View();
        }
        public JsonResult Modify(int id)
        {


            return Json(BuildFactory.GoodsFactory().ModifyGoods(id), JsonRequestBehavior.AllowGet);
        }


        public ActionResult DoModifyGoods(Shop_Photo photo, Shop_Goods goods)
        {
            #region 上传图片
            //上传和返回(保存到数据库中)的路径
            string uppath = string.Empty;
            string savepath = string.Empty;
            if (Request.Files.Count > 0)
            {
                //  HttpPostedFileBase imgFile = Request.Files["imgName"];
                //if (imgFile != null)
                //{
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    //创建图片新的名称
                    string nameImg = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    //获得上传图片的路径
                    // string strPath = imgFile.FileName[i].ToString();
                    var httpPostedFileBase = Request.Files[i];
                    if (httpPostedFileBase != null && httpPostedFileBase.FileName!="")
                    {
                        string strPath = httpPostedFileBase.FileName.ToString();
                        //获得上传图片的类型(后缀名)
                        string type = strPath.Substring(strPath.LastIndexOf(".") + 1).ToLower();
                        if (ValidateImg(type))
                        {
                            //拼写数据库保存的相对路径字符串
                            savepath = "../../../Uploads/Files/";
                            savepath += nameImg + "." + type;
                            //拼写上传图片的路径
                            uppath = Server.MapPath("~/Uploads/Files/");
                            uppath += nameImg + "." + type;
                            //上传图片
                            httpPostedFileBase.SaveAs(uppath);
                        }
                        else
                        {
                            return Content("<script>alert('请上传正确的格式图片');location.href = '/shop/shop/selectshop'</script>");
                        }


                    }
                    photo.PhotoURL = savepath;
                    photo.PhontName = nameImg;
                    photo.BoolYX = true;

                    //}

                }

            }
            #endregion

         
            if (BuildFactory.GoodsFactory().Modify(goods, photo))
            {
                return Content("<script>alert('修改成功');location.href = '/shop/shop/selectshop'</script>");
            }
            ;

            return Content("<script>alert('修改失败，请重新修改');location.href = '/shop/shop/selectshop'</script>");



        }
        #region 判断是否为Image类型文件
        //判断是否为Image类型文件
        private bool ValidateImg(string imgName)
        {
            string[] imgType = new string[] { "gif", "jpg", "png", "bmp" };

            int i = 0;
            bool blean = false;
            string message = string.Empty;

            //判断是否为Image类型文件
            while (i < imgType.Length)
            {
                if (imgName.Equals(imgType[i].ToString()))
                {
                    blean = true;
                    break;
                }
                else if (i == (imgType.Length - 1))
                {
                    break;
                }
                else
                {
                    i++;
                }
            }
            return blean;
        }
        #endregion
    }
}
