namespace database_oop_project.UI
{
    internal class Textbox : Control
    {
        internal byte Width;
        internal byte Lines;
        internal bool IsReadOnly;

        public Textbox(int x, int y, int tabIndex, string label, string text, string hint, ConsoleColor frontColor, byte width, byte lines, bool isReadOnly, Action callerAction) : base(x, y, tabIndex, label, text, hint, frontColor, callerAction)
        {
            Width = width;
            Lines = lines;
            IsReadOnly = isReadOnly;
        }

        internal override void Show(Window window)
        {
            if (IsVisible)
            {
                ConsoleColor border = IsActive ? ConsoleColor.White : ConsoleColor.DarkGray;
                int labelPos = X - Label.Length - 1;

                if (!IsReadOnly)
                {
                    for (int i = 0; i < Lines; i++)
                    {
                        window.AddChar(Decorator.SinVertical, border, X + Width, Y + i);
                    }

                    window.AddChar(Decorator.SinRndCornerSe, border, X + Width, Y + Lines);
                    for (int i = 0; i < Width; i++)
                    {
                        window.AddChar(Decorator.SinHorizontal, border, X + i, Y + Lines);
                    }

                    for (int i = labelPos; i < labelPos + Label.Length; i++)
                    {
                        window.AddChar(Label[i - labelPos], FrontColor, i, Y);
                    }
                }

                if (!string.IsNullOrEmpty(Text))
                {
                    string[] text = Text.Split('\n');
                    int end = Math.Min(text.Length, Lines);
                    for (int i = 0; i < end; i++)
                    {
                        string s = text[i].Substring(0, Math.Min(Width, text[i].Length));
                        for (int j = 0; j < s.Length; j++)
                        {
                            window.AddChar(s[j], FrontColor, X + 2 + j, Y + i);
                        }
                    }
                }
            }
        }
    }
}
