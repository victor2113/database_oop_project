namespace database_oop_project.UI
{
    // класс для основного экрана окна
    public class Window
    {
        internal ConsoleColor BorderColor; // цвет бордюра окна
        internal List<Control?> Controls; // список элементов интерфейса
        internal string Title; // заголовок окна
        internal Status? StatusBar; // статусная строка
        internal int Width; // ширина окна
        internal int Height; // высота окна
        internal ConsoleChar[,] Screen; // символы экрана окна
        private ConsoleChar[,] oldScreen; // символы экрана окна
        internal bool IsActive = true; // окно активно или нет
        internal bool IsVisible = true; // окно отображается или нет
        internal int X = 0; // координата X верхнего левого угла окна
        internal int Y = 0; // координата Y верхнего левого угла окна

        // конструктор
        public Window(ConsoleColor borderColor, List<Control?> controls, string title, Status? statusBar = null)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;
            Console.CursorVisible = false;
            BorderColor = borderColor;
            Controls = controls;
            Title = $" {title.Trim()} ";
            StatusBar = statusBar;
            Width = Console.WindowWidth;
            Height = Console.WindowHeight;
            Screen = InitScreen(Width, Height);
            Controls = Controls.OrderBy(c => c.TabIndex).ToList();
            int minTab = Controls.Find(c => c.TabIndex > 0).TabIndex;
            foreach (Control? c in Controls)
            {
                c.IsActive = c.TabIndex == minTab;
            }
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
                    k = Console.ReadKey(true);
                }
            }
        }

        internal void ActivateNextControl(List<Control?> controls)
        {
            int i = Controls.Find(c => c.IsActive).TabIndex;
            Controls.Find(c => c.IsActive).IsActive = false;
            if (Controls.Find(c => c.TabIndex == i + 1) != null)
            {
                Controls.Find(c => c.TabIndex == i + 1).IsActive = true;
            }
            else
            {
                int minTab = Controls.Find(c => c.TabIndex > 0).TabIndex;
                foreach (Control? c in Controls)
                {
                    c.IsActive = c.TabIndex == minTab;
                }
            }

            Control cnt = Controls.Find(c => c.IsActive);
            if (cnt is InputBox)
            {
                ((InputBox)cnt).CursorPos = 0;
            }

        }

        // подготовка экрана к перерисовке
        internal void PrepareScreen()
        {
            oldScreen = Screen;
            Screen = InitScreen(Width, Height);
            AddWindowBorder();
            foreach (Control? control in Controls)
            {
                control.Parent = this;
                control.realX = control.X + X;
                control.realY = control.Y + Y;
                control.Show();
            }
            AddStatus();
        }

        // перерисовка экрана
        internal void Show()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = Screen[0, 0].FrontColor;
            if (IsVisible)
            {
                if (IsActive)
                {
                    PrepareScreen();
                }

                for (int j = 0; j < Screen.GetLength(1); j++)
                    for (int i = 0; i < Screen.GetLength(0); i++)
                    {
                        if (Screen[i, j].Char != oldScreen[i,j].Char || Screen[i, j].FrontColor != oldScreen[i, j].FrontColor)
                        {
                            Console.SetCursorPosition(i, j);
                            Console.ForegroundColor = Screen[i, j].FrontColor;
                            Console.Write(Screen[i, j].Char);
                        }
                    }
                Console.ResetColor();
            }
        }

        // отрисовка статусной строки внизу окна
        private void AddStatus()
        {
            if (StatusBar != null)
            {
                for (int i = 0; i < StatusBar.Text.Length; i++)
                {
                    AddChar(StatusBar.Text[i], BorderColor, i + 1, Height - 2);
                }
            }
        }

        // отрисовка границ окна
        private void AddWindowBorder()
        {
            AddChar(Decorator.DblCornerNw, BorderColor, 0, 0);
            AddChar(Decorator.DblCornerNe, BorderColor, Width - 1, 0);
            AddChar(Decorator.DblCornerSw, BorderColor, 0, Height - 1);
            AddChar(Decorator.DblCornerSe, BorderColor, Width - 1, Height - 1);

            for (int i = 1 + Title.Length; i < Width - 1; i++)
            {
                AddChar(Decorator.DblHorizontal, BorderColor, i, 0);
            }

            for (int i = 1; i < Title.Length + 1; i++)
            {
                AddChar(Title[i - 1], ConsoleColor.Cyan, i, 0);
            }

            for (int i = 1; i < Width - 1; i++)
            {
                AddChar(Decorator.DblHorizontal, BorderColor, i, Height - 1);
                AddChar(Decorator.DblHorizontal, BorderColor, i, Height - 3);
            }

            for (int i = 1; i < Height - 1; i++)
            {
                AddChar(Decorator.DblVertical, BorderColor, 0, i);
                AddChar(Decorator.DblVertical, BorderColor, Width - 1, i);
            }

            AddChar(Decorator.DblLeftJoin, BorderColor, 0, Height - 3);
            AddChar(Decorator.DblRightJoin, BorderColor, Width - 1, Height - 3);
        }

        internal void AddChar(char c, ConsoleColor colorF, int x, int y)
        {
            Screen[x, y].Char = c;
            Screen[x, y].FrontColor = colorF;
        }

        internal ConsoleChar[,] InitScreen(int w, int h)
        {
            ConsoleChar[,] screen = new ConsoleChar[w, h];
            for (int i = 0; i < screen.GetLength(0); i++)
                for (int j = 0; j < screen.GetLength(1); j++)
                {
                    screen[i, j] = new ConsoleChar();
                }
            return screen;
        }
    }
}
