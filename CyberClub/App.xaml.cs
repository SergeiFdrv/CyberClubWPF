using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CyberClub.Data;

namespace CyberClub
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
        }

        public int UserID { get; set; }

        public User User { get; set; }

        private DBContext _DB;
        public DBContext DB
        {
            get => _DB;
            set
            {
                if (_DB is DBContext) return;
                _DB = value;
            }
        }

        private void Application_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            _DB?.Dispose();
        }

        private void ComboBox_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            (sender as ComboBox).Focus();
        }
    }
}
