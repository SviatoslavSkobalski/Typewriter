using Prism.Mvvm;

namespace Typewriter.ViewModels
{
	public class ProjectItemReferenceViewModel : BindableBase
	{
		private string _source;

		public ProjectItemReferenceViewModel(int id, int itemId, string source)
		{
			Id = id;
			ItemId = itemId;
			_source = source;
		}

		public int Id { get; }

		public int ItemId { get; }

		public string Source
		{
			get { return _source; }
			set { SetProperty(ref _source, value); }
		}
	}
}
