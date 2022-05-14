using ArtRaid.ViewModels;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ArtRaid.Converters {
    public class RarityToColorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            switch ((ArtRarityEnum)value) {
                case ArtRarityEnum.Common:
                    return new SolidColorBrush(Colors.Gray);
                case ArtRarityEnum.Uncommon:
                    return new SolidColorBrush(Colors.Green);
                case ArtRarityEnum.Rare:
                    return new SolidColorBrush(Colors.DodgerBlue);
                case ArtRarityEnum.Epic:
                    return new SolidColorBrush(Colors.Violet);
                case ArtRarityEnum.Legendary:
                    return new SolidColorBrush(Colors.Orange);
                default:
                    return new SolidColorBrush(Colors.White);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
