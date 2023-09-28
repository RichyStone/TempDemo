using System.Windows;

namespace CommonUILib.AttachedProperty
{
    public class AttachedProperty
    {
        public static bool GetHasText(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasText);
        }

        public static void SetHasText(DependencyObject obj, bool value)
        {
            obj.SetValue(HasText, value);
        }

        public static readonly DependencyProperty HasText =
            DependencyProperty.RegisterAttached(nameof(HasText), typeof(bool), typeof(AttachedProperty),
                new PropertyMetadata(false, (d, e) =>
            {
            }));
    }
}