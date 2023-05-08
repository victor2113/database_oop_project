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
        LK lk;
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

            lk = new LK(user);
            string[] options = { "Red Room", "Kid Room", "Console Room", "\nGo Back!" } ;
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
                case 3:
                    lk.Window();
                    break;
            }
        }

        private void Day(int i)
        {
            Console.CursorVisible = false;
            string promt = @"
Use the arrow keys to choose Date and press enter to select one";

            string[] options = { $"{date1.ToString("d")}", $"{date1.AddDays(1).ToString("d")}", $"{date1.AddDays(2).ToString("d")}", $"{date1.AddDays(3).ToString("d")}", $"{date1.AddDays(4).ToString("d")}", $"{date1.AddDays(5).ToString("d")}", $"{date1.AddDays(6).ToString("d")}", "\nGo Back!" };
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
                case 7:
                    Entry();
                    break;
            }
        }

        private void Time(int i)
        {
            Console.CursorVisible = false;
            string promt = @"
Use the arrow keys to choose Time of reservation and press enter to select one";

            string reservation1;
            string reservation2;
            string reservation3;

            if (!checkTime(i, "Noon"))
                reservation1 = "is not reserved";
            else
                reservation1 = "is already reserved";

            if (!checkTime(i, "Evening"))
                reservation2 = "is not reserved";
            else
                reservation2 = "is already reserved";

            if (!checkTime(i, "Night"))
                reservation3 = "is not reserved";
            else
                reservation3 = "is already reserved";

            string[] options = { $"Noon: {reservation1}", $"Evening: {reservation2}", $"Night: {reservation3}", "\nGo Back!" };
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
                case 3:
                    Day(i);
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
                    Console.Clear();
                    Console.WriteLine("success");
                    Console.ReadKey(true);
                    Entry();
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
                Console.Clear();
                if (!deleteReservation(i))
                {
                    Console.WriteLine("\nroom is already reserved!");
                    Console.ReadKey(true);
                    Time(i);
                }
                else
                {
                    Console.WriteLine("\nyour reservation is deleted");
                    Console.ReadKey(true);
                    Time(i);
                }
            }
        }

        public Boolean checkTime(int i, string time)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query_time = "";

            if (i == 0)
            {
                query_time =
                $"select id_user , user_login , user_full_name, user_orderDate, user_orderTime from entry_red_room where user_orderDate = '{user_orderDate}' and user_orderTime = '{time}'";
            }
            if (i == 1)
            {
                query_time =
                $"select id_user , user_login , user_full_name, user_orderDate, user_orderTime from entry_kid_room where user_orderDate = '{user_orderDate}' and user_orderTime = '{time}'";
            }
            if (i == 2)
            {
                query_time =
                $"select id_user , user_login , user_full_name, user_orderDate, user_orderTime from entry_console_room where user_orderDate = '{user_orderDate}' and user_orderTime = '{time}'";
            }

            SqlCommand command = new SqlCommand(query_time, database.getConnection());


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

        public Boolean deleteReservation(int i)
        {
            // создаем строку запроса к базе данных
            string query_delete = "";
            if (i == 0)
            {
                query_delete = $"DELETE FROM entry_red_room WHERE user_login = '{user.user_login}' AND user_orderDate = '{user_orderDate}' AND user_orderTime = '{user_orderTime}'";
            }
            if (i == 1)
            {
                query_delete = $"DELETE FROM entry_kid_room WHERE user_login = '{user.user_login}' AND user_orderDate = '{user_orderDate}' AND user_orderTime = '{user_orderTime}'";
            }
            if (i == 2)
            {
                query_delete = $"DELETE FROM entry_console_room WHERE user_login = '{user.user_login}'  AND user_orderDate = '{user_orderDate}' AND user_orderTime = '{user_orderTime}'";
            }

            // создаем команду и выполняем ее
            SqlCommand command = new SqlCommand(query_delete, database.getConnection());
            int rowsAffected = command.ExecuteNonQuery();

            // проверяем, была ли выполнена команда и возвращаем результат
            if (rowsAffected > 0)
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
