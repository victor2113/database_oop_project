namespace database_oop_project.UI
{
    // класс для кнопки
    internal class Button : Control
    {
        internal ConsoleColor BorderColor;

        public Button(int x, int y, byte tabIndex, string label, string text, ConsoleColor frontColor, ConsoleColor borderColor, Action callerAction)
            : base(x, y, tabIndex, label, text, frontColor, callerAction)
        {
            BorderColor = borderColor;
            Label = string.Empty;
        }

        internal override void Show()
        {
            if (IsVisible)
            {
                ConsoleColor border = IsActive ? ConsoleColor.White : BorderColor;
                Parent.AddChar(Decorator.SinRndCornerNw, border, realX, realY);
                Parent.AddChar(Decorator.SinVertical, border, realX, realY + 1);
                Parent.AddChar(Decorator.SinRndCornerSw, border, realX, realY + 2);

                for (int i = realX + 1; i < realX + Text.Length + 3; i++)
                {
                    Parent.AddChar(Decorator.SinHorizontal, border, i, realY);
                    Parent.AddChar(Decorator.SinHorizontal, border, i, realY + 2);
                }

                Parent.AddChar(Decorator.SinRndCornerNe, border, realX + Text.Length + 3, realY);
                Parent.AddChar(Decorator.SinVertical, border, realX + Text.Length + 3, realY + 1);
                Parent.AddChar(Decorator.SinRndCornerSe, border, realX + Text.Length + 3, realY + 2);

                for (int i = realX + 2; i < realX + 2 + Text.Length; i++)
                {
                    Parent.AddChar(Text[i - realX - 2], FrontColor, i, realY + 1);
                }
            }
        }
    }
}
