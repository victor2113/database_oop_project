namespace database_oop_project.UI
{
    // родительский класс для контролов
    public class Control
    {
        internal int X;
        internal int Y;
        internal int TabIndex;
        internal string Label;
        internal string Text;
        internal ConsoleColor FrontColor;
        internal bool IsVisible = true;
        internal bool IsActive = false;
        internal Action CallerAction;
        internal Window Parent;
        protected internal int realX;
        protected internal int realY;
        public Control(int x, int y, int tabIndex, string label, string text, ConsoleColor frontColor, Action callerAction)
        {
            X = x;
            Y = y;
            TabIndex = tabIndex;
            Text = text;
            FrontColor = frontColor;
            Label = label;
            CallerAction = callerAction;
        }

        internal virtual void Show() { }
    }
}
