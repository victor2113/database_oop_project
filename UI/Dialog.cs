namespace database_oop_project.UI
{
    internal class Dialog : Window
    {
        public Dialog(ConsoleColor borderColor, List<Control?> controls, string title, byte x, byte y, int w, int h) : base(borderColor, controls, title)
        {
            X = x;
            Y = y;
            Width = w; 
            Height = h;
            StatusBar = null;
        }

        private void PrepareScreen(Window parent)
        {
            for (int i = X; i < X + Width; i++)
                for (int j = Y; j < Y + Height; j++)
                {
                    parent.AddChar(' ', ConsoleColor.Black, i, j);
                }

            parent.AddChar(Decorator.SinRndCornerNw, BorderColor, X, Y);
            parent.AddChar(Decorator.SinRndCornerNe, BorderColor, X + Width, Y);
            parent.AddChar(Decorator.SinRndCornerSw, BorderColor, X, Y + Height);
            parent.AddChar(Decorator.SinRndCornerSe, BorderColor, X + Width, Y + Height);

            for (int i = X + 1; i < X + Width; i++)
            {
                parent.AddChar(Decorator.SinHorizontal, BorderColor, i, Y);
                parent.AddChar(Decorator.SinHorizontal, BorderColor, i, Y + Height);
                parent.AddChar(Decorator.Shadow, ConsoleColor.DarkGray, i + 1, Y + Height + 1);
            }

            for (int i = X + 1; i < X + Title.Length + 1; i++)
            {
                parent.AddChar(Title[i - X - 1], ConsoleColor.Cyan, i, Y);
            }

            for (int i = Y + 1; i < Y + Height; i++)
            {
                parent.AddChar(Decorator.SinVertical, BorderColor, X, i);
                parent.AddChar(Decorator.SinVertical, BorderColor, X + Width, i);
                parent.AddChar(Decorator.Shadow, ConsoleColor.DarkGray, X + Width + 1, i + 1);
            }
            parent.AddChar(Decorator.Shadow, ConsoleColor.DarkGray, X + Width + 1, Y + 1);
            parent.AddChar(Decorator.Shadow, ConsoleColor.DarkGray, X + Width + 1, Y + Height + 1);

            foreach (Control? control in Controls)
            {
                control.Show(parent);
            }
        }

        internal void Show(Window parent)
        {
            parent.IsActive = false;
            if (IsActive && IsVisible)
            {
                parent.PrepareScreen();
                PrepareScreen(parent);
            }
            parent.Show();
        }

        // считывание пользовательских действий
        internal void Run(Window parent)
        {
            if (IsActive && IsVisible)
            {
                Show(parent);
                ConsoleKeyInfo k = Console.ReadKey(true);
                for (; ; )
                {
                    if (!IsActive) break;
                    switch (k.Key)
                    {
                        case ConsoleKey.Tab:
                            break;
                        case ConsoleKey.Enter:
                            Control? cnt = Controls.Find(c => c.IsActive);
                            if (cnt is Button)
                            {
                                cnt.CallerAction();
                            }
                            break;
                        default:
                            break;
                    }
                    k = Console.ReadKey(true);
                }
            }
        }

        internal void Close(Window parent)
        {
            parent.IsActive = true;
            parent.IsVisible = true;
            IsActive = false;
            IsVisible = false;
        }
    }
}
