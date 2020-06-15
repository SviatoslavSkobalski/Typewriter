using System;
using System.IO;
using System.Windows.Input;
using Typewriter.Core;
using Typewriter.Core.Item;
using Typewriter.ViewModels;
using TypeWriter.Windows.Interfaces;

namespace Typewriter.Commands
{
    public class AddExistingManuscriptItemCommand : ICommand
    {
        private readonly IManuscriptService _manuscriptService;
        private readonly IWindowsDialogService _windowsDialogService;

        public AddExistingManuscriptItemCommand(IManuscriptService manuscriptService, IWindowsDialogService windowsDialogService)
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
            return parameter is ProjectCategoryViewModel;
        }

        public void Execute(object parameter)
        {
            var vm = parameter as ProjectCategoryViewModel;

            var ctg = _manuscriptService.GetCategoryById(vm.Id);

            var source = _windowsDialogService.ShowOpenFileDialog(TypeWriter.Windows.Services.ItemFilterKey.TextFiles);

            if (File.Exists(source))
            {
                var item = new ManuscriptItem(ctg.Items.Count == 0 ? 0 : ctg.Items.Count + 1, ctg.Id, ctg.ProjectId, Path.GetFileNameWithoutExtension(source), source);

                ctg.AddItem(item);
            }
        }
    }
}
