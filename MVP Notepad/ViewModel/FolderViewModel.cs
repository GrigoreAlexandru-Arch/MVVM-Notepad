using MVP_Notepad.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_Notepad.ViewModel
{
    internal class FolderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        FolderModel _folderModel;

        public FolderViewModel(FolderModel folderModel)
        {
            _folderModel = folderModel;
        }

        public string Path
        {
            get => _folderModel.Path;
            set
            {
                if(value!=_folderModel.Path)
                {
                    _folderModel.Path = value;
                    NotifyPropertyChanged("Path");
                }
            }
        }

        public bool IsFile
        {
            get => _folderModel.IsFile;
            set
            {
                if (value != _folderModel.IsFile)
                {
                    _folderModel.IsFile = value;
                    NotifyPropertyChanged("IsFile");
                }
            }
        }

        public string Name => _folderModel.Name;
        public ObservableCollection<FolderViewModel> Children => _folderModel.Children;
    }
}
