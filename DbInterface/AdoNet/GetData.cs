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
        public Contact.Contact GetContact(string surname, string name)
        {
            string sql = string.Format($"Select* from Contact where Name Like '{name}%' " +
                $"and Surname like '{surname}%'");
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

                        var x = (Contact.SexEnum)Convert.ToInt32(reader["Sex"]);
                        var x1 = (string)DbNull.IsDbNull(reader["PhoneNumber"]);
                        var x2 = (string)DbNull.IsDbNull(reader["Post"]);
                        var x3 = GetOrganization((int)DbNull.IsDbNull(reader["Job"]));
                        var x4 = (string)reader["TaxId"];
                        var x5 = (DateTime)reader["Birthday"];

                        var contact1 = new Contact.Contact("", "", "", x, null, x5, x4, x2, x3); 

                        contact = new Contact.Contact((string) reader["Name"],
                                                      (string)reader["Surname"],
                                                     (string)DbNull.IsDbNull(reader["Lastname"]),
                                                     (Contact.SexEnum) Convert.ToInt32(reader["Sex"]),
                                                     (string)DbNull.IsDbNull( reader["PhoneNumber"]),
                                                     (DateTime) reader["Birthday"],
                                                      (string) reader["TaxId"],
                                                     (string)DbNull.IsDbNull( reader["Post"]),
                                                     GetOrganization((int)DbNull.IsDbNull( reader["Job"]))
                                                     );
                    }

                    cmd.Dispose();
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            return contact;
        }

     

        public Organization GetOrganization(int id)
        {
            string sql = string.Format($"Select* from Organization where ID = '{id}'");
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

                        var x = reader["PhoneNamber"];

                        job = new Organization((string) reader["Name"],
                                               (string) reader["PhoneNamber"]);
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
