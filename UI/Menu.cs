namespace database_oop_project.UI
{
    // класс для меню
    internal class Menu : Control
    {
        internal Dictionary<string, Action> MenuItems;
        internal string ActiveKey;

        public Menu(int x, int y, byte tabIndex, string label, string text, ConsoleColor frontColor, Dictionary<string, Action> items)
            : base(x, y, tabIndex, label, text, frontColor, null)
        {
            Label = string.Empty;
            MenuItems = items;
            ActiveKey = MenuItems.First().Key;
        }

        internal override void Show()
        {
            if (IsVisible)
            {
                int y = 0;
                foreach (KeyValuePair<string, Action> k in MenuItems)
                {
                    string item = k.Key == ActiveKey ? $"> {k.Key}" : k.Key;
                    for (int i = 0; i < item.Length; i++)
                    {
                        Parent.AddChar(item[i], k.Key == ActiveKey ? ConsoleColor.White : FrontColor, realX + i, realY + y);
                    }
                    y++;
                }
            }
        }
    }
}
