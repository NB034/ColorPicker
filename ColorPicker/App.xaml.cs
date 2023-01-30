using ColorPicker.Models;
using ColorPicker.ViewModels;
using ColorPicker.Views;
using System.Windows;

namespace ColorPicker
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var loader = new JsonColorsLoader();
            var viewModel= new ColorPickerViewModel(loader);
            var view = new ColorPickerView(viewModel);

            view.Show();
        }
    }
}
