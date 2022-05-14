using ArtRaid.Extensions;
using ArtRaid.ViewModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ArtRaid.Converters {
    public class ArtToImageConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return ArtKindEnum.None;
            }
            var viewModel = (ArtViewModel)value;
            switch (viewModel.kind) {
                case ArtKindEnum.Weapon:
                case ArtKindEnum.Helmet:
                case ArtKindEnum.Shield:
                case ArtKindEnum.Gloves:
                case ArtKindEnum.Chest:
                case ArtKindEnum.Boots:
                    return $@"Resources\{viewModel.setKind.GetDescription() ?? viewModel.setKind.ToString()}_{viewModel.kind}.png";
                case ArtKindEnum.Ring:
                case ArtKindEnum.Amulet:
                case ArtKindEnum.Banner:
                case ArtKindEnum.Cloak:
                    return $@"Resources\{viewModel.requiredFraction}_{viewModel.kind}.png";
            }
            throw new Exception();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
