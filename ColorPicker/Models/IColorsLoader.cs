using System.Windows.Media;

namespace ColorPicker.Models
{
    public interface IColorsLoader
    {
        Color[] Load();
        void Save(Color[] brushes);
    }
}
