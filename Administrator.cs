using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database_oop_project
{
    internal class Administrator : User
    {
        DataBase database = new DataBase();
        public Administrator(string user_login, string user_password) : base(user_login, user_password)
        {
            this.user_fullName = user_fullName;
            this.user_age = user_age;   
        }

        public void ClearTable()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string query_string =
            $"select id_user , user_login ,user_full_name, user_password from register where user_login = '{user_login}' and user_password = '{user_password}'";
            SqlCommand command = new SqlCommand(query_string, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count == 1)
            {
                user_entry = true;
            }
            else
            {
                Console.WriteLine("No such account!");
                Console.ReadKey(true);
            }
        }

    }
}
