using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Regions;
using Typewriter.DAL;
using Typewriter.Services;
using Typewriter.ViewModels;

namespace Typewriter.Commands
{
    public class LoadContentCommand : ICommand
    {
        private readonly IRegionManager _regionManager;

        public LoadContentCommand(IRegionManager regionManager)
        {
            _regionManager = regionManager;
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
            if (!(parameter is ProjectItemViewModel vm))
            {
                return;
            }

            var navigationParams = new NavigationParameters();

            if (!string.IsNullOrEmpty(vm.Source))
            {
                var loader = new FileContentLoader(vm.Source);

                navigationParams.Add("itemContent", loader.LoadContent());
                navigationParams.Add("contentSource", vm.Source);
                navigationParams.Add("itemReferences", vm.References);
            }
            else
            {
                navigationParams.Add("itemContent", string.Empty);
                navigationParams.Add("contetnSource", string.Empty);
                navigationParams.Add("itemReferences", new ObservableCollection<ProjectItemReferenceViewModel>());
            }

            _regionManager.RequestNavigate(RegionKey.ProjectItemContentRegion.ToString(), "ProjectItemContent", navigationParams);
        }
    }
}
