using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Typewriter.Controls
{
    public class ProjectItemControl : Control
    {
        public static readonly DependencyProperty IsEditingProperty =
           DependencyProperty.Register(nameof(IsEditing), typeof(bool), typeof(ProjectItemControl), new PropertyMetadata(false));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(ProjectItemControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsFirstCreatedProperty =
            DependencyProperty.Register("IsFirstCreated", typeof(bool), typeof(ProjectItemControl), new PropertyMetadata(false));

        private readonly string _textBoxName = "PART_TextBox";

        private TextBox _textBox;

        static ProjectItemControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProjectItemControl), new FrameworkPropertyMetadata(typeof(ProjectItemControl)));
        }

        public ProjectItemControl()
        {
            CommandBindings.Add(new CommandBinding(EditCommand, (s, e) => ToggleEditMode(e.Parameter.ToString())));
        }

        public static RoutedCommand EditCommand = new RoutedCommand("Edit", typeof(ProjectItemControl));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public bool IsEditing
        {
            get { return (bool)GetValue(IsEditingProperty); }
            set { SetValue(IsEditingProperty, value); }
        }

        public bool IsFirstCreated
        {
            get { return (bool)GetValue(IsFirstCreatedProperty); }
            set { SetValue(IsFirstCreatedProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _textBox = Template.FindName(_textBoxName, this) as TextBox;

            if (IsFirstCreated)
            {
                ToggleEditMode("Enter");
            }

            _textBox.LostFocus += OnTextBoxLostFocus;
        }

        private void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            ToggleEditMode("Exit");
        }

        private void ToggleEditMode(string mode)
        {
            switch (mode)
            {
                case "Enter":
                    IsEditing = true;
                    _textBox.Focus();
                    _textBox.SelectAll();
                    break;
                case "Exit":
                    IsEditing = false;
                    break;
                default:
                    break;
            }
        }
    }
}
