using System.Windows;
using System.Windows.Controls;

namespace WpfNet6.CommonUi.TemplateSelector
{
    public class ParamTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var template = new DataTemplate();
            if (container is not FrameworkElement element || item == null) return template;

            return template ?? new DataTemplate();
        }
    }
}