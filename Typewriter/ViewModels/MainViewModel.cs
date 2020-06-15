using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Typewriter.Commands;
using Typewriter.Commands.Events;
using Typewriter.Core;
using Typewriter.Core.Project;
using TypeWriter.Windows.Interfaces;

namespace Typewriter.ViewModels
{
	public class MainViewModel : BindableBase, INavigationAware
	{
		private readonly IManuscriptService _manuscriptService;
		private readonly IEventAggregator _eventAggregator;
		private readonly IWindowsDialogService _windowsDialogService;
		private readonly IRegionManager _regionManager;
		private ManuscriptProjectViewModel _currentProject;
		private string _title;

		public MainViewModel(IManuscriptService manuscriptService, IEventAggregator eventAggregator, IWindowsDialogService windowsDialogService, IRegionManager regionManager)
		{
			CreateNewProjectCommand = new CreateNewProjectCommand(manuscriptService, eventAggregator, windowsDialogService);
			OpenExistingProjectCommand = new OpenExistingProjectCommand(manuscriptService, eventAggregator, windowsDialogService);
			SaveProjectCommand = new SaveProjectCommand(manuscriptService, windowsDialogService);
			ApplicationShutdownCommand = new ApplicationShutdownCommand();
			_manuscriptService = manuscriptService;
			_eventAggregator = eventAggregator;
			_windowsDialogService = windowsDialogService;
			_regionManager = regionManager;
		}

		public ManuscriptProjectViewModel CurrentProject
		{
			get { return _currentProject; }
			set { SetProperty(ref _currentProject, value); }
		}

		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		public CreateNewProjectCommand CreateNewProjectCommand { get; private set; }

		public OpenExistingProjectCommand OpenExistingProjectCommand { get; private set; }

		public SaveProjectCommand SaveProjectCommand { get; private set; }

		public ApplicationShutdownCommand ApplicationShutdownCommand { get; private set; }

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			_eventAggregator.GetEvent<CurrentProjectChanged>().Unsubscribe(OnCurrentProjectChanged);
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			_eventAggregator.GetEvent<CurrentProjectChanged>().Subscribe(OnCurrentProjectChanged);
		}

		private void OnCurrentProjectChanged(IManuscriptProject obj)
		{
			if (obj != null)
			{
				CurrentProject = new ManuscriptProjectViewModel(obj, _regionManager, _manuscriptService, _windowsDialogService);

				Title = CurrentProject.Name;

				var navigationParams = new NavigationParameters
				{
					{ "currentCategories", CurrentProject.Categories }
				};

				_regionManager.RequestNavigate("ProjectStructureRegion", "ProjectStructure", navigationParams);
			}
		}
	}
}
