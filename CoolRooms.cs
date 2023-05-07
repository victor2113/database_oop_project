using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database_oop_project
{
    public class CoolRooms
    {
        public string user_orderDate { get; set; } 
        public string user_orderTime { get; set; }

        DataBase database = new DataBase();

        User user;
        public CoolRooms(User user)
        {
            this.user = user;
        }

        DateTime date1 = DateTime.Now;
        
        public void Entry()
        {
            user.GetFullName();
            Console.CursorVisible = false;
            string promt = @"
Use the arrow keys to choose room and press enter to select one";


            string[] options = { "Red Room", "Kid Room", "Console Room" };
            StartMenu startMenu = new StartMenu(options, promt);
            int selectedIndex = startMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Day(0);
                    break;
                case 1:
                    Day(1);
                    break;
                case 2:
                    Day(2);
                    break;
            }
        }

        private void Day(int i)
        {
            Console.CursorVisible = false;
            string promt = @"
Use the arrow keys to choose Date and press enter to select one";

            string[] options = { $"{date1.ToString("d")}", $"{date1.AddDays(1).ToString("d")}", $"{date1.AddDays(2).ToString("d")}", $"{date1.AddDays(3).ToString("d")}", $"{date1.AddDays(4).ToString("d")}", $"{date1.AddDays(5).ToString("d")}", $"{date1.AddDays(6).ToString("d")}" };
            StartMenu startMenu = new StartMenu(options, promt);
            int selectedIndex = startMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    user_orderDate = date1.ToString("d");
                    Time(i);
                    break;
                case 1:
                    user_orderDate = date1.AddDays(1).ToString("d");
                    Time(i);
                    break;
                case 2:
                    user_orderDate = date1.AddDays(2).ToString("d");
                    Time(i);
                    break;
                case 3:
                    user_orderDate = date1.AddDays(3).ToString("d");
                    Time(i);  
                    break;
                case 4:
                    user_orderDate = date1.AddDays(4).ToString("d");
                    Time(i);  
                    break;
                case 5:
                    user_orderDate = date1.AddDays(5).ToString("d");
                    Time(i);
                    break;
                case 6:
                    user_orderDate = date1.AddDays(6).ToString("d");
                    Time(i);
                    break;
            }
        }

        private void Time(int i)
        {
            Console.CursorVisible = false;
            string promt = @"
Use the arrow keys to choose Time of reservation and press enter to select one";

            string reservation = "";

            if (existingRoom(i))
                reservation = "is reserved";
            else
                reservation = "is not reserved";

            string[] options = { $"Noon: {reservation}", $"Evening: {reservation}", $"Night: {reservation}" };
            StartMenu startMenu = new StartMenu(options, promt);
            int selectedIndex = startMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    user_orderTime = "Noon";
                    EntryRoom(i);
                    break;
                case 1:
                    user_orderTime = "Evening";
                    EntryRoom(i);
                    break;
                case 2:
                    user_orderTime = "Night";
                    EntryRoom(i);
                    break;
            }
        }
        private void EntryRoom(int i)
        {
            database.openConnection();
            string query_string = "";

            if (!existingRoom(i))
            {
                if (i == 0)
                {
                    query_string =
                    $"insert into entry_red_room(user_login , user_full_name, user_orderDate, user_orderTime)  values('{user.user_login}', '{user.user_fullName}', '{user_orderDate}', '{user_orderTime}')";
                }
                else if (i == 1)
                {
                    query_string =
                    $"insert into entry_kid_room(user_login , user_full_name, user_orderDate, user_orderTime)  values('{user.user_login}', '{user.user_fullName}', '{user_orderDate}', '{user_orderTime}')";
                }
                else if (i == 2)
                {
                    query_string =
                    $"insert into entry_console_room(user_login , user_full_name, user_orderDate, user_orderTime)  values('{user.user_login}', '{user.user_fullName}', '{user_orderDate}', '{user_orderTime}')";
                }
                    

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
            }
            else
            {
                Console.WriteLine("room is already reserved!");
            }
        }

        public Boolean existingRoom(int i)
        {

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query_string = "";

            if (i == 0)
            {
                query_string =
                $"select id_user , user_login , user_full_name, user_orderDate, user_orderTime from entry_red_room where user_orderDate = '{user_orderDate}' and user_orderTime = '{user_orderTime}'";
            }
            if (i == 1)
            {
                query_string =
                $"select id_user , user_login , user_full_name, user_orderDate, user_orderTime from entry_kid_room where user_orderDate = '{user_orderDate}' and user_orderTime = '{user_orderTime}'";
            }
            if (i == 2)
            {
                query_string =
                $"select id_user , user_login , user_full_name, user_orderDate, user_orderTime from entry_console_room where user_orderDate = '{user_orderDate}' and user_orderTime = '{user_orderTime}'";
            }

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
