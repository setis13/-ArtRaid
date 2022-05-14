using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ArtRaid.Converters {
    /// <summary>
    ///     Represents converter "Object" -> "Boolean" </summary>
    [ValueConversion(typeof(object), typeof(bool))]
    public class CompareConverter : IValueConverter {
        public bool Inverted { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (Inverted == false) {
                return Object.Equals(parameter, value);
            } else {
                return !Object.Equals(parameter, value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter == null)
                return DependencyProperty.UnsetValue;
            return parameter;
        }
    }
}