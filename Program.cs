using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using database_oop_project.UI;

namespace database_oop_project
{
    class Program
    {
        DataBase db = new DataBase();

        static void Main(string[] args)
        {
            // show main window
            UserInterface.MainWindow.Run();
            
            //App myapp = new App();
            //myapp.Begin();
        }
    }

}
