using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.CompilerServices;

namespace database_oop_project
{
    public class App
    {
        DataBase database = new DataBase();
        User defaultUser = new User("", "", "", 0);
        User user;


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
                    user = Login();
                    user.Sign_in();
                    break;
                case 1:
                    user = Registration();
                    user.Sign_up();
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

        public User Login() 
        {
            string userLogin = "";
            string userPassword = "";

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

            User user = new User(userLogin, userPassword, "", 0);
            return user;
        }
        

        private User Registration()
        {
            string userLogin = "";
            string userFullName = "";
            string userAge = "";
            string userPassword = "";
            string checkPassword = "";
            int Age;


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
                Registration();

            }
            else
            {
                Console.WriteLine("Login: {0}", userLogin);
                Console.WriteLine("Full name: {0}", userFullName);
                Console.WriteLine("Your age: {0}", userAge);
            }

            Age = Convert.ToInt32(userAge);
            User user = new User(userLogin, userPassword, userFullName, Age);

            if (!user.existingUser())
            {

                Console.WriteLine("registration is successful");

                return user;
            }
            else
            {
                Console.WriteLine("acc already exist!");
                Console.ReadKey(true);
                Registration();
            }
            return defaultUser;

        }
        
       
    }
}
