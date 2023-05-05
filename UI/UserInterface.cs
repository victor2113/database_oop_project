namespace database_oop_project.UI
{
    // класс для описания диалогов и окон, использующихся в программе
    internal static class UserInterface
    {
        // *********************
        // Main Window
        // *********************
        static readonly List<Control?> MainWindowControls = new()
        {
            new Button(5, 3, 1, "", "LOGIN",  ConsoleColor.DarkYellow, ConsoleColor.Cyan, LoginShow),
            new Button(5, 6, 2, "", "SIGNUP", ConsoleColor.DarkYellow, ConsoleColor.Cyan, SignupShow),
            new Button(5, 9, 3, "", "ABOUT", ConsoleColor.DarkYellow, ConsoleColor.Cyan, AboutShow),
            new Button(5, 12, 4, "", "EXIT", ConsoleColor.DarkYellow, ConsoleColor.Cyan, ExitConfirmation),
        };
        internal static Window MainWindow = new(ConsoleColor.DarkCyan, MainWindowControls, "FEFU Database Project", new Status("Press Tab to select element, Enter to click a button", ConsoleColor.DarkMagenta));
        // Main window actions
        static void LoginShow()
        {
            LoginDialog.IsActive = true;
            LoginDialog.IsVisible = true;
            LoginDialog.Run();
        }
        static void SignupShow()
        {
            SignupDialog.IsActive = true;
            SignupDialog.IsVisible = true;
            SignupDialog.Run();
        }
        static void AboutShow()
        {
            AboutDialog.IsActive = true;
            AboutDialog.IsVisible = true;
            AboutDialog.Run();
        }
        static void ExitConfirmation()
        {
            ConfirmationDialog.IsActive = true;
            ConfirmationDialog.IsVisible = true;
            ConfirmationDialog.Run();
        }

        // *********************
        // About dialog
        // *********************
        static readonly List<Control?> AboutControls = new()
        {
            new Textbox( 6, 3,-1,"","TEAM:\n\nIgnatov Ilya\n\nMikhaylova Eleonora\n\nAprosimov Anatoliy\n\nMekumyanova Irina\n\nGribanova Katerina",ConsoleColor.DarkGreen,40,12, null),
            new Button( 25,  15, 1, "", "OK", ConsoleColor.DarkYellow, ConsoleColor.Cyan, AboutClose),
        };
        internal static Dialog AboutDialog = new(ConsoleColor.DarkMagenta, AboutControls, "About", 50, 18, MainWindow, AboutClose);
        // About dialog actions
        static void AboutClose()
        {
            AboutDialog.Close();
            MainWindow.Run();
        }

        // *********************
        // Confirmation messagebox
        // *********************
        static readonly List<Control?> ConfirmControls = new()
        {
            new Textbox( 10, 3,-1,"","Do you really want to exit?",ConsoleColor.DarkGreen,40,12, null),
            new Button( 18,  6, 1, "", "Yes", ConsoleColor.DarkYellow, ConsoleColor.Cyan, ConfirmationYes),
            new Button( 28,  6, 2, "", "No", ConsoleColor.DarkYellow, ConsoleColor.Cyan, ConfirmationClose),
        };
        internal static Dialog ConfirmationDialog = new(ConsoleColor.DarkMagenta, ConfirmControls, "Confirmation", 50, 10, MainWindow, ExitApplication);
        // Confirmation dialog actions
        static void ConfirmationYes()
        {
            ConfirmationDialog.DialogAction();
        }
        static void ConfirmationClose()
        {
            ConfirmationDialog.Close();
            MainWindow.Run();
        }
        static void ExitApplication()
        {
            Environment.Exit(0);
        }

        // *********************
        // Error messagebox
        // *********************
        static readonly List<Control?> ErrorControls = new()
        {
            new Textbox( 10, 3,-1,"","Error message",ConsoleColor.DarkGreen,40,12, null),
            new Button( 22,  6, 1, "", "Ok", ConsoleColor.DarkYellow, ConsoleColor.Cyan, ErrorClose),
        };
        internal static Dialog ErrorDialog = new(ConsoleColor.DarkRed, ErrorControls, "Error", 50, 10, MainWindow, ExitApplication);
        // Error dialog actions
        static void ErrorClose()
        {
            if (ErrorDialog.ParentDialog == null)
            {
                ErrorDialog.Close();
                MainWindow.Run();
            }
            else
            {
                ErrorDialog.Close(ErrorDialog.ParentDialog);
            }
        }

        // *********************
        // Login dialog
        // *********************
        static readonly List<Control?> LoginControls = new()
        {
            new InputBox( 15, 3,1,"Username:","",ConsoleColor.DarkCyan,30,1, false, false, null),
            new InputBox( 15, 6,2,"Password:","",ConsoleColor.DarkCyan,30,1, false, true, null),
            new Button( 25,  9, 3, "", "OK", ConsoleColor.DarkYellow, ConsoleColor.Cyan, LoginSubmit),
            new Button( 35,  9, 4, "", "CANCEL", ConsoleColor.DarkYellow, ConsoleColor.Cyan, LoginClose),
        };
        internal static Dialog LoginDialog = new(ConsoleColor.DarkMagenta, LoginControls, "Login", 50, 12, MainWindow, LoginSubmit);
        // Login dialog actions
        static void LoginSubmit()
        {
            LoginDialog.Close();

            string username = LoginDialog.Controls.Find(c => c.Label.Contains("Username")).Text;
            string password = LoginDialog.Controls.Find(c => c.Label.Contains("Password")).Text;

            // 
            // Add login function call here
            // 

            MainWindow.Run();
        }
        static void LoginClose()
        {
            LoginDialog.Close();
            MainWindow.Run();
        }

        // *********************
        // Signup dialog
        // *********************
        static readonly List<Control?> SignupControls = new()
        {
            new InputBox( 24, 3,1,"Username:","",ConsoleColor.DarkCyan,30,1, false, false, null),
            new InputBox( 24, 6,2,"Email:","",ConsoleColor.DarkCyan,30,1, false, false, null),
            new InputBox( 24, 9,3,"Password:","",ConsoleColor.DarkCyan,30,1, false, true, null),
            new InputBox( 24, 12,4,"Confirm password:","",ConsoleColor.DarkCyan,30,1, false, true, null),
            new Button( 30,  15, 5, "", "OK", ConsoleColor.DarkYellow, ConsoleColor.Cyan, SignupSubmit),
            new Button( 40,  15, 6, "", "CANCEL", ConsoleColor.DarkYellow, ConsoleColor.Cyan, SignupClose),
        };
        internal static Dialog SignupDialog = new(ConsoleColor.DarkMagenta, SignupControls, "Sign Up", 60, 18, MainWindow, SignupSubmit);
        // Signup dialog actions
        static void SignupSubmit()
        {
            string username = SignupDialog.Controls.Find(c => c.Label.Contains("Username")).Text;
            string password1 = SignupDialog.Controls.Find(c => c.Label.Contains("Password")).Text;
            string password2 = SignupDialog.Controls.Find(c => c.Label.Contains("Confirm password")).Text;

            if (!string.Equals(password1, password2))
            {
                ErrorDialog.IsActive = true;
                ErrorDialog.IsVisible = true;
                ErrorDialog.Controls[0].Text = "Passwords are not equal!";
                ErrorDialog.ParentDialog = SignupDialog;
                ErrorDialog.Run();
            }
            else
            {
                SignupDialog.Close();
            }

            // 
            // Add signup function call here
            // 

            MainWindow.Run();
        }
        static void SignupClose()
        {
            SignupDialog.Close();
            MainWindow.Run();
        }
    }
}
