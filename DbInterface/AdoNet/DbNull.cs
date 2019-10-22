using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbInterface.AdoNet
{
   internal static class DbNull
    {
        public static object TryToDbNull(object obj)
        {
            var strObj = Convert.ToString(obj);
            int.TryParse(strObj, out int result);
            if ((obj == null)||(result==-1)||(strObj==""))
                return Convert.DBNull;
            else
                return obj;
        }

        public static object IsDbNull(object obj)
        {
            if (obj == null)
                return null;
            if (Convert.IsDBNull(obj))
                return null;
            else
                return obj;
        }
    }
}
