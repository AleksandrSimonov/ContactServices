using System.Data.SqlClient;
using System;
using Contact;

namespace DbInterface.AdoNet
{
    internal class UpdateData
    {
        private SqlConnectionStringBuilder _ConnectionString;
        private string _SqlContact;
        private string _SqlOrganization;

        public UpdateData(SqlConnectionStringBuilder connectionString)
        {
            _ConnectionString = connectionString;
            _SqlContact = string.Format("Update Contact Set " +
               "Name = @Name, Surname = @Surname, Lastname = @Lastname, Sex = @Sex," +
               "PhoneNumber = @PhoneNumber, Birthday = @Birthday, TaxId = @TaxId, Post = @Post, Job = @Job " +
               "where TaxId = @TaxId");
        }
        public void UpdateContact(string Name, string Surname, string Lastname, int Sex, string PhoneNumber,
                                    DateTime Birthday, string TaxId, string Post, int Job)
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
                    cmd.Parameters.AddWithValue("@Lastname", DbNull.TryToDbNull(Lastname));
                    cmd.Parameters.AddWithValue("@Sex", Sex);
                    cmd.Parameters.AddWithValue("@PhoneNumber", DbNull.TryToDbNull(PhoneNumber));
                    cmd.Parameters.AddWithValue("@Birthday", Birthday);
                    cmd.Parameters.AddWithValue("@TaxId", TaxId);
                    cmd.Parameters.AddWithValue("@Post", DbNull.TryToDbNull(Post));
                    cmd.Parameters.AddWithValue("@Job", DbNull.TryToDbNull(Job));

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
        }
        public void UpdateOrganization(string column, string newValue, int id)
        {
            string sql = string.Format($"Update Contact Set {column} = '{newValue}' Where CarID = '{id}'");
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
