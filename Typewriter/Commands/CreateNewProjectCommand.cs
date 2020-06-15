using System;
using System.Windows.Input;
using Prism.Events;
using Typewriter.Commands.Events;
using Typewriter.Core;
using Typewriter.Core.Project;
using TypeWriter.Windows.Interfaces;

namespace Typewriter.Commands
{
    public class CreateNewProjectCommand : ICommand
    {
        private readonly IManuscriptService _manuscriptService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowsDialogService _windowsDialogService;

        public CreateNewProjectCommand(IManuscriptService manuscriptService, IEventAggregator eventAggregator, IWindowsDialogService windowsDialogService)
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

                if (res == true)
                {
                    var source = _windowsDialogService.ShowSaveFileDialog(TypeWriter.Windows.Services.ItemFilterKey.ProjectFiles);
                    _manuscriptService.SaveToFile(source, parameter as IManuscriptProject);
                }

                if (res == null)
                {
                    return;
                }
            }

            var project = _manuscriptService.CreateDefaultProject(true);

            _eventAggregator.GetEvent<CurrentProjectChanged>().Publish(project);
        }
    }
}
