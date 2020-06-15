using TypeWriter.Windows.Services;

namespace TypeWriter.Windows.Interfaces
{
    public interface IWindowsDialogService
    {
        bool? ShowConfirmationDialog(string text, string caption);
        void ShowInformationDialog(string text, string caption);
        void ShowErrorMessageDialog(string text, string caption);
        string ShowOpenFileDialog(ItemFilterKey filter);
        string ShowSaveFileDialog(ItemFilterKey filter);
    }
}