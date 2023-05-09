using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database_oop_project
{
    public class User
    {
        public string user_login { get; set; }
        public string user_password { get; set; }
        public string user_fullName { get; set; }
        public int user_age { get; set; }
        public bool user_entry { get; set; }

        DataBase database = new DataBase();

        public User(string user_login, string user_password, string user_fullName, int user_age) 
        {
            this.user_login = user_login;
            this.user_password = user_password; 
            this.user_fullName = user_fullName; 
            this.user_age = user_age;
            this.user_entry = false;
        }

        public User(string user_login, string user_password)
        {
            this.user_login = user_login;
            this.user_password = user_password;
            this.user_fullName = "";
            this.user_age = 0;
        }

        public void Sign_in()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            
            string query_string =
            $"select id_user , user_login ,user_full_name, user_password from register where user_login = '{user_login}' and user_password = '{user_password}'";
            SqlCommand command = new SqlCommand(query_string, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                user_entry = true;
            }
            else
            {
                Console.WriteLine("No such account!");    
            }
        }

        public void Sign_up()
        {
            database.openConnection();
            

            if (!existingUser())
            {
                string query_string =
            $"insert into register(user_login , user_full_name,  user_age , user_password)  values('{user_login}', '{user_fullName}', '{user_age}', '{user_password}')";

                SqlCommand command = new SqlCommand(query_string, database.getConnection());



                if (command.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("success");
                }
                else
                {
                    Console.WriteLine("error!");
                }
                database.closeConnection();
            }
            else
            {
                Console.WriteLine("acc already exist!");
            }
        }

        public Boolean existingUser()
        {

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string query_string =
           $"select id_user , user_login , user_full_name, user_password from register where user_login = '{user_login}'";
            SqlCommand command = new SqlCommand(query_string, database.getConnection());


            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public async void GetFullName()
        {
            database.openConnection();
            SqlDataReader adapter = null;

            string query_string =
           $"select id_user , user_login , user_full_name , user_password from register where user_login = '{user_login}' and user_password = '{user_password}'";
            SqlCommand command = new SqlCommand(query_string, database.getConnection());

            adapter = await command.ExecuteReaderAsync();  
            adapter.Read();
            user_fullName = Convert.ToString(adapter["user_full_name"]);

            database.closeConnection();
        }
    }
}
