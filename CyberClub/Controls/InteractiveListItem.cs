using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberClub.Controls
{
    public class InteractiveListItem : INotifyPropertyChanged
    {
        private bool _IsIncluded;
        public bool IsIncluded
        {
            get => _IsIncluded;
            set
            {
                _IsIncluded = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsIncluded"));
            }
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
