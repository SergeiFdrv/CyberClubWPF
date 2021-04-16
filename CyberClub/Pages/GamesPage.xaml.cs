using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CyberClub.Controls;
using System.IO;
using CyberClub.ViewModels;

namespace CyberClub.Pages
{
    /// <summary>
    /// Логика взаимодействия для GamesPage.xaml
    /// </summary>
    public partial class GamesPage : AdminEditorPage
    {
        public GamesPage()
        {
            InitializeComponent();
            DataContext = new GameViewModel();
            UpdateGameItems();
            UpdateDeveloperItems();
            UpdateGenrePickerItems();
            UpdateImageItems();
            UpdateSubsInfo();
        }

        #region Override
        protected override void SetEditMode(bool value)
        {
            GameIDBox.SelectedItem =
            DeveloperIDBox.SelectedItem =
            ImageIDBox.SelectedItem = null;
            if (_IsInEditMode = value)
            {
                GameIDBox.Visibility = DeveloperDelButton.Visibility =
                GenreDelButton.Visibility = ImageIDBox.Visibility =
                GameButton0.Visibility = SubsInfoText.Visibility =
                    Visibility.Visible;
                DeveloperButton.Content =
                GenreButton.Content =
                ImageButton0.Content = AppResources.Lang.Rename;
                ImageButton1.Content = AppResources.Lang.Delete;
                GameButton1.Content = AppResources.Lang.Delete;
            }
            else
            {
                GameIDBox.Visibility = DeveloperDelButton.Visibility =
                GenreDelButton.Visibility = ImageIDBox.Visibility =
                GameButton0.Visibility = SubsInfoText.Visibility =
                    Visibility.Collapsed;
                DeveloperButton.Content =
                GenreButton.Content =
                GameButton1.Content =
                ImageButton1.Content = AppResources.Lang.Add;
                ImageButton0.Content = AppResources.Lang.Browse;
            }
            ClearFields();
        }

        public override void OpenFromTable(object obj)
        {
            GameIDBox.SelectedItem = obj as Data.Game;
        }

        protected override void ClearFields()
        {
            GameNameText.Text = PathText.Text = string.Empty;
            DeveloperIDBox.SelectedItem = ImageIDBox.SelectedItem = null;
            SPToggle.IsChecked = MPToggle.IsChecked = false;
            foreach (InteractiveListItem i in GenrePicker.Items)
            {
                i.IsTicked = false;
            }
        }
        #endregion

        #region Game
        private void GameIDBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectGame();
        }

        #region Game item details
        private void UpdateGameItems()
        {
            GameIDBox.ItemsSource = Global.DB.Games.ToList();
        }

        private void SelectGame()
        {
            if (!(GameIDBox.SelectedItem is Data.Game game)) return;
            GameNameText.Text = game.GameName;
            PathText.Text = game.GameExePath;
            DeveloperIDBox.SelectedItem = game.DeveloperID;
            ImageIDBox.SelectedItem = game.Image;
            SPToggle.IsChecked = game.IsSingleplayer;
            MPToggle.IsChecked = game.IsMultiplayer;
            UpdateGenrePickerItems(game);
            UpdateSubsInfo(game);
        }
        #endregion
        #endregion

        #region Path
        private void PathButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = Properties.Resources.FileFilterEXE
            };
            openFileDialog.ShowDialog();
            PathText.Text = openFileDialog.FileName;
        }
        #endregion

        #region Developer
        private void DeveloperIDBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeveloperText.Text = (DeveloperIDBox.SelectedItem as Data.Developer)?.DeveloperName;
        }

        private void DeveloperButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsInEditMode) RenameDeveloper();
            else AddDeveloper();
        }

        private void DeveloperDelButton_Click(object sender, RoutedEventArgs e)
        {
            if (Voice.Say(AppResources.Question.DeleteDeveloper, MessageBoxButton.YesNo) ==
                MessageBoxResult.Yes)
            {
                Global.DB.Developers.Remove(DeveloperIDBox.SelectedItem as Data.Developer);
                Global.DB.SaveChanges();
                UpdateDeveloperItems();
            }
        }

        #region Developer details
        /// <summary>
        /// Если возможно, по имени находит в базе данных разработчика.
        /// Иначе добавляет нового разработчика с именем name и возвращает его.
        /// </summary>
        private Data.Developer AddGetDeveloper(string name)
        {
            if (name.Length == 0) return null;
            Data.Developer dev = Global.DB.Developers.FirstOrDefault(i => i.DeveloperName == name);
            if (dev is null)
            {
                dev = Global.DB.Developers.Add(new Data.Developer
                {
                    DeveloperName = name
                });
                Global.DB.SaveChanges();
                UpdateDeveloperItems();
            }
            return dev;
        }

        private void RenameDeveloper()
        {
            if (!string.IsNullOrWhiteSpace(DeveloperText.Text) &&
            Voice.Say(AppResources.Question.RenameDeveloper, MessageBoxButton.YesNo) ==
            MessageBoxResult.Yes)
            {
                (DeveloperIDBox.SelectedItem as Data.Developer).DeveloperName = DeveloperText.Text;
                Global.DB.SaveChanges();
            }
        }

        private void AddDeveloper()
        {
            if (AddGetDeveloper(DeveloperText.Text) is null) Voice.Say(AppResources.Error.AddError);
            DeveloperText.Text = string.Empty;
        }
        private void UpdateDeveloperItems()
        {
            DeveloperIDBox.ItemsSource = Global.DB.Developers.ToList();
        }
        #endregion
        #endregion

        #region Genre
        private void GenreButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GenreText.Text) ||
                Global.DB.Genres.Any(g => g.GenreName == GenreText.Text)) return;
            if (IsInEditMode && !string.IsNullOrWhiteSpace(GenreText.Text) &&
                GenrePicker.SelectedItem != null &&
                Voice.Say(AppResources.Question.RenameGenre, MessageBoxButton.YesNo) ==
                MessageBoxResult.Yes)
            { // Rename genre
                ((GenrePicker.SelectedItem as InteractiveListItem)?.Item as Data.Genre)
                    .GenreName = GenreText.Text;
                Global.DB.SaveChanges();
            }
            else
            { // Add genre to DB
                Global.DB.Genres.Add(new Data.Genre
                {
                    GenreName = GenreText.Text
                });
                GenreText.Text = string.Empty;
            }
            Global.DB.SaveChanges();
            UpdateGenrePickerItems();
        }

        private void GenreDelButton_Click(object sender, RoutedEventArgs e)
        {
            if (Voice.Say(AppResources.Question.DeleteGenre, MessageBoxButton.YesNo) ==
                MessageBoxResult.Yes)
            {
                Global.DB.Genres.Remove((GenrePicker.SelectedItem as InteractiveListItem)?
                    .Item as Data.Genre);
                Global.DB.SaveChanges();
                UpdateGenrePickerItems();
            }
        }

        private void GenrePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GenreText.Text =
                ((GenrePicker.SelectedItem as InteractiveListItem)?.Item as Data.Genre)?
                .GenreName;
        }

        #region Genre details
        private void UpdateGenrePickerItems(Data.Game game = null)
        {
            GenrePicker.ItemsSource = (from i in Global.DB.Genres
                                       select new InteractiveListItem
                                       { Text = i.GenreName, Item = i }).ToList();
            if (game != null) foreach (InteractiveListItem i in GenrePicker.Items)
                {
                    i.IsTicked = game.Genres
                        .FirstOrDefault(g => g.GenreName == i.Text) is Data.Genre;
                }
        }

        private List<Data.Genre> GetTickedGenres()
        {
            List<Data.Genre> genres = new List<Data.Genre>();
            foreach (InteractiveListItem genre in GenrePicker.Items)
            {
                if (genre.IsTicked)
                {
                    genres.Add(genre.Item as Data.Genre); // yield return: CS1624
                }
            }
            return genres;
        }
        #endregion
        #endregion

        #region Picture
        private void ImageIDBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Data.Image pic = ImageIDBox.SelectedItem as Data.Image;
            ImageBox.Source = LoadImage(pic?.Bin);
            ImageNameText.Text = pic?.ImageName;
        }

        private void ImageButton0_Click(object sender, RoutedEventArgs e)
        {
            if (IsInEditMode) RenameImage();
            else OpenImageFromDialog();
        }

        private void ImageButton1_Click(object sender, RoutedEventArgs e)
        {
            if (IsInEditMode) DeleteImage();
            else AddImage();
        }

        #region Image details
        private Data.Image AddGetImage(string name)
        { // Добавить картинку в БД
            if (name.Length == 0) return null;
            ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
            byte[] imageBin = 
                (byte[])imageSourceConverter.ConvertTo(ImageBox.Source, typeof(byte[]));
            Data.Image pic = new Data.Image
            {
                ImageName = ImageNameText.Text,
                Bin = imageBin
            };
            pic = Global.DB.Images.Add(pic);
            Global.DB.SaveChanges();
            UpdateImageItems();
            return pic;
        }

        private void UpdateImageItems()
        {
            ImageIDBox.ItemsSource = Global.DB.Images.ToList();
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private void RenameImage()
        {
            if (!string.IsNullOrWhiteSpace(ImageNameText.Text))
            {
                (ImageIDBox.SelectedItem as Data.Image).ImageName = ImageNameText.Text;
                Global.DB.SaveChanges();
            }
        }

        private void OpenImageFromDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = Properties.Resources.FileFilterIMG,
                Multiselect = false
            };
            bool? ofdShow = openFileDialog.ShowDialog();
            if (ofdShow.HasValue && ofdShow.Value)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(openFileDialog.FileName);
                bitmapImage.EndInit();
                ImageBox.Source = bitmapImage;
                ImageNameText.Text = openFileDialog.FileName
                    .Remove(0, openFileDialog.FileName.LastIndexOf('\\') + 1);
            }
        }

        private void DeleteImage()
        {
            if (Voice.Say(AppResources.Question.DeleteImage, MessageBoxButton.YesNo) ==
                MessageBoxResult.Yes)
            {
                Global.DB.Images.Remove(ImageIDBox.SelectedItem as Data.Image);
                Global.DB.SaveChanges();
                UpdateImageItems();
            }
        }

        private void AddImage()
        {
            Voice.Say(AddGetImage(ImageNameText.Text) is null ?
                AppResources.Error.NameNotEntered : AppResources.Lang.AddedSuccessfully);
        }
        #endregion
        #endregion

        #region Subs
        private void UpdateSubsInfo(Data.Game game = null)
        {
            int numberOfSubs = (game is null ? 0 : game.Subscriptions.Count);
            int numberOfRrates = 0;
            double avgRate = 0;
            if (game != null && game.Subscriptions.Count > 0)
            {
                var rates = game.Subscriptions.Where(s => s.Rate != null);
                numberOfSubs = rates.Count();
                if (numberOfRrates > 0) avgRate = rates.Average(s => (double)s.Rate);
            }
            SubsInfoText.Text = AppResources.Lang.SubsRatesAndAvg
                .Replace("{subs}", numberOfSubs.ToString())
                .Replace("{rates}", numberOfRrates.ToString())
                .Replace("{avg}", avgRate.ToString());
        }
        #endregion

        #region Submit
        private void GameButton0_Click(object sender, RoutedEventArgs e)
        {
            SubmitGameEdits();
        }

        private void GameButton1_Click(object sender, RoutedEventArgs e)
        {
            if (IsInEditMode) DeleteGame();
            else AddGame();
        }

        #region Submission details
        private void SubmitGameEdits()
        {
            if (!(GameIDBox.SelectedItem is Data.Game game)) return;
            if (Voice.Say(AppResources.Question.UpdateGame, MessageBoxButton.YesNo) ==
                MessageBoxResult.No) return;
            if (!string.IsNullOrWhiteSpace(GameNameText.Text))
            {
                game.GameName = GameNameText.Text;
            }
            game.GameExePath = PathText.Text;
            game.Developer = DeveloperIDBox.SelectedItem as Data.Developer;
            game.Image = ImageIDBox.SelectedItem as Data.Image;
            game.Genres = GetTickedGenres();
            game.IsSingleplayer = SPToggle.IsChecked;
            game.IsMultiplayer = MPToggle.IsChecked;
            Global.DB.SaveChanges();
        }

        private void DeleteGame()
        {
            if ((GameIDBox.SelectedItem is Data.Game) &
                Voice.Say(AppResources.Question.DeleteGame, MessageBoxButton.YesNo) ==
                MessageBoxResult.Yes)
            {
                Global.DB.Games.Remove(GameIDBox.SelectedItem as Data.Game);
                Global.DB.SaveChanges();
                UpdateGameItems();
            }
        }

        private void AddGame()
        {
            if (string.IsNullOrWhiteSpace(GameNameText.Text)) return;
            Data.Game game = new Data.Game
            {
                GameName = GameNameText.Text,
                Developer = AddGetDeveloper(DeveloperText.Text),
                GameExePath = PathText.Text,
                Image = AddGetImage(ImageNameText.Text),
                IsSingleplayer = SPToggle.IsChecked,
                IsMultiplayer = MPToggle.IsChecked,
                Genres = GetTickedGenres()
            };
            Global.DB.Games.Add(game);
            Global.DB.SaveChanges();
            UpdateGameItems();
            ClearFields();
            Voice.Say(AppResources.Lang.GameAddedToDB);
        }
        #endregion
        #endregion
    }
}
