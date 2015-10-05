using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using BaoXin.Entity;

namespace BaoXin.DAL
{


    public class SchedulerConfiguration
    {
        private int sleepInterval;
        private ArrayList jobs = new ArrayList();
        public int SleepInterval
        {
            get { return sleepInterval; }
        }
        public ArrayList Jobs
        {
            get { return jobs; }
        }
        public SchedulerConfiguration(int newSleepInterval)
        {
            sleepInterval = newSleepInterval;
        }
    }

    public class Scheduler
    {
        private SchedulerConfiguration configuration = null;
        public Scheduler(SchedulerConfiguration config)
        {
            configuration = config;
        }
        public void Start()
        {
            while (true)
            {
                try
                {
                    foreach (ISchedulerJob job in configuration.Jobs)
                    {
                        job.Execute();
                    }

                }
                catch { }
                finally
                {
                    Thread.Sleep(configuration.SleepInterval);
                }
            }
        }
    }
    public class SchedulerAgent
    {
        private static System.Threading.Thread schedulerThread = null;
        public static void StartAgent()
        {
            SchedulerConfiguration config = new SchedulerConfiguration(1000 * 60);//设置时间，此处为1分钟
            config.Jobs.Add(new AlertJob());
            Scheduler scheduler = new Scheduler(config);
            System.Threading.ThreadStart myThreadStart = new System.Threading.ThreadStart(scheduler.Start);
            schedulerThread = new System.Threading.Thread(myThreadStart);
            schedulerThread.Start();
        }
        public static void Stop()
        {
            if (null != schedulerThread)
            {
                schedulerThread.Abort();
            }
        }
    }
    public interface ISchedulerJob
    {
        void Execute();
    }
    public class AlertJob : ISchedulerJob
    {
      
        public void Execute()
        {
            //TimeSpan ts = DateTime.Now -
            //              Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Date + " 23:55:00");
            TimeSpan ts = DateTime.Now -
                     Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 23:58:00");
            //虽然可能会执行多次，但是相对timer要好些
            if (ts.Minutes >= 1 && ts.Minutes <= 3)
            {
                int num = UsersDAL.UpdateAll();
               // Log.LogMsg("当前时间：" + DateTime.Now + "一共更新了" + num + "条记录！");
                //Content += "当前时间："+DateTime.Now+"一共更新了"+num+"条记录！";
            }
            else
            {
               // Log.LogMsg("当前时间：" + DateTime.Now + "我执行了");
            }

        }
    }

    public static class Log
    {
        public static void LogMsg(string msg)
        {
            string date = DateTime.Now.ToString("yyyyMMdd");
            string path = "/log/";
            //判断Log目录是否存在，不存在则创建
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + date + ".log";
            //使用StreamWriter写日志，包含时间，错误路径，错误信息
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("-----------------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-----------------");
                sw.WriteLine(msg);
                sw.WriteLine("\r\n");
            }
        }

    }
    //public static class SchedulerConfiguration
    //{
    //    private static string content = "";
    //    /// <summary>
    //    /// 输出信息存储的地方.
    //    /// </summary>
      
    //    /// <summary>
    //    /// 定时器委托任务 调用的方法
    //    /// </summary>
    //    /// <param name="source"></param>
    //    /// <param name="e"></param>
    //    public static void SetContent(object source, ElapsedEventArgs e)
    //    { 
    //        //当前时间为今天的最后一分钟的时候重置用户删除权限
            
    //        if (DateTime.Now.ToString("HH:mm:ss") == "23:59:00")
    //        {
    //            int num=UsersDAL.UpdateAll();
    //            //Content += "当前时间："+DateTime.Now+"一共更新了"+num+"条记录！";

    //        }
    //    }



        
       
    //}
}
