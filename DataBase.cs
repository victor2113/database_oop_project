using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace database_oop_project
{
    public class DataBase
    {
        SqlConnection connection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS;Initial Catalog  = clients;Integrated Security = true");

        public void openConnection()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public SqlConnection getConnection()
        {
           return connection;
        }
    }
}
