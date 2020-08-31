using System.Windows;
using System.Windows.Controls;

namespace CustomControls
{
    public class ComboBoxItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SelectedItemTemplate { get; set; }
        public DataTemplate ItemTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            bool selected = false;
            FrameworkElement fe = container as FrameworkElement;
            if (fe != null)
            {
                DependencyObject parent = fe.TemplatedParent;
                if (parent != null)
                {
                    ComboBox cbo = parent as ComboBox;
                    if (cbo != null)
                        selected = true;
                }
            }
            if (selected)
                return SelectedItemTemplate;
            else
                return ItemTemplate;
        }
    }

    // Xaml Example Template...
    /*
     <ComboBox.ItemTemplateSelector>
        <local:ComboBoxItemTemplateSelector>
            <local:ComboBoxItemTemplateSelector.ItemTemplate>
                <DataTemplate>
                    <TextBlock Text="{Binding}"/>
                </DataTemplate>
            </local:ComboBoxItemTemplateSelector.ItemTemplate>
            <local:ComboBoxItemTemplateSelector.SelectedItemTemplate>
                <DataTemplate>
                    <TextBlock Text="{Binding}"/>
                </DataTemplate>
            </local:ComboBoxItemTemplateSelector.SelectedItemTemplate>
        </local:ComboBoxItemTemplateSelector>
    </ComboBox.ItemTemplateSelector>
    */
}
