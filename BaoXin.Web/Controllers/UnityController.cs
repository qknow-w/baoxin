using BaoXin.Core;
using BaoXin.Web.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaoXin.Web.Controllers
{
    public class UnityController : BaseWebController
    {
        #region 文件上传(图片)
        /// <summary>
        /// 图片上传处理ashx
        /// </summary>
        /// <returns></returns>
        public ContentResult Upload()
        {
            HttpPostedFileBase file = Request.Files["Filedata"];//接收文件.
            if (file == null)
            {
                return Content("文件为空,请重试!");
            }
            string fileName = Path.GetFileName(file.FileName);//获取文件名.
            string fileExt = Path.GetExtension(fileName);//获取文件后缀名
            if (fileExt == ".jpg" || fileExt == ".png")
            {
                string dir = "/Uploads/Images/origin/" + DateTime.Now.ToString("yyyyMMddHHmm")+"/";
                Directory.CreateDirectory(Path.GetDirectoryName(Server.MapPath(dir)));//创建文件夹
                string fullDir = dir + Guid.NewGuid().ToString() + fileExt;
                string originPath = Server.MapPath(fullDir);
                using (Image img = Image.FromStream(file.InputStream))//根据上传的图片创建一个Image
                {
                    file.SaveAs(originPath);//保存到物理地址,绝对地址
                    string smaldir = "/Uploads/Images/small/" + DateTime.Now.ToString("yyyyMMddHHmm") + "/";
                    Directory.CreateDirectory(Path.GetDirectoryName(Server.MapPath(smaldir)));//创建文件夹

                    string smallImgPath = smaldir + DateTime.Now.ToString("yyyyMMddHHmm ") + fileExt;
                    string smallPath = Request.MapPath(smallImgPath);
                    ImageHelper.MakeThumbnail(originPath,
                               smallPath,
                               169,
                               168,
                               "W");//生成缩略图
                    return Content(smallImgPath);
                }
            }
            else
            {
                return Content("文件格式错误!!");
            }

        }
        #endregion
        #region 生成验证码
        public ActionResult ValidateCode()
        {
            return View();
        }

        #endregion

        #region 文件上传(图片)
        /// <summary>
        /// 图片上传处理ashx
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadSpeechImage()
        {
            HttpPostedFileBase file = Request.Files["Filedata"];//接收文件.
            if (file == null)
            {
                return AjaxResult("err", "文件为空,请重试!", false);
            }
            string fileName = Path.GetFileName(file.FileName);//获取文件名.
            string fileExt = Path.GetExtension(fileName);//获取文件后缀名
            if (fileExt == ".jpg" || fileExt == ".png")
            {
                string dir = "/Uploads/Images/origin/" + DateTime.Now.ToString("yyyyMMddHHmm") + "/";
                Directory.CreateDirectory(Path.GetDirectoryName(Server.MapPath(dir)));//创建文件夹
                string fullDir = dir + Guid.NewGuid().ToString() + fileExt;
                string originPath = Server.MapPath(fullDir);
                using (Image img = Image.FromStream(file.InputStream))//根据上传的图片创建一个Image
                {
                    file.SaveAs(originPath);//保存到物理地址,绝对地址
                    string smaldir = "/Uploads/Images/small/" + DateTime.Now.ToString("yyyyMMddHHmm") + "/";
                    Directory.CreateDirectory(Path.GetDirectoryName(Server.MapPath(smaldir)));//创建文件夹

                    string smallImgPath = smaldir + DateTime.Now.ToString("yyyyMMddHHmm ") + fileExt;
                    string smallPath = Request.MapPath(smallImgPath);
                    ImageHelper.MakeThumbnail(originPath,
                               smallPath,
                               241,
                               167,
                               "W");//生成缩略图
                    return AjaxResult("ok", smallImgPath, false);
                }
            }
            else
            {
                return AjaxResult("err", "文件格式错误!!", false);
            }

        }
        #endregion


    }
}
