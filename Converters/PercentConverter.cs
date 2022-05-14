using ArtRaid.Extensions;
using ArtRaid.ViewModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ArtRaid.Converters {
    public class PercentConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (int)(100 * (float)value) + "%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
