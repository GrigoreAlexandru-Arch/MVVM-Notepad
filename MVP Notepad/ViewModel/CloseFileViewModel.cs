using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using static MVP_Notepad.ViewModel.TabsViewModel;
namespace MVP_Notepad.ViewModel
{
    internal class CloseFileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool? dialogResult;


        public CloseFileViewModel()
        {
            closeWithoutSavingCommand = new DelegateCommand(CloseWithoutSaving);
            saveCommand = new DelegateCommand(Save);
        }

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

        private readonly ICommand closeWithoutSavingCommand;
        public ICommand CloseWithoutSavingCommand => closeWithoutSavingCommand;
        private void CloseWithoutSaving(object Parameter)
        {
            DialogResult = false;
        }

        private readonly ICommand saveCommand;
        public ICommand SaveCommand => saveCommand;
        private void Save(object Parameter)
        {
            DialogResult = true;
        }
    }
}
