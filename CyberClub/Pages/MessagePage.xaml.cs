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
            if (!(obj is Data.TextMessage msg)) return;
            UserNameText.Text = msg.Reciever is null ? string.Empty :
                msg.Reciever.UserName + " (" + msg.Reciever.UserID + ')';
            TimeText.Text = msg.DT.ToString("G");
            TopicText.Text = msg.ShortText;
            TextText.Text = msg.LongText;
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
