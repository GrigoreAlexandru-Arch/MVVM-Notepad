using MVP_Notepad.Model;
using MVP_Notepad.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using System;
using Microsoft.Win32;
using System.IO;

namespace MVP_Notepad.ViewModel
{
    internal class TabsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private static void NotifyStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
        public static int LastTabNumber { get; set; } = 1;

        public static ObservableCollection<TabViewModel> Tabs { get; set; } =
            new ObservableCollection<TabViewModel>()
            { new TabViewModel(new TabModel() { Header = "File 1", Content = "", Index = 0 }) };

        private static int selectedTabIndex;
        public static int SelectedTabIndex
        {
            get => selectedTabIndex;
            set
            {
                if (value != selectedTabIndex)
                {
                    selectedTabIndex = value;
                    NotifyStaticPropertyChanged("SelectedTabIndex");
                }
            }
        }

    }
}
