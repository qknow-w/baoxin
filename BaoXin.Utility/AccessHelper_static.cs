/*
    AccessHelper static members
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace BaoXin.Utility
{
    public partial class AccessHelper
    {
        static string get_defualt_dbpath()
        {
            return Path.GetDirectoryName(Assembly.GetAssembly(typeof(AccessHelper)).Location)+"\\db.mdb";
        }

        public static string SafeText(string T)
        {
            return T.Replace("'", "''");
        }
    }
}
