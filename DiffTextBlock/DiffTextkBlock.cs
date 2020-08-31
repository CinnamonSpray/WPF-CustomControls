using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using DiffMatchPatch;

namespace CustomControls
{
    public class DiffTextkBlock : TextBlock
    {
        private static diff_match_patch _DIFF;

        static DiffTextkBlock()
        {
            _DIFF = new diff_match_patch();
        }

        public string FirstText
        {
            get { return GetValue(FirstTextProperty).ToString(); }
            set { SetValue(FirstTextProperty, value); }
        }

        public static readonly DependencyProperty FirstTextProperty =
            DependencyProperty.Register("FirstText", typeof(string), typeof(DiffTextkBlock),
                new FrameworkPropertyMetadata(string.Empty, UpdatetFirstText));

        public string SecondText
        {
            get { return GetValue(SecondTextProperty).ToString(); }
            set { SetValue(SecondTextProperty, value); }
        }

        public static readonly DependencyProperty SecondTextProperty =
            DependencyProperty.Register("SecondText", typeof(string), typeof(DiffTextkBlock),
                new FrameworkPropertyMetadata(string.Empty, UpdateSecondtText));

        private static void UpdatetFirstText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dtb = d as DiffTextkBlock;

            if (dtb == null) return;

            var temp = dtb.FirstText;

            dtb.Inlines.Clear();

            dtb.Inlines.Add(new Run(temp)
            {
                Background = Brushes.Orange,
            });
        }

        private static void UpdateSecondtText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dtb = d as DiffTextkBlock;

            if (dtb == null) return;

            dtb.Inlines.Clear();

            DiffMatchText(dtb, dtb.FirstText, dtb.SecondText);
        }

        private static void DiffMatchText(DiffTextkBlock dtb, string first, string second)
        {
            var diffs = _DIFF.diff_main(first, second);
            _DIFF.diff_cleanupSemantic(diffs);

            foreach (var diff in diffs)
            {
                switch (diff.operation)
                {
                    case Operation.EQUAL: dtb.Inlines.Add(diff.text); break;

                    case Operation.DELETE:
                        dtb.Inlines.Add(new Run(diff.text)
                        {
                            Background = Brushes.Orange
                        });
                        break;

                    case Operation.INSERT: break;
                }
            }
        }
    }
}
