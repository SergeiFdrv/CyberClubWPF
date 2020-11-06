﻿using System;
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
using System.Windows.Shapes;
using CyberClub.Pages;

namespace CyberClub
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }
        protected static App Global => Application.Current as App;

        private GamesPage GamesPage { get; } = new GamesPage();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GamesButton.IsChecked = TableTabButton.IsChecked = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Owner.Show();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void GamesButton_Checked(object sender, RoutedEventArgs e)
        {
            AccountsButton.IsChecked = MessagesButton.IsChecked = GamesButton.IsEnabled =
                !(AccountsButton.IsEnabled = MessagesButton.IsEnabled = true);
            DataGridTable.ItemsSource = Global.DB.Games.ToList();
            PageFrame.Navigate(GamesPage);
        }

        private void AccountsButton_Checked(object sender, RoutedEventArgs e)
        {
            GamesButton.IsChecked = MessagesButton.IsChecked = AccountsButton.IsEnabled =
                !(GamesButton.IsEnabled = MessagesButton.IsEnabled = true);
            DataGridTable.ItemsSource = Global.DB.Users.ToList();
            //PageFrame.Navigate(AccountsPage);
        }

        private void MessagesButton_Checked(object sender, RoutedEventArgs e)
        {
            GamesButton.IsChecked = AccountsButton.IsChecked = MessagesButton.IsEnabled =
                !(GamesButton.IsEnabled = AccountsButton.IsEnabled = true);
            DataGridTable.ItemsSource = Global.DB.Feedbacks.ToList();
            //PageFrame.Navigate(MessagesPage);
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TableTabButton_Checked(object sender, RoutedEventArgs e)
        {
            AddTabButton.IsChecked = EditTabButton.IsChecked = TableTabButton.IsEnabled =
                !(AddTabButton.IsEnabled = EditTabButton.IsEnabled = true);
            DataGridTable.Visibility = Visibility.Visible;
            PageFrame.Visibility = Visibility.Collapsed;
        }

        private void AddTabButton_Checked(object sender, RoutedEventArgs e)
        {
            TableTabButton.IsChecked = EditTabButton.IsChecked = AddTabButton.IsEnabled =
                !(TableTabButton.IsEnabled = EditTabButton.IsEnabled = true);
            (PageFrame.Content as AdminEditorPage).IsInEditMode = false;
            PageFrame.Visibility = Visibility.Visible;
            DataGridTable.Visibility = Visibility.Collapsed;
        }

        private void EditTabButton_Checked(object sender, RoutedEventArgs e)
        {
            TableTabButton.IsChecked = AddTabButton.IsChecked = EditTabButton.IsEnabled =
                !(TableTabButton.IsEnabled = AddTabButton.IsEnabled = true);
            (PageFrame.Content as AdminEditorPage).IsInEditMode = true;
            PageFrame.Visibility = Visibility.Visible;
            DataGridTable.Visibility = Visibility.Collapsed;
        }
    }
}
