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

namespace CyberClub
{
    /// <summary>
    /// Логика взаимодействия для Voice.xaml.<br/>
    /// Voice — наш "дизайнерский" MessageBox. Ничего лишнего
    /// </summary>
    public partial class Voice : Window
    {
        public Voice()
        {
            InitializeComponent();
        }

        public string ResultString => ResponceInput.Text;

        public static MessageBoxResult Say(string text, MessageBoxButton buttons = MessageBoxButton.OK)
        {
            Voice v = new Voice();
            v.ErrorText.Text = text;
            switch (buttons)
            {
                case MessageBoxButton.OK:
                    v.OK.Visibility = Visibility.Visible;
                    v.Yes.Visibility = v.No.Visibility = v.Cancel.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.OKCancel:
                    v.OK.Visibility = v.Cancel.Visibility = Visibility.Visible;
                    v.Yes.Visibility = v.No.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.YesNo:
                    v.Yes.Visibility = v.No.Visibility = Visibility.Visible;
                    v.OK.Visibility = v.Cancel.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.YesNoCancel:
                    v.Yes.Visibility = v.No.Visibility = v.Cancel.Visibility = Visibility.Visible;
                    v.OK.Visibility = Visibility.Collapsed;
                    break;
            }
            bool? res = v.ShowDialog();
            if (res is null)
            {
                return MessageBoxResult.Cancel;
            }
            if (res.Value)
            {
                if (buttons == MessageBoxButton.YesNo || buttons == MessageBoxButton.YesNoCancel)
                {
                    return MessageBoxResult.Yes;
                }
                else return MessageBoxResult.OK;
            }
            return MessageBoxResult.No;
        }

        public static MessageBoxResult Prompt(string request, out string response)
        {
            var form = new Voice();
            form.ErrorText.Text = request;
            form.OK.Visibility = form.Cancel.Visibility = Visibility.Visible;
            form.Yes.Visibility = form.No.Visibility = Visibility.Collapsed;
            bool? res = form.ShowDialog();
            if (res is null)
            {
                response = string.Empty;
                return MessageBoxResult.Cancel;
            }
            if (res.Value)
            {
                response = form.ResultString;
                return MessageBoxResult.OK;
            }
            response = string.Empty;
            return MessageBoxResult.Cancel;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = null;
            Close();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
