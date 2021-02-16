using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberClub.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        private string _UserName = "Admin";
        public string UserName
        {
            get => _UserName;
            set
            {
                _UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string PasswordText { get; set; }
    }
}
