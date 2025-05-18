using System.Windows.Controls;
using System.Windows;

namespace Bimbrownik_Desktop.Layouts   // ← dokładnie tak!
{
    public class BaseLayout : ContentControl   // ← public
    {
        static BaseLayout()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(BaseLayout),
                new FrameworkPropertyMetadata(typeof(BaseLayout)));
        }
    }
}