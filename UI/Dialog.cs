namespace database_oop_project.UI
{
    // класс для всплывающего диалога
    internal class Dialog : Window
    {
        internal Window Parent;
        internal Dialog? ParentDialog = null;
        internal Action DialogAction;
        public Dialog(ConsoleColor borderColor, List<Control?> controls, string title, int w, int h, Window parent, Action dialogAction) : base(borderColor, controls, title)
        {
            Parent = parent;
            DialogAction = dialogAction;
            Width = w;
            Height = h;
            X = GetX(Width);
            Y = GetY(Height);
            StatusBar = null;
        }

        // отрисовка диалога
        private void PrepareScreen()
        {
            for (int i = X; i < X + Width; i++)
                for (int j = Y; j < Y + Height; j++)
                {
                    Parent.AddChar(' ', ConsoleColor.Black, i, j);
                }

            Parent.AddChar(Decorator.SinRndCornerNw, BorderColor, X, Y);
            Parent.AddChar(Decorator.SinRndCornerNe, BorderColor, X + Width, Y);
            Parent.AddChar(Decorator.SinRndCornerSw, BorderColor, X, Y + Height);
            Parent.AddChar(Decorator.SinRndCornerSe, BorderColor, X + Width, Y + Height);

            for (int i = X + 1; i < X + Width; i++)
            {
                Parent.AddChar(Decorator.SinHorizontal, BorderColor, i, Y);
                Parent.AddChar(Decorator.SinHorizontal, BorderColor, i, Y + Height);
                Parent.AddChar(Decorator.Shadow, ConsoleColor.DarkGray, i + 1, Y + Height + 1);
            }

            for (int i = X + 1; i < X + Title.Length + 1; i++)
            {
                Parent.AddChar(Title[i - X - 1], ConsoleColor.Cyan, i, Y);
            }

            for (int i = Y + 1; i < Y + Height; i++)
            {
                Parent.AddChar(Decorator.SinVertical, BorderColor, X, i);
                Parent.AddChar(Decorator.SinVertical, BorderColor, X + Width, i);
                Parent.AddChar(Decorator.Shadow, ConsoleColor.DarkGray, X + Width + 1, i + 1);
            }
            Parent.AddChar(Decorator.Shadow, ConsoleColor.DarkGray, X + Width + 1, Y + 1);
            Parent.AddChar(Decorator.Shadow, ConsoleColor.DarkGray, X + Width + 1, Y + Height + 1);

            foreach (Control? control in Controls)
            {
                control.Parent = Parent;
                control.realX = control.X + X;
                control.realY = control.Y + Y;
                control.Show();
            }
        }

        // отрисовка окна приложения
        internal void Show()
        {
            Parent.IsActive = false;
            if (IsActive && IsVisible)
            {
                Parent.PrepareScreen();
                PrepareScreen();
            }
            Parent.Show();
        }

        // считывание пользовательских действий
        internal void Run()
        {
            if (IsActive && IsVisible)
            {
                Show();
                ConsoleKeyInfo k = Console.ReadKey(true);
                for (; ; )
                {
                    if (!IsActive) break;
                    Control? cnt = Controls.Find(c => c.IsActive);
                    switch (k.Key)
                    {
                        case ConsoleKey.Tab:
                            ActivateNextControl(Controls);
                            Show();
                            break;
                        case ConsoleKey.Enter:
                            if (cnt is Button)
                            {
                                cnt.CallerAction();
                            }
                            break;
                        case ConsoleKey.Delete:
                        case ConsoleKey.Backspace:
                            if (cnt is InputBox)
                            {
                                ((InputBox)cnt).DeleteChar(k.Key);
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.RightArrow:
                            if (cnt is InputBox)
                            {
                                ((InputBox)cnt).MoveCursor(k.Key);
                            }
                            break;
                        default:
                            if (!char.IsControl(k.KeyChar) && cnt is InputBox)
                            {
                                ((InputBox)cnt).AppendChar(k.KeyChar);
                            }
                            break;
                    }
                    Show();
                    k = Console.ReadKey(true);
                }
            }
        }

        // закрытие диалога с окрытием предыдущего диалога, например при использовании UserInterface.ErrorDialog
        internal void Close(Dialog d)
        {
            IsActive = false;
            IsVisible = false;
            int minTab = Controls.Find(c => c.TabIndex > 0).TabIndex;
            foreach (Control? control in Controls)
            {
                control.IsActive = control.TabIndex == minTab;
                if (control is InputBox)
                {
                    ((InputBox)control).Text = ((InputBox)control).DefText;
                    ((InputBox)control).CursorPos = 0;
                }
            }

            d.IsActive = true;
            d.IsVisible = true;
            d.Run();
        }

        // простое закрытие диалога и отображение основного экрана
        internal void Close()
        {
            Parent.IsActive = true;
            Parent.IsVisible = true;
            IsActive = false;
            IsVisible = false;
            int minTab = Controls.Find(c => c.TabIndex > 0).TabIndex;
            foreach (Control? control in Controls)
            {
                control.IsActive = control.TabIndex == minTab;
                if (control is InputBox)
                {
                    ((InputBox)control).Text = ((InputBox)control).DefText;
                    ((InputBox)control).CursorPos = 0;
                }
            }
        }

        // методы для центрирования диалога на экране
        internal int GetX(int width)
        {
            return (Parent.Width - width) / 2;
        }

        internal int GetY(int height)
        {
            return (Parent.Height - height - 4) / 2;
        }
    }
}
