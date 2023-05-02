namespace database_oop_project.UI
{
    internal static class UserInterface
    {
        private const byte OffX = 35;
        private const byte OffY = 2;

        // Main Window
        static readonly List<Control?> MainWindowControls = new()
        {
            new Button(5, 3, 1, "", "LOGIN", "Hint", ConsoleColor.DarkYellow, ConsoleColor.Cyan, LoginShow),
            new Button(5, 6, 2, "", "ABOUT", "Hint", ConsoleColor.DarkYellow, ConsoleColor.Cyan, AboutShow),
            new Button(5, 9, 3, "", "EXIT", "Hint", ConsoleColor.DarkYellow, ConsoleColor.Cyan, ExitApplication),
        };

        internal static Window MainWindow = new(ConsoleColor.DarkCyan, MainWindowControls, "FEFU Database Project", new Status("Press tab to select element", ConsoleColor.DarkMagenta));
        static void LoginShow()
        {
            LoginDialog.IsActive = true;
            LoginDialog.IsVisible = true;
            LoginDialog.Run(MainWindow);
        }
        static void AboutShow()
        {
            AboutDialog.IsActive = true;
            AboutDialog.IsVisible = true;
            AboutDialog.Run(MainWindow);
        }
        static void ExitApplication()
        {
            Environment.Exit(0);
        }

        // About dialog
        static readonly List<Control?> AboutControls = new()
        {
            new Textbox(OffX + 6,OffY + 3,-1,"","TEAM:\n\nIgnatov Ilya\n\nMikhaylova Eleonora\n\nAprosimov Anatoliy\n\nMekumyanova Irina\n\nGribanova Katerina","About screen",ConsoleColor.DarkGreen,40,12, true, null),
            new Button(OffX + 25, OffY + 16, 1, "", "OK", "Hint", ConsoleColor.DarkYellow, ConsoleColor.Cyan, AboutClose),
        };

        internal static Dialog AboutDialog = new(ConsoleColor.DarkMagenta, AboutControls, "About", OffX, OffY, 50, 19);
        static void AboutClose()
        {
            AboutDialog.Close(MainWindow);
            MainWindow.Run();
        }

        // Login dialog
        static readonly List<Control?> LoginControls = new()
        {
            new Textbox(OffX + 15,OffY+3,2,"Username:","","Enter username",ConsoleColor.DarkGreen,30,1, false, null),
            new Textbox(OffX + 15,OffY+7,3,"Password:","","Enter password",ConsoleColor.DarkGreen,30,1, false, null),
            new Button(OffX + 25, OffY + 12, 4, "", "OK", "Hint", ConsoleColor.DarkYellow, ConsoleColor.Cyan, LoginSubmit),
            new Button(OffX + 35, OffY + 12, 1, "", "CANCEL", "Hint", ConsoleColor.DarkYellow, ConsoleColor.Cyan, LoginClose),
        };

        internal static Dialog LoginDialog = new(ConsoleColor.DarkMagenta, LoginControls, "Login", OffX, OffY, 50, 15);
        static void LoginSubmit()
        {
            LoginDialog.Close(MainWindow);
            MainWindow.Run();
        }
        static void LoginClose()
        {
            LoginDialog.Close(MainWindow);
            MainWindow.Run();
        }
    }
}
