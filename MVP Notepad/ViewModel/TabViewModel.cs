using MVP_Notepad.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_Notepad.ViewModel
{
    internal class TabViewModel : INotifyPropertyChanged
    {
        private readonly TabModel _tabModel;

        public TabViewModel(TabModel tabModel)
        {
            _tabModel = tabModel;
        }
        public string Header
        {
            get => _tabModel.Header;
            set
            {
                if (value != _tabModel.Header)
                {
                    _tabModel.Header = value;
                    NotifyPropertyChanged("Header");
                }
            }
        }
        public string Content
        {
            get => _tabModel.Content;
            set
            {
                if (value != _tabModel.Content)
                {
                    _tabModel.Content = value;
                    Saved = false;
                    NotifyPropertyChanged("Content");
                }
            }
        }

        public int SelectionStart
        {
            get => _tabModel.SelectionStart;
            set
            {
                if (value != _tabModel.SelectionStart)
                {
                    _tabModel.SelectionStart = value;
                    NotifyPropertyChanged("SelectionStart");
                }
            }
        }
        public int SelectionLength
        {
            get => _tabModel.SelectionLength;
            set
            {
                if (value != _tabModel.SelectionLength)
                {
                    _tabModel.SelectionLength = value;
                    NotifyPropertyChanged("SelectionLength");
                }
            }
        }

        public int Index
        {
            get => _tabModel.Index;
            set
            {
                if (value != _tabModel.Index)
                {
                    _tabModel.Index = value;
                    NotifyPropertyChanged("Index");
                }
            }
        }

        public bool Saved
        {
            get => _tabModel.Saved;
            set
            {
                if (value != _tabModel.Saved)
                {
                    _tabModel.Saved = value;
                    NotifyPropertyChanged("Saved");
                }
            }
        }

        public string Path
        {
            get => _tabModel.Path;
            set
            {
                if (value != _tabModel.Path)
                {
                    _tabModel.Path = value;
                    NotifyPropertyChanged("Path");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
