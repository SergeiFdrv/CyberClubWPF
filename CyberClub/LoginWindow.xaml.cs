using CyberClub.AppResources;
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
using CyberClub.Data;

namespace CyberClub
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            InitDB();
        }

        private static App Global => Application.Current as App;

        private async void InitDB()
        {
            LoginButton.IsEnabled = false;
            DBContext db = await Global.SetDBAsync();
            if (db is null)
            {
                Voice loadingMsg = new Voice();
                loadingMsg.ErrorText.Text = "Database connection failed.";
                loadingMsg.OK.Visibility = loadingMsg.Yes.Visibility =
                    loadingMsg.No.Visibility = Visibility.Collapsed;
                loadingMsg.Cancel.Visibility = Visibility.Visible;
                loadingMsg.Cancel.Content = "Close";
                loadingMsg.Cancel.Click += delegate
                {
                    loadingMsg.Close();
                    Close();
                };
                loadingMsg.ShowDialog();
            }
            LoginButton.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginInput.Text)) return;
            Global.User = Global.DB.Users.FirstOrDefault(user =>
                user.UserName == LoginInput.Text);
            if (Global.User is null || Global.User.UserPass != PasswordInput.Password)
            {
                Voice.Say(Error.LoginPasswordNotFound);
                return;
            }
            LoginInput.Text = PasswordInput.Password = string.Empty;
            string authname = "admin";//Global.DB.Hierarchy.Where(level => 
                //level.AuthID == Global.User.Authority).First().AuthName;
            if (authname == "banned")
            {
                Voice.Say(Error.YouAreBanned);
                Global.User = null;
            }
            else
            {
                Hide();
                Window window;
                if (authname == "admin")
                {
                    (window = new AdminWindow { Owner = this }).ShowDialog();
                }
                else
                {
                    (window = new UserWindow { Owner = this }).ShowDialog();
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
