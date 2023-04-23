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
        public void Begin()
        {
            Console.CursorVisible = false;
            string promt = @"
Use the arrow keys to choose options and press enter to select one";



            string[] options = { "Registration", "Registration", "Exit" };
            StartMenu startMenu = new StartMenu(options, promt);
            int selectedIndex = startMenu.Run();




            switch (selectedIndex)
            {
                case 0:
                    Registration();
                    break;
                case 1:
                    Registration();
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
        private void Registration()
        {   
            string userLogin = "";
            string userPassword = "";
            

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            
            
            Console.Clear();
            Console.WriteLine("Enter ;ogin");
            while (true)
            {
                ConsoleKeyInfo c = Console.ReadKey();
                Console.Clear();
                if (c.Key == ConsoleKey.Enter)
                    break;
                if (c.Key == ConsoleKey.Backspace)
                {
                    if (userLogin.Length > 0)
                        userLogin = userLogin.Remove(userPassword.Length - 1);
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
           



            string query_string = $"select id_user , user_login , user_password from register where user_login = '{userLogin}' and user_password = '{userPassword}'";
            SqlCommand command = new SqlCommand(query_string , database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if(table.Rows.Count == 1)
            {
                Console.WriteLine("No error!");
            }
            else
            {
                Console.WriteLine("No such acc!");
            }    

            Console.ReadKey(true);
            Begin();
        }


        private void RunTheGame()
        {
            Console.Clear();
            Console.WriteLine("bla bla");
            Console.ReadKey(true);
            Begin();
        }
       
    }
}
