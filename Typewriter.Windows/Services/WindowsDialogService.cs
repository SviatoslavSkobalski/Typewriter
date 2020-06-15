using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;
using TypeWriter.Windows.Interfaces;

namespace TypeWriter.Windows.Services
{
    public class WindowsDialogService : IWindowsDialogService
    {
        private readonly Dictionary<ItemFilterKey, string> dialogFilters = new Dictionary<ItemFilterKey, string>()
        {
            {ItemFilterKey.ImageFiles,  "Image Files (*.jpg) | *.jpg"},
            {ItemFilterKey.ProjectFiles, "Project Files (*.mnscr) | *.mnscr" },
            {ItemFilterKey.TextFiles, "Text Files (*.txt) | *.txt" }
        };

        public void ShowInformationDialog(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowErrorMessageDialog(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool? ShowConfirmationDialog(string text, string caption)
        {
            MessageBoxResult res = MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (res == MessageBoxResult.Yes)
            {
                return true;
            }

            if (res == MessageBoxResult.Cancel)
            {
                return null;
            }

            return false;
        }

        public string ShowOpenFileDialog(ItemFilterKey filter)
        {
            var openFileDialog = new OpenFileDialog
            {
                DefaultExt = ExtensionKey.mnscr.ToString(),

                Filter = dialogFilters[filter]
            };

            openFileDialog.ShowDialog();

            return openFileDialog.FileName;
        }

        public string ShowSaveFileDialog(ItemFilterKey filter)
        {
            var saveFileDialog = new SaveFileDialog
            {
                DefaultExt = ExtensionKey.mnscr.ToString(),

                Filter = dialogFilters[filter]
            };

            saveFileDialog.ShowDialog();

            return saveFileDialog.FileName;
        }
    }
}
