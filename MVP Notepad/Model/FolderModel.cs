using MVP_Notepad.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_Notepad.Model
{
    internal class FolderModel
    {
        public bool IsFile { get; set; } = false;
        public string Path { get; set; } = "";
        public string Name => Path.Substring(Path.LastIndexOf('\\') + 1);
        public ObservableCollection<FolderViewModel> Children { get; } = new ObservableCollection<FolderViewModel>();
    }
}
