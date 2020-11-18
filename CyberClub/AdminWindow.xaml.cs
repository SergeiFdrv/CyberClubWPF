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

        #region Grid
        #region Games
        private GamesPage GamesPage { get; } = new GamesPage();

        private List<DataGridColumn> GamesDGColumns { get; } =
            new List<DataGridColumn>
        {
                new DataGridTextColumn
                {
                    Header = "ID",
                    Binding = new Binding("GameID")
                },
                new DataGridTextColumn
                {
                    Header = "Name",
                    Binding = new Binding("GameName")
                },
                new DataGridTextColumn
                {
                    Header = "Developed by",
                    Binding = new Binding("Dev.DevName")
                },
                new DataGridTextColumn
                {
                    Header = "Subs",
                    Binding = new Binding("Subscriptions.Count")
                }
        };
        #endregion

        #region Accounts
        //private AccountsPage AccountsPage { get; } = new AccountsPage();

        private List<DataGridColumn> AccountsDGColumns { get; } =
            new List<DataGridColumn>
        {
                new DataGridTextColumn
                {
                    Header = "ID",
                    Binding = new Binding("UserID")
                },
                new DataGridTextColumn
                {
                    Header = "Name",
                    Binding = new Binding("UserName")
                },
                new DataGridTextColumn
                {
                    Header = "Level",
                    Binding = new Binding("Hierarchy.AuthName")
                }
        };
        #endregion

        #region Messages
        //private MessagesPage MessagesPage { get; } = new MessagesPage();

        private List<DataGridColumn> MessagesDGColumns { get; } =
            new List<DataGridColumn>
        {
                new DataGridTextColumn
                {
                    Header = "ID",
                    Binding = new Binding("MessageID")
                },
                new DataGridCheckBoxColumn
                {
                    Header = "Rd",
                    Binding = new Binding("IsRead")
                },
                new DataGridTextColumn
                {
                    Header = "From",
                    Binding = new Binding("User.UserName")
                },
                new DataGridTextColumn
                {
                    Header = "Topic",
                    Binding = new Binding("Briefly")
                }
        };
        #endregion
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GamesButton.IsChecked = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Owner.Show();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void SetDGColumns(IEnumerable<DataGridColumn> columns)
        {
            DataGridTable.Columns.Clear();
            foreach (var i in columns)
            {
                DataGridTable.Columns.Add(i);
            }
        }

        private void GamesButton_Checked(object sender, RoutedEventArgs e)
        {
            TableTabButton.IsChecked = true;
            ChangeTab(GamesButton, AccountsButton, MessagesButton);
            DataGridTable.ItemsSource = Global.DB.Games.ToList();
            SetDGColumns(GamesDGColumns);
            PageFrame.Navigate(GamesPage);
        }

        private void AccountsButton_Checked(object sender, RoutedEventArgs e)
        {
            TableTabButton.IsChecked = true;
            ChangeTab(AccountsButton, GamesButton, MessagesButton);
            DataGridTable.ItemsSource = Global.DB.Users.ToList();
            SetDGColumns(AccountsDGColumns);
            //PageFrame.Navigate(AccountsPage);
        }

        private void MessagesButton_Checked(object sender, RoutedEventArgs e)
        {
            TableTabButton.IsChecked = true;
            ChangeTab(MessagesButton, GamesButton, AccountsButton);
            DataGridTable.ItemsSource = Global.DB.Feedbacks.ToList();
            SetDGColumns(MessagesDGColumns);
            //PageFrame.Navigate(MessagesPage);
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TableTabButton_Checked(object sender, RoutedEventArgs e)
        {
            ChangeTab(TableTabButton, AddTabButton, EditTabButton);
            DataGridTable.Visibility = Visibility.Visible;
            PageFrame.Visibility = Visibility.Collapsed;
        }

        private void AddTabButton_Checked(object sender, RoutedEventArgs e)
        {
            ChangeTab(AddTabButton, TableTabButton, EditTabButton);
            (PageFrame.Content as AdminEditorPage).IsInEditMode = false;
            PageFrame.Visibility = Visibility.Visible;
            DataGridTable.Visibility = Visibility.Collapsed;
        }

        private void EditTabButton_Checked(object sender, RoutedEventArgs e)
        {
            ChangeTab(EditTabButton, TableTabButton, AddTabButton);
            (PageFrame.Content as AdminEditorPage).IsInEditMode = true;
            PageFrame.Visibility = Visibility.Visible;
            DataGridTable.Visibility = Visibility.Collapsed;
        }

        private static void ChangeTab(ToggleButton On, ToggleButton Off0, ToggleButton Off1)
        {
            Off0.IsChecked = Off1.IsChecked = On.IsEnabled =
                !(Off0.IsEnabled = Off1.IsEnabled = true);
        }

        private void DataGridTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditTabButton.IsChecked = true;
            (PageFrame.Content as AdminEditorPage).OpenFromTable(DataGridTable.SelectedItem);
        }
    }
}
