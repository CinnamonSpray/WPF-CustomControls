using System.Windows;
using System.Windows.Controls;

namespace CustomControls
{
    /// <summary>
    /// FontDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FontDialog : Window
    {
        public FontInfo Font { get; set; }

        public FontDialog()
        {
            InitializeComponent();
        }

        private void SyncFontName()
        {
            string fontFamilyName = Font.Family.FamilyNames[System.Windows.Markup.XmlLanguage.GetLanguage("en-US")];
            bool foundMatch = false;
            int idx = 0;
            foreach (var item in FontChooser.lstFamily.Items)
            {
                if (fontFamilyName == item.ToString())
                {
                    foundMatch = true;
                    break;
                }
                idx++;
            }
            if (!foundMatch)
            {
                idx = 0;
            }
            FontChooser.lstFamily.SelectedIndex = idx;
            FontChooser.lstFamily.ScrollIntoView(FontChooser.lstFamily.Items[idx]);
        }

        private void SyncFontSize()
        {
            double fontSize = Font.Size;
            foreach (ListBoxItem item in FontChooser.lstFontSizes.Items)
            {
                if (double.Parse(item.Content.ToString()) != fontSize)
                {
                    continue;
                }
                item.IsSelected = true;
                break;
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Font = FontChooser.SelectedFont;
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SyncFontName();
            SyncFontSize();
        }
    }
}
