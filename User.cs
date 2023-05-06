using System;
using System.Collections.Generic;
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
        public string user_age { get; set; }

        public User(string user_login, string user_password, string user_fullName, string user_age) 
        {
            this.user_login = user_login;
            this.user_password = user_password; 
            this.user_fullName = user_fullName; 
            this.user_age = user_age;
        }

        public void Entry()
        {
            Console.CursorVisible = false;
            string promt = @"
Use the arrow keys to choose room and press enter to select one";

            DateTime date1 = DateTime.Today;

            string[] options = { "Red Room", "Kid Room", "Console Room" };
            StartMenu startMenu = new StartMenu(options, promt);
            int selectedIndex = startMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    RedRoom();
                    break;
                case 1:
                    KidRoom();
                    break;
                case 2:
                    ConsoleRoom();
                    break;
            }
        }
        private void RedRoom()
        {

        }

        private void KidRoom()
        {

        }

        private void ConsoleRoom()
        {

        }
    }
}
