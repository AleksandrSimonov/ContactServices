using System.Data.SqlClient;

namespace ConsoleApp1.ADO.NET
{
    internal class UpdateData
    {
        private SqlConnectionStringBuilder _ConnectionString;
        private string _SqlContact;
        private string _SqlOrganization;

        public UpdateData(SqlConnectionStringBuilder connectionString)
        {
            _ConnectionString = connectionString;
        }
        public void UpdateContact(string column, string newValue, int id)
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

            }
        }
    }
}
