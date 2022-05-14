using ArtRaid.Converters;
using ArtRaid.Extensions;
using ArtRaid.ViewModels;
using ArtRaid.WebServices;
using Newtonsoft.Json;
using RaidExtractor.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ArtRaid {
    public partial class MainWindow : Window {

        public RootViewModel ViewModel => (RootViewModel)DataContext;

        public ArtWebService WebService = new ArtWebService();
        public int UserId = 1;

        private readonly Extractor raidExtractor;

        public MainWindow() {
            InitializeComponent();

            raidExtractor = new Extractor();
            DataContext = new RootViewModel();
        }

        private void HandlePreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            if (!e.Handled) {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }

        private void ListView_GotFocus(object sender, RoutedEventArgs e) {
            ((UIElement)sender).UpdateLayout();
            ((ListView)sender).SelectedItem = null;
        }

        private void ListView_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if (((ListView)sender).SelectedItem == ((FrameworkElement)e.OriginalSource).DataContext) {
                ((ListView)sender).SelectedItem = null;
                e.Handled = true;
            }
        }

        private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var artViewModel = (ArtViewModel)((FrameworkElement)sender).DataContext;
            if (ViewModel.Selection == null || ViewModel.SelectionChanging == true || ViewModel.Loading == true) return;
            foreach (HeroViewModel item in e.RemovedItems) {
                // Web
                try {
                    ViewModel.Loading = true;
                    ViewModel.ErrorMessage = null;
                    var webResult = await WebService.DeleteArt(UserId, artViewModel.id);
                    if (webResult.Success) {
                        ViewModel.UpdateFavorits();
                    } else {
                        ViewModel.ErrorMessage = webResult.Message;
                    }
                } finally {
                    ViewModel.Loading = false;
                }
            }

            foreach (HeroViewModel item in e.AddedItems) {
                // Web
                try {
                    ViewModel.Loading = true;
                    ViewModel.ErrorMessage = null;
                    var webResult = await WebService.AddArt(UserId, new Dtos.WebArtDto() {
                        UserId = UserId,
                        ArtId = artViewModel.id,
                        HeroId = (short)item.Id,
                    });
                    if (webResult.Success) {
                        ViewModel.UpdateFavorits();
                    } else {
                        ViewModel.ErrorMessage = webResult.Message;
                    }
                } finally {
                    ViewModel.Loading = false;
                }
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e) {
            ViewModel.Selection.Hero = null;
        }

        private async void Refresh_Click(object sender, RoutedEventArgs args) {
            ViewModel.Loading = true;
            ViewModel.Heroes.Clear();
            ViewModel.Arts.Clear();
            ViewModel.Favorits.Clear();
            ViewModel.UpdateFavorits();
#if !DEBUG
            ViewModel.Loading = true;
            AccountDump dump = await Task.Factory.StartNew(() => {
                try {
                    return raidExtractor.GetDump();
                } catch (Exception e) {
                    MessageBox.Show(e.ToString());
                    return null;
                }
            });
            ViewModel.Loading = false;
            if (dump == null) {
                return;
            }
#else
            var json = File.ReadAllText(@"D:\Develop\ArtRaid\dump.json");
            var dump = Newtonsoft.Json.JsonConvert.DeserializeObject<RaidExtractor.Core.AccountDump>(json);
#endif
            ViewModel.AllArts = dump.Artifacts.Select(e => ActConverter.Convert(e)).ToList();
            ViewModel.UpdateAtrs();

            var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var images = Directory.GetFiles(Path.Combine(rootPath, @"Resources\Heroes"));
            var heroes = images.Select(e => new HeroViewModel() { Id = Int32.Parse(e.Split('\\').Last().OnlyNumbers()), Icon = new BitmapImage(new Uri(e)) }).ToList();
            foreach (var hero in heroes) {
                ViewModel.Heroes.Add(hero);
            }

            // Web
            try {
                ViewModel.ErrorMessage = null;
                var webResult = await WebService.GetArts(UserId);
                if (webResult.Success) {
                    foreach (var art in ViewModel.AllArts) {
                        foreach (var webArt in webResult.Data) {
                            if (art.id == webArt.ArtId) {
                                art.Hero = ViewModel.Heroes.FirstOrDefault(e => e.Id == webArt.HeroId);
                                art.Comment = webArt.Comment;
                                art.Order = webArt.Order;
                            }
                        }
                    }
                } else {
                    ViewModel.ErrorMessage = webResult.Message;
                }
            } finally {
            }
            ViewModel.UpdateFavorits();
            ViewModel.Loading = false;
        }

        private void Favorite_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (e.AddedItems.Cast<ArtViewModel>().Any())
                ViewModel.Selection = e.AddedItems.Cast<ArtViewModel>().First();
        }

        private void Art_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            FavoritesListView.SelectedItem = null;
        }

        Task task;
        private async void Comment_KeyUp(object sender, KeyEventArgs e) {
            var selection = ViewModel.Selection;
            if (selection == null) return;
            #region [ single request + delay ]
            var localTask = task = Task.Delay(1000);
            await task;
            if (localTask != task) {
                return;
            }
            #endregion [ single request + delay ]

            var result = await WebService.UpdateArt(UserId, new Dtos.WebArtDto() {
                Id = selection.id,
                UserId = UserId,
                ArtId = selection.id,
                HeroId = (short)selection.Hero.Id,
                Level = (byte)selection.level,
                Order = selection.Order,
                Comment = selection.Comment,
            });
        }

        private async void Rating_KeyUp(object sender, KeyEventArgs e) {
            var selection = ViewModel.Selection;
            if (selection == null) return;
            #region [ single request + delay ]
            var localTask = task = Task.Delay(1000);
            await task;
            if (localTask != task) {
                return;
            }
            #endregion [ single request + delay ]

            var result = await WebService.UpdateArt(UserId, new Dtos.WebArtDto() {
                Id = selection.id,
                UserId = UserId,
                ArtId = selection.id,
                HeroId = (short)selection.Hero.Id,
                Level = (byte)selection.level,
                Order = selection.Order,
                Comment = selection.Comment,
            });
            ViewModel.UpdateFavorits();
        }
    }
}
