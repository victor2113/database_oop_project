using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database_oop_project.UI
{
    public class Status
    {
        internal string Text;
        internal ConsoleColor FrontColor;

        public Status(string text, ConsoleColor frontColor)
        {
            Text = text;
            FrontColor = frontColor;
        }
    }
}
