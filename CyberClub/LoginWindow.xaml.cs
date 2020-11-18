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
        }

        private static App Global => Application.Current as App;

        private List<User> Users { get; } = Global.DB.Users.ToList();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginInput.Text)) return;
            User selection = Users.FirstOrDefault(user =>
                user.UserName == LoginInput.Text &&
                user.Passwd == PasswordInput.Password);
            if (selection is null)
            {
                Voice.Say(Lang.LoginPasswordNotFound);
                return;
            }
            LoginInput.Text = PasswordInput.Password = string.Empty;
            Global.UserID = selection.UserID;
            string authname = Global.DB.Hierarchy.Where(level => 
                level.AuthID == selection.Authority).First().AuthName;
            if (authname == "banned")
            {
                Voice.Say(Lang.YouAreBanned);
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
