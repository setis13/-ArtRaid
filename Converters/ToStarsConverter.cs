using ArtRaid.Extensions;
using ArtRaid.ViewModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ArtRaid.Converters {
    public class ToStarsConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            switch ((ArtRankEnum)value) {
                case ArtRankEnum.One:
                    return "1★";
                case ArtRankEnum.Two:
                    return "2★";
                case ArtRankEnum.Three:
                    return "3★";
                case ArtRankEnum.Four:
                    return "4★";
                case ArtRankEnum.Five:
                    return "5★";
                case ArtRankEnum.Six:
                    return "6★";
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
