using ColorPicker.ViewModels;
using System.Windows;

namespace ColorPicker.Views
{
    public partial class ColorPickerView : Window
    {
        public ColorPickerView(ColorPickerViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
