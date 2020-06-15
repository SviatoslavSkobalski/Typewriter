using System;
using System.IO;
using System.Windows.Input;
using Typewriter.Core;
using Typewriter.Core.Reference;
using Typewriter.ViewModels;
using TypeWriter.Windows.Interfaces;

namespace Typewriter.Commands
{
    public class AddItemReferenceCommand : ICommand
    {
        private readonly IManuscriptService _manuscriptService;
        private readonly IWindowsDialogService _windowsDialogService;

        public AddItemReferenceCommand(IManuscriptService manuscriptService, IWindowsDialogService windowsDialogService)
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
            return parameter is ProjectItemViewModel;
        }

        public void Execute(object parameter)
        {
            var vm = parameter as ProjectItemViewModel;

            var item = _manuscriptService.GetCategoryById(vm.CtgId).GetItemById(vm.Id);

            var source = _windowsDialogService.ShowOpenFileDialog(TypeWriter.Windows.Services.ItemFilterKey.ImageFiles);

            if (File.Exists(source))
            {
                var id = item.References.Count == 0 ? 0 : item.References[item.References.Count - 1].Id + 1;

                item.AddReference(new ManuscriptItemReference(id, item.Id, source));
            }
        }
    }
}
