using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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
            Exit += App_Exit;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            DB.Dispose();
        }

        public int UserID { get; set; }
        public DBContext DB { get; } = new DBContext();
    }
}
