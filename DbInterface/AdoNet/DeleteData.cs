using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DbInterface.AdoNet
{
   internal class DeleteData
    {
        private readonly SqlConnectionStringBuilder _ConnectionString;
        private readonly string _SqlContact;
        private readonly string _SqlOrganization;

        public DeleteData(SqlConnectionStringBuilder connectionString)
        {
            _ConnectionString = connectionString;
        }
        public void DeleteFromContact(string ITN)
        {
            
        }
        public void DeleteFromOrganization(string phoneNumber)
        {
            
        }
    }
}
