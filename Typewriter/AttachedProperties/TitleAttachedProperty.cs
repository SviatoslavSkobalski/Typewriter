using System.Windows;

namespace Typewriter.AttachedProperties
{
    public class TitleAttachedProperty : DependencyObject
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.RegisterAttached("Title", typeof(string), typeof(TitleAttachedProperty), new PropertyMetadata("TypeWriter", new PropertyChangedCallback(ApplicationTitleChanged)));

        public static void SetTitle(DependencyObject d, string value)
        {
            d.SetValue(TitleProperty, value);
        }

        public static string GetTitle(DependencyObject d)
        {
            return (string)d.GetValue(TitleProperty);
        }

        private static void ApplicationTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var title = e.NewValue as string;
            if (!string.IsNullOrWhiteSpace(title))
            {
                Application.Current.MainWindow.Title = title;
            }
        }
    }
}
