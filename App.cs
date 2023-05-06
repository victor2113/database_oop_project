using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace database_oop_project
{
    public class App
    {
        DataBase database = new DataBase();
        public User user { get; set; }
        public void Begin()
        {
            
            Console.CursorVisible = false;
            string promt = @"
Use the arrow keys to choose options and press enter to select one";



            string[] options = { "Sign_in", "Sign_up", "Exit" };
            StartMenu startMenu = new StartMenu(options, promt);
            int selectedIndex = startMenu.Run();




            switch (selectedIndex)
            {
                case 0:
                    Sign_in(user);
                    break;
                case 1:
                    Sign_up(user);
                    break;
                case 2:
                    Exit();
                    break;
            }

            Console.ReadKey(true);

        }
        private void Exit()
        {
            Console.Clear();
            Console.WriteLine("\nPress any key to exit....");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
        private void Sign_in(User user)
        {   
            string userLogin = "";
            string userPassword = "";


            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            
            
            Console.Clear();
            Console.WriteLine("Enter login");
            while (true)
            {
                ConsoleKeyInfo c = Console.ReadKey();
                Console.Clear();
                if (c.Key == ConsoleKey.Enter)
                    break;
                if (c.Key == ConsoleKey.Backspace)
                {
                    if (userLogin.Length > 0)
                        userLogin = userLogin.Remove(userLogin.Length - 1);
                }
                else
                    userLogin += c.KeyChar;
                Console.Write(userLogin);
               
            }
            Console.Clear() ;
            Console.WriteLine("Enter pass");
            while (true)
            {
                ConsoleKeyInfo c = Console.ReadKey();
                Console.Clear();
                if (c.Key == ConsoleKey.Enter)
                    break;
                if (c.Key == ConsoleKey.Backspace)
                {
                    if (userPassword.Length > 0)
                        userPassword = userPassword.Remove(userPassword.Length - 1);
                }
                else
                    userPassword += c.KeyChar;
                foreach (char ch in userPassword)
                    Console.Write('*');
            }
            Console.WriteLine("Login: {0}", userLogin);
            Console.WriteLine("Pass: {0}", userPassword);
           



            string query_string =
            $"select id_user , user_login , user_password from register where user_login = '{userLogin}' and user_password = '{userPassword}'";
            SqlCommand command = new SqlCommand(query_string , database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if(table.Rows.Count == 1)
            {
                user.Entry();
            }
            else
            {
                Console.WriteLine("No such acc!");
            }    

            Console.ReadKey(true);
            Begin();
        }

        private void Sign_up(User user)
        {
            string userLogin = "";
            string userFullName = "";
            string userAge = "";
            string userPassword = "";
            string checkPassword = "";
            database.openConnection();
            Console.Clear();
            Console.WriteLine("Enter login");
            while (true)
            {
                ConsoleKeyInfo c = Console.ReadKey();
                Console.Clear();
                if (c.Key == ConsoleKey.Enter)
                    break;
                if (c.Key == ConsoleKey.Backspace)
                {
                    if (userLogin.Length > 0)
                        userLogin = userLogin.Remove(userLogin.Length - 1);
                }
                else
                    userLogin += c.KeyChar;
                Console.Write(userLogin);

            }
            Console.Clear();
            Console.WriteLine("Enter your full name");
            while (true)
            {
                ConsoleKeyInfo c = Console.ReadKey();
                Console.Clear();
                if (c.Key == ConsoleKey.Enter)
                    break;
                if (c.Key == ConsoleKey.Backspace)
                {
                    if (userFullName.Length > 0)
                        userFullName = userFullName.Remove(userFullName.Length - 1);
                }
                else
                    userFullName += c.KeyChar;
                Console.Write(userFullName);

            }
            Console.Clear();
            Console.WriteLine("Enter your age");
            while (true)
            {
                ConsoleKeyInfo c = Console.ReadKey();
                Console.Clear();
                if (c.Key == ConsoleKey.Enter)
                    break;
                if (c.Key == ConsoleKey.Backspace)
                {
                    if (userAge.Length > 0)
                        userAge = userAge.Remove(userAge.Length - 1);
                }
                else
                    userAge += c.KeyChar;
                Console.Write(userAge);

            }
            Console.Clear();
            Console.WriteLine("Enter password");
            while (true)
            {
                ConsoleKeyInfo c = Console.ReadKey();
                Console.Clear();
                if (c.Key == ConsoleKey.Enter)
                    break;
                if (c.Key == ConsoleKey.Backspace)
                {
                    if (userPassword.Length > 0)
                        userPassword = userPassword.Remove(userPassword.Length - 1);
                }
                else
                    userPassword += c.KeyChar;
                foreach (char ch in userPassword)
                    Console.Write('*');
            }

            Console.Clear();
            Console.WriteLine("Confirm password");
            while (true)
            {
                ConsoleKeyInfo c = Console.ReadKey();
                Console.Clear();
                if (c.Key == ConsoleKey.Enter)
                    break;
                if (c.Key == ConsoleKey.Backspace)
                {
                    if (checkPassword.Length > 0)
                        checkPassword = checkPassword.Remove(checkPassword.Length - 1);
                }
                else
                    checkPassword += c.KeyChar;
                foreach (char ch in checkPassword)
                    Console.Write('*');
            }
            if (userPassword != checkPassword)
            {
                Console.WriteLine("Password mismatch");
                Console.ReadKey(true);
                Sign_up(user);

            }
            else
            {
                Console.WriteLine("Login: {0}", userLogin);
                Console.WriteLine("Full name: {0}", userFullName);
                Console.WriteLine("Your age: {0}", userAge);
            }

            if(!existingUser(userLogin, userPassword))
            {
                string query_string =
            $"insert into register(user_login , user_full_name,  user_age , user_password)  values('{userLogin}', '{userFullName}', '{userAge}', '{userPassword}')";

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

                Console.ReadKey(true);
                Sign_in(user);
            }
            else { 
                Console.WriteLine("acc already exist!");
                Console.ReadKey(true);
                Begin();
            }

            
        }
        
       private Boolean existingUser(string login,string password)
        {
            string userLogin = "";
            string userPassword = "";

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string query_string =
           $"select id_user , user_login , user_password from register where user_login = '{userLogin}' and user_password = '{userPassword}'";
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
    }
}
