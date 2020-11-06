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
            GameIDBox.ItemsSource = Global.DB.Games.ToList();
            DevIDBox.ItemsSource = Global.DB.Devs.ToList();
            UpdateGenrePickerItems();
            PicIDBox.ItemsSource = Global.DB.Pics.ToList();
        }

        #region Override
        protected override void SetEditMode(bool value)
        {
            GameIDBox.SelectedItem =
            DevIDBox.SelectedItem =
            PicIDBox.SelectedItem = null;
            if (_IsInEditMode = value)
            {
                GameIDBox.Visibility = DevDelButton.Visibility =
                GenreDelButton.Visibility = PicIDBox.Visibility =
                GameButton0.Visibility =
                    Visibility.Visible;
                DevButton.Content =
                GenreButton.Content =
                PicButton0.Content = AppResources.Lang.Rename;
                PicButton1.Content = AppResources.Lang.Delete;
                GameButton0.Visibility = Visibility.Visible;
                GameButton1.Content = AppResources.Lang.Delete;
            }
            else
            {
                GameIDBox.Visibility = DevDelButton.Visibility =
                GenreDelButton.Visibility = PicIDBox.Visibility =
                GameButton0.Visibility =
                    Visibility.Collapsed;
                DevButton.Content =
                GenreButton.Content =
                PicButton1.Content = AppResources.Lang.Add;
                PicButton0.Content = AppResources.Lang.Browse;
                GameButton1.Content = AppResources.Lang.Add;
                ClearFields();
            }
        }
        #endregion

        #region Common
        // TODO: DRY. Наплодить методов
        private void ClearFields()
        {
            GameNameText.Text = PathText.Text = string.Empty;
            DevIDBox.SelectedItem = PicIDBox.SelectedItem = null;
            SPToggle.IsChecked = MPToggle.IsChecked = false;
            foreach (InteractiveListItem i in GenrePicker.Items)
            {
                i.IsIncluded = false;
            }
        }
        #endregion

        #region Name
        private void GameIDBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { // Выбор редактируемой записи об игре
            if (!(GameIDBox.SelectedItem is Data.Game game)) return;
            GameNameText.Text = game.GameName;
            PathText.Text = game.GameLink;
            DevIDBox.SelectedItem = game.Dev;
            PicIDBox.SelectedItem = game.Pic;
            SPToggle.IsChecked = game.Singleplayer;
            MPToggle.IsChecked = game.Multiplayer;
            // Subs, Rating
            // Genres
            foreach (InteractiveListItem i in GenrePicker.Items)
            {
                i.IsIncluded = game.Genres
                    .FirstOrDefault(g => g.GenreName == i.Name) is Data.Genre;
            }
        }
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
        private void DevIDBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DevText.Text = (DevIDBox.SelectedItem as Data.Dev)?.DevName;
        }

        private void DevButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsInEditMode)
            {// Переименовать разработчика в БД

            }
            else
            {// Записать разработчика в БД
                if (AddGetDev(DevText.Text) is null) Voice.Say(AppResources.Lang.AddError);
                DevText.Text = string.Empty;
            }
        }

        private void DevDelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Если возможно, по имени находит в базе данных разработчика.
        /// Иначе добавляет нового разработчика с именем name и возвращает его.
        /// </summary>
        private static Data.Dev AddGetDev(string name)
        {
            if (name.Length == 0) return null;
            Data.Dev dev = Global.DB.Devs.First(i => i.DevName == name);
            if (dev is null)
            {
                dev = Global.DB.Devs.Add(new Data.Dev
                {
                    DevName = name
                });
            }
            return dev;
        }
        #endregion

        #region Genre
        private void UpdateGenrePickerItems()
        {
            GenrePicker.ItemsSource = (from i in Global.DB.Genres
                             select new InteractiveListItem { Name = i.GenreName }).ToList();
        }

        private void GenreButton_Click(object sender, RoutedEventArgs e)
        { // Записать жанр в БД
            if (GenreText.Text.Length == 0) return;
            Global.DB.Genres.Add(new Data.Genre
            {
                GenreName = GenreText.Text
            });
            GenreText.Text = string.Empty;
            Global.DB.SaveChanges();
            UpdateGenrePickerItems();
        }

        private void GenrePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        #endregion

        #region Picture
        private void PicButton0_Click(object sender, RoutedEventArgs e)
        {
            if (IsInEditMode)
            {// Переименовать картинку в БД

            }
            else
            {// Найти и отобразить картинку
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
                    PicNameText.Text = openFileDialog.FileName
                        .Remove(0, openFileDialog.FileName.LastIndexOf('\\') + 1);
                }
            }
        }

        private void PicButton1_Click(object sender, RoutedEventArgs e)
        {
            Voice.Say(AddGetPic(PicNameText.Text) is null ?
                AppResources.Lang.NameNotEntered : AppResources.Lang.AddedSuccessfully);
        }

        private Data.Pic AddGetPic(string name)
        { // Добавить картинку в БД
            if (name.Length == 0) return null;
            ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
            byte[] imageBin = 
                (byte[])imageSourceConverter.ConvertTo(ImageBox.Source, typeof(byte[]));
            Data.Pic pic = new Data.Pic
            {
                PicName = PicNameText.Text,
                Bin = imageBin
            };
            pic = Global.DB.Pics.Add(pic);
            return pic;
        }
        #endregion

        #region Submit
        private void GameButton0_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GameButton1_Click(object sender, RoutedEventArgs e)
        {
            if (IsInEditMode)
            { // Удалить игру

            }
            else
            { // Добавить в базу новую игру
                if (GameNameText.Text.Length == 0) return;
                ICollection<Data.Genre> genres = (ICollection<Data.Genre>)Global.DB.Genres
                    .Select(g => GenrePicker.SelectedItems.Contains(g));
                Data.Game game = new Data.Game
                {
                    GameName = GameNameText.Text,
                    MadeBy = AddGetDev(DevText.Text)?.DevID,
                    GameLink = PathText.Text,
                    GamePic = AddGetPic(PicNameText.Text)?.PicID,
                    Singleplayer = SPToggle.IsChecked,
                    Multiplayer = MPToggle.IsChecked,
                    Genres = genres
                };
                game = Global.DB.Games.Add(game);
                Voice.Say(AppResources.Lang.GameAddedToDB);
            }
        }
        #endregion
    }
}
