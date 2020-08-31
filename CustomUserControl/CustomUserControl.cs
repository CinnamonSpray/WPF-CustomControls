using System.Windows.Controls;

using CustomControls.InternalPattern;

namespace CustomControls
{
    public class CustomUserControl : UserControl
    {
        public static EventAggregator EvtHub { get; private set; }

        static CustomUserControl()
        {
            EvtHub = new EventAggregator();
        }
    }
}
