using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace CommonUILib.CustomBehavior
{
    public class BorderBehavior : Behavior<Border>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseEnter += (d, e) => { AssociatedObject.Background = Brushes.Blue; };
            AssociatedObject.MouseLeave += (d, e) => { AssociatedObject.Background = Brushes.Red; };
        }


        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseEnter -= (d, e) => { AssociatedObject.Background = Brushes.Blue; };
            AssociatedObject.MouseLeave -= (d, e) => { AssociatedObject.Background = Brushes.Red; };
        }
    }
}
