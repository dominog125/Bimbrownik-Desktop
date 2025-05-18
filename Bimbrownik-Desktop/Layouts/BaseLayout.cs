using System.Windows.Controls;
using System.Windows;

namespace Bimbrownik_Desktop.Layouts
{
    public class BaseLayout : ContentControl
    {
        static BaseLayout()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(BaseLayout),
                new FrameworkPropertyMetadata(typeof(BaseLayout)));
        }
    }
}