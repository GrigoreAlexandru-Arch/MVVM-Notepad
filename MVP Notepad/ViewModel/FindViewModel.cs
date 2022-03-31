using System.ComponentModel;
using System.Windows.Input;
using static MVP_Notepad.ViewModel.TabsViewModel;

namespace MVP_Notepad.ViewModel
{
    internal class FindViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string searchedText;

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

        public FindViewModel()
        {
            findCommnad = new DelegateCommand(Find);
        }

        private readonly DelegateCommand findCommnad;

        public ICommand FindCommnad => findCommnad;

        private void Find(object parameter)
        {
            if (SearchedText == null || SearchedText == "")
            {
                return;
            }

            if (!IgnoreCase)
            {
                if (!SearchInAllTabs)
                {
                    int aux = Tabs[SelectedTabIndex].Content.IndexOf(SearchedText);

                    if (aux != -1)
                    {
                        Tabs[SelectedTabIndex].SelectionStart = aux;
                        Tabs[SelectedTabIndex].SelectionLength = SearchedText.Length;
                        DialogResult = true;
                    }
                }
                else
                {
                    for (int index = 0; index < Tabs.Count; index++)
                    {
                        int aux = Tabs[index].Content.IndexOf(SearchedText);

                        if (aux != -1)
                        {
                            SelectedTabIndex = index;
                            Tabs[index].SelectionStart = aux;
                            Tabs[index].SelectionLength = SearchedText.Length;
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
                    int aux = Tabs[SelectedTabIndex].Content.ToLower().IndexOf(SearchedText.ToLower());

                    if (aux != -1)
                    {
                        Tabs[SelectedTabIndex].SelectionStart = aux;
                        Tabs[SelectedTabIndex].SelectionLength = SearchedText.Length;
                        DialogResult = true;
                    }
                }
                else
                {
                    for (int index = 0; index < Tabs.Count; index++)
                    {
                        int aux = Tabs[index].Content.ToLower().IndexOf(SearchedText.ToLower());

                        if (aux != -1)
                        {
                            SelectedTabIndex = index;
                            Tabs[index].SelectionStart = aux;
                            Tabs[index].SelectionLength = SearchedText.Length;
                            DialogResult = true;
                            break;
                        }
                    }
                }
            }
        }
    }
}