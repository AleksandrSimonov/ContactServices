using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace ConsoleApp1.ADO.NET
{
   public class InsertData
    {
        private SqlConnectionStringBuilder _ConnectionString;
        private string _SqlContact;
        private string _SqlOrganization;
        public InsertData(SqlConnectionStringBuilder connectionString)
        {
            _ConnectionString = connectionString;

            _SqlContact = string.Format("Insert Into Contact" +
                  "(Name, Surname, Lastname, Sex, PhoneNumber, Birthday, TaxId, Post, Job) " +
                  "Values(@Name, @Surname, @Lastname, @Sex, @PhoneNumber, @Birthday, @TaxId, @Post, @Job)");

            _SqlOrganization = string.Format("Insert Into Organization" +
                  "(Name, PhoneNumber) " +
                  "Values(@Name, @PhoneNumber)");
        }

        public void InsertToContact(string Name, string Surname, string Lastname, string Sex, string PhoneNumber,
                                    DateTime Birthday, string TaxId, string Post, string Job)
        {
            try
            {
                using (var connction = new SqlConnection())
                {
                    connction.ConnectionString = _ConnectionString.ToString();
                    connction.Open();

                    var cmd = new SqlCommand(_SqlContact, connction);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Surname", Surname);
                    cmd.Parameters.AddWithValue("@Lastname", Lastname);
                    cmd.Parameters.AddWithValue("@Sex", Sex);
                    cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                    cmd.Parameters.AddWithValue("@Birthday", Birthday);
                    cmd.Parameters.AddWithValue("@TaxId", TaxId);
                    cmd.Parameters.AddWithValue("@Post", Post);
                    cmd.Parameters.AddWithValue("@Job", Job);

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }catch(SqlException sqlEx)
            {
                
            }
        }
        public void InsertToOrganization(string Name, string PhoneNumber)
        {
            try
            {
                using (var connction = new SqlConnection())
                {
                    connction.ConnectionString = _ConnectionString.ToString();
                    connction.Open();

                    var cmd = new SqlCommand(_SqlOrganization, connction);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (SqlException sqlEx)
            {

            }
        }
    }
}
