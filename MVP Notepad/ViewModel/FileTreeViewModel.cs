using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MVP_Notepad.ViewModel.TabsViewModel;
using System.Windows.Input;
using MVP_Notepad.Model;
using System.IO;

namespace MVP_Notepad.ViewModel
{
    internal class FileTreeViewModel
    {
        public static ObservableCollection<FolderViewModel> Folders { get; } =
            new ObservableCollection<FolderViewModel>();

        public FileTreeViewModel()
        {
            openFileFromTreeCommand = new DelegateCommand(OpenFileFromTree);
        }
        public ICommand openFileFromTreeCommand;
        public ICommand OpenFileFromTreeCommand => openFileFromTreeCommand;

        public void OpenFileFromTree(object Parameter)
        {
            if( Parameter is FolderViewModel folderViewModel)
            {
                if (folderViewModel.IsFile)
                {
                    TabViewModel newTab = new TabViewModel(new TabModel() { Header = $"File {++LastTabNumber}", Content = "", Index = Tabs.Count });
                    Tabs.Add(newTab);
                    SelectedTabIndex = Tabs.Count - 1;

                    Tabs[SelectedTabIndex].Header = folderViewModel.Path.Substring(folderViewModel.Path.LastIndexOf('\\') + 1);
                    Tabs[SelectedTabIndex].Path = folderViewModel.Path.Remove(folderViewModel.Path.LastIndexOf('\\') + 1);
                    Tabs[SelectedTabIndex].Content = File.ReadAllText(folderViewModel.Path);
                    Tabs[SelectedTabIndex].Saved = true;
                }
            }
        }
    }
}
