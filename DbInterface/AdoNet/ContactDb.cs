using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Contact;

namespace DbInterface.AdoNet
{
  public  class ContactDB
    {
        private SqlConnectionStringBuilder _ConnectionString;
        private readonly string _DataSource;

        public ContactDB(string dataSource)
        {
            _ConnectionString = new SqlConnectionStringBuilder();
            _DataSource = dataSource;
            _ConnectionString.DataSource = dataSource;
            _ConnectionString.InitialCatalog = "Contacts";
            _ConnectionString.IntegratedSecurity = true;
            _ConnectionString.Pooling = false;
        }
      
        public List<Contact.Contact> GetContacts(string surnamePart, string namePart)
        {
            string sql = string.Format($"SELECT* FROM Contacts.dbo.Contact where Contact.Name like N'{namePart}%' " +
                    $"and Surname like N'{surnamePart}%'");

            var contacts = new List<Contact.Contact>();
            try
            {
                using (var connction = new SqlConnection())
                {
                    connction.ConnectionString = _ConnectionString.ToString();
                    connction.Open();

                    var cmd = new SqlCommand(sql, connction);
                    var reader = cmd.ExecuteReader();

                    var organization = new OrganizationDB(_DataSource);

                    while (reader.Read())//есть ли данные
                    {
                        var id = Convert.ToInt32(reader["ID"]);
                        var sex = (SexEnum)Convert.ToInt32(reader["Sex"]);
                        var phoneNumber = Convert.ToString(DbNull.IsDbNull(reader["PhoneNumber"]));
                        var post = Convert.ToString(DbNull.IsDbNull(reader["Post"]));
                        var jobId = Convert.ToInt32(DbNull.IsDbNull(reader["Job"]));
                        var job = organization.GetOrganization(jobId);
                        var ITN = Convert.ToString(reader["ITN"]);
                        var birthday = Convert.ToDateTime(reader["Birthday"]);
                        var name = Convert.ToString(reader["Name"]);
                        var surname = Convert.ToString(reader["Surname"]);
                        var lastname = Convert.ToString(DbNull.IsDbNull(reader["Lastname"]));

                        contacts.Add(new Contact.Contact(id, name, surname, lastname, sex, phoneNumber, birthday, ITN, post, job));
                    }
                    cmd.Dispose();
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            return contacts;
        }

        public void InsertOrUpdateContact(int id, string Name, string Surname, string Lastname, int Sex, string PhoneNumber,
                                    DateTime Birthday, string ITN, string Post, int Job)
        {
            var insertSqlContact = string.Format("Insert Into Contact" +
                  "(Name, Surname, Lastname, Sex, PhoneNumber, Birthday, ITN, Post, Job) " +
                  "Values(@Name, @Surname, @Lastname, @Sex, @PhoneNumber, @Birthday, @ITN, @Post, @Job)");

            var updateSqlContact = string.Format("Update Contact Set " +
               "Name = @Name, Surname = @Surname, Lastname = @Lastname, Sex = @Sex," +
               "PhoneNumber = @PhoneNumber, Birthday = @Birthday, ITN = @ITN, Post = @Post, Job = @Job " +
               "where ITN = @ITN");

            try
            {
                using (var connction = new SqlConnection())
                {
                    connction.ConnectionString = _ConnectionString.ToString();
                    connction.Open();


                    string sql = id > 0 ? updateSqlContact : insertSqlContact;
                    SqlCommand cmd = new SqlCommand(sql, connction);
                  
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Surname", Surname);
                    cmd.Parameters.AddWithValue("@Lastname", DbNull.TryToDbNull(Lastname));
                    cmd.Parameters.AddWithValue("@Sex", Sex);
                    cmd.Parameters.AddWithValue("@PhoneNumber", DbNull.TryToDbNull(PhoneNumber));
                    cmd.Parameters.AddWithValue("@Birthday", Birthday);
                    cmd.Parameters.AddWithValue("@ITN", ITN);
                    cmd.Parameters.AddWithValue("@Post", DbNull.TryToDbNull(Post));
                    cmd.Parameters.AddWithValue("@Job", Convert.ToString(DbNull.TryToDbNull(Job)));

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
        }

        public void DeleteContact(string id)
        {
            string sql = string.Format($"Delete from Contact where ID = '{id}'");

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
