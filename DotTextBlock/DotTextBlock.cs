using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace CustomControls
{
    public class DotTextBlock : TextBlock
    {
        public new string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public new static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string),
            typeof(DotTextBlock), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(UpdateText)));

        private static void UpdateText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tb = d as DotTextBlock;
            var text = tb.Text;

            if (string.IsNullOrEmpty(text))
            {
                tb.Inlines.Clear();

                tb.Inlines.Add(text == null ? string.Empty : text);
            }

            else
            {
                tb.Inlines.Clear();

                if (text.IndexOf("Dot:") < 0) tb.Inlines.Add(text);

                else
                {
                    var oldIndex = 0;
                    var indexs = AllIndexOf(text, "Dot:");

                    foreach (var index in indexs)
                    {
                        if (index > oldIndex)
                            tb.Inlines.Add(text.Substring(oldIndex, index - oldIndex));

                        switch (text.Substring(index, 5))
                        {
                            case "Dot:0": tb.Inlines.Add(new Run("●") { Foreground = Brushes.Red }); break;
                            case "Dot:1": tb.Inlines.Add(new Run("●") { Foreground = Brushes.Blue }); break;
                        }

                        oldIndex = index + 5;
                    }

                    if (oldIndex < text.Length)
                        tb.Inlines.Add(text.Substring(oldIndex));
                }
            }
        }

        private static IEnumerable<int> AllIndexOf(string text, string split)
        {
            List<int> result = new List<int>();

            int index = text.IndexOf(split);
            int splitLength = split.Length;

            int accrue = 0;

            while (true)
            {
                index = text.IndexOf(split);

                if (index == -1) break;

                result.Add(accrue += index);

                text = text.Remove(0, index + splitLength);

                accrue += splitLength;
            }

            return result;
        }
    }
}
