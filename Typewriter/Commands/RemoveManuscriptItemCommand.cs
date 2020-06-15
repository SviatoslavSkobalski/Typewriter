using System;
using System.Windows.Input;
using Typewriter.Core;
using Typewriter.ViewModels;

namespace Typewriter.Commands
{
    public class RemoveManuscriptItemCommand : ICommand
    {
        private readonly IManuscriptService _manuscriptService;

        public RemoveManuscriptItemCommand(IManuscriptService manuscriptService)
        {
            _manuscriptService = manuscriptService;
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
            if (parameter is ProjectItemViewModel)
            {
                if (!(parameter is ProjectItemViewModel vm))
                {
                    return;
                }

                var category = _manuscriptService.GetCategoryById(vm.CtgId);

                var item = category.GetItemById(vm.Id);

                if (item == null)
                {
                    return;
                }

                category.RemoveItem(item);
            }
        }
    }
}
