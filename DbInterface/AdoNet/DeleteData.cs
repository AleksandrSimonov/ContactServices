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
        private SqlConnectionStringBuilder _ConnectionString;
        private string _SqlContact;
        private string _SqlOrganization;

        public DeleteData(SqlConnectionStringBuilder connectionString)
        {
            _ConnectionString = connectionString;
        }
        public void DeleteFromContact(string taxId)
        {
            string sql = string.Format($"Delete from Contact where TaxId = '{taxId}'");

            try
            {
                using (var connction = new SqlConnection())
                {
                    connction.ConnectionString = _ConnectionString.ToString();
                    connction.Open();

                    var cmd = new SqlCommand(sql, connction);

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
        }
        public void DeleteFromOrganization(string phoneNumber)
        {
            string sql = string.Format($"Delete from Organization where PhoneNumber = '{phoneNumber}'");

            try
            {
                using (var connction = new SqlConnection())
                {
                    connction.ConnectionString = _ConnectionString.ToString();
                    connction.Open();

                    var cmd = new SqlCommand(sql, connction);

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
        }
    }
}
