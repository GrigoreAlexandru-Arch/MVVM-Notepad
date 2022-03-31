using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_Notepad.Model
{
    internal class TabModel
    {
        public string Header { get; set; } = "";
        public string Content { get; set; } = "";
        public int SelectionStart { get; set; } = 0;
        public int SelectionLength { get; set; } = 0;
        public int Index { get; set; } = 0;
        public bool Saved { get; set; } = true;
        public string Path { get; set; } = "";
    }
}
