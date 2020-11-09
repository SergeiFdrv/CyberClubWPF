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
        private bool _IsTicked;
        public bool IsTicked
        {
            get => _IsTicked;
            set
            {
                _IsTicked = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsTicked"));
            }
        }

        private string _Text;
        public string Text
        {
            get => _Text;
            set
            {
                _Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
            }
        }

        public object Item { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public override string ToString() => Text;
    }
}
