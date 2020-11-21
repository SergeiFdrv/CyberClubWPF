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
    /// Логика взаимодействия для MessagePage.xaml
    /// </summary>
    public partial class MessagePage : AdminEditorPage
    {
        public MessagePage()
        {
            InitializeComponent();
        }

        public override void OpenFromTable(object obj)
        {
            if (!(obj is Data.Feedback msg)) return;
            UserNameText.Text = msg.User is null ? string.Empty :
                msg.User.UserName + " (" + msg.User.UserID + ')';
            TimeText.Text = msg.Dt.ToString("G");
            TopicText.Text = msg.Briefly;
            TextText.Text = msg.InDetails;
        }

        protected override void ClearFields()
        {
            UserNameText.Text = TimeText.Text = TopicText.Text = TextText.Text =
                string.Empty;
        }

        protected override void SetEditMode(bool value)
        {
            UserNameText.IsEnabled =
            TimeText.IsEnabled =
            TopicText.IsEnabled =
            TextText.IsEnabled =
                !(_IsInEditMode = value);
        }
    }
}
