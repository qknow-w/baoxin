using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

namespace 飞机订票系统MVC.Areas.Admin.Utilities.Zip
{
    public   class Zip
    {        
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileName">要压缩的所有文件（完全路径)</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="name">压缩后文件路径</param>
        /// <param name="Level">压缩级别</param>
        public static void ZipFileMain(string[] filenames, string[] fileName, string name, int Level)
        {
            #region MyRegion
            ZipOutputStream s = new ZipOutputStream(File.Create(name));
            Crc32 crc = new Crc32();
            //压缩级别
            s.SetLevel(Level); // 0 - store only to 9 - means best compression
            try
            {
                int m = 0;
                foreach (string file in filenames)
                {
                    //打开压缩文件
                    FileStream fs = File.OpenRead(file);//文件地址
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    //建立压缩实体
                    ZipEntry entry = new ZipEntry(fileName[m].ToString());//原文件名
                    //时间
                    entry.DateTime = DateTime.Now;
                    //空间大小
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
                    s.Write(buffer, 0, buffer.Length);
                    m++;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                s.Finish();
                s.Close();
            } 
            #endregion


        }
    }
}