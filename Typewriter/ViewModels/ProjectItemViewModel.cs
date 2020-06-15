using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Regions;
using Typewriter.Commands;
using Typewriter.Core;
using TypeWriter.Windows.Interfaces;

namespace Typewriter.ViewModels
{
	public class ProjectItemViewModel : BindableBase
    {
		private string _name;

		private ObservableCollection<ProjectItemReferenceViewModel> _references;

		private bool _isFirstCreated;

		public ProjectItemViewModel(string name, int ctgId, int id, string source, ObservableCollection<ProjectItemReferenceViewModel> references,  IRegionManager regionManager, IManuscriptService manuscriptService, IWindowsDialogService windowsDialogService)
		{
			_name = name;
			_references = references;

			CtgId = ctgId;
			Id = id;
			Source = source;

			AddItemReferenceCommand = new AddItemReferenceCommand(manuscriptService, windowsDialogService);
			RemoveManuscriptItemCommand = new RemoveManuscriptItemCommand(manuscriptService);
			LoadContentCommand = new LoadContentCommand(regionManager);
		}

		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}

		public ObservableCollection<ProjectItemReferenceViewModel> References
		{
			get { return _references; }
			set { SetProperty(ref _references, value); }
		}

		public bool IsFirstCreated
		{
			get { return _isFirstCreated; }
			set { SetProperty(ref _isFirstCreated, value); }
		}

		public int CtgId { get; }

		public int Id { get; }

		public string Source { get; }

		public AddItemReferenceCommand AddItemReferenceCommand { get; private set; }

		public RemoveManuscriptItemCommand RemoveManuscriptItemCommand { get; private set; }

		public ICommand LoadContentCommand { get; private set; }
	}
}
