using System;
using System.Windows.Input;
using Typewriter.Core;
using Typewriter.Core.Item;
using Typewriter.ViewModels;

namespace Typewriter.Commands
{
    public class AddNewManuscriptItemCommand : ICommand
    {
        private readonly IManuscriptService _manuscriptService;

        public AddNewManuscriptItemCommand(IManuscriptService manuscriptService)
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
            return parameter is ProjectCategoryViewModel;
        }

        public void Execute(object parameter)
        {
            if (parameter is ProjectCategoryViewModel)
            {
                if (!(parameter is ProjectCategoryViewModel vm))
                {
                    return;
                }

                var category = _manuscriptService.GetCategoryById(vm.Id);

                var id = category.Items.Count == 0 ? 0 : category.Items[category.Items.Count - 1].Id + 1;

                category.AddItem(new ManuscriptItem(id, category.Id, category.ProjectId, "New Item"));
            }
        }
    }
}
