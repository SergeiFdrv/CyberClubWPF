using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CyberClub.Pages
{
    /// <summary>
    /// Логика взаимодействия для GamesPage.xaml
    /// </summary>
    public partial class GamesPage : Page
    {
        public GamesPage()
        {
            InitializeComponent();
            GameIDBox.ItemsSource = new System.Collections.ArrayList(Global.DB.Games.ToList());
            DevText.ItemsSource = new System.Collections.ArrayList(Global.DB.Devs.ToList());
            GenrePicker.ItemsSource = new System.Collections.ArrayList(Global.DB.Genres.ToList());
        }

        #region Properties
        private static App Global => Application.Current as App;

        private bool _IsInEditMode;
        public bool IsInEditMode
        {
            get => _IsInEditMode;
            set
            {
                if (_IsInEditMode = value)
                {
                    GameIDBox.Visibility = DevDelButton.Visibility =
                    GenreDelButton.Visibility = PicIDBox.Visibility =
                        Visibility.Visible;
                    PathButton.Content = DevButton.Content =
                    GenreButton.Content = AppResources.Lang.Edit;
                }
                else
                {
                    GameIDBox.Visibility = DevDelButton.Visibility =
                    GenreDelButton.Visibility = PicIDBox.Visibility =
                        Visibility.Collapsed;
                    PathButton.Content = DevButton.Content =
                    GenreButton.Content = AppResources.Lang.Add;
                }
            }
        }
        #endregion

        #region Name
        private void GameIDBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { // Выбор редактируемой записи об игре
            if (!int.TryParse(GameIDBox.Text, out int id))
            {
                GameNameText.Text = string.Empty;
                GameNameText.IsEnabled = false;
                //GEditSubsN.Text = GEditRatesN.Text = $"{0}";
                //GEditRatingN.Text = $"{0.0}";
                return;
            }
            GameNameText.IsEnabled = true;
            Data.Game game = GameIDBox.SelectedItem as Data.Game;
            /*
            using (SqlConnection conn = new SqlConnection(LoginForm.CS))
            {
                if (!ConnOpen(conn)) return;
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT TOP 1 gamename, gamelink, gamepic, " +
                    "singleplayer, multiplayer, devname, COUNT(who) AS subs, " +
                    "COUNT(rate) AS rates, CONVERT(varchar, AVG(CAST(rate AS float))) " +
                    "AS rating FROM games LEFT JOIN subscriptions ON gameid = game " +
                    "LEFT JOIN devs ON madeby = devid WHERE gameid = @gid GROUP BY " +
                    "multiplayer, singleplayer, gamepic, gamelink, " +
                    "devname, gamename, gameid";
                command.Parameters.Add(new SqlParameter("@gid", id));
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    GEditName.Text = dataReader["gamename"].ToString();
                    GEditLink.Text = dataReader["gamelink"].ToString();
                    GEditDev.Text = dataReader["devname"].ToString();
                    GEditPicID.Text = dataReader["gamepic"].ToString();
                    GEditSingleCB.Checked = (bool)dataReader["singleplayer"];
                    GEditMultiCB.Checked = (bool)dataReader["multiplayer"];
                    GEditSubsN.Text = dataReader["subs"].ToString();
                    GEditRatesN.Text = dataReader["rates"].ToString();
                    GEditRatingN.Text = dataReader["rating"].ToString();
                    dataReader.Close();
                    command.CommandText = "SELECT genrename FROM gamegenre " +
                        "LEFT JOIN genres ON genre = genreid WHERE game = @gid";
                    dataReader = command.ExecuteReader();
                    for (int i = 0; i < GEditGenresCLB.Items.Count; i++)
                        GEditGenresCLB.SetItemChecked(i, false);
                    while (dataReader.Read())
                        GEditGenresCLB.SetItemChecked(GEditGenresCLB.Items.IndexOf(
                            dataReader["genrename"].ToString()), true);
                }
            }*/
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
        private void DevButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsInEditMode)
            {// Переименовать разработчика в БД

            }
            else
            {// Записать разработчика в БД
                if (AddGetDev(DevText.SelectedItem.ToString()) is null) Voice.Say(AppResources.Lang.AddError);
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
        private void GenreButton_Click(object sender, RoutedEventArgs e)
        { // Записать жанр в БД
            if (GenreText.Text.Length == 0) return;
            Global.DB.Genres.Add(new Data.Genre
            {
                GenreName = GenreText.Text
            });
            GenreText.Text = string.Empty;
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
            byte[] imageBin = (byte[])imageSourceConverter.ConvertTo(ImageBox.Source, typeof(byte[]));
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
        #endregion
    }
}
