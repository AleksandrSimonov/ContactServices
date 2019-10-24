using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Contact;

namespace DbInterface.AdoNet
{
   internal class GetData
    {
        private SqlConnectionStringBuilder _ConnectionString;
        private string _SqlContact;
        private string _SqlOrganization;
        public GetData(SqlConnectionStringBuilder connectionString)
        {
            _ConnectionString = connectionString;
        }
    
        

    }
}
