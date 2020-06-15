using System.Windows;
using System.Windows.Controls;

namespace Typewriter.Controls
{
    public class ProjectCategoryControl : Control
    {
        public static readonly DependencyProperty CategoryNameProperty =
            DependencyProperty.Register("CategoryName", typeof(string), typeof(ProjectCategoryControl), new PropertyMetadata(string.Empty));

        static ProjectCategoryControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProjectCategoryControl), new FrameworkPropertyMetadata(typeof(ProjectCategoryControl)));
        }

        public string CategoryName
        {
            get { return (string)GetValue(CategoryNameProperty); }
            set { SetValue(CategoryNameProperty, value); }
        }
    }
}
