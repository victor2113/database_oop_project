namespace database_oop_project.UI
{
    // класс для однострочного поля ввода
    // длина текста ограничена размером поля
    internal class InputBox : Control
    {
        internal byte Width;
        internal bool IsReadOnly;
        internal bool IsPassword;
        internal int CursorPos = 0;
        internal string DefText;
        public InputBox(int x, int y, int tabIndex, string label, string text, ConsoleColor frontColor, byte width, byte lines, bool isReadOnly, bool isPassword, Action callerAction) : base(x, y, tabIndex, label, text, frontColor, callerAction)
        {
            Width = width;
            IsReadOnly = isReadOnly;
            IsPassword = isPassword;
            DefText = text;
        }

        internal override void Show()
        {
            if (IsVisible)
            {
                ConsoleColor border = IsActive ? ConsoleColor.White : ConsoleColor.DarkGray;
                int labelPos = realX - Label.Length - 1;

                if (!IsReadOnly)
                {
                    Parent.AddChar(Decorator.SinVertical, border, realX + Width, realY);

                    Parent.AddChar(Decorator.SinRndCornerSe, border, realX + Width, realY + 1);
                    for (int i = 0; i < Width; i++)
                    {
                        Parent.AddChar(Decorator.SinHorizontal, border, realX + i, realY + 1);
                    }

                    if (IsActive)
                    {
                        Parent.AddChar(Decorator.Cursor, border, realX + CursorPos, realY + 1);
                    }

                    for (int i = labelPos; i < labelPos + Label.Length; i++)
                    {
                        Parent.AddChar(Label[i - labelPos], FrontColor, i, realY);
                    }
                }

                if (!string.IsNullOrEmpty(Text))
                {
                    for (int i = 0; i < Text.Length; i++)
                    {
                        if (IsPassword)
                        {
                            Parent.AddChar('*', ConsoleColor.Gray, realX + i, realY);
                        }
                        else
                        {
                            Parent.AddChar(Text[i], ConsoleColor.Gray, realX + i, realY);
                        }
                    }
                }
            }
        }

        public void AppendChar(char c)
        {
            if (!IsReadOnly && Text.Length < Width)
            {
                Text = Text.Substring(0,CursorPos) + c + Text.Substring(CursorPos);
                MoveCursor(ConsoleKey.RightArrow);
            }
        }

        public void MoveCursor(ConsoleKey kKey)
        {
            if (!IsReadOnly)
            {
                switch (kKey)
                {
                    case ConsoleKey.RightArrow:
                        if (CursorPos < Width && CursorPos < Text.Length) CursorPos++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (CursorPos > 0) CursorPos--;
                        break;
                }
            }
        }

        public void DeleteChar(ConsoleKey kKey)
        {
            if (!IsReadOnly)
            {
                switch (kKey)
                {
                    case ConsoleKey.Delete:
                        if (CursorPos < Text.Length)
                            Text = Text.Substring(0,CursorPos) + Text.Substring(CursorPos + 1);
                        break;
                    case ConsoleKey.Backspace:
                        if(CursorPos > 0)
                            Text = Text.Substring(0, CursorPos - 1) + Text.Substring(CursorPos);
                        MoveCursor(ConsoleKey.LeftArrow);
                        break;
                }
            }
        }
    }
}
