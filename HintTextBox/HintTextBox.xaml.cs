using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace CustomControls
{
    public partial class HintTextBox : UserControl
    {
        public Brush WaterMarkBrush
        {
            get { return WaterMarkBox.Foreground; }
            set { WaterMarkBox.Foreground = value; }
        }

        public string WaterMarkText
        {
            get { return (string)GetValue(WaterMarkTextProperty); }
            set { SetValue(WaterMarkTextProperty, value); }
        }

        public static readonly DependencyProperty WaterMarkTextProperty =
            DependencyProperty.Register("WaterMarkText", typeof(string),
                typeof(HintTextBox), new UIPropertyMetadata(string.Empty));

        public string BaseText
        {
            get { return (string)GetValue(BaseTextProperty); }
            set { SetValue(BaseTextProperty, value); }
        }

        public static readonly DependencyProperty BaseTextProperty =
            DependencyProperty.Register("BaseText", typeof(string),
                typeof(HintTextBox), new UIPropertyMetadata(string.Empty));

        public bool IsNumeric
        {
            get { return (bool)GetValue(IsNumericProperty); }
            set { SetValue(IsNumericProperty, value); }
        }

        public static readonly DependencyProperty IsNumericProperty =
            DependencyProperty.Register("IsNumeric", typeof(bool),
                typeof(HintTextBox), new UIPropertyMetadata(false));

        public static readonly RoutedEvent BaseTextChangedEvent =
            EventManager.RegisterRoutedEvent("BaseTextChanged", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(HintTextBox));

        public event RoutedEventHandler BaseTextChanged
        {
            add { AddHandler(BaseTextChangedEvent, value); }
            remove { RemoveHandler(BaseTextChangedEvent, value); }
        }

        public HintTextBox()
        {
            InitializeComponent();
            
            DataObject.AddPastingHandler(BaseTextBox, OnPaste);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)) && IsNumeric)
            {
                var pastedText = e.DataObject.GetData(typeof(string)) as string;

                if (MatchToNumberString(pastedText))
                    e.CancelCommand();
            }
        }

        private void BaseTextBox_PreviewTextInput(object sender, TextCompositionEventArgs args)
        {
            if (IsNumeric)
            {
                args.Handled = MatchToNumberString(args.Text);
            }
        }
        
        private bool MatchToNumberString(string str)
        {
            return Regex.IsMatch(str, "[^0-9]");
        }

        private void BaseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(BaseTextChangedEvent));
        }
    }

    internal class HintTextConditionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is bool && values[1] is bool)
            {
                bool hasText = !(bool)values[0];
                bool hasFocus = (bool)values[1];

                if (hasFocus || hasText)
                    return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}