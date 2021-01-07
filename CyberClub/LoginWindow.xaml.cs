﻿using CyberClub.AppResources;
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

        private void InitDB()
        {
            Voice loadingMsg = new Voice();
            loadingMsg.ErrorText.Text = "Establishing database connection. Please wait...";
            loadingMsg.OK.Visibility = loadingMsg.Cancel.Visibility =
            loadingMsg.Yes.Visibility = loadingMsg.No.Visibility =
                Visibility.Collapsed;
            loadingMsg.Show();
            try
            {
                Global.DB = new DBContext();
            }
            catch
            {
                loadingMsg.Close();
                loadingMsg = new Voice();
                loadingMsg.ErrorText.Text =
                    "Database connection failed.\n" +
                    "Closing the app in a few seconds.";
                loadingMsg.OK.Visibility = loadingMsg.Cancel.Visibility =
                loadingMsg.Yes.Visibility = loadingMsg.No.Visibility =
                    Visibility.Collapsed;
                loadingMsg.Show();
                System.Threading.Thread.Sleep(5000);
                loadingMsg.Close();
                Close();
            }
            loadingMsg.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginInput.Text)) return;
            Global.User = Global.DB.Users.FirstOrDefault(user =>
                user.UserName == LoginInput.Text &&
                user.Passwd == PasswordInput.Password);
            if (Global.User is null)
            {
                Voice.Say(Lang.LoginPasswordNotFound);
                return;
            }
            LoginInput.Text = PasswordInput.Password = string.Empty;
            string authname = Global.DB.Hierarchy.Where(level => 
                level.AuthID == Global.User.Authority).First().AuthName;
            if (authname == "banned")
            {
                Voice.Say(Lang.YouAreBanned);
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
