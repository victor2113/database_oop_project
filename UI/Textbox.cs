namespace database_oop_project.UI
{
    // класс для отображения многострочного нередактируемого текста 
    internal class Textbox : Control
    {
        internal byte Width;
        internal byte Lines;

        public Textbox(int x, int y, int tabIndex, string label, string text, ConsoleColor frontColor, byte width, byte lines, Action callerAction) : base(x, y, tabIndex, label, text, frontColor, callerAction)
        {
            Width = width;
            Lines = lines;
        }

        internal override void Show()
        {
            if (IsVisible)
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    string[] text = Text.Split('\n');
                    int end = Math.Min(text.Length, Lines);
                    for (int i = 0; i < end; i++)
                    {
                        string s = text[i].Substring(0, Math.Min(Width, text[i].Length));
                        for (int j = 0; j < s.Length; j++)
                        {
                            Parent.AddChar(s[j], FrontColor, realX + 2 + j, realY + i);
                        }
                    }
                }
            }
        }
    }
}
