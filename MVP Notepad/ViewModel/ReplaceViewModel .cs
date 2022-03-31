using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using static MVP_Notepad.ViewModel.TabsViewModel;

namespace MVP_Notepad.ViewModel
{
    internal class ReplaceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string searchedText = "Text that should be replaced";

        public string SearchedText
        {
            get => searchedText;
            set
            {
                if (value != searchedText)
                {
                    searchedText = value;
                    NotifyPropertyChanged("SearchedText");
                }
            }
        }

        private string replacedText = "Text that will replace";

        public string ReplacedText
        {
            get => replacedText;
            set
            {
                if (value != replacedText)
                {
                    replacedText = value;
                    NotifyPropertyChanged("ReplacedText");
                }
            }
        }

        private bool ignoreLowercase;

        public bool IgnoreCase
        {
            get => ignoreLowercase;
            set
            {
                if (value != ignoreLowercase)
                {
                    ignoreLowercase = value;
                    NotifyPropertyChanged("IgnoreLowerCase");
                }
            }
        }

        private bool searchInAllTabs;

        public bool SearchInAllTabs
        {
            get => searchInAllTabs;
            set
            {
                if (value != searchInAllTabs)
                {
                    searchInAllTabs = value;
                    NotifyPropertyChanged("SearchInAllTabs");
                }
            }
        }

        private bool? dialogResult;

        public bool? DialogResult
        {
            get => dialogResult;
            set
            {
                if (value != dialogResult)
                {
                    dialogResult = value;
                    NotifyPropertyChanged("DialogResult");
                }
            }
        }

        public ReplaceViewModel()
        {
            replaceCommnad = new DelegateCommand(Replace);
        }

        private readonly DelegateCommand replaceCommnad;

        public ICommand ReplaceCommand => replaceCommnad;

        private void Replace(object parameter)
        {
            if (SearchedText == "")
            {
                return;
            }

            if (!IgnoreCase)
            {
                if (!SearchInAllTabs)
                {
                    Regex regex = new Regex(Regex.Escape(SearchedText));
                    Tabs[SelectedTabIndex].Content = regex.Replace(Tabs[SelectedTabIndex].Content, ReplacedText, 1);
                    DialogResult = true;
                }
                else
                {
                    for (int index = 0; index < Tabs.Count; index++)
                    {
                        Regex regex = new Regex(Regex.Escape(SearchedText));
                        string aux = Tabs[index].Content;
                        Tabs[index].Content = regex.Replace(Tabs[index].Content, ReplacedText, 1);
                        if (aux != Tabs[index].Content)
                        {
                            DialogResult = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                if (!SearchInAllTabs)
                {
                    Regex regex = new Regex(Regex.Escape(SearchedText.ToLower()), RegexOptions.IgnoreCase);
                    Tabs[SelectedTabIndex].Content = regex.Replace(Tabs[SelectedTabIndex].Content, ReplacedText, 1);
                    DialogResult = true;
                }
                else
                {
                    for (int index = 0; index < Tabs.Count; index++)
                    {
                        Regex regex = new Regex(Regex.Escape(SearchedText.ToLower()), RegexOptions.IgnoreCase);
                        string aux = Tabs[index].Content;
                        Tabs[index].Content = regex.Replace(Tabs[index].Content, ReplacedText, 1);
                        if (aux != Tabs[index].Content)
                        {
                            DialogResult = true;
                            break;
                        }
                    }
                }
            }
        }
    }
}