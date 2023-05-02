namespace database_oop_project.UI
{
    public class Control
    {
        internal int X;
        internal int Y;
        internal int TabIndex;
        internal string Label;
        internal string Text;
        internal string Hint;
        internal ConsoleColor FrontColor;
        internal bool IsVisible = true;
        internal bool IsActive = false;
        internal Action CallerAction;

        public Control(int x, int y, int tabIndex, string label, string text, string hint, ConsoleColor frontColor, Action callerAction)
        {
            X = x;
            Y = y;
            TabIndex = tabIndex;
            Text = text;
            FrontColor = frontColor;
            Hint = hint;
            Label = label;
            CallerAction = callerAction;
        }

        internal virtual void Show(Window window) { }
    }
}
