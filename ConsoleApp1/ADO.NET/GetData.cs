using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Contact;

namespace ConsoleApp1.ADO.NET
{
    class GetData
    {
        private SqlConnectionStringBuilder _ConnectionString;
        private string _SqlContact;
        private string _SqlOrganization;
        public GetData(SqlConnectionStringBuilder connectionString)
        {
            _ConnectionString = connectionString;
        }
        public Contact.Contact GetContact(string taxId)
        {
            string sql = string.Format($"Select from Contact where TaxId = '{taxId}'");
            Contact.Contact contact = null; 
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

                        contact = new Contact.Contact((string) reader["Name"],
                                                      (string)reader["Surname"],
                                                     (string) reader["Lastname"],
                                                     (Contact.SexEnum) reader["Sex"],
                                                     (string) reader["PhoneNumber"],
                                                     (DateTime) reader["Birthday"],
                                                      (string) reader["TaxId"],
                                                     (string) reader["Post"],
                                                     GetOrganization((string)reader["Job"])
                                                     );
                    }

                    cmd.Dispose();
                }
            }
            catch (SqlException sqlEx)
            {

            }
            return contact;
        }
        public Organization GetOrganization(string phoneNumber)
        {
            string sql = string.Format($"Select from Organization where PhoneNumber = '{phoneNumber}'");
            Organization job=null;
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

                        job = new Organization((string) reader["Name"],
                                                   (string) reader["PhoneNumber"]);
                    }

                    cmd.Dispose();
                }
            }
            catch (SqlException sqlEx)
            {

            }
            return job;
        }
    }
}
