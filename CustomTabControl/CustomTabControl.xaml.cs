using System.Windows;
using System.Windows.Controls;

namespace CustomControls
{
    /// <summary>
    /// CustomTabControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CustomTabControl : TabControl
    {
        public static readonly RoutedEvent TabItemClosedEvent =
            EventManager.RegisterRoutedEvent("TabItemClosed", RoutingStrategy.Direct,
                typeof(RoutedEventHandler), typeof(CustomTabControl));

        public event RoutedEventHandler TabItemClosed
        {
            add { AddHandler(TabItemClosedEvent, value); }
            remove { RemoveHandler(TabItemClosedEvent, value); }
        }

        public CustomTabControl()
        {
            InitializeComponent();
        }

        private void TabHeaderClose_Click(object sender, RoutedEventArgs e)
        {
            var element = sender as FrameworkElement;

            RaiseEvent(new RoutedEventArgs(TabItemClosedEvent, element.DataContext));
        }
    }
}
