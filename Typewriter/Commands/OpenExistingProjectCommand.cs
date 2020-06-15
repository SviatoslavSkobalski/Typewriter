using System;
using System.Windows.Input;
using Prism.Events;
using Typewriter.Commands.Events;
using Typewriter.Core;
using Typewriter.ViewModels;
using TypeWriter.Windows.Interfaces;

namespace Typewriter.Commands
{
    public class OpenExistingProjectCommand : ICommand
    {
        private readonly IManuscriptService _manuscriptService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowsDialogService _windowsDialogService;

        public OpenExistingProjectCommand(IManuscriptService manuscriptService, IEventAggregator eventAggregator, IWindowsDialogService windowsDialogService)
        {
            _manuscriptService = manuscriptService;
            _eventAggregator = eventAggregator;
            _windowsDialogService = windowsDialogService;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                var res = _windowsDialogService.ShowConfirmationDialog("Save Current", "Confirmation");

                var proj = parameter as ManuscriptProjectViewModel;

                switch (res)
                {
                    case true:
                        var source = _windowsDialogService.ShowSaveFileDialog(TypeWriter.Windows.Services.ItemFilterKey.ProjectFiles);
                        _manuscriptService.SaveToFile(source, proj.ToManuscriptProject(source));
                        break;
                    case null:
                        return;
                    default:
                        break;
                }
            }

            try
            {
                var source = _windowsDialogService.ShowOpenFileDialog(TypeWriter.Windows.Services.ItemFilterKey.ProjectFiles);

                var project = _manuscriptService.GetFromFile(source);

                _eventAggregator.GetEvent<CurrentProjectChanged>().Publish(project);
            }
            catch (Exception e)
            {
                _windowsDialogService.ShowErrorMessageDialog(e.Message, e.Source);
            }
        }
    }
}
