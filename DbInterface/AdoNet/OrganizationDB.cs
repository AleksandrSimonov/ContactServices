using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Contact;

namespace DbInterface.AdoNet
{
   public class OrganizationDB
    {
        private SqlConnectionStringBuilder _ConnectionString;
        public OrganizationDB(string dataSource)
        {
            _ConnectionString = new SqlConnectionStringBuilder();
            _ConnectionString.DataSource = dataSource;
            _ConnectionString.InitialCatalog = "Contacts";
            _ConnectionString.IntegratedSecurity = true;
            _ConnectionString.Pooling = false;
        }

        public List<Organization> GetAllOrganizations()
        {
            string sql = string.Format($"Select* from Organization");
            var jobs = new List<Organization>();
            try
            {
                using (var connction = new SqlConnection())
                {
                    connction.ConnectionString = _ConnectionString.ToString();
                    connction.Open();

                    var cmd = new SqlCommand(sql, connction);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())//есть ли данные
                    {

                        var id = Convert.ToInt32(reader["ID"]);
                        var name = Convert.ToString(reader["Name"]);
                        var phoneNumber = Convert.ToString(reader["PhoneNumber"]);

                        jobs.Add(new Organization(id, name, phoneNumber));
                    }

                    cmd.Dispose();
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            return jobs;
        }
        public void InsertOrganization(string name, string phoneNumber)
        {
            var sqlOrganization = string.Format("Insert Into Organization" +
                  "(Name, PhoneNumber) " +
                  "Values(@Name, @PhoneNumber)");
            string sqlUpdateOrganization = string.Format($"Update Organization Set Name = @Name, PhoneNumber = @PhoneNumber Where ID = @ID");
            try
            {
                using (var connction = new SqlConnection())
                {
                    connction.ConnectionString = _ConnectionString.ToString();
                    connction.Open();

                    var cmd = new SqlCommand(sqlOrganization, connction);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
        }
        public void DeleteOrganization(string id)
        {
            string sql = string.Format($"Delete from Organization where ID = '{id}'");

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
        public Organization GetOrganization(int id)
        {
            string sql = string.Format($"Select* from Organization where ID = '{id}'");
            Organization job = null;
            try
            {
                using (var connction = new SqlConnection())
                {
                    connction.ConnectionString = _ConnectionString.ToString();
                    connction.Open();

                    var cmd = new SqlCommand(sql, connction);
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)//есть ли данные
                    {
                        reader.Read();

                        var name = Convert.ToString(reader["Name"]);
                        var phoneNumber = Convert.ToString(reader["PhoneNumber"]);

                        job = new Organization(id, name, phoneNumber);
                    }

                    cmd.Dispose();
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            return job;
        }
    }
}
