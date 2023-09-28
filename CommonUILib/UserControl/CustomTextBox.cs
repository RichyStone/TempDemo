using System.Windows;
using System.Windows.Controls;

namespace CommonUILib.UserControl
{
    public class CustomTextBox : TextBox
    {
        public bool IsHighLighted
        {
            get { return (bool)GetValue(IsHighLightedProperty); }
            set { SetValue(IsHighLightedProperty, value); }
        }

        public static readonly DependencyProperty IsHighLightedProperty =
            DependencyProperty.Register(
                "IsHighLighted",
                typeof(bool),
                typeof(CustomTextBox),
                new PropertyMetadata(false, (d, e) =>
                {
                }));

        static CustomTextBox()
        {
            HasTextPropertyKey = DependencyProperty.RegisterReadOnly("HasText", typeof(bool), typeof(CustomTextBox), new PropertyMetadata(false));
            HasTextProperty = HasTextPropertyKey.DependencyProperty;
        }

        public bool HasText
        {
            get { return (bool)GetValue(HasTextProperty); }
        }

        public static readonly DependencyPropertyKey HasTextPropertyKey;

        public static readonly DependencyProperty HasTextProperty;
    }
}