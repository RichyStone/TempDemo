using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CommonUILib.TemplateSelector
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
