using System;
using System.IO;
using System.Windows.Input;
using Microsoft.Win32;
using MVP_Notepad.Model;
using MVP_Notepad.View;
using static MVP_Notepad.ViewModel.TabsViewModel;
using static MVP_Notepad.ViewModel.FileTreeViewModel;
using System.Windows;
using System.Collections.ObjectModel;

namespace MVP_Notepad.ViewModel
{
    internal class MainWindowCommands
    {
        public MainWindowCommands()
        {
            newTabCommand = new DelegateCommand(NewTab);
            exitCommand = new DelegateCommand(Exit);
            saveAsCommand = new DelegateCommand(SaveAs);
            saveCommand = new DelegateCommand(Save);
            openFileCommand = new DelegateCommand(OpenFile);
            findCommand = new DelegateCommand(Find);
            replaceCommand = new DelegateCommand(Replace);
            replaceAllCommand = new DelegateCommand(ReplaceAll);
            closeTabCommand = new DelegateCommand(CloseTab);
            closeWindowCommand = new DelegateCommand(CloseWindow);
            openFolderCommand = new DelegateCommand(OpenFolder);
            aboutCommand = new DelegateCommand(About);
        }

        private readonly DelegateCommand newTabCommand;
        public ICommand NewTabCommand => newTabCommand;
        private void NewTab(object parameter)
        {
            TabViewModel newTab = new TabViewModel(new TabModel() { Header = $"File {++LastTabNumber}", Content = "", Index = Tabs.Count });
            Tabs.Add(newTab);
        }

        private readonly DelegateCommand exitCommand;
        public ICommand ExitCommand => exitCommand;
        private void Exit(object Parameter)
        {
            Environment.Exit(0);
        }

        private static void SaveTab(int index, string Path)
        {
            try
            {
                File.WriteAllText(Path, Tabs[SelectedTabIndex].Content);
                Tabs[index].Header = Path.Substring(Path.LastIndexOf('\\') + 1);
                Tabs[index].Path = Path.Remove(Path.LastIndexOf('\\') + 1);
                Tabs[index].Saved = true;

                ObservableCollection<FolderViewModel> NewFolders =
               new ObservableCollection<FolderViewModel>();

                for (int folderIndex = 0; folderIndex < Folders.Count; folderIndex++)
                {
                    FolderModel folderModel = new FolderModel() { Path = Folders[folderIndex].Path };
                    FolderViewModel folderViewModel = new FolderViewModel(folderModel);

                    DFSFile(folderViewModel);
                    NewFolders.Add(folderViewModel);
                }

                Folders.Clear();
                foreach (FolderViewModel folder in NewFolders)
                {
                    Folders.Add(folder);
                }
            }
            catch (Exception e)
            {
                _ = MessageBox.Show(e.Message);
            }
        }

        private readonly DelegateCommand saveAsCommand;
        public ICommand SaveAsCommand => saveAsCommand;
        private void SaveAs(object Parameter)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    Filter = "Text file (*.txt)|*.txt"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    SaveTab(SelectedTabIndex, saveFileDialog.FileName);
                }
            }
            catch (Exception e)
            {
                _ = MessageBox.Show(e.Message);
            }
        }

        private readonly DelegateCommand saveCommand;
        public ICommand SaveCommand => saveCommand;
        private void Save(object Parameter)
        {
            if (Tabs[SelectedTabIndex].Path == "")
            {
                SaveAs(Parameter);
            }
            else
            {
                try
                {
                    File.WriteAllText(Tabs[SelectedTabIndex].Path + Tabs[SelectedTabIndex].Header, Tabs[SelectedTabIndex].Content);
                    Tabs[SelectedTabIndex].Saved = true;
                }
                catch (Exception e)
                {
                    _ = MessageBox.Show(e.Message);
                }
            }
        }

        private readonly DelegateCommand openFileCommand;
        public ICommand OpenFileCommand => openFileCommand;
        private void OpenFile(object Parameter)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Filter = "Text file (*.txt)|*.txt|All files|*.*"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    NewTab(Parameter);
                    SelectedTabIndex = Tabs.Count - 1;

                    Tabs[SelectedTabIndex].Header = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf('\\') + 1);
                    Tabs[SelectedTabIndex].Path = openFileDialog.FileName.Remove(openFileDialog.FileName.LastIndexOf('\\') + 1);
                    Tabs[SelectedTabIndex].Content = File.ReadAllText(openFileDialog.FileName);
                    Tabs[SelectedTabIndex].Saved = true;
                }
            }
            catch (Exception e)
            {
                _ = MessageBox.Show(e.Message);
            }
        }

        private readonly DelegateCommand openFolderCommand;
        public ICommand OpenFolderCommand => openFolderCommand;
        private void OpenFolder(object Parameter)
        {
            try
            {
                using (System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog())
                {
                    System.Windows.Forms.DialogResult result = fbd.ShowDialog();

                    if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        FolderModel folderModel = new FolderModel() { Path = fbd.SelectedPath };
                        FolderViewModel folderViewModel = new FolderViewModel(folderModel);

                        DFSFile(folderViewModel);
                        Folders.Add(folderViewModel);
                    }
                }
            }
            catch (Exception e)
            {
                _ = MessageBox.Show(e.Message);
            }
        }

        private static void DFSFile(FolderViewModel folderViewModel)
        {
            try
            {
                if (!folderViewModel.IsFile)
                {
                    string[] directories = Directory.GetDirectories(folderViewModel.Path);
                    foreach (string directory in directories)
                    {
                        folderViewModel.Children.Add(new FolderViewModel(new FolderModel() { Path = directory }));
                    }
                    string[] files = Directory.GetFiles(folderViewModel.Path);
                    foreach (string file in files)
                    {
                        folderViewModel.Children.Add(new FolderViewModel(new FolderModel() { Path = file, IsFile = true }));
                    }
                    for (int index = 0; index < folderViewModel.Children.Count; index++)
                    {
                        DFSFile(folderViewModel.Children[index]);
                    }
                }
            }
            catch (Exception e)
            {
                _ = MessageBox.Show(e.Message);
            }
        }

        private readonly DelegateCommand findCommand;
        public ICommand FindCommand => findCommand;
        private void Find(object Parameter)
        {
            FindDialog findDialog = new FindDialog();

            _ = findDialog.ShowDialog();

        }

        private readonly DelegateCommand replaceCommand;
        public ICommand ReplaceCommand => replaceCommand;
        private void Replace(object Parameter)
        {
            ReplaceDialog replaceDialog = new ReplaceDialog();

            _ = replaceDialog.ShowDialog();

        }

        private readonly DelegateCommand replaceAllCommand;
        public ICommand ReplaceAllCommand => replaceAllCommand;
        private void ReplaceAll(object Parameter)
        {
            ReplaceAllDialog replaceAllDialog = new ReplaceAllDialog();

            _ = replaceAllDialog.ShowDialog();

        }

        private readonly DelegateCommand closeWindowCommand;
        public ICommand CloseWindowCommand => closeWindowCommand;
        private void CloseWindow(object Parameter)
        {
            try
            {
                for (int indexPass = 0; indexPass < Tabs.Count; indexPass++)
                {
                    if (!Tabs[indexPass].Saved)
                    {
                        CloseMainWindowDialog closeMainWindowDialog = new CloseMainWindowDialog();
                        if ((bool)closeMainWindowDialog.ShowDialog())
                        {
                            for (int index = indexPass; index < Tabs.Count; index++)
                            {
                                if (!Tabs[index].Saved)
                                {
                                    if (Tabs[index].Path == "")
                                    {
                                        SaveFileDialog saveFileDialog = new SaveFileDialog()
                                        {
                                            Filter = "Text file (*.txt)|*.txt"
                                        };

                                        if (saveFileDialog.ShowDialog() == true)
                                        {
                                            SaveTab(index, saveFileDialog.FileName);
                                        }
                                    }
                                    else
                                    {
                                        File.WriteAllText(Tabs[index].Path + Tabs[index].Header, Tabs[index].Content);
                                        Tabs[index].Saved = true;
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                _ = MessageBox.Show(e.Message);
            }
        }

        private readonly DelegateCommand closeTabCommand;
        public ICommand CloseTabCommand => closeTabCommand;
        private void CloseTab(object Parameter)
        {
            try
            {
                if (Parameter is int index)
                {
                    if (!Tabs[index].Saved)
                    {
                        CloseFileDialog closeFileDialog = new CloseFileDialog();
                        bool save = (bool)closeFileDialog.ShowDialog();
                        if (save)
                        {
                            if (Tabs[index].Path == "")
                            {
                                SaveFileDialog saveFileDialog = new SaveFileDialog()
                                {
                                    Filter = "Text file (*.txt)|*.txt"
                                };

                                if (saveFileDialog.ShowDialog() == true)
                                {
                                    SaveTab(index, saveFileDialog.FileName);
                                }
                            }
                            else
                            {
                                File.WriteAllText(Tabs[index].Path + Tabs[index].Header, Tabs[index].Content);
                                Tabs[index].Saved = true;
                            }
                        }
                    }

                    Tabs.RemoveAt(index);
                    for (int tabsIndex = index; tabsIndex < Tabs.Count; tabsIndex++)
                    {
                        Tabs[tabsIndex].Index = tabsIndex;
                    }
                }
            }
            catch (Exception e)
            {
                _ = MessageBox.Show(e.Message);
            }
        }

        private readonly DelegateCommand aboutCommand;
        public ICommand AboutCommand => aboutCommand;
        private void About(object Parameter)
        {
            AboutDialog aboutDialog = new AboutDialog();

            _ = aboutDialog.ShowDialog();

        }
    }
}
