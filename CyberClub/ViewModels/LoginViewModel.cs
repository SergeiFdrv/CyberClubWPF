using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberClub.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private RelayCommand _FocusOnNext;
        public RelayCommand FocusOnNext
        {
            get => _FocusOnNext ?? (_FocusOnNext = new RelayCommand(obj =>
            {
                //
            }));
        }
    }
}
