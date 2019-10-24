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
               "PhoneNumber = @PhoneNumber, Birthday = @Birthday, ITN = @ITN, Post = @Post, Job = @Job " +
               "where ITN = @ITN");
        }
        public void UpdateContact(string Name, string Surname, string Lastname, int Sex, string PhoneNumber,
                                    DateTime Birthday, string ITN, string Post, int Job)
        {
         
         
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
