using ColorPicker.Accessories;
using ColorPicker.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace ColorPicker.ViewModels
{
    public class ColorPickerViewModel : INotifyPropertyChanged
    {
        readonly IColorsLoader loader;
        private SolidColorBrush sampleColor;

        public ObservableCollection<SolidColorBrush> BrushesCollection { get; private set; }

        public SolidColorBrush SampleColor
        {
            get => sampleColor;
            set
            {
                sampleColor = value;
                NotifyPropertyChanged(nameof(SampleColor));
            }
        }

        readonly AutoEventCommandBase refreshSampleColorCommand;
        readonly AutoEventCommandBase addColorCommand;
        readonly AutoEventCommandBase deleteColorCommand;
        readonly AutoEventCommandBase saveChangesCommand;

        public AutoEventCommandBase RefreshSampleColorCommand => refreshSampleColorCommand;
        public AutoEventCommandBase AddColorCommand => addColorCommand;
        public AutoEventCommandBase DeleteColorCommand => deleteColorCommand;
        public AutoEventCommandBase SaveChangesCommand => saveChangesCommand;

        public ColorPickerViewModel(IColorsLoader loader)
        {
            this.loader = loader;
            BrushesCollection = new ObservableCollection<SolidColorBrush>();
            foreach (var color in loader.Load())
            {
                BrushesCollection.Add(new SolidColorBrush(color));
            }

            refreshSampleColorCommand = new AutoEventCommandBase(o => RefreshSampleColor(o), _ => true);
            addColorCommand = new AutoEventCommandBase(_ => BrushesCollection.Add(SampleColor), _ => true);
            deleteColorCommand = new AutoEventCommandBase(o => DeleteBrush(o), _ => true);
            saveChangesCommand = new AutoEventCommandBase(_ => SaveChanges(), _ => BrushesCollection.Any());

            sampleColor = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            NotifyPropertyChanged(nameof(SampleColor));
        }

        void RefreshSampleColor(object tuple)
        {
            if (tuple is Tuple<SlidersTypes, Byte> checkedTuple)
            {
                Color color = sampleColor.Color;
                switch (checkedTuple.Item1)
                {
                    case SlidersTypes.Alpha:
                        SampleColor = new SolidColorBrush(Color.FromArgb(checkedTuple.Item2, color.R, color.G, color.B));
                        break;
                    case SlidersTypes.Red:
                        SampleColor = new SolidColorBrush(Color.FromArgb(color.A, checkedTuple.Item2, color.G, color.B));
                        break;
                    case SlidersTypes.Green:
                        SampleColor = new SolidColorBrush(Color.FromArgb(color.A, color.R, checkedTuple.Item2, color.B));
                        break;
                    case SlidersTypes.Blue:
                        SampleColor = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, checkedTuple.Item2));
                        break;
                    default:
                        break;
                }
            }
        }

        void DeleteBrush(object brush)
        {
            if (brush is SolidColorBrush checkedBrush)
            {
                BrushesCollection.Remove(checkedBrush);
            }
        }

        void SaveChanges()
        {
            Color[] colors = new Color[BrushesCollection.Count];
            for (int i = 0; i < BrushesCollection.Count; i++)
            {
                colors[i] = BrushesCollection[i].Color;
            }
            loader.Save(colors);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
