using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database_oop_project
{
    public class LK
    {
        App myapp = new App();
        User user;
        CoolRooms room;
        public LK(User user)
        {
            this.user = user;
        }
        public void Window()
        {
            Console.CursorVisible = false;
            string promt = @$"
Personal Information:
Login: {user.user_login}
Full Name: {user.user_fullName}
Age: {user.user_age}";

            room = new CoolRooms(user);
            string[] options = { "See my reservations", "Add new reservation", "Edit account", "Exit" };
            StartMenu startMenu = new StartMenu(options, promt);
            int selectedIndex = startMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Res();
                    break;
                case 1:
                    room.Entry();
                    break;
                case 2:
                    Edit();
                    break;
                case 3:
                    myapp.Begin();
                    break;
            }
        }

        private void Edit()
        {

        }

        private void Res()
        {
            Console.CursorVisible = false;
            room = new CoolRooms(user);
            string promt = @"
My reservations: ";


            string[] options = { "Add new reservation", "Delete reservation", "\nGo Back!" };
            StartMenu startMenu = new StartMenu(options, promt);
            int selectedIndex = startMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    room.Entry();
                    break;
                case 1:
                    Del();
                    break;
                case 2:
                    Window();
                    break;
            }
        }

        private void Del()
        {

        }
    }
}
