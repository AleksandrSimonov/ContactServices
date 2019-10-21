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

            if ((obj == null)||(Convert.ToInt32( obj)==-1))
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
