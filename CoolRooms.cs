using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database_oop_project
{
    public class CoolRooms
    {
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
