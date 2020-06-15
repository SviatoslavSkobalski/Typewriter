using System;
using System.Windows.Input;
using Typewriter.Core;
using Typewriter.ViewModels;
using TypeWriter.Windows.Interfaces;

namespace Typewriter.Commands
{
    public class SaveProjectCommand : ICommand
    {
        private readonly IManuscriptService _manuscriptService;
        private readonly IWindowsDialogService _windowsDialogService;

        public SaveProjectCommand(IManuscriptService manuscriptService, IWindowsDialogService windowsDialogService)
        {
            _manuscriptService = manuscriptService;
            _windowsDialogService = windowsDialogService;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return parameter is ManuscriptProjectViewModel;
        }

        public void Execute(object parameter)
        {
            try
            {
                var source = _windowsDialogService.ShowSaveFileDialog(TypeWriter.Windows.Services.ItemFilterKey.ProjectFiles);

                var project = parameter as ManuscriptProjectViewModel;

                if (project == null)
                {
                    return;
                }

                _manuscriptService.SaveToFile(source, project.ToManuscriptProject(source));
            }
            catch (Exception e)
            {
                _windowsDialogService.ShowErrorMessageDialog(e.Message, e.Source);
            }
        }
    }
}
