namespace database_oop_project.UI
{
    internal class Button : Control
    {
        internal ConsoleColor BorderColor;

        public Button(int x, int y, byte tabIndex, string label, string text, string hint, ConsoleColor frontColor, ConsoleColor borderColor, Action callerAction)
            : base(x, y, tabIndex, label, text, hint, frontColor, callerAction)
        {
            BorderColor = borderColor;
            Label = string.Empty;
        }

        internal override void Show(Window window)
        {
            if (IsVisible)
            {
                ConsoleColor border = IsActive ? ConsoleColor.White : BorderColor;
                window.AddChar(Decorator.SinRndCornerNw, border, X, Y);
                window.AddChar(Decorator.SinVertical, border, X, Y + 1);
                window.AddChar(Decorator.SinRndCornerSw, border, X, Y + 2);

                for (int i = X + 1; i < X + Text.Length + 3; i++)
                {
                    window.AddChar(Decorator.SinHorizontal, border, i, Y);
                    window.AddChar(Decorator.SinHorizontal, border, i, Y + 2);
                }

                window.AddChar(Decorator.SinRndCornerNe, border, X + Text.Length + 3, Y);
                window.AddChar(Decorator.SinVertical, border, X + Text.Length + 3, Y + 1);
                window.AddChar(Decorator.SinRndCornerSe, border, X + Text.Length + 3, Y + 2);

                for (int i = X + 2; i < X + 2 + Text.Length; i++)
                {
                    window.AddChar(Text[i - X - 2], FrontColor, i, Y + 1);
                }
            }
        }
    }
}
