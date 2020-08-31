using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CustomControls
{
    public class FilterListBox : ListBox
    {
        public object ViewSource
        {
            get { return GetValue(ViewSourceProperty); }
            set { SetValue(ViewSourceProperty, value); }
        }

        public static readonly DependencyProperty ViewSourceProperty =
            DependencyProperty.Register("ViewSource", typeof(object), typeof(FilterListBox), 
                new UIPropertyMetadata(null, UpdateViewSource));

        private static void UpdateViewSource(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var flb = (FilterListBox)d;

            // 하나의 CollectionView를 공유하고자 하면 아래 코드로 작성
            // flb.ItemsSource = CollectionViewSource.GetDefaultView(e.NewValue);

            // 각각의 CollectionView를 가지고자 하면 아래 코드로 작성
            flb.ItemsSource = new CollectionViewSource() { Source = e.NewValue }.View;

            flb.RaiseEvent(new RoutedEventArgs(RegisterViewEvent));
        }

        public static readonly RoutedEvent RegisterViewEvent =
            EventManager.RegisterRoutedEvent("RegisterView", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(FilterListBox));

        public event RoutedEventHandler RegisterView
        {
            add { AddHandler(RegisterViewEvent, value); }
            remove { RemoveHandler(RegisterViewEvent, value); }
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ViewCountEvent));

            base.OnItemsChanged(e);
        }

        public static readonly RoutedEvent ViewCountEvent =
            EventManager.RegisterRoutedEvent("ViewCount", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(FilterListBox));

        public event RoutedEventHandler ViewCount
        {
            add { AddHandler(ViewCountEvent, value); }
            remove { RemoveHandler(ViewCountEvent, value); }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)SelectedScrollIntoView);

            base.OnSelectionChanged(e);
        }

        private void SelectedScrollIntoView()
        {
            if (SelectedItem == null) return;

            ScrollIntoView(SelectedItem);
        }

        /* legacy code...
        public int MaxDrawItemCount
        {
            get { return (int)GetValue(MaxDrawItemCountProperty); }
            set { SetValue(MaxDrawItemCountProperty, value); }
        }

        public static readonly DependencyProperty MaxDrawItemCountProperty =
            DependencyProperty.Register("DrawToItemNumber", typeof(int),
                typeof(FilterListBox), new UIPropertyMetadata(0));

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            if (MaxDrawItemCount > 0 && Items.Count > 0)
            {
                var listBoxItem = ItemContainerGenerator.ContainerFromItem(Items[0]) as ListBoxItem;

                if (listBoxItem != null)
                {
                    if (Double.IsNaN(Height) || MaxDrawItemCount < Items.Count)
                        Height = listBoxItem.ActualHeight * MaxDrawItemCount + 4.0;

                    else
                        Height = listBoxItem.ActualHeight * Items.Count + 4.0;
                }
            }

            base.OnRenderSizeChanged(sizeInfo);
        }
        */
    }
}
