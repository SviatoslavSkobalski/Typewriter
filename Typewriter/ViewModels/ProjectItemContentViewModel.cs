using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Prism.Mvvm;
using Prism.Regions;
using Typewriter.Commands;

namespace Typewriter.ViewModels
{
    public class ProjectItemContentViewModel : BindableBase, INavigationAware
    {
        private string _selectedFont;
        private List<string> _fonts;
        private List<int> _fontSizes;
        private int _fontSize;

        private string _content;
        private ObservableCollection<ProjectItemReferenceViewModel> _references;

        public ProjectItemContentViewModel()
        {
            Fonts = FontFamily.Families.Select(f => f.Name).ToList();

            FontSizes = new List<int>();

            for (int i = 8; i < 100; i += 2)
            {
                FontSizes.Add(i);
            }

            SelectedFont = Fonts.Where(font => font == "Arial").FirstOrDefault();
            FontSize = 12;
        }

        public List<int> FontSizes
        {
            get { return _fontSizes; }
            set { SetProperty(ref _fontSizes, value); }
        }

        public int FontSize
        {
            get { return _fontSize; }
            set { SetProperty(ref _fontSize, value); }
        }

        public string SelectedFont
        {
            get { return _selectedFont; }
            set { SetProperty(ref _selectedFont, value); }
        }

        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        public ObservableCollection<ProjectItemReferenceViewModel> References
        {
            get { return _references; }
            set { SetProperty(ref _references, value); }
        }

        public string ContentSource { get; private set; }

        public List<string> Fonts
        {
            get { return _fonts; }
            set { SetProperty(ref _fonts, value); }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Content = navigationContext.Parameters.GetValue<string>("itemContent");
            ContentSource = navigationContext.Parameters.GetValue<string>("contentSource");
            References = navigationContext.Parameters.GetValue<ObservableCollection<ProjectItemReferenceViewModel>>("itemReferences");
        }
    }
}
