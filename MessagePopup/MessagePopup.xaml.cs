using System.Windows;
using System.Windows.Controls.Primitives;

namespace CustomControls
{
    /// <summary>
    /// InfomationContent.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MessagePopup : Popup
    {
        public MessagePopup()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            IsOpen = false;
        }
    }
}
