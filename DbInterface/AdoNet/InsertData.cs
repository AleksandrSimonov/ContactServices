using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;


namespace DbInterface.AdoNet
{
    internal class InsertData
    {
        private SqlConnectionStringBuilder _ConnectionString;
        private string _SqlContact;
        private string _SqlOrganization;
        public InsertData(SqlConnectionStringBuilder connectionString)
        {
            _ConnectionString = connectionString;

            _SqlContact = string.Format("Insert Into Contact" +
                  "(Name, Surname, Lastname, Sex, PhoneNumber, Birthday, ITN, Post, Job) " +
                  "Values(@Name, @Surname, @Lastname, @Sex, @PhoneNumber, @Birthday, @ITN, @Post, @Job)");

            _SqlOrganization = string.Format("Insert Into Organization" +
                  "(Name, PhoneNumber) " +
                  "Values(@Name, @PhoneNumber)");
        }

        public void InsertToContact(string Name, string Surname, string Lastname, int Sex, string PhoneNumber,
                                    DateTime Birthday, string ITN, string Post, int Job)
        {
           
        }
       
        public void InsertToOrganization(string Name, string PhoneNumber)
        {
            
        }
    }
}
