using ArtRaid.Extensions;
using ArtRaid.Properties;
using ArtRaid.ViewModels;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace ArtRaid.Converters {
    public class ArtToImageConverter : IValueConverter {
        private static string[] _resourcrNames = null;
        private static string[] ResourcrNames {
            get {
                if (_resourcrNames == null) {
                    var assembly = Assembly.GetExecutingAssembly();
                    string resName = assembly.GetName().Name + ".g.resources";
                    using (var stream = assembly.GetManifestResourceStream(resName)) {
                        using (var reader = new System.Resources.ResourceReader(stream)) {
                            _resourcrNames = reader.Cast<DictionaryEntry>().Select(entry =>
                                      (string)entry.Key).ToArray();
                        }
                    }
                }
                return _resourcrNames;
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string path = String.Empty;
            if (value == null) {
                return @"Resources/regular_star.png";
            }
            var viewModel = (ArtViewModel)value;
            switch (viewModel.kind) {
                case ArtKindEnum.Weapon:
                case ArtKindEnum.Helmet:
                case ArtKindEnum.Shield:
                case ArtKindEnum.Gloves:
                case ArtKindEnum.Chest:
                case ArtKindEnum.Boots:
                    path = $@"Resources/{viewModel.setKind.GetDescription() ?? viewModel.setKind.ToString()}_{viewModel.kind}.png";
                    break;
                case ArtKindEnum.Amulet:
                case ArtKindEnum.Ring:
                    //switch (viewModel.setKind) {
                    //    case ArtSetKindEnum.ShieldAccessory:
                    //        return $@"Resources/{viewModel.requiredFraction.GetDescription() ?? viewModel.requiredFraction.ToString()}_{viewModel.kind}.png";
                    //    case ArtSetKindEnum.ChangeHitType:
                    //        return $@"Resources/{viewModel.requiredFraction.GetDescription() ?? viewModel.requiredFraction.ToString()}_{viewModel.kind}.png";
                    //    default:
                    path = $@"Resources/{viewModel.requiredFraction.GetDescription() ?? viewModel.requiredFraction.ToString()}_{viewModel.kind}.png";
                    break;
                //}
                case ArtKindEnum.Banner:
                    path = $@"Resources/{viewModel.requiredFraction.GetDescription() ?? viewModel.requiredFraction.ToString()}_{viewModel.kind}.png";
                    break;
                case ArtKindEnum.Cloak:
                    path = $@"Resources/{viewModel.requiredFraction.GetDescription() ?? viewModel.requiredFraction.ToString()}_Pendant.png";
                    break;
            }
            if (ResourcrNames.Contains(path.ToLower())) {
                return path;
            } else {
                return @"Resources\regular_star.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
