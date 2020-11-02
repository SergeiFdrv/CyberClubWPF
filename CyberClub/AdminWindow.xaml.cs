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

        private GamesPage GamesPage { get; } = new GamesPage();

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Owner.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(GamesPage);
        }

        private void GamesButton_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(GamesPage);
        }

        private void AccountsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MessagesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TableTabButton_Checked(object sender, RoutedEventArgs e)
        {
            AddTabButton.IsChecked = EditTabButton.IsChecked = false;
        }

        private void AddTabButton_Checked(object sender, RoutedEventArgs e)
        {
            TableTabButton.IsChecked = EditTabButton.IsChecked = false;
            (PageFrame.Content as GamesPage).IsInEditMode = false;
        }

        private void EditTabButton_Checked(object sender, RoutedEventArgs e)
        {
            TableTabButton.IsChecked = AddTabButton.IsChecked = false;
            (PageFrame.Content as GamesPage).IsInEditMode = true;
        }
    }
}
