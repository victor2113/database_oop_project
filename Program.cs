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
            //UserInterface.test.Run();


            //App myapp = new App();
            // myapp.Begin();
        }
    }

}
