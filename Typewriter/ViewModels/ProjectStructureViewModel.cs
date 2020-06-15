using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Typewriter.Commands;
using Typewriter.Core;
using TypeWriter.Windows.Interfaces;

namespace Typewriter.ViewModels
{
	public class ProjectStructureViewModel : BindableBase, INavigationAware
	{
		private ObservableCollection<ProjectCategoryViewModel> _categories;

		private ObservableCollection<ProjectItemViewModel> _items;

		private ProjectCategoryViewModel _selectedCategory;

		public ProjectStructureViewModel(IManuscriptService manuscriptService, IWindowsDialogService windowsDialogService)
		{
			GetCategoryItemsCommand = new DelegateCommand<ProjectCategoryViewModel>(GetCategoryItems);
			AddNewManuscriptItemCommand = new AddNewManuscriptItemCommand(manuscriptService);
			AddExistingManuscriptItemCommand = new AddExistingManuscriptItemCommand(manuscriptService, windowsDialogService);
		}

		public ObservableCollection<ProjectCategoryViewModel> Categories
		{
			get { return _categories; }
			set { SetProperty(ref _categories, value); }
		}

		public ProjectCategoryViewModel SelectedCategory
		{
			get { return _selectedCategory; }
			set { SetProperty(ref _selectedCategory, value); }
		}

		public ObservableCollection<ProjectItemViewModel> Items
		{
			get { return _items; }
			set { SetProperty(ref _items, value); }
		}

		public DelegateCommand<ProjectCategoryViewModel> GetCategoryItemsCommand { get; private set; }

		public AddNewManuscriptItemCommand AddNewManuscriptItemCommand { get; private set; }

		public AddExistingManuscriptItemCommand AddExistingManuscriptItemCommand { get; private set; }

		#region Navigation
		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			var currentCategories = navigationContext.Parameters.GetValue<ObservableCollection<ProjectCategoryViewModel>>("currentCategories");

			Categories = currentCategories;

			SelectedCategory = Categories[0];
			GetCategoryItems(SelectedCategory);
		}
		#endregion

		private void GetCategoryItems(ProjectCategoryViewModel obj)
		{
			Items = obj.Items;
		}
	}
}
