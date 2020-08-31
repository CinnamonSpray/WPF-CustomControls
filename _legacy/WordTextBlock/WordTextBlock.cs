using System;
using System.Windows;
using System.Windows.Controls;

namespace CustomControls.legacy
{
    public class WordTextBlock : TextBlock
    {
        public string FilterPhrase
        {
            get { return GetValue(FilterPhraseProperty).ToString(); }
            set { SetValue(FilterPhraseProperty, value); }
        }

        public static readonly DependencyProperty FilterPhraseProperty =
            DependencyProperty.Register("FilterPhrase", typeof(string), typeof(WordTextBlock),
                new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(UpdateText)));

        private static void UpdateText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ExtractFirstWord(d as WordTextBlock);
        }

        private static void ExtractFirstWord(WordTextBlock tb)
        {
            string filterphrase = tb.FilterPhrase;
            string text = tb.Text;

            tb.Inlines.Clear();

            int start = text.IndexOf(filterphrase, StringComparison.OrdinalIgnoreCase);

            start = start + filterphrase.Length;

            if (start > -1 && start < text.Length)
            {
                int last = text.IndexOfAny(new char[] { ' ', '\n' }, char.IsWhiteSpace(text[start]) ? start + 1 : start);

                tb.Inlines.Add(text.Substring(start, last < 0 ? text.Length - start : last - start));
            }
        }
    }
}
