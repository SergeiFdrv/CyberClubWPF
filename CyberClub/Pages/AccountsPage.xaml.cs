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
    /// Логика взаимодействия для AccountsPage.xaml
    /// </summary>
    public partial class AccountsPage : AdminEditorPage
    {
        public AccountsPage()
        {
            InitializeComponent();
            UpdateAccountItems();
            LevelBox.ItemsSource = Global.DB.Hierarchy.ToList();
        }

        public override void OpenFromTable(object obj)
        {
            UserIDBox.SelectedItem = obj as Data.User;
        }

        protected override void SetEditMode(bool value)
        {
            UserIDBox.SelectedItem = LevelBox.SelectedItem = null;
            if (_IsInEditMode = value)
            {
                UserIDBox.Visibility =
                UserSubmitButton.Visibility =
                UserDeleteButton.Visibility = Visibility.Visible;
                UserAddButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                UserIDBox.Visibility =
                UserSubmitButton.Visibility =
                UserDeleteButton.Visibility = Visibility.Collapsed;
                UserAddButton.Visibility = Visibility.Visible;
            }
            ClearFields();
        }

        protected override void ClearFields()
        {
            UserNameText.Text = EmailText.Text = AboutText.Text =
                Password = string.Empty;
            LevelBox.SelectedItem = null;
        }

        private string Password
        {
            get
            {
                if (PasswordToggle.IsChecked == true)
                {
                    return PasswordHideText.Password;
                }
                return PasswordShowText.Text;
            }
            set
            {
                if (PasswordToggle.IsChecked == true)
                {
                    PasswordHideText.Password = value;
                }
                else PasswordShowText.Text = value;
            }
        }

        private void UpdateAccountItems()
        {
            UserIDBox.ItemsSource = Global.DB.Users.ToList();
        }

        private void SelectAccount()
        {
            if (!(UserIDBox.SelectedItem is Data.User user)) return;
            UserNameText.Text = user.UserName;
            EmailText.Text = user.EMail;
            AboutText.Text = user.Info;
            LevelBox.SelectedItem = user.Hierarchy;
            Password = user.Passwd;
        }

        private void UserIDBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectAccount();
        }

        private void PasswordToggle_Checked(object sender, RoutedEventArgs e)
        {
            PasswordHideText.Visibility = Visibility.Visible;
            PasswordShowText.Visibility = Visibility.Collapsed;
            PasswordHideText.Password = PasswordShowText.Text;
        }

        private void PasswordToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordShowText.Visibility = Visibility.Visible;
            PasswordHideText.Visibility = Visibility.Collapsed;
            PasswordShowText.Text = PasswordHideText.Password;
        }

        private void UserAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserNameText.Text)) return;
            Data.User user = new Data.User
            {
                UserName = UserNameText.Text,
                EMail = EmailText.Text,
                Info = AboutText.Text,
                Passwd = Password,
                Hierarchy = LevelBox.SelectedItem as Data.Hierarchy
            };
            Global.DB.Users.Add(user);
            Global.DB.SaveChanges();
            UpdateAccountItems();
            ClearFields();
            Voice.Say(AppResources.Lang.AddedSuccessfully);
        }

        private void UserSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(UserIDBox.SelectedItem is Data.User user)) return;
            if (Voice.Say(AppResources.Question.UpdateAccount, MessageBoxButton.YesNo) ==
                MessageBoxResult.No) return;
            if (!string.IsNullOrWhiteSpace(UserNameText.Text))
            {
                user.UserName = UserNameText.Text;
            }
            user.EMail = EmailText.Text;
            user.Info = AboutText.Text;
            user.Passwd = Password;
            user.Hierarchy = LevelBox.SelectedItem as Data.Hierarchy;
            Global.DB.SaveChanges();
        }

        private void UserDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if ((UserIDBox.SelectedItem is Data.Game) &
                Voice.Say(AppResources.Question.DeleteAccount, MessageBoxButton.YesNo) ==
                MessageBoxResult.Yes)
            {
                Global.DB.Users.Remove(UserIDBox.SelectedItem as Data.User);
                Global.DB.SaveChanges();
                UpdateAccountItems();
            }
        }
    }
}
