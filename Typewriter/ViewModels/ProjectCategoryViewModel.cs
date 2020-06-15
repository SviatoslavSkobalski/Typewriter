using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace Typewriter.ViewModels
{
	public class ProjectCategoryViewModel : BindableBase
    {
		private string _name;

		private ObservableCollection<ProjectItemViewModel> _items;

		public ProjectCategoryViewModel(string name, int id, ObservableCollection<ProjectItemViewModel> items)
		{
			Name = name;
			Id = id;
			Items = items;
		}

		public ObservableCollection<ProjectItemViewModel> Items
		{
			get { return _items; }
			set { SetProperty(ref _items, value); }
		}

		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}

		public int Id { get; }
	}
}
